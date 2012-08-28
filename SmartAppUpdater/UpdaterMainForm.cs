using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommonLib;
using System.Net;
using System.IO;
using System.Xml;
using System.Reflection;

namespace SmartAppUpdater
{
    public partial class UpdaterMainForm : Form
    {
        private const string ENDL = "\r\n";

        public UpdaterMainForm()
        {
            InitializeComponent();
        }

        protected void AddStatusLine(string message)
        {
            textBox1.Text += message;
            Application.DoEvents();
        }

        protected bool DownloadVersionFile()
        {
            textBox1.Text = string.Empty;
            WebClient wc = new WebClient();
            bool bFileDownloadOK = false;

            if (!Directory.Exists(Application.StartupPath + "tmpUpdate"))
            {
                Directory.CreateDirectory(Application.StartupPath + Path.DirectorySeparatorChar + "tmpUpdate");
            }
            try
            {
                AddStatusLine(Program.LangSys.C("Cheking for updates") + ENDL);
                string downloadURL = Program.FileUrl + Program.VersionInfoFile;
                if (checkBox1.Checked) // version Beta
                    downloadURL = Program.BetaFileUrl + Program.VersionInfoFile;
                string localFile = Application.StartupPath + Path.DirectorySeparatorChar + "tmpUpdate" + Path.DirectorySeparatorChar + Program.VersionInfoFile;
                wc.DownloadFile(downloadURL, localFile);
                bFileDownloadOK = true;
            }
            catch (Exception e)
            {
                AddStatusLine(string.Format(Program.LangSys.C("Error while looking for version file ({0})", e.Message)) + ENDL);
                AddStatusLine(Program.LangSys.C("Update canceled") + ENDL);
            }
            finally
            {
                wc.Dispose();
            }
            if (bFileDownloadOK)
            {
                AddStatusLine(Program.LangSys.C("Version file updated") + ENDL);
            }
            return bFileDownloadOK;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private StringCollection CheckVersions()
        {
            try
            {
                AddStatusLine(Program.LangSys.C("Detecting local version") + ENDL);
                XmlDocument versionFile = new XmlDocument();
                string localFile = Application.StartupPath + Path.DirectorySeparatorChar + "tmpUpdate" + Path.DirectorySeparatorChar + Program.VersionInfoFile;
                versionFile.Load(localFile);
                XmlNode rootNode = versionFile.DocumentElement;
                StringCollection listAssemblyToDownload = new StringCollection();
                foreach (XmlNode node in rootNode.ChildNodes)
                {
                    if (node.Name == "assemblyInfo")
                    {
                        XmlNode attrName = node.Attributes.GetNamedItem("fileName");
                        XmlNode attrVersion = node.Attributes.GetNamedItem("lastVersion");
                        // il faut déjà trouver si l'assembly est présent
                        bool bLocalAssemblyExists = false;
                        string localAssemblyVersion = string.Empty;
                        try
                        {
                            Assembly asm = Assembly.LoadFrom(Application.StartupPath + Path.DirectorySeparatorChar + attrName.Value);
                            Version asmVer = asm.GetName().Version;
                            localAssemblyVersion = asmVer.ToString();
                            bLocalAssemblyExists = true;
                        }
                        catch (Exception /*asmEx*/)
                        {
                        }
                        if (bLocalAssemblyExists)
                        {
                            string versionString = attrVersion.Value;
                            string[] remoteVersionIndices = versionString.Split('.');
                            string[] localVersionIndices = localAssemblyVersion.Split('.');
                            for (int i = 0; i < remoteVersionIndices.Length; i++)
                            {
                                int iRemoteIndice = int.Parse(remoteVersionIndices[i]);
                                int iLocalIndice = int.Parse(localVersionIndices[i]);
                                if (iRemoteIndice > iLocalIndice)
                                {
                                    listAssemblyToDownload.Add(attrName.Value);
                                    AddStatusLine(string.Format(
                                        Program.LangSys.C("Component {0} must be updated (local version = {1}, server version = {2})"),
                                        attrName.Value, 
                                        localAssemblyVersion, 
                                        attrVersion.Value) + ENDL);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            // nouvel assembly à récupérer
                            AddStatusLine(string.Format(
                                Program.LangSys.C("Component {0} is not present on your system and will be acquired (server version = {1})"),
                                attrName.Value,
                                attrVersion.Value) + ENDL);
                            listAssemblyToDownload.Add(attrName.Value);
                        }
                    }
                }
                if (listAssemblyToDownload.Count > 0)
                {
                    AddStatusLine(Program.LangSys.C(
                        string.Format("Total : {0} file will be downloaded",
                        listAssemblyToDownload.Count)) + ENDL);
                    return listAssemblyToDownload;
                }
            }
            catch (Exception e)
            {
                AddStatusLine(string.Format(Program.LangSys.C("Error while detecting versions ({0})"), e.Message) + ENDL);
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arguments"></param>
        /// <param name="FilesToUpdate"></param>
        public bool DownloadFiles(StringCollection FilesToUpdate)
        {
            bool bAllFileOK = true;
            if (FilesToUpdate != null)
            {
                foreach (string file in FilesToUpdate)
                {
                    AddStatusLine(Program.LangSys.C(
                        string.Format("Downloading file {0}",
                        file)) + ENDL);

                    WebClient wc = new WebClient();
                    try
                    {
                        string downloadURL = Program.FileUrl + file;
                        if (checkBox1.Checked)
                            downloadURL = Program.BetaFileUrl + file;

                        wc.DownloadFile(downloadURL, Application.StartupPath + Path.DirectorySeparatorChar + "tmpUpdate" + Path.DirectorySeparatorChar + file);
                        AddStatusLine(Program.LangSys.C("Done") + ENDL);
                    }
                    catch (Exception e)
                    {
                        AddStatusLine(Program.LangSys.C(
                            string.Format("Error while downloading file {0} ({1})",
                            file, e.Message)) + ENDL);
                        bAllFileOK = false;
                        break;
                    }
                }
            }
            return bAllFileOK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (DownloadVersionFile())
            {
                StringCollection assemblyToDownload = CheckVersions();
                if (assemblyToDownload == null)
                {
                    AddStatusLine(Program.LangSys.C("Your version is already up to date") + ENDL);
                }
                else
                {
                    if (DownloadFiles(assemblyToDownload))
                    {
                        MessageBox.Show(Program.LangSys.C("Updater will now close and finalize update, do not try so start SmartApp before the console windows is closed"), Program.LangSys.C("Information"));
                        Program.StartBatchCopy();
                        Application.Exit();
                    }
                }
            }
        }
    }
}

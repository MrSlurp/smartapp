using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Xml;
using System.Reflection;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Diagnostics;

namespace SmartAppUpdater
{
    class Program
    {
        private const string BetaFileUrl = "http://www.smartappsoftware.net/smartapp/autoupdateB/";
        private const string FileUrl = "http://www.smartappsoftware.net/smartapp/autoupdate/";
        private const string VersionInfoFile = "lastversioninfo.xml";


        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            if (!Directory.Exists(Application.StartupPath + "tmpUpdate"))
            {
                Directory.CreateDirectory(Application.StartupPath + Path.DirectorySeparatorChar + "tmpUpdate");
            }

            StringCollection arguments = new StringCollection();
            arguments.AddRange(args);
            if (arguments.Contains("-GenerateVersFile"))
            {
                GenerateVersionFile(arguments);
            }
            else
            {
                StringCollection filesToUpdate = CheckUpdates(arguments);
                DownloadFiles(arguments, filesToUpdate);
                Console.ReadKey();
                StartBatchCopy();
            }
        }

        public static void StartBatchCopy()
        {
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "postUpdateCopy.bat";
            psi.UseShellExecute = true;
            Process copyProcess = Process.Start(psi);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        private static StringCollection CheckUpdates(StringCollection arguments)
        {
            WebClient wc = new WebClient();
            bool bFileDownloadOK = false;
            Console.WriteLine("Recherche des mises à jour");
            try
            {
                string downloadURL = FileUrl + VersionInfoFile;
                if (arguments.Contains("-B"))
                    downloadURL = BetaFileUrl + VersionInfoFile;
                string localFile = Application.StartupPath + Path.DirectorySeparatorChar + "tmpUpdate" + Path.DirectorySeparatorChar + VersionInfoFile;
                wc.DownloadFile(downloadURL, localFile);
                bFileDownloadOK = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("Erreur de récupération du fichier d'information des version ({0})", e.Message));
                Console.WriteLine("Mise a jour interrompue");
            }
            finally
            {
                wc.Dispose();
            }

            if (bFileDownloadOK)
            {
                try
                {
                    XmlDocument versionFile = new XmlDocument();
                    string localFile = Application.StartupPath + Path.DirectorySeparatorChar + "tmpUpdate" + Path.DirectorySeparatorChar + VersionInfoFile;
                    versionFile.Load(localFile);

                    XmlNode rootNode = versionFile.DocumentElement;
                    StringCollection listAssemblyToDownload = new StringCollection();
                    foreach (XmlNode node in rootNode.ChildNodes)
                    {
                        if (node.Name == "assemblyInfo")
                        {
                            XmlNode attrName = node.Attributes.GetNamedItem("fileName");
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
                                XmlNode attrVersion = node.Attributes.GetNamedItem("lastVersion");
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
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                // nouvel assembly à récupérer
                                listAssemblyToDownload.Add(attrName.Value);
                            }
                        }
                    }
                    if (listAssemblyToDownload.Count > 0)
                    {
                        Console.WriteLine(string.Format("Il y a {0} fichier(s) à mettre à jour", listAssemblyToDownload.Count));
                        return listAssemblyToDownload;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(string.Format("Erreur de détéction des version ({0})", e.Message));
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arguments"></param>
        static void GenerateVersionFile(StringCollection arguments)
        {
            string strAppDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
            StringCollection AssemblyList = new StringCollection();
            AssemblyList.AddRange(Directory.GetFiles(strAppDir, "*.*", SearchOption.TopDirectoryOnly));
            //AssemblyList.AddRange(Directory.GetFiles(strAppDir, "*.exe", SearchOption.TopDirectoryOnly));

            XmlDocument versionFile = new XmlDocument();
            versionFile.LoadXml("<root/>");

            for (int i = 0; i < AssemblyList.Count; i++)
            {
                if ((AssemblyList[i].EndsWith(".dll") || AssemblyList[i].EndsWith(".exe")) && !AssemblyList[i].EndsWith(".vshost.exe"))
                {
                    try
                    {
                        Assembly assembly = Assembly.LoadFrom(AssemblyList[i]);
                        Version asmVer = assembly.GetName().Version;
                        XmlNode assemblyNode = versionFile.CreateElement("assemblyInfo");
                        XmlAttribute attrName = versionFile.CreateAttribute("fileName");
                        XmlAttribute attrVersion = versionFile.CreateAttribute("lastVersion");
                        attrVersion.Value = asmVer.ToString();
                        attrName.Value = Path.GetFileName(AssemblyList[i]);
                        assemblyNode.Attributes.Append(attrName);
                        assemblyNode.Attributes.Append(attrVersion);
                        versionFile.DocumentElement.AppendChild(assemblyNode);

                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Impossible de charger la version du fichier " + AssemblyList[i]);
                    }
                }
            }
            versionFile.Save("lastversioninfo.xml");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arguments"></param>
        /// <param name="FilesToUpdate"></param>
        static public void DownloadFiles(StringCollection arguments, StringCollection FilesToUpdate)
        {
            if (FilesToUpdate != null)
            {
                foreach (string file in FilesToUpdate)
                {
                    WebClient wc = new WebClient();
                    try
                    {
                        string downloadURL = FileUrl + file;
                        if (arguments.Contains("-B"))
                            downloadURL = BetaFileUrl + file;

                        wc.DownloadFile(downloadURL, Application.StartupPath + Path.DirectorySeparatorChar + "tmpUpdate" + Path.DirectorySeparatorChar + file);
                        Console.WriteLine("Fichier téléchargé : " + file);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("erreur telechargement fichier : " + file);
                        Console.WriteLine(e.Message);

                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Xml;
using System.Reflection;
using System.Windows.Forms;

namespace SmartAppUpdater
{
    class Program
    {
        static void Main(string[] args)
        {
            WebClient wc = new WebClient();
            bool bFileDownloadOK = false;
            try
            {
                string downloadURL = "http://www.smartappsoftware.net/smartapp/autoupdate/lastversioninfo.xml";
                if (args.Length > 0 && args[0] == "-B")
                    downloadURL = "http://www.smartappsoftware.net/smartapp/autoupdate/lastversioninfoB.xml";
                wc.DownloadFile(downloadURL, "lastversioninfo.xml");
                bFileDownloadOK = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("Erreur de récupération du fichier ({0})", e.Message));
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
                    versionFile.Load("lastversioninfo.xml");

                    bool bNeedUpdate = false;
                    XmlNode rootNode = versionFile.DocumentElement;
                    foreach (XmlNode node in rootNode.ChildNodes)
                    {
                        if (node.Name == "assemblyInfo")
                        {
                            XmlNode attrName = node.Attributes.GetNamedItem("fileName");
                            // il faut déjà trouver si l'assembly est présent
                            string localAssemblyVersion = string.Empty;
                            try
                            {
                                Assembly asm = Assembly.LoadFrom(Application.StartupPath + Path.DirectorySeparatorChar + attrName.Value);
                                Version asmVer = asm.GetName().Version;
                                localAssemblyVersion = asmVer.ToString();
                            }
                            catch (Exception asmEx)
                            {
                                continue;
                            }
                            XmlNode attrVersion = node.Attributes.GetNamedItem("lastVersion");
                            string versionString = attrVersion.Value;
                            string[] remoteVersionIndices = versionString.Split('.');
                            string[] localVersionIndices = localAssemblyVersion.Split('.');
                            for (int i = 0; i < remoteVersionIndices.Length; i++ )
                            {
                                int iRemoteIndice = int.Parse(remoteVersionIndices[i]);
                                int iLocalIndice = int.Parse(localVersionIndices[i]);
                                if (iRemoteIndice > iLocalIndice)
                                {
                                    bNeedUpdate = true;
                                    break;
                                }
                            }
                        }
                    }
                    // la liste des noeud en dessous du root correspond à une liste de version d'assembly
                }
                catch (Exception e)
                {
                    Console.WriteLine(string.Format("Erreur de détéction des version ({0})", e.Message));

                }
            }
        }
    }
}

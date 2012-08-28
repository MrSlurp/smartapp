using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Xml;
using System.Reflection;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Diagnostics;
using CommonLib;

namespace SmartAppUpdater
{
    class Program
    {
        public const string BetaFileUrl = "http://www.smartappsoftware.net/smartapp/autoupdateB/";
        public const string FileUrl = "http://www.smartappsoftware.net/smartapp/autoupdate/";
        public const string VersionInfoFile = "lastversioninfo.xml";

#if BUILD_LANG
#if TEST_LANG
        static Lang m_SingLangSys = new Lang(true, true);
#else
        static Lang m_SingLangSys = new Lang(true, false);
#endif
#else
        static Lang m_SingLangSys = new Lang();
#endif
        public static Lang LangSys
        {
            get { return m_SingLangSys; }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        [STAThread]
        static void Main(string[] args)
        {
            StringCollection arguments = new StringCollection();
            arguments.AddRange(args);
            if (arguments.Contains("-GenerateVersFile"))
            {
                GenerateVersionFile();
                return;
            }

            LangSys.Initialize(Cste.STR_DEV_LANG, "EN", "SmartAppUpdater");
            Form mainForm = new UpdaterMainForm();
            Application.Run(mainForm);

            /*
                StringCollection filesToUpdate = CheckUpdates(arguments);
                DownloadFiles(arguments, filesToUpdate);
                Console.ReadKey();
            }*/
        }

        public static void StartBatchCopy()
        {
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "postUpdateCopy.bat";
            psi.UseShellExecute = true;
            Process copyProcess = Process.Start(psi);
        }

        /// <summary>
        /// génération automatique du fichier de version des assembly
        /// </summary>
        /// <param name="arguments"></param>
        static void GenerateVersionFile()
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
    }
}

/*
    This file is part of SmartApp.
    Copyright (C) 2007-2016  Pascal Bigot

    SmartApp is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    SmartApp is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with Foobar.  If not, see <http://www.gnu.org/licenses/>.
*/
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
            string curLang = "EN";
            int langArgIndex = arguments.IndexOf("-Lang");
            if (langArgIndex != -1 && arguments.Count > langArgIndex + 1)
                curLang = arguments[langArgIndex + 1];
            LangSys.Initialize(Cste.STR_DEV_LANG, curLang, "SmartAppUpdater");
            Form mainForm = new UpdaterMainForm();
            Application.Run(mainForm);
        }

        public static void StartBatchCopy()
        {
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "\"" + Application.StartupPath + "postUpdateCopy.bat\"";
            psi.Arguments = " > updatelog.txt";
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
            StringCollection FileList = new StringCollection();
            FileList.AddRange(Directory.GetFiles(strAppDir, "*.*", SearchOption.TopDirectoryOnly));

            XmlDocument versionFile = new XmlDocument();
            versionFile.LoadXml("<root/>");

            for (int i = 0; i < FileList.Count; i++)
            {
                if ((FileList[i].EndsWith(".dll") || FileList[i].EndsWith(".exe")) && !FileList[i].EndsWith(".vshost.exe"))
                {
                    try
                    {
                        Assembly assembly = Assembly.LoadFrom(FileList[i]);
                        Version asmVer = assembly.GetName().Version;
                        XmlNode assemblyNode = versionFile.CreateElement("assemblyInfo");
                        XmlAttribute attrName = versionFile.CreateAttribute("fileName");
                        XmlAttribute attrVersion = versionFile.CreateAttribute("lastVersion");
                        attrVersion.Value = asmVer.ToString();
                        attrName.Value = Path.GetFileName(FileList[i]);
                        assemblyNode.Attributes.Append(attrName);
                        assemblyNode.Attributes.Append(attrVersion);
                        versionFile.DocumentElement.AppendChild(assemblyNode);

                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Impossible de charger la version du fichier " + FileList[i]);
                    }
                }
            }
            versionFile.Save("lastversioninfo.xml");
        }
    }
}

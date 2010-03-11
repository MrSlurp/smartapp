using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Collections;
using System.Text;
using System.IO;
using System.Reflection;

namespace CommonLib
{
    public class DllControlGest
    {
        private List<IDllControlInterface> m_ListDlls = new List<IDllControlInterface>();
        private Hashtable m_HashDllIDs = new Hashtable();
        private string mCurrentLang;

        public DllControlGest()
        {

        }

        public string CurrentLang
        {
            get { return mCurrentLang; }
            set { mCurrentLang = value; }
        }

        public void ChangeLang(string newLang)
        {
            for (int i = 0; i < m_ListDlls.Count; i++)
            {
                m_ListDlls[i].CurrentLang = newLang;
            }
        }

        public void LoadExistingDlls()
        {
            //Assembly Dll = Assembly.Load();
            string strAppDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
            string[] filenames = Directory.GetFiles(strAppDir, "*.dll", SearchOption.TopDirectoryOnly);
            for (int i = 0; i < filenames.Length; i++)
            {
                if (!filenames[i].Contains("CommonLib"))
                {
                    
                    Assembly Dll = Assembly.LoadFrom(filenames[i]);
                    Module[] Mdl = Dll.GetModules();
                    Type[] typsFile = Mdl[0].GetTypes();
                    for (int j = 0; j < typsFile.Length; j++)
                    {
                        Type oType = typsFile[j];
                        if (oType.IsPublic && oType.Name == "DllEntryClass")
                        {
                            try
                            {
                                IDllControlInterface oDll = (IDllControlInterface)Dll.CreateInstance(oType.FullName);
                                if (oDll != null)
                                {
                                    oDll.CurrentLang = mCurrentLang;
                                    m_HashDllIDs.Add(oDll.DllID, oDll);
                                    m_ListDlls.Add(oDll);
                                }
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show(string.Format(Lang.LangSys.C("Error while loading Plugins controls (Error in DLL {0})") 
                                                               + "\n" 
                                                               + Lang.LangSys.C("Plugins control will not be available"), 
                                                               oType.Assembly.FullName), 
                                                               Lang.LangSys.C("Error loading plugins"));
                                Console.WriteLine(e.Message);
                            }
                        }
                    }
                }
            }
        }

        public IDllControlInterface this[uint IdDll]
        {
            get
            {
                return (IDllControlInterface)m_HashDllIDs[IdDll];
            }
        }

        public IDllControlInterface this[int i]
        {
            get
            {
                return m_ListDlls[i];
            }
        }
        public int Count
        {
            get
            {
                return m_ListDlls.Count;
            }
        }

    }
}

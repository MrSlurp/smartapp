/*
    This file is part of SmartApp.

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
using System.Collections.Specialized;
using System.Windows.Forms;
using System.Collections;
using System.Text;
using System.IO;
using System.Reflection;
using System.Xml;

namespace CommonLib
{
    public class DllControlGest
    {
        private List<IDllControlInterface> m_ListDlls = new List<IDllControlInterface>();
        private Hashtable m_HashDllIDs = new Hashtable();

        private SortedList<string, List<IDllControlInterface>> m_mapTypeToList = new SortedList<string, List<IDllControlInterface>>();

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
                                    System.Console.WriteLine(string.Format("DLL ajouté id = {0}, nom = {1}", oDll.DllID, filenames[i]));
                                    m_ListDlls.Add(oDll);
                                    if (m_mapTypeToList.ContainsKey(oDll.PluginType) )
                                    {
                                        List<IDllControlInterface> list = m_mapTypeToList[oDll.PluginType] as List<IDllControlInterface>;
                                        list.Add(oDll);
                                    }
                                    else
                                    {
                                        List<IDllControlInterface> list = new List<IDllControlInterface>();
                                        list.Add(oDll);
                                        m_mapTypeToList.Add(oDll.PluginType, list);
                                    }
                                }
                            }
                            catch (Exception)
                            {
                                MessageBox.Show(string.Format(Lang.LangSys.C("Error while loading Plugins controls (Error in DLL {0})") 
                                                               + "\n" 
                                                               + Lang.LangSys.C("Plugins control will not be available"), 
                                                               oType.Assembly.FullName), 
                                                               Lang.LangSys.C("Error loading plugins"),
                                                               MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                Traces.LogAddDebug(TraceCat.Plugin, "Erreur chargement DLL plugin");
                            }
                        }
                    }
                }
            }
        }

        public List<IDllControlInterface> this[string type]
        {
            get
            {
                return m_mapTypeToList[type] as List<IDllControlInterface>;
            }
        }

        public StringCollection TypeList
        {
            get
            {
                
                StringCollection list = new StringCollection();
                foreach(string item in m_mapTypeToList.Keys)
                {
                    list.Add(item);
                }
                return list;
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

        public int TypeCount
        {
            get
            {
                return m_mapTypeToList.Count;
            }
        }

        public bool ReadInPluginsGlobals(XmlNode XmlGlobaslNode)
        {
            for (int iChilds = 0; iChilds < XmlGlobaslNode.ChildNodes.Count; iChilds++)
            {
                XmlNode dllNode = XmlGlobaslNode.ChildNodes[iChilds];
                if (dllNode.Name == XML_CF_TAG.Plugin.ToString())
                {
                    XmlNode attrID = dllNode.Attributes.GetNamedItem(XML_CF_ATTRIB.DllID.ToString());
                    uint id = uint.Parse(attrID.Value);
                    IDllControlInterface dll = m_HashDllIDs[id] as IDllControlInterface;
                    dll.ReadInModuleGlobalInfo(dllNode);
                }
            }
            return true;
        }

        public bool WriteOutPluginsGlobals(XmlDocument XmlDoc, XmlNode XmlGlobalsNode )
        {
            for (int iDll = 0; iDll < m_ListDlls.Count; iDll++)
            {
                m_ListDlls[iDll].WriteOutModuleGlobalInfo(XmlDoc, XmlGlobalsNode);
            }
            return true;
        }
    }
}

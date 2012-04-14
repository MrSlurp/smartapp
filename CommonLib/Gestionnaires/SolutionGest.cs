using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace CommonLib
{
    public class SolutionGest : SortedList<string, BTDoc>
    {
        private const string NODE_SOLUTION_ROOT = "Solution";
        private const string NODE_SOLUTION_FILEVER = "FileVersion";
        private const string NODE_PROJECT_LIST = "ProjectList";
        private const string NODE_PROJECT_ITEM = "Project";
        private const string ATTR_VALUE = "Value";
        private const int SOLLUTION_FILE_VERSION = 1;

        public delegate void DocumentOpenCloseEventHandler(BTDoc doc);
        public delegate void DocumentScreenEditHandler(string screenName, BTDoc document);


        public event DocumentOpenCloseEventHandler OnDocOpened;
        public event DocumentOpenCloseEventHandler OnDocClosed;
        public event DocumentScreenEditHandler OnDocScreenEdit;

        TYPE_APP m_TypeApp;
        DllControlGest m_GestDLL;

        PathTranslator m_PathTranslator = new PathTranslator();
        string m_SolutionPath;

        public string FilePath
        {
            get { return m_SolutionPath; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeApp"></param>
        /// <param name="dllGest"></param>
        public SolutionGest(TYPE_APP typeApp, DllControlGest dllGest)
        {
            m_TypeApp = typeApp;
            m_GestDLL = dllGest;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strFilePath"></param>
        /// <returns></returns>
        public BTDoc OpenDocument(string strFilePath)
        {
            if (!this.ContainsKey(strFilePath))
            {
                BTDoc openedDoc = new BTDoc(m_TypeApp);
                if (openedDoc.ReadConfigDocument(strFilePath, m_TypeApp, m_GestDLL))
                {
                    this.Add(strFilePath, openedDoc);
                    if (OnDocOpened != null)
                    {
                        OnDocOpened(openedDoc);
                    }
                    return openedDoc;
                }
                else
                    return null;
            }
            else
            {
                return this[strFilePath];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strFilePath"></param>
        /// <returns></returns>
        public void AddDocument(BTDoc doc)
        {
            if (!this.ContainsValue(doc))
            {
                this.Add(doc.FileName, doc);
                if (OnDocOpened != null)
                {
                    OnDocOpened(doc);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc"></param>
        public void CloseDocument(BTDoc doc)
        {
            if (this.ContainsValue(doc))
            {
                this.Values.Remove(doc);
                if (OnDocClosed != null)
                {
                    OnDocClosed(doc);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSolutionPath"></param>
        /// <returns></returns>
        public bool ReadInSolution(string strSolutionPath)
        {
            XmlDocument XmlDoc = new XmlDocument();
            try
            {
                XmlDoc.Load(strSolutionPath);
            }
            catch (Exception e)
            {
                Traces.LogAddDebug(TraceCat.Document, "Le fichier est corrompu");
                Console.WriteLine(e.Message);
                return false;
            }
            XmlNode RootNode = XmlDoc.FirstChild;
            if (RootNode.Name != NODE_SOLUTION_ROOT)
                return false;

            m_PathTranslator.BTDocPath = Path.GetDirectoryName(strSolutionPath);
            m_SolutionPath = strSolutionPath;
            int SolutionFileVersion = 0;
            for (int iRootChilds = 0; iRootChilds < RootNode.ChildNodes.Count; iRootChilds++)
            {
                XmlNode curNode = RootNode.ChildNodes[iRootChilds];
                if (curNode.Name == NODE_SOLUTION_FILEVER)
                {
                    XmlAttribute attr = curNode.Attributes[ATTR_VALUE];
                    if (attr != null)
                    {
                        SolutionFileVersion = int.Parse(attr.Value);
                    }
                    if (SolutionFileVersion < SOLLUTION_FILE_VERSION)
                    {
                        if (m_TypeApp == TYPE_APP.SMART_CONFIG)
                            MessageBox.Show(Lang.LangSys.C("This file have been created with an oldest version, if you save this file, you will not be able to read it with previous version"),
                                            Lang.LangSys.C("Warning"),
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Exclamation);
                    }
                }
                else if (curNode.Name == NODE_PROJECT_LIST)
                {
                    foreach (XmlNode NodeProj in curNode.ChildNodes)
                    {
                        XmlAttribute attr = NodeProj.Attributes[ATTR_VALUE];
                        string cheminProj = attr.Value;
                        cheminProj = m_PathTranslator.RelativePathToAbsolute(cheminProj);
                        cheminProj = PathTranslator.LinuxVsWindowsPathUse(cheminProj);
                        bool KeepGhostProject = true;
                        if (!File.Exists(cheminProj))
                        {
                            DialogResult dlgRes = MessageBox.Show(string.Format(Lang.LangSys.C("Unable to open file {0}, do you want to keep it in solution?"),cheminProj),
                                                                  Lang.LangSys.C("Error"),
                                                                  MessageBoxButtons.YesNo,
                                                                  MessageBoxIcon.Error);
                            if (dlgRes == DialogResult.Yes)
                            {
                                KeepGhostProject = true; // TODO
                            }
                        }
                        else if (this.OpenDocument(cheminProj) == null)
                        {
                            DialogResult dlgRes = MessageBox.Show(string.Format(Lang.LangSys.C("File {0}, is corrupted. Do you want to keep it in solution?"), cheminProj),
                                                                  Lang.LangSys.C("Error"),
                                                                  MessageBoxButtons.YesNo,
                                                                  MessageBoxIcon.Error);
                            if (dlgRes == DialogResult.Yes)
                            {
                                KeepGhostProject = true; // TODO
                            }
                        }
                        if (KeepGhostProject)
                        {
                            // TODO ENVOYER LE GHOST
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool WriteOutSolution()
        {
            return WriteOutSolution(m_SolutionPath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSolutionPath"></param>
        /// <returns></returns>
        public bool WriteOutSolution(string strSolutionPath)
        {
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.LoadXml("<Solution></Solution>");
            XmlNode NodeVersion = XmlDoc.CreateElement(NODE_SOLUTION_FILEVER);
            XmlAttribute attr = XmlDoc.CreateAttribute(ATTR_VALUE);
            attr.Value = SOLLUTION_FILE_VERSION.ToString();
            NodeVersion.Attributes.Append(attr);
            XmlDoc.DocumentElement.AppendChild(NodeVersion);
            XmlNode NodeProjList = XmlDoc.CreateElement(NODE_PROJECT_LIST);
            XmlDoc.DocumentElement.AppendChild(NodeProjList);

            foreach (BTDoc doc in Values)
            {
                XmlNode NodeProj = XmlDoc.CreateElement(NODE_PROJECT_LIST);
                attr = XmlDoc.CreateAttribute(ATTR_VALUE);
                string relProjPath = m_PathTranslator.AbsolutePathToRelative(doc.FileName);
                relProjPath = PathTranslator.LinuxVsWindowsPathStore(relProjPath);
                attr.Value = relProjPath;
                NodeProj.Attributes.Append(attr);
                NodeProjList.AppendChild(NodeProj);
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        public void SaveAllDocumentsAndSolution()
        {
            WriteOutSolution();
            foreach (BTDoc doc in this.Values)
            {
                doc.WriteConfigDocument(true);
                doc.Modified = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool HaveModifiedDocument
        {
            get
            {
                bool bModifiedDoc = false;
                foreach (BTDoc doc in this.Values)
                {
                    bModifiedDoc |= doc.Modified;
                }
                return bModifiedDoc;
            }
            set
            {
                foreach (BTDoc doc in this.Values)
                {
                    doc.Modified = value;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="screenSymbol"></param>
        /// <param name="document"></param>
        public void OpenScreenEditor(string screenSymbol, BTDoc document)
        {
            if (OnDocScreenEdit != null)
            {
                OnDocScreenEdit(screenSymbol, document);
            }
        }
    }
}

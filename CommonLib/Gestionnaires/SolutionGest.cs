using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace CommonLib
{
    public class SolutionGest : SortedList<string, BaseDoc>
    {
        private const string NODE_SOLUTION_ROOT = "Solution";
        private const string NODE_SOLUTION_FILEVER = "FileVersion";
        private const string NODE_PROJECT_LIST = "ProjectList";
        private const string NODE_PROJECT_ITEM = "Project";
        private const string ATTR_VALUE = "Value";
        private const int SOLLUTION_FILE_VERSION = 1;

        public delegate void DocumentOpenCloseEventHandler(BaseDoc doc);
        public delegate void DocumentScreenEditHandler(string screenName, BTDoc document);
        public delegate void SolutionNameChangedEventHandler();
        public delegate void SolutionDocumentChangedEventHandler();

        public event DocumentOpenCloseEventHandler OnDocOpened;
        public event DocumentOpenCloseEventHandler OnDocClosed;
        public event DocumentScreenEditHandler OnDocScreenEdit;
        public event SolutionNameChangedEventHandler OnSolutionNameChanged;
        public event SolutionDocumentChangedEventHandler OnDocumentChanged;

        TYPE_APP m_TypeApp;
        DllControlGest m_GestDLL;

        PathTranslator m_PathTranslator = new PathTranslator();
        string m_SolutionPath;
        bool m_bModified = false;

        public string FilePath
        {
            get { return m_SolutionPath; }
            private set
            {
                if (m_SolutionPath != value)
                {
                    m_SolutionPath = value;
                    if (OnSolutionNameChanged != null)
                    {
                        OnSolutionNameChanged();
                    }
                }
            }
        }

        protected bool Modified
        {
            get
            {
                return m_bModified;
            }
            set
            {
                bool bOldValue = m_bModified;
                m_bModified = value;
                if (bOldValue != m_bModified && OnDocumentChanged != null)
                {
                    OnDocumentChanged();
                }
            }
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
        public BaseDoc OpenDocument(string strFilePath)
        {
            if (!this.ContainsKey(strFilePath))
            {
                BTDoc openedDoc = new BTDoc(m_TypeApp);
                if (openedDoc.ReadConfigDocument(strFilePath, m_TypeApp, m_GestDLL))
                {
                    this.Add(strFilePath, openedDoc);
                    Modified = true;
                    openedDoc.OnDocumentModified += new DocumentModifiedEvent(OnDocumentModified);
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
        public void AddDocument(BaseDoc doc)
        {
            if (!this.ContainsValue(doc))
            {
                this.Add(doc.FileName, doc);
                Modified = true;
                doc.OnDocumentModified += new DocumentModifiedEvent(OnDocumentModified);
                if (OnDocOpened != null)
                {
                    OnDocOpened(doc);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        void OnDocumentModified()
        {
            if (this.OnDocumentChanged != null)
            {
                this.OnDocumentChanged();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc"></param>
        public void CloseDocument(BaseDoc doc)
        {
            if (this.ContainsValue(doc))
            {
                this.Remove(doc.FileName);
                Modified = true;
                if (OnDocClosed != null)
                {
                    OnDocClosed(doc);
                }
            }
        }

        public void CloseDocumentsForCommand()
        {
            foreach (BaseDoc doc in this.Values)
            {
                if (doc is BTDoc)
                    ((BTDoc)doc).CloseDocumentForCommand();
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
            this.FilePath = strSolutionPath;
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
                        //bool KeepGhostProject = true;
                        if (!File.Exists(cheminProj))
                        {
                            DialogResult dlgRes = MessageBox.Show(string.Format(Lang.LangSys.C("Unable to open file {0}, this project will be removed from solution"),cheminProj),
                                                                  Lang.LangSys.C("Error"),
                                                                  MessageBoxButtons.OK,
                                                                  MessageBoxIcon.Error);
                            /*if (dlgRes == DialogResult.Yes)
                            {
                                KeepGhostProject = true; 
                            }*/
                        }
                        else if (this.OpenDocument(cheminProj) == null)
                        {
                            DialogResult dlgRes = MessageBox.Show(string.Format(Lang.LangSys.C("File {0}, is corrupted. this project will be removed from solution"), cheminProj),
                                                                  Lang.LangSys.C("Error"),
                                                                  MessageBoxButtons.OK,
                                                                  MessageBoxIcon.Error);
                            /*if (dlgRes == DialogResult.Yes)
                            {
                                KeepGhostProject = true; 
                            }*/
                        }
                        /*
                        if (KeepGhostProject)
                        {
                            // TODO ENVOYER LE GHOST
                        }*/
                    }
                }
            }
            Modified = false;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSolutionPath"></param>
        /// <returns></returns>
        protected bool WriteOutSolution(string strSolutionPath)
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

            foreach (BaseDoc doc in Values)
            {
                XmlNode NodeProj = XmlDoc.CreateElement(NODE_PROJECT_LIST);
                attr = XmlDoc.CreateAttribute(ATTR_VALUE);
                string relProjPath = m_PathTranslator.AbsolutePathToRelative(doc.FileName);
                relProjPath = PathTranslator.LinuxVsWindowsPathStore(relProjPath);
                attr.Value = relProjPath;
                NodeProj.Attributes.Append(attr);
                NodeProjList.AppendChild(NodeProj);
            }
            XmlDoc.Save(strSolutionPath);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        public void SaveAllDocumentsAndSolution()
        {
            WriteOutSolution(m_SolutionPath);
            SaveDocuments();
            Modified = false;
        }

        /// <summary>
        /// 
        /// </summary>
        public void SaveAllDocumentsAndSolution(string strSolutionPath)
        {
            this.FilePath = strSolutionPath;
            WriteOutSolution(strSolutionPath);
            SaveDocuments();
            Modified = false;
        }

        /// <summary>
        /// 
        /// </summary>
        protected void SaveDocuments()
        {
            foreach (BaseDoc doc in this.Values)
            {
                if (doc is BTDoc)
                    ((BTDoc)doc).WriteConfigDocument(true);

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
                foreach (BaseDoc doc in this.Values)
                {
                    bModifiedDoc |= doc.Modified;
                }
                bModifiedDoc |= Modified;
                return bModifiedDoc;
            }
            set
            {
                foreach (BaseDoc doc in this.Values)
                {
                    doc.Modified = value;
                }
                Modified = true;
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

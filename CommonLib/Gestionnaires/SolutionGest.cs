using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;

namespace CommonLib
{
    public class SolutionGest : SortedList<string, BTDoc>
    {
        public delegate void DocumentOpenCloseEventHandler(BTDoc doc);

        public event DocumentOpenCloseEventHandler OnDocOpened;
        public event DocumentOpenCloseEventHandler OnDocClosed;

        TYPE_APP m_TypeApp;
        DllControlGest m_GestDLL;

        public SolutionGest(TYPE_APP typeApp, DllControlGest dllGest)
        {
            m_TypeApp = typeApp;
            m_GestDLL = dllGest;
        }

        public BTDoc OpenDocument(string strFilePath)
        {
            if (!this.ContainsKey(strFilePath))
            {
                BTDoc openedDoc = new BTDoc(m_TypeApp) ;
                openedDoc.ReadConfigDocument(strFilePath, m_TypeApp, m_GestDLL);
                this.Add(strFilePath, openedDoc);
                if (OnDocOpened != null)
                {
                    OnDocOpened(openedDoc);
                }
                return openedDoc;
            }
            return null;
        }

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
            return true;
        }

        public bool WriteOutSolution(string strSolutionPath)
        {

            return false;
        }

        public bool WriteOutSolution(XmlDocument xmlDoc)
        {

            return false;
        }

        public void SaveAllDocumentsAndSolution()
        {
            foreach (BTDoc doc in this.Values)
            {
                doc.WriteConfigDocument(true);
                doc.Modified = false;
            }
        }

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

        }
    }
}

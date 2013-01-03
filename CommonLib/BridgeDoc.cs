using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace CommonLib
{

    /// <summary>
    /// 
    /// </summary>
    public class BridgeDoc : BaseDoc
    {
        SolutionGest m_solution;

        List<DataBridgeInfo> m_ListDataBridge = new List<DataBridgeInfo>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TypeApp"></param>
        /// <param name="gestSolution"></param>
        public BridgeDoc(TYPE_APP TypeApp, SolutionGest gestSolution) : base (TypeApp)
        {
            m_solution = gestSolution;
        }

        public List<DataBridgeInfo> DocumentBridges
        {
            get { return m_ListDataBridge; }
        }

        public DataBridgeInfo CreateNewBridge()
        {
            string strNewBrdigeSymbol = string.Empty;
            for (int i = 0; i < Int32.MaxValue; i++)
            {
                strNewBrdigeSymbol = string.Format("BRIDGE_{0}", i);
                bool bNameExists = false;
                foreach (DataBridgeInfo br in m_ListDataBridge)
                {
                    if (strNewBrdigeSymbol == br.Symbol)
                    {
                        bNameExists = true;
                        break;
                    }
                }
                if (!bNameExists)
                    break;
            }
            DataBridgeInfo bgInfo = new DataBridgeInfo();
            bgInfo.Symbol = strNewBrdigeSymbol;
            m_ListDataBridge.Add(bgInfo);
            return bgInfo;
        }

        public void DeleteBridge(string symbol)
        {
            DataBridgeInfo brInfo = null;
            foreach (DataBridgeInfo br in m_ListDataBridge)
            {
                if (symbol == br.Symbol)
                {
                    brInfo = br;
                    break;
                }
            }
            if (brInfo != null)
            {
                m_ListDataBridge.Remove(brInfo);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bShowError"></param>
        /// <returns></returns>
        public override bool WriteConfigDocument(bool bShowError)
        {
            return WriteOut(m_strfileFullName, bShowError);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public bool ReadIn(string filename)
        {
            m_strfileFullName = filename;
            XmlDocument XmlDoc = new XmlDocument();
            try
            {
                XmlDoc.Load(filename);
            }
            catch (Exception e)
            {
                Traces.LogAddDebug(TraceCat.Document, "Le fichier est corrompu");
                Console.WriteLine(e.Message);
                return false;
            }
            XmlNode RootNode = XmlDoc.DocumentElement;
            if (RootNode.Name != XML_CF_TAG.Root.ToString())
                return false;

            foreach (XmlNode Node in RootNode.ChildNodes)
            {
                if (Node.Name == "DataBridge")
                {
                    DataBridgeInfo bridge = new DataBridgeInfo();
                    if (bridge.ReadIn(Node))
                        this.m_ListDataBridge.Add(bridge);
                }
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="bShowError"></param>
        /// <returns></returns>
        public bool WriteOut(string filename, bool bShowError)
        {
            m_strfileFullName = filename;

            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.LoadXml("<Root/>");
            foreach (DataBridgeInfo bridge in m_ListDataBridge)
            {
                bridge.WriteOut(XmlDoc, XmlDoc.DocumentElement);
            }
            XmlDoc.Save(m_strfileFullName);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool FinalizeRead()
        {
            bool bRet = true;
            foreach (DataBridgeInfo bridgeInfo in m_ListDataBridge)
            {
                bRet &= bridgeInfo.FinalizeRead(m_solution);
            }
            return bRet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Mess"></param>
        /// <param name="Param"></param>
        /// <param name="TypeApp"></param>
        public void TraiteMessage(BTDoc sender, MESSAGE Mess, object Param, TYPE_APP TypeApp)
        {
            foreach (DataBridgeInfo bridgeInfo in m_ListDataBridge)
            {
                if (Mess == MESSAGE.MESS_CMD_RUN)
                {
                    bridgeInfo.TraiteMessage(sender, MESSAGE.MESS_PRE_PARSE, Param, TypeApp);
                }
                bridgeInfo.TraiteMessage(sender, Mess, Param, TypeApp);
            }
            switch (Mess)
            {
                // les message suivant sont rerouté vers tout les objets
                case MESSAGE.MESS_ASK_ITEM_DELETE:
                case MESSAGE.MESS_ITEM_DELETED:
                case MESSAGE.MESS_ITEM_RENAMED:
                case MESSAGE.MESS_CMD_RUN:
                case MESSAGE.MESS_CMD_STOP:
                case MESSAGE.MESS_PRE_PARSE:
                    break;
            }
        }
    }
}

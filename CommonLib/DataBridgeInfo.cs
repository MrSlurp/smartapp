using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.IO;

namespace CommonLib
{
    public class DataBridgeInfo : BaseObject
    {
        #region donnée membres
        string m_SrcDoc = string.Empty;
        string m_DstDoc = string.Empty;
        StringCollection m_SrcDataList = new StringCollection();
        StringCollection m_DstDataList = new StringCollection();

        int m_execPeriod = 1000;
        string m_sTriggerScript;
        string m_sPostExecFunction;
        #endregion

        #region donnée membres en mode commandes
        BTDoc m_SrcDocObj;
        BTDoc m_DstDocObj;
        List<Data> m_SrcObjDataList = new List<Data>();
        List<Data> m_DstObjDataList = new List<Data>();
        int m_TriggerQuickId;
        int m_PostExecQuickId;
        BaseObject m_TriggerScriptObject; //objet qui declenche la recopie des données
        Function m_postExecFunction; // fonction qui est executé après la recopie des donnée

        Timer m_ExecTimer = new Timer();
        #endregion

        #region attributs
        public string SrcDoc
        {
            get { return m_SrcDoc; }
            set { m_SrcDoc = value; }
        }

        public string DstDoc
        {
            get { return m_DstDoc; }
            set { m_DstDoc = value; }
        }

        public StringCollection SrcDataList
        {
            get { return m_SrcDataList; }
        }

        public StringCollection DstDataList
        {
            get { return m_DstDataList; }
        }

        public string TriggerObjSymbol
        {
            get { return m_sTriggerScript; }
            set { m_sTriggerScript = value; }
        }

        public string PostExecFunction
        {
            get { return m_sPostExecFunction; }
            set { m_sPostExecFunction = value; }
        }

        public int ExecTimerPeriod
        {
            get { return m_execPeriod; }
            set { m_execPeriod = value; }
        }
        #endregion

        #region constructeur
        public DataBridgeInfo()
        {
            m_ExecTimer.Tick += new EventHandler(ExecTimer_Tick);
        }
        #endregion

        #region fonction de lecture /écriture de l'objet
        /// <summary>
        /// 
        /// </summary>
        /// <param name="BridgeNode"></param>
        /// <returns></returns>
        public bool ReadIn(XmlNode BridgeNode)
        {
            ReadInBaseObject(BridgeNode);
            XmlNode attrSrcProjName = BridgeNode.Attributes.GetNamedItem("SrcProj");
            XmlNode attrDstProjName = BridgeNode.Attributes.GetNamedItem("DstProj");
            XmlNode attrPeriod = BridgeNode.Attributes.GetNamedItem("Period");
            XmlNode attrTriggerSymbol = BridgeNode.Attributes.GetNamedItem("TriggerSymbol");
            XmlNode attrPostExecSymbol = BridgeNode.Attributes.GetNamedItem("PostExecSymbol");

            m_SrcDoc = attrSrcProjName.Value;
            m_DstDoc = attrDstProjName.Value;
            m_execPeriod = int.Parse(attrPeriod.Value);
            m_sTriggerScript = attrTriggerSymbol.Value;
            m_sPostExecFunction = attrPostExecSymbol.Value;

            foreach (XmlNode childNode in BridgeNode.ChildNodes)
            {
                StringCollection usedList = null;

                if (childNode.Name == "SrcDatas")
                    usedList = m_SrcDataList;
                else if (childNode.Name == "DstDatas")
                    usedList = m_DstDataList;
                if (childNode.Name == "SrcDatas" || childNode.Name == "DstDatas")
                {
                    foreach (XmlNode DataNode in childNode.ChildNodes)
                    {
                        if (DataNode.Name == "Data")
                        {
                            XmlNode attrSymbol = DataNode.Attributes.GetNamedItem("Symbol");
                            usedList.Add(attrSymbol.Value);
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="parentNode"></param>
        /// <returns></returns>
        public bool WriteOut(XmlDocument doc, XmlNode parentNode)
        {
            XmlNode bridgeNode = doc.CreateElement("DataBridge");
            parentNode.AppendChild(bridgeNode);
            WriteOutBaseObject(doc, bridgeNode);
            XmlAttribute attrSrcProjName = doc.CreateAttribute("SrcProj");
            XmlAttribute attrDstProjName = doc.CreateAttribute("DstProj");
            XmlAttribute attrPeriod = doc.CreateAttribute("Period");
            XmlAttribute attrTriggerSymbol = doc.CreateAttribute("TriggerSymbol");
            XmlAttribute attrPostExecSymbol = doc.CreateAttribute("PostExecSymbol");

            attrSrcProjName.Value = m_SrcDoc;
            attrDstProjName.Value = m_DstDoc;
            attrPeriod.Value = m_execPeriod.ToString();
            attrTriggerSymbol.Value = m_sTriggerScript;
            attrPostExecSymbol.Value = m_sPostExecFunction;
            bridgeNode.Attributes.Append(attrSrcProjName);
            bridgeNode.Attributes.Append(attrDstProjName);
            bridgeNode.Attributes.Append(attrPeriod);
            bridgeNode.Attributes.Append(attrTriggerSymbol);
            bridgeNode.Attributes.Append(attrPostExecSymbol);

            XmlNode SrcDatasNode = doc.CreateElement("SrcDatas");
            XmlNode DstDatasNode = doc.CreateElement("DstDatas");
            bridgeNode.AppendChild(SrcDatasNode);
            bridgeNode.AppendChild(DstDatasNode);

            foreach (string dataSymbol in m_SrcDataList)
            {
                XmlNode DataNode = doc.CreateElement("Data");
                SrcDatasNode.AppendChild(DataNode);
                XmlAttribute attrSymbol = doc.CreateAttribute("Symbol");
                attrSymbol.Value = dataSymbol;
                DataNode.Attributes.Append(attrSymbol);
            }
            foreach (string dataSymbol in m_DstDataList)
            {
                XmlNode DataNode = doc.CreateElement("Data");
                DstDatasNode.AppendChild(DataNode);
                XmlAttribute attrSymbol = doc.CreateAttribute("Symbol");
                attrSymbol.Value = dataSymbol;
                DataNode.Attributes.Append(attrSymbol);
            }

            return true;
        }

        /// <summary>
        /// commence la finalisation du du document bridge en récupérant les références directes
        /// sur les objets qui seront utilisés (document, données, fonction/timer)
        /// </summary>
        /// <param name="solGest">Gestionnaire de la solution</param>
        /// <returns></returns>
        public bool FinalizeRead(SolutionGest solGest)
        {
            // on récupère d'abord les documents
            foreach (string key in solGest.Keys)
            {
                string projName = Path.GetFileNameWithoutExtension(key);
                if (projName == m_SrcDoc)
                    m_SrcDocObj = solGest[key] as BTDoc;
                if (projName == m_DstDoc)
                    m_DstDocObj = solGest[key] as BTDoc;
            }

            if (m_DstDocObj == null || m_SrcDocObj == null)
                return false;

            m_SrcDocObj.Executer.EventScriptExecuted += new ScriptExecuted(SrcExecuter_EventScriptExecuted);
            //on construit les list de références vers les données
            foreach (string dataSymbol in m_SrcDataList)
            {
                Data dt = m_SrcDocObj.GestData.GetFromSymbol(dataSymbol) as Data;
                if (dt == null)
                {
                    m_SrcObjDataList.Add(null);
                    // TODO addlogevent
                }
                else
                {
                    m_SrcObjDataList.Add(dt);
                }
            }
            foreach (string dataSymbol in m_DstDataList)
            {
                Data dt = m_DstDocObj.GestData.GetFromSymbol(dataSymbol) as Data;
                if (dt == null)
                {
                    m_SrcObjDataList.Add(null);
                    // TODO addlogevent
                }
                else
                {
                    m_DstObjDataList.Add(dt);
                }
            }

            // on récupère les instances d'objet trigger et postExec
            if (!string.IsNullOrEmpty(m_sTriggerScript))
            {
                BaseObject objTimer = m_SrcDocObj.GestTimer.GetFromSymbol(m_sTriggerScript);
                BaseObject objFunction = m_SrcDocObj.GestFunction.GetFromSymbol(m_sTriggerScript);
                m_TriggerScriptObject = objTimer != null ? objTimer : (objFunction != null) ? objFunction : null;
            }
            if (!string.IsNullOrEmpty(m_sPostExecFunction))
            {
                BaseObject objFunction = m_DstDocObj.GestFunction.GetFromSymbol(m_sPostExecFunction);
                m_postExecFunction = objFunction as Function;
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Mess"></param>
        /// <param name="Param"></param>
        /// <param name="TypeApp"></param>
        public void TraiteMessage(BTDoc sender, MESSAGE Mess, object Param, TYPE_APP TypeApp)
        {
            switch (Mess)
            {
                case MESSAGE.MESS_ASK_ITEM_DELETE:
                // TODO, gérer les donnée supprimées
                case MESSAGE.MESS_ITEM_DELETED:
                // TODO, gérer les donnée supprimées
                case MESSAGE.MESS_ITEM_RENAMED:
                // TODO, gérer les donnée renommées
                case MESSAGE.MESS_CMD_RUN:
                    // si on a pas d'objet declencheur, c'est un fonctionnement par timer
                    if (m_TriggerScriptObject == null)
                        m_ExecTimer.Start();
                    break;
                case MESSAGE.MESS_CMD_STOP:
                    // si on a pas d'objet declencheur, c'est un fonctionnement par timer
                    if (m_TriggerScriptObject == null)
                        m_ExecTimer.Stop();
                    break;
                case MESSAGE.MESS_PRE_PARSE:
                    // à ce stade le pré parse est normamement déjà fait dans les projet classiques
                    if (m_TriggerScriptObject != null && m_TriggerScriptObject.QuickScriptID != -1)
                    {
                        m_TriggerQuickId = m_TriggerScriptObject.QuickScriptID;
                    }
                    if (m_postExecFunction != null && m_postExecFunction.QuickScriptID != -1)
                    {
                        m_PostExecQuickId = m_postExecFunction.QuickScriptID;
                    }
                    break;
            }
        }
        #endregion

        #region execution en mode commande
        void SrcExecuter_EventScriptExecuted(int quickID)
        {
            if (quickID == m_TriggerQuickId)
            {
                DoDataCopy();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ExecTimer_Tick(object sender, EventArgs e)
        {
            DoDataCopy();
        }

        /// <summary>
        /// 
        /// </summary>
        protected void DoDataCopy()
        {
            if (m_SrcObjDataList.Count == m_DstObjDataList.Count)
            {
                for (int i = 0; i < m_SrcObjDataList.Count; i++)
                {
                    if (m_SrcObjDataList[i] != null && m_DstObjDataList[i] != null)
                        m_DstObjDataList[i].Value = m_SrcObjDataList[i].Value;
                }
            }
            if (m_PostExecQuickId != 0)
                m_DstDocObj.Executer.ExecuteScript(m_PostExecQuickId);
        }
        #endregion

    }
}

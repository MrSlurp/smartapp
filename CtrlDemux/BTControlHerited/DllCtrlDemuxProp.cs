using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using CommonLib;


namespace CtrlDemux
{
    internal class DllCtrlDemuxProp : SpecificControlProp
    {
        #region constantes
        private const string NODE_DEMUX_DATA = "DEMUX_DATA";
        private const string NODE_DEMUX_PROP = "DEMUX_PROP";
        private const string ATTR_ADDR_DATA = "AddrData";
        private const string ATTR_VAL_DATA = "ValueData";
        #endregion
        // ajouter ici les données membres des propriété
        string m_strAdressData = string.Empty;
        string m_strValueData = string.Empty;
        
        StringCollection m_ListDemuxData = new StringCollection();

        // ajouter ici les accesseur vers les données membres des propriété
        public StringCollection ListDemuxData
        {
            get
            {
                return m_ListDemuxData;
            }
        }
        
        public string AdressData
        {
            get
            {
                return m_strAdressData;
            }
            set
            {
                m_strAdressData = value;
            }
        }

        public string ValueData
        {
            get
            {
                return m_strValueData;
            }
            set
            {
                m_strValueData = value;
            }
        }

        public override bool ReadIn(System.Xml.XmlNode Node)
        {
            if (Node.FirstChild != null)
            {
                for (int ch = 0; ch < Node.ChildNodes.Count; ch++)
                {
                    if (Node.ChildNodes[ch].Name == NODE_DEMUX_PROP)
                    {
                        XmlNode AttrAddrData = Node.ChildNodes[ch].Attributes.GetNamedItem(ATTR_ADDR_DATA);
                        XmlNode AttrValueData = Node.ChildNodes[ch].Attributes.GetNamedItem(ATTR_VAL_DATA);
                        this.m_strAdressData = AttrAddrData.Value;
                        this.m_strValueData = AttrValueData.Value;
                    }
                    else if (Node.ChildNodes[ch].Name == NODE_DEMUX_DATA)
                    {
                        XmlNode AttrDataSymbol = Node.ChildNodes[ch].Attributes.GetNamedItem(XML_CF_ATTRIB.strSymbol.ToString());
                        m_ListDemuxData.Add(AttrDataSymbol.Value);
                    }
                }
            }
            return true;
        }

        public override bool WriteOut(XmlDocument XmlDoc, XmlNode Node)
        {
            for (int i = 0; i < m_ListDemuxData.Count; i++)
            {
                XmlNode NodeDemuxData = XmlDoc.CreateElement(NODE_DEMUX_DATA);

                XmlAttribute AttrSymbol = XmlDoc.CreateAttribute(XML_CF_ATTRIB.strSymbol.ToString());
                AttrSymbol.Value = m_ListDemuxData[i];
                NodeDemuxData.Attributes.Append(AttrSymbol);
                Node.AppendChild(NodeDemuxData);
            }
            XmlNode NodeDemuxProp = XmlDoc.CreateElement(NODE_DEMUX_PROP);
            XmlAttribute AttrAddrData = XmlDoc.CreateAttribute(ATTR_ADDR_DATA);
            XmlAttribute AttrValueData = XmlDoc.CreateAttribute(ATTR_VAL_DATA);
            AttrAddrData.Value = m_strAdressData;
            AttrValueData.Value = m_strValueData;
            NodeDemuxProp.Attributes.Append(AttrAddrData);
            NodeDemuxProp.Attributes.Append(AttrValueData);
            Node.AppendChild(NodeDemuxProp);
            return true;
        }

        public override void CopyParametersFrom(SpecificControlProp SrcSpecificProp)
        {
            if (SrcSpecificProp.GetType() == typeof(DllCtrlDemuxProp))
            {
                m_ListDemuxData.Clear();
                for (int i = 0; i < ((DllCtrlDemuxProp)SrcSpecificProp).m_ListDemuxData.Count; i++)
                {
                  m_ListDemuxData.Add(((DllCtrlDemuxProp)SrcSpecificProp).m_ListDemuxData[i]);
                }
                m_strAdressData = ((DllCtrlDemuxProp)SrcSpecificProp).m_strAdressData;
                m_strValueData = ((DllCtrlDemuxProp)SrcSpecificProp).m_strValueData;
            }
        }
		
        public override void TraiteMessage(MESSAGE Mess, object obj, TYPE_APP TypeApp, BTControl PropOwner)
        {
            if (TypeApp == TYPE_APP.SMART_CONFIG)
            {
                switch (Mess)
                {
                    case MESSAGE.MESS_ASK_ITEM_DELETE:
                        if (((MessAskDelete)obj).TypeOfItem == typeof(Data))
                        {
                            MessAskDelete MessParam = (MessAskDelete)obj;
                            string strMess = string.Empty;
                            if (MessParam.WantDeletetItemSymbol == m_strAdressData)
                            {
                                strMess = string.Format(DllEntryClass.LangSys.C("Demux {0} : Adresse Data will be removed"), PropOwner.Symbol);
                                MessParam.ListStrReturns.Add(strMess);
                            }
                            if (MessParam.WantDeletetItemSymbol == m_strValueData)
                            {
                                strMess = string.Format(DllEntryClass.LangSys.C("Demux {0} : Value Data will be removed"), PropOwner.Symbol);
                                MessParam.ListStrReturns.Add(strMess);
                            }
                            for (int i = 0; i < m_ListDemuxData.Count; i++)
                            {
                                if (m_ListDemuxData[i] == MessParam.WantDeletetItemSymbol)
                                {
                                    strMess = string.Format(DllEntryClass.LangSys.C("Demux {0} : Ouput Data will be removed"), PropOwner.Symbol);
                                    MessParam.ListStrReturns.Add(strMess);
                                }
                            }
                        }
                        break;
                    case MESSAGE.MESS_ITEM_DELETED:
                        if (((MessDeleted)obj).TypeOfItem == typeof(Data))
                        {
                            MessDeleted MessParam = (MessDeleted)obj;
                            if (MessParam.DeletetedItemSymbol == m_strAdressData)
                            {
                                m_strAdressData = string.Empty;
                            }
                            if (MessParam.DeletetedItemSymbol == m_strValueData)
                            {
                                m_strValueData = string.Empty;
                            }
                            for (int i = 0; i < m_ListDemuxData.Count; i++)
                            {
                                if (m_ListDemuxData[i] == MessParam.DeletetedItemSymbol)
                                {
                                    m_ListDemuxData.RemoveAt(i);
                                }
                            }
                        }
                        break;
                    case MESSAGE.MESS_ITEM_RENAMED:
                        if (((MessItemRenamed)obj).TypeOfItem == typeof(Data))
                        {
                            MessItemRenamed MessParam = (MessItemRenamed)obj;
                            if (MessParam.OldItemSymbol == m_strAdressData)
                            {
                                m_strAdressData = MessParam.NewItemSymbol;
                            }
                            if (MessParam.OldItemSymbol == m_strValueData)
                            {
                                m_strValueData = MessParam.NewItemSymbol;
                            }
                            for (int i = 0; i < m_ListDemuxData.Count; i++)
                            {
                                if (m_ListDemuxData[i] == MessParam.NewItemSymbol)
                                {
                                    m_ListDemuxData[i] = MessParam.NewItemSymbol;
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }		
    }
}

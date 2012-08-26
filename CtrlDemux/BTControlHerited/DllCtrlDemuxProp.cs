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

        public DllCtrlDemuxProp(ItemScriptsConainter scriptContainter) : base(scriptContainter) { }

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

        public override bool ReadIn(XmlNode Node, BTDoc document)
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

        public override bool WriteOut(XmlDocument XmlDoc, XmlNode Node, BTDoc document)
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

        public override void CopyParametersFrom(SpecificControlProp SrcSpecificProp, bool bFromOtherInstance, BTDoc document)
        {
            if (SrcSpecificProp is DllCtrlDemuxProp)
            {
                DllCtrlDemuxProp SpecProps = SrcSpecificProp as DllCtrlDemuxProp;
                m_ListDemuxData.Clear();
                for (int i = 0; i < SpecProps.m_ListDemuxData.Count; i++)
                {
                    string strTmp = BTControl.CheckAndDoAssociateDataCopy(document, SpecProps.m_ListDemuxData[i]);
                    if (!string.IsNullOrEmpty(strTmp))
                        m_ListDemuxData.Add(strTmp);
                }
                m_strAdressData = BTControl.CheckAndDoAssociateDataCopy(document, SpecProps.m_strAdressData);
                m_strValueData = BTControl.CheckAndDoAssociateDataCopy(document, SpecProps.m_strValueData);
            }
        }
		
        public override void TraiteMessage(MESSAGE Mess, object obj, TYPE_APP TypeApp, BTControl PropOwner)
        {
            if (TypeApp == TYPE_APP.SMART_CONFIG)
            {
                BTControl.TraiteMessageDataDelete(Mess, obj, m_strAdressData, PropOwner, DllEntryClass.LangSys.C("Demux {0} : Adresse Data will be removed"));
                BTControl.TraiteMessageDataDelete(Mess, obj, m_strValueData, PropOwner, DllEntryClass.LangSys.C("Demux {0} : Value Data will be removed"));
                for (int i = 0; i < m_ListDemuxData.Count; i++)
                {
                    BTControl.TraiteMessageDataDelete(Mess, obj, m_ListDemuxData[i], PropOwner, DllEntryClass.LangSys.C("Demux {0} : Ouput Data will be removed"));
                    m_ListDemuxData[i] = BTControl.TraiteMessageDataDeleted(Mess, obj, m_ListDemuxData[i]);
                    m_ListDemuxData[i] = BTControl.TraiteMessageDataRenamed(Mess, obj, m_ListDemuxData[i]);
                }
            }
        }		
    }
}

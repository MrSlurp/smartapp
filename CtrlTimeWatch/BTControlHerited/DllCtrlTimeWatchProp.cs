using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using CommonLib;


namespace CtrlTimeWatch
{
    internal class DllCtrlTimeWatchProp : SpecificControlProp
    {
        private const string SPEC_SECTION = "TIMEWATCHPARAM";
        private const string HOUR_DATA = "HoursData";
        private const string MINUTES_DATA = "MinutesData";
        private const string SECOND_DATA = "SecondsData";

        private string m_strDataHours = string.Empty;

        private string m_strDataMinutes = string.Empty;

        private string m_strDataSecond = string.Empty;

        public DllCtrlTimeWatchProp(ItemScriptsConainter scriptContainter) : base(scriptContainter) { }

        public string DataHours
        {
            get
            {
                return m_strDataHours;
            }
            set
            {
                m_strDataHours = value;
            }
        }

        public string DataMinutes
        {
            get
            {
                return m_strDataMinutes;
            }
            set
            {
                m_strDataMinutes = value;
            }
        }

        public string DataSecond
        {
            get
            {
                return m_strDataSecond;
            }
            set
            {
                m_strDataSecond = value;
            }
        }

        public override bool ReadIn(System.Xml.XmlNode Node)
        {
            for (int ch = 0; ch < Node.ChildNodes.Count ; ch++)
            {
                if (Node.ChildNodes[ch].Name == SPEC_SECTION)
                {
                    XmlNode DataHours = Node.ChildNodes[ch].Attributes.GetNamedItem(HOUR_DATA);
                    XmlNode DataMinutes = Node.ChildNodes[ch].Attributes.GetNamedItem(MINUTES_DATA);
                    XmlNode DataSecond = Node.ChildNodes[ch].Attributes.GetNamedItem(SECOND_DATA);

                    m_strDataHours = DataHours.Value;
                    m_strDataMinutes = DataMinutes.Value;
                    m_strDataSecond = DataSecond.Value;
                    break;
                }
            }
            return true;
        }

        public override bool WriteOut(XmlDocument XmlDoc, XmlNode Node)
        {
            XmlNode ElemSpecSection = XmlDoc.CreateElement(SPEC_SECTION);
            XmlAttribute AttrDataHours = XmlDoc.CreateAttribute(HOUR_DATA);
            XmlAttribute AttrDataMinutes = XmlDoc.CreateAttribute(MINUTES_DATA);
            XmlAttribute AttrDataSecond = XmlDoc.CreateAttribute(SECOND_DATA);

            AttrDataHours.Value = m_strDataHours;
            AttrDataMinutes.Value = m_strDataMinutes;
            AttrDataSecond.Value = m_strDataSecond;
            ElemSpecSection.Attributes.Append(AttrDataHours);
            ElemSpecSection.Attributes.Append(AttrDataMinutes);
            ElemSpecSection.Attributes.Append(AttrDataSecond);
            Node.AppendChild(ElemSpecSection);
            return true;
        }

        public override void CopyParametersFrom(SpecificControlProp SrcSpecificProp, bool bFromOtherInstance)
        {
            if (SrcSpecificProp.GetType() == typeof(DllCtrlTimeWatchProp))
            {
                if (!bFromOtherInstance)
                {
                    m_strDataHours = ((DllCtrlTimeWatchProp)SrcSpecificProp).m_strDataHours;
                    m_strDataMinutes = ((DllCtrlTimeWatchProp)SrcSpecificProp).m_strDataMinutes;
                    m_strDataSecond = ((DllCtrlTimeWatchProp)SrcSpecificProp).m_strDataSecond;
                }
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
                            if (MessParam.WantDeletetItemSymbol == m_strDataHours)
                            {
                                strMess = string.Format(DllEntryClass.LangSys.C("TimeWatch {0} : hours data will be removed"), PropOwner.Symbol);
                                MessParam.ListStrReturns.Add(strMess);
                            }
                            if (MessParam.WantDeletetItemSymbol == m_strDataMinutes)
                            {
                                strMess = string.Format(DllEntryClass.LangSys.C("TimeWatch {0} : minutes data will be removed"), PropOwner.Symbol);
                                MessParam.ListStrReturns.Add(strMess);
                            }
                            if (MessParam.WantDeletetItemSymbol == m_strDataSecond)
                            {
                                strMess = string.Format(DllEntryClass.LangSys.C("TimeWatch {0} : seconds data will be removed"), PropOwner.Symbol);
                                MessParam.ListStrReturns.Add(strMess);
                            }
                        }
                        break;
                    case MESSAGE.MESS_ITEM_DELETED:
                        if (((MessDeleted)obj).TypeOfItem == typeof(Data))
                        {
                            MessDeleted MessParam = (MessDeleted)obj;
                            if (MessParam.DeletetedItemSymbol == m_strDataHours)
                            {
                                m_strDataHours = string.Empty;
                            }
                            if (MessParam.DeletetedItemSymbol == m_strDataMinutes)
                            {
                                m_strDataMinutes = string.Empty;
                            }
                            if (MessParam.DeletetedItemSymbol == m_strDataSecond)
                            {
                                m_strDataSecond = string.Empty;
                            }
                        }
                        break;
                    case MESSAGE.MESS_ITEM_RENAMED:
                        if (((MessItemRenamed)obj).TypeOfItem == typeof(Data))
                        {
                            MessItemRenamed MessParam = (MessItemRenamed)obj;
                            if (MessParam.OldItemSymbol == m_strDataHours)
                            {
                                m_strDataHours = MessParam.NewItemSymbol;
                            }
                            if (MessParam.OldItemSymbol == m_strDataMinutes)
                            {
                                m_strDataMinutes = MessParam.NewItemSymbol;
                            }
                            if (MessParam.OldItemSymbol == m_strDataSecond)
                            {
                                m_strDataSecond = MessParam.NewItemSymbol;
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

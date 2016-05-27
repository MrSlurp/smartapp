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

        public override bool ReadIn(XmlNode Node, BTDoc document)
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

        public override bool WriteOut(XmlDocument XmlDoc, XmlNode Node, BTDoc document)
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

        public override void CopyParametersFrom(SpecificControlProp SrcSpecificProp, bool bFromOtherInstance, BTDoc document)
        {
            if (SrcSpecificProp is DllCtrlTimeWatchProp)
            {
                DllCtrlTimeWatchProp SpecProps = SrcSpecificProp as DllCtrlTimeWatchProp;
                m_strDataHours = BTControl.CheckAndDoAssociateDataCopy(document, SpecProps.m_strDataHours);
                m_strDataMinutes = BTControl.CheckAndDoAssociateDataCopy(document, SpecProps.m_strDataMinutes);
                m_strDataSecond = BTControl.CheckAndDoAssociateDataCopy(document, SpecProps.m_strDataSecond);
            }
        }

        public override void TraiteMessage(MESSAGE Mess, object obj, TYPE_APP TypeApp, BTControl PropOwner)
        {
            if (TypeApp == TYPE_APP.SMART_CONFIG)
            {
                BTControl.TraiteMessageDataDelete(Mess, obj, m_strDataHours, PropOwner, DllEntryClass.LangSys.C("TimeWatch {0} : hours data will be removed"));
                BTControl.TraiteMessageDataDelete(Mess, obj, m_strDataMinutes, PropOwner, DllEntryClass.LangSys.C("TimeWatch {0} : minutes data will be removed"));
                BTControl.TraiteMessageDataDelete(Mess, obj, m_strDataSecond, PropOwner, DllEntryClass.LangSys.C("TimeWatch {0} : seconds data will be removed"));

                m_strDataHours = BTControl.TraiteMessageDataDeleted(Mess, obj, m_strDataHours);
                m_strDataMinutes = BTControl.TraiteMessageDataDeleted(Mess, obj, m_strDataMinutes);
                m_strDataSecond = BTControl.TraiteMessageDataDeleted(Mess, obj, m_strDataSecond);

                m_strDataHours = BTControl.TraiteMessageDataRenamed(Mess, obj, m_strDataHours);
                m_strDataMinutes = BTControl.TraiteMessageDataDeleted(Mess, obj, m_strDataMinutes);
                m_strDataSecond = BTControl.TraiteMessageDataDeleted(Mess, obj, m_strDataSecond);
            }
        }
    }
}

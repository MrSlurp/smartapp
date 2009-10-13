using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using CommonLib;


namespace CtrlDataTrigger
{
    class DllCtrlDataTriggerProp : SpecificControlProp
    {
        private const string SPEC_SECTION = "TRIGGERPARAM";
        private const string BEHAVE_SCHMITT = "SchmittBehave";
        private const string ON_TO_OFF = "OnToOff";
        private const string OFF_TO_ON = "OffToOn";
        private const string SCRIPT_ON_OFF = "ONOFFSCRIPT";
        private const string SCRIPT_OFF_ON = "OFFONSCRIPT";

        private bool m_bBehaveLikeTrigger = false;

        private string m_strDataOnToOff = string.Empty;

        private string m_strDataOffToOn = string.Empty;

        private StringCollection m_ScriptOnToOff = new StringCollection();

        private StringCollection m_ScriptOffToOn = new StringCollection();

        public bool BehaveLikeTrigger
        {
            get
            {
                return m_bBehaveLikeTrigger;
            }
            set
            {
                m_bBehaveLikeTrigger = value;
            }
        }

        public string DataOnToOff
        {
            get
            {
                return m_strDataOnToOff;
            }
            set
            {
                m_strDataOnToOff = value;
            }
        }

        public string DataOffToOn
        {
            get
            {
                return m_strDataOffToOn;
            }
            set
            {
                m_strDataOffToOn = value;
            }
        }

        public string[] ScriptOnToOff
        {
            get
            {
                string[] TabLines = new string[m_ScriptOnToOff.Count];
                for (int i = 0; i < m_ScriptOnToOff.Count; i++)
                {
                    TabLines[i] = m_ScriptOnToOff[i];
                }
                return TabLines;
            }
            set
            {
                m_ScriptOnToOff.Clear();
                for (int i = 0; i < value.Length; i++)
                {
                    m_ScriptOnToOff.Add(value[i]);
                }
            }
        }

        public string[] ScriptOffToOn
        {
            get
            {
                string[] TabLines = new string[m_ScriptOffToOn.Count];
                for (int i = 0; i < m_ScriptOffToOn.Count; i++)
                {
                    TabLines[i] = m_ScriptOffToOn[i];
                }
                return TabLines;
            }
            set
            {
                m_ScriptOffToOn.Clear();
                for (int i = 0; i < value.Length; i++)
                {
                    m_ScriptOffToOn.Add(value[i]);
                }
            }
        }   
        public override bool ReadIn(System.Xml.XmlNode Node)
        {
            for (int ch = 0; ch < Node.ChildNodes.Count ; ch++)
            {
                if (Node.ChildNodes[ch].Name == SPEC_SECTION)
                {
                    XmlNode AttrBehaveTrigger = Node.ChildNodes[ch].Attributes.GetNamedItem(BEHAVE_SCHMITT);
                    XmlNode AttrOnOff = Node.ChildNodes[ch].Attributes.GetNamedItem(ON_TO_OFF);
                    XmlNode AttrOffOn = Node.ChildNodes[ch].Attributes.GetNamedItem(OFF_TO_ON);

                    m_bBehaveLikeTrigger = bool.Parse(AttrBehaveTrigger.Value);
                    m_strDataOnToOff = AttrOnOff.Value;
                    m_strDataOffToOn = AttrOffOn.Value;
                    break;
                }
            }
            base.ReadScript(ref m_ScriptOnToOff, Node, SCRIPT_ON_OFF);
            base.ReadScript(ref m_ScriptOffToOn, Node, SCRIPT_OFF_ON);

            return true;
        }

        public override bool WriteOut(XmlDocument XmlDoc, XmlNode Node)
        {
            XmlNode ElemSpecSection = XmlDoc.CreateElement(SPEC_SECTION);
            XmlAttribute AttrBehaveTrigger = XmlDoc.CreateAttribute(BEHAVE_SCHMITT);
            XmlAttribute AttrOnOff = XmlDoc.CreateAttribute(ON_TO_OFF);
            XmlAttribute AttrOffOn = XmlDoc.CreateAttribute(OFF_TO_ON);

            AttrBehaveTrigger.Value = m_bBehaveLikeTrigger.ToString();
            AttrOnOff.Value = m_strDataOnToOff;
            AttrOffOn.Value = m_strDataOffToOn;
            ElemSpecSection.Attributes.Append(AttrBehaveTrigger);
            ElemSpecSection.Attributes.Append(AttrOnOff);
            ElemSpecSection.Attributes.Append(AttrOffOn);
            Node.AppendChild(ElemSpecSection);
            base.WriteScript(m_ScriptOnToOff, XmlDoc, Node, SCRIPT_ON_OFF);
            base.WriteScript(m_ScriptOffToOn, XmlDoc, Node, SCRIPT_OFF_ON);
            return true;
        }

        public override void CopyParametersFrom(SpecificControlProp SrcSpecificProp)
        {
            if (SrcSpecificProp.GetType() == typeof(DllCtrlDataTriggerProp))
            {
                m_bBehaveLikeTrigger = ((DllCtrlDataTriggerProp)SrcSpecificProp).m_bBehaveLikeTrigger;
                m_strDataOffToOn = ((DllCtrlDataTriggerProp)SrcSpecificProp).m_strDataOffToOn;
                m_strDataOnToOff = ((DllCtrlDataTriggerProp)SrcSpecificProp).m_strDataOnToOff;

                base.CopyScript(ref m_ScriptOffToOn, ((DllCtrlDataTriggerProp)SrcSpecificProp).m_ScriptOffToOn);
                base.CopyScript(ref m_ScriptOnToOff, ((DllCtrlDataTriggerProp)SrcSpecificProp).m_ScriptOnToOff);
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
                            if (MessParam.WantDeletetItemSymbol == m_strDataOffToOn)
                            {
                                strMess = string.Format("Data Trigger {0} : Data \"Off to On\" will be removed", PropOwner.Symbol);
                                MessParam.ListStrReturns.Add(strMess);
                            }
                            if (MessParam.WantDeletetItemSymbol == m_strDataOnToOff)
                            {
                                strMess = string.Format("Data Trigger {0} : Data \"On to Off\" will be removed", PropOwner.Symbol);
                                MessParam.ListStrReturns.Add(strMess);
                            }
                        }
                        base.ScriptTraiteMessage(Mess, m_ScriptOffToOn, obj, PropOwner);
                        base.ScriptTraiteMessage(Mess, m_ScriptOnToOff, obj, PropOwner);
                        break;
                    case MESSAGE.MESS_ITEM_DELETED:
                        if (((MessDeleted)obj).TypeOfItem == typeof(Data))
                        {
                            MessDeleted MessParam = (MessDeleted)obj;
                            if (MessParam.DeletetedItemSymbol == m_strDataOffToOn)
                            {
                                m_strDataOffToOn = string.Empty;
                            }
                            if (MessParam.DeletetedItemSymbol == m_strDataOnToOff)
                            {
                                m_strDataOnToOff = string.Empty;
                            }
                        }
                        base.ScriptTraiteMessage(Mess, m_ScriptOffToOn, obj, PropOwner);
                        base.ScriptTraiteMessage(Mess, m_ScriptOnToOff, obj, PropOwner);
                        break;
                    case MESSAGE.MESS_ITEM_RENAMED:
                        if (((MessItemRenamed)obj).TypeOfItem == typeof(Data))
                        {
                            MessItemRenamed MessParam = (MessItemRenamed)obj;
                            if (MessParam.OldItemSymbol == m_strDataOffToOn)
                            {
                                m_strDataOffToOn = MessParam.NewItemSymbol;
                            }
                            if (MessParam.OldItemSymbol == m_strDataOnToOff)
                            {
                                m_strDataOnToOff = MessParam.NewItemSymbol;
                            }
                        }
                        base.ScriptTraiteMessage(Mess, m_ScriptOffToOn, obj, PropOwner);
                        base.ScriptTraiteMessage(Mess, m_ScriptOnToOff, obj, PropOwner);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}

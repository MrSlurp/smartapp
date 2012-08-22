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

        private int m_iQuickScriptIDOnToOff;
        private int m_iQuickScriptIDOffToOn;

        public DllCtrlDataTriggerProp(ItemScriptsConainter scriptContainter) : base(scriptContainter) 
        { 
            m_scriptContainer["OnToOffScript"] = new string[1];
            m_scriptContainer["OffToOnScript"] = new string[1];
        }

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
                return m_scriptContainer["OnToOffScript"];
            }
            set
            {
                m_scriptContainer["OnToOffScript"] = value;
            }
        }

        public string[] ScriptOffToOn
        {
            get
            {
                return m_scriptContainer["OffToOnScript"];
            }
            set
            {
                m_scriptContainer["OffToOnScript"] = value;
            }
        }

        public int QuickScriptIDOnToOff
        {
            get
            {
                return m_iQuickScriptIDOnToOff;
            }
            set
            {
                m_iQuickScriptIDOnToOff = value;
            }
        }

        public int QuickScriptIDOffToOn
        {
            get
            {
                return m_iQuickScriptIDOffToOn;
            }
            set
            {
                m_iQuickScriptIDOffToOn = value;
            }
        }

        public override bool ReadIn(XmlNode Node, BTDoc document)
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
            string[] tmp;
            base.ReadScript(out tmp, Node, SCRIPT_ON_OFF);
            this.m_scriptContainer["OnToOffScript"] = tmp;
            base.ReadScript(out tmp, Node, SCRIPT_OFF_ON);
            this.m_scriptContainer["OffToOnScript"] = tmp;

            return true;
        }

        public override bool WriteOut(XmlDocument XmlDoc, XmlNode Node, BTDoc document)
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
            base.WriteScript(m_scriptContainer["OnToOffScript"], XmlDoc, Node, SCRIPT_ON_OFF);
            base.WriteScript(m_scriptContainer["OffToOnScript"], XmlDoc, Node, SCRIPT_OFF_ON);
            return true;
        }

        public override void CopyParametersFrom(SpecificControlProp SrcSpecificProp, bool bFromOtherInstance, BTDoc document)
        {
            if (SrcSpecificProp is DllCtrlDataTriggerProp)
            {
                DllCtrlDataTriggerProp SrcProps = SrcSpecificProp as DllCtrlDataTriggerProp;
                if (!bFromOtherInstance)
                {
                    m_bBehaveLikeTrigger = SrcProps.m_bBehaveLikeTrigger;
                    m_strDataOffToOn = SrcProps.m_strDataOffToOn;
                    m_strDataOnToOff = SrcProps.m_strDataOnToOff;
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
                            if (MessParam.WantDeletetItemSymbol == m_strDataOffToOn)
                            {
                                strMess = string.Format(DllEntryClass.LangSys.C("Data Trigger {0} : Off to On Data will be removed"), PropOwner.Symbol);
                                MessParam.ListStrReturns.Add(strMess);
                            }
                            if (MessParam.WantDeletetItemSymbol == m_strDataOnToOff)
                            {
                                strMess = string.Format(DllEntryClass.LangSys.C("Data Trigger {0} : On to Off Data will be removed"), PropOwner.Symbol);
                                MessParam.ListStrReturns.Add(strMess);
                            }
                        }
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
                        break;
                    default:
                        break;
                }
            }
        }
    }
}

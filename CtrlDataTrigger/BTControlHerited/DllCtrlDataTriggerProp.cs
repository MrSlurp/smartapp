/*
    This file is part of SmartApp.
    Copyright (C) 2007-2016  Pascal Bigot

    SmartApp is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    SmartApp is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with Foobar.  If not, see <http://www.gnu.org/licenses/>.
*/
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
                BTControl.TraiteMessageDataDelete(Mess, obj, m_strDataOffToOn, PropOwner, DllEntryClass.LangSys.C("Data Trigger {0} : Off to On Data will be removed"));
                BTControl.TraiteMessageDataDelete(Mess, obj, m_strDataOnToOff, PropOwner, DllEntryClass.LangSys.C("Data Trigger {0} : Off to On Data will be removed"));

                m_strDataOffToOn = BTControl.TraiteMessageDataDeleted(Mess, obj, m_strDataOffToOn);
                m_strDataOnToOff = BTControl.TraiteMessageDataDeleted(Mess, obj, m_strDataOnToOff);
                m_strDataOffToOn = BTControl.TraiteMessageDataRenamed(Mess, obj, m_strDataOffToOn);
                m_strDataOnToOff = BTControl.TraiteMessageDataRenamed(Mess, obj, m_strDataOnToOff);
            }
        }
    }
}

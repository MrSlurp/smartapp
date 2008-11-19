using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace CommonLib
{
    public delegate void EventVirtualDataValueChange(VirtualData vData);
    public class VirtualData : Data
    {
        public event EventVirtualDataValueChange VirtualDataValueChanged;

        public override int Value
        {
            get
            {
                return m_CurrentVal;
            }
            set
            {
                // on test d'abord savoir si la valeur a changé
                bool bValueChange = false;
                if (m_CurrentVal != value)
                    bValueChange = true;
                // selon les bornes et si la donnée est constante, on effectue l'assignation...
                if (value >= m_MinVal && value <= m_MaxVal && !this.IsConstant)
                {
                    m_CurrentVal = value;
                }
                else if (!this.IsConstant)
                {
                    // ...ou la saturation en cas de dépassement des bornes
                    if (value < m_MinVal)
                    {
                        m_CurrentVal = m_MinVal;
                        LogEvent log = new LogEvent(LOG_EVENT_TYPE.INFO, string.Format("Data {0} : min value saturation ({1})", Symbol, m_MinVal));
                        AddLogEvent(log);
                    }
                    else if (value > m_MaxVal)
                    {
                        m_CurrentVal = m_MaxVal;
                        LogEvent log = new LogEvent(LOG_EVENT_TYPE.INFO, string.Format("Data {0} : max value saturation ({1})", Symbol, m_MaxVal));
                        AddLogEvent(log);
                    }
                }
                else
                {
                    LogEvent log = new LogEvent(LOG_EVENT_TYPE.WARNING, string.Format("Data {0} is constant, value can't be set", Symbol));
                    AddLogEvent(log);
                }
                // si la valeur de la donnée a changé on le notifie
                if (bValueChange && VirtualDataValueChanged != null)
                    VirtualDataValueChanged(this);
            }
        }

        public bool TestValue(int Value)
        {
            // ...ou la saturation en cas de dépassement des bornes
            if (Value < m_MinVal)
            {
                return false;
            }
            else if (Value > m_MaxVal)
            {
                return false;
            }
            return true;
        }

        public override bool WriteOut(XmlDocument XmlDoc, XmlNode Node)
        {
            // il est interdit de faire une écriture complète des paramètres pour un
            return false;
        }

        public bool ReadInInstantImage(XmlNode Node, TYPE_APP TypeApp)
        {
            XmlNode AttrDefVal = Node.Attributes.GetNamedItem(XML_CF_ATTRIB.DefVal.ToString());
            if (AttrDefVal == null)
                return false;
            DefaultValue = int.Parse(AttrDefVal.Value);
            this.InitVal();
            return true;
        }

        public bool WriteOutInstantImage(XmlDocument XmlDoc, XmlNode Node)
        {
            // on écrit la base pour avoir le symbole
            base.WriteOut(XmlDoc, Node);
            this.DefaultValue = this.Value;
            XmlAttribute AttrDefVal = XmlDoc.CreateAttribute(XML_CF_ATTRIB.DefVal.ToString());
            AttrDefVal.Value = DefaultValue.ToString();
            Node.Attributes.Append(AttrDefVal);
            return true;
        }

    }
}

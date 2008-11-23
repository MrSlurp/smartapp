using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using CommonLib;


namespace FormatedDisplay
{
    class DllFormatedDisplayProp : SpecificControlProp
    {
        private string m_FormatString = ":F0";

        public string FormatString
        {
            get
            {
                return m_FormatString;
            }
            set
            {
                m_FormatString = value;
            }
        }

        public override bool ReadIn(System.Xml.XmlNode Node)
        {
            return true;
        }

        public override bool WriteOut(XmlDocument XmlDoc, XmlNode Node)
        {
            return true;
        }
    }
}

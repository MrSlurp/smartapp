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

        private const string NOM_ATTIB_FORMAT = "Format";
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
            XmlNode AttrFormat = Node.Attributes.GetNamedItem(NOM_ATTIB_FORMAT);
            if (AttrFormat == null)
                return false;
            FormatString = AttrFormat.Value;
            return true;
        }

        public override bool WriteOut(XmlDocument XmlDoc, XmlNode Node)
        {
            XmlAttribute AttrFormat = XmlDoc.CreateAttribute(NOM_ATTIB_FORMAT);
            AttrFormat.Value = FormatString;
            Node.Attributes.Append(AttrFormat);
            return true;
        }

        public override void CopyParametersFrom(SpecificControlProp SrcSpecificProp, bool bFromOtherInstance)
        {
            FormatString = ((DllFormatedDisplayProp)SrcSpecificProp).FormatString;
        }

    }
}

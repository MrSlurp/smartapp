using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using CommonLib;


namespace DigitalDisplay
{
    class DllDigitalDisplayProp : SpecificControlProp
    {
        private string m_FormatString = ":F0";
        Color m_DigitColor = Color.GreenYellow;

        private const string NOM_ATTRIB_FORMAT = "Format";
        private const string NOM_ATTRIB_COLOR = "DigitColor";

        public Color DigitColor
        {
            get
            { return m_DigitColor; }
            set
            { m_DigitColor = value; }
        }


        public string FormatString
        {
            get
            { return m_FormatString; }
            set
            { m_FormatString = value; }
        }

        public override bool ReadIn(System.Xml.XmlNode Node)
        {
            XmlNode AttrFormat = Node.Attributes.GetNamedItem(NOM_ATTRIB_FORMAT);
            XmlNode AttrColor = Node.Attributes.GetNamedItem(NOM_ATTRIB_COLOR);
            if (AttrFormat == null || AttrColor == null)
                return false;
            FormatString = AttrFormat.Value;

            string[] rgbVal = AttrColor.Value.Split(',');
            int r = int.Parse(rgbVal[0]);
            int g = int.Parse(rgbVal[1]);
            int b = int.Parse(rgbVal[2]);
            this.DigitColor = Color.FromArgb(r, g, b);

            return true;
        }

        public override bool WriteOut(XmlDocument XmlDoc, XmlNode Node)
        {
            XmlAttribute AttrFormat = XmlDoc.CreateAttribute(NOM_ATTRIB_FORMAT);
            XmlAttribute AttrColor = XmlDoc.CreateAttribute(NOM_ATTRIB_COLOR);
            AttrFormat.Value = FormatString;
            AttrColor.Value = string.Format("{0}, {1}, {2}", DigitColor.R, DigitColor.G, DigitColor.B);

            Node.Attributes.Append(AttrColor);
            Node.Attributes.Append(AttrFormat);
            return true;
        }

        public override void CopyParametersFrom(SpecificControlProp SrcSpecificProp)
        {
            FormatString = ((DllDigitalDisplayProp)SrcSpecificProp).FormatString;
            DigitColor = ((DllDigitalDisplayProp)SrcSpecificProp).DigitColor;
        }
    }
}

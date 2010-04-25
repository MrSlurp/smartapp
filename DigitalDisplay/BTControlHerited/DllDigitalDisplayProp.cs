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
        Color m_BackColor = Color.Black;

        private const string NOM_ATTRIB_FORMAT = "Format";
        private const string NOM_ATTRIB_COLOR = "DigitColor";
        private const string NOM_ATTRIB_BACKCOLOR = "DigitBackColor";

        public Color DigitColor
        {
            get
            { return m_DigitColor; }
            set
            { m_DigitColor = value; }
        }

        public Color BackColor
        {
            get
            { return m_BackColor; }
            set
            { m_BackColor = value; }
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
            XmlNode AttrBackColor = Node.Attributes.GetNamedItem(NOM_ATTRIB_BACKCOLOR);
            if (AttrFormat == null || AttrColor == null || AttrBackColor == null)
                return false;
            FormatString = AttrFormat.Value;

            string[] rgbVal = AttrColor.Value.Split(',');
            int r = int.Parse(rgbVal[0]);
            int g = int.Parse(rgbVal[1]);
            int b = int.Parse(rgbVal[2]);
            this.DigitColor = Color.FromArgb(r, g, b);

            string[] rgbValBack = AttrBackColor.Value.Split(',');
            int r2 = int.Parse(rgbValBack[0]);
            int g2 = int.Parse(rgbValBack[1]);
            int b2 = int.Parse(rgbValBack[2]);
            this.BackColor = Color.FromArgb(r2, g2, b2);

            return true;
        }

        public override bool WriteOut(XmlDocument XmlDoc, XmlNode Node)
        {
            XmlAttribute AttrFormat = XmlDoc.CreateAttribute(NOM_ATTRIB_FORMAT);
            XmlAttribute AttrColor = XmlDoc.CreateAttribute(NOM_ATTRIB_COLOR);
            XmlAttribute AttrColorBack = XmlDoc.CreateAttribute(NOM_ATTRIB_BACKCOLOR);
            AttrFormat.Value = FormatString;
            AttrColor.Value = string.Format("{0}, {1}, {2}", DigitColor.R, DigitColor.G, DigitColor.B);
            AttrColorBack.Value = string.Format("{0}, {1}, {2}", BackColor.R, BackColor.G, BackColor.B);

            Node.Attributes.Append(AttrColor);
            Node.Attributes.Append(AttrColorBack);
            Node.Attributes.Append(AttrFormat);
            return true;
        }

        public override void CopyParametersFrom(SpecificControlProp SrcSpecificProp, bool bFromOtherInstance)
        {
            FormatString = ((DllDigitalDisplayProp)SrcSpecificProp).FormatString;
            DigitColor = ((DllDigitalDisplayProp)SrcSpecificProp).DigitColor;
            BackColor = ((DllDigitalDisplayProp)SrcSpecificProp).BackColor;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;

namespace CommonLib
{
    public abstract class SpecificControlProp
    {
        public abstract bool ReadIn(XmlNode Node);
        public abstract bool WriteOut(XmlDocument XmlDoc, XmlNode Node);
    }

    public class TwoColorProp : SpecificControlProp
    {
        private Color m_ColorInactive = Color.Black;
        private Color m_ColorActive = Color.Blue;

        public Color ColorInactive
        {
            get
            {
                return m_ColorInactive;
            }
            set
            {
                m_ColorInactive = value;
            }
        }

        public Color ColorActive
        {
            get
            {
                return m_ColorActive;
            }
            set
            {
                m_ColorActive = value;
            }
        }

        public override bool ReadIn(XmlNode Node)
        {
            XmlNode AttrActiveColor = Node.Attributes.GetNamedItem(XML_CF_ATTRIB.ActiveColor.ToString());
            XmlNode AttrInactiveColor = Node.Attributes.GetNamedItem(XML_CF_ATTRIB.InactiveColor.ToString());
            if (AttrActiveColor == null
                || AttrInactiveColor == null)
                return false;

            string[] rgbVal = AttrActiveColor.Value.Split(',');
            int r = int.Parse(rgbVal[0]);
            int g = int.Parse(rgbVal[1]);
            int b = int.Parse(rgbVal[2]);
            this.ColorActive = Color.FromArgb(r, g, b);
            string[] rgbVal2 = AttrInactiveColor.Value.Split(',');
            int r2 = int.Parse(rgbVal2[0]);
            int g2 = int.Parse(rgbVal2[1]);
            int b2 = int.Parse(rgbVal2[2]);
            this.ColorInactive = Color.FromArgb(r2, g2, b2);
            return true;
        }

        public override bool WriteOut(XmlDocument XmlDoc, XmlNode Node)
        {
            XmlAttribute AttrActColor = XmlDoc.CreateAttribute(XML_CF_ATTRIB.ActiveColor.ToString());
            XmlAttribute AttrInactColor = XmlDoc.CreateAttribute(XML_CF_ATTRIB.InactiveColor.ToString());
            AttrActColor.Value = string.Format("{0}, {1}, {2}", ColorActive.R, ColorActive.G, ColorActive.B);
            AttrInactColor.Value = string.Format("{0}, {1}, {2}", ColorInactive.R, ColorInactive.G, ColorInactive.B);
            Node.Attributes.Append(AttrActColor);
            Node.Attributes.Append(AttrInactColor);
            return true;
        }
    }
}

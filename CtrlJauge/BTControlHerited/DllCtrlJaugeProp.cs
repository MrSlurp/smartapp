using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using CommonLib;


namespace CtrlJauge
{
    public enum eOrientationJauge
    {
        eHorizontaleGD = 0,
        eHorizontaleDG = 1,
        eVerticaleBT = 2,
        eVerticaleTB = 3,
    };

    class DllCtrlJaugeProp : SpecificControlProp
    {
        private const string ORIENTATION = "Orientation";
        private const string COLOR_MIN = "ColorMin";
        private const string COLOR_MAX = "ColorMax";

        eOrientationJauge m_Orientation = eOrientationJauge.eHorizontaleDG;
        Color m_ColorMax = Color.White;
        Color m_ColorMin = Color.Blue;

        // ajouter ici les accesseur vers les données membres des propriété
        public eOrientationJauge Orientation
        {
            get
            { return m_Orientation; }
            set
            { m_Orientation = value; }
        }

        public Color ColorMin
        {
            get
            { return m_ColorMin; }
            set
            { m_ColorMin = value; }
        }

        public Color ColorMax
        {
            get
            { return m_ColorMax; }
            set
            { m_ColorMax = value; }
        }

        public override bool ReadIn(System.Xml.XmlNode Node)
        {
            XmlNode AttrColorMin = Node.Attributes.GetNamedItem(COLOR_MIN);
            XmlNode AttrColorMax = Node.Attributes.GetNamedItem(COLOR_MAX);
            XmlNode AttrOrient = Node.Attributes.GetNamedItem(ORIENTATION);
            if (AttrColorMin == null
                || AttrColorMax == null
                || AttrOrient == null)
                return false;

            Orientation = (eOrientationJauge)int.Parse(AttrOrient.Value);
            this.ColorMin = ColorTranslate.StringToColor(AttrColorMin.Value);
            this.ColorMax = ColorTranslate.StringToColor(AttrColorMax.Value);
            return true;
        }

        public override bool WriteOut(XmlDocument XmlDoc, XmlNode Node)
        {
            XmlAttribute AttrColorMin = XmlDoc.CreateAttribute(COLOR_MIN);
            XmlAttribute AttrColorMax = XmlDoc.CreateAttribute(COLOR_MAX);
            XmlAttribute AttrOrient = XmlDoc.CreateAttribute(ORIENTATION);
            AttrColorMin.Value = ColorTranslate.ColorToString(ColorMin);
            AttrColorMax.Value = ColorTranslate.ColorToString(ColorMax); ;
            AttrOrient.Value = ((int)Orientation).ToString();
            Node.Attributes.Append(AttrColorMin);
            Node.Attributes.Append(AttrColorMax);
            Node.Attributes.Append(AttrOrient);
            return true;
        }

        public override void CopyParametersFrom(SpecificControlProp SrcSpecificProp)
        {
            Orientation = ((DllCtrlJaugeProp)SrcSpecificProp).Orientation;
            ColorMin = ((DllCtrlJaugeProp)SrcSpecificProp).ColorMin;
            ColorMax = ((DllCtrlJaugeProp)SrcSpecificProp).ColorMax;
        }

    }
}

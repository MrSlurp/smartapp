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

        public DllCtrlJaugeProp(ItemScriptsConainter scriptContainter) : base(scriptContainter) { }

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

        public override bool ReadIn(XmlNode Node, BTDoc document)
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

        public override bool WriteOut(XmlDocument XmlDoc, XmlNode Node, BTDoc document)
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

        public override void CopyParametersFrom(SpecificControlProp SrcSpecificProp, bool bFromOtherInstance, BTDoc document)
        {
            Orientation = ((DllCtrlJaugeProp)SrcSpecificProp).Orientation;
            ColorMin = ((DllCtrlJaugeProp)SrcSpecificProp).ColorMin;
            ColorMax = ((DllCtrlJaugeProp)SrcSpecificProp).ColorMax;
        }

    }
}

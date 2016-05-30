/*
    This file is part of SmartApp.

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

        public DllFormatedDisplayProp(ItemScriptsConainter scriptContainter) : base(scriptContainter) { }

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

        public override bool ReadIn(XmlNode Node, BTDoc document)
        {
            XmlNode AttrFormat = Node.Attributes.GetNamedItem(NOM_ATTIB_FORMAT);
            if (AttrFormat == null)
                return false;
            FormatString = AttrFormat.Value;
            return true;
        }

        public override bool WriteOut(XmlDocument XmlDoc, XmlNode Node, BTDoc document)
        {
            XmlAttribute AttrFormat = XmlDoc.CreateAttribute(NOM_ATTIB_FORMAT);
            AttrFormat.Value = FormatString;
            Node.Attributes.Append(AttrFormat);
            return true;
        }

        public override void CopyParametersFrom(SpecificControlProp SrcSpecificProp, bool bFromOtherInstance, BTDoc document)
        {
            FormatString = ((DllFormatedDisplayProp)SrcSpecificProp).FormatString;
        }

    }
}

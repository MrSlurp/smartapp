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


namespace CtrlTwoBitmap
{
    class TwoBitmapProp : SpecificControlProp
    {
        private string m_NomFichierInactif;
        private string m_NomFichierActif;
        private const string NOM_ATTIB_ACTIVE = "AciveBitmap";
        private const string NOM_ATTIB_INACTIVE = "InactiveBitmap";

        public TwoBitmapProp(ItemScriptsConainter scriptContainter) : base(scriptContainter) { }

        public string NomFichierInactif
        {
            get
            {
                return m_NomFichierInactif;
            }
            set
            {
                m_NomFichierInactif = value;
            }
        }

        public string NomFichierActif
        {
            get
            {
                return m_NomFichierActif;
            }
            set
            {
                m_NomFichierActif = value;
            }
        }

        public override bool ReadIn(XmlNode Node, BTDoc document)
        {
            XmlNode AttrActive = Node.Attributes.GetNamedItem(NOM_ATTIB_ACTIVE);
            XmlNode AttrInactive = Node.Attributes.GetNamedItem(NOM_ATTIB_INACTIVE);
            if (AttrActive == null
                || AttrInactive == null)
                return false;
            NomFichierActif = AttrActive.Value;
            NomFichierInactif = AttrInactive.Value;
            return true;
        }

        public override bool WriteOut(XmlDocument XmlDoc, XmlNode Node, BTDoc document)
        {
            XmlAttribute AttrActive = XmlDoc.CreateAttribute(NOM_ATTIB_ACTIVE);
            XmlAttribute AttrInactive = XmlDoc.CreateAttribute(NOM_ATTIB_INACTIVE);
            AttrActive.Value = document.PathTr.AbsolutePathToRelative(m_NomFichierActif);
            AttrInactive.Value = document.PathTr.AbsolutePathToRelative(m_NomFichierInactif);
            Node.Attributes.Append(AttrActive);
            Node.Attributes.Append(AttrInactive);
            return true;
        }

        public override void CopyParametersFrom(SpecificControlProp SrcSpecificProp, bool bFromOtherInstance, BTDoc document)
        {
            if (File.Exists(PathTranslator.LinuxVsWindowsPathUse(
                            document.PathTr.RelativePathToAbsolute(
                            ((TwoBitmapProp)SrcSpecificProp).NomFichierActif))))
            {
                NomFichierActif = ((TwoBitmapProp)SrcSpecificProp).NomFichierActif;
            }
            if (File.Exists(PathTranslator.LinuxVsWindowsPathUse(
                            document.PathTr.RelativePathToAbsolute(
                            ((TwoBitmapProp)SrcSpecificProp).NomFichierInactif))))
            {
                NomFichierInactif = ((TwoBitmapProp)SrcSpecificProp).NomFichierInactif;
            }
        }
    }
}

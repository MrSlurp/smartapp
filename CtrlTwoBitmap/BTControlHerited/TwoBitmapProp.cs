using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using CommonLib;


namespace CtrlTwoBitmap
{
    class TwoBitmapProp : SpecificControlProp
    {
        private string m_NomFichierInactif;
        private string m_NomFichierActif;

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

        public override bool ReadIn(System.Xml.XmlNode Node)
        {
            XmlNode AttrActive = Node.Attributes.GetNamedItem("AciveBitmap");
            XmlNode AttrInactive = Node.Attributes.GetNamedItem("InactiveBitmap");
            if (AttrActive == null
                || AttrInactive == null)
                return false;
            NomFichierActif = AttrActive.Value;
            NomFichierInactif = AttrInactive.Value;
            return true;
        }

        public override bool WriteOut(XmlDocument XmlDoc, XmlNode Node)
        {
            XmlAttribute AttrActive = XmlDoc.CreateAttribute("AciveBitmap");
            XmlAttribute AttrInactive = XmlDoc.CreateAttribute("InactiveBitmap");
            AttrActive.Value = NomFichierActif;
            AttrInactive.Value = m_NomFichierInactif;
            Node.Attributes.Append(AttrActive);
            Node.Attributes.Append(AttrInactive);
            return true;
        }
    }
}

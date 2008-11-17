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
            XmlNode AttrActive = Node.Attributes.GetNamedItem(NOM_ATTIB_ACTIVE);
            XmlNode AttrInactive = Node.Attributes.GetNamedItem(NOM_ATTIB_INACTIVE);
            if (AttrActive == null
                || AttrInactive == null)
                return false;
            NomFichierActif = AttrActive.Value;
            NomFichierInactif = AttrInactive.Value;
            return true;
        }

        public override bool WriteOut(XmlDocument XmlDoc, XmlNode Node)
        {
            XmlAttribute AttrActive = XmlDoc.CreateAttribute(NOM_ATTIB_ACTIVE);
            XmlAttribute AttrInactive = XmlDoc.CreateAttribute(NOM_ATTIB_INACTIVE);
            AttrActive.Value = NomFichierActif;
            AttrInactive.Value = m_NomFichierInactif;
            Node.Attributes.Append(AttrActive);
            Node.Attributes.Append(AttrInactive);
            return true;
        }
    }
}

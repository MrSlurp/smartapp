using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using CommonLib;


namespace FourBitmap
{
    class DllFourBitmapProp : SpecificControlProp
    {
        private string m_NomFichier0;
        private string m_NomFichier1;
        private string m_NomFichier2;
        private string m_NomFichier3;
        private const string NOM_ATTIB_0 = "Bitmap0";
        private const string NOM_ATTIB_1 = "Bitmap1";
        private const string NOM_ATTIB_2 = "Bitmap2";
        private const string NOM_ATTIB_3 = "Bitmap3";

        public string NomFichier0
        {
            get
            {
                return m_NomFichier0;
            }
            set
            {
                m_NomFichier0 = value;
            }
        }

        public string NomFichier1
        {
            get
            {
                return m_NomFichier1;
            }
            set
            {
                m_NomFichier1 = value;
            }
        }

        public string NomFichier2
        {
            get
            {
                return m_NomFichier2;
            }
            set
            {
                m_NomFichier2 = value;
            }
        }

        public string NomFichier3
        {
            get
            {
                return m_NomFichier3;
            }
            set
            {
                m_NomFichier3 = value;
            }
        }

        public override bool ReadIn(System.Xml.XmlNode Node)
        {
            XmlNode Attr0 = Node.Attributes.GetNamedItem(NOM_ATTIB_0);
            XmlNode Attr1 = Node.Attributes.GetNamedItem(NOM_ATTIB_1);
            XmlNode Attr2 = Node.Attributes.GetNamedItem(NOM_ATTIB_2);
            XmlNode Attr3 = Node.Attributes.GetNamedItem(NOM_ATTIB_3);
            if (Attr0 == null
                || Attr1 == null
                || Attr2 == null
                || Attr3 == null
                )
                return false;
            NomFichier0 = Attr0.Value;
            NomFichier1 = Attr1.Value;
            NomFichier2 = Attr2.Value;
            NomFichier3 = Attr3.Value;
            return true;
        }

        public override bool WriteOut(XmlDocument XmlDoc, XmlNode Node)
        {
            XmlAttribute Attr0 = XmlDoc.CreateAttribute(NOM_ATTIB_0);
            XmlAttribute Attr1 = XmlDoc.CreateAttribute(NOM_ATTIB_1);
            XmlAttribute Attr2 = XmlDoc.CreateAttribute(NOM_ATTIB_2);
            XmlAttribute Attr3 = XmlDoc.CreateAttribute(NOM_ATTIB_3);
            Attr0.Value = PathTranslator.AbsolutePathToRelative(NomFichier0);
            Attr1.Value = PathTranslator.AbsolutePathToRelative(NomFichier1);
            Attr2.Value = PathTranslator.AbsolutePathToRelative(NomFichier2);
            Attr3.Value = PathTranslator.AbsolutePathToRelative(NomFichier3);
            Node.Attributes.Append(Attr0);
            Node.Attributes.Append(Attr1);
            Node.Attributes.Append(Attr2);
            Node.Attributes.Append(Attr3);
            return true;
        }

        public override void CopyParametersFrom(SpecificControlProp SrcSpecificProp)
        {
            NomFichier0 = ((DllFourBitmapProp)SrcSpecificProp).NomFichier0;
            NomFichier1 = ((DllFourBitmapProp)SrcSpecificProp).NomFichier1;
            NomFichier2 = ((DllFourBitmapProp)SrcSpecificProp).NomFichier2;
            NomFichier3 = ((DllFourBitmapProp)SrcSpecificProp).NomFichier3;
        }

    }
}

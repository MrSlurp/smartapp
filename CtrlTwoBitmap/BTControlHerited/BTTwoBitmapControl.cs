using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using CommonLib;

namespace CtrlTwoBitmap
{
    internal class BTTwoBitmapControl : BTControl
    {
        protected SpecificControlProp m_SpecificProp = null;

        public BTTwoBitmapControl(BTDoc document)
            : base(document)
        {
            m_IControl = new InteractiveTwoBitmap();
            if (m_IControl != null)
                m_IControl.SourceBTControl = this;

            m_SpecificProp = new TwoBitmapProp(this.ItemScripts);
        }

        public BTTwoBitmapControl(BTDoc document, InteractiveControl Ctrl)
            : base(document, Ctrl)
        {
            m_IControl = Ctrl;
            if (m_IControl != null)
                m_IControl.SourceBTControl = this;

            m_SpecificProp = new TwoBitmapProp(this.ItemScripts);
        }

        public override SpecificControlProp SpecificProp
        {
            get
            {
                return m_SpecificProp;
            }
        }

        #region ReadIn / WriteOut
        //*****************************************************************************************************
        // Description: Lit les données de l'objet a partir de son noeud XML
        // Return: /
        //*****************************************************************************************************
        public override bool ReadIn(XmlNode Node, BTDoc document)
        {
            if (!ReadInBaseObject(Node))
                return false;
            if (!ReadInCommonBTControl(Node))
                return false;
 
            m_SpecificProp.ReadIn(Node, document);
            // on lit le script si il y en a un
            ReadScript(Node);

            return true;
        }

        //*****************************************************************************************************
        // Description: ecrit les données de l'objet a partir de son noeud XML
        // Return: /
        //*****************************************************************************************************
        public override bool WriteOut(XmlDocument XmlDoc, XmlNode Node, BTDoc document)
        {
            XmlNode NodeControl = XmlDoc.CreateElement(XML_CF_TAG.DllControl.ToString());
            Node.AppendChild(NodeControl);
            WriteOutBaseObject(XmlDoc, NodeControl);
            XmlAttribute AttrDllId = XmlDoc.CreateAttribute(XML_CF_ATTRIB.DllID.ToString());
            AttrDllId.Value = CtrlTwoBitmap.DllEntryClass.TwoBitmap_Control_ID.ToString();
            NodeControl.Attributes.Append(AttrDllId);
            // on écrit les différents attributs du control
            if (!WriteOutCommonBTControl(XmlDoc, NodeControl))
                return false;

            if (!m_SpecificProp.WriteOut(XmlDoc, NodeControl, document))
                return false;

            /// on écrit le script
            WriteScript(XmlDoc, NodeControl);
            return true;
        }
        #endregion

        public override bool IsDllControl
        {
            get
            {
                return true;
            }
        }

        public override uint DllControlID
        {
            get
            {
                return DllEntryClass.TwoBitmap_Control_ID;
            }
        }

    }
}

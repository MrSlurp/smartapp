using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Drawing;

namespace CommonLib
{
    public class BTFilledEllipseControl : BTControl
    {
        #region données membres
        protected SpecificControlProp m_SpecificProp = null;
        #endregion

        #region constructeurs
        //*****************************************************************************************************
        // Description: 
        // Return: /
        //*****************************************************************************************************
        public BTFilledEllipseControl()
        {
            m_IControl = new TwoColorFilledEllipse();
            if (m_IControl != null)
                m_IControl.SourceBTControl = this;

            m_SpecificProp = new TwoColorProp();
        }

        //*****************************************************************************************************
        // Description: 
        // Return: /
        //*****************************************************************************************************
        public BTFilledEllipseControl(InteractiveControl Ctrl)
        {
            m_IControl = Ctrl;
            if (m_IControl != null)
                m_IControl.SourceBTControl = this;

            m_SpecificProp = new TwoColorProp();
        }
        #endregion

        #region attributs
        //*****************************************************************************************************
        // Description: 
        // Return: /
        //*****************************************************************************************************
        public override SpecificControlProp SpecificProp
        {
            get
            {
                return m_SpecificProp;
            }
        }
        #endregion

        #region ReadIn / WriteOut
        //*****************************************************************************************************
        // Description: Lit les données de l'objet a partir de son noeud XML
        // Return: /
        //*****************************************************************************************************
        public override bool ReadIn(XmlNode Node, TYPE_APP TypeApp)
        {
            if (!ReadInBaseObject(Node))
                return false;
            if (!ReadInCommonBTControl(Node))
                return false;
 
            m_SpecificProp.ReadIn(Node);
            // on lit le script si il y en a un
            ReadScript(Node);

            return true;
        }

        //*****************************************************************************************************
        // Description: ecrit les données de l'objet a partir de son noeud XML
        // Return: /
        //*****************************************************************************************************
        public override bool WriteOut(XmlDocument XmlDoc, XmlNode Node)
        {
            XmlNode NodeControl = XmlDoc.CreateElement(XML_CF_TAG.SpecificControl.ToString());
            Node.AppendChild(NodeControl);
            WriteOutBaseObject(XmlDoc, NodeControl);
            XmlAttribute AttrSpec = XmlDoc.CreateAttribute(XML_CF_ATTRIB.SpecificType.ToString());
            AttrSpec.Value = SPECIFIC_TYPE.FILLED_ELLIPSE.ToString();
            NodeControl.Attributes.Append(AttrSpec);
            // on écrit les différents attributs du control
            if (!WriteOutCommonBTControl(XmlDoc, NodeControl))
                return false;

            if (!m_SpecificProp.WriteOut(XmlDoc, NodeControl))
                return false;

            /// on écrit le script
            WriteScript(XmlDoc, NodeControl);
            return true;
        }
        #endregion

    }
}

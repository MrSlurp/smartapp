using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using CommonLib;

namespace CtrlDataTrigger
{
    internal class BTDllCtrlDataTriggerControl : BTControl
    {
        protected SpecificControlProp m_SpecificProp = null;

        public BTDllCtrlDataTriggerControl()
        {
            m_IControl = new InteractiveCtrlDataTriggerDllControl();
            if (m_IControl != null)
                m_IControl.SourceBTControl = this;

            m_SpecificProp = new DllCtrlDataTriggerProp();
        }

        public BTDllCtrlDataTriggerControl(InteractiveControl Ctrl)
        {
            m_IControl = Ctrl;
            if (m_IControl != null)
                m_IControl.SourceBTControl = this;

            m_SpecificProp = new DllCtrlDataTriggerProp();
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
            XmlNode NodeControl = XmlDoc.CreateElement(XML_CF_TAG.DllControl.ToString());
            Node.AppendChild(NodeControl);
            WriteOutBaseObject(XmlDoc, NodeControl);
            XmlAttribute AttrDllId = XmlDoc.CreateAttribute(XML_CF_ATTRIB.DllID.ToString());
            AttrDllId.Value = CtrlDataTrigger.DllEntryClass.DLL_Control_ID.ToString();
            NodeControl.Attributes.Append(AttrDllId);
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
                return CtrlDataTrigger.DllEntryClass.DLL_Control_ID;
            }
        }

        // le traite message dans smart config
        // n'a lieu d'être que si dans le paramétrage on trouve des symboles d'objets
        public override void TraiteMessage(MESSAGE Mess, object obj, TYPE_APP TypeApp)
        {
            base.TraiteMessage(Mess, obj, TypeApp);
            m_SpecificProp.TraiteMessage(Mess, obj, TypeApp, this);
        }

    }
}

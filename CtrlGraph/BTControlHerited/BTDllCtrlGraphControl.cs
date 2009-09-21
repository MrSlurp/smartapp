using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using CommonLib;

namespace CtrlGraph
{
    internal class BTDllCtrlGraphControl : BTControl
    {
        protected SpecificControlProp m_SpecificProp = null;

        public BTDllCtrlGraphControl()
        {
            m_IControl = new InteractiveCtrlGraphDllControl();
            if (m_IControl != null)
                m_IControl.SourceBTControl = this;

            m_SpecificProp = new DllCtrlGraphProp();
        }

        public BTDllCtrlGraphControl(InteractiveControl Ctrl)
        {
            m_IControl = Ctrl;
            if (m_IControl != null)
                m_IControl.SourceBTControl = this;

            m_SpecificProp = new DllCtrlGraphProp();
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
            AttrDllId.Value = CtrlGraph.DllEntryClass.DLL_Control_ID.ToString();
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
                return CtrlGraph.DllEntryClass.DLL_Control_ID;
            }
        }

        public override void TraiteMessage(MESSAGE Mess, object obj, TYPE_APP TypeApp)
        {
            base.TraiteMessage(Mess, obj, TypeApp);
            if (TypeApp == TYPE_APP.SMART_CONFIG)
            {
                DllCtrlGraphProp Props = (DllCtrlGraphProp)m_SpecificProp;
                switch (Mess)
                {
                    case MESSAGE.MESS_ASK_ITEM_DELETE:
                        if (((MessAskDelete)obj).TypeOfItem == typeof(Data))
                        {
                            MessAskDelete MessParam = (MessAskDelete)obj;
                            for (int i = 0; i < DllCtrlGraphProp.NB_CURVE; i++)
                            {
                                if (Props.GetSymbol(i) == MessParam.WantDeletetItemSymbol)
                                {
                                    string strMess = string.Format("Graphic {0} will lost data", Symbol);
                                    MessParam.ListStrReturns.Add(strMess);
                                }
                            }
                        }
                        break;
                    case MESSAGE.MESS_ITEM_DELETED:
                        if (((MessDeleted)obj).TypeOfItem == typeof(Data))
                        {
                            MessDeleted MessParam = (MessDeleted)obj;
                            for (int i = 0; i < DllCtrlGraphProp.NB_CURVE; i++)
                            {
                                if (Props.GetSymbol(i) == MessParam.DeletetedItemSymbol)
                                {
                                    Props.SetSymbol(i, "");
                                }
                            }
                        }
                        break;
                    case MESSAGE.MESS_ITEM_RENAMED:
                        if (((MessItemRenamed)obj).TypeOfItem == typeof(Data))
                        {
                            MessItemRenamed MessParam = (MessItemRenamed)obj;
                            for (int i = 0; i < DllCtrlGraphProp.NB_CURVE; i++)
                            {
                                if (Props.GetSymbol(i) == MessParam.OldItemSymbol)
                                {
                                    Props.SetSymbol(i,MessParam.NewItemSymbol);
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}

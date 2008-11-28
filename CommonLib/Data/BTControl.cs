/***************************************************************************/
// PROJET : 
// 
/***************************************************************************/
// Fichier : 
/***************************************************************************/
// description :
// 
/***************************************************************************/
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Xml;

namespace CommonLib
{
    public partial class BTControl : BaseObject, IScriptable
    {
        #region Déclaration des données de la classe
        // control affiché dans le designer d'ecrans
        protected InteractiveControl m_IControl = new InteractiveControl();
        // symbol de la donnée associée au control
        protected string m_strAssociateData;
        // indique si le control utilise l'évènement d'écran
        protected bool m_bUseScreenEvent = false;
        // indique si le control est read only
        protected bool m_bIsReadOnly = false;
        // collection de string qui contiennent le script a executer
        protected StringCollection m_ScriptLines = new StringCollection();

        #endregion

        #region constructeurs
        //*****************************************************************************************************
        // Description: constructeur par défaut
        // Return: /
        //*****************************************************************************************************
        public BTControl()
        {
            m_IControl = new InteractiveControl();
            m_IControl.SourceBTControl = this;
        }

        //*****************************************************************************************************
        // Description: constructeur prenant en paramètre un IntercativeControl
        // ce constructeur permet d'associé l'objet graphique lors qu'il est droppé dans le designer
        // Return: /
        //*****************************************************************************************************
        public BTControl(InteractiveControl Ctrl)
        {
            m_IControl = Ctrl;
            if (m_IControl != null)
                m_IControl.SourceBTControl = this;
        }

        public static BTControl CreateNewBTControl(InteractiveControl Ctrl)
        {
            BTControl newControl = null;
            switch (Ctrl.ControlType)
            {
                case InteractiveControlType.SpecificControl:
                    if (Ctrl.GetType() == typeof(TwoColorFilledRect))
                    {
                        newControl = new BTFilledRectControl(Ctrl);
                    }
                    else if (Ctrl.GetType() == typeof(TwoColorFilledEllipse))
                    {
                        newControl = new BTFilledEllipseControl(Ctrl);
                    }
                    else
                    {
                        System.Diagnostics.Debug.Assert(false);
                    }
                    break;
                default:
                    newControl = new BTControl(Ctrl);
                    break;
            }
            return newControl;
        }
        #endregion

        #region attributs

        //*****************************************************************************************************
        // Description: en lecture seul, renvoie la référence vers l'objet graphique utilisé dans le designer
        // Return: /
        //*****************************************************************************************************
        public InteractiveControl IControl
        {
            get
            {
                return m_IControl;
            }
        }

        //*****************************************************************************************************
        // Description: accesseur de la propriété IsReadOnly
        // Return: /
        //*****************************************************************************************************
        public bool IsReadOnly
        {
            get
            {
                return m_bIsReadOnly;
            }
            set
            {
                m_bIsReadOnly = value;
            }
        }

        //*****************************************************************************************************
        // Description: accesseur de la propriété UseScreenEvent
        // Return: /
        //*****************************************************************************************************
        public bool UseScreenEvent
        {
            get
            {
                return m_bUseScreenEvent;
            }
            set
            {
                m_bUseScreenEvent = value;
            }
        }

        //*****************************************************************************************************
        // Description: accesseur de la propriété UseScreenEvent
        // Return: /
        //*****************************************************************************************************
        public virtual bool IsDllControl
        {
            get
            {
                return false;
            }
        }

        public virtual uint DllControlID
        {
            get
            {
                return 0;
            }
        }

        //*****************************************************************************************************
        // Description: accesseur vers le symbol de la données associée
        // Return: /
        //*****************************************************************************************************
        public string AssociateData
        {
            get
            {
                return m_strAssociateData;
            }
            set
            {
                m_strAssociateData = value;
            }
        }

        
        //*****************************************************************************************************
        // Description: 
        // Return: /
        //*****************************************************************************************************
        public virtual SpecificControlProp SpecificProp
        {
            get
            {
                //surchargé dans les objet hérité pour retourner les propriété spécifiques
                return null;
            }
        }
        
        //*****************************************************************************************************
        // Description: accesseur du script
        // renvoie un string[] pour pouvoir être utilisé directement avec le RichEditBox
        // Return: /
        //*****************************************************************************************************
        public string[] ScriptLines
        {
            get
            {
                string[] TabLines = new string[m_ScriptLines.Count];
                for (int i = 0; i < m_ScriptLines.Count; i++)
                {
                    TabLines[i] = m_ScriptLines[i];
                }
                return TabLines;
            }
            set
            {
                m_ScriptLines.Clear();
                for (int i = 0; i < value.Length; i++)
                {
                    m_ScriptLines.Add(value[i]);
                }

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
            if (!base.ReadIn(Node, TypeApp))
                return false;
            // attribut Type (type de control) cette valeur n'est pas directement stocké dans l'objet
            // mais dans l'objet graphique associé
            XmlNode AttrType = Node.Attributes.GetNamedItem(XML_CF_ATTRIB.Type.ToString());
            // on test la présence de tout les attributs obligatoires
            if ( AttrType == null)
                return false;

            CONTROL_TYPE TypeId = CONTROL_TYPE.NULL;
            // on parse le type de control
            try
            {
                TypeId = (CONTROL_TYPE)Enum.Parse(typeof(CONTROL_TYPE), AttrType.Value, true);
            }
            catch (Exception)
            {
                // en cas de tag non reconne dans l'enum, une exeption est levée, 
                // on la récupère car ca peut arriver
            }
         
            // on assigne le type du InteractiveControl
            switch (TypeId)
            {
                case CONTROL_TYPE.BUTTON:
                    m_IControl.ControlType = InteractiveControlType.Button;
                    break;
                case CONTROL_TYPE.CHECK:
                    m_IControl.ControlType = InteractiveControlType.CheckBox;
                    break;
                case CONTROL_TYPE.COMBO:
                    m_IControl.ControlType = InteractiveControlType.Combo;
                    break;
                case CONTROL_TYPE.SLIDER:
                    m_IControl.ControlType = InteractiveControlType.Slider;
                    break;
                case CONTROL_TYPE.STATIC:
                    m_IControl.ControlType = InteractiveControlType.Text;
                    break;
                case CONTROL_TYPE.UP_DOWN:
                    m_IControl.ControlType = InteractiveControlType.NumericUpDown;
                    break;
                case CONTROL_TYPE.SPECIFIC:
                    System.Diagnostics.Debug.Assert(false);
                    break;
                case CONTROL_TYPE.NULL:
                default:
                    Console.WriteLine("Type de control indéfini");
                    return false;
            }
            ReadInCommonBTControl(Node);
            ReadScript(Node);
            return true;
        }

        protected bool ReadInCommonBTControl(XmlNode Node)
        {
            // attribut AssociateData
            XmlNode AttrAssocData = Node.Attributes.GetNamedItem(XML_CF_ATTRIB.AssociateData.ToString());
            // attribut text. Meme chose que pour le type
            XmlNode AttrText = Node.Attributes.GetNamedItem(XML_CF_ATTRIB.Text.ToString());
            // attribut screen event
            XmlNode AttrScreenEvent = Node.Attributes.GetNamedItem(XML_CF_ATTRIB.ScreenEvent.ToString());
            // attribut taille Meme chose que pour le type
            XmlNode AttrSize = Node.Attributes.GetNamedItem(XML_CF_ATTRIB.size.ToString());
            // attribut position Meme chose que pour le type
            XmlNode AttrPos = Node.Attributes.GetNamedItem(XML_CF_ATTRIB.Pos.ToString());
            // attribut readOnly
            XmlNode AttrReadOnly = Node.Attributes.GetNamedItem(XML_CF_ATTRIB.ReadOnly.ToString());
            // on test la présence de tout les attributs obligatoires
            if (AttrAssocData == null
                || AttrScreenEvent == null
                || AttrSize == null
                || AttrPos == null
                || AttrText == null)
                return false;

            // on assigne toutes les valeurs
            m_strAssociateData = AttrAssocData.Value;
            m_bUseScreenEvent = bool.Parse(AttrScreenEvent.Value);
            string[] TabStrPos = AttrPos.Value.Split(',');
            string[] TabStrSize = AttrSize.Value.Split(',');
            m_IControl.Text = AttrText.Value;
            // read only est facultatif et est traité indépendemeent
            if (AttrReadOnly != null)
                this.IsReadOnly = bool.Parse(AttrReadOnly.Value);

            // on assigne la taille après avoir changé le type
            // sinon le changement de type provoque une modification de la taille (en fonction des dimension minimales)

            m_IControl.Location = new System.Drawing.Point(int.Parse(TabStrPos[0]), int.Parse(TabStrPos[1]));
            m_IControl.Size = new System.Drawing.Size(int.Parse(TabStrSize[0]), int.Parse(TabStrSize[1]));
            SetControlRect();
            return true;
        }
        //*****************************************************************************************************
        // Description: ecrit les données de l'objet a partir de son noeud XML
        // Return: /
        //*****************************************************************************************************
        public override bool WriteOut(XmlDocument XmlDoc, XmlNode Node)
        {
            XmlNode NodeControl = XmlDoc.CreateElement(XML_CF_TAG.Control.ToString());
            Node.AppendChild(NodeControl);
            base.WriteOut(XmlDoc, NodeControl);
            // on écrit les différents attributs du control
            XmlAttribute AttrType = XmlDoc.CreateAttribute(XML_CF_ATTRIB.Type.ToString());
            switch (m_IControl.ControlType)
            {
                case InteractiveControlType.Button:
                    AttrType.Value = CONTROL_TYPE.BUTTON.ToString();
                    break;
                case InteractiveControlType.CheckBox:
                    AttrType.Value = CONTROL_TYPE.CHECK.ToString();
                    break;
                case InteractiveControlType.Combo:
                    AttrType.Value = CONTROL_TYPE.COMBO.ToString();
                    break;
                case InteractiveControlType.Slider:
                    AttrType.Value = CONTROL_TYPE.SLIDER.ToString();
                    break;
                case InteractiveControlType.Text:
                    AttrType.Value = CONTROL_TYPE.STATIC.ToString();
                    break;
                case InteractiveControlType.NumericUpDown:
                    AttrType.Value = CONTROL_TYPE.UP_DOWN.ToString();
                    break;
                case InteractiveControlType.SpecificControl:
                case InteractiveControlType.DllControl:
                    System.Diagnostics.Debug.Assert(false);
                    break;
                default:
                    Console.WriteLine("Type de control indéfini");
                    break;
            }
            NodeControl.Attributes.Append(AttrType);
            if (!WriteOutCommonBTControl(XmlDoc, NodeControl))
                return false;
            // on écrit le script
            WriteScript(XmlDoc, NodeControl);
            return true;
        }

        protected bool WriteOutCommonBTControl(XmlDocument XmlDoc, XmlNode Node)
        {
            XmlAttribute AttrAssocData = XmlDoc.CreateAttribute(XML_CF_ATTRIB.AssociateData.ToString());
            XmlAttribute AttrText = XmlDoc.CreateAttribute(XML_CF_ATTRIB.Text.ToString());
            XmlAttribute AttrScreenEvent = XmlDoc.CreateAttribute(XML_CF_ATTRIB.ScreenEvent.ToString());
            XmlAttribute AttrSize = XmlDoc.CreateAttribute(XML_CF_ATTRIB.size.ToString());
            XmlAttribute AttrPos = XmlDoc.CreateAttribute(XML_CF_ATTRIB.Pos.ToString());
            XmlAttribute AttrReadOnly = XmlDoc.CreateAttribute(XML_CF_ATTRIB.ReadOnly.ToString());
            AttrAssocData.Value = m_strAssociateData;
            AttrScreenEvent.Value = m_bUseScreenEvent.ToString();
            AttrPos.Value = string.Format("{0},{1}", m_IControl.Location.X, m_IControl.Location.Y);
            AttrSize.Value = string.Format("{0},{1}", m_IControl.Size.Width, m_IControl.Size.Height);
            AttrText.Value = m_IControl.Text;
            AttrReadOnly.Value = IsReadOnly.ToString();
            Node.Attributes.Append(AttrAssocData);
            Node.Attributes.Append(AttrScreenEvent);
            Node.Attributes.Append(AttrPos);
            Node.Attributes.Append(AttrSize);
            Node.Attributes.Append(AttrText);
            Node.Attributes.Append(AttrReadOnly);

            return true;
        }

        protected void ReadScript(XmlNode Node)
        {
            // on lit le script si il y en a un
            if (Node.FirstChild != null && Node.FirstChild.Name == XML_CF_TAG.EventScript.ToString())
            {
                for (int i = 0; i < Node.FirstChild.ChildNodes.Count; i++)
                {
                    if (Node.FirstChild.ChildNodes[i].Name == XML_CF_TAG.Line.ToString()
                        && Node.FirstChild.ChildNodes[i].FirstChild != null)
                    {

                        m_ScriptLines.Add(Node.FirstChild.ChildNodes[i].FirstChild.Value);
                    }
                }
            }
        }

        protected void WriteScript(XmlDocument XmlDoc, XmlNode NodeControl)
        {
            XmlNode XmlEventScript = XmlDoc.CreateElement(XML_CF_TAG.EventScript.ToString());
            for (int i = 0; i < m_ScriptLines.Count; i++)
            {
                XmlNode NodeLine = XmlDoc.CreateElement(XML_CF_TAG.Line.ToString());
                XmlNode NodeText = XmlDoc.CreateTextNode(m_ScriptLines[i]);
                NodeLine.AppendChild(NodeText);
                XmlEventScript.AppendChild(NodeLine);
            }
            NodeControl.AppendChild(XmlEventScript);
        }


        //*****************************************************************************************************
        // Description: on ne fait rien dans cette classe car elle n'est pas utilisée en mode command
        // ehh oui c'est une feinte (voir BTScreen)
        // Return: /
        //*****************************************************************************************************
        public override bool FinalizeRead(BTDoc Doc)
        {
            if (!string.IsNullOrEmpty(m_strAssociateData))
            {
                m_AssociateData = (Data)Doc.GestData.GetFromSymbol(m_strAssociateData);
                if (m_AssociateData == null)
                {
                    // TODO : loguer les erreurs
                    // pas d'assert ici, car par exemple un bouton ou un static peuvent ne pas avoir de donnée
                    //Console.WriteLine("Donnée Associée non trouvée");
                    return true;
                }
                else
                {
                    m_AssociateData.DataValueChanged += new EventDataValueChange(UpdateFromData);
                }
            }
            Executer = Doc.Executer; 
            return true;
        }

        #endregion

        #region Gestion des AppMessages
        //*****************************************************************************************************
        // Description: effectue les opération nécessaire lors de la récéption d'un message
        // Return: /
        //*****************************************************************************************************
        public override void TraiteMessage(MESSAGE Mess, object obj, TYPE_APP TypeApp)
        {
            // dans le cas du control, il faut tester si l'objet supprimée/renomé ne serai
            // pas (par le plus grand des hasard) la donnée associée
            switch (Mess)
            {
                // message de requête sur les conséquence d'une supression
                case MESSAGE.MESS_ASK_ITEM_DELETE:
                    if (((MessAskDelete)obj).TypeOfItem == typeof(Data))
                    {
                        if (((MessAskDelete)obj).WantDeletetItemSymbol == m_strAssociateData)
                        {
                            string strMess = string.Format("Control {0} associate data removed", Symbol);
                            ((MessAskDelete)obj).ListStrReturns.Add(strMess);
                        }
                    }
                    break;
                // message de supression
                case MESSAGE.MESS_ITEM_DELETED:
                    if (((MessDeleted)obj).TypeOfItem == typeof(Data))
                    {
                        if (((MessDeleted)obj).DeletetedItemSymbol == m_strAssociateData)
                        {
                            m_strAssociateData = "";
                        }

                    }
                    break;
                // message de renomage
                case MESSAGE.MESS_ITEM_RENAMED:
                    if (((MessItemRenamed)obj).TypeOfItem == typeof(Data))
                    {
                        if (((MessItemRenamed)obj).OldItemSymbol == m_strAssociateData)
                        {
                            m_strAssociateData = ((MessItemRenamed)obj).NewItemSymbol;
                        }
                    }
                    break;
                case MESSAGE.MESS_UPDATE_FROM_DATA:
                    UpdateFromData();
                    break;
                default:
                    break;
            }
            // mais l'objet peux aussi être utilisé dans le script
            ScriptTraiteMessage(this, Mess, m_ScriptLines, obj);
        }


        #endregion

        public void CopyParametersFrom(BTControl SrcBtControl)
        {
            m_strAssociateData = SrcBtControl.m_strAssociateData;
            m_bUseScreenEvent = SrcBtControl.m_bUseScreenEvent;
            if (SpecificProp != null && SrcBtControl.SpecificProp != null)
            {
                SpecificProp.CopyParametersFrom(SrcBtControl.SpecificProp);
            }
        }
    }
}

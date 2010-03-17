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
    /// <summary>
    /// classe BTControl, elle représent les objets graphiques disposés dans la surface de dessin
    /// de SmartConfig
    /// </summary>
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
        /// <summary>
        /// constructeur par défaut
        /// </summary>
        public BTControl()
        {
            m_IControl = new InteractiveControl();
            m_IControl.SourceBTControl = this;
        }

        /// <summary>
        /// constructeur prenant en paramètre un IntercativeControl
        /// ce constructeur permet d'associé l'objet graphique lors qu'il est droppé dans le designer
        /// </summary>
        /// <param name="Ctrl">objet interactif posé dans la surface de dessin</param>
        protected BTControl(InteractiveControl Ctrl)
        {
            m_IControl = Ctrl;
            if (m_IControl != null)
                m_IControl.SourceBTControl = this;
        }

        /// <summary>
        /// crée un objet BT control du type adapté à l'objet interactive control passé en paramètre
        /// (controle de base ou controle spécifique)
        /// </summary>
        /// <param name="Ctrl">objet interactif posé dans la surface de dessin</param>
        /// <returns>l'objet BT control crée</returns>
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

        /// <summary>
        /// en lecture seul, renvoie la référence vers l'objet graphique utilisé dans le designer
        /// </summary>
        public InteractiveControl IControl
        {
            get
            {
                return m_IControl;
            }
        }

        /// <summary>
        /// obtient ou assigne la propriété readonly
        /// </summary>
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

        /// <summary>
        /// obtient ou assigne la propriété UseScreenEvent
        /// </summary>
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

        /// <summary>
        /// indique si le controle est de type DLL
        /// </summary>
        public virtual bool IsDllControl
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// renvoie l'identifiant DLL du control
        /// </summary>
        public virtual uint DllControlID
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// obtient ou assigne le symbole de la donnée associée
        /// </summary>
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

        
        /// <summary>
        /// renvoie les propriété spécifiques du controle si celui ci en a
        /// </summary>
        public virtual SpecificControlProp SpecificProp
        {
            get
            {
                //surchargé dans les objet hérité pour retourner les propriété spécifiques
                return null;
            }
        }
    
        /// <summary>
        /// renvoie true si le control à au moins une donnée associé qui rend son état dynamique
        /// Utilisé pour masquer les controls lors de l'enregistrement de l'image de fond de plan
        /// </summary>
        public virtual bool HaveDataAssociation
        {
            get
            {
                bool bRet = false;
                bRet |= !string.IsNullOrEmpty(m_strAssociateData);
                if (SpecificProp != null)
                    bRet |= SpecificProp.HaveDataAssociation;
                     
                return bRet;
            }
        } 
        
        /// <summary>
        /// obtient ou assigne le script du controle
        /// </summary>
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
        /// <summary>
        /// Lit les données de l'objet a partir de son noeud XML
        /// </summary>
        /// <param name="Node">Noeud Xml de l'objet</param>
        /// <param name="TypeApp">type d'application courante</param>
        /// <returns>true si la lecture s'est bien passé</returns>
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
                    Traces.LogAddDebug(TraceCat.Serialization, "Erreur récupération du type de controle");
                    return false;
            }
            ReadInCommonBTControl(Node);
            ReadScript(Node);
            return true;
        }

        /// <summary>
        /// lit les données communes à tout les BT Control
        /// </summary>
        /// <param name="Node"></param>
        /// <returns></returns>
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

        /// <summary>
        /// écrit les données de l'objet dans le fichier XML
        /// </summary>
        /// <param name="XmlDoc">Document XML courant</param>
        /// <param name="Node">Noeud parent du controle dans le document</param>
        /// <returns>true si l'écriture s'est déroulée avec succès</returns>
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

        /// <summary>
        /// écrit les données communes à tout les BT control
        /// </summary>
        /// <param name="XmlDoc">Document Xml</param>
        /// <param name="Node">noeud parent de l'objet courant</param>
        /// <returns>true si l'écriture s'est déroulée correctement</returns>
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

        /// <summary>
        /// écrit le script de l'objet
        /// </summary>
        /// <param name="Node">noeud de l'objet courant</param>
        protected void ReadScript(XmlNode Node)
        {
            // on lit le script si il y en a un
            if (Node.FirstChild != null)
            {
                for (int ch = 0; ch < Node.ChildNodes.Count; ch++)
                {
                    if (Node.ChildNodes[ch].Name == XML_CF_TAG.EventScript.ToString())
                    {
                        for (int i = 0; i < Node.ChildNodes[ch].ChildNodes.Count; i++)
                        {
                            if (Node.ChildNodes[ch].ChildNodes[i].Name == XML_CF_TAG.Line.ToString()
                                && Node.ChildNodes[ch].ChildNodes[i].FirstChild != null)
                            {

                                m_ScriptLines.Add(Node.ChildNodes[ch].ChildNodes[i].FirstChild.Value);
                            }
                        }
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// écrit le script de l'objet courant
        /// </summary>
        /// <param name="XmlDoc">document XML</param>
        /// <param name="NodeControl">noeud du controle courant</param>
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


        /// <summary>
        /// appelé uniquement dans SmartCommand
        /// Cette fonction est utilisé pour finaliser la lecture, et principalement pour prendre directement
        /// des références sur les objets utilisés (donnée associée etc)
        /// afin d'optimiser les performances dans Smart Command en évitant d'avoir à parcourir les gestionnaires
        /// </summary>
        /// <param name="Doc">Document courant</param>
        /// <returns>true si tout s'est bien passé</returns>
        public override bool FinalizeRead(BTDoc Doc)
        {
            if (!string.IsNullOrEmpty(m_strAssociateData))
            {
                m_AssociateData = (Data)Doc.GestData.GetFromSymbol(m_strAssociateData);
                if (m_AssociateData == null)
                {
                    // pas d'assert ici, car par exemple un bouton ou un static peuvent ne pas avoir de donnée
                    //Console.WriteLine("Donnée Associée non trouvée");
                    return true;
                }
                else
                {
                    // liaison à l'évènement de changement de valeur de la donnée
                    m_AssociateData.DataValueChanged += new EventDataValueChange(UpdateFromData);
                }
            }
            m_Executer = Doc.Executer; 
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
                            string strMess = string.Format(Lang.LangSys.C("Control {0} associate data removed"), Symbol);
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
#if QUICK_MOTOR
                case MESSAGE.MESS_PRE_PARSE:
                    if (this.ScriptLines.Length != 0)
                        this.m_iQuickScriptID = m_Executer.PreParseScript((IScriptable) this);    
                    break;
#endif
                default:
                    break;
            }
            // mais l'objet peux aussi être utilisé dans le script
            ScriptTraiteMessage(this, Mess, m_ScriptLines, obj);
        }


        #endregion

        #region Méthodes diverses
        public void CopyParametersFrom(BTControl SrcBtControl)
        {
            m_strAssociateData = SrcBtControl.m_strAssociateData;
            m_bUseScreenEvent = SrcBtControl.m_bUseScreenEvent;
            if (SpecificProp != null && SrcBtControl.SpecificProp != null)
            {
                SpecificProp.CopyParametersFrom(SrcBtControl.SpecificProp);
            }
        }
        #endregion
    }
}

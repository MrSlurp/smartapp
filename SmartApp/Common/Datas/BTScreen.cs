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
using System.Windows.Forms;
using SmartApp.Ihm;
using SmartApp.Gestionnaires;
using SmartApp.Controls;
using System.Drawing;
using SmartApp.Scripts;
using SmartApp.AppEventLog;

namespace SmartApp.Datas
{
    public class BTScreen : BaseObject, IInitScriptable, IScriptable
    {
        #region Données de la classe
        // gestionnaire des control appartenant a l'écran
        private GestControl m_GestControl;
        // titre de l'écran
        private string m_strTitle;
        // script de l'event de l'ecran
        private StringCollection m_ScriptLines = new StringCollection();
        // script d'init de l'écran
        private StringCollection m_InitScriptLines = new StringCollection();
        // fichier image de "background de l'écran"
        private string m_strBackPictureFile="";
        #endregion

        #region données sépcifiques au fonctionement en mode "Command"
        // objet dynamic pannel qui est affiché
        private DynamicPanel m_DynamicPanel = new DynamicPanel();
        // liste des "Control" de l'écran en mode commande
        private List<BaseControl> m_ListControls = new List<BaseControl>();
        // liens vers l'executer de script
        protected ScriptExecuter m_Executer = null;
        #endregion


        //*****************************************************************************************************
        // Description: accesseur sur l'executer de script
        // Return: /
        //*****************************************************************************************************
        public ScriptExecuter Executer
        {
            get
            {
                return m_Executer;
            }
            set
            {
                m_Executer = value;
            }
        }


        #region constructeur
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public BTScreen()
        {
            m_GestControl = new GestControl();
            m_GestControl.DoSendMessage += new SendMessage(this.ObjectSendMessage);
        }
        #endregion

        #region attributs
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public GestControl Controls
        {
            get
            {
                return m_GestControl;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public string Title
        {
            get
            {
                return m_strTitle;
            }
            set
            {
                m_strTitle = value;
            }
        }

        //*****************************************************************************************************
        // Description:
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
                //string[] TabLines = new string[m_ScriptLines.Count];
                m_ScriptLines.Clear();
                for (int i = 0; i < value.Length; i++)
                {
                    m_ScriptLines.Add(value[i]);
                }

            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public string[] InitScriptLines
        {
            get
            {
                string[] TabLines = new string[m_InitScriptLines.Count];
                for (int i = 0; i < m_InitScriptLines.Count; i++)
                {
                    TabLines[i] = m_InitScriptLines[i];
                }
                return TabLines;
            }
            set
            {
                m_InitScriptLines.Clear();
                for (int i = 0; i < value.Length; i++)
                {
                    m_InitScriptLines.Add(value[i]);
                }

            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public string BackPictureFile
        {
            get
            {
                return m_strBackPictureFile;
            }
            set
            {
                m_strBackPictureFile = value;
            }
        }
           
        #endregion

        //*****************************************************************************************************
        // Description: accesseur sur le Dynamic Panel a afficher
        // Return: /
        //*****************************************************************************************************
        public DynamicPanel Panel
        {
            get
            {
                return m_DynamicPanel;
            }
        }

        #region ReadIn / WriteOut
        //*****************************************************************************************************
        // Description: Lit les données de l'objet a partir de son noeud XML
        // Return: /
        //*****************************************************************************************************
        public override bool ReadIn(XmlNode Node)
        {
            if (!base.ReadIn(Node))
                return false;
            // on prend d'abord le text
            XmlNode TitleAttrib = Node.Attributes.GetNamedItem(XML_CF_ATTRIB.Text.ToString());
            if (TitleAttrib != null)
                m_strTitle = TitleAttrib.Value;

            // ensuite on parcour tout les noeud enfant et on test leur type
            for (int i = 0; i < Node.ChildNodes.Count; i++)
            {
                XmlNode ChildNode = Node.ChildNodes[i];
                XML_CF_TAG TypeId = 0;
                try
                {
                    TypeId = (XML_CF_TAG)Enum.Parse(typeof(XML_CF_TAG), ChildNode.Name, true);
                }
                catch (Exception)
                {
                    // en cas de tag non reconne dans l'enum, une exeption est levée, 
                    // on la récupère car ca peut arriver
                    continue;
                }
                switch (TypeId)
                {
                    case XML_CF_TAG.ControlList:
                        {
                            // ceci ne peux pas être fait de la même manière en mode Config ou en mode Command
                            // dans le cas ou on est en mode config, on crée des objet BTControl
                            // qui utiliseront un intercative control dans le designer
                            if (Program.TypeApp == TYPE_APP.SMART_CONFIG)
                            {

                                // on parcour la liste des control
                                for (int j = 0; j < ChildNode.ChildNodes.Count; j++)
                                {
                                    XmlNode NodeControl = ChildNode.ChildNodes[j];
                                    if (NodeControl.Name != XML_CF_TAG.Control.ToString())
                                        continue;
                                    BTControl Control = new BTControl();
                                    if (!Control.ReadIn(NodeControl))
                                        return false;

                                    m_GestControl.AddObj(Control);
                                }
                            }
                            // sinon on effectue une lecture spéciale mode Command
                            else if (Program.TypeApp == TYPE_APP.SMART_COMMAND)
                            {
                                if (!ReadControlForCommandMode(ChildNode))
                                    return false;
                            }
                            else
                                System.Diagnostics.Debug.Assert(false);

                            break;
                        }
                    //on lit le script d'event
                    case XML_CF_TAG.EventScript:
                        {
                            for (int j = 0; j < ChildNode.ChildNodes.Count; j++)
                            {
                                if (ChildNode.ChildNodes[j].Name == XML_CF_TAG.Line.ToString()
                                    && ChildNode.ChildNodes[j].FirstChild != null)
                                {
                                    m_ScriptLines.Add(ChildNode.ChildNodes[j].FirstChild.Value);
                                }
                            }
                        }
                        break;
                    // on lit le script d'init
                    case XML_CF_TAG.InitScript:
                        {
                            for (int j = 0; j < ChildNode.ChildNodes.Count; j++)
                            {
                                if (ChildNode.ChildNodes[j].Name == XML_CF_TAG.Line.ToString()
                                    && ChildNode.ChildNodes[j].FirstChild != null)
                                {
                                    m_InitScriptLines.Add(ChildNode.ChildNodes[j].FirstChild.Value);
                                }
                            }
                        }
                        break;
                    // on lit le chemin de l'image de background
                    case XML_CF_TAG.ImagePath:
                        if (ChildNode.FirstChild != null)
                            this.m_strBackPictureFile = ChildNode.FirstChild.Value;
                        break;
                    default:
                        break;
                }
            }
            return true;
        }

        //*****************************************************************************************************
        // Description: ecrit les données de l'objet a partir de son noeud XML
        // Return: /
        //*****************************************************************************************************
        private bool ReadControlForCommandMode(XmlNode Node)
        {
            // En mode Commande on doit crée des objets pouvant afficher des vrais controls du framework
            // ceci est fait grace aux objets "baseControl" et dérivés
            for (int i = 0; i < Node.ChildNodes.Count; i++)
            {
                XmlNode ChildNode = Node.ChildNodes[i];
                if (ChildNode.Name != XML_CF_TAG.Control.ToString())
                    continue;
                XmlNode AttrType = ChildNode.Attributes.GetNamedItem(XML_CF_ATTRIB.Type.ToString());
                CONTROL_TYPE TypeId = 0;
                try
                {
                    TypeId = (CONTROL_TYPE)Enum.Parse(typeof(CONTROL_TYPE), AttrType.Value, true);
                }
                catch (Exception)
                {
                    // en cas de tag non reconne dans l'enum, une exeption est levée, 
                    // on la récupère car ca peut arriver
                    continue;
                }

                BaseControl NewControl = null;
                // on crée chaque objet en fonction de son type
                switch (TypeId)
                {
                    case CONTROL_TYPE.BUTTON:
                        NewControl = new ButtonControl();
                        NewControl.ReadIn(ChildNode);
                        break;
                    case CONTROL_TYPE.CHECK:
                        NewControl = new CheckControl();
                        NewControl.ReadIn(ChildNode);
                        break;
                    case CONTROL_TYPE.COMBO:
                        NewControl = new ComboControl();
                        NewControl.ReadIn(ChildNode);
                        break;
                    case CONTROL_TYPE.SLIDER:
                        NewControl = new SliderControl();
                        NewControl.ReadIn(ChildNode);
                        break;
                    case CONTROL_TYPE.STATIC:
                        NewControl = new StaticControl();
                        NewControl.ReadIn(ChildNode);
                        break;
                    case CONTROL_TYPE.UP_DOWN:
                        NewControl = new UpDownControl();
                        NewControl.ReadIn(ChildNode);
                        break;
                    case CONTROL_TYPE.NULL:
                    default:
                        Console.WriteLine("Type de control indéfini");
                        NewControl = null;
                        break;
                }
                if (NewControl != null)
                    m_ListControls.Add(NewControl);
            }
            return true;
        }

        //*****************************************************************************************************
        // Description: ecrit les données de l'objet a partir de son noeud XML
        // Return: /
        //*****************************************************************************************************
        public override bool WriteOut(XmlDocument XmlDoc, XmlNode Node)
        {
            // on écrit les attributs de l'écran
            base.WriteOut(XmlDoc, Node);
            XmlAttribute TitleAttrib = XmlDoc.CreateAttribute(XML_CF_ATTRIB.Text.ToString());
            TitleAttrib.Value = this.Title;
            Node.Attributes.Append(TitleAttrib);
            XmlNode XmlControlList = XmlDoc.CreateElement(XML_CF_TAG.ControlList.ToString());
            XmlNode XmlInitScript = XmlDoc.CreateElement(XML_CF_TAG.InitScript.ToString());
            XmlNode XmlEventScript = XmlDoc.CreateElement(XML_CF_TAG.EventScript.ToString());
            Node.AppendChild(XmlControlList);
            Node.AppendChild(XmlInitScript);
            Node.AppendChild(XmlEventScript);
            // on demande a chque control de s'écrire
            for (int i = 0; i < this.m_GestControl.Count; i++)
            {
                BTControl dt = (BTControl)m_GestControl[i];
                dt.WriteOut(XmlDoc, XmlControlList);
            }
            // on écrit les scripts
            for (int i = 0; i < m_InitScriptLines.Count; i++)
            {
                XmlNode NodeLine = XmlDoc.CreateElement(XML_CF_TAG.Line.ToString());
                XmlNode NodeText = XmlDoc.CreateTextNode(m_InitScriptLines[i]);
                NodeLine.AppendChild(NodeText);
                XmlInitScript.AppendChild(NodeLine);
            }
            for (int i = 0; i < m_ScriptLines.Count; i++)
            {
                XmlNode NodeLine = XmlDoc.CreateElement(XML_CF_TAG.Line.ToString());
                XmlNode NodeText = XmlDoc.CreateTextNode(m_ScriptLines[i]);
                NodeLine.AppendChild(NodeText);
                XmlEventScript.AppendChild(NodeLine);
            }
            // et le chemin du de l'image de fond
            XmlNode NodeImage = XmlDoc.CreateElement(XML_CF_TAG.ImagePath.ToString());
            XmlNode NodeTextImage = XmlDoc.CreateTextNode(m_strBackPictureFile);
            NodeImage.AppendChild(NodeTextImage);
            Node.AppendChild(NodeImage);
            return true;
        }

        //*****************************************************************************************************
        // Description: termine la lecture de l'objet. utilisé en mode Commande pour récupérer les référence
        // vers les objets utilisés et effectuer d'autre traitements
        // Return: /
        //*****************************************************************************************************
        public override bool FinalizeRead(BTDoc Doc)
        {
            // en mode command, on crée les controls et on les ajoute aux dynamic panel
            if (Program.TypeApp == TYPE_APP.SMART_COMMAND)
            {
                // on mémorise le point le plus en bas a droite
                Point pt = new Point();
                for (int i = 0; i < m_ListControls.Count; i++)
                {
                    // dernières init du control
                    m_ListControls[i].FinalizeRead(Doc);
                    m_ListControls[i].SetParent(this);
                    // on crée l'objet
                    m_ListControls[i].CreateControl();
                    // a partir de ce moment on connais leur taille définitive
                    if (pt.X < m_ListControls[i].DisplayedControl.Right)
                        pt.X = m_ListControls[i].DisplayedControl.Right;
                    if (pt.Y < m_ListControls[i].DisplayedControl.Bottom)
                        pt.Y = m_ListControls[i].DisplayedControl.Bottom;

                    this.m_DynamicPanel.Controls.Add(m_ListControls[i].DisplayedControl);
                }
                // on ajuste la taille du dynamic Panel
                m_DynamicPanel.Size = new Size(pt.X + 10, pt.Y + 10);
                try
                {
                    // si il y a une image, on la charge
                    Bitmap imgBack = new Bitmap(BackPictureFile);
                    imgBack.MakeTransparent(Cste.TransparencyColor);
                    // ets i besoin on réajuste la taille du panel
                    if (imgBack.Width > m_DynamicPanel.Width)
                        m_DynamicPanel.Width = imgBack.Width;
                    if (imgBack.Height > m_DynamicPanel.Height)
                        m_DynamicPanel.Height = imgBack.Height;

                    m_DynamicPanel.BackgroundImage = imgBack;
                }
                catch (Exception)
                {
                    // en cas d'ereur on logue al'utilisateur que l'image n'as pas été chargée
                    if (!string.IsNullOrEmpty(m_strBackPictureFile))
                    {
                        LogEvent log = new LogEvent(LOG_EVENT_TYPE.WARNING, string.Format("Screen {0} Failed to load file {1}", Symbol, m_strBackPictureFile));
                        MDISmartCommandMain.EventLogger.AddLogEvent(log);
                    }
                }
                // on garde un lien vers l'executer
                m_Executer = Doc.Executer;
            }
            return true;
        }

        #endregion

        #region Gestion des AppMessages
        //*****************************************************************************************************
        // Description: effectue les opération nécessaire lors de la récéption d'un message
        // Return: /
        //*****************************************************************************************************
        public override void TraiteMessage(MESSAGE Mess, object obj)
        {
            // en mode config, on execute les différents traitements sur les scripts
            if (Program.TypeApp == TYPE_APP.SMART_CONFIG)
            {
                m_GestControl.TraiteMessage(Mess, obj);
                ScriptTraiteMessage(Mess, m_InitScriptLines, obj);
                ScriptTraiteMessage(Mess, m_ScriptLines, obj);
            }
            else
            {
                // si on passe en mode run alors on execute les inits
                if (Mess == MESSAGE.MESS_CMD_RUN)
                {
                    ExecuteInitScript();
                }
            }
        }

        //*****************************************************************************************************
        // Description: effectue le traitement specifique aux script
        // Return: /
        //*****************************************************************************************************
        protected void ScriptTraiteMessage(MESSAGE Mess, StringCollection Script, object obj)
        {
            switch (Mess)
            {
                case MESSAGE.MESS_ASK_ITEM_DELETE:
                    if (((MessAskDelete)obj).TypeOfItem == typeof(Trame)
                        || ((MessAskDelete)obj).TypeOfItem == typeof(Function)
                        || ((MessAskDelete)obj).TypeOfItem == typeof(Logger)
                        || ((MessAskDelete)obj).TypeOfItem == typeof(BTTimer)
                        )
                    {
                        MessAskDelete MessParam = (MessAskDelete)obj;
                        for (int i = 0; i < Script.Count; i++)
                        {
                            string stritem = SmartApp.Scripts.ScriptParser.GetLineToken(Script[i], SmartApp.Scripts.ScriptParser.INDEX_TOKEN_SYMBOL);
                            if (stritem == MessParam.WantDeletetItemSymbol)
                            {
                                string strMess = string.Format("Screen {0} Script: Line {1} will be removed", Symbol, i + 1);
                                MessParam.ListStrReturns.Add(strMess);
                            }
                        }
                    }
                    break;
                case MESSAGE.MESS_ITEM_DELETED:
                    if (((MessDeleted)obj).TypeOfItem == typeof(Trame)
                        || ((MessDeleted)obj).TypeOfItem == typeof(Function)
                        || ((MessDeleted)obj).TypeOfItem == typeof(Logger)
                        || ((MessDeleted)obj).TypeOfItem == typeof(BTTimer)
                        )
                    {
                        MessDeleted MessParam = (MessDeleted)obj;
                        for (int i = 0; i < Script.Count; i++)
                        {
                            string stritem = SmartApp.Scripts.ScriptParser.GetLineToken(Script[i], SmartApp.Scripts.ScriptParser.INDEX_TOKEN_SYMBOL);
                            if (stritem == MessParam.DeletetedItemSymbol)
                            {
                                Script.RemoveAt(i);
                            }
                        }
                    }
                    break;
                case MESSAGE.MESS_ITEM_RENAMED:
                    if (((MessItemRenamed)obj).TypeOfItem == typeof(Trame)
                        || ((MessItemRenamed)obj).TypeOfItem == typeof(Function)
                        || ((MessItemRenamed)obj).TypeOfItem == typeof(Logger)
                        || ((MessItemRenamed)obj).TypeOfItem == typeof(BTTimer)
                        )
                    {
                        MessItemRenamed MessParam = (MessItemRenamed)obj;
                        for (int i = 0; i < Script.Count; i++)
                        {
                            string stritem = SmartApp.Scripts.ScriptParser.GetLineToken(Script[i], SmartApp.Scripts.ScriptParser.INDEX_TOKEN_SYMBOL);
                            if (stritem == MessParam.OldItemSymbol)
                            {
                                Script[i] = Script[i].Replace(MessParam.OldItemSymbol, MessParam.NewItemSymbol);
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public void ControlEvent()
        {
            if (m_ScriptLines.Count != 0)
            {
                m_Executer.ExecuteScript(this.ScriptLines);
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void ExecuteInitScript()
        {
            if (m_InitScriptLines.Count != 0)
            {
                m_Executer.ExecuteScript(this.InitScriptLines);
            }
        }
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public virtual void ObjectSendMessage(MESSAGE Mess, object Param)
        {
            ProcessSendMessage(Mess, Param);
        }

        #endregion
    }
}

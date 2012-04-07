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
using System.Drawing;

namespace CommonLib
{
    public class BTScreen : BaseObject, IScriptable
    {
        #region Données de la classe
        private static Control m_singStdConfigPanel;

        // gestionnaire des control appartenant a l'écran
        private GestControl m_GestControl;
        // titre de l'écran
        private string m_strTitle;

        private int m_iQuickScriptIDIni;
        // fichier image de "background de l'écran"
        private string m_strBackPictureFile= string.Empty;

        protected ItemScriptsConainter m_ScriptContainer = new ItemScriptsConainter();

        #endregion

        #region données sépcifiques au fonctionement en mode "Command"
        // objet dynamic pannel qui est affiché
        private DynamicPanel m_DynamicPanel = new DynamicPanel();
        // liste des "Control" de l'écran en mode commande
        private List<BTControl> m_ListControls = new List<BTControl>();
        // liens vers l'executer de script
#if !QUICK_MOTOR
        protected ScriptExecuter m_Executer = null;
#else
        protected QuickExecuter m_Executer = null;
#endif
        #endregion

        #region constructeur
        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public BTScreen()
        {
            m_GestControl = new GestControl();
            m_GestControl.DoSendMessage += new SendMessage(this.ObjectSendMessage);
            m_ScriptContainer["EvtScreen"] = new string[1];
            m_ScriptContainer["InitScreen"] = new string[1];
        }
        #endregion

        #region attributs
        /// <summary>
        /// obtient le gestionnaire de control de l'écran
        /// </summary>
        public GestControl Controls
        {
            get
            {
                return m_GestControl;
            }
        }

        /// <summary>
        /// obtient ou assigne le tire de l'écran
        /// </summary>
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

        /// <summary>
        /// obtient ou assigne le script du controle
        /// </summary>
        public ItemScriptsConainter ItemScripts
        {
            get
            {
                return m_ScriptContainer;
            }
        }

        public override Control StdConfigPanel
        {
            get
            {
                if (m_singStdConfigPanel == null)
                {
                    m_singStdConfigPanel = new ScreenPropertiesPanel();
                }
                return m_singStdConfigPanel;
            }
        }

        /// <summary>
        /// obtient ou assigne le chemin du fichier d'arrière plan de l'écran
        /// </summary>
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

        /// <summary>
        /// obtient ou assigne l'executer de l'écran
        /// </summary>
#if !QUICK_MOTOR
        public ScriptExecuter Executer
        {
            get
            {
                return m_Executer;
            }
        }
#else
        public QuickExecuter Executer
        {
            get
            {
                return m_Executer;
            }
        }
#endif

        /// <summary>
        /// valide uniquement dans SmartCommand
        /// obtient ou assigne le dynamic panel associé à l'écran
        /// </summary>
        public DynamicPanel Panel
        {
            get
            {
                return m_DynamicPanel;
            }
        }
        #endregion

        #region ReadIn / WriteOut
        /// <summary>
        /// Lit les données de l'objet a partir de son noeud XML
        /// NE PAS UTILISER
        /// </summary>
        /// <param name="Node">Noeud Xml de l'objet</param>
        /// <param name="TypeApp">type d'application courante</param>
        /// <returns>true si la lecture s'est bien passé</returns>
        public override bool ReadIn(XmlNode Node, TYPE_APP TypeApp)
        {
            System.Diagnostics.Debug.Assert(false);
            return false;
        }

        /// <summary>
        /// Lit les données de l'objet a partir de son noeud XML
        /// </summary>
        /// <param name="Node">Noeud Xml de l'objet</param>
        /// <param name="TypeApp">type d'application courante</param>
        /// <param name="GestDLL">Getsionnaire des DLL plugin</param>
        /// <returns>true si la lecture s'est bien passé</returns>
        public bool ReadIn(XmlNode Node, TYPE_APP TypeApp, DllControlGest GestDLL)
        {
            if (!base.ReadIn(Node, TypeApp))
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
                    Traces.LogAddDebug(TraceCat.Serialization, "Erreur récupération du type noeud fils dans BTScreen");
                    continue;
                }
                switch (TypeId)
                {
                    case XML_CF_TAG.ControlList:
                        {
                            // ceci ne peux pas être fait de la même manière en mode Config ou en mode Command
                            // dans le cas ou on est en mode config, on crée des objet BTControl
                            // qui utiliseront un intercative control dans le designer
                            if (TypeApp == TYPE_APP.SMART_CONFIG)
                            {

                                // on parcour la liste des control
                                for (int j = 0; j < ChildNode.ChildNodes.Count; j++)
                                {
                                    XmlNode NodeControl = ChildNode.ChildNodes[j];
                                    if (NodeControl.Name == XML_CF_TAG.Control.ToString())
                                    {
                                        BTControl Control = new BTControl();
                                        if (!Control.ReadIn(NodeControl, TypeApp))
                                            return false;

                                        m_GestControl.AddObj(Control);
                                    }
                                    else if (NodeControl.Name == XML_CF_TAG.SpecificControl.ToString())
                                    {
                                        BTControl Control = SpecificControlParser.ParseAndCreateSpecificControl(NodeControl);
                                        if (Control != null)
                                        {
                                            if (!Control.ReadIn(NodeControl, TypeApp))
                                                return false;
                                            m_GestControl.AddObj(Control);
                                        }
                                    }
                                    else if (NodeControl.Name == XML_CF_TAG.DllControl.ToString())
                                    {
                                        uint DllID= SpecificControlParser.ParseDllID(NodeControl);
                                        if (GestDLL[DllID] != null)
                                        {
                                            BTControl Control = GestDLL[DllID].CreateBTControl();
                                            if (Control != null)
                                            {
                                                if (!Control.ReadIn(NodeControl, TypeApp))
                                                    return false;
                                                m_GestControl.AddObj(Control);
                                            }
                                            else
                                            {
                                                System.Diagnostics.Debug.Assert(false);
                                                return false;
                                            }
                                        }
                                        else
                                        {
                                            XmlNode AttrSymbol = NodeControl.Attributes.GetNamedItem(XML_CF_ATTRIB.strSymbol.ToString());
                                            string symbol = "Unknown";
                                            if (!string.IsNullOrEmpty(AttrSymbol.Value))
                                                symbol = AttrSymbol.Value;
                                                
                                            MessageBox.Show(string.Format(Lang.LangSys.C("Missing plugins (ID = {1}) for object {0}") 
                                                                           + "\n" 
                                                                           + Lang.LangSys.C("If you save the file, object will be lost"), 
                                                                           symbol, 
                                                                           DllID), 
                                                                           Lang.LangSys.C("Warning"),
                                                                           MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        }
                                    }
                                    else
                                    {
                                        System.Diagnostics.Debug.Assert(false);
                                    }
                                }
                            }
                            // sinon on effectue une lecture spéciale mode Command
                            else if (TypeApp == TYPE_APP.SMART_COMMAND)
                            {
                                if (!ReadControlForCommandMode(ChildNode, GestDLL))
                                    return false;
                            }
                            else
                                System.Diagnostics.Debug.Assert(false);

                            break;
                        }
                    //on lit le script d'event
                    case XML_CF_TAG.EventScript:
                        {
                            List<string> listLines = new List<string>();
                            for (int j = 0; j < ChildNode.ChildNodes.Count; j++)
                            {
                                if (ChildNode.ChildNodes[j].Name == XML_CF_TAG.Line.ToString()
                                    && ChildNode.ChildNodes[j].FirstChild != null)
                                {
                                    listLines.Add(ChildNode.ChildNodes[j].FirstChild.Value);
                                }
                            }
                            m_ScriptContainer["EvtScreen"] = listLines.ToArray();
                        }
                        break;
                    // on lit le script d'init
                    case XML_CF_TAG.InitScript:
                        {
                            List<string> listLines = new List<string>();
                            for (int j = 0; j < ChildNode.ChildNodes.Count; j++)
                            {
                                if (ChildNode.ChildNodes[j].Name == XML_CF_TAG.Line.ToString()
                                    && ChildNode.ChildNodes[j].FirstChild != null)
                                {
                                    listLines.Add(ChildNode.ChildNodes[j].FirstChild.Value);
                                }
                            }
                            m_ScriptContainer["InitScreen"] = listLines.ToArray();
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
            for (int i = 0; i < m_GestControl.Count; i++)
            {
                BTControl ctrl = m_GestControl[i] as BTControl;
                ctrl.Parent = this;
            }
            return true;
        }

        /// <summary>
        /// Lit les données de l'objet a partir de son noeud XML spécialement pour SmartCommand
        /// </summary>
        /// <param name="Node">Noeud Xml de l'objet</param>
        /// <param name="GestDLL">Getsionnaire des DLL plugin</param>
        /// <returns>true si la lecture s'est bien passé</returns>
        private bool ReadControlForCommandMode(XmlNode Node, DllControlGest GestDll)
        {
            // En mode Commande on doit crée des objets pouvant afficher des vrais controls du framework
            // ceci est fait grace aux objets "baseControl" et dérivés
            for (int i = 0; i < Node.ChildNodes.Count; i++)
            {
                XmlNode ChildNode = Node.ChildNodes[i];
                if (ChildNode.Name == XML_CF_TAG.Control.ToString())
                {
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
                        Traces.LogAddDebug(TraceCat.Serialization, "Erreur récupération du type de controle BTScreen");
                        continue;
                    }

                    BTControl NewControl = null;
                    // on crée chaque objet en fonction de son type
                    switch (TypeId)
                    {
                        case CONTROL_TYPE.BUTTON:
                            NewControl = new ButtonControl();
                            NewControl.ReadIn(ChildNode, TYPE_APP.SMART_COMMAND);
                            break;
                        case CONTROL_TYPE.CHECK:
                            NewControl = new CheckControl();
                            NewControl.ReadIn(ChildNode, TYPE_APP.SMART_COMMAND);
                            break;
                        case CONTROL_TYPE.COMBO:
                            NewControl = new ComboControl();
                            NewControl.ReadIn(ChildNode, TYPE_APP.SMART_COMMAND);
                            break;
                        case CONTROL_TYPE.SLIDER:
                            NewControl = new SliderControl();
                            NewControl.ReadIn(ChildNode, TYPE_APP.SMART_COMMAND);
                            break;
                        case CONTROL_TYPE.STATIC:
                            NewControl = new StaticControl();
                            NewControl.ReadIn(ChildNode, TYPE_APP.SMART_COMMAND);
                            break;
                        case CONTROL_TYPE.UP_DOWN:
                            NewControl = new UpDownControl();
                            NewControl.ReadIn(ChildNode, TYPE_APP.SMART_COMMAND);
                            break;
                        case CONTROL_TYPE.SPECIFIC:
                            System.Diagnostics.Debug.Assert(false);
                            break;
                        case CONTROL_TYPE.NULL:
                        default:
                            Traces.LogAddDebug(TraceCat.Serialization, "ScreenReadCommand", "Type de control indéfini");
                            NewControl = null;
                            break;
                    }
                    if (NewControl != null)
                    {
                        NewControl.EventAddLogEvent += new AddLogEventDelegate(this.AddLogEvent);
                        m_ListControls.Add(NewControl);
                    }
                }
                else if (ChildNode.Name == XML_CF_TAG.SpecificControl.ToString())
                {
                    SPECIFIC_TYPE ControlType = SpecificControlParser.ParseSpecificControlType(ChildNode);
                    BTControl NewControl = null;
                    switch (ControlType)
                    {
                        case SPECIFIC_TYPE.FILLED_RECT:
                            NewControl = new FilledRectControl();
                            NewControl.ReadIn(ChildNode, TYPE_APP.SMART_COMMAND);
                            break;
                        case SPECIFIC_TYPE.FILLED_ELLIPSE:
                            NewControl = new FilledEllipseControl();
                            NewControl.ReadIn(ChildNode, TYPE_APP.SMART_COMMAND);
                            break;
                        case SPECIFIC_TYPE.NULL:
                        default:
                            Traces.LogAddDebug(TraceCat.Serialization, "ScreenReadCommand", "Type de control spécifique indéfini");
                            break;
                    }
                    if (NewControl != null)
                    {
                        NewControl.EventAddLogEvent += new AddLogEventDelegate(this.AddLogEvent);
                        m_ListControls.Add(NewControl);
                    }
                }
                else if (ChildNode.Name == XML_CF_TAG.DllControl.ToString())
                {
                    uint DllID = SpecificControlParser.ParseDllID(ChildNode);
                    BTControl NewControl = GestDll[DllID].CreateCommandBTControl();
                    NewControl.ReadIn(ChildNode, TYPE_APP.SMART_COMMAND);
                    if (NewControl != null)
                    {
                        NewControl.EventAddLogEvent += new AddLogEventDelegate(this.AddLogEvent);
                        m_ListControls.Add(NewControl);
                    }
                }
                else
                {
                    System.Diagnostics.Debug.Assert(false);
                    return false;
                }
            }
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
            for (int i = 0; i < m_ScriptContainer["InitScreen"].Length; i++)
            {
                if (!string.IsNullOrEmpty(m_ScriptContainer["InitScreen"][i]))
                {
                    XmlNode NodeLine = XmlDoc.CreateElement(XML_CF_TAG.Line.ToString());
                    XmlNode NodeText = XmlDoc.CreateTextNode(m_ScriptContainer["InitScreen"][i]);
                    NodeLine.AppendChild(NodeText);
                    XmlInitScript.AppendChild(NodeLine);
                }
            }
            for (int i = 0; i < m_ScriptContainer["EvtScreen"].Length; i++)
            {
                if (!string.IsNullOrEmpty(m_ScriptContainer["EvtScreen"][i]))
                {
                    XmlNode NodeLine = XmlDoc.CreateElement(XML_CF_TAG.Line.ToString());
                    XmlNode NodeText = XmlDoc.CreateTextNode(m_ScriptContainer["EvtScreen"][i]);
                    NodeLine.AppendChild(NodeText);
                    XmlEventScript.AppendChild(NodeLine);
                }
            }
            // et le chemin du de l'image de fond
            XmlNode NodeImage = XmlDoc.CreateElement(XML_CF_TAG.ImagePath.ToString());
            string strTemp = PathTranslator.AbsolutePathToRelative(m_strBackPictureFile);
            strTemp = PathTranslator.LinuxVsWindowsPathStore(strTemp);
            XmlNode NodeTextImage = XmlDoc.CreateTextNode(strTemp);
            NodeImage.AppendChild(NodeTextImage);
            Node.AppendChild(NodeImage);
            return true;
        }

        /// <summary>
        /// termine la lecture de l'objet. utilisé en mode Commande pour générer les objets graphiques affichés
        /// </summary>
        /// <param name="Doc">Document courant</param>
        /// <returns>true si tout s'est bien passé</returns>
        public override bool FinalizeRead(BTDoc Doc)
        {
            // en mode command, on crée les controls et on les ajoute aux dynamic panel
            if (Doc.TypeApp == TYPE_APP.SMART_COMMAND)
            {
                // on mémorise le point le plus en bas a droite
                Point pt = new Point();
                for (int i = 0; i < m_ListControls.Count; i++)
                {
                    // dernières init du control
                    m_ListControls[i].FinalizeRead(Doc);
                    m_ListControls[i].Parent = this;
                    // on crée l'objet
                    
                    m_ListControls[i].CreateControl();
                    m_ListControls[i].ApplyControlFont();
                    // a partir de ce moment on connais leur taille définitive
                    if (pt.X < m_ListControls[i].DisplayedControl.Right)
                        pt.X = m_ListControls[i].DisplayedControl.Right;
                    if (pt.Y < m_ListControls[i].DisplayedControl.Bottom)
                        pt.Y = m_ListControls[i].DisplayedControl.Bottom;


                }
                // on fait l'init graphique du panel (ajout de tout les control, création des listes d'optimisation
                // du refresh pour les objet dessiné sur le panel
                this.m_DynamicPanel.MyInitializeComponent(m_ListControls);
                // on ajuste la taille du dynamic Panel
                m_DynamicPanel.Size = new Size(pt.X + 10, pt.Y + 10);
                string strImageFullPath = PathTranslator.RelativePathToAbsolute(BackPictureFile);
                strImageFullPath = PathTranslator.LinuxVsWindowsPathUse(strImageFullPath);
                try
                {
                    // si il y a une image, on la charge
                    Bitmap imgBack = new Bitmap(strImageFullPath);
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
                    if (!string.IsNullOrEmpty(strImageFullPath))
                    {
                        LogEvent log = new LogEvent(LOG_EVENT_TYPE.WARNING, string.Format(Lang.LangSys.C("Screen {0} Failed to load file {1}"), Symbol, strImageFullPath));
                        AddLogEvent(log);
                    }
                }
                // on garde un lien vers l'executer
                m_Executer = Doc.Executer;
            }
            return true;
        }

        #endregion

        #region Gestion des AppMessages
        /// <summary>
        /// effectue les opération nécessaire lors de la récéption d'un message
        /// </summary>
        /// <param name="Mess">Type de message</param>
        /// <param name="obj">objet contenant les paramètres du messages</param>
        /// <param name="TypeApp">Type d'application courante</param>
        public override void TraiteMessage(MESSAGE Mess, object obj, TYPE_APP TypeApp)
        {
            // en mode config, on execute les différents traitements sur les scripts
            if (TypeApp == TYPE_APP.SMART_CONFIG)
            {
                m_GestControl.TraiteMessage(Mess, obj, TypeApp);
                ScriptTraiteMessage(this, Mess, this.m_ScriptContainer, obj);
            }
            else
            {
                // si on passe en mode run alors on execute les inits
                if (Mess == MESSAGE.MESS_CMD_RUN)
                {
                    ExecuteInitScript();
                }
#if QUICK_MOTOR
                else if (Mess==MESSAGE.MESS_PRE_PARSE)
                {
                    this.m_iQuickScriptID = m_Executer.PreParseScript(this.m_ScriptContainer["EvtScreen"]);
                    this.m_iQuickScriptIDIni = m_Executer.PreParseScript(this.m_ScriptContainer["InitScreen"]);
                }
#endif
                for (int i = 0; i < m_ListControls.Count; i++)
                {
                    m_ListControls[i].TraiteMessage(Mess, obj, TypeApp);
                }
            }
        }

        /// <summary>
        /// appelé par les controles lorsqu'ils ont l'option "Use Screen Event Script"
        /// en mode SmartCommande uniquement
        /// </summary>
        public void ControlEvent()
        {
            if (m_ScriptContainer["EvtScreen"].Length != 0)
            {
#if !QUICK_MOTOR
                m_Executer.ExecuteScript(this.ScriptLines);
#else
                m_Executer.ExecuteScript(this.m_iQuickScriptID);
#endif
            }
        }

        /// <summary>
        /// Execute le script d'initialisation
        /// </summary>
        protected void ExecuteInitScript()
        {
            if (m_ScriptContainer["InitScreen"].Length != 0)
            {
#if !QUICK_MOTOR
                m_Executer.ExecuteScript(this.InitScriptLines);
#else
                m_Executer.ExecuteScript(this.m_iQuickScriptIDIni);
#endif
            }
        }

        /// <summary>
        /// handler d'event appelé par le gestionnnaire de controle lors que les objets qu'ils contiens 
        /// déclenchent l'envoie d'un message
        /// </summary>
        /// <param name="Mess">Type de message</param>
        /// <param name="obj">objet contenant les paramètres du messages</param>
        /// <param name="TypeApp">Type d'application courante</param>
        public virtual void ObjectSendMessage(MESSAGE Mess, object Param, TYPE_APP TypeApp)
        {
            ProcessSendMessage(Mess, Param, TypeApp);
        }

        #endregion

        #region fonction SmartCommand
        /// <summary>
        /// Affiche l'écran au premier plan dans smart commande (fonction de script)
        /// </summary>
        public void ShowScreenToTop()
        {
            if (m_DynamicPanel != null)
            {
                m_DynamicPanel.SetToTop();
            }
        }
        public void TakeScreenShot(string filePath)
        {
            if (m_DynamicPanel != null)
            {
                m_DynamicPanel.DoScreenShot(filePath, this.Symbol);
            }
        }
        #endregion

    }
}

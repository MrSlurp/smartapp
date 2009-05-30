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
        private List<BTControl> m_ListControls = new List<BTControl>();
        // liens vers l'executer de script
        protected ScriptExecuter m_Executer = null;
        #endregion

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

        #endregion

        #region ReadIn / WriteOut
        //*****************************************************************************************************
        // Description: Lit les données de l'objet a partir de son noeud XML
        // Return: /
        //*****************************************************************************************************
        public override bool ReadIn(XmlNode Node, TYPE_APP TypeApp)
        {
            System.Diagnostics.Debug.Assert(false);
            return false;
        }

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
                                            MessageBox.Show(string.Format("Missing plugins (ID = {1}) for object {0}\nIf you save the file, object will be lost", symbol, DllID), "WARINING");
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
                            Console.WriteLine("Type de control indéfini");
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
                            Console.WriteLine("Type de control indéfini");
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
            string strTemp = PathTranslator.AbsolutePathToRelative(m_strBackPictureFile);
            strTemp = PathTranslator.LinuxVsWindowsPathStore(strTemp);
            XmlNode NodeTextImage = XmlDoc.CreateTextNode(strTemp);
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
            if (Doc.TypeApp == TYPE_APP.SMART_COMMAND)
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
                        LogEvent log = new LogEvent(LOG_EVENT_TYPE.WARNING, string.Format("Screen {0} Failed to load file {1}", Symbol, strImageFullPath));
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
        //*****************************************************************************************************
        // Description: effectue les opération nécessaire lors de la récéption d'un message
        // Return: /
        //*****************************************************************************************************
        public override void TraiteMessage(MESSAGE Mess, object obj, TYPE_APP TypeApp)
        {
            // en mode config, on execute les différents traitements sur les scripts
            if (TypeApp == TYPE_APP.SMART_CONFIG)
            {
                m_GestControl.TraiteMessage(Mess, obj, TypeApp);
                ScriptTraiteMessage(this, Mess, m_InitScriptLines, obj);
                ScriptTraiteMessage(this, Mess, m_ScriptLines, obj);
            }
            else
            {
                // si on passe en mode run alors on execute les inits
                if (Mess == MESSAGE.MESS_CMD_RUN)
                {
                    ExecuteInitScript();
                }
                for (int i = 0; i < m_ListControls.Count; i++)
                {
                    m_ListControls[i].TraiteMessage(Mess, obj, TypeApp);
                }
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
        public virtual void ObjectSendMessage(MESSAGE Mess, object Param, TYPE_APP TypeApp)
        {
            ProcessSendMessage(Mess, Param, TypeApp);
        }

        #endregion
    }
}

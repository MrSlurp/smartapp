/***************************************************************************/
// PROJET : BTCommand : system de commande paramétrable pour équipement
// ayant une mécanisme de commande par liaison série/ethernet/http
/***************************************************************************/
// Fichier : 
/***************************************************************************/
// description :
// 
/***************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace CommonLib
{
    public delegate void NeedRefreshHMI(MessNeedUpdate Mess);
    public delegate void DocumentModifiedEvent();
    public delegate void RunStateChangeEvent(BTDoc doc);
    public delegate void DocComStateChange(BTDoc doc);
    /// <summary>
    /// 
    /// </summary>
    public class BTDoc : BaseDoc
    {
        #region données membres
        // Stocke la liste de toutes les données de l'application
        private GestData        m_GestData = new GestData();
        // Stocke la liste de tous les ecrans de l'application
        private GestScreen      m_GestScreen = new GestScreen();
        // Stocke la liste de toutes les trames de l'application
        private GestTrame       m_GestTrame = new GestTrame();
        // Stocke la liste de toutes les fonctions de l'application
        private GestFunction    m_GestFunction = new GestFunction();
        // Stocke la liste de toutes les timers de l'application
        private GestTimer       m_GestTimer = new GestTimer();
        // Stocke la liste de toutes les loggers de l'application
        private GestLogger      m_GestLogger = new GestLogger();
        // C'est une image de la liste des données sans gestion des groupes (les groupes ne sont pas lu)
        private GestDataVirtual m_GestVirtualData = new GestDataVirtual();

        private DllControlGest m_GestDLL;

        PathTranslator m_PathTranslator = new PathTranslator();

        VirtualDataForm m_virtualDataForm;

        bool m_bUseMainContainer = false;
        protected Point m_MCPosition = new Point(-1, -1);
        protected Size m_MCSize = new Size(-1, -1);
        protected string m_MCTitle = string.Empty;
        protected bool m_bMCShowInTaskBar = true;
        protected bool m_bMCShowTitleBar = true;
        #endregion

        #region données membres en mode SmartCommand
        protected BTComm m_Comm = new BTComm();
        protected string m_strLogFilePath;
        // Liste des pages "utilisateur" ==> une par BTScreen présent dans chaque document
        private List<DynamicPanelForm> m_FormList;
        private DocumentMainContainer m_MainContainer;
        #endregion

        #region Events
        public event NeedRefreshHMI UpdateDocumentFrame;
        public event DocComStateChange OnCommStateChange;
        public event RunStateChangeEvent OnRunStateChange;
        #endregion

        #region donnée spécifiques aux fonctionement en mode Command
        QuickExecuter m_Executer = null;  
        #endregion

        #region attributs
        public QuickExecuter Executer
        {
            get
            {
                return m_Executer;
            }
        }

        private bool RunningState
        {
            get { return m_bModeRun; }
            set
            {
                bool prevValue = m_bModeRun;
                m_bModeRun = value;
                if (value != prevValue)
                {
                    UpdateRunState();
                    if (OnRunStateChange != null)
                        OnRunStateChange(this);
                }
            }
        }
        /// <summary>
        /// accesseur du type d'application dans lequel le document est chargé
        /// </summary>
        public TYPE_APP TypeApp
        {
            get
            {
                return m_TypeApp;
            }
        }

        /// <summary>
        /// renvoie le chemin du fichier de log
        /// </summary>
        public string LogFilePath
        {
            get
            {
                if (string.IsNullOrEmpty(m_strLogFilePath)
                    || !Directory.Exists(m_strLogFilePath))
                {
                    return Application.StartupPath;
                }
                else
                    return m_strLogFilePath;
            }
            set
            {
                m_strLogFilePath = value;
            }
        }

        /// <summary>
        /// renvoie le type de communication utilisé par le document
        /// </summary>
        public TYPE_COMM TypeComm
        {
            get
            {
                return m_Comm.CommType;
            }
        }

        public PathTranslator PathTr
        {
            get { return m_PathTranslator; }
        }

        public BTComm Communication
        {
            get { return m_Comm; }
        }
        #endregion

        #region constructeur
        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="TypeApp">Type d'application courante</param>
        public BTDoc(TYPE_APP TypeApp) : base(TypeApp)
        {
            m_GestData.DoSendMessage += new SendMessage(TraiteMessage);
            m_GestData.EventAddLogEvent += new AddLogEventDelegate(AddLogEvent);
            m_GestVirtualData.DoSendMessage += new SendMessage(TraiteMessage);
            m_GestVirtualData.EventAddLogEvent += new AddLogEventDelegate(AddLogEvent);
            m_GestScreen.DoSendMessage += new SendMessage(TraiteMessage);
            m_GestScreen.EventAddLogEvent += new AddLogEventDelegate(AddLogEvent);
            m_GestTrame.DoSendMessage += new SendMessage(TraiteMessage);
            m_GestTrame.EventAddLogEvent += new AddLogEventDelegate(AddLogEvent);
            m_GestFunction.DoSendMessage += new SendMessage(TraiteMessage);
            m_GestFunction.EventAddLogEvent += new AddLogEventDelegate(AddLogEvent);
            m_GestTimer.DoSendMessage += new SendMessage(TraiteMessage);
            m_GestTimer.EventAddLogEvent += new AddLogEventDelegate(AddLogEvent);
            m_GestLogger.DoSendMessage += new SendMessage(TraiteMessage);
            m_GestLogger.EventAddLogEvent += new AddLogEventDelegate(AddLogEvent);
            m_Comm.OnCommStateChange += new CommOpenedStateChange(this.CommeStateChangeEvent);
            m_Comm.EventAddLogEvent += new AddLogEventDelegate(AddLogEvent);
            m_Executer = new QuickExecuter();            
            m_Executer.EventAddLogEvent += new AddLogEventDelegate(AddLogEvent);
            m_Executer.Document = this;
        }

        /// <summary>
        /// destructeur
        /// </summary>
        ~BTDoc()
        {
            TraiteMessage(MESSAGE.MESS_CMD_STOP, null, TYPE_APP.SMART_COMMAND);
        }

        /// <summary>
        /// détache le handler d'event sur le status de la communication
        /// </summary>
        /// <param name="Handler">handler à détacher</param>
        public void DetachCommEventHandler(CommOpenedStateChange Handler)
        {
            if (OnCommStateChange != null)
            {
                m_Comm.OnCommStateChange -= this.CommeStateChangeEvent;
            }
        }
        #endregion

        #region attributs du main Container

        public bool UseMainContainer
        {
            get { return m_bUseMainContainer; }
            set { m_bUseMainContainer = value; }
        }

        public Size MCSize
        {
            get { return m_MCSize; }
            set { m_MCSize = value; }
        }

        public Point MCPosition
        {
            get { return m_MCPosition; }
            set { m_MCPosition = value; }
        }

        public bool MCStyleVisibleInTaskBar
        {
            get { return m_bMCShowInTaskBar; }
            set { m_bMCShowInTaskBar = value; }
        }

        public bool MCStyleShowTitleBar
        {
            get { return m_bMCShowTitleBar; }
            set { m_bMCShowTitleBar = value; }
        }

        public string MCTitle
        {
            get { return m_MCTitle; }
            set { m_MCTitle = value; }
        }

        #endregion

        #region attributs d'accès aux gestionnaires
        /// <summary>
        /// accesseur du gestionnaire de donnée du document
        /// </summary>
        public GestData GestData
        {   
            get
            {
                return m_GestData;
            }
        }

        /// <summary>
        /// accesseur du gestionnaire de donnée virtuelles du document
        /// </summary>
        public GestDataVirtual GestDataVirtual
        {
            get
            {
                return m_GestVirtualData;
            }
        }

        /// <summary>
        /// accesseur du gestionnaire de d'écran du document
        /// </summary>
        public GestScreen GestScreen
        {
            get
            {
                return m_GestScreen;
            }
        }

        /// <summary>
        /// accesseur du gestionnaire de de trammes du document
        /// </summary>
        public GestTrame GestTrame
        {
            get
            {
                return m_GestTrame;
            }
        }

        /// <summary>
        /// accesseur du gestionnaire de trames du document
        /// </summary>
        public GestFunction GestFunction
        {
            get
            {
                return m_GestFunction;
            }
        }

        /// <summary>
        /// accesseur du gestionnaire de timer du document
        /// </summary>
        public GestTimer GestTimer
        {
            get
            {
                return m_GestTimer;
            }
        }

        /// <summary>
        /// accesseur du gestionnaire de logger du document
        /// </summary>
        public GestLogger GestLogger
        {
            get
            {
                return m_GestLogger;
            }
        }
        #endregion

        #region Gestion des AppMessages
        /// <summary>
        /// Traite les messages d'application 
        /// </summary>
        /// <param name="Mess">type du message</param>
        /// <param name="Param">objet d'information étendu du message</param>
        /// <param name="TypeApp">type d'application </param>
        public void TraiteMessage(MESSAGE Mess, object Param, TYPE_APP TypeApp)
        {
            switch (Mess)
            {
                // les message suivant sont rerouté vers tout les objets
                case MESSAGE.MESS_ASK_ITEM_DELETE:
                case MESSAGE.MESS_ITEM_DELETED:
                case MESSAGE.MESS_ITEM_RENAMED:
                case MESSAGE.MESS_CMD_RUN:
                case MESSAGE.MESS_CMD_STOP:
                    if (MESSAGE.MESS_CMD_RUN == Mess)
                    {
                        TraiteMessage(MESSAGE.MESS_PRE_PARSE, null, TypeApp);
                        RunningState = true;
                    }
                    m_Executer.TraiteMessage(Mess, Param, TypeApp);
                    GestData.TraiteMessage(Mess, Param, TypeApp);
                    GestDataVirtual.TraiteMessage(Mess, Param, TypeApp);
                    GestScreen.TraiteMessage(Mess, Param, TypeApp);
                    GestTrame.TraiteMessage(Mess, Param, TypeApp);
                    GestTimer.TraiteMessage(Mess, Param, TypeApp);
                    GestLogger.TraiteMessage(Mess, Param, TypeApp);
                    GestFunction.TraiteMessage(Mess, Param, TypeApp);

                    if (MESSAGE.MESS_CMD_STOP == Mess)
                        RunningState = false;

                    if (UpdateDocumentFrame != null
                        && (Mess == MESSAGE.MESS_ITEM_DELETED || Mess == MESSAGE.MESS_ITEM_RENAMED)
                        )
                    {
                        if (((BaseMessage)Param).TypeOfItem != typeof(BTControl))
                        {
                            UpdateDocumentFrame.Invoke(new MessNeedUpdate((BaseMessage)Param));
                        }
                        Modified = true;
                    }
                    break;
                // ce message n'as pas a être transféré
                case MESSAGE.MESS_CHANGE: 
                    Modified = true;
                    break;
                case MESSAGE.MESS_UPDATE_FROM_DATA:
                    // le message sera transféré du gestionaire, vers les ecrans, 
                    // puis des ecrans vers les gestionaires de control, et donc vers les controles
                    GestScreen.TraiteMessage(Mess, Param, TypeApp);
                    break;
                case MESSAGE.MESS_PRE_PARSE:
                    if (!m_Executer.PreParsedDone)
                    {
                        GestScreen.TraiteMessage(Mess, null, TypeApp);
                        GestTimer.TraiteMessage(Mess, null, TypeApp);
                        GestFunction.TraiteMessage(Mess, null, TypeApp);
                        m_Executer.PreParsedDone = true;
                    }
                    break;
            }
        }
        #endregion

        #region lecture et sauvegarde du fichier
        /// <summary>
        /// lit le document XML de supervision
        /// </summary>
        /// <param name="strFullFileName">nom de chemin complet du fichier</param>
        /// <param name="TypeApp">type d'application</param>
        /// <param name="GestDll">gestionnaire de DLL à utiliser</param>
        /// <returns>true si la lecture a réussi, sinon false</returns>
        public bool ReadConfigDocument(string strFullFileName, TYPE_APP TypeApp, DllControlGest GestDll)
        {
            m_GestDLL = GestDll;
            m_strfileFullName = strFullFileName;
            m_PathTranslator.BTDocPath = Path.GetDirectoryName(strFullFileName);
            XmlDocument XmlDoc = new XmlDocument();
            try
            {
                XmlDoc.Load(strFullFileName);
            }
            catch (Exception e)
            {
                Traces.LogAddDebug(TraceCat.Document, "Le fichier est corrompu");
                Console.WriteLine(e.Message);
                return false;
            }

            XmlNode RootNode = XmlDoc.FirstChild;
            if (RootNode.Name != XML_CF_TAG.Root.ToString())
                return false;

            XmlNode Node = RootNode.FirstChild;
            while (Node != null)
            {
                XML_CF_TAG Id = XML_CF_TAG.Root;
                try
                {
                    Id = (XML_CF_TAG)Enum.Parse(typeof(XML_CF_TAG), Node.Name, true);
                }
                catch (Exception)
                {
                    // le fait de parser sur un enum peu lever une exception si la valeur n'appartiens pas a l'enum
                    Node = Node.NextSibling;
                    continue;
                }
                // on charge une par une chaque section
                switch (Id)
                {
                    case XML_CF_TAG.ProjOptions:
                        ReadProjectOptions(Node);
                        break;
                    case XML_CF_TAG.Comm:
                        m_Comm.ReadIn(RootNode, this);
                        break;
                    case XML_CF_TAG.PluginsGlobals:
                        m_GestDLL.ReadInPluginsGlobals(Node);
                        break;
                    case XML_CF_TAG.DataSection:
                        if (!this.GestData.ReadIn(Node, this))
                            return false;
                        if (TypeApp == TYPE_APP.SMART_COMMAND)
                        {
                            if (!this.GestDataVirtual.ReadIn(Node, this))
                                return false;
                        }
                        break;
                    case XML_CF_TAG.TrameSection:
                        if (!this.GestTrame.ReadIn(Node, this))
                            return false;
                        break;
                    case XML_CF_TAG.ScreenSection:
                        if (!this.GestScreen.ReadIn(Node, this, GestDll))
                            return false;
                        break;
                    case XML_CF_TAG.FileHeader:
                        if (!this.ReadFileHeader(Node, this.TypeApp))
                            return false;
                        break;
                    case XML_CF_TAG.Program:
                        if (!this.GestFunction.ReadIn(Node, this)
                            || !this.GestTimer.ReadIn(Node, this)
                            || !this.GestLogger.ReadIn(Node, this)
                            )
                            return false;
                        break;
                    default:
                        break;
                }
                Node = Node.NextSibling;
            }
            return true;
        }

        /// <summary>
        /// Lit le header du fichier
        /// </summary>
        /// <param name="NodeHeader">Noeud xml du header</param>
        /// <param name="TypeApp">type d'application</param>
        /// <returns>true si la lecture a reussi</returns>
        protected bool ReadFileHeader(XmlNode NodeHeader, TYPE_APP TypeApp)
        {
            for (int i = 0; i < NodeHeader.ChildNodes.Count; i++)
            {
                XmlNode SubHeaderNode = NodeHeader.ChildNodes.Item(i);
                XML_CF_TAG Id = XML_CF_TAG.Root;
                try
                {
                    Id = (XML_CF_TAG)Enum.Parse(typeof(XML_CF_TAG), SubHeaderNode.Name, true);
                }
                catch (Exception)
                {
                    continue;
                }
                
                switch (Id)
                {
                    case XML_CF_TAG.FileVersion:
                        // vérifier la version de fichier
                        if (Cste.CUR_FILE_VERSION.ToString() != SubHeaderNode.Value)
                        {
                            int FileVer = 0;
                            XmlNode AttrIndice = SubHeaderNode.Attributes.GetNamedItem(XML_CF_ATTRIB.Indice.ToString());
                            if (AttrIndice != null && int.TryParse(AttrIndice.Value, out FileVer))
                            {
                                if (FileVer < Cste.CUR_FILE_VERSION)
                                {
                                    if (TypeApp == TYPE_APP.SMART_CONFIG)
                                        MessageBox.Show(Lang.LangSys.C("This file have been created with an oldest version, if you save this file, you will not be able to read it with previous version"), 
                                                        Lang.LangSys.C("Warning"), 
                                                        MessageBoxButtons.OK,
                                                        MessageBoxIcon.Exclamation);
                                }
                                else if (FileVer > Cste.CUR_FILE_VERSION)
                                {
                                    if (TypeApp == TYPE_APP.SMART_CONFIG)
                                        MessageBox.Show(Lang.LangSys.C("This file have been created with a newer version, see website for updating your software"), 
                                                        Lang.LangSys.C("Warning"),
                                                        MessageBoxButtons.OK,
                                                        MessageBoxIcon.Exclamation);
                                    return false;
                                }
                            }
                            else
                                return false;
                        }
                        break;
                    case XML_CF_TAG.SoftVersion:
                        {
                            // vérifie la version de logiciel
                            XmlNode AttrIndice = SubHeaderNode.Attributes.GetNamedItem(XML_CF_ATTRIB.Indice.ToString());
                            if (AttrIndice.Value != Application.ProductVersion)
                            {
                                // voir si il y a un traitement a effectuer
                            }
                        }
                        break;
                    default:
                        // champ invalide
                        return false;
                }
            }
            return true;
        }

        /// <summary>
        /// sauvegarde le document si celui ci est issue de l'ouverture d'un fichier existant
        /// </summary>
        /// <param name="bShowError">affiche ou non les erreurs</param>
        /// <returns>true si la sauvegarde a reussie</returns>
        public override bool WriteConfigDocument(bool bShowError)
        {
            return WriteConfigDocument(m_strfileFullName, bShowError, m_GestDLL);
        }

        /// <summary>
        /// sauvegarde le document sous le nom passé en paramètre
        /// </summary>
        /// <param name="strFullFileName">nom complet du fichier</param>
        /// <param name="bShowError">affiche ou non les erreurs</param>
        /// <returns>true si la sauvegarde a réussie</returns>
        public bool WriteConfigDocument(string strFullFileName, bool bShowError, DllControlGest GestDll)
        {
            m_GestDLL = GestDll;
            // on met a jour les données de control juste avant de sauver le document
            // elles n'étaient pas utiles avant, mais la le fichier peut être utilisé dans BTCommand
            // et donc ces données sont nécessaires
            // remarque ceci pourrai être fait au démarrage de BTCommand.... a voir
            m_strfileFullName = strFullFileName;

            GestData.UpdateAllControlDatas(this.GestTrame);

            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.LoadXml("<Root></Root>");
            WriteFileHeader(XmlDoc);

            m_Comm.WriteOut(XmlDoc, XmlDoc.DocumentElement, this);
            WriteProjectOptions(XmlDoc, XmlDoc.DocumentElement);
            XmlNode NodePluginsGlobals = XmlDoc.CreateElement(XML_CF_TAG.PluginsGlobals.ToString());
            XmlDoc.DocumentElement.AppendChild(NodePluginsGlobals);
            m_GestDLL.WriteOutPluginsGlobals(XmlDoc, NodePluginsGlobals);

            XmlNode NodeDataSection = XmlDoc.CreateElement(XML_CF_TAG.DataSection.ToString());
            XmlDoc.DocumentElement.AppendChild(NodeDataSection);
            GestData.WriteOut(XmlDoc, NodeDataSection, this);

            XmlNode NodeScreenSection = XmlDoc.CreateElement(XML_CF_TAG.ScreenSection.ToString());
            XmlDoc.DocumentElement.AppendChild(NodeScreenSection);
            GestScreen.WriteOut(XmlDoc, NodeScreenSection, this);

            XmlNode NodeTrameSection = XmlDoc.CreateElement(XML_CF_TAG.TrameSection.ToString());
            XmlDoc.DocumentElement.AppendChild(NodeTrameSection);
            GestTrame.WriteOut(XmlDoc, NodeTrameSection, this);

            XmlNode NodeProg = XmlDoc.CreateElement(XML_CF_TAG.Program.ToString());
            XmlDoc.DocumentElement.AppendChild(NodeProg);
            GestFunction.WriteOut(XmlDoc, NodeProg, this);
            GestTimer.WriteOut(XmlDoc, NodeProg, this);
            GestLogger.WriteOut(XmlDoc, NodeProg, this);

            try
            {
                XmlDoc.Save(strFullFileName);
            }
            catch (UnauthorizedAccessException e)
            {
                if (bShowError)
                    MessageBox.Show(e.Message, Lang.LangSys.C("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return true;
        }

        /// <summary>
        /// écrit le header du fichier contenant les versions
        /// </summary>
        /// <param name="XmlDoc">Document XML dans lequel le header est écrit</param>
        private void WriteFileHeader(XmlDocument XmlDoc)
        {
            XmlNode NodeFileHeader = XmlDoc.CreateElement(XML_CF_TAG.FileHeader.ToString());
            XmlDoc.DocumentElement.AppendChild(NodeFileHeader);
            XmlNode NodeFileVer = XmlDoc.CreateElement(XML_CF_TAG.FileVersion.ToString());
            XmlAttribute FIndice = XmlDoc.CreateAttribute(XML_CF_ATTRIB.Indice.ToString());
            FIndice.Value = Cste.CUR_FILE_VERSION.ToString();
            NodeFileVer.Attributes.Append(FIndice);
            NodeFileHeader.AppendChild(NodeFileVer);
            XmlNode NodeSoftVer = XmlDoc.CreateElement(XML_CF_TAG.SoftVersion.ToString());
            XmlAttribute AIndice = XmlDoc.CreateAttribute(XML_CF_ATTRIB.Indice.ToString());
            AIndice.Value = Application.ProductVersion;
            NodeSoftVer.Attributes.Append(AIndice);
            //NodeFileVer.Value = "1";
            NodeFileHeader.AppendChild(NodeSoftVer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProjOptNode"></param>
        private void ReadProjectOptions(XmlNode ProjOptNode)
        {
            foreach (XmlNode node in ProjOptNode.ChildNodes)
            {
                if (node.Name == XML_CF_TAG.MainContainer.ToString())
                {
                    XmlNode attrIsUsed = node.Attributes.GetNamedItem("IsUsed");
                    if (attrIsUsed != null)
                    {
                        m_bUseMainContainer = bool.Parse(attrIsUsed.Value);
                        XmlNode AttrSize = node.Attributes.GetNamedItem(XML_CF_ATTRIB.size.ToString());
                        XmlNode AttrPos = node.Attributes.GetNamedItem(XML_CF_ATTRIB.Pos.ToString());
                        XmlNode AttrShowInTaskBar = node.Attributes.GetNamedItem("ShowInTaskBar");
                        XmlNode AttrShowTitleBar = node.Attributes.GetNamedItem("ShowTitleBar");
                        XmlNode AttrTitle = node.Attributes.GetNamedItem("MCTitle");
                        if (AttrSize != null
                            && AttrPos != null
                            && AttrShowInTaskBar != null
                            && AttrShowTitleBar != null)
                        {
                            string[] TabStrPos = AttrPos.Value.Split(',');
                            string[] TabStrSize = AttrSize.Value.Split(',');
                            this.MCPosition = new Point(int.Parse(TabStrPos[0]), int.Parse(TabStrPos[1]));
                            this.MCSize = new Size(int.Parse(TabStrSize[0]), int.Parse(TabStrSize[1]));
                            m_bMCShowInTaskBar = bool.Parse(AttrShowInTaskBar.Value);
                            m_bMCShowTitleBar = bool.Parse(AttrShowTitleBar.Value);
                            
                        }
                        if (AttrTitle != null)
                        {
                            this.m_MCTitle = AttrTitle.Value;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="XmlDoc"></param>
        private void WriteProjectOptions(XmlDocument XmlDoc, XmlNode node)
        {
            XmlNode projOptNode = XmlDoc.CreateElement(XML_CF_TAG.ProjOptions.ToString());
            XmlNode containerNode = XmlDoc.CreateElement(XML_CF_TAG.MainContainer.ToString());
            projOptNode.AppendChild(containerNode);
            XmlAttribute usedNode = XmlDoc.CreateAttribute("IsUsed");
            containerNode.Attributes.Append(usedNode);
            usedNode.Value = m_bUseMainContainer.ToString();

            XmlAttribute AttrSize = XmlDoc.CreateAttribute(XML_CF_ATTRIB.size.ToString());
            XmlAttribute AttrPos = XmlDoc.CreateAttribute(XML_CF_ATTRIB.Pos.ToString());
            XmlAttribute AttrShowInTaskBar = XmlDoc.CreateAttribute("ShowInTaskBar");
            XmlAttribute AttrShowTitleBar = XmlDoc.CreateAttribute("ShowTitleBar");
            XmlAttribute AttrTitle = XmlDoc.CreateAttribute("MCTitle");
            containerNode.Attributes.Append(AttrSize);
            containerNode.Attributes.Append(AttrPos);
            containerNode.Attributes.Append(AttrShowInTaskBar);
            containerNode.Attributes.Append(AttrShowTitleBar);
            containerNode.Attributes.Append(AttrTitle);
            AttrPos.Value = string.Format("{0},{1}", m_MCPosition.X, m_MCPosition.Y);
            AttrSize.Value = string.Format("{0},{1}", m_MCSize.Width, m_MCSize.Height);
            AttrShowInTaskBar.Value = m_bMCShowInTaskBar.ToString();
            AttrShowTitleBar.Value = m_bMCShowTitleBar.ToString();
            AttrTitle.Value = m_MCTitle;
            node.AppendChild(projOptNode);
        }
        /// <summary>
        /// finalise la lecture du document
        /// Cette étape est effectué à la fin de la lecture afin que les objet
        /// puisse établir des références directes sur les autres objets qu'ils utilisent/appel
        /// </summary>
        /// <param name="ParentMdiForm"></param>
        /// <returns></returns>
        public bool FinalizeRead(Form ParentMdiForm)
        {
            if (TypeApp == TYPE_APP.SMART_COMMAND)
            {
                if (!m_GestData.FinalizeRead(this)
                    || !m_GestVirtualData.FinalizeRead(this)
                    || !m_GestScreen.FinalizeRead(this)
                    || !m_GestTrame.FinalizeRead(this)
                    || !m_GestFunction.FinalizeRead(this)
                    || !m_GestLogger.FinalizeRead(this)
                    || !m_GestTimer.FinalizeRead(this)
                    )
                    return false;

            }
            return true;
        }

        #endregion

        /// <summary>
        /// ouvre la connexion du document
        /// </summary>
        public void OpenDocumentComm()
        {
            m_Comm.OpenComm();
        }
        /// <summary>
        /// ferme la connexion du document
        /// </summary>
        public void CloseDocumentComm()
        {
            m_Comm.CloseComm();
        }

        /// <summary>
        /// appelé lors du changement d'état de la connexion
        /// </summary>
        private void CommeStateChangeEvent()
        {
            if (OnCommStateChange != null)
                OnCommStateChange(this);

            if (m_Comm.IsOpen && m_Comm.CommType == TYPE_COMM.VIRTUAL)
            {
                m_virtualDataForm = new VirtualDataForm(this);
                m_virtualDataForm.Show();
                m_virtualDataForm.BringToFront();
            }
            else
            {
                if (m_virtualDataForm != null)
                {
                    m_virtualDataForm.Close();
                    m_virtualDataForm = null;
                }
            }
            if (!m_Comm.IsOpen && this.IsRunning)
            {
                this.TraiteMessage(MESSAGE.MESS_CMD_STOP, null, this.TypeApp);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected void UpdateRunState()
        {
            if (m_FormList != null)
            {
                foreach (DynamicPanelForm frm in m_FormList)
                {
                    if (frm.Document == this)
                    {
                        frm.DynamicPanelEnabled = this.IsRunning;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void CloseSupervisionForms()
        {
            if (m_FormList != null)
            {
                foreach (DynamicPanelForm frm in m_FormList)
                {
                    frm.DisableCloseProtection = true;
                    frm.Close();
                }
            }
            m_FormList.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Doc"></param>
        /// <returns></returns>
        public bool OpenDocumentForCommand()
        {
            m_FormList = new List<DynamicPanelForm>();
            if (this.m_bUseMainContainer)
            {
                m_MainContainer = new DocumentMainContainer();
                m_MainContainer.ShowInTaskbar = this.m_bMCShowInTaskBar;
                if (!m_bMCShowTitleBar)
                    m_MainContainer.FormBorderStyle = FormBorderStyle.None;

                if (MCPosition.X != -1)
                {
                    m_MainContainer.Left = MCPosition.X;
                    m_MainContainer.StartPosition = FormStartPosition.Manual;
                }
                if (MCPosition.Y != -1)
                {
                    m_MainContainer.Top = MCPosition.Y;
                    m_MainContainer.StartPosition = FormStartPosition.Manual;
                }

                if (MCSize.Width != -1)
                    m_MainContainer.Width = MCSize.Width;

                if (MCSize.Height != -1)
                    m_MainContainer.Height = MCSize.Height;

                m_MainContainer.Text = m_MCTitle;
            }
            // abonnement aux event de com et d'état
            for (int i = (GestScreen.Count -1) ; i >= 0; i--)
            {
                BTScreen Scr = this.GestScreen[i] as BTScreen;
                Scr.Panel.DocumentFileName = this.FileName;
                DynamicPanelForm Frm = new DynamicPanelForm(Scr.Panel);
                Scr.Panel.Location = new Point(0, 0);
                Frm.ClientSize = new Size(Scr.Panel.Width ,
                                          Scr.Panel.Height);

                Frm.ShowInTaskbar = Scr.StyleVisibleInTaskBar;
                if (!Scr.StyleShowTitleBar)
                    Frm.FormBorderStyle = FormBorderStyle.None;

                Frm.Show();

                if (Scr.ScreenPosition.X != -1)
                {
                    Frm.Left = Scr.ScreenPosition.X;
                    Frm.StartPosition = FormStartPosition.Manual;
                }
                if (Scr.ScreenPosition.Y != -1)
                {
                    Frm.Top = Scr.ScreenPosition.Y;
                    Frm.StartPosition = FormStartPosition.Manual;
                }

                if (Scr.ScreenSize.Width != -1)
                    Frm.Width = Scr.ScreenSize.Width;

                if (Scr.ScreenSize.Height != -1)
                    Frm.Height = Scr.ScreenSize.Height;


                Frm.Document = this;
                Frm.Text = Scr.Title;
                if (m_MainContainer != null)
                    Frm.MdiParent = m_MainContainer;
                Frm.DynamicPanelEnabled = false;
                m_FormList.Add(Frm);
            }
            if (m_FormList.Count != 0)
            {
                if (m_MainContainer != null)
                    m_MainContainer.Show();
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        public void CloseDocumentForCommand()
        {
            if (m_FormList.Count != 0)
            {
                if (m_MainContainer != null)
                {
                    m_MainContainer.DisableCloseProtection = true;
                    m_MainContainer.Close();
                    CloseSupervisionForms();
                    return;
                }
                else
                {
                    CloseSupervisionForms();
                }
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public void BuildStatFileInfo()
        {
            int iNbData = m_GestData.Count;
            int iNbScreen = m_GestScreen.Count;
            int iMoyItemPersScreen = 0;
            int iScreenItemMoyScriptLines = 0;
            int iNbItemWithScript = 0;
            for (int i = 0; i < m_GestScreen.Count; i++)
            {
                BTScreen scr = m_GestScreen[i] as BTScreen;
                iMoyItemPersScreen += scr.Controls.Count;
                for(int j = 0; j< scr.Controls.Count; j++)
                {
                    BTControl ctrl = scr.Controls[j] as BTControl;
                    foreach (string key in ctrl.ItemScripts.ScriptKeys)
                    {
                        iScreenItemMoyScriptLines += ctrl.ItemScripts[key].Length;
                    }
                    iNbItemWithScript++;
                }
            }
            if (iNbItemWithScript != 0)
                iScreenItemMoyScriptLines = iScreenItemMoyScriptLines / iNbItemWithScript;
            if (iNbScreen != 0)
                iMoyItemPersScreen = iMoyItemPersScreen / iNbScreen;

            int iNbTimer = m_GestTimer.Count;
            int iNbFunction = m_GestFunction.Count;
            int iNbLogger = m_GestLogger.Count;

            int iFunctionMoyScriptLines = 0;
            int iNbTotalScriptCount = 0;
            int iFunctionMaxScriptLines = 0;
            for (int i = 0; i < m_GestFunction.Count; i++)
            {
                Function item = m_GestFunction[i] as Function;
                if (item.ItemScripts.Count > 0)
                {
                    foreach (string key in item.ItemScripts.ScriptKeys)
                    {
                        if (iFunctionMaxScriptLines < item.ItemScripts[key].Length)
                            iFunctionMaxScriptLines = item.ItemScripts[key].Length;

                        iFunctionMoyScriptLines += item.ItemScripts[key].Length;
                        iNbTotalScriptCount++;
                    }
                }
            }
            if (iNbTotalScriptCount != 0)
                iFunctionMoyScriptLines = iFunctionMoyScriptLines / iNbTotalScriptCount;

            string commType = m_Comm.CommType.ToString();
        }
    }
}

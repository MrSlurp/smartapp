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
using System.Windows.Forms;
using System.IO;

namespace CommonLib
{
    public delegate void NeedRefreshHMI(MessNeedUpdate Mess);
    public delegate void DocumentModifiedEvent();
    //*****************************************************************************************************
    // Description:
    // Return: /
    //*****************************************************************************************************
    public class BTDoc : Object
    {
        #region données membres
        private string m_strfileFullName;
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

        private TYPE_APP m_TypeApp = TYPE_APP.NONE;
        // indique si le document a été modifié
        bool m_bModified = false;
        #endregion

        #region données membres en mode SmartCommand
        public BTComm m_Comm = new BTComm();
        protected string m_strLogFilePath;
        #endregion

        #region Events
        public event NeedRefreshHMI UpdateDocumentFrame;
        public event DocumentModifiedEvent OnDocumentModified;
        public event CommOpenedStateChange OnCommStateChange;
        public event AddLogEventDelegate EventAddLogEvent;
        #endregion

        #region donnée spécifiques aux fonctionement en mode Command
        ScriptExecuter m_Executer = new ScriptExecuter();
        #endregion

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public ScriptExecuter Executer
        {
            get
            {
                return m_Executer;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public TYPE_APP TypeApp
        {
            get
            {
                return m_TypeApp;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
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

        #region constructeur
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public BTDoc(TYPE_APP TypeApp)
        {
            m_GestData.DoSendMessage += new SendMessage(TraiteMessage);
            m_GestData.EventAddLogEvent += new AddLogEventDelegate(AddLogEvent);
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
            m_Executer.Document = this;
            m_TypeApp = TypeApp;
        }

        ~BTDoc()
        {
            TraiteMessage(MESSAGE.MESS_CMD_STOP, null, TYPE_APP.SMART_COMMAND);
        }

        public void DetachCommEventHandler(CommOpenedStateChange Handler)
        {
            if (OnCommStateChange != null)
            {
                Delegate.Remove(OnCommStateChange, Handler);
            }
        }
        #endregion

        #region attributs
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public string FileName
        {
            get
            {
                return m_strfileFullName;
            }
        }

        public bool Modified
        {
            get
            {
                return m_bModified;
            }
            set
            {
                m_bModified = value;
                if (OnDocumentModified != null)
                    OnDocumentModified();
            }
        }

        #endregion

        #region attributs d'accès aux gestionnaires
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public GestData GestData
        {   
            get
            {
                return m_GestData;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public GestScreen GestScreen
        {
            get
            {
                return m_GestScreen;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public GestTrame GestTrame
        {
            get
            {
                return m_GestTrame;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public GestFunction GestFunction
        {
            get
            {
                return m_GestFunction;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public GestTimer GestTimer
        {
            get
            {
                return m_GestTimer;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public GestLogger GestLogger
        {
            get
            {
                return m_GestLogger;
            }
        }
        #endregion

        #region Gestion des AppMessages
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
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
                    GestData.TraiteMessage(Mess, Param, TypeApp);
                    GestScreen.TraiteMessage(Mess, Param, TypeApp);
                    GestTrame.TraiteMessage(Mess, Param, TypeApp);
                    GestTimer.TraiteMessage(Mess, Param, TypeApp);
                    GestLogger.TraiteMessage(Mess, Param, TypeApp);
                    GestFunction.TraiteMessage(Mess, Param, TypeApp);
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
            }
        }
        #endregion

        #region lecture et sauvegarde du fichier;
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public bool ReadConfigDocument(string strFullFileName, TYPE_APP TypeApp)
        {
            m_strfileFullName = strFullFileName;
            XmlDocument XmlDoc = new XmlDocument();
            try
            {
                XmlDoc.Load(strFullFileName);
            }
            catch (Exception e)
            {
                string strErr = "The file is corrupted";
                strErr += e.Message;
                Console.WriteLine(strErr);
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
                    case XML_CF_TAG.DataSection:
                        if (!this.GestData.ReadIn(Node, TypeApp))
                            return false;
                        break;
                    case XML_CF_TAG.TrameSection:
                        if (!this.GestTrame.ReadIn(Node, TypeApp))
                            return false;
                        break;
                    case XML_CF_TAG.ScreenSection:
                        if (!this.GestScreen.ReadIn(Node, TypeApp))
                            return false;
                        break;
                    case XML_CF_TAG.FileHeader:
                        if (!this.ReadFileHeader(Node, TypeApp))
                            return false;
                        break;
                    case XML_CF_TAG.Program:
                        if (!this.GestFunction.ReadIn(Node, TypeApp)
                            || !this.GestTimer.ReadIn(Node, TypeApp)
                            || !this.GestLogger.ReadIn(Node, TypeApp)
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

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
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
                                        MessageBox.Show("This file have been created with an oldest version, if you save this file, you will not be able tio read it with previous version", "Warning");
                                }
                                else if (FileVer > Cste.CUR_FILE_VERSION)
                                {
                                    if (TypeApp == TYPE_APP.SMART_CONFIG)
                                        MessageBox.Show("This file have been created with a newer version, Contact distributor for updating your software", "Warning");
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

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public bool WriteConfigDocument(bool bShowError)
        {
            return WriteConfigDocument(m_strfileFullName, bShowError);
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public bool WriteConfigDocument(string strFullFileName, bool bShowError)
        {
            // on met a jour les données de control juste avant de sauver le document
            // elles n'étaient pas utiles avant, mais la le fichier peut être utilisé dans BTCommand
            // et donc ces données sont nécessaires
            // remarque ceci pourrai être fait au démarrage de BTCommand.... a voir
            m_strfileFullName = strFullFileName;

            GestData.UpdateAllControlDatas(this.GestTrame);

            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.LoadXml("<Root></Root>");
            WriteFileHeader(XmlDoc);

            XmlNode NodeDataSection = XmlDoc.CreateElement(XML_CF_TAG.DataSection.ToString());
            XmlDoc.DocumentElement.AppendChild(NodeDataSection);
            GestData.WriteOut(XmlDoc, NodeDataSection);

            XmlNode NodeScreenSection = XmlDoc.CreateElement(XML_CF_TAG.ScreenSection.ToString());
            XmlDoc.DocumentElement.AppendChild(NodeScreenSection);
            GestScreen.WriteOut(XmlDoc, NodeScreenSection);

            XmlNode NodeTrameSection = XmlDoc.CreateElement(XML_CF_TAG.TrameSection.ToString());
            XmlDoc.DocumentElement.AppendChild(NodeTrameSection);
            GestTrame.WriteOut(XmlDoc, NodeTrameSection);

            XmlNode NodeProg = XmlDoc.CreateElement(XML_CF_TAG.Program.ToString());
            XmlDoc.DocumentElement.AppendChild(NodeProg);
            GestFunction.WriteOut(XmlDoc, NodeProg);
            GestTimer.WriteOut(XmlDoc, NodeProg);
            GestLogger.WriteOut(XmlDoc, NodeProg);

            try
            {
                XmlDoc.Save(strFullFileName);
            }
            catch (UnauthorizedAccessException e)
            {
                if (bShowError)
                    MessageBox.Show(e.Message);
            }
            return true;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
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

        //*****************************************************************************************************
        // Description: 
        // Return: /
        //*****************************************************************************************************
        public bool FinalizeRead(Form ParentMdiForm)
        {
            if (TypeApp == TYPE_APP.SMART_COMMAND)
            {
                if (!m_GestData.FinalizeRead(this)
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

        //*****************************************************************************************************
        // Description: 
        // Return: /
        //*****************************************************************************************************
        public void OpenDocumentComm()
        {
            m_Comm.OpenComm();
        }
        //*****************************************************************************************************
        // Description: 
        // Return: /
        //*****************************************************************************************************
        public void CloseDocumentComm()
        {
            m_Comm.CloseComm();
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void CommeStateChangeEvent()
        {
            if (OnCommStateChange != null)
                OnCommStateChange();
        }

        protected void AddLogEvent(LogEvent Event)
        {
            if (EventAddLogEvent != null)
            {
                EventAddLogEvent(Event);
            }
        }
    }
}

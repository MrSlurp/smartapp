using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;

namespace CommonLib
{
    public class Logger : BaseObject
    {
        #region Déclaration des données de la classe
        // liste des symbols des données a loguer
        private StringCollection m_ListStrDatas = new StringCollection();
        // liste des données a loguer utilisé en mode Command
        private ArrayList m_ListRefDatas = new ArrayList();
        // type de Logger
        string m_LogType;
        // nom du fichier a loguer
        string m_strFileName;
        // période de l'auto logger
        int m_iPeriod;

        private bool m_bAutoStart = false;
        
        private char m_CsvSeperator = '\t';
        #endregion

        #region donnée spécifiques aux fonctionement en mode Command
        // timer executant périodiquement les logs
        Timer m_Timer = new Timer();
        // indique si le timer est actif
        bool m_bTimerActive = false;
        // chemin ou crée le fichier
        string m_strLogFilePath;
        // objet pour écrire le fichier
        StreamWriter m_FileWriter;
        #endregion

        #region attribut
        /// <summary>
        /// accesseur sur la liste des symboles des données loguées
        /// </summary>
        public StringCollection LoggerDatas
        {
            get
            {
                return m_ListStrDatas;
            }
        }

        /// <summary>
        /// Obtient ou assigne le type de logger
        /// </summary>
        public string LogType
        {
            get
            {
                return m_LogType;
            }
            set
            {
                m_LogType = value;
            }
        }

        /// <summary>
        /// obtient ou assigne le nom du fichier dans lequel les données seront loguées
        /// </summary>
        public string LogFile
        {
            get
            {
                return m_strFileName;
            }
            set
            {
                m_strFileName = value;
            }
        }

        /// <summary>
        /// obtient ou assigne la période du logger en ms
        /// </summary>
        public int Period
        {
            get
            {
                return m_iPeriod;
            }
            set
            {
                m_iPeriod = value;
            }
        }

        /// <summary>
        /// obtient ou assigne la booléen indiquant si le logger est activé automatiquement au démarrage
        /// </summary>
        public bool AutoStart
        {
            get
            {
                return m_bAutoStart;
            }
            set
            {
                m_bAutoStart = value;
            }
        }

        public char CsvSeperator
        {
            get
            {
                return m_CsvSeperator;
            }
            set
            {
                m_CsvSeperator = value;
            }
        }
        #endregion

        #region constructeurs
        /// <summary>
        /// constructeur par défaut
        /// </summary>
        public Logger()
        {
            m_LogType = LOGGER_TYPE.STANDARD.ToString();
            m_iPeriod = 1000;
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
            bool bRet = base.ReadIn(Node, TypeApp);
            // on lit les attributs et la liste des données
            XmlNode LoggerTypeAttrib = Node.Attributes.GetNamedItem(XML_CF_ATTRIB.LoggerType.ToString());
            XmlNode PeriodAttrib = Node.Attributes.GetNamedItem(XML_CF_ATTRIB.Period.ToString());
            XmlNode FileNameAttrib = Node.Attributes.GetNamedItem(XML_CF_ATTRIB.FileName.ToString());
            if (LoggerTypeAttrib == null
                || PeriodAttrib == null
                || FileNameAttrib == null)
                return false;

            m_LogType = LoggerTypeAttrib.Value;
            m_iPeriod = int.Parse(PeriodAttrib.Value);
            m_strFileName = FileNameAttrib.Value;

            if (Node.FirstChild.Name != XML_CF_TAG.DataList.ToString())
                return false;
            XmlNode NodeDataList = Node.FirstChild;
            for (int j = 0; j < NodeDataList.ChildNodes.Count; j++)
            {
                XmlNode NodeData = NodeDataList.ChildNodes[j];
                if (NodeData.Name != XML_CF_TAG.Data.ToString())
                    continue;
                XmlNode SymbAttr = NodeData.Attributes.GetNamedItem(XML_CF_ATTRIB.strSymbol.ToString());
                m_ListStrDatas.Add(SymbAttr.Value);
            }

            XmlNode AutoStartAttrib = Node.Attributes.GetNamedItem(XML_CF_ATTRIB.AutoStart.ToString());
            if (AutoStartAttrib != null)
                m_bAutoStart = bool.Parse(AutoStartAttrib.Value);

            XmlNode SepAttrib = Node.Attributes.GetNamedItem(XML_CF_ATTRIB.CsvSeparator.ToString());
            if (SepAttrib != null && !string.IsNullOrEmpty(SepAttrib.Value))
            {
                char [] attrChars = SepAttrib.Value.ToCharArray();
                m_CsvSeperator = attrChars[0];
            }

            return bRet;
        }

        /// <summary>
        /// écrit les données de l'objet dans le fichier XML
        /// </summary>
        /// <param name="XmlDoc">Document XML courant</param>
        /// <param name="Node">Noeud parent du controle dans le document</param>
        /// <returns>true si l'écriture s'est déroulée avec succès</returns>
        public override bool WriteOut(XmlDocument XmlDoc, XmlNode Node)
        {
            base.WriteOut(XmlDoc, Node);
            // on écrit les attributs et la liste des données
            XmlAttribute LoggerTypeAttrib = XmlDoc.CreateAttribute(XML_CF_ATTRIB.LoggerType.ToString());
            XmlAttribute PeriodAttrib = XmlDoc.CreateAttribute(XML_CF_ATTRIB.Period.ToString());
            XmlAttribute FileNameAttrib = XmlDoc.CreateAttribute(XML_CF_ATTRIB.FileName.ToString());
            XmlAttribute AutoStartAttrib = XmlDoc.CreateAttribute(XML_CF_ATTRIB.AutoStart.ToString());
            XmlAttribute SepAttrib = XmlDoc.CreateAttribute(XML_CF_ATTRIB.CsvSeparator.ToString());
            PeriodAttrib.Value = m_iPeriod.ToString();
            LoggerTypeAttrib.Value = m_LogType;
            FileNameAttrib.Value = m_strFileName;
            AutoStartAttrib.Value = m_bAutoStart.ToString();
            SepAttrib.Value = new string(m_CsvSeperator,1);
            Node.Attributes.Append(PeriodAttrib);
            Node.Attributes.Append(LoggerTypeAttrib);
            Node.Attributes.Append(FileNameAttrib);
            Node.Attributes.Append(AutoStartAttrib);
            Node.Attributes.Append(SepAttrib);

            XmlNode NodeDataList = XmlDoc.CreateElement(XML_CF_TAG.DataList.ToString());
            Node.AppendChild(NodeDataList);
            for (int i = 0; i < m_ListStrDatas.Count; i++)
            {
                string strSymb = (string)m_ListStrDatas[i];
                XmlNode NodeData = XmlDoc.CreateElement(XML_CF_TAG.Data.ToString());
                XmlAttribute attrSymb = XmlDoc.CreateAttribute(XML_CF_ATTRIB.strSymbol.ToString());
                attrSymb.Value = strSymb;
                NodeData.Attributes.Append(attrSymb);
                NodeDataList.AppendChild(NodeData);
            }

            return true;
        }

        /// <summary>
        /// termine la lecture de l'objet. utilisé en mode Commande pour récupérer les référence
        /// vers les objets utilisés
        /// </summary>
        /// <param name="Doc">Document courant</param>
        /// <returns>true si tout s'est bien passé</returns>
        public override bool FinalizeRead(BTDoc Doc)
        {

            // on fait une liste de références directe sur les données
            for (int i = 0; i < m_ListStrDatas.Count; i++)
            {
                string strData = (string)m_ListStrDatas[i];
                Data Dat = (Data)Doc.GestData.GetFromSymbol(strData);
                if (Dat == null)
                {
                    string strMessage;
                    strMessage = string.Format("Data to log not found (Logger {0}, Data {1}", m_strSymbol, strData);
                    LogEvent log = new LogEvent(LOG_EVENT_TYPE.INFO, strMessage);
                    AddLogEvent(log);
                    continue;
                }
                m_ListRefDatas.Add(Dat);
            }
            m_Timer.Interval = this.m_iPeriod;
            m_Timer.Tick += new EventHandler(OnTimerTick);
            m_strLogFilePath = Doc.LogFilePath;
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
            if (TypeApp == TYPE_APP.SMART_CONFIG)
            {
                switch (Mess)
                {
                    case MESSAGE.MESS_ASK_ITEM_DELETE:
                        if (((MessAskDelete)obj).TypeOfItem == typeof(Data))
                        {
                            MessAskDelete MessParam = (MessAskDelete)obj;
                            for (int i = 0; i < m_ListStrDatas.Count; i++)
                            {
                                if (m_ListStrDatas[i] == MessParam.WantDeletetItemSymbol)
                                {
                                    string strMess = string.Format("Logger {0} will lost data", Symbol);
                                    MessParam.ListStrReturns.Add(strMess);
                                }
                            }
                        }
                        break;
                    case MESSAGE.MESS_ITEM_DELETED:
                        if (((MessDeleted)obj).TypeOfItem == typeof(Data))
                        {
                            MessDeleted MessParam = (MessDeleted)obj;
                            for (int i = 0; i < m_ListStrDatas.Count; i++)
                            {
                                if (m_ListStrDatas[i] == MessParam.DeletetedItemSymbol)
                                {
                                    m_ListStrDatas.RemoveAt(i);
                                }
                            }
                        }
                        break;
                    case MESSAGE.MESS_ITEM_RENAMED:
                        if (((MessItemRenamed)obj).TypeOfItem == typeof(Data))
                        {
                            MessItemRenamed MessParam = (MessItemRenamed)obj;
                            for (int i = 0; i < m_ListStrDatas.Count; i++)
                            {
                                if (m_ListStrDatas[i] == MessParam.OldItemSymbol)
                                {
                                    m_ListStrDatas[i] = MessParam.NewItemSymbol;
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (Mess)
                {

                    case MESSAGE.MESS_CMD_RUN:
                        if (m_LogType == LOGGER_TYPE.AUTO.ToString() && m_bAutoStart)
                        {
                            string fileFullPath = PathTranslator.LinuxVsWindowsPathUse(m_strLogFilePath + @"\" + m_strFileName);
                            m_FileWriter = new StreamWriter(File.Open(fileFullPath, FileMode.Append, FileAccess.Write, FileShare.Read));
                            this.StartAutoLogger();
                        }
                        break;
                    case MESSAGE.MESS_CMD_STOP:
                        if (m_LogType == LOGGER_TYPE.AUTO.ToString())
                        {
                            this.StopAutoLogger();
                            if (m_FileWriter != null)
                                m_FileWriter.Close();
                            m_FileWriter = null;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion

        #region fonction d'execution en mode Command
        /// <summary>
        /// démarre le logger automatique
        /// </summary>
        public void StartAutoLogger()
        {
            if (m_LogType == LOGGER_TYPE.AUTO.ToString())
            {
                if (m_bTimerActive)
                {
                    //LogEvent log = new LogEvent(LOG_EVENT_TYPE.INFO, string.Format("{0} Trying to start an already started auto logger", Symbol));
                    //MDISmartCommandMain.EventLogger.AddLogEvent(log);
                }
                m_Timer.Start();
                m_bTimerActive = true;
                //string fileFullPath = m_strLogFilePath + @"\" + m_strFileName;
                //m_FileWriter = new StreamWriter(File.Open(fileFullPath, FileMode.Append));
            }
            else
            {
                LogEvent log = new LogEvent(LOG_EVENT_TYPE.INFO, string.Format("{0} Can't start standard logger", Symbol));
                AddLogEvent(log);
            }
        }

        /// <summary>
        /// arrête de logger automatique
        /// </summary>
        public void StopAutoLogger()
        {
            if (m_LogType == LOGGER_TYPE.AUTO.ToString())
            {
                if (!m_bTimerActive)
                {
                    //LogEvent log = new LogEvent(LOG_EVENT_TYPE.INFO, string.Format("{0} Trying to stop an already stopped auto logger", Symbol));
                    //MDISmartCommandMain.EventLogger.AddLogEvent(log);
                }
                m_Timer.Stop();
                m_bTimerActive = false;
            }
            else
            {
                LogEvent log = new LogEvent(LOG_EVENT_TYPE.INFO, string.Format("{0} Can't start standard logger", Symbol));
                AddLogEvent(log);
            }
        }

        /// <summary>
        /// execute le log des données en vérifiant que le fichier est accessible
        /// </summary>
        public void LogData()
        {
            if (m_FileWriter != null)
            {
                LogLine();
            }
            else
            {
                // sile fichier est fermé, on l'ouvre
                string fileFullPath = PathTranslator.LinuxVsWindowsPathUse(m_strLogFilePath + @"\" + m_strFileName);
                try
                {
                    m_FileWriter = new StreamWriter(File.Open(fileFullPath, FileMode.Append, FileAccess.Write, FileShare.Read));
                }
                catch (Exception)
                {
                    LogEvent log = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format("{0} Can't access file", Symbol));
                    AddLogEvent(log);
                    return;
                }
                // et on logue
                LogLine();
            }
        }

        /// <summary>
        /// vide le fichier de log
        /// </summary>
        public void ClearLog()
        {
            if (m_FileWriter != null)
            {
                // on ferme le fichier en cour
                m_FileWriter.Close();
                m_FileWriter = null;
                string fileFullPath = PathTranslator.LinuxVsWindowsPathUse(m_strLogFilePath + @"\" + m_strFileName);
                // on supprime son contenu
                try
                {
                    m_FileWriter = new StreamWriter(File.Open(fileFullPath, FileMode.Truncate, FileAccess.Write, FileShare.Read));
                }
                catch (Exception )
                {
                    LogEvent log = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format("{0} Can't access file", Symbol));
                    AddLogEvent(log);
                    return;
                }
                if (m_FileWriter != null)
                    m_FileWriter.Close();
                m_FileWriter = null;

                // on dois ensuite le réouvrir en ecriture
                try
                {
                    m_FileWriter = new StreamWriter(File.Open(fileFullPath, FileMode.Append, FileAccess.Write, FileShare.Read));
                }
                catch (Exception )
                {
                    LogEvent log = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format("{0} Can't access file", Symbol));
                    AddLogEvent(log);
                    return;
                }
            }
            else
            {
                // sinon on est en mode manuel, on ouvre le fichier et on le fermera a la fin
                string fileFullPath = PathTranslator.LinuxVsWindowsPathUse(m_strLogFilePath + @"\" + m_strFileName);
                try
                {
                    m_FileWriter = new StreamWriter(File.Open(fileFullPath, FileMode.Truncate, FileAccess.Write, FileShare.Read));
                }
                catch (Exception )
                {
                    LogEvent log = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format("{0} Can't access file", Symbol));
                    AddLogEvent(log);
                    return;
                }
                m_FileWriter.Close();
                m_FileWriter = null;
            }
        }

        /// <summary>
        /// logue une ligne de données. Met l'entete de fichier si besoin
        /// </summary>
        private void LogLine()
        {
            // si le fichier est fermé a ce moment on quitte
            if (m_FileWriter == null)
                return;

            // si il n'y a pas de données a loguer, on avertis l'utilisateur et on sort
            if (m_ListRefDatas.Count == 0)
            {
                LogEvent log = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format("{0} No Data to log", Symbol));
                AddLogEvent(log);
                return;
            }
            //le fichier a déja été ouvert on est en mode auto
            string lineToWrite;
            
            // si le fichier est vide on met l'en tete
            string fileFullPath = PathTranslator.LinuxVsWindowsPathUse(m_strLogFilePath + @"\" + m_strFileName);
            FileInfo fi = new FileInfo(fileFullPath);
            if (fi.Length == 0)
            {
                lineToWrite = "Time" + CsvSeperator;
                for (int i = 0; i < m_ListRefDatas.Count; i++)
                {
                    lineToWrite += ((BaseObject)m_ListRefDatas[i]).Symbol + CsvSeperator;
                }
                m_FileWriter.WriteLine(lineToWrite);
            }
            
            // ensuite on logue la ligne
            string strDate = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() +":" + string.Format("{0,3:d}", DateTime.Now.Millisecond);
            lineToWrite = strDate + CsvSeperator;
            for (int i = 0; i < m_ListRefDatas.Count; i++)
            {
                lineToWrite += ((Data)m_ListRefDatas[i]).Value.ToString();
                if (!(i == m_ListRefDatas.Count - 1))
                    lineToWrite += CsvSeperator;
            }
            m_FileWriter.WriteLine(lineToWrite);
            m_FileWriter.Flush();
        }

        /// <summary>
        /// évènement du timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTimerTick(object sender, EventArgs e)
        {
            m_Timer.Stop();
            LogData();
            if (m_bTimerActive)
                m_Timer.Start();
        }
        #endregion
    }
}

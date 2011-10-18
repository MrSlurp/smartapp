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
        public enum LogMode
        {
            none,
            autoNum,
            autoDated,
        };
        #region Déclaration des données de la classe
        // liste des symbols des données a loguer
        private StringCollection m_ListStrDatas = new StringCollection();
        // liste des données a loguer utilisé en mode Command
        private ArrayList m_ListRefDatas = new ArrayList();
        // type de Logger
        string m_LogType;
        // nom du fichier a loguer
        string m_strFileName;
        string m_UsedFileName;
        // période de l'auto logger
        int m_iPeriod = 1000;

        // indique si le logger démarre automatiquement au démarrage de la supervision
        private bool m_bAutoStart = false;
        // séparateur utilisé dans les fichier csv
        private char m_CsvSeperator = '\t';

        LogMode m_LoggerMode = LogMode.none;

        string m_DateFormatString = "yyyy-MM-dd_HH-mm-ss";
        
        bool m_bDoNotKeepFileOpen = false;
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
        /// obtient ou assigne le mode de loggere
        /// </summary>e
        public LogMode LoggerMode
        {
            get
            {
                return m_LoggerMode;
            }
            set
            {
                m_LoggerMode = value;
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
                ComputeFileName();
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

        /// <summary>
        /// séparateur de fichier csv
        /// </summary>
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

        /// <summary>
        /// formar string
        /// </summary>
        public string DateFormatString
        {
            get
            {
                return m_DateFormatString;
            }
            set
            {
                m_DateFormatString = value;
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

            XmlNode FormatStringAttrib = Node.Attributes.GetNamedItem(XML_CF_ATTRIB.FormatString.ToString());
            if (FormatStringAttrib != null && !string.IsNullOrEmpty(FormatStringAttrib.Value))
            {
                m_DateFormatString = FormatStringAttrib.Value;
            }

            XmlNode LogModeAttrib = Node.Attributes.GetNamedItem(XML_CF_ATTRIB.LogMode.ToString());
            if (LogModeAttrib != null && !string.IsNullOrEmpty(LogModeAttrib.Value))
            {
                LogMode lgMode = LogMode.none;
                try
                {
                    lgMode = (LogMode)Enum.Parse(typeof(LogMode), LogModeAttrib.Value, true);
                }
                catch (Exception)
                {
                    Traces.LogAddDebug(TraceCat.SmartConfig, "Exception lors de la récupération du mode de log " + this.Symbol);
                }
                m_LoggerMode = lgMode;
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
            XmlAttribute FormatStringAttrib = XmlDoc.CreateAttribute(XML_CF_ATTRIB.FormatString.ToString());
            XmlAttribute LogModeAttrib = XmlDoc.CreateAttribute(XML_CF_ATTRIB.LogMode.ToString());

            PeriodAttrib.Value = m_iPeriod.ToString();
            LoggerTypeAttrib.Value = m_LogType;
            FileNameAttrib.Value = m_strFileName;
            AutoStartAttrib.Value = m_bAutoStart.ToString();
            SepAttrib.Value = new string(m_CsvSeperator,1);
            FormatStringAttrib.Value = m_DateFormatString;
            LogModeAttrib.Value = m_LoggerMode.ToString();

            Node.Attributes.Append(PeriodAttrib);
            Node.Attributes.Append(LoggerTypeAttrib);
            Node.Attributes.Append(FileNameAttrib);
            Node.Attributes.Append(AutoStartAttrib);
            Node.Attributes.Append(SepAttrib);
            Node.Attributes.Append(FormatStringAttrib);
            Node.Attributes.Append(LogModeAttrib);

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
                    strMessage = string.Format(Lang.LangSys.C("Data to log not found (Logger {0}, Data {1}"), m_strSymbol, strData);
                    LogEvent log = new LogEvent(LOG_EVENT_TYPE.INFO, strMessage);
                    AddLogEvent(log);
                    continue;
                }
                m_ListRefDatas.Add(Dat);
            }
            m_UsedFileName = m_strFileName; 
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
                                    string strMess = string.Format(Lang.LangSys.C("Logger {0} will lost data"), Symbol);
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
                        ComputeFileName();
                        if (m_LogType == LOGGER_TYPE.AUTO.ToString() && m_bAutoStart)
                        {
                            this.StartAutoLogger();
                        }
                        break;
                    case MESSAGE.MESS_CMD_STOP:
                        if (m_LogType == LOGGER_TYPE.AUTO.ToString())
                        {
                            this.StopAutoLogger();
                            CleanFileStreamClose();
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
                    //AddLogEvent(log);
                }
                else
                {
                    m_Timer.Start();
                    m_bTimerActive = true;
                }
            }
            else
            {
                LogEvent log = new LogEvent(LOG_EVENT_TYPE.INFO, string.Format(Lang.LangSys.C("{0} Can't start standard logger"), Symbol));
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
                    //AddLogEvent(log);
                }
                else
                {
                    m_Timer.Stop();
                    m_bTimerActive = false;
                    CleanFileStreamClose();
                }
            }
            else
            {
                LogEvent log = new LogEvent(LOG_EVENT_TYPE.INFO, string.Format(Lang.LangSys.C("{0} Can't stop standard logger"), Symbol));
                AddLogEvent(log);
            }
        }

        /// <summary>
        /// execute le log des données en vérifiant que le fichier est accessible
        /// </summary>
        public void LogData()
        {
            OpenStreamWriter(FileMode.Append);
            LogLine();
        }

        /// <summary>
        /// ouvre le filestream pour écrire dans le fichier
        /// </summary>
        /// <param name="fm"></param>
        /// <returns></returns>
        private bool OpenStreamWriter(FileMode fm)
        {
            if (m_FileWriter == null)
            {
                string fileFullPath = PathTranslator.LinuxVsWindowsPathUse(m_strLogFilePath + @"\" + m_UsedFileName);
                try
                {
                    m_FileWriter = new StreamWriter(File.Open(fileFullPath, fm, FileAccess.Write, FileShare.Read));
                }
                catch (Exception)
                {
                    LogEvent log = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format(Lang.LangSys.C("{0} Can't access file"), Symbol));
                    AddLogEvent(log);
                    Traces.LogAddDebug(TraceCat.CommonLib, "Erreur accès au fichier dans LogData");
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// ferme le filestream proprement et remet la référence à null
        /// </summary>
        private void CleanFileStreamClose()
        {
            if (m_FileWriter != null)
            {
                // on ferme le fichier en cour
                m_FileWriter.Close();
                m_FileWriter.Dispose();
                m_FileWriter = null;
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
                CleanFileStreamClose();
                // on supprime son contenu
                if (!OpenStreamWriter(FileMode.Truncate))
                    return;

                CleanFileStreamClose();
                // on dois ensuite le réouvrir en ecriture
                if (!OpenStreamWriter(FileMode.Append))
                    return;
            }
            else
            {
                // sinon on est en mode manuel, on ouvre le fichier et on le fermera a la fin
                if (!OpenStreamWriter(FileMode.Truncate))
                    return;
                CleanFileStreamClose();
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
                LogEvent log = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format(Lang.LangSys.C("{0} No Data to log"), Symbol));
                AddLogEvent(log);
                return;
            }
            //le fichier a déja été ouvert on est en mode auto
            string lineToWrite;
            
            // si le fichier est vide on met l'en tete
            string fileFullPath = PathTranslator.LinuxVsWindowsPathUse(m_strLogFilePath + @"\" + m_UsedFileName);
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
            if (m_LogType == LOGGER_TYPE.STANDARD.ToString() || m_bDoNotKeepFileOpen)
            {
                CleanFileStreamClose();
            }

        }

        /// <summary>
        /// ferme le file stream et met a jour le nom de fichier à utiliser à la prochaine ouverture de celui ci.
        /// </summary>
        public void NewFile()
        {
            CleanFileStreamClose();
            ComputeFileName();
        }

        /// <summary>
        /// 
        /// </summary>
        private void ComputeFileName()
        {
            DateTime currentTime = DateTime.Now;
            
            switch (m_LoggerMode)
            {
                case LogMode.autoNum:
                    for (int iIdx = 1; iIdx < int.MaxValue; iIdx++)
                    {
                        string finalFileName = m_strFileName + string.Format("{0}", iIdx);
                        string FullPath = PathTranslator.LinuxVsWindowsPathUse(m_strLogFilePath + @"\" + finalFileName);
                        if (!File.Exists(FullPath + ".csv"))
                        {
                            m_UsedFileName = finalFileName;
                            break;
                        }
                    }
                    break;
                case LogMode.autoDated:
                    {
                        string finalFileName = m_strFileName + currentTime.ToString(m_DateFormatString);
                        string FullPath = PathTranslator.LinuxVsWindowsPathUse(m_strLogFilePath + @"\" + finalFileName);
                        for (int iIdx = 1; iIdx < int.MaxValue; iIdx++)
                        {
                            if (File.Exists(FullPath + ".csv"))
                            {
                                m_UsedFileName = finalFileName + string.Format("_{0}", iIdx);
                            }
                            else
                            {
                                m_UsedFileName = finalFileName;
                                break;
                            }
                        }
                    }
                    break;
                default:
                case LogMode.none:
                    m_UsedFileName = m_strFileName;
                    break;
            }
            m_UsedFileName += ".csv";
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

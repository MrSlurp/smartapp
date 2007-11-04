using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using SmartApp.Datas;
using SmartApp.AppEventLog;

namespace SmartApp.Scripts
{
    public class ScriptExecuter
    {
        private delegate void ScriptAddedToExecute();
        BTDoc m_Document = null;
        static bool m_bIsWaiting = false;
        static int nbinstanceexecuter = 0;
        Queue<StringCollection> m_PileScriptsToExecute = new Queue<StringCollection>();

        private event ScriptAddedToExecute EvScriptToExecute;

        public ScriptExecuter()
        {
            EvScriptToExecute += new ScriptAddedToExecute(ScriptExecuter_EvScriptToExecute);
            nbinstanceexecuter++;
        }

         ~ScriptExecuter()
        {
            nbinstanceexecuter--;
        }

        void ScriptExecuter_EvScriptToExecute()
        {
            while (m_PileScriptsToExecute.Count != 0)
            {
                // on prend le script sans l'enlever afin de savoir qu'il n'est pas encore execut�
                StringCollection Script = m_PileScriptsToExecute.Peek();
                if (m_bIsWaiting)
                    System.Diagnostics.Debug.Assert(false, "appel en trop");

                InternalExecuteScript(Script);
                // il est �x�cut�, on l'enl�ve de la liste.
                m_PileScriptsToExecute.Dequeue();
            }
        }
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public BTDoc Document
        {
            get
            {
                return m_Document;
            }
            set
            {
                m_Document = value;
            }
        }

        public void ExecuteScript(string[] ScriptLines)
        {
            StringCollection strCollct = new StringCollection();
            strCollct.Clear();
            for (int i = 0; i < ScriptLines.Length; i++)
            {
                strCollct.Add(ScriptLines[i]);
            }
            m_PileScriptsToExecute.Enqueue(strCollct);
            if (m_PileScriptsToExecute.Count > 1)
                return;
            if (EvScriptToExecute != null)
                EvScriptToExecute();
        }

        public void ExecuteScript(StringCollection Script)
        {
            m_PileScriptsToExecute.Enqueue(Script);
            // si on a plus de un script a executer, alors on ce sera fait 
            //par la boucle d'execution (voir ScriptExecuter_EvScriptToExecute)
            //donc on quitte
            if (m_PileScriptsToExecute.Count > 1)
                return;
            if (EvScriptToExecute != null)
                EvScriptToExecute();
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public void InternalExecuteScript(string[] ScriptLines)
        {
            StringCollection strCollct = new StringCollection();
            strCollct.Clear();
            for (int i = 0; i < ScriptLines.Length; i++)
            {
                strCollct.Add(ScriptLines[i]);
            }
            InternalExecuteScript(strCollct);
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public void InternalExecuteScript(StringCollection Lines)
        {
            for (int i = 0; i < Lines.Count; i++)
            {
                if (Lines[i].Length > 0)
                {
                    string Line = Lines[i];
                    string[] strTab = Line.Split('.');
                    if (strTab.Length > 1)
                    {
                        string strScrObject = strTab[0];
                        SCR_OBJECT FirstTokenType = ParseFirstTokenType(Line);
                        switch (FirstTokenType)
                        {
                            case SCR_OBJECT.FRAMES:
                                ParseExecuteFrame(Line);
                                break;
                            case SCR_OBJECT.FUNCTIONS:
                                ExecuteFunctionScript(strTab[1]);
                                break;
                            case SCR_OBJECT.LOGGERS:
                                ParseExecuteLogger(Line);
                                break;
                            case SCR_OBJECT.TIMERS:
                                ParseExecuteTimers(Line);
                                break;
                            case SCR_OBJECT.INVALID:
                            default:
                                break;
                        }
                    }
                    else
                    {
                    }
                }
            }
            //m_Document.m_Comm.DiscardRecievedDatas();
        }
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected SCR_OBJECT ParseFirstTokenType(string Line)
        {
            string[] strTab = Line.Split('.');
            if (strTab.Length > 0)
            {
                string strScrObject = strTab[0];
                SCR_OBJECT FirstTokenType = SCR_OBJECT.INVALID;
                try
                {
                    FirstTokenType = (SCR_OBJECT)Enum.Parse(typeof(SCR_OBJECT), strScrObject);
                }
                catch (Exception)
                {
                    return SCR_OBJECT.INVALID;
                }
                return FirstTokenType;
            }
            return SCR_OBJECT.INVALID;
        }

        #region execution des fonction
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void ExecuteFunctionScript(string FunctionSymbol)
        {
            FunctionSymbol = FunctionSymbol.Remove(FunctionSymbol.Length - 2);
            Function fct = (Function)m_Document.GestFunction.GetFromSymbol(FunctionSymbol);
            if (fct != null)
                this.ExecuteScript(fct.ScriptLines);
            else
            {
                LogEvent log = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format("Unknown function {0}", FunctionSymbol));
                MDISmartCommandMain.EventLogger.AddLogEvent(log);
            }
        }
        #endregion

        #region execution des timers
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void ParseExecuteTimers(string line)
        {
            string[] strTab = line.Split('.');
            if (strTab.Length > 2)
            {
                string strTimer = strTab[1];
                strTimer = strTimer.Trim();
                string strTemp = strTab[2];
                string strTempFull = strTemp;
                int posOpenParenthese = strTemp.LastIndexOf('(');
                if (posOpenParenthese >= 0)
                    strTemp = strTemp.Remove(posOpenParenthese);

                string strScrObject = strTemp;
                TIMER_FUNC SecondTokenType = TIMER_FUNC.INVALID;
                try
                {
                    SecondTokenType = (TIMER_FUNC)Enum.Parse(typeof(TIMER_FUNC), strScrObject);
                }
                catch (Exception)
                {
                    return;
                }
                switch (SecondTokenType)
                {
                    case TIMER_FUNC.START:
                        ExecuteStartTimer(strTimer);
                        break;
                    case TIMER_FUNC.STOP:
                        ExecuteStopTimer(strTimer);
                        break;
                }
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void ExecuteStartTimer(string TimerSymbol)
        {
            BTTimer tm = (BTTimer)m_Document.GestTimer.GetFromSymbol(TimerSymbol);
            tm.StartTimer();
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void ExecuteStopTimer(string TimerSymbol)
        {
            BTTimer tm = (BTTimer)m_Document.GestTimer.GetFromSymbol(TimerSymbol);
            tm.StopTimer();
        }

        #endregion

        #region execution des trames
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void ParseExecuteFrame(string line)
        {
            string[] strTab = line.Split('.');
            if (strTab.Length > 2)
            {
                string strFrame = strTab[1];
                strFrame = strFrame.Trim();
                string strTemp = strTab[2];
                string strTempFull = strTemp;
                int posOpenParenthese = strTemp.LastIndexOf('(');
                if (posOpenParenthese >= 0)
                    strTemp = strTemp.Remove(posOpenParenthese);

                string strScrObject = strTemp;
                FRAME_FUNC SecondTokenType = FRAME_FUNC.INVALID;
                try
                {
                    SecondTokenType = (FRAME_FUNC)Enum.Parse(typeof(FRAME_FUNC), strScrObject);
                }
                catch (Exception)
                {
                    return;
                }
                switch (SecondTokenType)
                {
                    case FRAME_FUNC.RECEIVE:
                        ExecuteRecieveFrame(strFrame);
                        break;
                    case FRAME_FUNC.SEND:
                        ExecuteSendFrame(strFrame);
                        break;
                }
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void ExecuteSendFrame(string FrameSymbol)
        {
            Trame TrameToSend = (Trame)m_Document.GestTrame.GetFromSymbol(FrameSymbol);
            Byte[] buffer = TrameToSend.CreateTrameToSend();
            if (m_Document.m_Comm.IsOpen)
            {
                //Console.WriteLine("{1} Trame Envoy� {0}", FrameSymbol, DateTime.Now.ToLongTimeString() + ":" + DateTime.Now.Millisecond);
                m_Document.m_Comm.SendData(buffer);
            }
            else
            {
                LogEvent log = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format("Trying sending frame {0} while connection is closed", TrameToSend.Symbol));
                MDISmartCommandMain.EventLogger.AddLogEvent(log);
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void ExecuteRecieveFrame(string FrameSymbol)
        {
            Trame TrameToRecieve = (Trame)m_Document.GestTrame.GetFromSymbol(FrameSymbol);
            if (m_Document.m_Comm.IsOpen)
            {
                byte[] FrameHeader = TrameToRecieve.FrameHeader;
                int ConvertedSize = TrameToRecieve.GetConvertedTrameSizeInByte();
                //Console.WriteLine("{1} Trame En attente de r�c�ption {0}", FrameSymbol, DateTime.Now.ToLongTimeString() + ":" + DateTime.Now.Millisecond);
                m_bIsWaiting = true;
                if (!m_Document.m_Comm.WaitTrameRecieved(ConvertedSize, FrameHeader))
                {
                    m_bIsWaiting = false;
                    //indiquer qu'une trame n'a pas �t� recu
                    // et demander a l'utilisateur si il souhaite continuer l'execution des actions
                    // si il ne veux pas, remonter au parent qu'il doit arr�ter les actions
                    //COMM_ERROR Err = m_Doc.m_Comm.ErrorCode;
                    string strmess = string.Format("Message {0} have not been recieved (Timeout)", TrameToRecieve.Symbol);
                    LogEvent log = new LogEvent(LOG_EVENT_TYPE.ERROR, strmess); 
                    MDISmartCommandMain.EventLogger.AddLogEvent(log);
                    return;
                }
                m_bIsWaiting = false;
                byte[] buffer = m_Document.m_Comm.GetRecievedData(ConvertedSize, FrameHeader);
                if (buffer == null || !TrameToRecieve.TreatRecieveTrame(buffer))
                {
                    string strmess;

                    if (buffer == null)
                        strmess = string.Format("Error reading message {0} (Recieved frame is not the one expected)", TrameToRecieve.Symbol);
                    else
                        strmess = string.Format("Error reading message {0}", TrameToRecieve.Symbol);
                    LogEvent log = new LogEvent(LOG_EVENT_TYPE.ERROR, strmess);
                    MDISmartCommandMain.EventLogger.AddLogEvent(log);
                }
                else
                {
                    //Console.WriteLine("{1} Trame Lue {0}", FrameSymbol, DateTime.Now.ToLongTimeString() + ":" + DateTime.Now.Millisecond);
                }

            }
            else
            {
                LogEvent log = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format("Trying reading frame {0} while connection is closed", TrameToRecieve.Symbol));
                MDISmartCommandMain.EventLogger.AddLogEvent(log);
            }
        }

        #endregion

        #region execution des Loggers
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void ParseExecuteLogger(string line)
        {
            string[] strTab = line.Split('.');
            if (strTab.Length > 2)
            {
                string strLog = strTab[1];
                strLog = strLog.Trim();
                string strTemp = strTab[2];
                string strTempFull = strTemp;
                int posOpenParenthese = strTemp.LastIndexOf('(');
                if (posOpenParenthese >= 0)
                    strTemp = strTemp.Remove(posOpenParenthese);

                string strScrObject = strTemp;
                LOGGER_FUNC SecondTokenType = LOGGER_FUNC.INVALID;
                try
                {
                    SecondTokenType = (LOGGER_FUNC)Enum.Parse(typeof(LOGGER_FUNC), strScrObject);
                }
                catch (Exception)
                {
                    return;
                }
                switch (SecondTokenType)
                {
                    case LOGGER_FUNC.CLEAR:
                        ExecuteClearLogger(strLog);
                        break;
                    case LOGGER_FUNC.LOG:
                        ExecuteLogLogger(strLog);
                        break;
                    case LOGGER_FUNC.START:
                        ExecuteStartAutoLogger(strLog);
                        break;
                    case LOGGER_FUNC.STOP:
                        ExecuteStopAutoLogger(strLog);
                        break;
                }
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void ExecuteClearLogger(string LoggerSymbol)
        {
            Logger log = (Logger)m_Document.GestLogger.GetFromSymbol(LoggerSymbol);
            log.ClearLog();
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void ExecuteLogLogger(string LoggerSymbol)
        {
            Logger log = (Logger)m_Document.GestLogger.GetFromSymbol(LoggerSymbol);
            log.LogData();
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void ExecuteStartAutoLogger(string LoggerSymbol)
        {
            Logger log = (Logger)m_Document.GestLogger.GetFromSymbol(LoggerSymbol);
            log.StartAutoLogger();
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void ExecuteStopAutoLogger(string LoggerSymbol)
        {
            Logger log = (Logger)m_Document.GestLogger.GetFromSymbol(LoggerSymbol);
            log.StopAutoLogger();
        }

        #endregion

    }
}

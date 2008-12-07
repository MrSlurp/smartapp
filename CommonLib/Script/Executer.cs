using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace CommonLib
{
    public class ScriptExecuter
    {
        private delegate void ScriptAddedToExecute();
        BTDoc m_Document = null;
        static bool m_bIsWaiting = false;
        static int nbinstanceexecuter = 0;
        Queue<StringCollection> m_PileScriptsToExecute = new Queue<StringCollection>();

        private event ScriptAddedToExecute EvScriptToExecute;
        public event AddLogEventDelegate EventAddLogEvent;

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
                // on prend le script sans l'enlever afin de savoir qu'il n'est pas encore executé
                StringCollection Script = m_PileScriptsToExecute.Peek();
                if (m_bIsWaiting)
                    System.Diagnostics.Debug.Assert(false, "appel en trop");

                InternalExecuteScript(Script);
                // il est éxécuté, on l'enlève de la liste.
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

        #region Fonction d'execution des scripts
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
                            case SCR_OBJECT.MATHS:
                                ParseExecuteMaths(Line);
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
        #endregion

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
                AddLogEvent(log);
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
            if (tm != null)
                tm.StartTimer();
            else
            {
                LogEvent log = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format("Unknown Timer {0}", TimerSymbol));
                AddLogEvent(log);
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void ExecuteStopTimer(string TimerSymbol)
        {
            BTTimer tm = (BTTimer)m_Document.GestTimer.GetFromSymbol(TimerSymbol);
            if (tm != null)
                tm.StopTimer();
            else
            {
                LogEvent log = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format("Unknown Timer {0}", TimerSymbol));
                AddLogEvent(log);
            }
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
            if (TrameToSend != null)
            {
                Byte[] buffer = TrameToSend.CreateTrameToSend(false);
                if (m_Document.m_Comm.IsOpen)
                {
                    if (m_Document.m_Comm.CommType == TYPE_COMM.VIRTUAL)
                        m_Document.m_Comm.SendData(TrameToSend, m_Document.GestData, m_Document.GestDataVirtual);
                    else
                        m_Document.m_Comm.SendData(buffer);
                    
                }
                else
                {
                    LogEvent log = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format("Trying sending frame {0} while connection is closed", TrameToSend.Symbol));
                    AddLogEvent(log);
                }
            }
            else
            {
                LogEvent log = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format("Unknown Frame {0}", FrameSymbol));
                AddLogEvent(log);
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void ExecuteRecieveFrame(string FrameSymbol)
        {
            Trame TrameToRecieve = (Trame)m_Document.GestTrame.GetFromSymbol(FrameSymbol);
            if (TrameToRecieve != null)
            {
                if (m_Document.m_Comm.IsOpen)
                {
                    byte[] FrameHeader = TrameToRecieve.FrameHeader;
                    int ConvertedSize = TrameToRecieve.GetConvertedTrameSizeInByte();
                    m_bIsWaiting = true;
                    if (!m_Document.m_Comm.WaitTrameRecieved(ConvertedSize, FrameHeader))
                    {
                        m_bIsWaiting = false;
                        //indiquer qu'une trame n'a pas été recu
                        // et demander a l'utilisateur si il souhaite continuer l'execution des actions
                        // si il ne veux pas, remonter au parent qu'il doit arrèter les actions
                        //COMM_ERROR Err = m_Doc.m_Comm.ErrorCode;
                        string strmess = string.Format("Message {0} have not been recieved (Timeout)", TrameToRecieve.Symbol);
                        LogEvent log = new LogEvent(LOG_EVENT_TYPE.ERROR, strmess);
                        AddLogEvent(log);
                        return;
                    }
                    m_bIsWaiting = false;
                    byte[] buffer = null;
                    if (m_Document.m_Comm.CommType == TYPE_COMM.VIRTUAL)
                        buffer = m_Document.m_Comm.GetRecievedData(ConvertedSize, TrameToRecieve);
                    else
                        buffer = m_Document.m_Comm.GetRecievedData(ConvertedSize, FrameHeader);

                    if (buffer == null || !TrameToRecieve.TreatRecieveTrame(buffer))
                    {
                        string strmess;

                        if (buffer == null)
                            strmess = string.Format("Error reading message {0} (Recieved frame is not the one expected)", TrameToRecieve.Symbol);
                        else
                            strmess = string.Format("Error reading message {0}", TrameToRecieve.Symbol);
                        LogEvent log = new LogEvent(LOG_EVENT_TYPE.ERROR, strmess);
                        AddLogEvent(log);
                    }
                    else
                    {
                        //Console.WriteLine("{1} Trame Lue {0}", FrameSymbol, DateTime.Now.ToLongTimeString() + ":" + DateTime.Now.Millisecond);
                    }
                }
                else
                {
                    LogEvent log = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format("Trying reading frame {0} while connection is closed", TrameToRecieve.Symbol));
                    AddLogEvent(log);
                }
            }
            else
            {
                LogEvent log = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format("Unknown Timer {0}", FrameSymbol));
                AddLogEvent(log);
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
            if (log != null)
                log.ClearLog();
            else
            {
                LogEvent logEvent = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format("Unknown Logger {0}", LoggerSymbol));
                AddLogEvent(logEvent);
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void ExecuteLogLogger(string LoggerSymbol)
        {
            Logger log = (Logger)m_Document.GestLogger.GetFromSymbol(LoggerSymbol);
            if (log != null)
                log.LogData();
            else
            {
                LogEvent logEvent = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format("Unknown Logger {0}", LoggerSymbol));
                AddLogEvent(logEvent);
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void ExecuteStartAutoLogger(string LoggerSymbol)
        {
            Logger log = (Logger)m_Document.GestLogger.GetFromSymbol(LoggerSymbol);
            if (log != null)
                log.StartAutoLogger();
            else
            {
                LogEvent logEvent = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format("Unknown Logger {0}", LoggerSymbol));
                AddLogEvent(logEvent);
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void ExecuteStopAutoLogger(string LoggerSymbol)
        {
            Logger log = (Logger)m_Document.GestLogger.GetFromSymbol(LoggerSymbol);
            if (log != null)
                log.StopAutoLogger();
            else
            {
                LogEvent logEvent = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format("Unknown Logger {0}", LoggerSymbol));
                AddLogEvent(logEvent);
            }
        }

        #endregion

        #region execution des Loggers
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void ParseExecuteMaths(string line)
        {
            string[] strTab = line.Split('.');
            if (strTab.Length == 2)
            {
                string strTempFull = strTab[1];
                int posOpenParenthese = strTempFull.LastIndexOf('(');
                int posCloseParenthese = strTempFull.LastIndexOf(')');

                string MathFunc = strTab[1];
                MathFunc = MathFunc.Remove(posOpenParenthese);
                MathFunc = MathFunc.Trim();

                MATHS_FUNC SecondTokenType = MATHS_FUNC.INVALID;
                try
                {
                    SecondTokenType = (MATHS_FUNC)Enum.Parse(typeof(MATHS_FUNC), MathFunc);
                }
                catch (Exception)
                {
                    return;
                }
                if (SecondTokenType != MATHS_FUNC.INVALID)
                {
                    string strParams = strTempFull.Substring(posOpenParenthese + 1, strTempFull.Length - 2 - posOpenParenthese);
                    strParams = strParams.Trim('(');
                    strParams = strParams.Trim(')');
                    strParams = strParams.Trim(' ');
                    string[] strParamList = strParams.Split(',');
                    if (strParamList.Length != 3)
                    {
                        LogEvent logEvent = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format("Math function invalid parameters count"));
                        AddLogEvent(logEvent);
                        return;
                    }
                    // on parse les données
                    string ResultSymbol = strParamList[0].Trim();
                    string OP1Symbol = strParamList[1].Trim();
                    string OP2Symbol = strParamList[2].Trim();
                    bool bOP1IsNumeric = false;
                    bool bOP2IsNumeric = false;
                    bOP1IsNumeric = ScriptParser.IsNumericValue(OP1Symbol);
                    bOP2IsNumeric = ScriptParser.IsNumericValue(OP2Symbol);
                    Data ResultData = (Data)m_Document.GestData.GetFromSymbol(ResultSymbol);
                    Data Operator1Data = null;
                    if (!bOP1IsNumeric)
                        Operator1Data = (Data)m_Document.GestData.GetFromSymbol(OP1Symbol);
                    else
                    {
                        Operator1Data = new Data("TEMP_MATHS_DATA1", int.Parse(OP1Symbol), DATA_SIZE.DATA_SIZE_32B, false);
                        Operator1Data.InitVal();
                    }

                    Data Operator2Data = null;
                    if (!bOP2IsNumeric)
                        Operator2Data = (Data)m_Document.GestData.GetFromSymbol(OP2Symbol);
                    else
                    {
                        Operator2Data = new Data("TEMP_MATHS_DATA2", int.Parse(OP2Symbol), DATA_SIZE.DATA_SIZE_32B, false);
                        Operator2Data.InitVal();
                    }

                    if (ResultData == null)
                    {
                        LogEvent logEvent = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format("Unknown Data {0}", ResultSymbol));
                        AddLogEvent(logEvent);
                        return;
                    }
                    if (Operator1Data == null && !bOP1IsNumeric)
                    {
                        LogEvent logEvent = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format("Unknown Data {0}", OP1Symbol));
                        AddLogEvent(logEvent);
                        return;
                    }
                    if (Operator2Data == null && !bOP2IsNumeric)
                    {
                        LogEvent logEvent = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format("Unknown Data {0}", OP2Symbol));
                        AddLogEvent(logEvent);
                        return;
                    }
                    switch (SecondTokenType)
                    {
                        case MATHS_FUNC.ADD:
                            ExecuteMathAdd(ResultData, Operator1Data, Operator2Data);
                            break;
                        case MATHS_FUNC.SUB:
                            ExecuteMathSub(ResultData, Operator1Data, Operator2Data);
                            break;
                        case MATHS_FUNC.MUL:
                            ExecuteMathMul(ResultData, Operator1Data, Operator2Data);
                            break;
                        case MATHS_FUNC.DIV:
                            ExecuteMathDiv(ResultData, Operator1Data, Operator2Data);
                            break;
                    }
                }
                else
                {
                    LogEvent logEvent = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format("Invalid Math function"));
                    AddLogEvent(logEvent);
                    return;
                }
            }
        }

        //*****************************************************************************************************
        protected void ExecuteMathAdd(Data Result, Data Operator1, Data Operator2)
        {
            Result.Value = Operator1.Value + Operator2.Value;
        }

        //*****************************************************************************************************
        protected void ExecuteMathSub(Data Result, Data Operator1, Data Operator2)
        {
            Result.Value = Operator1.Value - Operator2.Value;
        }

        //*****************************************************************************************************
        protected void ExecuteMathMul(Data Result, Data Operator1, Data Operator2)
        {
            Result.Value = Operator1.Value * Operator2.Value;
        }

        //*****************************************************************************************************
        protected void ExecuteMathDiv(Data Result, Data Operator1, Data Operator2)
        {
            if (Operator2.Value != 0)
            {
                Result.Value = Operator1.Value / Operator2.Value;
            }
            else
            {
                LogEvent logEvent = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format("Division by zero forbidden"));
                AddLogEvent(logEvent);
                return;
            }
        }
        //*****************************************************************************************************

        #endregion

        public void AddLogEvent(LogEvent Event)
        {
            if (EventAddLogEvent != null)
            {
                EventAddLogEvent(Event);
            }
        }



    }
}

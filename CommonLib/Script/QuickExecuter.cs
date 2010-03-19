using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Windows.Forms;

namespace CommonLib
{
    /// <summary>
    /// classe contenant les informations direct pré parsé pour accélerer l'execution 
    /// </summary>
    internal class PreParsedLine
    {
        // doit toujours être définit
        public SCR_OBJECT m_SrcObject = SCR_OBJECT.INVALID;
        // la fonction qui va être executé (reste FUNC invalide dans le cas d'appel aux fonction
        // car avec le SRC_OBJECT on sait déja que c'est une fonction)
        public ALL_FUNC m_FunctionToExec = ALL_FUNC.INVALID;
        // est définit pour les fonctions ayant des arguments (Maths et Logic) sous forme de tableau à plusieurs index
        // ou alors sous forme de tableau à index unique (0) pour les autres types
        public BaseObject[] m_Arguments = null;

    }

    public class QuickExecuter
    {
        private delegate void ScriptAddedToExecute();

        #region données membres
        BTDoc m_Document = null;
        static bool m_bIsWaiting = false;
        static int nbinstanceexecuter = 0;
        Queue<int> m_PileScriptsToExecute = new Queue<int>();

        PreParser m_PreParser = new PreParser();
        Dictionary<int ,List<PreParsedLine> > m_DictioQuickScripts = new Dictionary<int ,List<PreParsedLine>>();
        bool m_bPreParsedDone = false;

        private int m_iQuickIdCounter = 1;
        #endregion

        #region events
        private event ScriptAddedToExecute EvScriptToExecute;
        public event AddLogEventDelegate EventAddLogEvent;
        #endregion

        #region cosntructeur / destructeur
        public QuickExecuter()
        {
            EvScriptToExecute += new ScriptAddedToExecute(ScriptExecuter_EvScriptToExecute);
            nbinstanceexecuter++;
            m_PreParser.EventAddLogEvent += new AddLogEventDelegate(AddLogEvent);
        }

        ~QuickExecuter()
        {
            nbinstanceexecuter--;
        }
        #endregion

        public bool PreParsedDone
        {
            get
            {
                return m_bPreParsedDone;                
            }
            set
            {
                m_bPreParsedDone = value;
            }
        }
        #region fonction diverses
        public void AddLogEvent(LogEvent Event)
        {
            if (EventAddLogEvent != null)
            {
                EventAddLogEvent(Event);
            }
        }
    
        public int PreParseScript(IScriptable scriptable)
        {
            int Id = 0;
            List<PreParsedLine> preParsedScript = m_PreParser.PreParseScript(scriptable.ScriptLines);
            if (preParsedScript != null)
            {
                Id = ++m_iQuickIdCounter;
                m_DictioQuickScripts.Add(Id, preParsedScript);
            }
            return Id;
        }
    
        public int PreParseScript(String[] script)
        {
            int Id = 0;
            List<PreParsedLine> preParsedScript = m_PreParser.PreParseScript(script);
            if (preParsedScript != null)
            {
                Id = ++m_iQuickIdCounter;
                m_DictioQuickScripts.Add(Id, preParsedScript);
            }
            return Id;
        }

        public int PreParseScript(IInitScriptable scriptable)
        {
            int Id = 0;
            List<PreParsedLine> preParsedScript = m_PreParser.PreParseScript(scriptable.InitScriptLines);
            if (preParsedScript != null)
            {
                Id = ++m_iQuickIdCounter;
                m_DictioQuickScripts.Add(Id, preParsedScript);
            }
            return Id;
        }
    
        #endregion
        
        #region attributs
        public BTDoc Document
        {
            get
            {
                return m_Document;
            }
            set
            {
                m_Document = value;
                m_PreParser.Document = value;
            }
        }
        #endregion        

        #region execution
        internal void ScriptExecuter_EvScriptToExecute()
        {
            CommonLib.PerfChrono theChrono = new PerfChrono();
            while (m_PileScriptsToExecute.Count != 0)
            {
                // on prend le script sans l'enlever afin de savoir qu'il n'est pas encore executé
                int QuickId = m_PileScriptsToExecute.Peek();
                if (m_bIsWaiting)
                    System.Diagnostics.Debug.Assert(false, "appel en trop");

                InternalExecuteScript(QuickId);
                // il est éxécuté, on l'enlève de la liste.
                m_PileScriptsToExecute.Dequeue();
            }
            theChrono.EndMeasure("ScriptExecuter");
        }

        public void ExecuteScript(int QuickID)
        {
            if (m_DictioQuickScripts.ContainsKey(QuickID))
            {
                m_PileScriptsToExecute.Enqueue(QuickID);
                if (m_PileScriptsToExecute.Count > 1)
                    return;
                if (EvScriptToExecute != null)
                    EvScriptToExecute();
            }
#if DEBUG
            else
            {
                // l'ID 0 est l'ID script vide, on ne le trace pas
                if (QuickID != 0)
                {
                    LogEvent log = new LogEvent(LOG_EVENT_TYPE.WARNING, string.Format(Lang.LangSys.C("Failed to find {0}"), QuickID)); 
                    AddLogEvent(log);
                }
            }
#endif
        }


        internal void InternalExecuteScript(int QuickID)
        {
            List<PreParsedLine> QuickScript = m_DictioQuickScripts[QuickID];
            for (int i = 0; i < QuickScript.Count; i++)
            {
                CommonLib.PerfChrono theChrono = new PerfChrono();
                switch (QuickScript[i].m_SrcObject)
                {
                    case SCR_OBJECT.FRAMES:
                    case SCR_OBJECT.LOGGERS:
                    case SCR_OBJECT.TIMERS:
                    case SCR_OBJECT.SCREEN:
                        QuickExecuteFunc(QuickScript[i]);
                        break;
                    case SCR_OBJECT.FUNCTIONS:
                        int Id = QuickScript[i].m_Arguments[0].QuickScriptID;
                        if (Traces.IsDebugAndCatOK(TraceCat.ExecuteFunc))
                            Traces.LogAddDebug(TraceCat.ExecuteFunc, 
                                               string.Format("Executing func {0}", QuickScript[i].m_Arguments[0].Symbol));
                        InternalExecuteScript(Id);  
                        break;
                    case SCR_OBJECT.LOGIC:
                        QuickExecuteLogic(QuickScript[i]);
                        break;
                    case SCR_OBJECT.MATHS:
                        QuickExecuteMaths(QuickScript[i]);
                        break;
                    case SCR_OBJECT.INVALID:
                    default:
                        break;
                }
                theChrono.EndMeasure("EndMesureInternalExecute " + QuickScript[i].m_SrcObject.ToString() + " " + QuickScript[i].m_FunctionToExec.ToString());
            }
        }

        internal void QuickExecuteFunc(PreParsedLine QuickScript)
        {
            switch (QuickScript.m_FunctionToExec)
            {
                case ALL_FUNC.FRAME_RECEIVE:
                    QuickExecuteRecieveFrame((Trame)QuickScript.m_Arguments[0]);
                    break;
                case ALL_FUNC.FRAME_SEND:
                    QuickExecuteSendFrame((Trame)QuickScript.m_Arguments[0]);
                    break;
                case ALL_FUNC.LOGGER_CLEAR:
                    if (Traces.IsDebugAndCatOK(TraceCat.ExecuteLogger))
                        Traces.LogAddDebug(TraceCat.ExecuteLogger, 
                                           string.Format("Logger Clear {0}", QuickScript.m_Arguments[0].Symbol));
                    ((Logger)QuickScript.m_Arguments[0]).ClearLog();
                    break;
                case ALL_FUNC.LOGGER_LOG:
                    if (Traces.IsDebugAndCatOK(TraceCat.ExecuteLogger))
                        Traces.LogAddDebug(TraceCat.ExecuteLogger, 
                                           string.Format("Logger Log {0}", QuickScript.m_Arguments[0].Symbol));
                    ((Logger)QuickScript.m_Arguments[0]).LogData();
                    break;
                case ALL_FUNC.LOGGER_START:
                    if (Traces.IsDebugAndCatOK(TraceCat.ExecuteLogger))
                        Traces.LogAddDebug(TraceCat.ExecuteLogger, 
                                           string.Format("Logger.Start {0}", QuickScript.m_Arguments[0].Symbol));
                    ((Logger)QuickScript.m_Arguments[0]).StartAutoLogger();
                    break;
                case ALL_FUNC.LOGGER_STOP:
                    if (Traces.IsDebugAndCatOK(TraceCat.ExecuteLogger))
                        Traces.LogAddDebug(TraceCat.ExecuteLogger, 
                                           string.Format("Logger.STOP {0}", QuickScript.m_Arguments[0].Symbol));
                    ((Logger)QuickScript.m_Arguments[0]).StopAutoLogger();
                    break;
                case ALL_FUNC.TIMER_START:
                    if (Traces.IsDebugAndCatOK(TraceCat.ExecuteTimer))
                        Traces.LogAddDebug(TraceCat.ExecuteTimer, 
                                           string.Format("timer.START {0}", QuickScript.m_Arguments[0].Symbol));
                    ((BTTimer)QuickScript.m_Arguments[0]).StartTimer();
                    break;
                case ALL_FUNC.TIMER_STOP:
                    if (Traces.IsDebugAndCatOK(TraceCat.ExecuteTimer))
                        Traces.LogAddDebug(TraceCat.ExecuteTimer, 
                                           string.Format("timer.STOP {0}", QuickScript.m_Arguments[0].Symbol));
                    ((BTTimer)QuickScript.m_Arguments[0]).StopTimer();
                    break;
                case ALL_FUNC.SCREEN_SHOW_ON_TOP:
                    if (Traces.IsDebugAndCatOK(TraceCat.ExecuteScreen))
                        Traces.LogAddDebug(TraceCat.ExecuteScreen, 
                                           string.Format("Screen.SHOW_ON_TOP {0}", QuickScript.m_Arguments[0].Symbol));
                    ((BTScreen)QuickScript.m_Arguments[0]).ShowScreenToTop();
                    break;
            }
        }
        #endregion

        #region execution des trames
        internal void QuickExecuteSendFrame(Trame tr)
        {
            if (Traces.IsDebugAndCatOK(TraceCat.ExecuteFrame))
                Traces.LogAddDebug(TraceCat.ExecuteFrame, string.Format("Send frame {0}", tr.Symbol));
                
            Byte[] buffer = tr.CreateTrameToSend(false);
            if (m_Document.m_Comm.IsOpen)
            {
                if (m_Document.m_Comm.CommType == TYPE_COMM.VIRTUAL)
                    m_Document.m_Comm.SendData(tr, m_Document.GestData, m_Document.GestDataVirtual);
                else
                    m_Document.m_Comm.SendData(buffer);
            }
            else
            {
                LogEvent log = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format(Lang.LangSys.C("Trying sending frame {0} while connection is closed"), tr.Symbol));
                AddLogEvent(log);
            }
        }

        internal void QuickExecuteRecieveFrame(Trame TrameToRecieve)
        {
            if (Traces.IsDebugAndCatOK(TraceCat.ExecuteFrame))
                Traces.LogAddDebug(TraceCat.ExecuteFrame, string.Format("Recieve frame {0}", TrameToRecieve.Symbol));
                
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
                    string strmess = string.Format(Lang.LangSys.C("Message {0} have not been recieved (Timeout)"), TrameToRecieve.Symbol);
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
                        strmess = string.Format(Lang.LangSys.C("Error reading message {0} (Recieved frame is not the one expected)"), TrameToRecieve.Symbol);
                    else
                        strmess = string.Format(Lang.LangSys.C("Error reading message {0}"), TrameToRecieve.Symbol);

                    LogEvent log = new LogEvent(LOG_EVENT_TYPE.ERROR, strmess);
                    AddLogEvent(log);
                }
            }
        }
        #endregion

        #region fonction maths
        internal void QuickExecuteMaths(PreParsedLine QuickScript)
        {
            Data ResultData = (Data)QuickScript.m_Arguments[0];
            Data Operator1Data = (Data)QuickScript.m_Arguments[1];
            Data Operator2Data = (Data)QuickScript.m_Arguments[2];
            switch (QuickScript.m_FunctionToExec)
            {
                case ALL_FUNC.MATHS_ADD:
                    ExecuteMathAdd(ResultData, Operator1Data, Operator2Data);
                    break;
                case ALL_FUNC.MATHS_DIV:
                    ExecuteMathDiv(ResultData, Operator1Data, Operator2Data);
                    break;
                case ALL_FUNC.MATHS_MUL:
                    ExecuteMathMul(ResultData, Operator1Data, Operator2Data);
                    break;
                case ALL_FUNC.MATHS_SUB:
                    ExecuteMathSub(ResultData, Operator1Data, Operator2Data);
                    break;
            }
        }

        //*****************************************************************************************************
        internal void ExecuteMathAdd(Data Result, Data Operator1, Data Operator2)
        {
            string opsValues = string.Empty;
            if (Traces.IsDebugAndCatOK(TraceCat.ExecuteMath))
                   opsValues = string.Format("{0} + {1}", Operator1.Value, Operator2.Value);
            Result.Value = Operator1.Value + Operator2.Value;
            if (Traces.IsDebugAndCatOK(TraceCat.ExecuteMath))
                Traces.LogAddDebug(TraceCat.ExecuteMath, "Math.ADD", string.Format("{0} = {1}", Result.Value, opsValues));
        }

        //*****************************************************************************************************
        internal void ExecuteMathSub(Data Result, Data Operator1, Data Operator2)
        {
            string opsValues = string.Empty;
            if (Traces.IsDebugAndCatOK(TraceCat.ExecuteMath))
                   opsValues = string.Format("{0} - {1}", Operator1.Value, Operator2.Value);
            Result.Value = Operator1.Value - Operator2.Value;
            if (Traces.IsDebugAndCatOK(TraceCat.ExecuteMath))
                Traces.LogAddDebug(TraceCat.ExecuteMath, "Math.SUB", string.Format("{0} = {1}", Result.Value, opsValues));
        }

        //*****************************************************************************************************
        protected void ExecuteMathMul(Data Result, Data Operator1, Data Operator2)
        {
            string opsValues = string.Empty;
            if (Traces.IsDebugAndCatOK(TraceCat.ExecuteMath))
                   opsValues = string.Format("{0} * {1}", Operator1.Value, Operator2.Value);
            Result.Value = Operator1.Value * Operator2.Value;
            if (Traces.IsDebugAndCatOK(TraceCat.ExecuteMath))
                Traces.LogAddDebug(TraceCat.ExecuteMath, "Math.MUL", string.Format("{0} = {1}", Result.Value, opsValues));
        }

        //*****************************************************************************************************
        internal void ExecuteMathDiv(Data Result, Data Operator1, Data Operator2)
        {
            if (Operator2.Value != 0)
            {
                string opsValues = string.Empty;
                if (Traces.IsDebugAndCatOK(TraceCat.ExecuteMath))
                       opsValues = string.Format("{0} / {1}", Operator1.Value, Operator2.Value);
                Result.Value = Operator1.Value / Operator2.Value;
                if (Traces.IsDebugAndCatOK(TraceCat.ExecuteMath))
                    Traces.LogAddDebug(TraceCat.ExecuteMath, "Math.DIV", string.Format("{0} = {1} ", Result.Value, opsValues));
            }
            else
            {
                LogEvent logEvent = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format(Lang.LangSys.C("Division by zero forbidden")));
                AddLogEvent(logEvent);
                if (Traces.IsDebugAndCatOK(TraceCat.ExecuteMath))
                    Traces.LogAddDebug(TraceCat.ExecuteMath, "Math.DIV", "Division by zero");
                return;
            }
        }
        //*****************************************************************************************************
        #endregion

        #region fonction logiques
        internal void QuickExecuteLogic(PreParsedLine QuickScript)
        {
            Data[] Arguments = new Data[QuickScript.m_Arguments.Length-1];
            Data ResultData = (Data)QuickScript.m_Arguments[0];
            for (int i = 1; i < QuickScript.m_Arguments.Length; i++)
            {
                Arguments[i - 1] = (Data)QuickScript.m_Arguments[i];
            }
            switch (QuickScript.m_FunctionToExec)
            {
                case ALL_FUNC.LOGIC_NOT:
                    ExecuteLogicNOT(ResultData, Arguments[0]);
                    break;
                case ALL_FUNC.LOGIC_AND:
                    ExecuteLogicAND(ResultData, Arguments);
                    break;
                case ALL_FUNC.LOGIC_OR:
                    ExecuteLogicOR(ResultData, Arguments);
                    break;
                case ALL_FUNC.LOGIC_NAND:
                    ExecuteLogicNAND(ResultData, Arguments);
                    break;
                case ALL_FUNC.LOGIC_NOR:
                    ExecuteLogicNOR(ResultData, Arguments);
                    break;
                case ALL_FUNC.LOGIC_XOR:
                    ExecuteLogicXOR(ResultData, Arguments);
                    break;
            }
        }

        internal void ExecuteLogicNOT(Data Result, Data Operator1)
        {
            if (Traces.IsDebugAndCatOK(TraceCat.ExecuteLogic))
                Traces.LogAddDebug(TraceCat.ExecuteLogic, "Logic.NOT", string.Format("{0} = !{1}", Result.Value, Operator1.Value));
            if (Operator1.Value == 0)
                Result.Value = 1;
            else
                Result.Value = 0;
        }

        //*****************************************************************************************************
        internal void ExecuteLogicAND(Data Result, Data[] Operators)
        {
            if (Traces.IsDebugAndCatOK(TraceCat.ExecuteLogic))
            {
                Traces.LogAddDebug(TraceCat.ExecuteLogic, 
                                   "Logic.AND", 
                                   string.Format("{0} = {1} & {2} & {3} & {4}", 
                                                 Result.Value, 
                                                 Operators[0] != null? Operators[0].Value.ToString() : "NA",
                                                 Operators[1] != null? Operators[1].Value.ToString() : "NA",
                                                 Operators.Length >= 3? Operators[2].Value.ToString() : "NA",
                                                 Operators.Length >= 4? Operators[3].Value.ToString() : "NA")
                                   );
            }
            bool bRes = true;
            for (int i = 0; i < Operators.Length; i++)
            {
                bRes &= Operators[i].Value != 0;
            }
            Result.Value = bRes ? 1 : 0;
        }

        //*****************************************************************************************************
        internal void ExecuteLogicOR(Data Result, Data[] Operators)
        {
            if (Traces.IsDebugAndCatOK(TraceCat.ExecuteLogic))
            {
                Traces.LogAddDebug(TraceCat.ExecuteLogic, 
                                   "Logic.OR", 
                                   string.Format("{0} = {1} | {2} | {3} | {4}", 
                                                 Result.Value, 
                                                 Operators[0] != null? Operators[0].Value.ToString() : "NA",
                                                 Operators[1] != null? Operators[1].Value.ToString() : "NA",
                                                 Operators.Length >= 3? Operators[2].Value.ToString() : "NA",
                                                 Operators.Length >= 4? Operators[3].Value.ToString() : "NA")
                                   );
            }            
            bool bRes = false;
            for (int i = 0; i < Operators.Length; i++)
            {
                bRes |= Operators[i].Value != 0;
            }
            Result.Value = bRes ? 1 : 0;
        }

        //*****************************************************************************************************
        internal void ExecuteLogicNAND(Data Result, Data[] Operators)
        {
            if (Traces.IsDebugAndCatOK(TraceCat.ExecuteLogic))
            {
                Traces.LogAddDebug(TraceCat.ExecuteLogic, 
                                   "Logic.NAND", 
                                   string.Format("{0} = !({1} & {2} & {3} & {4})", 
                                                 Result.Value, 
                                                 Operators[0] != null? Operators[0].Value.ToString() : "NA",
                                                 Operators[1] != null? Operators[1].Value.ToString() : "NA",
                                                 Operators.Length >= 3? Operators[2].Value.ToString() : "NA",
                                                 Operators.Length >= 4? Operators[3].Value.ToString() : "NA")
                                   );
            }
            bool bRes = true;
            for (int i = 0; i < Operators.Length; i++)
            {
                bRes &= Operators[i].Value != 0;
            }
            Result.Value = bRes ? 0 : 1;
        }

        //*****************************************************************************************************
        internal void ExecuteLogicNOR(Data Result, Data[] Operators)
        {
            if (Traces.IsDebugAndCatOK(TraceCat.ExecuteLogic))
            {
                Traces.LogAddDebug(TraceCat.ExecuteLogic, 
                                   "Logic.NOR", 
                                   string.Format("{0} = !({1} | {2} | {3} | {4})", 
                                                 Result.Value, 
                                                 Operators[0] != null? Operators[0].Value.ToString() : "NA",
                                                 Operators[1] != null? Operators[1].Value.ToString() : "NA",
                                                 Operators.Length >= 3? Operators[2].Value.ToString() : "NA",
                                                 Operators.Length >= 4? Operators[3].Value.ToString() : "NA")
                                   );
            }            
            bool bRes = false;
            for (int i = 0; i < Operators.Length; i++)
            {
                bRes |= Operators[i].Value != 0;
            }
            Result.Value = bRes ? 0 : 1;
        }

        internal void ExecuteLogicXOR(Data Result, Data[] Operators)
        {
            if (Traces.IsDebugAndCatOK(TraceCat.ExecuteLogic))
            {
                Traces.LogAddDebug(TraceCat.ExecuteLogic, 
                                   "Logic.XOR", 
                                   string.Format("{0} = {1} XOR {2}", 
                                                 Result.Value, 
                                                 Operators[0] != null? Operators[0].Value.ToString() : "NA",
                                                 Operators[1] != null? Operators[1].Value.ToString() : "NA")
                                   );
            }            
            bool bRes = false;
            if ((Operators[0].Value == 0 && Operators[1].Value == 1)
                || (Operators[0].Value == 1 && Operators[1].Value == 0)
                )
                bRes = true;
            Result.Value = bRes ? 1 : 0;
        }
        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Windows.Forms;
using System.Threading;

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
        // argument de type divers, peut être utilisé pour passer des chemin de fichier ou n'importe quoi d'autres que
        // des éléments héristant de base Object
        public object m_objArguments = null;
    }

    public delegate void FrameRecievedDelegate(Trame frame);

    public class QuickExecuter
    {
        //private delegate void ScriptAddedToExecute();

        #region données membres
        BTDoc m_Document = null;
        static int nbinstanceexecuter = 0;
        Queue<int> m_PileScriptsToExecute = new Queue<int>();

        PreParser m_PreParser = new PreParser();
        Dictionary<int, List<PreParsedLine>> m_DictioQuickScripts = new Dictionary<int, List<PreParsedLine>>();
        bool m_bPreParsedDone = false;

        private int m_iQuickIdCounter = 1;

        private Thread m_ExecutionThread;

        private bool m_bStopRequested = false;

        private Mutex m_QueueMutex = new Mutex();
        #endregion

        #region events
        //private event ScriptAddedToExecute EvScriptToExecute;
        public event AddLogEventDelegate EventAddLogEvent;
        public event FrameRecievedDelegate EventFrameRecieved;
        #endregion

        #region cosntructeur / destructeur
        /// <summary>
        /// constructeur par défaut de la classe
        /// </summary>
        public QuickExecuter()
        {
            //EvScriptToExecute += new ScriptAddedToExecute(ScriptExecuter_EvScriptToExecute);
            nbinstanceexecuter++;
            m_PreParser.EventAddLogEvent += new AddLogEventDelegate(AddLogEvent);
            
        }

        /// <summary>
        /// destructeur
        /// </summary>
        ~QuickExecuter()
        {
            nbinstanceexecuter--;
        }
        #endregion

        /// <summary>
        /// traite les message d'application
        /// </summary>
        /// <param name="Mess">Type de message</param>
        /// <param name="Param">paramètres du message</param>
        /// <param name="TypeApp">type d'application en cours</param>
        public void TraiteMessage(MESSAGE Mess, object Param, TYPE_APP TypeApp)
        {
            switch (Mess)
            {
                // lors du RUN on démarre le thread de traitement de la pile des scripts
                case MESSAGE.MESS_CMD_RUN:
                    m_bStopRequested = false;
                    if (m_ExecutionThread != null && m_ExecutionThread.IsAlive)
                    {
                        if (Traces.IsDebugAndCatOK(TraceCat.Executer))
                            Traces.LogAddDebug(TraceCat.Executer,
                                               string.Format("Excuter Thread still running "));
                    }
                    else
                    {
                        m_ExecutionThread = new Thread(ThreadScriptExecution);
                        m_ExecutionThread.Start();
                    }
                    break;
                // lors du STOP, on demande au thread de s'arrêter
                case MESSAGE.MESS_CMD_STOP:
                    m_bStopRequested = true;
                    break;
            }
        }

        /// <summary>
        /// indique si le pré parsing est fait
        /// </summary>
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
        /// <summary>
        /// Ajout un évènement de log
        /// </summary>
        /// <param name="Event"></param>
        public void AddLogEvent(LogEvent Event)
        {
            if (EventAddLogEvent != null)
            {
                EventAddLogEvent(Event);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="script"></param>
        /// <returns></returns>
        public int PreParseScript(string[] script)
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
        /// <summary>
        /// thread d'éxecution du script
        /// </summary>
        internal void ThreadScriptExecution()
        {
            do
            {
                CommonLib.PerfChrono theChrono = new PerfChrono();
                while (m_PileScriptsToExecute.Count != 0 && !m_bStopRequested)
                {
                    // on prend le script sans l'enlever afin de savoir qu'il n'est pas encore executé
                    //m_QueueMutex.WaitOne();
                    Object thisLock = new Object();
                    int QuickId = 0;
                    lock (thisLock)
                    {
                        QuickId = m_PileScriptsToExecute.Peek();
                    }
                    //if (m_bIsWaiting)
                    //    System.Diagnostics.Debug.Assert(false, "appel en trop");

                    InternalExecuteScript(QuickId);
                    // il est éxécuté, on l'enlève de la liste.
                    m_PileScriptsToExecute.Dequeue();
                    //m_QueueMutex.ReleaseMutex();
                    Thread.Sleep(20);
                }
                theChrono.EndMeasure("ScriptExecuter");
                Thread.Sleep(50);
            } while (!m_bStopRequested);
            m_bStopRequested = false;
        }

        /// <summary>
        /// ajoute le script dont l'id est passé en paramètre à la fifo de script.
        /// </summary>
        /// <param name="QuickID"></param>
        public void ExecuteScript(int QuickID)
        {
            if (m_DictioQuickScripts.ContainsKey(QuickID))
            {
                Object thisLock = new Object();
                lock (thisLock)
                {
                    // Critical code section
                    m_PileScriptsToExecute.Enqueue(QuickID);
                } 
                //m_QueueMutex.ReleaseMutex();
                //if (EvScriptToExecute != null)
                    //EvScriptToExecute();
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

        /// <summary>
        /// execute le script dont l'ID est passé en paramètre
        /// </summary>
        /// <param name="QuickID"></param>
        internal void InternalExecuteScript(int QuickID)
        {
            List<PreParsedLine> QuickScript = m_DictioQuickScripts[QuickID];
            for (int i = 0; i < QuickScript.Count; i++)
            {
                CommonLib.PerfChrono theChrono = new PerfChrono();
                switch (QuickScript[i].m_SrcObject)
                {
                    case SCR_OBJECT.FRAMES:
                    case SCR_OBJECT.TIMERS:
                        QuickExecuteFunc(QuickScript[i]);
                        break;
                    case SCR_OBJECT.SCREEN:
                        QuickExecuteScreen(QuickScript[i]);
                        break;
                    case SCR_OBJECT.LOGGERS:
                        QuickExecuteLogger(QuickScript[i]);
                        break;
                    case SCR_OBJECT.FUNCTIONS:
                        int Id = QuickScript[i].m_Arguments[0].QuickScriptID;
                        if (Traces.IsDebugAndCatOK(TraceCat.ExecuteFunc))
                            Traces.LogAddDebug(TraceCat.ExecuteFunc,
                                               string.Format("Executing func {0}", QuickScript[i].m_Arguments[0].Symbol));
                        if (Id != 0) //un ID a 0 signifie que le script est vide
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

        /// <summary>
        /// Execute une FUNCTION
        /// </summary>
        /// <param name="QuickScript"></param>
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
            }
        }
        #endregion

        #region execution des trames
        /// <summary>
        /// Execute l'envoie d'une trame
        /// </summary>
        /// <param name="tr"></param>
        internal void QuickExecuteSendFrame(Trame tr)
        {
            if (Traces.IsDebugAndCatOK(TraceCat.ExecuteFrame))
                Traces.LogAddDebug(TraceCat.ExecuteFrame, string.Format("Send frame {0}", tr.Symbol));

            Byte[] buffer = tr.CreateTrameToSend(false);
            if (m_Document.Communication.IsOpen)
            {
                if (m_Document.Communication.CommType == TYPE_COMM.VIRTUAL)
                    m_Document.Communication.SendData(tr, m_Document.GestData, m_Document.GestDataVirtual);
                else
                {
                    string strmess = string.Format(Lang.LangSys.C("Frame {0} sent"), tr.Symbol);
                    if (Traces.IsDebugAndCatOK(TraceCat.ExecuteFrame))
                    {
                        string strSendData = string.Empty;
                        for (int i = 0; i < buffer.Length; i++)
                        {
                            strSendData += string.Format(" {0:x2}", buffer[i]);
                        }
                        Traces.LogAddDebug(TraceCat.ExecuteFrame, string.Format("Frame datas {0}", strSendData));
                    }
                    m_Document.Communication.SendData(buffer);
                }
            }
            else
            {
                LogEvent log = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format(Lang.LangSys.C("Trying sending frame {0} while connection is closed"), tr.Symbol));
                AddLogEvent(log);
            }
        }

        /// <summary>
        /// Execute la récéption d'une trame
        /// </summary>
        /// <param name="TrameToRecieve"></param>
        internal void QuickExecuteRecieveFrame(Trame TrameToRecieve)
        {
            if (Traces.IsDebugAndCatOK(TraceCat.ExecuteFrame))
                Traces.LogAddDebug(TraceCat.ExecuteFrame, string.Format("Recieve frame {0}", TrameToRecieve.Symbol));

            if (m_Document.Communication.IsOpen)
            {
                byte[] FrameHeader = TrameToRecieve.FrameHeader;
                int ConvertedSize = TrameToRecieve.GetConvertedTrameSizeInByte();
                if (!m_Document.Communication.WaitTrameRecieved(ConvertedSize, FrameHeader))
                {
                    //indiquer qu'une trame n'a pas été recu
                    // et demander a l'utilisateur si il souhaite continuer l'execution des actions
                    // si il ne veux pas, remonter au parent qu'il doit arrèter les actions
                    //COMM_ERROR Err = m_Doc.m_Comm.ErrorCode;
                    string strmess = string.Format(Lang.LangSys.C("Message {0} have not been recieved (Timeout)"), TrameToRecieve.Symbol);
                    if (Traces.IsDebugAndCatOK(TraceCat.ExecuteFrame))
                    {
                        string strSendData = string.Empty;
                        Byte[] buffer2 = TrameToRecieve.CreateTrameToSend(false);
                        for (int i = 0; i < buffer2.Length; i++)
                        {
                            strSendData += string.Format(" {0:x2}", buffer2[i]);
                        }
                        Traces.LogAddDebug(TraceCat.ExecuteFrame, string.Format("Expected frame content {0}", strSendData));
                    }
                    LogEvent log = new LogEvent(LOG_EVENT_TYPE.ERROR, strmess);
                    AddLogEvent(log);
                    return;
                }
                byte[] buffer = null;
                if (m_Document.Communication.CommType == TYPE_COMM.VIRTUAL)
                    buffer = m_Document.Communication.GetRecievedData(ConvertedSize, TrameToRecieve);
                else
                    buffer = m_Document.Communication.GetRecievedData(ConvertedSize, FrameHeader);

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
                if (EventFrameRecieved != null)
                    EventFrameRecieved(TrameToRecieve);
            }
        }
        #endregion

        #region fonction maths
        /// <summary>
        /// Execute les fonction mathématiques
        /// </summary>
        /// <param name="QuickScript"></param>
        internal void QuickExecuteMaths(PreParsedLine QuickScript)
        {
            Data ResultData = (Data)QuickScript.m_Arguments[0];
            Data Operator1Data = (Data)QuickScript.m_Arguments[1];
            Data Operator2Data = QuickScript.m_Arguments.Length >=3 ? (Data)QuickScript.m_Arguments[2] : null;
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
                case ALL_FUNC.MATHS_COS:
                    ExecuteMathCos(ResultData, Operator1Data);
                    break;
                case ALL_FUNC.MATHS_SIN:
                    ExecuteMathSin(ResultData, Operator1Data);
                    break;
                case ALL_FUNC.MATHS_TAN:
                    ExecuteMathTan(ResultData, Operator1Data);
                    break;
                case ALL_FUNC.MATHS_SQRT:
                    ExecuteMathSqrt(ResultData, Operator1Data);
                    break;
                case ALL_FUNC.MATHS_POW:
                    ExecuteMathPow(ResultData, Operator1Data, Operator2Data);
                    break;
                case ALL_FUNC.MATHS_LN:
                    ExecuteMathLn(ResultData, Operator1Data);
                    break;
                case ALL_FUNC.MATHS_LOG:
                    ExecuteMathLog(ResultData, Operator1Data);
                    break;
                case ALL_FUNC.MATHS_SET:
                    ExecuteMathSet(ResultData, Operator1Data);
                    break;
                case ALL_FUNC.MATHS_MOD:
                    ExecuteMathMod(ResultData, Operator1Data, Operator2Data);
                    break;
            }
        }

        /// <summary>
        /// Execute une addition
        /// </summary>
        /// <param name="Result">donnée de sortie de l'opération</param>
        /// <param name="Operator1">premier opérateur de l'opération</param>
        /// <param name="Operator2">second opérateur de l'opération</param>
        internal void ExecuteMathAdd(Data Result, Data Operator1, Data Operator2)
        {
            string opsValues = string.Empty;
            if (Traces.IsDebugAndCatOK(TraceCat.ExecuteMath))
                opsValues = string.Format("{0} + {1}", Operator1.Value, Operator2.Value);
            Result.Value = Operator1.Value + Operator2.Value;
            if (Traces.IsDebugAndCatOK(TraceCat.ExecuteMath))
                Traces.LogAddDebug(TraceCat.ExecuteMath, "Math.ADD", string.Format("{0} = {1}", Result.Value, opsValues));
        }

        /// <summary>
        /// exécute une soustraction
        /// </summary>
        /// <param name="Result">donnée de sortie de l'opération</param>
        /// <param name="Operator1">premier opérateur de l'opération</param>
        /// <param name="Operator2">second opérateur de l'opération</param>
        internal void ExecuteMathSub(Data Result, Data Operator1, Data Operator2)
        {
            string opsValues = string.Empty;
            if (Traces.IsDebugAndCatOK(TraceCat.ExecuteMath))
                opsValues = string.Format("{0} - {1}", Operator1.Value, Operator2.Value);
            Result.Value = Operator1.Value - Operator2.Value;
            if (Traces.IsDebugAndCatOK(TraceCat.ExecuteMath))
                Traces.LogAddDebug(TraceCat.ExecuteMath, "Math.SUB", string.Format("{0} = {1}", Result.Value, opsValues));
        }

        /// <summary>
        /// exécute une multiplication
        /// </summary>
        /// <param name="Result">donnée de sortie de l'opération</param>
        /// <param name="Operator1">premier opérateur de l'opération</param>
        /// <param name="Operator2">second opérateur de l'opération</param>
        protected void ExecuteMathMul(Data Result, Data Operator1, Data Operator2)
        {
            string opsValues = string.Empty;
            if (Traces.IsDebugAndCatOK(TraceCat.ExecuteMath))
                opsValues = string.Format("{0} * {1}", Operator1.Value, Operator2.Value);
            Result.Value = Operator1.Value * Operator2.Value;
            if (Traces.IsDebugAndCatOK(TraceCat.ExecuteMath))
                Traces.LogAddDebug(TraceCat.ExecuteMath, "Math.MUL", string.Format("{0} = {1}", Result.Value, opsValues));
        }

        /// <summary>
        /// exécute une division
        /// </summary>
        /// <param name="Result">donnée de sortie de l'opération</param>
        /// <param name="Operator1">premier opérateur de l'opération</param>
        /// <param name="Operator2">second opérateur de l'opération</param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Result">donnée de sortie de l'opération</param>
        /// <param name="Operator1">premier opérateur de l'opération</param>
        protected void ExecuteMathSin(Data Result, Data Operator1)
        {
            string opsValues = string.Empty;
            if (Traces.IsDebugAndCatOK(TraceCat.ExecuteMath))
                opsValues = string.Format("Sin({0})", Operator1.Value);
            Result.Value = (int) (Math.Sin(Operator1.Value) * 100);
            if (Traces.IsDebugAndCatOK(TraceCat.ExecuteMath))
                Traces.LogAddDebug(TraceCat.ExecuteMath, "Math.SIN", string.Format("{0} = {1}", Result.Value, opsValues));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Result">donnée de sortie de l'opération</param>
        /// <param name="Operator1">premier opérateur de l'opération</param>
        protected void ExecuteMathCos(Data Result, Data Operator1)
        {
            string opsValues = string.Empty;
            if (Traces.IsDebugAndCatOK(TraceCat.ExecuteMath))
                opsValues = string.Format("Cos({0})", Operator1.Value);
            Result.Value = (int)(Math.Cos(Operator1.Value) * 100);
            if (Traces.IsDebugAndCatOK(TraceCat.ExecuteMath))
                Traces.LogAddDebug(TraceCat.ExecuteMath, "Math.SIN", string.Format("{0} = {1}", Result.Value, opsValues));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Result">donnée de sortie de l'opération</param>
        /// <param name="Operator1">premier opérateur de l'opération</param>
        protected void ExecuteMathTan(Data Result, Data Operator1)
        {
            string opsValues = string.Empty;
            if (Traces.IsDebugAndCatOK(TraceCat.ExecuteMath))
                opsValues = string.Format("Tan({0})", Operator1.Value);
            Result.Value = (int)(Math.Tan(Operator1.Value) * 100);
            if (Traces.IsDebugAndCatOK(TraceCat.ExecuteMath))
                Traces.LogAddDebug(TraceCat.ExecuteMath, "Math.TAN", string.Format("{0} = {1}", Result.Value, opsValues));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Result">donnée de sortie de l'opération</param>
        /// <param name="Operator1">premier opérateur de l'opération</param>
        /// <param name="Operator2">second opérateur de l'opération</param>
        protected void ExecuteMathPow(Data Result, Data Operator1, Data Operator2)
        {
            string opsValues = string.Empty;
            if (Traces.IsDebugAndCatOK(TraceCat.ExecuteMath))
                opsValues = string.Format("{0} ^ {1}", Operator1.Value, Operator2.Value);
            Result.Value = (int)(Math.Pow(Operator1.Value, Operator2.Value));
            if (Traces.IsDebugAndCatOK(TraceCat.ExecuteMath))
                Traces.LogAddDebug(TraceCat.ExecuteMath, "Math.POW", string.Format("{0} = {1}", Result.Value, opsValues));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Result">donnée de sortie de l'opération</param>
        /// <param name="Operator1">premier opérateur de l'opération</param>
        protected void ExecuteMathSqrt(Data Result, Data Operator1)
        {
            string opsValues = string.Empty;
            if (Traces.IsDebugAndCatOK(TraceCat.ExecuteMath))
                opsValues = string.Format("Sqrt({0})", Operator1.Value);
            Result.Value = (int)(Math.Sqrt(Operator1.Value));
            if (Traces.IsDebugAndCatOK(TraceCat.ExecuteMath))
                Traces.LogAddDebug(TraceCat.ExecuteMath, "Math.SQRT", string.Format("{0} = {1}", Result.Value, opsValues));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Result">donnée de sortie de l'opération</param>
        /// <param name="Operator1">premier opérateur de l'opération</param>
        protected void ExecuteMathLn(Data Result, Data Operator1)
        {
            string opsValues = string.Empty;
            if (Traces.IsDebugAndCatOK(TraceCat.ExecuteMath))
                opsValues = string.Format("Sqrt({0})", Operator1.Value);
            Result.Value = (int)(Math.Log(Operator1.Value)*100);
            if (Traces.IsDebugAndCatOK(TraceCat.ExecuteMath))
                Traces.LogAddDebug(TraceCat.ExecuteMath, "Math.SQRT", string.Format("{0} = {1}", Result.Value, opsValues));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Result">donnée de sortie de l'opération</param>
        /// <param name="Operator1">premier opérateur de l'opération</param>
        protected void ExecuteMathLog(Data Result, Data Operator1)
        {
            string opsValues = string.Empty;
            if (Traces.IsDebugAndCatOK(TraceCat.ExecuteMath))
                opsValues = string.Format("Sqrt({0})", Operator1.Value);
            Result.Value = (int)(Math.Log10(Operator1.Value));
            if (Traces.IsDebugAndCatOK(TraceCat.ExecuteMath))
                Traces.LogAddDebug(TraceCat.ExecuteMath, "Math.SQRT", string.Format("{0} = {1}", Result.Value, opsValues));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Result">donnée de sortie de l'opération</param>
        /// <param name="Operator1">premier opérateur de l'opération</param>
        protected void ExecuteMathSet(Data Result, Data Operator1)
        {
            string opsValues = string.Empty;
            if (Traces.IsDebugAndCatOK(TraceCat.ExecuteMath))
                opsValues = string.Format("Set({0})", Operator1.Value);
            Result.Value = Operator1.Value;
            if (Traces.IsDebugAndCatOK(TraceCat.ExecuteMath))
                Traces.LogAddDebug(TraceCat.ExecuteMath, "Math.SET", string.Format("{0} = {1}", Result.Value, opsValues));
        }

        /// <summary>
        /// exécute un un modulo
        /// </summary>
        /// <param name="Result">donnée de sortie de l'opération</param>
        /// <param name="Operator1">premier opérateur de l'opération</param>
        /// <param name="Operator2">second opérateur de l'opération</param>
        internal void ExecuteMathMod(Data Result, Data Operator1, Data Operator2)
        {
            if (Operator2.Value != 0)
            {
                string opsValues = string.Empty;
                if (Traces.IsDebugAndCatOK(TraceCat.ExecuteMath))
                    opsValues = string.Format("{0} % {1}", Operator1.Value, Operator2.Value);
                Result.Value = Operator1.Value % Operator2.Value;
                if (Traces.IsDebugAndCatOK(TraceCat.ExecuteMath))
                    Traces.LogAddDebug(TraceCat.ExecuteMath, "Math.MOD", string.Format("{0} = {1} ", Result.Value, opsValues));
            }
            else
            {
                LogEvent logEvent = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format(Lang.LangSys.C("Division by zero forbidden")));
                AddLogEvent(logEvent);
                if (Traces.IsDebugAndCatOK(TraceCat.ExecuteMath))
                    Traces.LogAddDebug(TraceCat.ExecuteMath, "Math.MOD", "Modulo by zero");
                return;
            }
        }
        #endregion

        #region fonction logiques
        /// <summary>
        /// 
        /// </summary>
        /// <param name="QuickScript"></param>
        internal void QuickExecuteLogic(PreParsedLine QuickScript)
        {
            Data[] Arguments = new Data[QuickScript.m_Arguments.Length - 1];
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="Operator1"></param>
        internal void ExecuteLogicNOT(Data Result, Data Operator1)
        {
            if (Traces.IsDebugAndCatOK(TraceCat.ExecuteLogic))
                Traces.LogAddDebug(TraceCat.ExecuteLogic, "Logic.NOT", string.Format("{0} = !{1}", Result.Value, Operator1.Value));
            if (Operator1.Value == 0)
                Result.Value = 1;
            else
                Result.Value = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="Operators"></param>
        internal void ExecuteLogicAND(Data Result, Data[] Operators)
        {
            if (Traces.IsDebugAndCatOK(TraceCat.ExecuteLogic))
            {
                Traces.LogAddDebug(TraceCat.ExecuteLogic,
                                   "Logic.AND",
                                   string.Format("{0} = {1} & {2} & {3} & {4}",
                                                 Result.Value,
                                                 Operators[0] != null ? Operators[0].Value.ToString() : "NA",
                                                 Operators[1] != null ? Operators[1].Value.ToString() : "NA",
                                                 Operators.Length >= 3 ? Operators[2].Value.ToString() : "NA",
                                                 Operators.Length >= 4 ? Operators[3].Value.ToString() : "NA")
                                   );
            }
            bool bRes = true;
            for (int i = 0; i < Operators.Length; i++)
            {
                bRes &= Operators[i].Value != 0;
            }
            Result.Value = bRes ? 1 : 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="Operators"></param>
        internal void ExecuteLogicOR(Data Result, Data[] Operators)
        {
            if (Traces.IsDebugAndCatOK(TraceCat.ExecuteLogic))
            {
                Traces.LogAddDebug(TraceCat.ExecuteLogic,
                                   "Logic.OR",
                                   string.Format("{0} = {1} | {2} | {3} | {4}",
                                                 Result.Value,
                                                 Operators[0] != null ? Operators[0].Value.ToString() : "NA",
                                                 Operators[1] != null ? Operators[1].Value.ToString() : "NA",
                                                 Operators.Length >= 3 ? Operators[2].Value.ToString() : "NA",
                                                 Operators.Length >= 4 ? Operators[3].Value.ToString() : "NA")
                                   );
            }
            bool bRes = false;
            for (int i = 0; i < Operators.Length; i++)
            {
                bRes |= Operators[i].Value != 0;
            }
            Result.Value = bRes ? 1 : 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="Operators"></param>
        internal void ExecuteLogicNAND(Data Result, Data[] Operators)
        {
            if (Traces.IsDebugAndCatOK(TraceCat.ExecuteLogic))
            {
                Traces.LogAddDebug(TraceCat.ExecuteLogic,
                                   "Logic.NAND",
                                   string.Format("{0} = !({1} & {2} & {3} & {4})",
                                                 Result.Value,
                                                 Operators[0] != null ? Operators[0].Value.ToString() : "NA",
                                                 Operators[1] != null ? Operators[1].Value.ToString() : "NA",
                                                 Operators.Length >= 3 ? Operators[2].Value.ToString() : "NA",
                                                 Operators.Length >= 4 ? Operators[3].Value.ToString() : "NA")
                                   );
            }
            bool bRes = true;
            for (int i = 0; i < Operators.Length; i++)
            {
                bRes &= Operators[i].Value != 0;
            }
            Result.Value = bRes ? 0 : 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="Operators"></param>
        internal void ExecuteLogicNOR(Data Result, Data[] Operators)
        {
            if (Traces.IsDebugAndCatOK(TraceCat.ExecuteLogic))
            {
                Traces.LogAddDebug(TraceCat.ExecuteLogic,
                                   "Logic.NOR",
                                   string.Format("{0} = !({1} | {2} | {3} | {4})",
                                                 Result.Value,
                                                 Operators[0] != null ? Operators[0].Value.ToString() : "NA",
                                                 Operators[1] != null ? Operators[1].Value.ToString() : "NA",
                                                 Operators.Length >= 3 ? Operators[2].Value.ToString() : "NA",
                                                 Operators.Length >= 4 ? Operators[3].Value.ToString() : "NA")
                                   );
            }
            bool bRes = false;
            for (int i = 0; i < Operators.Length; i++)
            {
                bRes |= Operators[i].Value != 0;
            }
            Result.Value = bRes ? 0 : 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="Operators"></param>
        internal void ExecuteLogicXOR(Data Result, Data[] Operators)
        {
            if (Traces.IsDebugAndCatOK(TraceCat.ExecuteLogic))
            {
                Traces.LogAddDebug(TraceCat.ExecuteLogic,
                                   "Logic.XOR",
                                   string.Format("{0} = {1} XOR {2}",
                                                 Result.Value,
                                                 Operators[0] != null ? Operators[0].Value.ToString() : "NA",
                                                 Operators[1] != null ? Operators[1].Value.ToString() : "NA")
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="QuickScript"></param>
        internal void QuickExecuteLogger(PreParsedLine QuickScript)
        {
            switch (QuickScript.m_FunctionToExec)
            {
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
                case ALL_FUNC.LOGGER_NEW_FILE:
                    if (Traces.IsDebugAndCatOK(TraceCat.ExecuteLogger))
                        Traces.LogAddDebug(TraceCat.ExecuteLogger,
                                           string.Format("Logger.NEW_FILE {0}", QuickScript.m_Arguments[0].Symbol));
                    ((Logger)QuickScript.m_Arguments[0]).NewFile();
                    break;
            }
        }

        internal void QuickExecuteScreen(PreParsedLine QuickScript)
        {
            switch (QuickScript.m_FunctionToExec)
            {
                case ALL_FUNC.SCREEN_HIDE:
                    if (Traces.IsDebugAndCatOK(TraceCat.ExecuteScreen))
                        Traces.LogAddDebug(TraceCat.ExecuteScreen,
                                           string.Format("Screen Show {0}", QuickScript.m_Arguments[0].Symbol));
                    ((BTScreen)QuickScript.m_Arguments[0]).ExecuteHide();
                    break;
                case ALL_FUNC.SCREEN_SHOW:
                    if (Traces.IsDebugAndCatOK(TraceCat.ExecuteScreen))
                        Traces.LogAddDebug(TraceCat.ExecuteScreen,
                                           string.Format("Screen Hide {0}", QuickScript.m_Arguments[0].Symbol));
                    ((BTScreen)QuickScript.m_Arguments[0]).ExecuteShow();
                    break;
                case ALL_FUNC.SCREEN_SHOW_ON_TOP:
                    if (Traces.IsDebugAndCatOK(TraceCat.ExecuteScreen))
                        Traces.LogAddDebug(TraceCat.ExecuteScreen,
                                           string.Format("Screen.SHOW_ON_TOP {0}", QuickScript.m_Arguments[0].Symbol));
                    ((BTScreen)QuickScript.m_Arguments[0]).ShowScreenToTop();
                    break;
                case ALL_FUNC.SCREEN_SCREEN_SHOT:
                    if (Traces.IsDebugAndCatOK(TraceCat.ExecuteScreen))
                        Traces.LogAddDebug(TraceCat.ExecuteScreen,
                                           string.Format("Screen.SCREEN_SNAPSHOT {0}", QuickScript.m_Arguments[0].Symbol));
                    ((BTScreen)QuickScript.m_Arguments[0]).TakeScreenShot("");
                    break;
            }
        }
    }
}

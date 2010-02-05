using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace CommonLib
{
    // classe contenant les informations direct pré parsé pour accélerer l'execution
    public class PreParsedLine
    {
        // doit toujours être définit
        public SCR_OBJECT m_SrcObject = SCR_OBJECT.INVALID;
        // doit toujours être définit
        public TOKEN_TYPE m_SecondTokenType = TOKEN_TYPE.NULL;
        public string m_SecondToken;
        // peut ne pas être définit dans le cas des fonctions maths et logic
        public TOKEN_TYPE m_ThirdTokenType = TOKEN_TYPE.NULL;
        public string m_ThirdToken;
        // n'est définit que pour les fonctions ayant des arguments
        public BaseObject[] m_Arguments = null;
    }


    public partial class ScriptExecuter
    {
        private delegate void ScriptAddedToExecute();

        #region données membres
        BTDoc m_Document = null;
        static bool m_bIsWaiting = false;
        static int nbinstanceexecuter = 0;
        Queue<StringCollection> m_PileScriptsToExecute = new Queue<StringCollection>();
        #endregion

        #region events
        private event ScriptAddedToExecute EvScriptToExecute;
        public event AddLogEventDelegate EventAddLogEvent;
        #endregion

        #region cosntructeur / destructeur
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public ScriptExecuter()
        {
            EvScriptToExecute += new ScriptAddedToExecute(ScriptExecuter_EvScriptToExecute);
            nbinstanceexecuter++;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        ~ScriptExecuter()
        {
            nbinstanceexecuter--;
        }
        #endregion

        #region attributs
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
        #endregion

        #region boucle d'execution
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        void ScriptExecuter_EvScriptToExecute()
        {
            CommonLib.PerfChrono theChrono = new PerfChrono();
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
            theChrono.EndMeasure("ScriptExecuter");
        }
        #endregion

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
                Traces.LogAddDebug("ScriptExecuter", "Execution de la ligne : \"" + Lines[i] + "\"");
                CommonLib.PerfChrono theChrono = new PerfChrono();
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
                            case SCR_OBJECT.LOGIC:
                                ParseExecuteLogic(Line);
                                break;
                            case SCR_OBJECT.SCREEN:
                                ParseExecuteScreen(Line);
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
                theChrono.EndMeasure("ScriptExecuter script = " + Lines[i]);
            }
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


        #region fonction diverses
        public void AddLogEvent(LogEvent Event)
        {
            if (EventAddLogEvent != null)
            {
                EventAddLogEvent(Event);
            }
        }
        #endregion
    }
}

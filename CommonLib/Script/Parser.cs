using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace CommonLib
{
    public enum TOKEN_TYPE
    {
        NULL,
        SCR_OBJECT,
        FRAME,
        TIMER,
        LOGGER,
        FUNCTION,
        LOGGER_FUNC,
        TIMER_FUNC,
        FRAME_FUNC,
    }

    // mots clef de base du langage BTScript
    public enum SCR_OBJECT
    {
        INVALID,
        FRAMES,
        FUNCTIONS,
        LOGGERS,
        TIMERS,
    }

    public enum FRAME_FUNC
    {
        INVALID,
        SEND,
        RECEIVE,
    }

    public enum LOGGER_FUNC
    {
        INVALID,
        LOG,
        CLEAR,
        START,
        STOP,
    }

    public enum TIMER_FUNC
    {
        INVALID,
        START,
        STOP
    }

    public enum ErrorType
    {
        NO_ERROR,
        ERROR,
        WARNING
    }

    //*****************************************************************************************************
    // Description:
    // Return: /
    //*****************************************************************************************************
    public class ScriptParserError
    {
        public string m_strMessage;
        public int m_line =0;
        public ErrorType m_ErrorType;

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public ScriptParserError()
        {

        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public ScriptParserError(string strMess, int line, ErrorType Err)
        {
            m_strMessage = strMess;
            m_line = line;
            m_ErrorType = Err;
        }
    }

    //*****************************************************************************************************
    // Description:
    // Return: /
    //*****************************************************************************************************
    public class ScriptParser
    {
        public const int INDEX_TOKEN_SYMBOL = 1;
        
        BTDoc m_Document = null;
        int m_iCurLine = 0;

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

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public ScriptParser()
        {

        }

        //*****************************************************************************************************
        // Description: renvoie le type du token a la position pos (position du curseur sur la ligne)
        // Return: /
        //*****************************************************************************************************
        public TOKEN_TYPE GetTokenTypeAtPos(string Line, int Pos)
        {
            List<int> listPointPos = new List<int>();
            int PosPoint = 0;
            while (PosPoint != -1 && Line.Length>0)
            {
                PosPoint = Line.IndexOf('.', PosPoint+1);
                if (PosPoint != -1)
                    listPointPos.Add(PosPoint);
            }
            int TokenNumAtPos = 0;
            if (listPointPos.Count == 0)
            {
                // il n'y a pas de point sur la ligne, on est au premier token
                return TOKEN_TYPE.SCR_OBJECT;
            }
            for (int i = 0; i < listPointPos.Count; i++)
            {
                if (Pos > listPointPos[i])
                {
                    TokenNumAtPos = i+1;
                }
            }
            if (TokenNumAtPos == 0)
            {
                // on est au premier token
                return TOKEN_TYPE.SCR_OBJECT;
            }

            if (TokenNumAtPos == 2)
            {
                int indexLastClosingParenthese = Line.LastIndexOf(')');
                int indexLastOpeningParenthese = Line.LastIndexOf('(');
                if (indexLastClosingParenthese > 0 && Pos > indexLastClosingParenthese)
                    return TOKEN_TYPE.NULL;
                else if (indexLastOpeningParenthese > 0 && Pos > indexLastOpeningParenthese)
                    return TOKEN_TYPE.NULL;
            }
            // on est ici, on au moin un point
            //on parse le premier token
            TOKEN_TYPE retTokenType = TOKEN_TYPE.NULL;
            List<ScriptParserError> ListErr = new List<ScriptParserError>();
            SCR_OBJECT ObjType = ParseFirstTokenType(Line, ListErr);
            switch (ObjType)
            {
                case SCR_OBJECT.FRAMES:
                    retTokenType = TOKEN_TYPE.FRAME;
                    if (TokenNumAtPos == 2)
                        retTokenType = TOKEN_TYPE.FRAME_FUNC;
                    break;
                case SCR_OBJECT.FUNCTIONS:
                    if (TokenNumAtPos == 1)
                        retTokenType = TOKEN_TYPE.FUNCTION;
                    else
                        retTokenType = TOKEN_TYPE.NULL;
                    break;
                case SCR_OBJECT.LOGGERS:
                    retTokenType = TOKEN_TYPE.LOGGER;
                    if (TokenNumAtPos == 2)
                        retTokenType = TOKEN_TYPE.LOGGER_FUNC;
                    break;
                case SCR_OBJECT.TIMERS:
                    retTokenType = TOKEN_TYPE.TIMER;
                    if (TokenNumAtPos == 2)
                        retTokenType = TOKEN_TYPE.TIMER_FUNC;
                    break;
                case SCR_OBJECT.INVALID:
                default:
                    break;
            }

            return retTokenType;
            
        }

        //*****************************************************************************************************
        // Description: renvoie une liste de chaine correspondant aux object utilisable au token donné a la position pos
        // Return: /
        //*****************************************************************************************************
        public StringCollection GetAutoCompletStringListAtPos(string Line, int Pos)
        {
            TOKEN_TYPE CurTokenType = GetTokenTypeAtPos(Line, Pos);
            StringCollection AutoCompleteStrings = new StringCollection();
            switch (CurTokenType)
            {
                case TOKEN_TYPE.SCR_OBJECT:
                    AutoCompleteStrings.Add(SCR_OBJECT.FRAMES.ToString());
                    AutoCompleteStrings.Add(SCR_OBJECT.FUNCTIONS.ToString());
                    AutoCompleteStrings.Add(SCR_OBJECT.LOGGERS.ToString());
                    AutoCompleteStrings.Add(SCR_OBJECT.TIMERS.ToString());
                    break;
                case TOKEN_TYPE.FRAME:
                    for (int i = 0; i < m_Document.GestTrame.Count; i++)
                    {
                        AutoCompleteStrings.Add(m_Document.GestTrame[i].Symbol);
                    }
                    break;
                case TOKEN_TYPE.FRAME_FUNC:
                    AutoCompleteStrings.Add(FRAME_FUNC.SEND.ToString());
                    AutoCompleteStrings.Add(FRAME_FUNC.RECEIVE.ToString());
                    break;
                case TOKEN_TYPE.TIMER:
                    for (int i = 0; i < m_Document.GestTimer.Count; i++)
                    {
                        AutoCompleteStrings.Add(m_Document.GestTimer[i].Symbol);
                    }
                    break;
                case TOKEN_TYPE.TIMER_FUNC:
                    AutoCompleteStrings.Add(TIMER_FUNC.START.ToString());
                    AutoCompleteStrings.Add(TIMER_FUNC.STOP.ToString());
                    break;
                case TOKEN_TYPE.LOGGER:
                    for (int i = 0; i < m_Document.GestLogger.Count; i++)
                    {
                        AutoCompleteStrings.Add(m_Document.GestLogger[i].Symbol);
                    }
                    break;
                case TOKEN_TYPE.LOGGER_FUNC:
                    AutoCompleteStrings.Add(LOGGER_FUNC.CLEAR.ToString());
                    AutoCompleteStrings.Add(LOGGER_FUNC.LOG.ToString());
                    AutoCompleteStrings.Add(LOGGER_FUNC.START.ToString());
                    AutoCompleteStrings.Add(LOGGER_FUNC.STOP.ToString());
                    break;
                case TOKEN_TYPE.FUNCTION:
                    for (int i = 0; i < m_Document.GestFunction.Count; i++)
                    {
                        AutoCompleteStrings.Add(m_Document.GestFunction[i].Symbol);
                    }
                    break;
                case TOKEN_TYPE.NULL:
                default:
                    break;
            }
            return AutoCompleteStrings;
        }


        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public bool ParseScript(string[] Lines, List<ScriptParserError> ErrorList)
        {
            for (int i = 0 ; i< Lines.Length; i++)
            {
                m_iCurLine = i;
                if (Lines[i].Length > 0)
                {
                    string Line = Lines[i];
                    string[] strTab = Line.Split('.');
                    if (strTab.Length > 0)
                    {
                        string strScrObject = strTab[0];
                        SCR_OBJECT FirstTokenType = ParseFirstTokenType(Line, ErrorList);
                        switch (FirstTokenType)
                        {
                            case SCR_OBJECT.FRAMES:
                                if (ParseFrame(Line, ErrorList))
                                    ParseFrameFuncion(Line, ErrorList);
                                break;
                            case SCR_OBJECT.FUNCTIONS:
                                ParseFunction(Line, ErrorList);
                                break;
                            case SCR_OBJECT.LOGGERS:
                                if (ParseLogger(Line, ErrorList))
                                    ParseLoggerFuncion(Line, ErrorList);
                                break;
                            case SCR_OBJECT.TIMERS:
                                if (ParseTimer(Line, ErrorList))
                                    ParseTimerFuncion(Line, ErrorList);
                                break;
                            case SCR_OBJECT.INVALID:
                            default:
                                break;
                        }
                    }
                    else
                    {
                        ScriptParserError Err = new ScriptParserError("Invalid line", m_iCurLine, ErrorType.ERROR);
                        ErrorList.Add(Err);
                    }
                }
            }

            if (ErrorList.Count > 0)
                return false;
            else
                return true;

        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        static public string GetLineToken(string line, int iTokenIndex)
        {
            string[] strTab = line.Split('.');
            if (iTokenIndex < strTab.Length)
            {
                // cas des fonction, il peux y avoir des parenthèses a la fin qu'il faut enlever
                string strTemp = strTab[iTokenIndex];
                int posOpenParenthese = strTemp.LastIndexOf('(');
                if (posOpenParenthese != -1)
                    strTemp = strTemp.Remove(posOpenParenthese);

                string strTok = strTemp;
                strTok = strTok.Trim();

                return strTok;
            }
            return "";

        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected SCR_OBJECT ParseFirstTokenType(string Line, List<ScriptParserError> ErrorList)
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
                    string strErr = string.Format("Invalid Keyword {0}", strScrObject);
                    ScriptParserError Err = new ScriptParserError(strErr, m_iCurLine, ErrorType.ERROR);
                    ErrorList.Add(Err);
                    return SCR_OBJECT.INVALID;
                }
                return FirstTokenType;
            }
            return SCR_OBJECT.INVALID;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected bool ParseFrame(string line, List<ScriptParserError> ErrorList)
        {
            string[] strTab = line.Split('.');
            if (strTab.Length > 1)
            {
                string strFrame = strTab[1];
                strFrame = strFrame.Trim();
                if (m_Document.GestTrame.GetFromSymbol(strFrame) == null)
                {
                    string strErr = string.Format("Invalid Frame symbol {0}", strFrame);
                    ScriptParserError Err = new ScriptParserError(strErr, m_iCurLine, ErrorType.ERROR);
                    ErrorList.Add(Err);
                    return false;
                }
                return true;
            }
            else
            {
                string strErr = string.Format("Invalid line, missing frame symbol");
                ScriptParserError Err = new ScriptParserError(strErr, m_iCurLine, ErrorType.ERROR);
                ErrorList.Add(Err);
            }

            return false;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected bool ParseFunction(string line, List<ScriptParserError> ErrorList)
        {
            string[] strTab = line.Split('.');
            if (strTab.Length > 1)
            {
                string strTemp = strTab[1];
                int posOpenParenthese = strTemp.LastIndexOf('(');
                if (posOpenParenthese != -1)
                    strTemp = strTemp.Remove(posOpenParenthese);

                string strFunc = strTemp;
                strFunc = strFunc.Trim();
                // todo Gsetionaire de timer, gestionaire de log

                if (m_Document.GestFunction.GetFromSymbol(strFunc) == null)
                {
                    string strErr = string.Format("Invalid Function symbol {0}", strFunc);
                    ScriptParserError Err = new ScriptParserError(strErr, m_iCurLine, ErrorType.ERROR);
                    ErrorList.Add(Err);
                    return false;
                }

                return true;
            }
            else
            {
                string strErr = string.Format("Invalid line, missing function symbol");
                ScriptParserError Err = new ScriptParserError(strErr, m_iCurLine, ErrorType.ERROR);
                ErrorList.Add(Err);
            }
            return false;

        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected bool ParseLogger(string line, List<ScriptParserError> ErrorList)
        {
            string[] strTab = line.Split('.');
            if (strTab.Length > 1)
            {
                string strLog = strTab[1];
                strLog = strLog.Trim();
                if (m_Document.GestLogger.GetFromSymbol(strLog) == null)
                {
                    string strErr = string.Format("Invalid Logger symbol {0}", strLog);
                    ScriptParserError Err = new ScriptParserError(strErr, m_iCurLine, ErrorType.ERROR);
                    ErrorList.Add(Err);
                    return false;
                }
                return true;
            }
            else
            {
                string strErr = string.Format("Invalid line, missing Logger symbol");
                ScriptParserError Err = new ScriptParserError(strErr, m_iCurLine, ErrorType.ERROR);
                ErrorList.Add(Err);
            }
            return false;

        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected bool ParseTimer(string line, List<ScriptParserError> ErrorList)
        {
            string[] strTab = line.Split('.');
            if (strTab.Length > 1)
            {
                string strTimer = strTab[1];
                strTimer = strTimer.Trim();
                if (m_Document.GestTimer.GetFromSymbol(strTimer) == null)
                {
                    string strErr = string.Format("Invalid Timer symbol {0}", strTimer);
                    ScriptParserError Err = new ScriptParserError(strErr, m_iCurLine, ErrorType.ERROR);
                    ErrorList.Add(Err);
                    return false;
                }
                return true;
            }
            return false;

        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void ParseFrameFuncion(string line, List<ScriptParserError> ErrorList)
        {
            string[] strTab = line.Split('.');
            if (strTab.Length > 2)
            {
                string strTemp = strTab[2];
                string strTempFull = strTemp;
                int posOpenParenthese = strTemp.LastIndexOf('(');
                if (posOpenParenthese>=0)
                    strTemp = strTemp.Remove(posOpenParenthese);

                string strScrObject = strTemp;
                FRAME_FUNC SecondTokenType = FRAME_FUNC.INVALID;
                try
                {
                    SecondTokenType = (FRAME_FUNC)Enum.Parse(typeof(FRAME_FUNC), strScrObject);
                }
                catch (Exception)
                {
                    string strErr = string.Format("Invalid Frame function {0}", strScrObject);
                    ScriptParserError Err = new ScriptParserError(strErr, m_iCurLine, ErrorType.ERROR);
                    ErrorList.Add(Err);
                    return;
                }

                bool bCheckParenthese = false;
                switch (SecondTokenType)
                {
                    case FRAME_FUNC.SEND:
                        bCheckParenthese = true;
                        // ajouter du code ici si il faut parser le contenu des parenthèses
                        break;
                    case FRAME_FUNC.RECEIVE:
                        bCheckParenthese = true;
                        // ajouter du code ici si il faut parser le contenu des parenthèses
                        break;
                    case FRAME_FUNC.INVALID:
                    default:
                        break;
                }
                if (bCheckParenthese)
                {
                    if (posOpenParenthese == -1)
                    {
                        ScriptParserError Err = new ScriptParserError("Syntax Error : Missing '('", m_iCurLine, ErrorType.ERROR);
                        ErrorList.Add(Err);
                        return;
                    }
                    int posCloseParenthese = strTempFull.LastIndexOf(')');
                    if (posCloseParenthese == -1)
                    {
                        ScriptParserError Err = new ScriptParserError("Syntax Error : Missing ')'", m_iCurLine, ErrorType.ERROR);
                        ErrorList.Add(Err);
                        return;
                    }
                }
            }
            else
            {
                string strErr = string.Format("Invalid line, missing frame function");
                ScriptParserError Err = new ScriptParserError(strErr, m_iCurLine, ErrorType.ERROR);
                ErrorList.Add(Err);
            }

        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void ParseLoggerFuncion(string line, List<ScriptParserError> ErrorList)
        {
            string[] strTab = line.Split('.');
            if (strTab.Length > 2)
            {
                string strTemp = strTab[2];
                int posOpenParenthese = strTemp.LastIndexOf('(');
                if (posOpenParenthese == -1)
                {
                    ScriptParserError Err = new ScriptParserError("Syntax Error : Missing '('", m_iCurLine, ErrorType.ERROR);
                    ErrorList.Add(Err);
                    return;
                }
                int posCloseParenthese = strTemp.LastIndexOf(')');
                if (posCloseParenthese == -1)
                {
                    ScriptParserError Err = new ScriptParserError("Syntax Error : Missing ')'", m_iCurLine, ErrorType.ERROR);
                    ErrorList.Add(Err);
                    return;
                }

                strTemp = strTemp.Remove(posOpenParenthese);
                string strScrObject = strTemp;
                LOGGER_FUNC SecondTokenType = LOGGER_FUNC.INVALID;
                try
                {
                    SecondTokenType = (LOGGER_FUNC)Enum.Parse(typeof(LOGGER_FUNC), strScrObject);
                }
                catch (Exception)
                {
                    string strErr = string.Format("Invalid Logger function {0}", strScrObject);
                    ScriptParserError Err = new ScriptParserError(strErr, m_iCurLine, ErrorType.ERROR);
                    ErrorList.Add(Err);
                }

                switch (SecondTokenType)
                {
                    case LOGGER_FUNC.LOG:
                        // ajouter du code ici si il faut parser le contenu des parenthèses
                        break;
                    case LOGGER_FUNC.CLEAR:
                        // ajouter du code ici si il faut parser le contenu des parenthèses
                        break;
                    case LOGGER_FUNC.START:
                        // ajouter du code ici si il faut parser le contenu des parenthèses
                        break;
                    case LOGGER_FUNC.STOP:
                        // ajouter du code ici si il faut parser le contenu des parenthèses
                        break;
                    case LOGGER_FUNC.INVALID:
                    default:
                        break;
                }
            }
            else
            {
                string strErr = string.Format("Invalid line, missing logger function");
                ScriptParserError Err = new ScriptParserError(strErr, m_iCurLine, ErrorType.ERROR);
                ErrorList.Add(Err);
            }

        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void ParseTimerFuncion(string line, List<ScriptParserError> ErrorList)
        {
            string[] strTab = line.Split('.');
            if (strTab.Length > 2)
            {
                string strTemp = strTab[2];
                int posOpenParenthese = strTemp.LastIndexOf('(');
                if (posOpenParenthese == -1)
                {
                    ScriptParserError Err = new ScriptParserError("Syntax Error : Missing '('", m_iCurLine, ErrorType.ERROR);
                    ErrorList.Add(Err);
                    return;
                }
                int posCloseParenthese = strTemp.LastIndexOf(')');
                if (posCloseParenthese == -1)
                {
                    ScriptParserError Err = new ScriptParserError("Syntax Error : Missing ')'", m_iCurLine, ErrorType.ERROR);
                    ErrorList.Add(Err);
                    return;
                }
                
                strTemp = strTemp.Remove(posOpenParenthese);
                string strScrObject = strTemp;
                TIMER_FUNC SecondTokenType = TIMER_FUNC.INVALID;
                try
                {
                    SecondTokenType = (TIMER_FUNC)Enum.Parse(typeof(TIMER_FUNC), strScrObject);
                }
                catch (Exception)
                {
                    string strErr = string.Format("Invalid Timer function {0}", strScrObject);
                    ScriptParserError Err = new ScriptParserError(strErr, m_iCurLine, ErrorType.ERROR);
                    ErrorList.Add(Err);
                }

                switch (SecondTokenType)
                {
                    case TIMER_FUNC.START:
                        // ajouter du code ici si il faut parser le contenu des parenthèses
                        break;
                    case TIMER_FUNC.STOP:
                        // ajouter du code ici si il faut parser le contenu des parenthèses
                        break;
                    case TIMER_FUNC.INVALID:
                    default:
                        break;
                }
            }
            else
            {
                string strErr = string.Format("Invalid line, missing timer function");
                ScriptParserError Err = new ScriptParserError(strErr, m_iCurLine, ErrorType.ERROR);
                ErrorList.Add(Err);
            }
        }
        
    }
}

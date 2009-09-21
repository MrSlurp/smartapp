using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace CommonLib
{
    #region enums
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
        MATHS,
        MATHS_FUNC,
        DATA,
        SCREEN,
        SCREEN_FUNC,
    }

    // mots clef de base du langage BTScript
    public enum SCR_OBJECT
    {
        INVALID,
        FRAMES,
        FUNCTIONS,
        LOGGERS,
        TIMERS,
        MATHS,
        SCREEN,
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

    public enum MATHS_FUNC
    {
        INVALID,
        ADD,
        SUB,
        MUL,
        DIV,
    }

    public enum SCREEN_FUNC
    {
        INVALID,
        SHOW_ON_TOP,
    }

    public enum ErrorType
    {
        NO_ERROR,
        ERROR,
        WARNING
    }
    #endregion

    #region classe ScriptParserError
    //*****************************************************************************************************
    // Description:
    // Return: /
    //*****************************************************************************************************
    public class ScriptParserError
    {
        #region données membres
        public string m_strMessage;
        public int m_line =0;
        public ErrorType m_ErrorType;
        #endregion

        #region constructeurs
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
        #endregion
    }
    #endregion

    //*****************************************************************************************************
    // Description:
    // Return: /
    //*****************************************************************************************************
    public class ScriptParser
    {
        public const int INDEX_TOKEN_SYMBOL = 1;

        #region données membres
        BTDoc m_Document = null;
        int m_iCurLine = 0;
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

        #region constructeur
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public ScriptParser()
        {

        }
        #endregion

        #region fonction utilitaires
        //*****************************************************************************************************
        // Description: renvoie le type du token a la position pos (position du curseur sur la ligne)
        // Return: /
        //*****************************************************************************************************
        public TOKEN_TYPE GetTokenTypeAtPos(string Line, int Pos, out bool IsParameter)
        {
            IsParameter = false;
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

            int indexLastClosingParenthese = Line.LastIndexOf(')');
            int indexLastOpeningParenthese = Line.LastIndexOf('(');

            List<ScriptParserError> ListErr = new List<ScriptParserError>();
            TOKEN_TYPE retTokenType = TOKEN_TYPE.NULL;
            SCR_OBJECT ObjType = ParseFirstTokenType(Line, ListErr);
            if (TokenNumAtPos == 2 || (TokenNumAtPos == 1 && ObjType == SCR_OBJECT.FUNCTIONS))
            {
                if (indexLastClosingParenthese > 0 && Pos > indexLastClosingParenthese)
                    return TOKEN_TYPE.NULL;
                else if (indexLastOpeningParenthese > 0 && Pos > indexLastOpeningParenthese)
                    return TOKEN_TYPE.NULL;
            }
            // on est ici, on au moin un point
            //on parse le premier token
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
                case SCR_OBJECT.MATHS:
                    retTokenType = TOKEN_TYPE.MATHS_FUNC;
                    if (indexLastOpeningParenthese != -1 && Pos > indexLastOpeningParenthese)
                    {
                        retTokenType = TOKEN_TYPE.DATA;
                        IsParameter = true;
                    }
                    break;
                case SCR_OBJECT.SCREEN:
                    retTokenType = TOKEN_TYPE.SCREEN;
                    if (TokenNumAtPos == 2)
                        retTokenType = TOKEN_TYPE.SCREEN_FUNC;
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
        public StringCollection GetAutoCompletStringListAtPos(string Line, int Pos, out bool IsParameter)
        {
            TOKEN_TYPE CurTokenType = GetTokenTypeAtPos(Line, Pos, out IsParameter);
            StringCollection AutoCompleteStrings = new StringCollection();
            switch (CurTokenType)
            {
                case TOKEN_TYPE.SCR_OBJECT:
                    AutoCompleteStrings.Add(SCR_OBJECT.FRAMES.ToString());
                    AutoCompleteStrings.Add(SCR_OBJECT.FUNCTIONS.ToString());
                    AutoCompleteStrings.Add(SCR_OBJECT.LOGGERS.ToString());
                    AutoCompleteStrings.Add(SCR_OBJECT.TIMERS.ToString());
                    AutoCompleteStrings.Add(SCR_OBJECT.MATHS.ToString());
                    AutoCompleteStrings.Add(SCR_OBJECT.SCREEN.ToString());
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
                case TOKEN_TYPE.MATHS_FUNC:
                    AutoCompleteStrings.Add(MATHS_FUNC.ADD.ToString());
                    AutoCompleteStrings.Add(MATHS_FUNC.SUB.ToString());
                    AutoCompleteStrings.Add(MATHS_FUNC.MUL.ToString());
                    AutoCompleteStrings.Add(MATHS_FUNC.DIV.ToString());
                    break;
                case TOKEN_TYPE.DATA:
                    for (int i = 0; i < m_Document.GestData.Count; i++)
                    {
                        AutoCompleteStrings.Add(m_Document.GestData[i].Symbol);
                    }
                    break;
                case TOKEN_TYPE.SCREEN_FUNC:
                    AutoCompleteStrings.Add(SCREEN_FUNC.SHOW_ON_TOP.ToString());
                    break;
                case TOKEN_TYPE.SCREEN:
                    for (int i = 0; i < m_Document.GestScreen.Count; i++)
                    {
                        AutoCompleteStrings.Add(m_Document.GestScreen[i].Symbol);
                    }
                    break;
                case TOKEN_TYPE.NULL:
                default:
                    break;
            }
            return AutoCompleteStrings;
        }
        #endregion

        #region méthodes de parsing principales
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
                                    ParseFrameFunction(Line, ErrorList);
                                break;
                            case SCR_OBJECT.FUNCTIONS:
                                ParseFunction(Line, ErrorList);
                                break;
                            case SCR_OBJECT.LOGGERS:
                                if (ParseLogger(Line, ErrorList))
                                    ParseLoggerFunction(Line, ErrorList);
                                break;
                            case SCR_OBJECT.TIMERS:
                                if (ParseTimer(Line, ErrorList))
                                    ParseTimerFunction(Line, ErrorList);
                                break;
                            case SCR_OBJECT.MATHS:
                                ParseMathsFunction(Line, ErrorList);
                                break;
                            case SCR_OBJECT.SCREEN:
                                if (ParseScreen(Line, ErrorList))
                                    ParseScreenFunction(Line, ErrorList);
                                break;
                            case SCR_OBJECT.INVALID:
                            default:
                                ScriptParserError Err = new ScriptParserError("Unkown keyword", m_iCurLine, ErrorType.ERROR);
                                ErrorList.Add(Err);
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
        #endregion

        #region parsing des trames
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
        protected void ParseFrameFunction(string line, List<ScriptParserError> ErrorList)
        {
            string[] strTab = line.Split('.');
            if (strTab.Length > 2)
            {
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
        #endregion

        #region parsing des fonction
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
        #endregion

        #region paring des loggers
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
        protected void ParseLoggerFunction(string line, List<ScriptParserError> ErrorList)
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
        #endregion

        #region parsing des timers
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
        protected void ParseTimerFunction(string line, List<ScriptParserError> ErrorList)
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
        #endregion

        #region parsing des maths
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void ParseMathsFunction(string line, List<ScriptParserError> ErrorList)
        {
            string[] strTab = line.Split('.');
            if (strTab.Length > 1)
            {
                string strTemp = strTab[1];
                string strTempFull = strTemp;
                int posOpenParenthese = strTemp.LastIndexOf('(');
                if (posOpenParenthese >= 0)
                    strTemp = strTemp.Remove(posOpenParenthese);

                int posCloseParenthese = strTempFull.LastIndexOf(')');

                string strScrObject = strTemp;
                MATHS_FUNC SecondTokenType = MATHS_FUNC.INVALID;
                try
                {
                    SecondTokenType = (MATHS_FUNC)Enum.Parse(typeof(MATHS_FUNC), strScrObject);
                }
                catch (Exception)
                {
                    string strErr = string.Format("Invalid Maths function {0}", strScrObject);
                    ScriptParserError Err = new ScriptParserError(strErr, m_iCurLine, ErrorType.ERROR);
                    ErrorList.Add(Err);
                    return;
                }

                switch (SecondTokenType)
                {
                    case MATHS_FUNC.ADD:
                    case MATHS_FUNC.SUB:
                    case MATHS_FUNC.MUL:
                    case MATHS_FUNC.DIV:
                        if (posOpenParenthese == -1)
                        {
                            ScriptParserError Err = new ScriptParserError("Syntax Error : Missing '('", m_iCurLine, ErrorType.ERROR);
                            ErrorList.Add(Err);
                            return;
                        }
                        if (posCloseParenthese == -1)
                        {
                            ScriptParserError Err = new ScriptParserError("Syntax Error : Missing ')'", m_iCurLine, ErrorType.ERROR);
                            ErrorList.Add(Err);
                            return;
                        }
                        // si on est ici, c'est que les parenthèses sont présentes
                        // on regarde ce qu'il y a à l'intérieur
                        string strParams = strTempFull.Substring(posOpenParenthese+1, strTempFull.Length - 2 - posOpenParenthese);
                        strParams = strParams.Trim('(');
                        strParams = strParams.Trim(')');
                        string[] strParamList = strParams.Split(',');
                        if (strParamList.Length < 3)
                        {
                            string strErr = string.Format("Invalid line, not enought parameters for Maths function");
                            ScriptParserError Err = new ScriptParserError(strErr, m_iCurLine, ErrorType.ERROR);
                            ErrorList.Add(Err);
                        }
                        else if (strParamList.Length > 3)
                        {
                            string strErr = string.Format("Invalid line, too many parameters for Maths function");
                            ScriptParserError Err = new ScriptParserError(strErr, m_iCurLine, ErrorType.ERROR);
                            ErrorList.Add(Err);
                        }
                        else // on en a exactement 3
                        {
                            for (int i = 0; i< strParamList.Length; i++)
                            {
                                string strTempParam = strParamList[i].Trim();
                                if (!IsNumericValue(strTempParam))
                                {
                                    if (m_Document.GestData.GetFromSymbol(strTempParam) == null)
                                    {
                                        string strErr = string.Format("Invalid Data symbol {0}", strTempParam);
                                        ScriptParserError Err = new ScriptParserError(strErr, m_iCurLine, ErrorType.ERROR);
                                        ErrorList.Add(Err);
                                    }
                                }
                            }
                        }
                        // ajouter du code ici si il faut parser le contenu des parenthèses
                        break;
                    case MATHS_FUNC.INVALID:
                    default:
                        break;
                }
            }
            else
            {
                string strErr = string.Format("Invalid line, missing Maths function");
                ScriptParserError Err = new ScriptParserError(strErr, m_iCurLine, ErrorType.ERROR);
                ErrorList.Add(Err);
            }
        }
        #endregion

        #region parsing des screen
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected bool ParseScreen(string line, List<ScriptParserError> ErrorList)
        {
            string[] strTab = line.Split('.');
            if (strTab.Length > 1)
            {
                string strScreen = strTab[1];
                strScreen = strScreen.Trim();
                if (m_Document.GestScreen.GetFromSymbol(strScreen) == null)
                {
                    string strErr = string.Format("Invalid Screen symbol {0}", strScreen);
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
        protected void ParseScreenFunction(string line, List<ScriptParserError> ErrorList)
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
                SCREEN_FUNC SecondTokenType = SCREEN_FUNC.INVALID;
                try
                {
                    SecondTokenType = (SCREEN_FUNC)Enum.Parse(typeof(SCREEN_FUNC), strScrObject);
                }
                catch (Exception)
                {
                    string strErr = string.Format("Invalid Screen function {0}", strScrObject);
                    ScriptParserError Err = new ScriptParserError(strErr, m_iCurLine, ErrorType.ERROR);
                    ErrorList.Add(Err);
                }

                switch (SecondTokenType)
                {
                    case SCREEN_FUNC.SHOW_ON_TOP:
                        // ajouter du code ici si il faut parser le contenu des parenthèses
                        break;
                    case SCREEN_FUNC.INVALID:
                    default:
                        break;
                }
            }
            else
            {
                string strErr = string.Format("Invalid line, missing screen function");
                ScriptParserError Err = new ScriptParserError(strErr, m_iCurLine, ErrorType.ERROR);
                ErrorList.Add(Err);
            }
        }
        #endregion

        #region fonction utiles
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public static bool IsNumericValue(string strValue)
        {
            int value =0;
            return int.TryParse(strValue, out value);
        }
        #endregion
    }
}

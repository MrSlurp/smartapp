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
        LOGIC,
        LOGIC_FUNC
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
        LOGIC
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

    public enum LOGIC_FUNC
    {
        INVALID,
        NOT,
        AND,
        OR,
        NAND,
        NOR,
        XOR,
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
    public partial class ScriptParser
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
                case SCR_OBJECT.LOGIC:
                    retTokenType = TOKEN_TYPE.LOGIC_FUNC;
                    if (indexLastOpeningParenthese != -1 && Pos > indexLastOpeningParenthese)
                    {
                        retTokenType = TOKEN_TYPE.DATA;
                        IsParameter = true;
                    }
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
                    AutoCompleteStrings.Add(SCR_OBJECT.LOGIC.ToString());
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
                case TOKEN_TYPE.LOGIC_FUNC:
                    AutoCompleteStrings.Add(LOGIC_FUNC.NOT.ToString());
                    AutoCompleteStrings.Add(LOGIC_FUNC.AND.ToString());
                    AutoCompleteStrings.Add(LOGIC_FUNC.OR.ToString());
                    AutoCompleteStrings.Add(LOGIC_FUNC.NAND.ToString());
                    AutoCompleteStrings.Add(LOGIC_FUNC.NOR.ToString());
                    AutoCompleteStrings.Add(LOGIC_FUNC.XOR.ToString());
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
                            case SCR_OBJECT.LOGIC:
                                ParseLogicFunction(Line, ErrorList);
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
    
        public bool CheckParenthese(string line, List<ScriptParserError> ErrorList, ref int posOpenParenthese, ref int posCloseParenthese)
        {
            string[] strTab = line.Split('.');
            if (strTab.Length > 1)
            {
                string strTemp = strTab[1];
                posOpenParenthese = strTemp.LastIndexOf('(');
                posCloseParenthese = strTemp.LastIndexOf(')');
                if (posOpenParenthese == -1)
                {
                    ScriptParserError Err = new ScriptParserError("Syntax Error : Missing '('", m_iCurLine, ErrorType.ERROR);
                    ErrorList.Add(Err);
                    return false;
                }
                if (posCloseParenthese == -1)
                {
                    ScriptParserError Err = new ScriptParserError("Syntax Error : Missing ')'", m_iCurLine, ErrorType.ERROR);
                    ErrorList.Add(Err);
                    return false;
                }
            }
            return true;
        } 
        #endregion
    }
}

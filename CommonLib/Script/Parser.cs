using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace CommonLib
{

    #region classe ScriptParserError
    /// <summary>
    /// Classe utilitaire contenant les erreur de parsing su script 
    /// </summary>
    public class ScriptParserError
    {
        #region données membres publiques
        public string m_strMessage;
        public int m_line =0;
        public ErrorType m_ErrorType;
        #endregion

        #region constructeurs
        /// <summary>
        /// Constructeur par défaut de la classe
        /// </summary>
        public ScriptParserError()
        {

        }

        /// <summary>
        /// Constructeur secondaire de la classe
        /// </summary>
        /// <param name="strMess">Message affiché à l'utilisateur pour expliquer l'erreur</param>
        /// <param name="line">Numéro de la ligne dans le script</param>
        /// <param name="Err">type d'erreur (WARNING, ERROR)</param>
        public ScriptParserError(string strMess, int line, ErrorType Err)
        {
            m_strMessage = strMess;
            m_line = line;
            m_ErrorType = Err;
        }
        #endregion
    }
    #endregion

    /// <summary>
    /// Classe contenant les fonctions de bases du parser. Les fonction spécifique à chaque type 
    /// d'objet source sont distribuées dans des fichiers spécifiques 
    /// </summary>
    public partial class ScriptParser
    {
        public const int INDEX_TOKEN_SYMBOL = 1;

        #region données membres
        BTDoc m_Document = null;
        int m_iCurLine = 0;
        #endregion

        #region attributs
        /// <summary>
        /// Document courant 
        /// </summary>
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
        /// <summary>
        /// Constructeur par défaut de la classe
        /// </summary>
        public ScriptParser()
        {

        }
        #endregion

        #region fonction utilitaires
        /// <summary>
        /// renvoie le type du token a la position pos (position du curseur sur la ligne)
        /// </summary>
        /// <param name="Line">Ligne de script</param>
        /// <param name="Pos">Position du curseur sur la ligne</param>
        /// <param name="IsParameter">Indique en sortie de la fonction si le token est un paramètre</param>
        /// <return>Le type de token à la position pos </return>
        public TOKEN_TYPE GetTokenTypeAtPos(string Line, int Pos, out bool IsParameter)
        {
            IsParameter = false;
            List<int> listPointPos = new List<int>();
            int PosPoint = 0;
            while (PosPoint != -1 && Line.Length>0)
            {
                PosPoint = Line.IndexOf(ParseExecGlobals.TOKEN_SEPARATOR, PosPoint + 1);
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

            int indexLastClosingParenthese = -1;
            int indexLastOpeningParenthese = -1;
            GetParenthesePos(Line, ref indexLastOpeningParenthese, ref indexLastClosingParenthese);

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

        /// <summary>
        /// renvoie une liste de chaine correspondant aux object utilisable au token donné a la position pos
        /// </summary>
        /// <param name="Line">Ligne de script</param>
        /// <param name="Pos">Position du curseur sur la ligne</param>
        /// <param name="IsParameter">Indique en sortie de la fonction si le token est un paramètre</param>
        /// <return>Liste des chaines correspondantes au type de token</return>
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
        /// <summary>
        /// parse le script passé en paramètre et remplit la liste des erreurs si il y en a
        /// </summary>
        /// <param name="Lines">Lignes du script</param>
        /// <param name="ErrorList">liste des erreurs (sortie)</param>
        /// <return>true si il n'y a pas d'erreur</return>
        public bool ParseScript(string[] Lines, List<ScriptParserError> ErrorList)
        {
            for (int i = 0 ; i< Lines.Length; i++)
            {
                m_iCurLine = i;
                if (Lines[i].Length > 0)
                {
                    string Line = Lines[i];
                    string[] strTab = Line.Split(ParseExecGlobals.TOKEN_SEPARATOR);
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

        /// <summary>
        /// renvoie le jeton donné par numéro de jeton (de 0 à 2 en général))
        /// </summary>
        /// <param name="line">Ligne de script</param>
        /// <param name="iTokenIndex">numéro de jeton</param>
        /// <return>le jeton voulu ou une chaine vide</return>
        static public string GetLineToken(string line, int iTokenIndex)
        {
            string[] strTab = line.Split(ParseExecGlobals.TOKEN_SEPARATOR);
            if (iTokenIndex < strTab.Length)
            {
                // cas des fonction, il peux y avoir des parenthèses a la fin qu'il faut enlever
                string strTemp = strTab[iTokenIndex];
                strTemp = strTemp.Trim(')');
                strTemp = strTemp.Trim('(');

                string strTok = strTemp;
                strTok = strTok.Trim();

                return strTok;
            }
            return "";

        }

        /// <summary>
        /// renvoie le type du premier jeton de la ligne (jeton source)
        /// </summary>
        /// <param name="Line">Ligne de script</param>
        /// <param name="ErrorList">liste des erreurs (sortie)</param>
        /// <return>type de l'objet source</return>
        protected SCR_OBJECT ParseFirstTokenType(string Line, List<ScriptParserError> ErrorList)
        {
            string[] strTab = Line.Split(ParseExecGlobals.TOKEN_SEPARATOR);
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
        /// <summary>
        /// indique si la chaine passé est de type numérique
        /// </summary>
        /// <param name="strValue">chaine à tester</param>
        /// <return>true si la valeur est un numérique</return>
        public static bool IsNumericValue(string strValue)
        {
            int value =0;
            return int.TryParse(strValue, out value);
        }
    
        /// <summary>
        /// recherche pa position des parenthèses sur la ligne
        /// </summary>
        /// <param name="line">ligne de script</param>
        /// <param name="posOpenParenthese">position de la parentèse ouvrante (sortie)</param>
        /// <param name="posCloseParenthese">position de la parentèse fermante (sortie)</param>
        public static void GetParenthesePos(string line, ref int posOpenParenthese, ref int posCloseParenthese)
        {
            posOpenParenthese = line.LastIndexOf('(');
            posCloseParenthese = line.LastIndexOf(')');
        }

        /// <summary>
        /// retire les parenthèdes de fin de la chaine
        /// </summary>
        /// <param name="strToTrim">chaine ou les parenthèses doivent être retiré</param>
        public static void TrimEndParenthese(ref string strToTrim)
        {
            strToTrim = strToTrim.Trim(')');
            strToTrim = strToTrim.Trim('(');
        }

        /// <summary>
        /// Vérifie la présence des parenthèses sur la ligne
        /// </summary>
        /// <param name="line">ligne de script</param>
        /// <param name="ErrorList">liste des erreurs (sortie)</param>
        /// <return>true si les deux parenthèses sont présentes</return>
        public bool CheckParenthese(string line, List<ScriptParserError> ErrorList)
        {
            int posOpen = -1;
            int posClose = -1;
            return CheckParenthese(line, ErrorList, ref posOpen, ref posClose);
        }

    
        /// <summary>
        /// Vérifie la présence des parenthèses sur la ligne
        /// </summary>
        /// <param name="line">ligne de script</param>
        /// <param name="ErrorList">liste des erreurs (sortie)</param>
        /// <param name="posOpenParenthese">position de la parentèse ouvrante (sortie)</param>
        /// <param name="posCloseParenthese">position de la parentèse fermante (sortie)</param>
        /// <return>true si les deux parenthèses sont présentes</return>
        public bool CheckParenthese(string line, List<ScriptParserError> ErrorList, ref int posOpenParenthese, ref int posCloseParenthese)
        {
            GetParenthesePos(line, ref posOpenParenthese, ref posCloseParenthese);
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
            return true;
        }
    
        /// <summary>
        /// extrait la liste des paramètres de la fonction de script
        /// </summary>
        /// <param name="line">ligne de script</param>
        /// <param name="ErrorList">liste des erreurs (sortie)</param>
        /// <param name="RetParamList">liste des paramètres sous forme de chaine(sortie)</param>
        /// <return>true si aucun argument n'est vide</return>
        public bool GetArgsAsString(string line, List<ScriptParserError> ErrorList, ref string[] RetParamList)
        {
            int posOpenParenthese = 0;
            int posCloseParenthese = 0;
            if (!CheckParenthese(line, ErrorList, ref posOpenParenthese, ref posCloseParenthese))
                return false;

            int ParamLength = (posCloseParenthese-1) - posOpenParenthese;                  
            string strAllParams = line.Substring(posOpenParenthese +1 , ParamLength);
            string[] ParamList = strAllParams.Split(ParseExecGlobals.PARAM_SEPARATOR);
            bool bIsErrorParamEmpty = false; 
            for (int i = 0; i < ParamList.Length; i++)
            {                         
                if (string.IsNullOrEmpty(ParamList[i]))
                {
                    string strErr = string.Format("Invalid line, one parameter or more is empty");
                    ScriptParserError Err = new ScriptParserError(strErr, m_iCurLine, ErrorType.ERROR);
                    ErrorList.Add(Err);
                    bIsErrorParamEmpty = true;
                    break;
                }
            }
            if (bIsErrorParamEmpty)
                return false;
                 
            RetParamList = ParamList;     
            return true;
        } 

        /// <summary>
        /// renvoie la liste des paramètre sous forme de DATA
        /// </summary>
        /// <param name="line">ligne de script</param>
        /// <param name="ErrorList">liste des erreurs (sortie)</param>
        /// <param name="RetDataList">liste des paramètres sous forme de DATA(sortie)</param>
        /// <return>true si aucun argument n'est vide</return>
        public bool CheckParamsAsDatas(string[] DatasSymbols, List<ScriptParserError> ErrorList, int MinIntVal, int MaxIntVal)
        {
            bool HaveError = false;     
            for (int i = 0; i < DatasSymbols.Length; i++)
            {
                string strTempParam = DatasSymbols[i].Trim();
                if (!IsNumericValue(strTempParam))
                {
                    if (m_Document.GestData.GetFromSymbol(strTempParam) == null)
                    {
                        string strErr = string.Format("Invalid Data symbol {0}", strTempParam);
                        ScriptParserError Err = new ScriptParserError(strErr, m_iCurLine, ErrorType.ERROR);
                        ErrorList.Add(Err);
                        HaveError = true;
                    }
                }
                else
                {
                    int value = int.Parse(strTempParam);
                    if (!(value >= MinIntVal && value <= MaxIntVal))
                    {
                        string strErr = string.Format("Invalid constant value for , must be between {0} and {1}",MinIntVal, MaxIntVal);
                        ScriptParserError Err = new ScriptParserError(strErr, m_iCurLine, ErrorType.ERROR);
                        ErrorList.Add(Err);
                        HaveError  = true;
                    }
                }
            }
            return !HaveError;
        } 
        #endregion
    }
}

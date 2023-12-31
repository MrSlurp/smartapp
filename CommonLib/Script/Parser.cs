/*
    This file is part of SmartApp.
    Copyright (C) 2007-2016  Pascal Bigot

    SmartApp is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    SmartApp is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with Foobar.  If not, see <http://www.gnu.org/licenses/>.
*/
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
                case SCR_OBJECT.SYSTEM:
                    retTokenType = TOKEN_TYPE.SYSTEM;
                    if (TokenNumAtPos == 1)
                        retTokenType = TOKEN_TYPE.SYSTEM_FUNC;
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
            IsParameter = false;
            if (Line.Trim(' ').StartsWith("//"))
                return null;
            TOKEN_TYPE CurTokenType = GetTokenTypeAtPos(Line, Pos, out IsParameter);
            StringCollection AutoCompleteStrings = new StringCollection();
            switch (CurTokenType)
            {
                case TOKEN_TYPE.SCR_OBJECT:
                    AutoCompleteStrings.AddRange(Enum.GetNames(typeof(SCR_OBJECT)));
                    break;
                case TOKEN_TYPE.FRAME:
                    for (int i = 0; i < m_Document.GestTrame.Count; i++)
                    {
                        AutoCompleteStrings.Add(m_Document.GestTrame[i].Symbol);
                    }
                    break;
                case TOKEN_TYPE.FRAME_FUNC:
                    AutoCompleteStrings.AddRange(Enum.GetNames(typeof(FRAME_FUNC)));
                    break;
                case TOKEN_TYPE.TIMER:
                    for (int i = 0; i < m_Document.GestTimer.Count; i++)
                    {
                        AutoCompleteStrings.Add(m_Document.GestTimer[i].Symbol);
                    }
                    break;
                case TOKEN_TYPE.TIMER_FUNC:
                    AutoCompleteStrings.AddRange(Enum.GetNames(typeof(TIMER_FUNC)));
                    break;
                case TOKEN_TYPE.LOGGER:
                    for (int i = 0; i < m_Document.GestLogger.Count; i++)
                    {
                        AutoCompleteStrings.Add(m_Document.GestLogger[i].Symbol);
                    }
                    break;
                case TOKEN_TYPE.LOGGER_FUNC:
                    AutoCompleteStrings.AddRange(Enum.GetNames(typeof(LOGGER_FUNC)));
                    break;
                case TOKEN_TYPE.FUNCTION:
                    for (int i = 0; i < m_Document.GestFunction.Count; i++)
                    {
                        AutoCompleteStrings.Add(m_Document.GestFunction[i].Symbol);
                    }
                    break;
                case TOKEN_TYPE.MATHS_FUNC:
                    AutoCompleteStrings.AddRange(Enum.GetNames(typeof(MATHS_FUNC)));
                    break;
                case TOKEN_TYPE.DATA:
                    for (int i = 0; i < m_Document.GestData.Count; i++)
                    {
                        AutoCompleteStrings.Add(m_Document.GestData[i].Symbol);
                    }
                    break;
                case TOKEN_TYPE.SCREEN_FUNC:
                    AutoCompleteStrings.AddRange(Enum.GetNames(typeof(SCREEN_FUNC)));
                    break;
                case TOKEN_TYPE.SCREEN:
                    for (int i = 0; i < m_Document.GestScreen.Count; i++)
                    {
                        AutoCompleteStrings.Add(m_Document.GestScreen[i].Symbol);
                    }
                    break;
                case TOKEN_TYPE.LOGIC_FUNC:
                    AutoCompleteStrings.AddRange(Enum.GetNames(typeof(LOGIC_FUNC)));
                    break;
                case TOKEN_TYPE.SYSTEM_FUNC:
                    AutoCompleteStrings.AddRange(Enum.GetNames(typeof(SYSTEM_FUNC)));
                    break;
                case TOKEN_TYPE.NULL:
                default:
                    break;
            }
            // on filtre les chaines qui ne doivent pas être visibles
            AutoCompleteStrings.Remove("NULL");
            AutoCompleteStrings.Remove("INVALID");
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
                string Line = Lines[i].Trim(' ');
                if (!string.IsNullOrEmpty(Line) && Line.Length > 0 && !Line.StartsWith("//"))
                {
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
                            case SCR_OBJECT.SYSTEM:
                                if (ParseSystem(Line, ErrorList))
                                    ParseSystemFunction(Line, ErrorList);
                                break;
                            case SCR_OBJECT.INVALID:
                            default:
                                ScriptParserError Err = new ScriptParserError(Lang.LangSys.C("Unkown keyword"), m_iCurLine, ErrorType.ERROR);
                                ErrorList.Add(Err);
                                break;
                        }
                    }
                    else
                    {
                        ScriptParserError Err = new ScriptParserError(Lang.LangSys.C("Invalid line"), m_iCurLine, ErrorType.ERROR);
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
            if (!string.IsNullOrEmpty(line))
            {
                string[] strTab = line.Split(ParseExecGlobals.TOKEN_SEPARATOR);
                if (iTokenIndex < strTab.Length)
                {
                    // cas des fonction, il peux y avoir des parenthèses a la fin qu'il faut enlever
                    string strTemp = strTab[iTokenIndex];
                    strTemp = strTemp.Trim(')');
                    strTemp = strTemp.Trim('(');
                    return strTemp.Trim();
                }
            }
            return "";

        }

        /// <summary>
        /// renvoie la liste des token d'un ligne de script
        /// </summary>
        /// <param name="line">ligne à analyser</param>
        /// <returns>liste des tokens de la ligne</returns>
        static public StringCollection GetAllTokens(string line)
        {
            StringCollection result = new StringCollection();
            if (!string.IsNullOrEmpty(line) && !line.StartsWith("//"))
            {
                // on vire les espaces
                line = CleanScriptLine(line);
                // on remplace la parenthèse ouvrante par une virgule
                line = line.Replace('(', ',');
                // on remplace les point par une virgule
                line = line.Replace('.', ',');
                // on vire la parenthèse fermante
                line = line.Replace(")","");
                // normalement on se retrouve avec une suite de mot séparés par des virgules
                // on fini par splitter sur les virgules
                string[] strTab = line.Split(',');
                result.AddRange(strTab);
            }

            return result;
        }

        /// <summary>
        /// nettoie une ligne de script en supprimant tout ce qui se trouve après la parenthèse fermante
        /// et en supprimant les espaces
        /// </summary>
        /// <param name="line">ligne de script à nettoyer</param>
        /// <returns>ligne de script "propre"</returns>
        public static string CleanScriptLine(string line)
        {
            if (!string.IsNullOrEmpty(line))
            {
                string res = line;
                int posEndParenthese = res.LastIndexOf(')');
                if (posEndParenthese != -1 && (posEndParenthese + 1) < res.Length)
                    res = res.Remove(posEndParenthese + 1);
                return res.Replace(" ", "");
            }
            return string.Empty;
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
                    string strErr = string.Format(Lang.LangSys.C("Invalid Keyword {0}"), strScrObject);
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
            return CheckParenthese(line, ErrorList, ref posOpenParenthese, ref posCloseParenthese, m_iCurLine);
        }

        /// <summary>
        /// Vérifie la présence des parenthèses sur la ligne
        /// </summary>
        /// <param name="line">ligne de script</param>
        /// <param name="ErrorList">liste des erreurs (sortie)</param>
        /// <param name="posOpenParenthese">position de la parentèse ouvrante (sortie)</param>
        /// <param name="posCloseParenthese">position de la parentèse fermante (sortie)</param>
        /// <param name="iCurLine">Numero de la ligne courante</param>
        /// <return>true si les deux parenthèses sont présentes</return>
        static public bool CheckParenthese(string line, List<ScriptParserError> ErrorList, ref int posOpenParenthese, ref int posCloseParenthese, int iCurLine)
        {
            GetParenthesePos(line, ref posOpenParenthese, ref posCloseParenthese);
            if (posOpenParenthese == -1)
            {
                if (ErrorList != null)
                {
                    ScriptParserError Err = new ScriptParserError(Lang.LangSys.C("Syntax Error : Missing '('"), iCurLine, ErrorType.ERROR);
                    ErrorList.Add(Err);
                }
                return false;
            }
            if (posCloseParenthese == -1)
            {
                if (ErrorList != null)
                {
                    ScriptParserError Err = new ScriptParserError(Lang.LangSys.C("Syntax Error : Missing ')'"), iCurLine, ErrorType.ERROR);
                    ErrorList.Add(Err);
                }
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
        /// <param name="iCurLine">Numero de la ligne courante</param>
        /// <return>true si aucun argument n'est vide</return>
        static public bool GetArgsAsString(string line, List<ScriptParserError> ErrorList, ref string[] RetParamList, int iCurLine)
        {
            int posOpenParenthese = 0;
            int posCloseParenthese = 0;
            if (!CheckParenthese(line, ErrorList, ref posOpenParenthese, ref posCloseParenthese, iCurLine))
                return false;

            int ParamLength = (posCloseParenthese-1) - posOpenParenthese;                  
            string strAllParams = line.Substring(posOpenParenthese +1 , ParamLength);
            string[] ParamList = strAllParams.Split(ParseExecGlobals.PARAM_SEPARATOR);
            bool bIsErrorParamEmpty = false; 
            for (int i = 0; i < ParamList.Length; i++)
            {                         
                if (string.IsNullOrEmpty(ParamList[i]))
                {
                    if (ErrorList != null)
                    {
                        string strErr = string.Format(Lang.LangSys.C("Invalid line, one parameter or more is empty"));
                        ScriptParserError Err = new ScriptParserError(strErr, iCurLine, ErrorType.ERROR);
                        ErrorList.Add(Err);
                    }
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
        /// extrait la liste des paramètres de la fonction de script
        /// </summary>
        /// <param name="line">ligne de script</param>
        /// <param name="ErrorList">liste des erreurs (sortie)</param>
        /// <param name="RetParamList">liste des paramètres sous forme de chaine(sortie)</param>
        /// <return>true si aucun argument n'est vide</return>
        public bool GetArgsAsString(string line, List<ScriptParserError> ErrorList, ref string[] RetParamList)
        {
            return GetArgsAsString(line, ErrorList, ref RetParamList, m_iCurLine);
        }
    
        /// <summary>
        /// extrait la liste des paramètres de la fonction de script
        /// </summary>
        /// <param name="line">ligne de script</param>
        /// <param name="ErrorList">liste des erreurs (sortie)</param>
        /// <param name="RetParamList">liste des paramètres sous forme de chaine(sortie)</param>
        /// <return>true si aucun argument n'est vide</return>
        static public bool GetArgsAsString(string line, ref string[] RetParamList)
        {
            return GetArgsAsString(line, null, ref RetParamList, 0);
        } 
     

        /// <summary>
        /// renvoie la liste des paramètre sous forme de DATA
        /// </summary>
        /// <param name="DatasSymbols">liste des symboles des données</param>
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
                        string strErr = string.Format(Lang.LangSys.C("Invalid Data symbol {0}"), strTempParam);
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
                        string strErr = string.Format(Lang.LangSys.C("Invalid constant value, must be between {0} and {1}"),MinIntVal, MaxIntVal);
                        ScriptParserError Err = new ScriptParserError(strErr, m_iCurLine, ErrorType.ERROR);
                        ErrorList.Add(Err);
                        HaveError  = true;
                    }
                }
            }
            return !HaveError;
        } 
        #endregion

        #region parsing des fonction systeme
        /// <summary>
        /// vérifie que la trame utilisé pour les appels aux fonctions system
        /// </summary>
        /// <param name="line">ligne de script</param>
        /// <param name="ErrorList">liste des erreur (sortie)</param>
        /// <returns>true si le symbol de trame est valide</returns>
        protected bool ParseSystem(string line, List<ScriptParserError> ErrorList)
        {
            string[] strTab = line.Split(ParseExecGlobals.TOKEN_SEPARATOR);
            if (strTab.Length > 1)
            {
                string strTemp = strTab[1];
                if (!CheckParenthese(line, ErrorList))
                {
                    return false;
                }
                return true;
            }
            else
            {
                string strErr = string.Format("Invalid line, missing system function");
                ScriptParserError Err = new ScriptParserError(strErr, m_iCurLine, ErrorType.ERROR);
                ErrorList.Add(Err);
            }
            return false;

        }

        protected void ParseSystemFunction(string line, List<ScriptParserError> ErrorList)
        {
            string[] strTab = line.Split(ParseExecGlobals.TOKEN_SEPARATOR);
            if (strTab.Length > 1)
            {
                int posOpenParenthese = -1;
                int posCloseParenthese = -1;
                if (!CheckParenthese(line, ErrorList, ref posOpenParenthese, ref posCloseParenthese))
                    return;
                string strTemp = strTab[1];
                string strTempFull = strTemp;
                GetParenthesePos(strTemp, ref posOpenParenthese, ref posCloseParenthese);

                strTemp = strTemp.Remove(posOpenParenthese);
                string strScrObject = strTemp;
                SYSTEM_FUNC SecondTokenType = SYSTEM_FUNC.INVALID;
                try
                {
                    SecondTokenType = (SYSTEM_FUNC)Enum.Parse(typeof(SYSTEM_FUNC), strScrObject);
                }
                catch (Exception)
                {
                    string strErr = string.Format(Lang.LangSys.C("Invalid system function {0}"), strScrObject);
                    ScriptParserError Err = new ScriptParserError(strErr, m_iCurLine, ErrorType.ERROR);
                    ErrorList.Add(Err);
                    return;
                }
                string[] strParamList = null;
                if (!GetArgsAsString(line, ErrorList, ref strParamList))
                    return;

                switch (SecondTokenType)
                {
                    case SYSTEM_FUNC.SHELL_EXEC:
                        if (strParamList.Length < 1)
                        {
                            string strErr = string.Format(Lang.LangSys.C("Invalid line, not enought parameters for system function"));
                            ScriptParserError Err = new ScriptParserError(strErr, m_iCurLine, ErrorType.ERROR);
                            ErrorList.Add(Err);
                        }
                        break;
                    case SYSTEM_FUNC.INVALID:
                    default:
                        break;
                }
            }
            else
            {
                string strErr = string.Format(Lang.LangSys.C("Invalid line, missing system function"));
                ScriptParserError Err = new ScriptParserError(strErr, m_iCurLine, ErrorType.ERROR);
                ErrorList.Add(Err);
            }

        }
        #endregion

    }
}

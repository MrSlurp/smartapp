using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.IO;

namespace CommonLib
{
    //*****************************************************************************************************
    // Description:
    // Return: /
    //*****************************************************************************************************
    public partial class ScriptParser
    {
        #region parsing des screen
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected bool ParseScreen(string line, List<ScriptParserError> ErrorList)
        {
            string[] strTab = line.Split(ParseExecGlobals.TOKEN_SEPARATOR);
            if (strTab.Length > 1)
            {
                string strScreen = strTab[1];
                strScreen = strScreen.Trim();
                if (m_Document.GestScreen.GetFromSymbol(strScreen) == null)
                {
                    string strErr = string.Format(Lang.LangSys.C("Invalid Screen symbol {0}"), strScreen);
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
            string[] strTab = line.Split(ParseExecGlobals.TOKEN_SEPARATOR);
            if (strTab.Length > 2)
            {
                string strTemp = strTab[2];
                int posOpenParenthese = -1;
                int posCloseParenthese = -1;
                if (!CheckParenthese(strTemp, ErrorList , ref posOpenParenthese, ref posCloseParenthese))
                    return;

                strTemp = strTemp.Remove(posOpenParenthese);
                string strScrObject = strTemp;
                SCREEN_FUNC SecondTokenType = SCREEN_FUNC.INVALID;
                try
                {
                    SecondTokenType = (SCREEN_FUNC)Enum.Parse(typeof(SCREEN_FUNC), strScrObject);
                }
                catch (Exception)
                {
                    string strErr = string.Format(Lang.LangSys.C("Invalid Screen function {0}"), strScrObject);
                    ScriptParserError Err = new ScriptParserError(strErr, m_iCurLine, ErrorType.ERROR);
                    ErrorList.Add(Err);
                }

                switch (SecondTokenType)
                {
                    case SCREEN_FUNC.SHOW_ON_TOP:
                        // ajouter du code ici si il faut parser le contenu des parenthèses
                        break;
                    case SCREEN_FUNC.SCREEN_SHOT:
                        //string[] strParamList = null;
                        //if (!GetArgsAsString(line, ErrorList, ref strParamList))
                        //    return;

                        /*
                        if (strParamList.Length != 1)
                        {
                            string strErr = string.Format(Lang.LangSys.C("Invalid line, not enought parameters for Screen function"));
                            ScriptParserError Err = new ScriptParserError(strErr, m_iCurLine, ErrorType.ERROR);
                            ErrorList.Add(Err);
                        }
                        // TODO, vérifier que le path est valide
                        */
                        
                        break;
                    case SCREEN_FUNC.INVALID:
                    default:
                        break;
                }
            }
            else
            {
                string strErr = string.Format(Lang.LangSys.C("Invalid line, missing screen function"));
                ScriptParserError Err = new ScriptParserError(strErr, m_iCurLine, ErrorType.ERROR);
                ErrorList.Add(Err);
            }
        }
        #endregion
    }
}

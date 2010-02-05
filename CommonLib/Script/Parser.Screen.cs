using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

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
    }
}
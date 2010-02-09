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
        #region parsing des timers
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected bool ParseTimer(string line, List<ScriptParserError> ErrorList)
        {
            string[] strTab = line.Split(ParseExecGlobals.TOKEN_SEPARATOR);
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
    }
}

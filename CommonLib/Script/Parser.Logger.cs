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
        #region paring des loggers
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected bool ParseLogger(string line, List<ScriptParserError> ErrorList)
        {
            string[] strTab = line.Split(TOKEN_SEPARATOR);
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
            string[] strTab = line.Split(TOKEN_SEPARATOR);
            if (strTab.Length > 2)
            {
                string strTemp = strTab[2];
                int posOpenParenthese = -1;
                int posCloseParenthese = -1;
                if (!CheckParenthese(strTemp, ErrorList , ref posOpenParenthese, ref posCloseParenthese))
                    return;

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
    }
}

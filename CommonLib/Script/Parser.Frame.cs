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
        #region parsing des trames
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected bool ParseFrame(string line, List<ScriptParserError> ErrorList)
        {
            string[] strTab = line.Split(TOKEN_SEPARATOR);
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
            string[] strTab = line.Split(TOKEN_SEPARATOR);
            if (strTab.Length > 2)
            {
                string strTemp = strTab[2];
                string strTempFull = strTemp;
                int posOpenParenthese = -1;
                int posCloseParenthese = -1;
                if (!CheckParenthese(strTemp, ErrorList , ref posOpenParenthese, ref posCloseParenthese))
                    return;

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

                switch (SecondTokenType)
                {
                    case FRAME_FUNC.SEND:
                        // ajouter du code ici si il faut parser le contenu des parenthèses
                        break;
                    case FRAME_FUNC.RECEIVE:
                        // ajouter du code ici si il faut parser le contenu des parenthèses
                        break;
                    case FRAME_FUNC.INVALID:
                    default:
                        break;
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
    }
}

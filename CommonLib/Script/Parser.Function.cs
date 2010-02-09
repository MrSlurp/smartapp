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
        #region parsing des fonction
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected bool ParseFunction(string line, List<ScriptParserError> ErrorList)
        {
            string[] strTab = line.Split(TOKEN_SEPARATOR);
            if (strTab.Length > 1)
            {
                string strTemp = strTab[1];
                if (!CheckParenthese(line, ErrorList))
                {
                    return false;
                }
                
                strTemp = strTemp.Trim(')');
                strTemp = strTemp.Trim('(');

                string strFunc = strTemp;
                strFunc = strFunc.Trim();

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
    }
}

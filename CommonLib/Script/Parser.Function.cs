using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace CommonLib
{
    public partial class ScriptParser
    {
        #region parsing des fonction
        /// <summary>
        /// vérifie que la fonction appelée existe
        /// </summary>
        /// <param name="line">ligne de script</param>
        /// <param name="ErrorList">liste des erreur (sortie)</param>
        /// <returns>true si le symbol de fonction est valide</returns>
        protected bool ParseFunction(string line, List<ScriptParserError> ErrorList)
        {
            string[] strTab = line.Split(ParseExecGlobals.TOKEN_SEPARATOR);
            if (strTab.Length > 1)
            {
                string strTemp = strTab[1];
                if (!CheckParenthese(line, ErrorList))
                {
                    return false;
                }
                TrimEndParenthese(ref strTemp);
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

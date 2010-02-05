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
        #region parsing des maths
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void ParseMathsFunction(string line, List<ScriptParserError> ErrorList)
        {
            string[] strTab = line.Split('.');
            if (strTab.Length > 1)
            {
                string strTemp = strTab[1];
                string strTempFull = strTemp;
                int posOpenParenthese = strTemp.LastIndexOf('(');
                if (posOpenParenthese >= 0)
                    strTemp = strTemp.Remove(posOpenParenthese);

                int posCloseParenthese = strTempFull.LastIndexOf(')');

                string strScrObject = strTemp;
                MATHS_FUNC SecondTokenType = MATHS_FUNC.INVALID;
                try
                {
                    SecondTokenType = (MATHS_FUNC)Enum.Parse(typeof(MATHS_FUNC), strScrObject);
                }
                catch (Exception)
                {
                    string strErr = string.Format("Invalid Maths function {0}", strScrObject);
                    ScriptParserError Err = new ScriptParserError(strErr, m_iCurLine, ErrorType.ERROR);
                    ErrorList.Add(Err);
                    return;
                }

                switch (SecondTokenType)
                {
                    case MATHS_FUNC.ADD:
                    case MATHS_FUNC.SUB:
                    case MATHS_FUNC.MUL:
                    case MATHS_FUNC.DIV:
                        if (posOpenParenthese == -1)
                        {
                            ScriptParserError Err = new ScriptParserError("Syntax Error : Missing '('", m_iCurLine, ErrorType.ERROR);
                            ErrorList.Add(Err);
                            return;
                        }
                        if (posCloseParenthese == -1)
                        {
                            ScriptParserError Err = new ScriptParserError("Syntax Error : Missing ')'", m_iCurLine, ErrorType.ERROR);
                            ErrorList.Add(Err);
                            return;
                        }
                        // si on est ici, c'est que les parenthèses sont présentes
                        // on regarde ce qu'il y a à l'intérieur
                        string strParams = strTempFull.Substring(posOpenParenthese+1, strTempFull.Length - 2 - posOpenParenthese);
                        strParams = strParams.Trim('(');
                        strParams = strParams.Trim(')');
                        string[] strParamList = strParams.Split(',');
                        if (strParamList.Length < 3)
                        {
                            string strErr = string.Format("Invalid line, not enought parameters for Maths function");
                            ScriptParserError Err = new ScriptParserError(strErr, m_iCurLine, ErrorType.ERROR);
                            ErrorList.Add(Err);
                        }
                        else if (strParamList.Length > 3)
                        {
                            string strErr = string.Format("Invalid line, too many parameters for Maths function");
                            ScriptParserError Err = new ScriptParserError(strErr, m_iCurLine, ErrorType.ERROR);
                            ErrorList.Add(Err);
                        }
                        else // on en a exactement 3
                        {
                            for (int i = 0; i< strParamList.Length; i++)
                            {
                                string strTempParam = strParamList[i].Trim();
                                if (!IsNumericValue(strTempParam))
                                {
                                    if (m_Document.GestData.GetFromSymbol(strTempParam) == null)
                                    {
                                        string strErr = string.Format("Invalid Data symbol {0}", strTempParam);
                                        ScriptParserError Err = new ScriptParserError(strErr, m_iCurLine, ErrorType.ERROR);
                                        ErrorList.Add(Err);
                                    }
                                }
                            }
                        }
                        // ajouter du code ici si il faut parser le contenu des parenthèses
                        break;
                    case MATHS_FUNC.INVALID:
                    default:
                        break;
                }
            }
            else
            {
                string strErr = string.Format("Invalid line, missing Maths function");
                ScriptParserError Err = new ScriptParserError(strErr, m_iCurLine, ErrorType.ERROR);
                ErrorList.Add(Err);
            }
        }
        #endregion
    }
}

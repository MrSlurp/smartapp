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

        #region parsing des Logic
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void ParseLogicFunction(string line, List<ScriptParserError> ErrorList)
        {
            string[] strTab = line.Split('.');
            if (strTab.Length > 1)
            {
                string strTemp = strTab[1];
                string strTempFull = strTemp;
                int posOpenParenthese = 0;//strTemp.LastIndexOf('(');
                int posCloseParenthese = 0;
                bool bretParenthese = CheckParenthese(line, ErrorList, ref posOpenParenthese, ref posCloseParenthese);
                if (!bretParenthese)
                    return;
                    
                if (posOpenParenthese >= 0)
                    strTemp = strTemp.Remove(posOpenParenthese);

                string strScrObject = strTemp;
                LOGIC_FUNC SecondTokenType = LOGIC_FUNC.INVALID;
                try
                {
                    SecondTokenType = (LOGIC_FUNC)Enum.Parse(typeof(LOGIC_FUNC), strScrObject);
                }
                catch (Exception)
                {
                    string strErr = string.Format("Invalid logic function {0}", strScrObject);
                    ScriptParserError Err = new ScriptParserError(strErr, m_iCurLine, ErrorType.ERROR);
                    ErrorList.Add(Err);
                    return;
                }

                string strParams = strTempFull.Substring(posOpenParenthese + 1, strTempFull.Length - 2 - posOpenParenthese);
                strParams = strParams.Trim('(');
                strParams = strParams.Trim(')');
                string[] strParamList = strParams.Split(',');

                switch (SecondTokenType)
                {
                    case LOGIC_FUNC.NOT:
                    case LOGIC_FUNC.AND:
                    case LOGIC_FUNC.OR:
                    case LOGIC_FUNC.NAND:
                    case LOGIC_FUNC.NOR:
                    case LOGIC_FUNC.XOR:
                        int MinParamCount = 2;
                        int MaxParameterCount = 5;
                        if (SecondTokenType == LOGIC_FUNC.XOR)
                            MaxParameterCount = 3;
                            
                        if (SecondTokenType == LOGIC_FUNC.XOR)
                        {
                            MinParamCount = 2;
                            MaxParameterCount = 2;
                        }
                        bool bIsErrorParamEmpty = false;
                        for (int i = 0; i < strParamList.Length; i++)
                        {
                            if (string.IsNullOrEmpty(strParamList[i]))
                            {
                                string strErr = string.Format("Invalid line, one parameter or more is empty");
                                ScriptParserError Err = new ScriptParserError(strErr, m_iCurLine, ErrorType.ERROR);
                                ErrorList.Add(Err);
                                bIsErrorParamEmpty = true;
                                break;
                            }
                        }
                        if (bIsErrorParamEmpty)
                            break;
                        // si on est ici, c'est que les parenthèses sont présentes
                        // on regarde ce qu'il y a à l'intérieur
                        if (strParamList.Length < MinParamCount)
                        {
                            string strErr = string.Format("Invalid line, not enought parameters for Logic function");
                            ScriptParserError Err = new ScriptParserError(strErr, m_iCurLine, ErrorType.ERROR);
                            ErrorList.Add(Err);
                        }
                        else if (strParamList.Length > MaxParameterCount)
                        {
                            string strErr = string.Format("Invalid line, too many parameters for Logic function");
                            ScriptParserError Err = new ScriptParserError(strErr, m_iCurLine, ErrorType.ERROR);
                            ErrorList.Add(Err);
                        }
                        else // on en a entre 2 et 5 (donc 1 à 4 variables)
                        {
                            for (int i = 0; i < strParamList.Length; i++)
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
                                else
                                {
                                    int value = int.Parse(strTempParam);
                                    if (value != 0 && value != 1)
                                    {
                                        string strErr = string.Format("Invalid constant parameter value for logic, must be 0 or 1");
                                        ScriptParserError Err = new ScriptParserError(strErr, m_iCurLine, ErrorType.ERROR);
                                        ErrorList.Add(Err);
                                    }
                                }
                            }
                        }
                        break;
                    case LOGIC_FUNC.INVALID:
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

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
            string[] strTab = line.Split(TOKEN_SEPARATOR);
            if (strTab.Length > 1)
            {
                string strTemp = strTab[1];
                string strTempFull = strTemp;
                int posOpenParenthese = 0;
                int posCloseParenthese = 0;
                if (!CheckParenthese(strTemp, ErrorList, ref posOpenParenthese, ref posCloseParenthese))
                    return;
                    
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
                        bool bIsError = false;
                        
                        string[] strParamList = null;
                        if (!GetArgsAsString(line, ErrorList, ref strParamList))
                        {
                            bIsError = true;
                        }
                        if (bIsError)
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
                            CheckParamsAsDatas(strParamList, ErrorList, 0, 1);
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

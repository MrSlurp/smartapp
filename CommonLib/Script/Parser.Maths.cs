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
            string[] strTab = line.Split(ParseExecGlobals.TOKEN_SEPARATOR);
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
                MATHS_FUNC SecondTokenType = MATHS_FUNC.INVALID;
                try
                {
                    SecondTokenType = (MATHS_FUNC)Enum.Parse(typeof(MATHS_FUNC), strScrObject);
                }
                catch (Exception)
                {
                    string strErr = string.Format(Lang.LangSys.C("Invalid Maths function {0}"), strScrObject);
                    ScriptParserError Err = new ScriptParserError(strErr, m_iCurLine, ErrorType.ERROR);
                    ErrorList.Add(Err);
                    return;
                }

                string[] strParamList  = null;
                if (!GetArgsAsString(line, ErrorList, ref strParamList))
                    return;
                switch (SecondTokenType)
                {
                    case MATHS_FUNC.ADD:
                    case MATHS_FUNC.SUB:
                    case MATHS_FUNC.MUL:
                    case MATHS_FUNC.DIV:
                    case MATHS_FUNC.POW:
                    case MATHS_FUNC.MOD:
                        // si on est ici, c'est que les parenthèses sont présentes
                        // on regarde ce qu'il y a à l'intérieur
                        //string strParams = strTempFull.Substring(posOpenParenthese+1, strTempFull.Length - 2 - posOpenParenthese);
                        //strParams = strParams.Trim('(');
                        //strParams = strParams.Trim(')');
                        //= strParams.Split(',');
                        if (strParamList.Length < 3)
                        {
                            string strErr = string.Format(Lang.LangSys.C("Invalid line, not enought parameters for Maths function"));
                            ScriptParserError Err = new ScriptParserError(strErr, m_iCurLine, ErrorType.ERROR);
                            ErrorList.Add(Err);
                        }
                        else if (strParamList.Length > 3)
                        {
                            string strErr = string.Format(Lang.LangSys.C("Invalid line, too many parameters for Maths function"));
                            ScriptParserError Err = new ScriptParserError(strErr, m_iCurLine, ErrorType.ERROR);
                            ErrorList.Add(Err);
                        }
                        else // on en a exactement 3
                        {
                            CheckParamsAsDatas(strParamList, ErrorList, -32768, 32767);
                        }
                        // ajouter du code ici si il faut parser le contenu des parenthèses
                        break;
                    case MATHS_FUNC.COS:
                    case MATHS_FUNC.SIN:
                    case MATHS_FUNC.TAN:
                    case MATHS_FUNC.SQRT:
                    case MATHS_FUNC.LN:
                    case MATHS_FUNC.LOG:
                    case MATHS_FUNC.SET:
                        if (strParamList.Length < 2)
                        {
                            string strErr = string.Format(Lang.LangSys.C("Invalid line, not enought parameters for Maths function"));
                            ScriptParserError Err = new ScriptParserError(strErr, m_iCurLine, ErrorType.ERROR);
                            ErrorList.Add(Err);
                        }
                        else if (strParamList.Length > 2)
                        {
                            string strErr = string.Format(Lang.LangSys.C("Invalid line, too many parameters for Maths function"));
                            ScriptParserError Err = new ScriptParserError(strErr, m_iCurLine, ErrorType.ERROR);
                            ErrorList.Add(Err);
                        }
                        else // on en a exactement 2
                        {
                            CheckParamsAsDatas(strParamList, ErrorList, -32768, 32767);
                        }
                        break;
                    case MATHS_FUNC.INVALID:
                    default:
                        break;
                }
            }
            else
            {
                string strErr = string.Format(Lang.LangSys.C("Invalid line, missing Maths function"));
                ScriptParserError Err = new ScriptParserError(strErr, m_iCurLine, ErrorType.ERROR);
                ErrorList.Add(Err);
            }
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace CommonLib
{
    public partial class ScriptExecuter
    {
        #region execution des fonction mathématiques
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void ParseExecuteMaths(string line)
        {
            string[] strTab = line.Split(ParseExecGlobals.TOKEN_SEPARATOR);
            if (strTab.Length == 2)
            {
                string strTempFull = strTab[1];
                int posOpenParenthese = strTempFull.LastIndexOf('(');
                int posCloseParenthese = strTempFull.LastIndexOf(')');

                string MathFunc = strTab[1];
                MathFunc = MathFunc.Remove(posOpenParenthese);
                MathFunc = MathFunc.Trim();

                MATHS_FUNC SecondTokenType = MATHS_FUNC.INVALID;
                try
                {
                    SecondTokenType = (MATHS_FUNC)Enum.Parse(typeof(MATHS_FUNC), MathFunc);
                }
                catch (Exception)
                {
                    return;
                }
                if (SecondTokenType != MATHS_FUNC.INVALID)
                {
                    string strParams = strTempFull.Substring(posOpenParenthese + 1, strTempFull.Length - 2 - posOpenParenthese);
                    strParams = strParams.Trim('(');
                    strParams = strParams.Trim(')');
                    strParams = strParams.Trim(' ');
                    string[] strParamList = strParams.Split(',');
                    if (strParamList.Length != 3)
                    {
                        LogEvent logEvent = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format("Math function invalid parameters count"));
                        AddLogEvent(logEvent);
                        return;
                    }
                    // on parse les données
                    string ResultSymbol = strParamList[0].Trim();
                    string OP1Symbol = strParamList[1].Trim();
                    string OP2Symbol = strParamList[2].Trim();
                    bool bOP1IsNumeric = false;
                    bool bOP2IsNumeric = false;
                    bOP1IsNumeric = ScriptParser.IsNumericValue(OP1Symbol);
                    bOP2IsNumeric = ScriptParser.IsNumericValue(OP2Symbol);
                    Data ResultData = (Data)m_Document.GestData.QuickGetFromSymbol(ResultSymbol);
                    Data Operator1Data = null;
                    if (!bOP1IsNumeric)
                        Operator1Data = (Data)m_Document.GestData.QuickGetFromSymbol(OP1Symbol);
                    else
                    {
                        Operator1Data = new Data("TEMP_MATHS_DATA1", int.Parse(OP1Symbol), DATA_SIZE.DATA_SIZE_32B, false);
                        Operator1Data.InitVal();
                    }

                    Data Operator2Data = null;
                    if (!bOP2IsNumeric)
                        Operator2Data = (Data)m_Document.GestData.QuickGetFromSymbol(OP2Symbol);
                    else
                    {
                        Operator2Data = new Data("TEMP_MATHS_DATA2", int.Parse(OP2Symbol), DATA_SIZE.DATA_SIZE_32B, false);
                        Operator2Data.InitVal();
                    }

                    if (ResultData == null)
                    {
                        LogEvent logEvent = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format("Unknown Data {0}", ResultSymbol));
                        AddLogEvent(logEvent);
                        return;
                    }
                    if (Operator1Data == null && !bOP1IsNumeric)
                    {
                        LogEvent logEvent = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format("Unknown Data {0}", OP1Symbol));
                        AddLogEvent(logEvent);
                        return;
                    }
                    if (Operator2Data == null && !bOP2IsNumeric)
                    {
                        LogEvent logEvent = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format("Unknown Data {0}", OP2Symbol));
                        AddLogEvent(logEvent);
                        return;
                    }
                    switch (SecondTokenType)
                    {
                        case MATHS_FUNC.ADD:
                            ExecuteMathAdd(ResultData, Operator1Data, Operator2Data);
                            break;
                        case MATHS_FUNC.SUB:
                            ExecuteMathSub(ResultData, Operator1Data, Operator2Data);
                            break;
                        case MATHS_FUNC.MUL:
                            ExecuteMathMul(ResultData, Operator1Data, Operator2Data);
                            break;
                        case MATHS_FUNC.DIV:
                            ExecuteMathDiv(ResultData, Operator1Data, Operator2Data);
                            break;
                    }
                }
                else
                {
                    LogEvent logEvent = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format("Invalid Math function"));
                    AddLogEvent(logEvent);
                    return;
                }
            }
        }

        //*****************************************************************************************************
        protected void ExecuteMathAdd(Data Result, Data Operator1, Data Operator2)
        {
            Result.Value = Operator1.Value + Operator2.Value;
        }

        //*****************************************************************************************************
        protected void ExecuteMathSub(Data Result, Data Operator1, Data Operator2)
        {
            Result.Value = Operator1.Value - Operator2.Value;
        }

        //*****************************************************************************************************
        protected void ExecuteMathMul(Data Result, Data Operator1, Data Operator2)
        {
            Result.Value = Operator1.Value * Operator2.Value;
        }

        //*****************************************************************************************************
        protected void ExecuteMathDiv(Data Result, Data Operator1, Data Operator2)
        {
            if (Operator2.Value != 0)
            {
                Result.Value = Operator1.Value / Operator2.Value;
            }
            else
            {
                LogEvent logEvent = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format("Division by zero forbidden"));
                AddLogEvent(logEvent);
                return;
            }
        }
        //*****************************************************************************************************

        #endregion
        
    }
}

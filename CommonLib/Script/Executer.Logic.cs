using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace CommonLib
{
#if !QUICK_MOTOR
    public partial class ScriptExecuter
    {
        #region execution des fonction Logiques
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void ParseExecuteLogic(string line)
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

                LOGIC_FUNC SecondTokenType = LOGIC_FUNC.INVALID;
                try
                {
                    SecondTokenType = (LOGIC_FUNC)Enum.Parse(typeof(LOGIC_FUNC), MathFunc);
                }
                catch (Exception)
                {
                    return;
                }
                if (SecondTokenType != LOGIC_FUNC.INVALID)
                {
                    string strParams = strTempFull.Substring(posOpenParenthese + 1, strTempFull.Length - 2 - posOpenParenthese);
                    string[] strParamList = strParams.Split(',');
                    // on parse les données
                    for (int i = 0; i < strParamList.Length; i++ )
                    {
                        strParamList[i] = strParamList[i].Trim();
                    }
                    string ResultSymbol = strParamList[0];

                    bool[] TabNumericsOPs = new bool[strParamList.Length -1];
                    for (int i = 1; i < strParamList.Length; i++ )
                    {
                        TabNumericsOPs[i-1] = ScriptParser.IsNumericValue(strParamList[i]);
                    }
                    Data ResultData = (Data)m_Document.GestData.QuickGetFromSymbol(ResultSymbol);
                    Data[] Operators = new Data[strParamList.Length -1];
                    for (int i = 1; i < strParamList.Length; i++)
                    {
                        if (!TabNumericsOPs[i-1])
                            Operators[i-1] = (Data)m_Document.GestData.QuickGetFromSymbol(strParamList[i]);
                        else
                        {
                            Operators[i-1] = new Data(string.Format("TEMP_MATHS_DATA{0}", i), int.Parse(strParamList[i]), DATA_SIZE.DATA_SIZE_32B, false);
                            Operators[i-1].InitVal();
                        }
                    }

                    if (ResultData == null)
                    {
                        LogEvent logEvent = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format("Unknown Data {0}", ResultSymbol));
                        AddLogEvent(logEvent);
                        return;
                    }
                    for (int i = 0; i < Operators.Length; i++)
                    {
                        if (Operators[i] == null)
                        {
                            LogEvent logEvent = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format("Unknown Data {0}", strParamList[i+1]));
                            AddLogEvent(logEvent);
                            return;
                        }
                    }

                    switch (SecondTokenType)
                    {
                        case LOGIC_FUNC.NOT:
                            ExecuteLogicNOT(ResultData, Operators[0]);
                            break;
                        case LOGIC_FUNC.AND:
                            ExecuteLogicAND(ResultData, Operators);
                            break;
                        case LOGIC_FUNC.OR:
                            ExecuteLogicOR(ResultData, Operators);
                            break;
                        case LOGIC_FUNC.NAND:
                            ExecuteLogicNAND(ResultData, Operators);
                            break;
                        case LOGIC_FUNC.NOR:
                            ExecuteLogicNOR(ResultData, Operators);
                            break;
                        case LOGIC_FUNC.XOR:
                            ExecuteLogicXOR(ResultData, Operators);
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
        protected void ExecuteLogicNOT(Data Result, Data Operator1)
        {
            if (Operator1.Value == 0)
                Result.Value = 1;
            else
                Result.Value = 0;
        }

        //*****************************************************************************************************
        protected void ExecuteLogicAND(Data Result, Data[] Operators)
        {
            bool bRes = true;
            for (int i = 0; i < Operators.Length; i++)
            {
                bRes &= Operators[i].Value != 0;
            }
            Result.Value = bRes ? 1 : 0;
        }

        //*****************************************************************************************************
        protected void ExecuteLogicOR(Data Result, Data[] Operators)
        {
            bool bRes = false;
            for (int i = 0; i < Operators.Length; i++)
            {
                bRes |= Operators[i].Value != 0;
            }
            Result.Value = bRes ? 1 : 0;
        }

        //*****************************************************************************************************
        protected void ExecuteLogicNAND(Data Result, Data[] Operators)
        {
            bool bRes = true;
            for (int i = 0; i < Operators.Length; i++)
            {
                bRes &= Operators[i].Value != 0;
            }
            Result.Value = bRes ? 0 : 1;
        }

        //*****************************************************************************************************
        protected void ExecuteLogicNOR(Data Result, Data[] Operators)
        {
            bool bRes = false;
            for (int i = 0; i < Operators.Length; i++)
            {
                bRes |= Operators[i].Value != 0;
            }
            Result.Value = bRes ? 0 : 1;
        }

        protected void ExecuteLogicXOR(Data Result, Data[] Operators)
        {
            bool bRes = false;
            if ((Operators[0].Value == 0 && Operators[1].Value == 1)
                || (Operators[0].Value == 1 && Operators[1].Value == 0)
                )
                bRes = true;
            Result.Value = bRes ? 1 : 0;
        }
        //*****************************************************************************************************

        #endregion        
    }
#endif
}

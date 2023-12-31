/*
    This file is part of SmartApp.
    Copyright (C) 2007-2016  Pascal Bigot

    SmartApp is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    SmartApp is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with Foobar.  If not, see <http://www.gnu.org/licenses/>.
*/
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace CommonLib
{
    /// <summary>
    /// cette classe à pour but de convertir le script brut en script pre parsé afin d'accélerer l'execution
    /// </summary>
    internal class PreParser
    {
        BTDoc m_Document = null;

        public event AddLogEventDelegate EventAddLogEvent;
        
        public PreParser()
        {

        }

        ~PreParser()
        {

        }
    
        public void AddLogEvent(LogEvent Event)
        {
            if (EventAddLogEvent != null)
            {
                EventAddLogEvent(Event);
            }
        }
    
        #region attributs
        public BTDoc Document
        {
            get
            {
                return m_Document;
            }
            set
            {
                m_Document = value;
            }
        }
        #endregion

        public List<PreParsedLine> PreParseScript(string[] script)
        {
            if (script == null || script.Length == 0)
                return null;
            List<PreParsedLine> retPreParsedScript = new List<PreParsedLine>();
            for (int i = 0; i < script.Length; i++)
            {
                string line = script[i].Replace(" ", "");
                if (!string.IsNullOrEmpty(line) && !line.StartsWith("//"))
                    retPreParsedScript.Add(PreParseLine(line));
            }
            return retPreParsedScript.Count != 0 ? retPreParsedScript : null;
        }

        private PreParsedLine PreParseLine(string Line)
        {
            PreParsedLine retPreParsedLine = new PreParsedLine();
            string[] strTab = Line.Split(ParseExecGlobals.TOKEN_SEPARATOR);
            if (strTab.Length > 1)
            {
                string strScrObject = strTab[0];
                SCR_OBJECT FirstTokenType = ParseFirstTokenType(Line);
                retPreParsedLine.m_SrcObject = FirstTokenType;
                switch (FirstTokenType)
                {
                    case SCR_OBJECT.FRAMES:
                        PreParseFrame(ref retPreParsedLine, Line);
                        break;
                    case SCR_OBJECT.FUNCTIONS:
                        PreParseFunction(ref retPreParsedLine, Line);
                        break;
                    case SCR_OBJECT.LOGGERS:
                        PreParseLogger(ref retPreParsedLine, Line);
                        break;
                    case SCR_OBJECT.TIMERS:
                        PreParseTimers(ref retPreParsedLine, Line);
                        break;
                    case SCR_OBJECT.MATHS:
                        PreParseMaths(ref retPreParsedLine, Line);
                        break;
                    case SCR_OBJECT.LOGIC:
                        PreParseLogic(ref retPreParsedLine, Line);
                        break;
                    case SCR_OBJECT.SCREEN:
                        PreParseScreen(ref retPreParsedLine, Line);
                        break;
                    case SCR_OBJECT.SYSTEM:
                        PreParseSystem(ref retPreParsedLine, Line);
                        break;
                    case SCR_OBJECT.INVALID:
                    default:
                        retPreParsedLine = null;
                        break;
                }
            }
            return retPreParsedLine;
        }
    
        /// <summary>
        /// renvoie le type du premier jeton de la ligne (jeton source)
        /// </summary>
        /// <param name="Line">Ligne de script</param>
        /// <return>type de l'objet source</return>
        protected SCR_OBJECT ParseFirstTokenType(string Line)
        {
            string[] strTab = Line.Split(ParseExecGlobals.TOKEN_SEPARATOR);
            if (strTab.Length > 0)
            {
                string strScrObject = strTab[0];
                SCR_OBJECT FirstTokenType = SCR_OBJECT.INVALID;
                try
                {
                    FirstTokenType = (SCR_OBJECT)Enum.Parse(typeof(SCR_OBJECT), strScrObject);
                }
                catch (Exception)
                {
                    Traces.LogAddDebug(TraceCat.Parser, "Erreur parsing premier token");
                    return SCR_OBJECT.INVALID;
                }
                return FirstTokenType;
            }
            return SCR_OBJECT.INVALID;
        }

        public void PreParseFrame(ref PreParsedLine retPreParsedLine, string line)
        {
            string[] strTab = line.Split(ParseExecGlobals.TOKEN_SEPARATOR);
            if (strTab.Length > 2)
            {
                string strFrame = strTab[1];
                strFrame = strFrame.Trim();
                string strTemp = strTab[2];
                string strTempFull = strTemp;
                int posOpenParenthese = strTemp.LastIndexOf('(');
                if (posOpenParenthese >= 0)
                    strTemp = strTemp.Remove(posOpenParenthese);

                string strScrObject = strTemp;
                FRAME_FUNC SecondTokenType = FRAME_FUNC.INVALID;
                try
                {
                    SecondTokenType = (FRAME_FUNC)Enum.Parse(typeof(FRAME_FUNC), strScrObject);
                }
                catch (Exception)
                {
                    Traces.LogAddDebug(TraceCat.Parser, "Erreur parsing frame func");
                    return;
                }
                retPreParsedLine.m_Arguments = new BaseObject[1] {m_Document.GestTrame.QuickGetFromSymbol(strFrame)};  
                switch (SecondTokenType)
                {
                    case FRAME_FUNC.RECEIVE:
                        retPreParsedLine.m_FunctionToExec = ALL_FUNC.FRAME_RECEIVE;
                        break;
                    case FRAME_FUNC.SEND:
                        retPreParsedLine.m_FunctionToExec = ALL_FUNC.FRAME_SEND;
                        break;
                }
            }
        }

        public void PreParseFunction(ref PreParsedLine retPreParsedLine, string line)
        {
            string[] strTab = line.Split(ParseExecGlobals.TOKEN_SEPARATOR);
            string FunctionSymb = strTab[1].Trim();
            ScriptParser.TrimEndParenthese(ref FunctionSymb);
            FunctionSymb = FunctionSymb.Trim();
            Function func = (Function)m_Document.GestFunction.QuickGetFromSymbol(FunctionSymb);
            retPreParsedLine.m_Arguments = new BaseObject[1] {func};
        }

        public void PreParseSystem(ref PreParsedLine retPreParsedLine, string line)
        {
            // dans le cas des fonction système, il peut y avoir des points entre les parenthèses
            string[] strTab = line.Split(ParseExecGlobals.TOKEN_SEPARATOR);
            if (strTab.Length >= 2)
            {
                string strTempFull = strTab[1];
                int posOpenParenthese = 0;
                int posCloseParenthese = 0;
                ScriptParser.GetParenthesePos(strTempFull, ref posOpenParenthese, ref posCloseParenthese);
                string MathFunc = strTempFull;
                MathFunc = MathFunc.Remove(posOpenParenthese);
                MathFunc = MathFunc.Trim();

                SYSTEM_FUNC SecondTokenType = SYSTEM_FUNC.INVALID;
                try
                {
                    SecondTokenType = (SYSTEM_FUNC)Enum.Parse(typeof(SYSTEM_FUNC), MathFunc);
                }
                catch (Exception)
                {
                    Traces.LogAddDebug(TraceCat.Parser, "Erreur parsing system func");
                    return;
                }
                if (SecondTokenType != SYSTEM_FUNC.INVALID)
                {
                    string[] strParamList = null;
                    ScriptParser.GetArgsAsString(line, ref strParamList);
                    retPreParsedLine.m_objArguments = new string[strParamList.Length];
                    for (int i = 0; i < strParamList.Length; i++)
                    {
                        ((string[])retPreParsedLine.m_objArguments)[i] = strParamList[i].Trim();
                    }
                    switch (SecondTokenType)
                    {
                        case SYSTEM_FUNC.SHELL_EXEC:
                            retPreParsedLine.m_FunctionToExec = ALL_FUNC.SYSTEM_SHELL_EXEC;
                            break;
                    }
                }
            }
        }
    
        public void PreParseTimers(ref PreParsedLine retPreParsedLine, string line)
        {
            string[] strTab = line.Split(ParseExecGlobals.TOKEN_SEPARATOR);
            if (strTab.Length > 2)
            {
                string strTimer = strTab[1];
                strTimer = strTimer.Trim();
                string strTemp = strTab[2];
                string strTempFull = strTemp;
                int posOpenParenthese = strTemp.LastIndexOf('(');
                if (posOpenParenthese >= 0)
                    strTemp = strTemp.Remove(posOpenParenthese);

                string strScrObject = strTemp;
                TIMER_FUNC SecondTokenType = TIMER_FUNC.INVALID;
                try
                {
                    SecondTokenType = (TIMER_FUNC)Enum.Parse(typeof(TIMER_FUNC), strScrObject);
                }
                catch (Exception)
                {
                    Traces.LogAddDebug(TraceCat.Parser, "Erreur parsing timer func");
                    return;
                }
                retPreParsedLine.m_Arguments = new BaseObject[1] {m_Document.GestTimer.QuickGetFromSymbol(strTimer)};  
                switch (SecondTokenType)
                {
                    case TIMER_FUNC.START:
                        retPreParsedLine.m_FunctionToExec = ALL_FUNC.TIMER_START;
                        break;
                    case TIMER_FUNC.STOP:
                        retPreParsedLine.m_FunctionToExec = ALL_FUNC.TIMER_STOP;
                        break;
                }
            }            
        }
    
        public void PreParseMaths(ref PreParsedLine retPreParsedLine, string line)
        {
            string[] strTab = line.Split(ParseExecGlobals.TOKEN_SEPARATOR);
            if (strTab.Length == 2)
            {
                string strTempFull = strTab[1];
                
                int posOpenParenthese = 0;
                int posCloseParenthese = 0;
                ScriptParser.GetParenthesePos(strTempFull, ref posOpenParenthese, ref posCloseParenthese);
                string MathFunc = strTempFull;
                MathFunc = MathFunc.Remove(posOpenParenthese);
                MathFunc = MathFunc.Trim();

                MATHS_FUNC SecondTokenType = MATHS_FUNC.INVALID;
                try
                {
                    SecondTokenType = (MATHS_FUNC)Enum.Parse(typeof(MATHS_FUNC), MathFunc);
                }
                catch (Exception)
                {
                    Traces.LogAddDebug(TraceCat.Parser, "Erreur parsing maths func");
                    return;
                }
                if (SecondTokenType != MATHS_FUNC.INVALID)
                {
                    string[] strParamList = null;
                    ScriptParser.GetArgsAsString(line, ref strParamList);
                    Data[] dataParams = GetParamsAsData(strParamList);
                    retPreParsedLine.m_Arguments = new BaseObject[dataParams.Length];
                    for (int i = 0; i< dataParams.Length; i++)
                    {
                        retPreParsedLine.m_Arguments[i] = dataParams[i];                        
                    }
                    switch (SecondTokenType)
                    {
                        case MATHS_FUNC.ADD:
                            retPreParsedLine.m_FunctionToExec = ALL_FUNC.MATHS_ADD;
                            break;
                        case MATHS_FUNC.SUB:
                            retPreParsedLine.m_FunctionToExec = ALL_FUNC.MATHS_SUB;
                            break;
                        case MATHS_FUNC.MUL:
                            retPreParsedLine.m_FunctionToExec = ALL_FUNC.MATHS_MUL;
                            break;
                        case MATHS_FUNC.DIV:
                            retPreParsedLine.m_FunctionToExec = ALL_FUNC.MATHS_DIV;
                            break;
                        case MATHS_FUNC.COS:
                            retPreParsedLine.m_FunctionToExec = ALL_FUNC.MATHS_COS;
                            break;
                        case MATHS_FUNC.SIN:
                            retPreParsedLine.m_FunctionToExec = ALL_FUNC.MATHS_SIN;
                            break;
                        case MATHS_FUNC.TAN:
                            retPreParsedLine.m_FunctionToExec = ALL_FUNC.MATHS_TAN;
                            break;
                        case MATHS_FUNC.SQRT:
                            retPreParsedLine.m_FunctionToExec = ALL_FUNC.MATHS_SQRT;
                            break;
                        case MATHS_FUNC.POW:
                            retPreParsedLine.m_FunctionToExec = ALL_FUNC.MATHS_POW;
                            break;
                        case MATHS_FUNC.LN:
                            retPreParsedLine.m_FunctionToExec = ALL_FUNC.MATHS_LN;
                            break;
                        case MATHS_FUNC.LOG:
                            retPreParsedLine.m_FunctionToExec = ALL_FUNC.MATHS_LOG;
                            break;
                        case MATHS_FUNC.SET:
                            retPreParsedLine.m_FunctionToExec = ALL_FUNC.MATHS_SET;
                            break;
                        case MATHS_FUNC.MOD:
                            retPreParsedLine.m_FunctionToExec = ALL_FUNC.MATHS_MOD;
                            break;
                    }
                }
            }            
        }
    
        public void PreParseLogic(ref PreParsedLine retPreParsedLine, string line)
        {
            string[] strTab = line.Split(ParseExecGlobals.TOKEN_SEPARATOR);
            if (strTab.Length == 2)
            {
                string strTempFull = strTab[1];
                
                int posOpenParenthese = 0;
                int posCloseParenthese = 0;
                ScriptParser.GetParenthesePos(strTempFull, ref posOpenParenthese, ref posCloseParenthese);
                string LogFunc = strTempFull;
                LogFunc = LogFunc.Remove(posOpenParenthese);
                LogFunc = LogFunc.Trim();

                LOGIC_FUNC SecondTokenType = LOGIC_FUNC.INVALID;
                try
                {
                    SecondTokenType = (LOGIC_FUNC)Enum.Parse(typeof(LOGIC_FUNC), LogFunc);
                }
                catch (Exception)
                {
                    Traces.LogAddDebug(TraceCat.Parser, "Erreur parsing logic func");
                    return;
                }
                if (SecondTokenType != LOGIC_FUNC.INVALID)
                {
                    string[] strParamList = null;
                    ScriptParser.GetArgsAsString(line, ref strParamList);
                    Data[] dataParams = GetParamsAsData(strParamList);
                    retPreParsedLine.m_Arguments = new BaseObject[dataParams.Length];
                    for (int i = 0; i< dataParams.Length; i++)
                    {
                        retPreParsedLine.m_Arguments[i] = dataParams[i];                        
                    }
                    switch (SecondTokenType)
                    {
                        case LOGIC_FUNC.NOT:
                            retPreParsedLine.m_FunctionToExec = ALL_FUNC.LOGIC_NOT;
                            break;
                        case LOGIC_FUNC.AND:
                            retPreParsedLine.m_FunctionToExec = ALL_FUNC.LOGIC_AND;
                            break;
                        case LOGIC_FUNC.OR:
                            retPreParsedLine.m_FunctionToExec = ALL_FUNC.LOGIC_OR;
                            break;
                        case LOGIC_FUNC.NAND:
                            retPreParsedLine.m_FunctionToExec = ALL_FUNC.LOGIC_NAND;
                            break;
                        case LOGIC_FUNC.NOR:
                            retPreParsedLine.m_FunctionToExec = ALL_FUNC.LOGIC_NOR;
                            break;
                        case LOGIC_FUNC.XOR:
                            retPreParsedLine.m_FunctionToExec = ALL_FUNC.LOGIC_XOR;
                            break;
                    }
                }
            }                
        }

        public void PreParseScreen(ref PreParsedLine retPreParsedLine, string line)
        {
            string[] strTab = line.Split(ParseExecGlobals.TOKEN_SEPARATOR);
            if (strTab.Length > 2)
            {
                string strScreen = strTab[1];
                strScreen = strScreen.Trim();
                string strTemp = strTab[2];
                string strTempFull = strTemp;
                int posOpenParenthese = strTemp.LastIndexOf('(');
                if (posOpenParenthese >= 0)
                    strTemp = strTemp.Remove(posOpenParenthese);

                string strScrObject = strTemp;
                SCREEN_FUNC SecondTokenType = SCREEN_FUNC.INVALID;
                try
                {
                    SecondTokenType = (SCREEN_FUNC)Enum.Parse(typeof(SCREEN_FUNC), strScrObject);
                }
                catch (Exception)
                {
                    Traces.LogAddDebug(TraceCat.Parser, "Erreur parsing screen func");
                    return;
                }
                retPreParsedLine.m_Arguments = new BaseObject[1] {m_Document.GestScreen.QuickGetFromSymbol(strScreen)};  
                switch (SecondTokenType)
                {
                    case SCREEN_FUNC.SHOW:
                        retPreParsedLine.m_FunctionToExec = ALL_FUNC.SCREEN_SHOW;
                        break;
                    case SCREEN_FUNC.HIDE:
                        retPreParsedLine.m_FunctionToExec = ALL_FUNC.SCREEN_HIDE;
                        break;
                    case SCREEN_FUNC.SHOW_ON_TOP:
                        retPreParsedLine.m_FunctionToExec = ALL_FUNC.SCREEN_SHOW_ON_TOP;
                        break;
                    case SCREEN_FUNC.SCREEN_SHOT:
                        {
                            retPreParsedLine.m_FunctionToExec = ALL_FUNC.SCREEN_SCREEN_SHOT;
                            /*
                            string[] strParamList = null;
                            ScriptParser.GetArgsAsString(line, ref strParamList);
                            retPreParsedLine.m_objArguments = new object[] { (strParamList!= null strParamList[0] : null)};
                            */
                        }
                        break;
                }
            }            
        }

        public void PreParseLogger(ref PreParsedLine retPreParsedLine, string line)
        {
            string[] strTab = line.Split(ParseExecGlobals.TOKEN_SEPARATOR);
            if (strTab.Length > 2)
            {
                string strLogger = strTab[1];
                strLogger = strLogger.Trim();
                string strTemp = strTab[2];
                string strTempFull = strTemp;
                int posOpenParenthese = strTemp.LastIndexOf('(');
                if (posOpenParenthese >= 0)
                    strTemp = strTemp.Remove(posOpenParenthese);

                string strScrObject = strTemp;
                LOGGER_FUNC SecondTokenType = LOGGER_FUNC.INVALID;
                try
                {
                    SecondTokenType = (LOGGER_FUNC)Enum.Parse(typeof(LOGGER_FUNC), strScrObject);
                }
                catch (Exception)
                {
                    Traces.LogAddDebug(TraceCat.Parser, "Erreur parsing logger func");
                    return;
                }
                retPreParsedLine.m_Arguments = new BaseObject[1] {m_Document.GestLogger.QuickGetFromSymbol(strLogger)};  
                switch (SecondTokenType)
                {
                    case LOGGER_FUNC.START:
                        retPreParsedLine.m_FunctionToExec = ALL_FUNC.LOGGER_START;
                        break;
                    case LOGGER_FUNC.STOP:
                        retPreParsedLine.m_FunctionToExec = ALL_FUNC.LOGGER_STOP;
                        break;
                    case LOGGER_FUNC.LOG:
                        retPreParsedLine.m_FunctionToExec = ALL_FUNC.LOGGER_LOG;
                        break;
                    case LOGGER_FUNC.CLEAR:
                        retPreParsedLine.m_FunctionToExec = ALL_FUNC.LOGGER_CLEAR;
                        break;
                    case LOGGER_FUNC.NEW_FILE:
                        retPreParsedLine.m_FunctionToExec = ALL_FUNC.LOGGER_NEW_FILE;
                        break;
                    default:
                        Traces.LogAddDebug(TraceCat.Parser, "parsing logger func : fonction non implémenté");
                        break;
                }
            }            
        }
    
        public Data[] GetParamsAsData(string[] Params)
        {
            Data[] retDatas= new Data[Params.Length];
            for (int i = 0; i < Params.Length; i++)
            {
                string strTempParam = Params[i].Trim();
                if (!ScriptParser.IsNumericValue(strTempParam))
                {
                    retDatas[i] = (Data)m_Document.GestData.QuickGetFromSymbol(strTempParam);
                }
                else
                {
                    int value = int.Parse(strTempParam);
                    Data OperatorTmpData = null;
                    OperatorTmpData = new Data("TEMP_DATA", value, DATA_SIZE.DATA_SIZE_32B, false);
                    OperatorTmpData.InitVal();
                    retDatas[i] = OperatorTmpData;
                }
            }
            return retDatas;
        }
    }
}

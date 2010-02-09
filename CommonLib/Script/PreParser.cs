using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace CommonLib
{
    /// <summary>
    /// cette classe à pour but de convertir le script brut en script pre parsé afin d'accélerer l'execution
    /// </summary>
    class PreParser
    {
        PreParser()
        {

        }

        ~PreParser()
        {

        }

        public PreParsedLine[] PreParseScript(StringCollection script)
        {
            PreParsedLine[] retPreParsedScript = new PreParsedLine[script.Count];
            for (int i = 0; i < script.Count; i++)
            {
                retPreParsedScript = PreParseLine(script[i]);
            }
            return retPreParsedScript;
        }

        public PreParsedLine PreParseLine(string script)
        {
            PreParsedLine retPreParsedLine = new PreParsedLine();
            string[] strTab = script.Split(ParseExecGlobals.TOKEN_SEPARATOR);
            if (strTab.Length > 1)
            {
                string strScrObject = strTab[0];
                SCR_OBJECT FirstTokenType = ParseFirstTokenType(Line);
                retPreParsedLine.m_SrcObject = FirstTokenType;
                switch (FirstTokenType)
                {
                    case SCR_OBJECT.FRAMES:
                        PreParseFrame();
                        break;
                    case SCR_OBJECT.FUNCTIONS:
                        PreParseFunction();
                        break;
                    case SCR_OBJECT.LOGGERS:
                        PreParseLogger();
                        break;
                    case SCR_OBJECT.TIMERS:
                        PreParseTimers();
                        break;
                    case SCR_OBJECT.MATHS:
                        PreParseMaths();
                        break;
                    case SCR_OBJECT.LOGIC:
                        PreParseLogic();
                        break;
                    case SCR_OBJECT.SCREEN:
                        PreParseScreen();
                        break;
                    case SCR_OBJECT.INVALID:
                    default:
                        break;
                }
            }
            return retPreParsedLine;
        }

    }
}

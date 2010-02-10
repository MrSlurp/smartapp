using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace CommonLib
{
#if !QUICK_MOTOR
    public partial class ScriptExecuter
    {
        #region execution des Loggers
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void ParseExecuteLogger(string line)
        {
            string[] strTab = line.Split(ParseExecGlobals.TOKEN_SEPARATOR);
            if (strTab.Length > 2)
            {
                string strLog = strTab[1];
                strLog = strLog.Trim();
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
                    return;
                }
                switch (SecondTokenType)
                {
                    case LOGGER_FUNC.CLEAR:
                        ExecuteClearLogger(strLog);
                        break;
                    case LOGGER_FUNC.LOG:
                        ExecuteLogLogger(strLog);
                        break;
                    case LOGGER_FUNC.START:
                        ExecuteStartAutoLogger(strLog);
                        break;
                    case LOGGER_FUNC.STOP:
                        ExecuteStopAutoLogger(strLog);
                        break;
                }
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void ExecuteClearLogger(string LoggerSymbol)
        {
            Logger log = (Logger)m_Document.GestLogger.QuickGetFromSymbol(LoggerSymbol);
            if (log != null)
                log.ClearLog();
            else
            {
                LogEvent logEvent = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format("Unknown Logger {0}", LoggerSymbol));
                AddLogEvent(logEvent);
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void ExecuteLogLogger(string LoggerSymbol)
        {
            Logger log = (Logger)m_Document.GestLogger.QuickGetFromSymbol(LoggerSymbol);
            if (log != null)
                log.LogData();
            else
            {
                LogEvent logEvent = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format("Unknown Logger {0}", LoggerSymbol));
                AddLogEvent(logEvent);
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void ExecuteStartAutoLogger(string LoggerSymbol)
        {
            Logger log = (Logger)m_Document.GestLogger.QuickGetFromSymbol(LoggerSymbol);
            if (log != null)
                log.StartAutoLogger();
            else
            {
                LogEvent logEvent = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format("Unknown Logger {0}", LoggerSymbol));
                AddLogEvent(logEvent);
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void ExecuteStopAutoLogger(string LoggerSymbol)
        {
            Logger log = (Logger)m_Document.GestLogger.QuickGetFromSymbol(LoggerSymbol);
            if (log != null)
                log.StopAutoLogger();
            else
            {
                LogEvent logEvent = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format("Unknown Logger {0}", LoggerSymbol));
                AddLogEvent(logEvent);
            }
        }

        #endregion        
    }
#endif
}

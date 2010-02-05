using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace CommonLib
{
    public partial class ScriptExecuter
    {
        #region execution des timers
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void ParseExecuteTimers(string line)
        {
            string[] strTab = line.Split('.');
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
                    return;
                }
                switch (SecondTokenType)
                {
                    case TIMER_FUNC.START:
                        ExecuteStartTimer(strTimer);
                        break;
                    case TIMER_FUNC.STOP:
                        ExecuteStopTimer(strTimer);
                        break;
                }
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void ExecuteStartTimer(string TimerSymbol)
        {
            BTTimer tm = (BTTimer)m_Document.GestTimer.QuickGetFromSymbol(TimerSymbol);
            tm.StartTimer();
            if (tm != null)
                tm.StartTimer();
            else
            {
                LogEvent log = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format("Unknown Timer {0}", TimerSymbol));
                AddLogEvent(log);
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void ExecuteStopTimer(string TimerSymbol)
        {
            BTTimer tm = (BTTimer)m_Document.GestTimer.QuickGetFromSymbol(TimerSymbol);
            if (tm != null)
                tm.StopTimer();
            else
            {
                LogEvent log = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format("Unknown Timer {0}", TimerSymbol));
                AddLogEvent(log);
            }
        }

        #endregion        
    }
}

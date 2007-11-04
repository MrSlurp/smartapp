using System;
using System.Collections.Generic;
using System.Text;

namespace SmartApp.AppEventLog
{
    public enum LOG_EVENT_TYPE
    {
        INFO,
        WARNING,
        ERROR,
    }

    public class LogEvent
    {
        public LOG_EVENT_TYPE m_LogEventType = LOG_EVENT_TYPE.INFO;
        public DateTime m_DateTime;
        public string m_strMessage;

        public LogEvent()
        {
            m_DateTime = DateTime.Now;
        }

        public LogEvent(LOG_EVENT_TYPE EventType, string strMessage)
        {
            m_DateTime = DateTime.Now;
            m_LogEventType = EventType;
            m_strMessage = strMessage;
        }
    }
}

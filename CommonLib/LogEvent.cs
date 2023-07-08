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
using System.Text;

namespace CommonLib
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

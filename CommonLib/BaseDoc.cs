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
    public delegate void RunStateChangeEvent(BaseDoc doc);

    public class BaseDoc
    {
        protected string m_strfileFullName;

        protected bool m_bModified = false;

        protected bool m_bModeRun = false;
        protected TYPE_APP m_TypeApp = TYPE_APP.NONE;

        public event AddLogEventDelegate EventAddLogEvent;
        public event DocumentModifiedEvent OnDocumentModified;
        public event RunStateChangeEvent OnRunStateChange;

        public BaseDoc(TYPE_APP typeApp)
        {
            m_TypeApp = typeApp;
        }

        /// <summary>
        /// accesseur du nom de fichier
        /// </summary>
        public string FileName
        {
            get
            {
                return m_strfileFullName;
            }
        }

        public bool IsRunning
        {
            get { return m_bModeRun; }
            protected set 
            {
                if (m_bModeRun != value)
                {
                    m_bModeRun = value;
                    NotifyRunStateChange();
                }
            }
        }

        protected void NotifyRunStateChange()
        {
            if (OnRunStateChange != null)
                OnRunStateChange(this);
        }

        /// <summary>
        /// ajout un message à la fenêtre de log de l'application
        /// </summary>
        /// <param name="Event">Event à afficher</param>
        protected void AddLogEvent(LogEvent Event)
        {
            if (EventAddLogEvent != null)
            {
                EventAddLogEvent(Event);
            }
        }

        /// <summary>
        /// indique si le document à été modifé depuis sa dernière sauvegarde
        /// et le notifie si son état est changé
        /// </summary>
        public bool Modified
        {
            get
            {
                return m_bModified;
            }
            set
            {
                m_bModified = value;
                if (OnDocumentModified != null)
                    OnDocumentModified();
            }
        }

        public virtual bool WriteConfigDocument(bool bShowError)
        {
            return true;
        }
    }
}

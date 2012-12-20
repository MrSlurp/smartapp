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
        }

        public void NotifyRunStateChange()
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

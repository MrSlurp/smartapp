using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommonLib;

namespace SmartApp.AppEventLog
{
    public partial class AppEventLogForm : Form
    {
        private delegate void delegateAddLog(LogEvent ev);
        #region donnés membres
        private int m_iMaxEventLine = 100;
        #endregion

        #region attributs
        public bool IsEmpty
        {
            get
            {
                return !(m_lvEvent.Items.Count > 0);
            }
        }
        #endregion

        #region constructeur
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public AppEventLogForm()
        {
            InitializeComponent();
        }
        #endregion

        #region methodes publiques
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public void AddLogEvent(LogEvent ev)
        {
            if (this.InvokeRequired)
            {
                delegateAddLog d = new delegateAddLog(AddLog);
                this.Invoke(d, ev);
            }
            else
            {
                AddLog(ev);
            }
        }
        #endregion

        #region Methodes privées
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void AddLog(LogEvent ev)
        {
            if (m_lvEvent.Items.Count < m_iMaxEventLine)
            {
                AddEventToListView(ev);
            }
            else
            {
                if (m_lvEvent.Items.Count > 0)
                {
                    m_lvEvent.Items.RemoveAt(0);
                }
                AddEventToListView(ev);
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void AddEventToListView(LogEvent ev)
        {
            if (m_lvEvent.Items.Count >= m_iMaxEventLine)
            {
                for (int i = 0; i < m_lvEvent.Items.Count; i++)
                {
                    if (m_lvEvent.Items[i].Index >= m_iMaxEventLine)
                        m_lvEvent.Items.RemoveAt(i);
                }
            }

            ListViewItem lviEvent = m_lvEvent.Items.Insert(0,(ev.m_DateTime.ToShortDateString() + " " + 
                                                            ev.m_DateTime.ToShortTimeString()+":"+
                                                            string.Format("{0,2:d}", ev.m_DateTime.Second.ToString())));
            lviEvent.SubItems.Add(ev.m_LogEventType.ToString());
            lviEvent.SubItems.Add(ev.m_strMessage);
            if (ev.m_LogEventType != LOG_EVENT_TYPE.INFO)
                this.BringToFront();
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void AppEventLogForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.WindowState = FormWindowState.Minimized;
            }
        }
        #endregion

        #region Handler d'évènements des objets de la form
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void m_btnClearLog_Click(object sender, EventArgs e)
        {
            m_lvEvent.Items.Clear();
        }
        #endregion
    }
}
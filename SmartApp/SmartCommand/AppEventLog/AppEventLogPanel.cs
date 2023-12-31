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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommonLib;

namespace SmartApp
{
    public partial class AppEventLogPanel : UserControl
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
        public AppEventLogPanel()
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
            if (ev.m_LogEventType != LOG_EVENT_TYPE.INFO && checkBringToTop.Checked)
                this.BringToFront();
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
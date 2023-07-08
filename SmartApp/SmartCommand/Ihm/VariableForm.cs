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
    public partial class VariableForm : Form
    {
        private GestData m_GestData;
        private Timer m_Timer;
        public VariableForm(GestData GestData)
        {
            this.WindowState = FormWindowState.Minimized;
            m_GestData = GestData;
            m_Timer = new Timer();
            m_Timer.Interval = 1500;
            m_Timer.Tick += new EventHandler(OnTimer);
            Program.LangSys.Initialize(this);
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Init();
            StartRefreshTimer();
        }

        protected void Init()
        {
            m_DataslistView.Items.Clear();
            for (int i = 0; i < m_GestData.Count; i++)
            {
                Data Dat = (Data)m_GestData[i];
                String[] ItemValues = new String[2];
                ItemValues[0] = Dat.Symbol;
                ItemValues[1] = String.Format("{0}", Dat.Value);
                m_DataslistView.Items.Add(new ListViewItem(ItemValues));
            }
        }

        protected void UpdateDataListView()
        {
            for (int i = 0; i < m_GestData.Count; i++)
            {
                Data Dat = (Data)m_GestData[i];
                String DataValue = String.Format("{0}", Dat.Value);
                m_DataslistView.Items[i].SubItems[1].Text = DataValue;
            }
        }

        public void StartRefreshTimer()
        {
            m_Timer.Enabled = true;
        }

        public void StopRefreshTimer()
        {
            m_Timer.Enabled = false;
        }

        protected void OnTimer(object obj, EventArgs e)
        {
            if (m_Timer.Enabled)
                UpdateDataListView();
        }
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.WindowState = FormWindowState.Minimized;
                e.Cancel = true;
            }
            else if (e.CloseReason == CloseReason.MdiFormClosing)
            {
                StopRefreshTimer();
            }
        }

    }
}
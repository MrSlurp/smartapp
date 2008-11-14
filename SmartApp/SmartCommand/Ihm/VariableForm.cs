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
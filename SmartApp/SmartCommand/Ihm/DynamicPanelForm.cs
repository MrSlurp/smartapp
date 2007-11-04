using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartApp.Ihm;

namespace SmartApp
{
    public partial class DynamicPanelForm : Form
    {
        private DynamicPanel m_DynamicPanel = null;

        public bool DynamicPanelEnabled
        {
            get
            {
                return m_DynamicPanel.Enabled;
            }
            set
            {
                m_DynamicPanel.Enabled = value;
            }
        }
        public DynamicPanelForm(DynamicPanel DynamicPanel)
        {
            InitializeComponent();
            m_DynamicPanel = DynamicPanel;
            this.Controls.Add(m_DynamicPanel);
        }

        private void DynamicPanelForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.WindowState = FormWindowState.Minimized;
            }
        }
    }
}
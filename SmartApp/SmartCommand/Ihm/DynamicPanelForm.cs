using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartApp.Ihm;
using CommonLib;

namespace SmartApp
{
    public partial class DynamicPanelForm : Form
    {
        private DynamicPanel m_DynamicPanel = null;

        BTDoc m_Document;

        /// <summary>
        /// 
        /// </summary>
        public BTDoc Document
        {
            get { return m_Document; }
            set { m_Document = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool DynamicPanelEnabled
        {
            get
            {
                return m_DynamicPanel.SpecialEnabled;
            }
            set
            {
                m_DynamicPanel.SpecialEnabled = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DynamicPanel"></param>
        public DynamicPanelForm(DynamicPanel DynamicPanel)
        {
            Program.LangSys.Initialize(this);
            InitializeComponent();
            m_DynamicPanel = DynamicPanel;
            this.Controls.Add(m_DynamicPanel);
            DynamicPanel.SetMeToTop += new DynamicPanel.SetMeToTopEvent(BringToFront);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
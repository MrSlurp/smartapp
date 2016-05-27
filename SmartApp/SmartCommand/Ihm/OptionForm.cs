using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartApp
{
    public partial class OptionForm : Form
    {
        public OptionForm()
        {
            Program.LangSys.Initialize(this);
            InitializeComponent();
        }

        public string LogFileDirectory
        {
            get
            {
                return m_textLogDir.Text;
            }
            set
            {
                m_textLogDir.Text = value;
            }
        }

        public bool AutoStartProjOnOpen
        {
            get
            {
                return m_checkAutoStartProjOnOpen.Checked;
            }
            set
            {
                m_checkAutoStartProjOnOpen.Checked = value;
            }
        }

        public bool HideMonitorAfterPrjStart
        {
            get
            {
                return m_checkHideMonitorOnStart.Checked;
            }
            set
            {
                m_checkHideMonitorOnStart.Checked = value;
            }
        }


        private void m_btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog Browser = new FolderBrowserDialog();
            Browser.RootFolder = Environment.SpecialFolder.MyComputer;
            if (!string.IsNullOrEmpty(m_textLogDir.Text))
            {
                Browser.SelectedPath = m_textLogDir.Text;
            }

            if (Browser.ShowDialog() == DialogResult.OK)
            {
                m_textLogDir.Text = Browser.SelectedPath;
            }
        }
    }
}
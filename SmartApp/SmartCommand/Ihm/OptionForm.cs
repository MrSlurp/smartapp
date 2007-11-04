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

        public bool SaveFileComm
        {
            get
            {
                return m_checkSaveComParam.Checked;
            }
            set
            {
                m_checkSaveComParam.Checked = value;
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
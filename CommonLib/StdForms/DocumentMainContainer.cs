using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CommonLib
{
    public partial class DocumentMainContainer : Form
    {
        private bool m_bDisableCloseProtection = false;
        public DocumentMainContainer()
        {
            InitializeComponent();
        }

        public bool DisableCloseProtection
        {
            get { return m_bDisableCloseProtection; }
            set { m_bDisableCloseProtection = value; }
        }

        private void DocumentMainContainer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && !m_bDisableCloseProtection)
            {
                e.Cancel = true;
                this.WindowState = FormWindowState.Minimized;
            }
        }
    }
}

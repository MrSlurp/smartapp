using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace SmartApp.Ihm
{
    public partial class ProjectNameForm : Form
    {
        bool m_bIsBridgeDoc = false;

        public bool IsBridgeDoc
        {
            get { return m_bIsBridgeDoc; }
            set { m_bIsBridgeDoc = value; }
        }

        public ProjectNameForm()
        {
            InitializeComponent();
            Program.LangSys.Initialize(this);
        }

        public string ProjectName
        {
            get { return textBox1.Text + (m_bIsBridgeDoc? ".sab" : ".saf"); }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            char[] invalidChars = Path.GetInvalidFileNameChars();
            bool bHaveInvalidChars = false;
            foreach (char c in invalidChars)
            {
                if (textBox1.Text.Contains(new string(c,1)))
                {
                    MessageBox.Show("Invalid char for filename");
                    bHaveInvalidChars = true;
                    break;
                }
            }
            if (!bHaveInvalidChars)
            {
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}

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
        public ProjectNameForm()
        {
            InitializeComponent();
        }

        public string ProjectName
        {
            get { return textBox1.Text; }
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

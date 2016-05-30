/*
    This file is part of SmartApp.

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

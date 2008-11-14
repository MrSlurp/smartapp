using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CommonLib
{
    public partial class ChangeListForm : Form
    {
        public StringCollection strListMess;
        public ChangeListForm()
        {
            InitializeComponent();
        }

        private void ChangeListForm_Load(object sender, EventArgs e)
        {
            if (strListMess != null)
            {
                for (int i = 0; i < strListMess.Count; i++)
                {
                    m_ListUsersMess.Items.Add(strListMess[i]);
                }
            }
        }

        private void m_BtnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void m_BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}
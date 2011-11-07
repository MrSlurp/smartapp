using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CtrlMailer
{
    public partial class ConfigSMTP : Form
    {
        public ConfigSMTP()
        {
            DllEntryClass.LangSys.Initialize(this);
            InitializeComponent();
            txtSMTPHost.Text = DllEntryClass.SMTP_Param.SMTP_host;
            txtPort.Value = DllEntryClass.SMTP_Param.SMTP_port;
            txtEmail.Text = DllEntryClass.SMTP_Param.userMail;
            txtPassword.Text = DllEntryClass.SMTP_Param.userPassword;
            chkUseSSL.Checked = DllEntryClass.SMTP_Param.useSSL;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DllEntryClass.SMTP_Param.SMTP_host = txtSMTPHost.Text;
            DllEntryClass.SMTP_Param.SMTP_port = (int)txtPort.Value;
            DllEntryClass.SMTP_Param.userMail = txtEmail.Text;
            DllEntryClass.SMTP_Param.userPassword = txtPassword.Text;
            DllEntryClass.SMTP_Param.useSSL = chkUseSSL.Checked;
        }
    }
}

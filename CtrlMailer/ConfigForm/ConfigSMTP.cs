/*
    This file is part of SmartApp.
    Copyright (C) 2007-2016  Pascal Bigot

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

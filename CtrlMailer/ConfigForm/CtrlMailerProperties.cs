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
using CommonLib;

namespace CtrlMailer
{
    public partial class CtrlMailerProperties : BaseControlPropertiesPanel, ISpecificPanel
    {
        DllCtrlMailerProp m_Props = new DllCtrlMailerProp(null);

        public DllCtrlMailerProp Props
        {
            get 
            {
                return m_Props; 
            }
            set
            { 
                m_Props = value;
            }
        }

        public CtrlMailerProperties()
        {
            DllEntryClass.LangSys.Initialize(this);
            InitializeComponent();
            updateSMTPInfo();
        }

        private void updateSMTPInfo()
        {
            lblHost.Text = DllEntryClass.SMTP_Param.SMTP_host;
            lblPort.Text = DllEntryClass.SMTP_Param.SMTP_port.ToString();
            lblSSL.Text = DllEntryClass.SMTP_Param.useSSL == true ? DllEntryClass.LangSys.C("Yes") : DllEntryClass.LangSys.C("No");
            edtFrom.Text = DllEntryClass.SMTP_Param.userMail;
        }

        private void btnCnfSMTP_Click(object sender, EventArgs e)
        {
            ConfigSMTP form = new ConfigSMTP();
            if (form.ShowDialog() == DialogResult.OK)
            {
                updateSMTPInfo();
            }
        }

        private void btnInsertData_Click(object sender, EventArgs e)
        {
            int posCarret = edtBody.SelectionStart;
            PickDataForm PickData = new PickDataForm();
            PickData.Document = this.Document;
            if (PickData.ShowDialog() == DialogResult.OK)
            {
                if (PickData.SelectedData != null)
                    edtBody.SelectedText = "<DATA." + PickData.SelectedData.Symbol + ">";
            }
        }

        public void PanelToObject()
        {
            m_Props.ListToMail = this.edtTo.Text;
            m_Props.MailSubject = this.edtSubject.Text;
            m_Props.MailBody = this.edtBody.Text;
            m_Control.SpecificProp.CopyParametersFrom(m_Props, false, Document);
            Document.Modified = true;
        }

        public void ObjectToPanel()
        {
            m_Props.CopyParametersFrom(m_Control.SpecificProp, false, Document);
            this.edtTo.Text = m_Props.ListToMail;
            this.edtSubject.Text = m_Props.MailSubject;
            this.edtBody.Text = m_Props.MailBody;
        }
    }
}

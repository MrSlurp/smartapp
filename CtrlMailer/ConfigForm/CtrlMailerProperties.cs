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

        #region events
        public event ControlPropertiesChange ControlPropertiesChanged;
        #endregion

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
            if (ControlPropertiesChanged != null)
                ControlPropertiesChanged(m_Control);
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

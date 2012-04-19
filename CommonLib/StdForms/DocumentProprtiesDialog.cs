using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CommonLib
{
    public partial class DocumentProprtiesDialog : Form
    {
        BTDoc m_Document;
        CommConfiguration m_commCfgPage;
        public DocumentProprtiesDialog()
        {
            InitializeComponent();
            m_commCfgPage = new CommConfiguration();
            m_commCfgPage.AllowRowSelect = true;
            chkDocumentUseMainContainer_CheckedChanged(null, null);
        }

        public BTDoc Document
        {
            get { return m_Document; }
            set 
            { 
                m_Document = value;
                ObjectToPanel();
            }
        }

        public void PanelToObject()
        {
            bool bDataPropChange = false;
            if (chkDocumentUseMainContainer.Checked != m_Document.UseMainContainer)
            {
                m_Document.UseMainContainer = chkDocumentUseMainContainer.Checked;
                bDataPropChange |= true;
            }
            Point pos = new Point((int)edtPosX.Value, (int)edtPosY.Value);
            if (m_Document.MCPosition != pos)
            {
                m_Document.MCPosition = pos;
                bDataPropChange |= true;
            }
            Size sz = new Size((int)edtSizeW.Value, (int)edtSizeH.Value);
            if (m_Document.MCSize != sz)
            {
                m_Document.MCSize = sz;
                bDataPropChange |= true;
            }
            if (m_Document.MCStyleShowTitleBar != chkShowTitleBar.Checked)
            {
                m_Document.MCStyleShowTitleBar = chkShowTitleBar.Checked;
                bDataPropChange |= true;
            }
            if (m_Document.MCStyleVisibleInTaskBar != chkShowInTaskBar.Checked)
            {
                m_Document.MCStyleVisibleInTaskBar = chkShowInTaskBar.Checked;
                bDataPropChange |= true;
            }

            if (bDataPropChange)
                m_Document.Modified = true;

        }

        public void ObjectToPanel()
        {
            edtProjectCnx.Text = FormatCnxString(m_Document.Communication.CommType, m_Document.Communication.CommParam);
            chkDocumentUseMainContainer.Checked = m_Document.UseMainContainer;
            edtPosX.Value = m_Document.MCPosition.X;
            edtPosY.Value = m_Document.MCPosition.Y;
            edtSizeW.Value = m_Document.MCSize.Width;
            edtSizeH.Value = m_Document.MCSize.Height;
            chkShowTitleBar.Checked = m_Document.MCStyleShowTitleBar;
            chkShowInTaskBar.Checked = m_Document.MCStyleVisibleInTaskBar;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            PanelToObject();
            this.DialogResult = DialogResult.OK;
        }

        private void btnCfgCom_Click(object sender, EventArgs e)
        {
            m_commCfgPage.CurComParam = m_Document.Communication.CommParam;
            m_commCfgPage.CurTypeCom = m_Document.Communication.CommType;
            DialogResult dlgRes = m_commCfgPage.ShowDialog();
            if (dlgRes == DialogResult.OK)
            {
                m_Document.Communication.SetCommTypeAndParam(m_commCfgPage.CurTypeCom, m_commCfgPage.CurComParam);
                m_Document.Modified = true;
                edtProjectCnx.Text = FormatCnxString(m_Document.Communication.CommType, m_Document.Communication.CommParam);
            }
        }

        private string FormatCnxString(TYPE_COMM type, string param)
        {
            return string.Format("{0}/{1}", type.ToString(), param);
        }

        private void chkDocumentUseMainContainer_CheckedChanged(object sender, EventArgs e)
        {
            groupPos.Enabled = chkDocumentUseMainContainer.Checked;
            groupStyle.Enabled = chkDocumentUseMainContainer.Checked;
        }
    }
}

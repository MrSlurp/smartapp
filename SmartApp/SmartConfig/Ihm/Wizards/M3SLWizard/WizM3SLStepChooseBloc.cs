using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SmartApp.Wizards;

namespace SmartApp.Ihm.Wizards
{
    public partial class WizM3SLStepChooseBloc : UserControl, IWizConfigForm
    {
        WizardConfigData m_WizConfig;

        public WizardConfigData WizConfig
        {
            set { m_WizConfig = value; }
        }

        public WizM3SLStepChooseBloc()
        {
            InitializeComponent();
        }

        private void cboCheckedChanged(object sender, EventArgs e)
        {
            // rien a faire
        }

        private void btnAllSLIN_Click(object sender, EventArgs e)
        {
            cboSLIN1.Checked = true;
            cboSLIN2.Checked = true;
            cboSLIN3.Checked = true;
        }

        private void btnAllSLOUT_Click(object sender, EventArgs e)
        {
            cboSLOUT1.Checked = true;
            cboSLOUT2.Checked = true;
            cboSLOUT3.Checked = true;
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            cboSLIN1.Checked = true;
            cboSLIN2.Checked = true;
            cboSLIN3.Checked = true;

            cboSLOUT1.Checked = true;
            cboSLOUT2.Checked = true;
            cboSLOUT3.Checked = true;
        }

        private void btnNone_Click(object sender, EventArgs e)
        {
            cboSLIN1.Checked = false;
            cboSLIN2.Checked = false;
            cboSLIN3.Checked = false;

            cboSLOUT1.Checked = false;
            cboSLOUT2.Checked = false;
            cboSLOUT3.Checked = false;
        }

        public void HmiToData()
        {
            m_WizConfig.SetBlocUsed(BlocsType.IN, 0, cboSLIN1.Checked);
            m_WizConfig.SetBlocUsed(BlocsType.IN, 1, cboSLIN2.Checked);
            m_WizConfig.SetBlocUsed(BlocsType.IN, 2, cboSLIN3.Checked);
            m_WizConfig.SetBlocUsed(BlocsType.OUT, 0, cboSLOUT1.Checked);
            m_WizConfig.SetBlocUsed(BlocsType.OUT, 1, cboSLOUT2.Checked);
            m_WizConfig.SetBlocUsed(BlocsType.OUT, 2, cboSLOUT3.Checked);
        }

    }
}

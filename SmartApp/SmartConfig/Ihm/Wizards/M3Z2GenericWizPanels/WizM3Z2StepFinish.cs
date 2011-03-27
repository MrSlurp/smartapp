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
    public partial class WizM3Z2StepFinish : UserControl, IWizConfigForm
    {
        WizardConfigData m_WizConfig;

        public WizardConfigData WizConfig
        {
            set { m_WizConfig = value; }
        }

        public WizM3Z2StepFinish()
        {
            InitializeComponent();
            lblTitle.Text = Program.LangSys.C("You've reach the wizard final step, here is things that will be created for you :");
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (Visible)
            {
                edtResume.Text = m_WizConfig.CreateFinalSummury();
            }
        }

        public void HmiToData()
        {
        }

    }
}

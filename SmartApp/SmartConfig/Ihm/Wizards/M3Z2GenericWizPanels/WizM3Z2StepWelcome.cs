using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SmartApp.Wizards;
using CommonLib;

namespace SmartApp.Ihm.Wizards
{
    public partial class WizM3Z2StepWelcome : UserControl, IWizConfigForm
    {
        WizardConfigData m_WizConfig;

        public WizardConfigData WizConfig
        {
            set { m_WizConfig = value; }
        }

        public WizM3Z2StepWelcome()
        {
            InitializeComponent();

        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (Visible)
            {
                lblWelcome.Text = m_WizConfig.GetWelcomeSpeech();

                pic_m3.Image = m_WizConfig.GetWelcomeImage();
            }
        }


        public void HmiToData()
        {
        }
    }
}

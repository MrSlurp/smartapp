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
    public partial class WizM3SLStepWelcome : UserControl, IWizConfigForm
    {
        WizardConfigData m_WizConfig;

        public WizardConfigData WizConfig
        {
            set { m_WizConfig = value; }
        }

        public WizM3SLStepWelcome()
        {
            InitializeComponent();
            lblWelcome.Text = Program.LangSys.C("Welcome to the Millenium 3 project wizard") +
                              "\n" +
                              Program.LangSys.C("This wizard will help you to create a new project for ") +
                              "\n" +
                              Program.LangSys.C("Millenium 3 supervision through Serial (or USB) link");

        }

        public void HmiToData()
        {
        }
    }
}

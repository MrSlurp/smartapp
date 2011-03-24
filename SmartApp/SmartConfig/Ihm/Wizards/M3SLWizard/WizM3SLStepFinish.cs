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
    public partial class WizM3SLStepFinish : UserControl, IWizConfigForm
    {
        WizardConfigData m_WizConfig;

        public WizardConfigData WizConfig
        {
            set { m_WizConfig = value; }
        }

        public WizM3SLStepFinish()
        {
            InitializeComponent();
            lblTitle.Text = Program.LangSys.C("You've reach the wizard final step, here is things that will be created for you :");
            

        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (Visible)
            {
                CreateResume();
            }
        }

        private void CreateResume()
        {
            string resume = string.Empty;
            if (m_WizConfig.HaveIOTypeUsed(BlocsType.IN))
            {
                resume = Program.LangSys.C("Frames for read and write:") + "\r\n";
                BlocConfig[] InCfg = m_WizConfig.GetBlocListByType(BlocsType.IN);
                for (int i = 0; i < InCfg.Length; i++)
                {
                    if (InCfg[i].IsUsed)
                    {
                        if (i != 0)
                            resume += ", ";
                        resume += InCfg[i].Name;
                    }
                }
                resume += "\r\n" + Program.LangSys.C("(All will be included in a single function)");
                resume += "\r\n";
            }
            if (m_WizConfig.HaveIOTypeUsed(BlocsType.OUT))
            {
                resume += Program.LangSys.C("Frames for read:") + "\n";
                BlocConfig[] InCfg = m_WizConfig.GetBlocListByType(BlocsType.OUT);
                for (int i = 0; i < InCfg.Length; i++)
                {
                    if (i != 0)
                        resume += ",";
                    if (InCfg[i].IsUsed)
                    {
                        string format = Program.LangSys.C("{0} bloc");
                        resume += string.Format(format, InCfg[i].Name);
                    }
                }
                resume += "\r\n" + Program.LangSys.C("(All will be included in a single function)");
                resume += "\r\n" + Program.LangSys.C("A timer will be created for periodically reading SL out bloc");
                resume += "\r\n";
            }
            resume += "\r\n" + Program.LangSys.C("All datas corresponding to your configurations");
            resume += "\r\n";
            resume += "\r\n" + Program.LangSys.C("A default screen with needed initialisations");

            edtResume.Text = resume;
        }

        public void HmiToData()
        {
        }

    }
}

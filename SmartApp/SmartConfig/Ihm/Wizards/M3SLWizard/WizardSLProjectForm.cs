using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartApp.Wizards;

namespace SmartApp.Ihm.Wizards
{
    public partial class WizardSLProjectForm : Form
    {
        enum SLWizardSteps
        {
            Welcome = 0,
            Step1 = 1,
            Step2 = 2,
            Step3 = 3,
            Step4 = 4,
            Final = 5,
        }

        IWizConfigForm m_CurrentDispPanel = null;
        SLWizardSteps m_CurrentState = SLWizardSteps.Welcome;
        WizM3SLStepChooseBloc m_UCStepChooseBloc = new WizM3SLStepChooseBloc();
        WizM3SLStepWelcome m_UCStepWelcome = new WizM3SLStepWelcome();
        WiM3SLStepConfigIOSplit m_UCStepConfigINSplit = new WiM3SLStepConfigIOSplit(BlocsType.IN);
        WiM3SLStepConfigIOSplit m_UCStepConfigOUTSplit = new WiM3SLStepConfigIOSplit(BlocsType.OUT);
        List<IWizConfigForm> m_listWizPanel = new List<IWizConfigForm>();

        WizardConfigData m_WizConfigData = new SLWizardConfigData();

        public WizardSLProjectForm()
        {
            InitializeComponent();
            btnNext.Text = Program.LangSys.C("Next");
            btnPrev.Text = Program.LangSys.C("Previous");
            SuspendLayout();

            m_listWizPanel.Add(m_UCStepWelcome);
            this.Controls.Add(m_UCStepWelcome);

            m_listWizPanel.Add(m_UCStepChooseBloc);
            this.Controls.Add(m_UCStepChooseBloc);

            m_listWizPanel.Add(m_UCStepConfigINSplit);
            this.Controls.Add(m_UCStepConfigINSplit);

            m_listWizPanel.Add(m_UCStepConfigOUTSplit);
            this.Controls.Add(m_UCStepConfigOUTSplit);

            for (int i = 0; i < m_listWizPanel.Count; i++)
            {
                m_listWizPanel[i].WizConfig = m_WizConfigData;
                PlaceWizForm(m_listWizPanel[i]);
            }
            ResumeLayout();

            StateMachine();
        }

        private void PlaceWizForm(IWizConfigForm frm)
        {
            frm.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
            frm.Width = this.ClientSize.Width;
            frm.Visible = false;
        }

        private void UpdatePrevNextBtnStates()
        {
            switch (m_CurrentState)
            {
                case SLWizardSteps.Welcome:
                    btnNext.Enabled = true;
                    btnPrev.Enabled = false;
                    break;
                case SLWizardSteps.Step1:
                case SLWizardSteps.Step2:
                case SLWizardSteps.Step3:
                    btnPrev.Enabled = true;
                    break;
                case SLWizardSteps.Step4:
                    btnNext.Text = Program.LangSys.C("Next");
                    break;
                case SLWizardSteps.Final:
                    btnNext.Text = Program.LangSys.C("Finish");
                    break;
            }
        }

        private void StateMachine()
        {
            if (m_CurrentDispPanel != null)
                m_CurrentDispPanel.HmiToData();

            ShowStepPanel(m_CurrentState);

            UpdatePrevNextBtnStates();
        }

        private void ShowStepPanel(SLWizardSteps step)
        {
            for (int i = 0; i < m_listWizPanel.Count; i++)
            {
                m_listWizPanel[i].Visible = false;
            }
            if ((int)step >= 0 && (int)step < m_listWizPanel.Count)
            {
                m_listWizPanel[(int)step].Visible = true;
                m_CurrentDispPanel = m_listWizPanel[(int)step];
            }
            else
                m_CurrentDispPanel = null;
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (m_CurrentState > SLWizardSteps.Welcome)
                m_CurrentState -= 1;
            StateMachine();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (m_CurrentState < SLWizardSteps.Final)
                m_CurrentState += 1;
            StateMachine();
        }


    }
}
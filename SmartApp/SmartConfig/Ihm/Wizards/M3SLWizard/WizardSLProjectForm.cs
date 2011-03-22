using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

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

        ISLWizConfigForm m_CurrentDispForm = null;
        SLWizardSteps m_CurrentState = SLWizardSteps.Welcome;
        WizM3SLStepChooseBloc m_UCStepChooseBloc = new WizM3SLStepChooseBloc();
        WizM3SLStepWelcome m_UCStepWelcome = new WizM3SLStepWelcome();
        WiM3SLStepConfigIOSplit m_UCStepConfigINSplit = new WiM3SLStepConfigIOSplit(BlocsType.IN);
        WiM3SLStepConfigIOSplit m_UCStepConfigOUTSplit = new WiM3SLStepConfigIOSplit(BlocsType.OUT);

        SLWizardConfigData m_WizConfigData = new SLWizardConfigData();

        public WizardSLProjectForm()
        {
            InitializeComponent();
            btnNext.Text = Program.LangSys.C("Next");
            btnPrev.Text = Program.LangSys.C("Previous");
            SuspendLayout();

            PlaceWizForm(m_UCStepWelcome);
            m_UCStepWelcome.WizConfig = m_WizConfigData;
            this.Controls.Add(m_UCStepWelcome);

            PlaceWizForm(m_UCStepChooseBloc);
            m_UCStepChooseBloc.WizConfig = m_WizConfigData;
            this.Controls.Add(m_UCStepChooseBloc);

            PlaceWizForm(m_UCStepConfigINSplit);
            m_UCStepConfigINSplit.WizConfig = m_WizConfigData;
            this.Controls.Add(m_UCStepConfigINSplit);

            PlaceWizForm(m_UCStepConfigOUTSplit);
            m_UCStepConfigOUTSplit.WizConfig = m_WizConfigData;
            this.Controls.Add(m_UCStepConfigOUTSplit);

            ResumeLayout();
            StateMachine();
        }

        private void PlaceWizForm(UserControl frm)
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
            if (m_CurrentDispForm != null)
                m_CurrentDispForm.HmiToData();
            switch (m_CurrentState)
            {
                case SLWizardSteps.Welcome:
                    m_UCStepWelcome.Visible = true;
                    m_UCStepChooseBloc.Visible = false;
                    m_CurrentDispForm = m_UCStepWelcome;
                    break;
                case SLWizardSteps.Step1:
                    m_UCStepWelcome.Visible = false;
                    m_UCStepChooseBloc.Visible = true;
                    m_UCStepConfigINSplit.Visible = false;
                    m_CurrentDispForm = m_UCStepChooseBloc;
                    break;
                case SLWizardSteps.Step2:
                    m_UCStepConfigINSplit.Visible = true;
                    m_UCStepChooseBloc.Visible = false;
                    m_UCStepConfigOUTSplit.Visible = false;
                    m_CurrentDispForm = m_UCStepConfigINSplit;
                    break;
                case SLWizardSteps.Step3:
                    m_UCStepConfigINSplit.Visible = false;
                    m_UCStepConfigOUTSplit.Visible = true;
                    m_CurrentDispForm = m_UCStepConfigOUTSplit;
                    break;
                case SLWizardSteps.Step4:
                    m_UCStepConfigOUTSplit.Visible = false;
                    m_CurrentDispForm = null;
                    break;
                case SLWizardSteps.Final:
                    break;
            }
            UpdatePrevNextBtnStates();
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
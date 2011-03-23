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
        /// <summary>
        /// enum des etapes de wizard
        /// </summary>
        enum SLWizardSteps
        {
            Welcome = 0,
            Step1 = 1, // configuration des bloc utilisés
            Step2 = 2, // configuration des split IN
            Step3 = 3, // configuration des split OUT
            Step4 = 4, // configuration des nom des sorties supervision (entree programme)
            Step5 = 5, // configuration des nom des entree supervision (sortie programme)
            Final = 6, // resumé avant géneration
        }

        /// <summary>
        /// panel affiché
        /// </summary>
        IWizConfigForm m_CurrentDispPanel = null;

        /// <summary>
        /// Etat courant du wizard
        /// </summary>
        SLWizardSteps m_CurrentState = SLWizardSteps.Welcome;

        /// <summary>
        /// Panel de bienvenu dans le wizard
        /// </summary>
        WizM3SLStepWelcome m_UCStepWelcome = new WizM3SLStepWelcome();

        /// <summary>
        /// panel de l'étape de choix du bloc
        /// </summary>
        WizM3SLStepChooseBloc m_UCStepChooseBloc = new WizM3SLStepChooseBloc();

        /// <summary>
        /// panel de config des split IN
        /// </summary>
        WizM3SLStepConfigIOSplit m_UCStepConfigINSplit = new WizM3SLStepConfigIOSplit(BlocsType.IN);

        /// <summary>
        /// panel de config des split OUT
        /// </summary>
        WizM3SLStepConfigIOSplit m_UCStepConfigOUTSplit = new WizM3SLStepConfigIOSplit(BlocsType.OUT);

        /// <summary>
        /// panel de config des split IN
        /// </summary>
        WizM3SLStepConfigIOName m_UCStepConfigInName = new WizM3SLStepConfigIOName(BlocsType.IN);
        /// <summary>
        /// panel de config des split OUT
        /// </summary>
        WizM3SLStepConfigIOName m_UCStepConfigOutName = new WizM3SLStepConfigIOName(BlocsType.OUT);

        /// <summary>
        /// liste des panels du wzard
        /// </summary>
        List<IWizConfigForm> m_listWizPanel = new List<IWizConfigForm>();

        /// <summary>
        /// données de configuration du wizard
        /// </summary>
        WizardConfigData m_WizConfigData = new SLWizardConfigData();

        /// <summary>
        /// constructeur de la form
        /// </summary>
        public WizardSLProjectForm()
        {
            InitializeComponent();
            // init des bouton du wizard
            btnPrev.Text = Program.LangSys.C("Previous");

            // init des panel du wizard
            SuspendLayout();
            m_listWizPanel.Add(m_UCStepWelcome);
            this.Controls.Add(m_UCStepWelcome);

            m_listWizPanel.Add(m_UCStepChooseBloc);
            this.Controls.Add(m_UCStepChooseBloc);

            m_listWizPanel.Add(m_UCStepConfigINSplit);
            this.Controls.Add(m_UCStepConfigINSplit);

            m_listWizPanel.Add(m_UCStepConfigOUTSplit);
            this.Controls.Add(m_UCStepConfigOUTSplit);

            m_listWizPanel.Add(m_UCStepConfigInName);
            this.Controls.Add(m_UCStepConfigInName);

            m_listWizPanel.Add(m_UCStepConfigOutName);
            this.Controls.Add(m_UCStepConfigOutName);

            for (int i = 0; i < m_listWizPanel.Count; i++)
            {
                m_listWizPanel[i].WizConfig = m_WizConfigData;
                PlaceWizForm(m_listWizPanel[i]);
            }
            ResumeLayout();

            StateMachine();
        }

        /// <summary>
        /// position les panel de wizard dans la form
        /// </summary>
        /// <param name="frm">panel à positionner</param>
        private void PlaceWizForm(IWizConfigForm frm)
        {
            frm.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
            frm.Width = this.ClientSize.Width;
            frm.Visible = false;
        }

        /// <summary>
        /// met a jour l'état des boutons en fonction de l'étape
        /// </summary>
        private void UpdatePrevNextBtnStates()
        {
            int count = Enum.GetValues(typeof(SLWizardSteps)).Length;
            if (m_CurrentState == SLWizardSteps.Final)
                btnNext.Text = Program.LangSys.C("Finish");
            else
                btnNext.Text = Program.LangSys.C("Next");

            if (m_CurrentState == 0)
                btnPrev.Enabled = false;
            else
                btnPrev.Enabled = true;
        }

        /// <summary>
        /// gère les changement d'étapes
        /// </summary>
        private void StateMachine()
        {
            if (m_CurrentDispPanel != null)
                m_CurrentDispPanel.HmiToData();

            ShowStepPanel(m_CurrentState);

            UpdatePrevNextBtnStates();
        }

        /// <summary>
        /// affiche le panel correspondant à l'étape en cours
        /// </summary>
        /// <param name="step"></param>
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

        /// <summary>
        /// bouton précédent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (m_CurrentState > SLWizardSteps.Welcome)
                m_CurrentState -= 1;
            StateMachine();
        }

        /// <summary>
        /// bouton suivant
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (m_CurrentState < SLWizardSteps.Final)
            {
                m_CurrentState += 1;
                StateMachine();
            }
            else
            {
                // todo ==> generation de tout
                DialogResult = DialogResult.OK;
            }

        }


    }
}
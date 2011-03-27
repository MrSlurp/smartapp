using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartApp.Wizards;
using CommonLib;

namespace SmartApp.Ihm.Wizards
{
    public partial class WizardM3Z2ProjectForm : Form
    {
        /// <summary>
        /// panel affiché
        /// </summary>
        IWizConfigForm m_CurrentDispPanel = null;

        /// <summary>
        /// Etat courant du wizard
        /// </summary>
        int m_CurrentStep = 0;

        /// <summary>
        /// Panel de bienvenue dans le wizard
        /// </summary>
        WizM3Z2StepWelcome m_UCStepWelcome = new WizM3Z2StepWelcome();

        /// <summary>
        /// panel de l'étape de choix du bloc
        /// </summary>
        WizM3Z2StepChooseBloc m_UCStepChooseBloc = new WizM3Z2StepChooseBloc();

        /// <summary>
        /// panel de config des split IN
        /// </summary>
        WizM3Z2StepConfigIOSplit m_UCStepConfigINSplit = new WizM3Z2StepConfigIOSplit(BlocsType.IN);

        /// <summary>
        /// panel de config des split OUT
        /// </summary>
        WizM3Z2StepConfigIOSplit m_UCStepConfigOUTSplit = new WizM3Z2StepConfigIOSplit(BlocsType.OUT);

        /// <summary>
        /// panel de config des split IN
        /// </summary>
        WizM3Z2StepConfigIOName m_UCStepConfigInName = new WizM3Z2StepConfigIOName(BlocsType.IN);
        /// <summary>
        /// panel de config des split OUT
        /// </summary>
        WizM3Z2StepConfigIOName m_UCStepConfigOutName = new WizM3Z2StepConfigIOName(BlocsType.OUT);

        /// <summary>
        /// Panel de fin du wizard
        /// </summary>
        WizM3Z2StepFinish m_UCStepFinish = new WizM3Z2StepFinish();


        /// <summary>
        /// liste des panels du wzard
        /// </summary>
        List<IWizConfigForm> m_listWizPanel = new List<IWizConfigForm>();

        /// <summary>
        /// données de configuration du wizard
        /// </summary>
        WizardConfigData m_WizConfigData;// = new SLWizardConfigData();

        /// <summary>
        /// constructeur de la form
        /// </summary>
        public WizardM3Z2ProjectForm(WizardConfigData WizCfgData)
        {
            m_WizConfigData = WizCfgData;
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
            m_UCStepConfigINSplit.TypeSplitImage1 = Resources.TypeSplit16;
            m_UCStepConfigINSplit.TypeSplitLabel1 = Program.LangSys.C("Split to 16");
            m_UCStepConfigINSplit.TypeSplitImage2 = Resources.TypeSplit4;
            m_UCStepConfigINSplit.TypeSplitLabel2 = Program.LangSys.C("Split to 4");
            m_UCStepConfigINSplit.TypeSplitImage3 = Resources.TypeSplit2;
            m_UCStepConfigINSplit.TypeSplitLabel3 = Program.LangSys.C("Split to 2");

            m_listWizPanel.Add(m_UCStepConfigOUTSplit);
            this.Controls.Add(m_UCStepConfigOUTSplit);
            m_UCStepConfigOUTSplit.TypeSplitImage1 = Resources.TypeSplit16;
            m_UCStepConfigOUTSplit.TypeSplitLabel1 = Program.LangSys.C("Split to 16");
            m_UCStepConfigOUTSplit.TypeSplitLabel2 = string.Empty;
            m_UCStepConfigOUTSplit.TypeSplitLabel3 = string.Empty;

            m_listWizPanel.Add(m_UCStepConfigInName);
            this.Controls.Add(m_UCStepConfigInName);

            m_listWizPanel.Add(m_UCStepConfigOutName);
            this.Controls.Add(m_UCStepConfigOutName);

            m_listWizPanel.Add(m_UCStepFinish);
            this.Controls.Add(m_UCStepFinish);

            for (int i = 0; i < m_listWizPanel.Count; i++)
            {
                m_listWizPanel[i].WizConfig = m_WizConfigData;
                PlaceWizForm(m_listWizPanel[i]);
            }
            ResumeLayout();

            StepMachine();
        }

        /// <summary>
        /// position les panel de wizard dans la form
        /// </summary>
        /// <param name="frm">panel à positionner</param>
        private void PlaceWizForm(IWizConfigForm frm)
        {
            frm.Width = this.ClientSize.Width;
            frm.Height = this.ClientSize.Height - (this.ClientSize.Height - btnCancel.Top) -10;
            frm.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            frm.Visible = false;
        }

        /// <summary>
        /// met a jour l'état des boutons en fonction de l'étape
        /// </summary>
        private void UpdatePrevNextBtnStates()
        {
            if (m_CurrentStep == m_listWizPanel.Count)
                btnNext.Text = Program.LangSys.C("Finish");
            else
                btnNext.Text = Program.LangSys.C("Next");

            if (m_CurrentStep == 0)
                btnPrev.Enabled = false;
            else
                btnPrev.Enabled = true;
        }

        /// <summary>
        /// gère les changement d'étapes
        /// </summary>
        private void StepMachine()
        {
            if (m_CurrentDispPanel != null)
                m_CurrentDispPanel.HmiToData();

            ShowStepPanel(m_CurrentStep);

            UpdatePrevNextBtnStates();
        }

        /// <summary>
        /// affiche le panel correspondant à l'étape en cours
        /// </summary>
        /// <param name="step"></param>
        private void ShowStepPanel(int step)
        {
            for (int i = 0; i < m_listWizPanel.Count; i++)
            {
                m_listWizPanel[i].Visible = false;
            }
            if ((int)step >= 0 && step < m_listWizPanel.Count)
            {
                m_listWizPanel[step].Visible = true;
                m_CurrentDispPanel = m_listWizPanel[step];
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
            if (m_CurrentStep > 0)
                m_CurrentStep -= 1;
            StepMachine();
        }

        /// <summary>
        /// bouton suivant
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (m_CurrentStep < m_listWizPanel.Count)
            {
                m_CurrentStep += 1;
                StepMachine();
            }
            else
            {
                // todo ==> generation de tout
                DialogResult = DialogResult.OK;
            }

        }

        public void CreateAllFromWizardData(BaseM3Z2ProjectCreator PrjCreator)
        {
            PrjCreator.WizConfig = m_WizConfigData;
            PrjCreator.CreateProjectFromWizConfig();
        }


    }
}
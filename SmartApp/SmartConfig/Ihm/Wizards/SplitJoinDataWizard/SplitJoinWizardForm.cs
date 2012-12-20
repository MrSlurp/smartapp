using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartApp.Wizards;
using CommonLib;

namespace SmartApp.Wizards
{
    public partial class SplitJoinWizardForm : Form
    {
        /// <summary>
        /// panel affiché
        /// </summary>
        ISJWizForm m_CurrentDispPanel = null;

        /// <summary>
        /// Etat courant du wizard
        /// </summary>
        int m_CurrentStep = 0;

        /// <summary>
        /// Panel de bienvenue dans le wizard
        /// </summary>
        SJWizWelcomeStep m_UCStepWelcome = new SJWizWelcomeStep();

        
        /// <summary>
        /// panel de choix de l'operation
        /// </summary>
        SJWizSelectOperation m_UCStepSelectOp = new SJWizSelectOperation();

        /// <summary>
        /// panel de config des split IN
        /// </summary>
        SJWizSelectDataStep m_UCStepSelectData = new SJWizSelectDataStep();

        /// <summary>
        /// panel de config des split OUT
        /// </summary>
        SJWizNewDataName m_UCStepNameOutData = new SJWizNewDataName();

        
        /// <summary>
        /// panel de config des split IN
        /// </summary>
        SJWizPreviewChange m_UCStepPreviewChange = new SJWizPreviewChange();
        

        /// <summary>
        /// liste des panels du wzard
        /// </summary>
        List<ISJWizForm> m_listWizPanel = new List<ISJWizForm>();

        /// <summary>
        /// données de configuration du wizard
        /// </summary>
        SJDataWizardManager m_WizConfigData;

        public SplitJoinWizardForm(BTDoc doc)
        {
            m_WizConfigData = new SJDataWizardManager(doc);
            InitializeComponent();
            // init des bouton du wizard
            btnPrev.Text = Program.LangSys.C("Previous");

            SuspendLayout();
            m_listWizPanel.Add(m_UCStepWelcome);
            this.Controls.Add(m_UCStepWelcome);

            m_listWizPanel.Add(m_UCStepSelectOp);
            this.Controls.Add(m_UCStepSelectOp);

            m_listWizPanel.Add(m_UCStepSelectData);
            this.Controls.Add(m_UCStepSelectData);

            m_listWizPanel.Add(m_UCStepNameOutData);
            this.Controls.Add(m_UCStepNameOutData);

            m_listWizPanel.Add(m_UCStepPreviewChange);
            this.Controls.Add(m_UCStepPreviewChange);

            for (int i = 0; i < m_listWizPanel.Count; i++)
            {
                m_listWizPanel[i].SJManager = m_WizConfigData;
                PlaceWizForm(m_listWizPanel[i]);
            }
            ResumeLayout();

            StepMachine();
        }

        /// <summary>
        /// position les panel de wizard dans la form
        /// </summary>
        /// <param name="frm">panel à positionner</param>
        private void PlaceWizForm(ISJWizForm frm)
        {
            frm.Width = this.ClientSize.Width;
            frm.Height = this.ClientSize.Height - (this.ClientSize.Height - btnCancel.Top) - 10;
            frm.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            frm.Visible = false;
        }

        /// <summary>
        /// met a jour l'état des boutons en fonction de l'étape
        /// </summary>
        private void UpdatePrevNextBtnStates()
        {
            if (m_CurrentStep == m_listWizPanel.Count - 1)
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
                m_listWizPanel[step].DataToHmi();
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
            if (m_CurrentDispPanel.AllowGoNext)
            {
                if (m_CurrentStep < m_listWizPanel.Count - 1)
                {
                    m_CurrentStep += 1;
                    StepMachine();
                }
                else
                {
                    DialogResult = DialogResult.OK;
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartApp.Ihm;
using SmartApp.Ihm.Wizards;
using SmartApp.Wizards;
using System.Reflection;
using SmartApp.Ihm.Designer;
using CommonLib;

namespace SmartApp.Ihm
{
    public delegate void AsyncUpdateHMI(MessNeedUpdate Mess);

    /// <summary>
    /// fenêtre principale de SmartConfig
    /// </summary>
    public partial class MDISmartConfigMain : Form
    {
        #region données membres
        private const string APP_TITLE = "SmartConfig";
        protected delegate void UpdateTitleDg(string str);
        private DragItemPanel m_panelToolDragItem;
        protected TraceConsole m_TraceConsole;
        protected List<DesignerForm> m_ListDesignForm = new List<DesignerForm>();
        protected SolutionGest m_GestSolution;
        protected FormsOptions m_FrmOpt = new FormsOptions(); 
        protected MruStripMenuInline m_mruStripMenu;
        static BasePropertiesDialog m_PropDialog;

        #endregion

        public static BasePropertiesDialog GlobalPropDialog
        {
            get
            {
                if (m_PropDialog == null)
                {
                    m_PropDialog = new BasePropertiesDialog();
                }
                return m_PropDialog;
            }
        }
        

        #region constructeurs
        /// <summary>
        /// 
        /// </summary>
        public MDISmartConfigMain()
        {
            Program.LangSys.Initialize(this);
            InitializeComponent();
            CommonConstructorInit();
            tsbtnConfigCom.Visible = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FileName"></param>
        public MDISmartConfigMain(string FileName)
        {
            Program.LangSys.Initialize(this);
            InitializeComponent();
            CommonConstructorInit();
            SolutionOpen(FileName);
        }

        /// <summary>
        /// 
        /// </summary>
        private void CommonConstructorInit()
        {
            this.Text = APP_TITLE;

            this.Icon = CommonLib.Resources.AppIcon;
            string strAppDir = Application.StartupPath;
            string strIniFilePath = PathTranslator.LinuxVsWindowsPathUse(strAppDir + @"\" + Cste.STR_FORMPOSINI_FILENAME);
            m_FrmOpt.Load(strIniFilePath);
            m_mruStripMenu = new MruStripMenuInline(this.menuFile, this.menuItemMruFiles, new MruStripMenu.ClickedHandler(OnMruFile), strIniFilePath);
            if (LaunchArgParser.DevMode)
            {
                menuItemTraceConfig.Visible = true;
                menuItemaddBridge.Visible = true;
                menuItemOpenDebugConsole.Visible = true;
            }
            CentralizedFileDlg.InitImgFileDialog(Application.StartupPath);
            CentralizedFileDlg.InitPrjFileDialog(Application.StartupPath);
            CentralizedFileDlg.InitSolFileDialog(Application.StartupPath);
            CentralizedFileDlg.ActiveProjectPath = Application.StartupPath;
            UpdateFileMenu();

            m_panelToolDragItem = new DragItemPanel();
            // 
            // m_panelToolDragItem
            // 
            m_panelToolDragItem.AutoScroll = true;
            m_panelToolDragItem.BackColor = System.Drawing.Color.Transparent;
            m_panelToolDragItem.Dock = System.Windows.Forms.DockStyle.Fill;
            m_panelToolDragItem.Name = "m_panelToolDragItem";
            toolsPanel.Controls.Add(m_panelToolDragItem);
        }
        #endregion

        #region handler d'event du gestionnaire de solution
        /// <summary>
        /// handler de demande d'édition d'un écran d'un document
        /// </summary>
        /// <param name="screenName"></param>
        /// <param name="document"></param>
        void GestSolution_OnDocScreenEdit(string screenName, BTDoc document)
        {
            BTScreen scr = document.GestScreen.GetFromSymbol(screenName) as BTScreen;
            foreach (DesignerForm form in this.MdiChildren)
            {
                if (form.Doc == document && form.CurrentScreen == scr)
                {
                    form.BringToFront();
                    return;
                }
            }
            DesignerForm DesignForm = new DesignerForm();
            m_ListDesignForm.Add(DesignForm);
            DesignForm.FormClosed += new FormClosedEventHandler(DesignFormClosed);
            DesignForm.MdiParent = this;
            DesignForm.Doc = document;
            DesignForm.CurrentScreen = scr ;
            DesignForm.Show();
        }
        #endregion

        #region handlers menu view

        /// <summary>
        /// handler du bouton de menu "masquer/afficher" la barre d'outil
        /// </summary>
        /// <param name="sender">standard</param>
        /// <param name="e">standard</param>
        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_toolStrip.Visible = menuItemViewHideToolbar.Checked;
        }

        /// <summary>
        /// handler du bouton de menu "masquer/afficher" la barre de status
        /// </summary>
        /// <param name="sender">standard</param>
        /// <param name="e">standard</param>
        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = menuItemViewHideStatusBar.Checked;
        }
        #endregion

        #region handlers réarangement des fenêtres
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TileVerticleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }
        #endregion

        #region fonctions de gestion d'ouverture/fermeture du document
        /// <summary>
        /// affiche une message box indiquant que le fichier a été modifié
        /// et propose le choix de sauvegarder/annuler/ignorer
        /// </summary>
        /// <returns>réponse de l'utilisateur</returns>
        private DialogResult ShowFileModifiedMessagebox()
        {
            return MessageBox.Show(Program.LangSys.C("File have been modified") 
                                                   + "\n" + 
                                                   Program.LangSys.C("Do you want to save it?"), 
                                                   Program.LangSys.C("Warning"),
                                                   MessageBoxButtons.YesNoCancel,
                                                   MessageBoxIcon.Question);
        }

        /// <summary>
        /// affiche une message box à l'utilisateur demandant la sauvegarde 
        /// si la solution à été modifiée
        /// </summary>
        private void SolutionAskUserToSaveIfIsModified()
        {
            if (m_GestSolution != null)
            {
                if (m_GestSolution.HaveModifiedDocument)
                {
                    DialogResult res = ShowFileModifiedMessagebox();
                    if (res == DialogResult.Yes)
                    {
                        SolutionSave(false);
                        UpdateTitle();
                    }
                    if (res == DialogResult.Cancel)
                    {
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void SolutionSave(bool bforceAskFileName)
        {
            if (m_GestSolution != null)
            {
                if (string.IsNullOrEmpty(m_GestSolution.FilePath) || bforceAskFileName)
                {
                    DialogResult dlgRes = CentralizedFileDlg.ShowSaveSolFileDilaog();
                    if (dlgRes == DialogResult.OK)
                    {
                        string strFileFullName = CentralizedFileDlg.SolSaveFileName;
                        string DossierFichier = Path.GetDirectoryName(strFileFullName);
                        CentralizedFileDlg.InitImgFileDialog(DossierFichier);
                        CentralizedFileDlg.InitPrjFileDialog(DossierFichier);
                        CentralizedFileDlg.InitSolFileDialog(DossierFichier);
                        CentralizedFileDlg.ActiveProjectPath = DossierFichier;
                        m_GestSolution.SaveAllDocumentsAndSolution(strFileFullName);
                        this.m_mruStripMenu.AddFile(strFileFullName);
                        m_GestSolution.HaveModifiedDocument = false;
                    }
                }
                else
                {
                    m_GestSolution.SaveAllDocumentsAndSolution();
                    UpdateTitle();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void SolutionOpen(string strSolutionPath)
        {
            if (string.IsNullOrEmpty(strSolutionPath))
            {
                DialogResult dlgRes = CentralizedFileDlg.ShowOpenSolFileDilaog();
                if (dlgRes == DialogResult.OK)
                    strSolutionPath = CentralizedFileDlg.SolOpenFileName;
            }
            if (m_GestSolution != null)
            {
                System.Diagnostics.Debug.Assert(false);
            }
            if (!string.IsNullOrEmpty(strSolutionPath))
            {
                m_GestSolution = new SolutionGest(Program.TypeApp, Program.DllGest);
                solutionTreeView.SolutionGest = m_GestSolution;
                string DossierFichier = Path.GetDirectoryName(strSolutionPath);
                CentralizedFileDlg.InitImgFileDialog(DossierFichier);
                CentralizedFileDlg.InitPrjFileDialog(DossierFichier);
                CentralizedFileDlg.InitSolFileDialog(DossierFichier);
                CentralizedFileDlg.ActiveProjectPath = DossierFichier;
                m_GestSolution.ReadInSolution(strSolutionPath);
                m_GestSolution.OnDocScreenEdit += new SolutionGest.DocumentScreenEditHandler(GestSolution_OnDocScreenEdit);
                m_GestSolution.OnDocumentChanged += new SolutionGest.SolutionDocumentChangedEventHandler(UpdateTitle);
                m_GestSolution.OnDocClosed += new SolutionGest.DocumentOpenCloseEventHandler(GestSolution_OnDocClosed);
                m_mruStripMenu.AddFile(strSolutionPath);
                UpdateTitle();
                UpdateFileMenu();
            }
        }

        void GestSolution_OnDocClosed(BaseDoc doc)
        {
            List<DesignerForm> toDelList = new List<DesignerForm>();
            foreach (DesignerForm frm in m_ListDesignForm)
            {
                if (frm.Doc == doc)
                {
                    toDelList.Add(frm);
                }
            }
            foreach (DesignerForm frm in toDelList)
            {
                m_ListDesignForm.Remove(frm);
                frm.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void SolutionNew()
        {
            DialogResult dlgRes = CentralizedFileDlg.ShowSaveSolFileDilaog();
            if (dlgRes == DialogResult.OK)
            {
                m_GestSolution = new SolutionGest(Program.TypeApp, Program.DllGest);
                solutionTreeView.SolutionGest = m_GestSolution;
                m_GestSolution.SaveAllDocumentsAndSolution(CentralizedFileDlg.SolSaveFileName);
                string DossierFichier = Path.GetDirectoryName(m_GestSolution.FilePath);
                CentralizedFileDlg.InitImgFileDialog(DossierFichier);
                CentralizedFileDlg.InitPrjFileDialog(DossierFichier);
                CentralizedFileDlg.InitSolFileDialog(DossierFichier);
                m_GestSolution.OnDocScreenEdit += new SolutionGest.DocumentScreenEditHandler(GestSolution_OnDocScreenEdit);
                m_GestSolution.OnDocumentChanged += new SolutionGest.SolutionDocumentChangedEventHandler(UpdateTitle);
                m_mruStripMenu.AddFile(m_GestSolution.FilePath);
                UpdateTitle();
                UpdateFileMenu();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void SolutionClose()
        {
            for (int i = 0;i < m_ListDesignForm.Count; i++)
            {
                m_ListDesignForm[i].Close();
            }
            m_ListDesignForm.Clear();
            m_GestSolution = null;
            solutionTreeView.SolutionGest = null;
            UpdateFileMenu();
        }

        /// <summary>
        /// 
        /// </summary>
        private void ExitSmartConfig()
        {
            SolutionAskUserToSaveIfIsModified();
            this.SolutionClose();
            this.Close();
        }
        #endregion

        #region gestion de la liste de fichier récents
        /// <summary>
        /// gestion des most recent used
        /// </summary>
        /// <param name="number"></param>
        /// <param name="filename"></param>
        private void OnMruFile(int number, String filename)
        {
            if (!File.Exists(filename))
            {
                MessageBox.Show(string.Format(Program.LangSys.C("The file '{0}' cannot be opened and will be removed from the recent list"), filename)
                    , Program.LangSys.C("Error")
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Error);
                m_mruStripMenu.RemoveFile(number);
            }
            else
            {
                SolutionAskUserToSaveIfIsModified();
                SolutionClose();
                SolutionOpen(filename);
                m_mruStripMenu.SetFirstFile(number);
            }
        }
        #endregion

        #region handler d'event menu caché développeur
        /// <summary>
        /// affiche le panel de configuration des logs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_tsMenuLogConfig_Click(object sender, EventArgs e)
        {
            LogCatForm LogForm = new LogCatForm();
            LogForm.Level = (TracesLevel)SmartApp.Properties.Settings.Default.LogLevel;
            LogForm.ActiveCats = (TraceCat)Convert.ToInt32(SmartApp.Properties.Settings.Default.LogCat, 16);
            LogForm.LogToFile = SmartApp.Properties.Settings.Default.LogToFile;
            if (LogForm.ShowDialog() == DialogResult.OK)
            {
                SmartApp.Properties.Settings.Default.LogLevel = (int)LogForm.Level;
                SmartApp.Properties.Settings.Default.LogCat = Convert.ToString((int)LogForm.ActiveCats, 16);
                SmartApp.Properties.Settings.Default.LogToFile = LogForm.LogToFile;
                SmartApp.Properties.Settings.Default.Save();
                Traces.Cats = LogForm.ActiveCats;
                Traces.Level = LogForm.Level;
                Traces.LogToFile = LogForm.LogToFile;
            }
        }

        /// <summary>
        /// ouvre la console d'affichage des logs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_tsMenuOpenDebugConsole_Click(object sender, EventArgs e)
        {
            if (m_TraceConsole == null)
                m_TraceConsole = new TraceConsole();
            else
            {
                m_TraceConsole.Dispose();
                m_TraceConsole = new TraceConsole();
            }
            m_TraceConsole.Show();
        }
        #endregion

        #region Update divers
        /// <summary>
        /// Met a jour le titre de l'application avec le nom du document courant
        /// (si il y a) et le flag de modification
        /// </summary>
        public void UpdateTitle()
        {
            string strTitle = APP_TITLE;
            if (m_GestSolution != null)
            {
                if (m_GestSolution.HaveModifiedDocument)
                {
                    strTitle += "*";
                }
            }
            if (this.InvokeRequired)
            {
                UpdateTitleDg dg = new UpdateTitleDg(SetTitle);
                this.Invoke(dg, strTitle);
            }
            else
            {
                this.Text = strTitle;
            }
        }

        /// <summary>
        /// fonction qui n'a lieu d'être qu'en cas d'appel asynchrone
        /// </summary>
        /// <param name="str"></param>
        private void SetTitle(string str)
        {
            this.Text = str;
        }

        /// <summary>
        /// appelé lorsqu'une opération nécessite la mise a jour de l'intégralité des IHM
        /// (modification ayant un impacte dans toutes les formes)
        /// </summary>
        /// <param name="Mess"></param>
        protected void OnNeedUpdateHMI(MessNeedUpdate Mess)
        {
            if (this.InvokeRequired)
            {
                AsyncUpdateHMI AsyncCall = new AsyncUpdateHMI(AsyncUpdater);
                this.Invoke(AsyncCall, Mess);
            }
            else
            {
                AsyncUpdater(Mess);
            }
        }

        /// <summary>
        /// met a jour l'état des différentes formes de l'appplication
        /// </summary>
        /// <param name="Mess"></param>
        protected void AsyncUpdater(MessNeedUpdate Mess)
        {
            if (Mess == null)
            {
                foreach (DesignerForm form in m_ListDesignForm)
                {
                    form.Refresh();
                }
            }
            else
            {

                if (Mess.bUpdateScreenForm)
                {
                    foreach (DesignerForm form in m_ListDesignForm)
                    {
                        form.Refresh();
                    }
                }
            }
        }

        /// <summary>
        /// met a jour l'état des commandes du menu fichier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateFileMenu()
        {
            if (m_GestSolution != null )
            {
                menuItemAddProj.Enabled = true;
                menuItemCloseSolution.Enabled = true;
                menuItemJumpToCmd.Enabled = true;
            }
            else
            {
                menuItemAddProj.Enabled = false;
                menuItemCloseSolution.Enabled = false;
                menuItemJumpToCmd.Enabled = false;
            }
        }

        #endregion

        #region handler's d'event
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            SolutionAskUserToSaveIfIsModified();
            m_mruStripMenu.SaveToFile();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DesignFormClosed(object sender, FormClosedEventArgs e)
        {
            DesignerForm frm = sender as DesignerForm;
            if (frm != null)
            {
                m_ListDesignForm.Remove(frm);
                frm.Dispose();
            }
        }
        #endregion

        #region menu ?
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm.ShowAbout();
        }

        /// <summary>
        /// affiche le panel de version des plugins
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pluginsVersionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PluginsVersionsForm plVer = new PluginsVersionsForm();
            plVer.ShowDialog();
        }
        #endregion

        #region Menu Tools
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JumpToSmartCommandMenuItemClick(object sender, EventArgs e)
        {
            if (m_GestSolution != null && m_GestSolution.Count!=0)
            {
                // si le document n'est pas sauvegardé, on demande de le faire
                SolutionSave(false);

                // on vérifie si il a été sauvegardé
                if (!string.IsNullOrEmpty(m_GestSolution.FilePath) && File.Exists(m_GestSolution.FilePath))
                {
                    this.Hide();
                    Application.DoEvents();
                    System.Diagnostics.Process proc = new System.Diagnostics.Process();
                    string Arguments = "-Cmd \"" + m_GestSolution.FilePath + "\"";
                    if (LaunchArgParser.DevMode)
                        Arguments += " -dev";
                    proc.StartInfo = new System.Diagnostics.ProcessStartInfo(Application.ExecutablePath, Arguments);
                    proc.StartInfo.UseShellExecute = true;

                    if (proc.Start())
                    {
                        proc.WaitForExit();
                    }
                    else
                    {
                        MessageBox.Show(Program.LangSys.C("Application fail on startup."), Program.LangSys.C("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    this.Show();
                }
            }
        }
        #endregion

        #region Assistant création de projet
        /// <summary>
        /// 
        /// </summary>
        private void AddEmptyProject()
        {
            if (m_GestSolution != null)
            {
                ProjectNameForm projNameFrm = new ProjectNameForm();
                DialogResult dlgRes = projNameFrm.ShowDialog();
                if (dlgRes == DialogResult.OK)
                {
                    string projectName = Path.GetFileName(projNameFrm.ProjectName);
                    string projectPath = Path.GetDirectoryName(m_GestSolution.FilePath) + Path.DirectorySeparatorChar + projectName;
                    BTDoc newDoc = new BTDoc(Program.TypeApp);
                    newDoc.WriteConfigDocument(projectPath, false, Program.DllGest);
                    m_GestSolution.AddDocument(newDoc);
                    newDoc.Modified = false;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddExistingProject()
        {
            if (m_GestSolution != null)
            {
                DialogResult dlgRes = CentralizedFileDlg.ShowOpenPrjFileDilaog();
                if (dlgRes == DialogResult.OK)
                {
                    m_GestSolution.OpenDocument(CentralizedFileDlg.PrjOpenFileName);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void WizardStartZ2SL(BTDoc document)
        {
            if (document != null)
            {
                WizardSLFormZ2 wiz = new WizardSLFormZ2();
                wiz.m_Document = document;
                if (wiz.ShowDialog() == DialogResult.OK)
                {
                    document.Modified = true;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void WizardStartTCPMB(BTDoc document)
        {
            if (document != null)
            {
                WizardTcpModbusForm wiz = new WizardTcpModbusForm();
                wiz.m_Document = document;
                if (wiz.ShowDialog() == DialogResult.OK)
                {
                    document.Modified = true;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void WizardStartM3SL(BTDoc document)
        {
            if (document != null)
            {
                WizardSLFormM3 wiz = new WizardSLFormM3();
                wiz.m_Document = document;
                if (wiz.ShowDialog() == DialogResult.OK)
                {
                    document.Modified = true;
                }
            }
        }

        /// <summary>
        /// lance le wizard de projet SL M3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WizardStartM3SLProject()
        {
            if (m_GestSolution!= null)
            {
                ProjectNameForm projNameFrm = new ProjectNameForm();
                DialogResult dlgRes = projNameFrm.ShowDialog();
                if (dlgRes == DialogResult.OK)
                {
                    string projectName = Path.GetFileName(projNameFrm.ProjectName);
                    string projectPath = Path.GetDirectoryName(m_GestSolution.FilePath) + Path.DirectorySeparatorChar + projectName;
                    BTDoc newDoc = new BTDoc(Program.TypeApp);
                    WizardM3Z2ProjectForm wiz = new WizardM3Z2ProjectForm(new SLWizardConfigData());
                    if (wiz.ShowDialog() == DialogResult.OK)
                    {
                        wiz.CreateAllFromWizardData(new SLM3ProjectCreator(newDoc));
                        newDoc.WriteConfigDocument(projectPath, false, Program.DllGest);
                        m_GestSolution.AddDocument(newDoc);
                        newDoc.Modified = false;
                    }
                }
            }
        }

        /// <summary>
        /// lance le wizard de projet SL Z2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WizardStartZ2SLProject()
        {
            if (m_GestSolution!= null)
            {
                ProjectNameForm projNameFrm = new ProjectNameForm();
                DialogResult dlgRes = projNameFrm.ShowDialog();
                if (dlgRes == DialogResult.OK)
                {
                    string projectName = Path.GetFileName(projNameFrm.ProjectName);
                    string projectPath = Path.GetDirectoryName(m_GestSolution.FilePath) + Path.DirectorySeparatorChar + projectName;
                    BTDoc newDoc = new BTDoc(Program.TypeApp);
                    WizardM3Z2ProjectForm wiz = new WizardM3Z2ProjectForm(new SLZ2WizardConfigData());
                    if (wiz.ShowDialog() == DialogResult.OK)
                    {
                        wiz.CreateAllFromWizardData(new SLZ2ProjectCreator(newDoc));
                        newDoc.WriteConfigDocument(projectPath, false, Program.DllGest);
                        m_GestSolution.AddDocument(newDoc);
                        newDoc.Modified = false;
                    }
                }
            }
        }

        /// <summary>
        /// lance le wizard de projet ETH M3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WizardStartM3ETHProject()
        {
            if (m_GestSolution!= null)
            {
                ProjectNameForm projNameFrm = new ProjectNameForm();
                DialogResult dlgRes = projNameFrm.ShowDialog();
                if (dlgRes == DialogResult.OK)
                {
                    string projectName = Path.GetFileName(projNameFrm.ProjectName);
                    string projectPath = Path.GetDirectoryName(m_GestSolution.FilePath) + Path.DirectorySeparatorChar + projectName;
                    BTDoc newDoc = new BTDoc(Program.TypeApp);
                    WizardM3Z2ProjectForm wiz = new WizardM3Z2ProjectForm(new M3XN05WizardConfigData());
                    if (wiz.ShowDialog() == DialogResult.OK)
                    {
                        wiz.CreateAllFromWizardData(new ETHM3ProjectCreator(newDoc));
                        newDoc.WriteConfigDocument(projectPath, false, Program.DllGest);
                        m_GestSolution.AddDocument(newDoc);
                        newDoc.Modified = false;
                    }
                }
            }
        }

        /// <summary>
        /// lance le wizard de projet ETH Z2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WizardStartZ2ETHProject()
        {
            if (m_GestSolution!= null)
            {
                ProjectNameForm projNameFrm = new ProjectNameForm();
                DialogResult dlgRes = projNameFrm.ShowDialog();
                if (dlgRes == DialogResult.OK)
                {
                    string projectName = Path.GetFileName(projNameFrm.ProjectName);
                    string projectPath = Path.GetDirectoryName(m_GestSolution.FilePath) + Path.DirectorySeparatorChar + projectName;
                    BTDoc newDoc = new BTDoc(Program.TypeApp);
                    WizardM3Z2ProjectForm wiz = new WizardM3Z2ProjectForm(new Z2SR3NETWizardConfigData());
                    if (wiz.ShowDialog() == DialogResult.OK)
                    {
                        wiz.CreateAllFromWizardData(new ETHZ2ProjectCreator(newDoc));
                        newDoc.WriteConfigDocument(projectPath, false, Program.DllGest);
                        m_GestSolution.AddDocument(newDoc);
                        newDoc.Modified = false;
                    }
                }
            }
        }
        #endregion

        #region edition des propriété d'un document
        private void tsbtnConfigCom_Click(object sender, EventArgs e)
        {
            // TODO
            /*
            if (m_Document != null)
            {
                CommConfiguration commCfgPage = new CommConfiguration();
                commCfgPage.AllowRowSelect = true;
                commCfgPage.CurComParam = m_Document.m_Comm.CommParam;
                commCfgPage.CurTypeCom = m_Document.m_Comm.CommType;
                DialogResult dlgRes = commCfgPage.ShowDialog();
                if (dlgRes == DialogResult.OK)
                {
                    m_Document.m_Comm.SetCommTypeAndParam(commCfgPage.CurTypeCom, commCfgPage.CurComParam);
                }
            }*/
        }
        #endregion

        #region handler d'event du menu file
        private void menuItemNewSolution_Click(object sender, EventArgs e)
        {
            SolutionNew();
        }

        private void menuItemOpenSolution_Click(object sender, EventArgs e)
        {
            SolutionAskUserToSaveIfIsModified();
            SolutionClose();
            this.SolutionOpen(null);
        }

        private void menuItemAddProj_emptyProject_Click(object sender, EventArgs e)
        {
            AddEmptyProject();
        }

        private void menuItemAddProj_importExisting_Click(object sender, EventArgs e)
        {
            AddExistingProject();
        }

        private void menuItemAddProj_M3SLWiz_Click(object sender, EventArgs e)
        {
            WizardStartM3SLProject();
        }

        private void menuItemAddProj_M3ETHWiz_Click(object sender, EventArgs e)
        {
            WizardStartM3ETHProject();
        }

        private void menuItemAddProj_Z2SLWiz_Click(object sender, EventArgs e)
        {
            WizardStartZ2SLProject();
        }

        private void menuItemAddProj_Z2ETHWiz_Click(object sender, EventArgs e)
        {
            WizardStartZ2ETHProject();
        }

        private void menuItemCloseSolution_Click(object sender, EventArgs e)
        {
            this.SolutionAskUserToSaveIfIsModified();
            SolutionClose();
        }

        private void menuItemExit_Click(object sender, EventArgs e)
        {
            ExitSmartConfig();
        }

        private void menuItemSave_Click(object sender, EventArgs e)
        {
            SolutionSave(false);
        }

        private void menuItemSaveAs_Click(object sender, EventArgs e)
        {
            SolutionSave(true);
        }

        private void toolBarItemOpenSolution_Click(object sender, EventArgs e)
        {
            SolutionAskUserToSaveIfIsModified();
            SolutionClose();
            SolutionOpen(null);
        }

        private void toolBarItemSaveAll_Click(object sender, EventArgs e)
        {
            SolutionSave(false);
        }

        /// <summary>
        /// affiche le panel de préférence de l'application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemPref_Click(object sender, EventArgs e)
        {
            PreferencesForm prfForm = new PreferencesForm();
            prfForm.SelectedLang = SmartApp.Properties.Settings.Default.Lang;
            if (prfForm.ShowDialog() == DialogResult.OK)
            {
                if (prfForm.SelectedLang != SmartApp.Properties.Settings.Default.Lang)
                {
                    SmartApp.Properties.Settings.Default.Lang = prfForm.SelectedLang;
                    SmartApp.Properties.Settings.Default.Save();
                    OnNeedUpdateHMI(null);
                    //MessageBox.Show(Program.LangSys.C("Please restart the application in order apply language change"), Program.LangSys.C("Informations"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Lang.LangSys.ChangeLangage(prfForm.SelectedLang);
                    Program.ChangePluginLang(prfForm.SelectedLang);
                    Program.LangSys.ChangeLangage(prfForm.SelectedLang);
                }
            }
        }

        #endregion

        #region handler des boutons pour masquer afficher les panneaux lateraux
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHideShowSolution_Click(object sender, EventArgs e)
        {
            if (this.lblSolutionView.Visible)
            {
                this.solutionTreeView.Visible = false;
                this.solutionPanel.Width = 40;
                this.btnHideShowSolution.Left = 2;
                this.lblSolutionView.Visible = false;
            }
            else
            {
                this.solutionTreeView.Visible = true;
                this.solutionPanel.Width = 250;
                this.btnHideShowSolution.Left = 210;
                this.lblSolutionView.Visible = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHideShowRightPanel_Click(object sender, EventArgs e)
        {
            if (this.lblToolsView.Visible)
            {
                this.rightPanel.Width = 40;
                this.toolsPanel.Visible = false;
                this.lblToolsView.Visible = false;
            }
            else
            {
                this.rightPanel.Width = 200;
                this.toolsPanel.Visible = true;
                this.lblToolsView.Visible = true;
            }

        }
        #endregion

        private void menuItemaddBridge_Click(object sender, EventArgs e)
        {
            if (m_GestSolution != null)
            {
                ProjectNameForm projNameFrm = new ProjectNameForm();
                DialogResult dlgRes = projNameFrm.ShowDialog();
                if (dlgRes == DialogResult.OK)
                {
                    string bridgeName = Path.GetFileName(projNameFrm.ProjectName);
                    string projectPath = Path.GetDirectoryName(m_GestSolution.FilePath) + Path.DirectorySeparatorChar + bridgeName;
                    BridgeDoc newDoc = new BridgeDoc(Program.TypeApp);
                    newDoc.WriteOut(projectPath, false);
                    m_GestSolution.AddDocument(newDoc);
                    newDoc.Modified = false;
                }
            }

        }
    }
}

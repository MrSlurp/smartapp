/*
    This file is part of SmartApp.
    Copyright (C) 2007-2016  Pascal Bigot

    SmartApp is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    SmartApp is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with Foobar.  If not, see <http://www.gnu.org/licenses/>.
*/
using System;
using System.Collections.Generic;
using System.Collections;
using System.Windows.Forms;
using System.Text;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using CommonLib;
using SmartApp.Ihm;
using SmartApp.Wizards;

namespace SmartApp
{
    class SolutionTreeView : TreeView
    {
        #region données membres
        TreeNode m_SolutionNode;
        SolutionGest m_GestSolution;
        ContextMenuStrip m_CtxMenuSolution = new ContextMenuStrip();
        ContextMenuStrip m_CtxMenuProject = new ContextMenuStrip();
        ContextMenuStrip m_CtxMenuGestGroups = new ContextMenuStrip();
        ContextMenuStrip m_CtxMenuGest = new ContextMenuStrip();
        ContextMenuStrip m_CtxMenuGroups = new ContextMenuStrip();
        ContextMenuStrip m_CtxMenuObject = new ContextMenuStrip();
        ContextMenuStrip m_CtxMenuTrame = new ContextMenuStrip();
        ContextMenuStrip m_CtxMenuBridge = new ContextMenuStrip();
        ContextMenuStrip m_CtxMenuBridgeItem = new ContextMenuStrip();
        //ContextMenuStrip m_CtxMenuStrip = new ContextMenuStrip();
        class DocumentElementNode
        {
            public TreeNode DocNode;
            public BaseDoc Document;
        }

        CheckBox m_chkViewTips = new CheckBox();

        BasePropertiesDialog m_PropDialog; //= new BasePropertiesDialog();

        SortedList<string, DocumentElementNode> m_ListDocument = new SortedList<string, DocumentElementNode>();
        #endregion

        #region constructeur et init
        /// <summary>
        /// Constructeur
        /// </summary>
        public SolutionTreeView()
        {
            if (this.ImageList == null && Resources.AppIcon != null)
            {
                this.ImageList = new ImageList();
                //this.StateImageList = this.ImageList;
                this.ImageList.Images.Add("node", new Bitmap(1,1));
                this.ImageList.Images.Add("Document", Resources.AppIcon);
                this.ImageList.Images.Add("Group", Resources.TreeViewGroupIcon);
                this.ImageList.Images.Add("Screen", Resources.TreeViewScreenIcon);
                this.ImageList.Images.Add("Timer", Resources.TreeViewTimerIcon);
                this.ImageList.Images.Add("Frame", Resources.TreeViewFrameIcon);
                this.ImageList.Images.Add("Function", Resources.TreeViewFunctionIcon);
                this.ImageList.Images.Add("Logger", Resources.TreeViewLoggerIcon);
                this.ImageList.Images.Add("IO", Resources.TreeViewIOIcon);
                this.ImageList.Images.Add("Solution", Resources.TreeViewSolutionIcon);
                this.ImageList.Images.Add("Bridge", Resources.TreeViewBridgeIcon);
                // ceci n'est init que si l'appli est lancé, pour pas que le designer tenter de l'ajouter lui même
                m_chkViewTips.AutoSize = true;
                m_chkViewTips.Text = Program.LangSys.C("Show tooltip");
                m_chkViewTips.Location = new Point(this.Right - (m_chkViewTips.Width + 5), 0);
                m_chkViewTips.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                m_chkViewTips.CheckAlign = ContentAlignment.MiddleRight;
                m_chkViewTips.CheckedChanged += new EventHandler(chkViewTips_CheckedChanged);
                m_chkViewTips.TextAlign = ContentAlignment.MiddleRight;
                this.Controls.Add(m_chkViewTips);
            }
            if (m_SolutionNode == null)
            {
                m_SolutionNode = new TreeNode();
                m_SolutionNode.NodeFont = new Font(SystemFonts.CaptionFont, FontStyle.Bold);
                m_SolutionNode.Text = Program.LangSys.C("Solution");
                m_SolutionNode.StateImageKey = "Solution";
                m_SolutionNode.ImageKey = "Solution";
                m_SolutionNode.SelectedImageKey = "Solution";
                m_SolutionNode.ForeColor = Color.Red;
            }
            m_PropDialog = MDISmartConfigMain.GlobalPropDialog;
            m_PropDialog.ObjectPropertiesChanged += new EventHandler(OnPropertiesDlgObjChangedNotified);
            InitContextMenu();
        }

        /// <summary>
        /// initialise les menu contextuels
        /// </summary>
        private void InitContextMenu()
        {
            // menu de l'item solution
            /*
            ToolStripMenuItem item = new ToolStripMenuItem(Program.LangSys.C("Add existing project"));
            item.Click += new EventHandler(CtxMenuAddExistingProj_Click);
            m_CtxMenuSolution.Items.Add(item);
            item = new ToolStripMenuItem(Program.LangSys.C("Add new project"));
            item.Click += new EventHandler(CtxMenuAddNewProj_Click);
            m_CtxMenuSolution.Items.Add(item);*/
            ToolStripMenuItem item = new ToolStripMenuItem(Program.LangSys.C("Open solution directory"));
            item.Click += new EventHandler(CtxMenuOpenSolutionDir_Click);
            m_CtxMenuSolution.Items.Add(item);

            // menus des item document
            item = new ToolStripMenuItem(Program.LangSys.C("Remove project from solution"));
            item.Click += new EventHandler(CtxMenuRemoveProj_Click);
            m_CtxMenuProject.Items.Add(item);
            item = new ToolStripMenuItem(Program.LangSys.C("Configure project / connection"));
            item.Click += new EventHandler(CtxMenuProjProperties_Click);
            m_CtxMenuProject.Items.Add(item);

            // menus des item document
            item = new ToolStripMenuItem(Program.LangSys.C("Remove bridge from solution"));
            item.Click += new EventHandler(CtxMenuRemoveProj_Click);
            m_CtxMenuBridge.Items.Add(item);
            item = new ToolStripMenuItem(Program.LangSys.C("Add bridge"));
            item.Click += new EventHandler(CtxMenuAddBridge_Click);
            m_CtxMenuBridge.Items.Add(item);

            item = new ToolStripMenuItem(Program.LangSys.C("Configure bridge"));
            item.Click += new EventHandler(CtxMenuBridgeProperties_Click);
            m_CtxMenuBridgeItem.Items.Add(item);
            item = new ToolStripMenuItem(Program.LangSys.C("Delete bridge"));
            item.Click += new EventHandler(CtxMenuObjectDeleteBridge_Click);
            m_CtxMenuBridgeItem.Items.Add(item);

            // menu pour un gestionnaire de group
            item = new ToolStripMenuItem(Program.LangSys.C("Manage group"));
            item.Click += new EventHandler(CtxMenuGroupProperties_Click);
            m_CtxMenuGestGroups.Items.Add(item);
            item = new ToolStripMenuItem(Program.LangSys.C("Add item"));
            item.Click += new EventHandler(CtxMenuObjectNew_Click);
            m_CtxMenuGestGroups.Items.Add(item);
#if DEBUG 
            item = new ToolStripMenuItem(Program.LangSys.C("Start Split/Join data wizard"));
            item.Click += new EventHandler(CtxMenuSplitJoinWizard_Click);
            m_CtxMenuGestGroups.Items.Add(item);
#endif

            // menu pour un gestionnaire standard
            item = new ToolStripMenuItem(Program.LangSys.C("Add item"));
            item.Click += new EventHandler(CtxMenuObjectNew_Click);
            m_CtxMenuGest.Items.Add(item);

            // menu pour un groupe
            item = new ToolStripMenuItem(Program.LangSys.C("Add Group"));
            item.Click += new EventHandler(CtxMenuGroupProperties_Click);
            m_CtxMenuGroups.Items.Add(item);

            // menu pour un objet standard
            item = new ToolStripMenuItem(Program.LangSys.C("Delete"));
            item.Click += new EventHandler(CtxMenuObjectDelete_Click);
            m_CtxMenuObject.Items.Add(item);
            item = new ToolStripMenuItem(Program.LangSys.C("Properties"));
            item.Click += new EventHandler(CtxMenuObjectProperties_Click);
            m_CtxMenuObject.Items.Add(item);

            // menu pour le gestionnaire de trames
            item = new ToolStripMenuItem(Program.LangSys.C("Use Modbus TCP Wizard"));
            item.Click += new EventHandler(CtxMenuTrameTCPMBWizard_Click);
            m_CtxMenuTrame.Items.Add(item);
            item = new ToolStripMenuItem(Program.LangSys.C("Use M3 SL Wizard"));
            item.Click += new EventHandler(CtxMenuTrameM3SLWizard_Click);
            m_CtxMenuTrame.Items.Add(item);
            item = new ToolStripMenuItem(Program.LangSys.C("Use Z2 SL Wizard"));
            item.Click += new EventHandler(CtxMenuTrameZ2SLWizard_Click);
            m_CtxMenuTrame.Items.Add(item);
            item = new ToolStripMenuItem(Program.LangSys.C("Add item"));
            item.Click += new EventHandler(CtxMenuObjectNew_Click);
            m_CtxMenuTrame.Items.Add(item);
        }
        #endregion

        #region handler souris
        /// <summary>
        /// gestion du click standard de la souris
        /// </summary>
        /// <param name="sender">standard</param>
        /// <param name="e">standard</param>
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            this.SelectedNode = this.GetNodeAt(e.X, e.Y);
            TreeNode selNode = this.SelectedNode;
            if (selNode == null)
                return;
            // gestion du clic souris avec le bouton droit
            // on fixe le menu contextuel approprié au type d'objet
            // et l'affiche si il y a lieu d'être
            if (e.Button == MouseButtons.Right)
            {

                // la solution
                if (selNode == m_SolutionNode)
                {
                    this.ContextMenuStrip = m_CtxMenuSolution;
                }
                // un groupe
                else if (selNode.Tag is BaseGestGroup.Group)
                {
                    this.ContextMenuStrip = m_CtxMenuGestGroups;
                    m_PropDialog.ConfiguredItem = null;
                }
                // un document
                else if (selNode.Tag is DocumentElementNode)
                {
                    DocumentElementNode docNode = selNode.Tag as DocumentElementNode;
                    if (docNode.Document is BTDoc)
                    {
                        this.ContextMenuStrip = m_CtxMenuProject;
                        m_PropDialog.ConfiguredItem = null;
                    }
                    else if (docNode.Document is BridgeDoc)
                    {
                        this.ContextMenuStrip = m_CtxMenuBridge;
                        m_PropDialog.ConfiguredItem = null;
                    }
                }
                else if (selNode.Tag is GestTrame)
                {
                    this.ContextMenuStrip = m_CtxMenuTrame;
                    m_PropDialog.ConfiguredItem = null;
                }
                // un gestionnaire de base
                else if (selNode.Tag is BaseGest)
                {
                    this.ContextMenuStrip = m_CtxMenuGest;
                    m_PropDialog.ConfiguredItem = null;
                }
                else if (selNode.Tag is DataBridgeInfo)
                {
                    this.ContextMenuStrip = m_CtxMenuBridgeItem;
                    m_PropDialog.ConfiguredItem = null;
                }
                // un objet quelconque
                else if (selNode.Tag is BaseObject)
                {
                    this.ContextMenuStrip = m_CtxMenuObject;
                }
                else
                {
                    this.ContextMenuStrip = null;
                    m_PropDialog.ConfiguredItem = null;
                }
                if (this.ContextMenuStrip != null)
                    this.ContextMenuStrip.Show(e.Location);
            }
            else
            {
                if (selNode.Tag is BaseObject)
                {
                    BaseObject bobj = selNode.Tag as BaseObject;
                    m_PropDialog.Document = GetDocFromParentNode(selNode) as BTDoc;
                    m_PropDialog.CurrentScreen = null;
                    m_PropDialog.ConfiguredItem = bobj;
                    m_PropDialog.Initialize();
                }
            }
        }

        /// <summary>
        /// gestion de l'évènement double clic sur un noeud
        /// </summary>
        /// <param name="e">Argument de l'évènement souris</param>
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            TreeNode selNode = this.SelectedNode;
            if (selNode != null)
            {
                if (selNode.Tag is BaseObject && !(selNode.Tag is BTScreen) && !(selNode.Tag is DataBridgeInfo))
                {
                    CtxMenuObjectProperties_Click(this, e);
                }
                else if (selNode.Tag is BaseObject && selNode.Tag is BTScreen)
                {
                    BTScreen scr = selNode.Tag as BTScreen;
                    m_GestSolution.OpenScreenEditor(scr.Symbol, GetDocFromParentNode(selNode) as BTDoc);
                }
                else if (selNode.Tag is BaseObject && selNode.Tag is DataBridgeInfo)
                {
                    DataBridgeInfo bri = selNode.Tag as DataBridgeInfo;
                    //DocumentElementNode elem = selNode.Tag as DocumentElementNode;
                    BridgeEditorForm brideDlg = new BridgeEditorForm()
                    {
                        Solution = m_GestSolution,
                        BridgeInfo = bri,
                    };
                    brideDlg.ShowDialog();
                }
            }
        }
        #endregion

        #region handler context menu
        /// <summary>
        /// handler du menu ajouter un projet existant
        /// </summary>
        /// <param name="sender">standard</param>
        /// <param name="e">standard</param>
        void CtxMenuAddExistingProj_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// handler du menu ouvrir le dossier de la solution
        /// </summary>
        /// <param name="sender">standard</param>
        /// <param name="e">standard</param>
        void CtxMenuOpenSolutionDir_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", "/select, " + m_GestSolution.FilePath);
        }

        /// <summary>
        /// handler du menu ajouter nouveau projet
        /// </summary>
        /// <param name="sender">standard</param>
        /// <param name="e">standard</param>
        void CtxMenuAddNewProj_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// handler du menu Supprimer un projet
        /// </summary>
        /// <param name="sender">standard</param>
        /// <param name="e">standard</param>
        void CtxMenuRemoveProj_Click(object sender, EventArgs e)
        {
            TreeNode selNode = SelectedNode;
            DocumentElementNode elem = selNode.Tag as DocumentElementNode;
            m_GestSolution.CloseDocument(elem.Document);
        }

        /// <summary>
        /// handler du menu propriété du projet
        /// </summary>
        /// <param name="sender">standard</param>
        /// <param name="e">standard</param>
        void CtxMenuProjProperties_Click(object sender, EventArgs e)
        {
            TreeNode selNode = SelectedNode;
            DocumentElementNode elem = selNode.Tag as DocumentElementNode;
            //DocumentProprtiesDialog projectPropDialog = new DocumentProprtiesDialog();
            DocumentProprtiesDialog CfgPage = new DocumentProprtiesDialog();
            CfgPage.Document = elem.Document as BTDoc;
            CfgPage.ShowDialog();
        }

        /// <summary>
        /// handler du menu propriété du projet
        /// </summary>
        /// <param name="sender">standard</param>
        /// <param name="e">standard</param>
        void CtxMenuBridgeProperties_Click(object sender, EventArgs e)
        {
            TreeNode selNode = SelectedNode;
            DataBridgeInfo bri = selNode.Tag as DataBridgeInfo;
            //DocumentElementNode elem = selNode.Tag as DocumentElementNode;
            BridgeEditorForm brideDlg = new BridgeEditorForm()
                {
                    Solution = m_GestSolution,
                    BridgeInfo = bri,
                };
            if (brideDlg.ShowDialog() == DialogResult.OK)
            {
                selNode.Text = bri.Symbol;
            }
        }

        /// <summary>
        /// handler du menu proprété d'un groupe
        /// </summary>
        /// <param name="sender">standard</param>
        /// <param name="e">standard</param>
        void CtxMenuGroupProperties_Click(object sender, EventArgs e)
        {
            TreeNode selNode = SelectedNode;
            GestData gest = selNode.Parent.Tag as GestData;
            ManageGroupForm form = new ManageGroupForm();
            form.GestData = gest;
            form.Initialize();
            form.ShowDialog();
            TreeNode parentGestNode = selNode.Parent;
            parentGestNode.Nodes.Clear();
            AddGestGroupContent(parentGestNode, gest, parentGestNode.ImageKey);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">standard</param>
        /// <param name="e">standard</param>
        void CtxMenuObjectNew_Click(object sender, EventArgs e)
        {
            TreeNode selNode = this.SelectedNode;
            BaseObject bobj = null;
            string imgKey = string.Empty;
            TreeNode NewItemParentNode = null;
            if (selNode.Tag is BaseGestGroup)
            {
                // le noeud parent possède un tag qui est le gestionnaire
                BaseGestGroup gest = selNode.Tag as BaseGestGroup;
                bobj = gest.AddNewObject(GetDocFromParentNode(selNode) as BTDoc);
                imgKey = selNode.ImageKey;
                NewItemParentNode = selNode.FirstNode;
            }
            else if (selNode.Tag is BaseGestGroup.Group)
            {
                // le noeud parent possède un tag qui est le gestionnaire
                BaseGestGroup gest = selNode.Parent.Tag as BaseGestGroup;
                BaseGestGroup.Group curGroup = selNode.Tag as BaseGestGroup.Group;
                bobj = gest.AddNewObject(GetDocFromParentNode(selNode) as BTDoc, curGroup.GroupSymbol);
                imgKey = selNode.Parent.ImageKey;
                NewItemParentNode = selNode;
            }
            else if (selNode.Tag is BaseGest)
            {
                BaseGest gest = selNode.Tag as BaseGest;
                bobj = gest.AddNewObject(GetDocFromParentNode(selNode) as BTDoc);
                imgKey = selNode.ImageKey;
                NewItemParentNode = selNode;
            }

            if (bobj != null)
            {
                TreeNode newNode = new TreeNode(bobj.Symbol);
                newNode.ImageKey = imgKey;
                newNode.SelectedImageKey = imgKey;
                newNode.StateImageKey = imgKey;
                newNode.Tag = bobj;
                newNode.ToolTipText = GetToolTipFromTag(bobj);
                NewItemParentNode.Nodes.Add(newNode);
                BTDoc doc = GetDocFromParentNode(newNode) as BTDoc;
                doc.Modified = true;
                this.SelectedNode = newNode;
                OnMouseDoubleClick(null);
            }
        }

        void CtxMenuAddBridge_Click(object sender, EventArgs e)
        {
            TreeNode selNode = this.SelectedNode;
            if (selNode.Tag is DocumentElementNode)
            {
                DocumentElementNode docNode = selNode.Tag as DocumentElementNode;
                if (docNode.Document is BridgeDoc)
                {
                    BridgeDoc bridge = docNode.Document as BridgeDoc;
                    DataBridgeInfo bgInfo = bridge.CreateNewBridge();
                    this.AddBridgeElemNode(selNode, bgInfo, "IO");
                }
            }
        }

        void CtxMenuObjectDeleteBridge_Click(object sender, EventArgs e)
        {
            TreeNode selNode = this.SelectedNode;
            if (selNode.Tag is DataBridgeInfo)
            {
                BaseDoc docNode = GetDocFromParentNode(selNode);
                if (docNode is BridgeDoc)
                {
                    ((BridgeDoc)docNode).DeleteBridge(selNode.Text);
                    selNode.Remove();
                }
            }
        }

        void CtxMenuSplitJoinWizard_Click(object sender, EventArgs e)
        {
            TreeNode selNode = this.SelectedNode;
            if (selNode.Tag is BaseGestGroup.Group)
            {
                // le noeud parent possède un tag qui est le gestionnaire
                BTDoc doc = this.GetDocFromParentNode(selNode) as BTDoc;
                SplitJoinWizardForm sjWizard = new SplitJoinWizardForm(doc);
                if (sjWizard.ShowDialog() == DialogResult.OK)
                {

                }
            }
        }

        /// <summary>
        /// handler du menu supprimer un élément
        /// </summary>
        /// <param name="sender">standard</param>
        /// <param name="e">standard</param>
        void CtxMenuObjectDelete_Click(object sender, EventArgs e)
        {
            TreeNode selNode = this.SelectedNode;
            BaseObject bobj = null;
            BaseGest objGest = null;
            if (selNode.Tag is BaseObject)
            {
                bobj = selNode.Tag as BaseObject;
                objGest = selNode.Parent.Tag as BaseGest;
                if (objGest == null)
                    objGest = selNode.Parent.Parent.Tag as BaseGest;
            }

            if (bobj != null && objGest != null)
            {
                if (objGest.RemoveObj(bobj))
                {
                    selNode.Remove();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">standard</param>
        /// <param name="e">standard</param>
        void CtxMenuObjectProperties_Click(object sender, EventArgs e)
        {
            TreeNode selNode = this.SelectedNode;
            if (selNode.Tag is BaseObject)
            {
                BaseObject bobj = selNode.Tag as BaseObject;
                m_PropDialog.ConfiguredItem = bobj;
                m_PropDialog.CurrentScreen = null;
                m_PropDialog.Document = GetDocFromParentNode(selNode) as BTDoc;
                m_PropDialog.Initialize();
                if (!m_PropDialog.Visible)
                {
                    m_PropDialog.Show(Program.CurrentMainForm);
                }
                m_PropDialog.BringToFront();
            }
        }

        void OnPropertiesDlgObjChangedNotified(object sender, EventArgs e)
        {
            TreeNode selNode = this.SelectedNode;
            if (m_PropDialog.ConfiguredItem == selNode.Tag)
            {
                BaseObject bobj = selNode.Tag as BaseObject;
                selNode.ToolTipText = GetToolTipFromTag(bobj);
                selNode.Text = bobj.Symbol;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CtxMenuTrameTCPMBWizard_Click(object sender, EventArgs e)
        {
            TreeNode selNode = this.SelectedNode;
            if (selNode.Tag is GestTrame)
            {
                BTDoc doc = this.GetDocFromParentNode(selNode) as BTDoc;
                MDISmartConfigMain mainFrame = this.Parent.Parent as MDISmartConfigMain;
                if (mainFrame != null)
                {
                    mainFrame.WizardStartTCPMB(doc);
                    selNode.Nodes.Clear();
                    AddBaseGestContent(selNode, selNode.Tag as BaseGest, selNode.ImageKey);
                    TreeNode gestDataNode = GetTreeNodeFromTag(this.Nodes[0], doc.GestData);
                    if (gestDataNode != null)
                    {
                        gestDataNode.Nodes.Clear();
                        AddGestGroupContent(gestDataNode, gestDataNode.Tag as BaseGestGroup, gestDataNode.ImageKey);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CtxMenuTrameM3SLWizard_Click(object sender, EventArgs e)
        {
            TreeNode selNode = this.SelectedNode;
            if (selNode.Tag is GestTrame)
            {
                BTDoc doc = this.GetDocFromParentNode(selNode) as BTDoc;
                MDISmartConfigMain mainFrame = this.Parent.Parent as MDISmartConfigMain;
                if (mainFrame != null)
                {
                    mainFrame.WizardStartM3SL(doc);
                    selNode.Nodes.Clear();
                    AddBaseGestContent(selNode, selNode.Tag as BaseGest, selNode.ImageKey);
                    TreeNode gestDataNode = GetTreeNodeFromTag(this.Nodes[0], doc.GestData);
                    if (gestDataNode != null)
                    {
                        gestDataNode.Nodes.Clear();
                        AddGestGroupContent(gestDataNode, gestDataNode.Tag as BaseGestGroup, gestDataNode.ImageKey);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CtxMenuTrameZ2SLWizard_Click(object sender, EventArgs e)
        {
            TreeNode selNode = this.SelectedNode;
            if (selNode.Tag is GestTrame)
            {
                BTDoc doc = this.GetDocFromParentNode(selNode) as BTDoc;
                MDISmartConfigMain mainFrame = this.Parent.Parent as MDISmartConfigMain;
                if (mainFrame != null)
                {
                    mainFrame.WizardStartZ2SL(doc);
                    selNode.Nodes.Clear();
                    AddBaseGestContent(selNode, selNode.Tag as BaseGest, selNode.ImageKey);
                    TreeNode gestDataNode = GetTreeNodeFromTag(this.Nodes[0], doc.GestData);
                    if (gestDataNode != null)
                    {
                        gestDataNode.Nodes.Clear();
                        AddGestGroupContent(gestDataNode, gestDataNode.Tag as BaseGestGroup, gestDataNode.ImageKey);
                    }
                }
            }
        }

        #endregion

        #region attributs
        /// <summary>
        /// Gestionnaire de solution
        /// </summary>
        public SolutionGest SolutionGest
        {
            get { return m_GestSolution; }
            set 
            { 
                
                if (value != null && m_GestSolution != value)
                {
                    m_GestSolution = value;
                    m_GestSolution.OnDocOpened += new SolutionGest.DocumentOpenCloseEventHandler(AddDocument);
                    m_GestSolution.OnDocClosed += new SolutionGest.DocumentOpenCloseEventHandler(RemoveDocument);
                    m_GestSolution.OnSolutionNameChanged += new SolutionGest.SolutionNameChangedEventHandler(SolutionNameChanged);
                    EmptySolution();
                }
                else
                {
                    m_GestSolution = null;
                    this.Nodes.Clear();
                    m_SolutionNode = null;
                }
            }
        }

        #endregion

        #region gestion de l'ajout/suppression/modification d'un document
        public void EmptySolution()
        {
            this.Nodes.Clear();
            m_ListDocument.Clear();
            if (m_SolutionNode == null)
            {
                m_SolutionNode = new TreeNode();
                m_SolutionNode.NodeFont = new Font(SystemFonts.CaptionFont, FontStyle.Bold);
                m_SolutionNode.Text = Program.LangSys.C("Solution");
                m_SolutionNode.StateImageKey = "Solution";
                m_SolutionNode.ImageKey = "Solution";
                m_SolutionNode.SelectedImageKey = "Solution";
                m_SolutionNode.ForeColor = Color.Red;
            }
            if (!this.Nodes.Contains(m_SolutionNode))
            {
                this.Nodes.Add(m_SolutionNode);
            }
        }

        /// <summary>
        /// Ajout un document à l'arbre
        /// </summary>
        /// <param name="doc">Document à ajouter</param>
        public void AddDocument(BaseDoc doc)
        {
            if (!this.Nodes.Contains(m_SolutionNode))
            {
                this.Nodes.Add(m_SolutionNode);
            }

            // noeud graphique du document
            TreeNode newNode = new TreeNode();
            // structure liant le document à un noeud et stocké dans une map
            DocumentElementNode docNode = new DocumentElementNode();
            docNode.Document = doc;
            docNode.DocNode = newNode;
            // on définit l'icone du noeud document
            if (doc is BTDoc)
            {
                newNode.ImageKey = "Document";
                newNode.StateImageKey = "Document";
                newNode.SelectedImageKey = "Document";
                newNode.ForeColor = Color.Blue;
                newNode.NodeFont = new Font(SystemFonts.CaptionFont, FontStyle.Bold);
                newNode.Tag = docNode;
                newNode.ToolTipText = GetToolTipFromTag(docNode);
            }
            if (doc is BridgeDoc)
            {
                newNode.ImageKey = "Bridge";
                newNode.StateImageKey = "Bridge";
                newNode.SelectedImageKey = "Bridge";
                newNode.ForeColor = Color.Green;
                newNode.NodeFont = new Font(SystemFonts.CaptionFont, FontStyle.Bold);
                newNode.Tag = docNode;
                newNode.ToolTipText = GetToolTipFromTag(docNode);
            }
            newNode.Text = Path.GetFileNameWithoutExtension(doc.FileName);
            m_SolutionNode.Nodes.Add(newNode);
            m_ListDocument.Add(newNode.Text, docNode);
            AddDocumentNodeGestsNodes(newNode.Text);
            doc.OnDocumentModified += new DocumentModifiedEvent(OnDocumentModified);
            docNode.DocNode.Expand();
            m_SolutionNode.Expand();
            m_chkViewTips.Checked = true;
            m_chkViewTips.Checked = false;
        }

        /// <summary>
        /// Appelé lorsque le document est modifié
        /// </summary>
        protected void OnDocumentModified()
        {
            //UpdateNodeFromTag(m_SolutionNode);
        }

        /// <summary>
        /// Enlève un document de l'arbre
        /// </summary>
        /// <param name="doc"></param>
        public void RemoveDocument(BaseDoc doc)
        {
            string docName = Path.GetFileNameWithoutExtension(doc.FileName);
            DocumentElementNode docElem = m_ListDocument[docName];
            m_SolutionNode.Nodes.Remove(docElem.DocNode);
            m_ListDocument.Remove(docName);
        }

        /// <summary>
        /// Ajoute les noeuds des différents gestionnaires d'objet d'un document
        /// </summary>
        /// <param name="docName">nom du document dans la map</param>
        public void AddDocumentNodeGestsNodes(string docName)
        {
            DocumentElementNode docElem = m_ListDocument[docName];
            if (docElem.Document is BTDoc)
            {
                BTDoc doc = docElem.Document as BTDoc;
                AddBaseGestNode(docName, doc.GestData, Program.LangSys.C("Datas"), "IO");
                AddBaseGestNode(docName, doc.GestScreen, Program.LangSys.C("Screens"), "Screen");
                AddBaseGestNode(docName, doc.GestTrame, Program.LangSys.C("Frames"), "Frame");
                AddBaseGestNode(docName, doc.GestTimer, Program.LangSys.C("Timers"), "Timer");
                AddBaseGestNode(docName, doc.GestFunction, Program.LangSys.C("Functions"), "Function");
                AddBaseGestNode(docName, doc.GestLogger, Program.LangSys.C("Loggers"), "Logger");
            }
            if (docElem.Document is BridgeDoc)
            {
                BridgeDoc doc = docElem.Document as BridgeDoc;
                this.AddBridgeNodes(docName, "IO");
            }
        }

        void SolutionNameChanged()
        {
            m_SolutionNode.Text = Path.GetFileNameWithoutExtension(m_GestSolution.FilePath);
        }
        #endregion

        #region Ajout des gestionnaires
        /// <summary>
        /// Ajout un noeud gestionnaire standard
        /// </summary>
        /// <param name="docName">nom du document dans la map</param>
        /// <param name="gest">Gestionnaire à ajouter</param>
        /// <param name="Label">label du noeud du gestionnaire</param>
        /// <param name="imageKey">clé de l'image du noeud dans l'arbre</param>
        public void AddBaseGestNode(string docName, BaseGest gest, string Label, string imageKey)
        {
            DocumentElementNode docElem = m_ListDocument[docName];
            TreeNode GestNode = new TreeNode(Label);
            GestNode.ImageKey = imageKey;
            GestNode.StateImageKey = imageKey;
            GestNode.SelectedImageKey = imageKey;
            GestNode.Tag = gest;
            GestNode.ToolTipText = GetToolTipFromTag(gest);
            docElem.DocNode.Nodes.Add(GestNode);
            if (gest is BaseGestGroup)
                AddGestGroupContent(GestNode, gest as BaseGestGroup, imageKey);
            else
                AddBaseGestContent(GestNode, gest, imageKey);
        }

        public void AddBridgeNodes(string docName, string imageKey)
        {
            DocumentElementNode docElem = m_ListDocument[docName];
            BridgeDoc doc = docElem.Document as BridgeDoc;
            for (int i = 0; i < doc.DocumentBridges.Count; i++)
            {
                BaseObject item = doc.DocumentBridges[i];
                AddBridgeElemNode(docElem.DocNode, item as DataBridgeInfo, imageKey);
            }
        }

        public void AddBridgeElemNode(TreeNode parentNode, DataBridgeInfo bridge, string imageKey)
        {
            TreeNode ItemNode = new TreeNode(bridge.Symbol);
            ItemNode.ImageKey = imageKey;
            ItemNode.StateImageKey = imageKey;
            ItemNode.SelectedImageKey = imageKey;
            ItemNode.Tag = bridge;
            ItemNode.ToolTipText = GetToolTipFromTag(bridge);
            parentNode.Nodes.Add(ItemNode);

        }

        /// <summary>
        /// Ajoute le contenu d'un gestionnaire de groupe dans l'arbre
        /// </summary>
        /// <param name="parentNode">noeud graphique auquel ajouter les éléments</param>
        /// <param name="gest">Gestionnaire de groupe dont les éléments doivent être insérés</param>
        /// <param name="imageKey">clé de l'image du noeud dans l'arbre</param>
        private void AddGestGroupContent(TreeNode parentNode, BaseGestGroup gest, string imageKey)
        {
            for (int i = 0; i < gest.GroupCount; i++)
            {
                BaseGestGroup.Group CurrentGroup = gest.Groups[i];
                TreeNode grpNode = new TreeNode(CurrentGroup.GroupName);
                grpNode.Tag = CurrentGroup;
                grpNode.ToolTipText = GetToolTipFromTag(CurrentGroup);
                grpNode.ImageKey = "Group";
                grpNode.StateImageKey = "Group";
                grpNode.SelectedImageKey ="Group";
                parentNode.Nodes.Add(grpNode);
                foreach (BaseObject item in CurrentGroup.Items)
                {
                    if (item.IsUserVisible)
                    {
                        BaseObject bo = gest.GetFromSymbol(item.Symbol);
                        System.Diagnostics.Debug.Assert(bo == item);
                        TreeNode ItemNode = new TreeNode(item.Symbol);
                        ItemNode.Tag = item;
                        ItemNode.ImageKey = imageKey;
                        ItemNode.StateImageKey = imageKey;
                        ItemNode.SelectedImageKey = imageKey;
                        ItemNode.ToolTipText = GetToolTipFromTag(item);
                        grpNode.Nodes.Add(ItemNode);
                    }
                }
            }
        }

        /// <summary>
        /// Ajoute le contenu d'un gestionnaire de base dans l'arbre
        /// </summary>
        /// <param name="parentNode">noeud graphique auquel ajouter les éléments</param>
        /// <param name="gest">Gestionnaire dont les éléments doivent être insérés</param>
        /// <param name="imageKey">clé de l'image du noeud dans l'arbre</param>
        private void AddBaseGestContent(TreeNode parentNode, BaseGest gest, string imageKey)
        {
            for (int i = 0; i < gest.Count; i++ )
            {
                BaseObject item = gest[i];
                if (item.IsUserVisible)
                {
                    TreeNode ItemNode = new TreeNode(item.Symbol);
                    ItemNode.ImageKey = imageKey;
                    ItemNode.StateImageKey = imageKey;
                    ItemNode.SelectedImageKey = imageKey;
                    ItemNode.Tag = item;
                    ItemNode.ToolTipText = GetToolTipFromTag(item);
                    parentNode.Nodes.Add(ItemNode);
                }
            }
        }
        #endregion

        #region gestion du drag & drop
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnItemDrag(ItemDragEventArgs e)
        {
            TreeNode selNode = e.Item as TreeNode;
            if (selNode != null && selNode.Tag is Data)
            {
                this.DoDragDrop(selNode, DragDropEffects.All);
            }
            else
            {
                base.OnItemDrag(e);
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="drgevent"></param>
        protected override void OnDragOver(DragEventArgs drgevent)
        {
            TreeNode overTreeNode = this.GetNodeAt(PointToClient( new Point(drgevent.X, drgevent.Y)));
            TreeNode DropedItem = (TreeNode)drgevent.Data.GetData(typeof(TreeNode));
            if (overTreeNode != null)
            {
                if (overTreeNode.Tag is BaseGestGroup.Group)
                {
                    BTDoc docDest = GetDocFromParentNode(overTreeNode) as BTDoc;
                    BTDoc docSrc = GetDocFromParentNode(DropedItem) as BTDoc;
                    // pour empècher de remettre dans le même groupe, vérifier si le groupe src et dest sont
                    // différents
                    //BaseGestGroup.Group dstGrp = overTreeNode.Tag as BaseGestGroup.Group;
                    if (docSrc == docDest)
                        drgevent.Effect = DragDropEffects.Move;
                }
                else
                {
                    drgevent.Effect = DragDropEffects.None;
                }
            }
            
            base.OnDragOver(drgevent);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="drgevent"></param>
        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            TreeNode overTreeNode = this.GetNodeAt(PointToClient( new Point(drgevent.X, drgevent.Y)));
            
            TreeNode DropedItem = (TreeNode)drgevent.Data.GetData(typeof(TreeNode));
            if (overTreeNode != null && DropedItem != null)
            {
                if (overTreeNode.Tag is BaseGestGroup.Group)
                {
                    // le noeud parent possède un tag qui est le gestionnaire
                    BTDoc docDest = GetDocFromParentNode(overTreeNode) as BTDoc;
                    BTDoc docSrc = GetDocFromParentNode(DropedItem) as BTDoc;
                    BaseGestGroup gest = overTreeNode.Parent.Tag as BaseGestGroup;
                    BaseGestGroup.Group curGroup = overTreeNode.Tag as BaseGestGroup.Group;
                    BaseObject bobj = DropedItem.Tag as BaseObject;
                    if (docDest == docSrc &&
                        bobj != null && 
                        gest != null && 
                        curGroup != null)
                    {
                        gest.AddObjectToGroup(curGroup.GroupSymbol, bobj);
                        DropedItem.Remove();
                        overTreeNode.Nodes.Add(DropedItem);
                    }
                }
                else
                {
                    drgevent.Effect = DragDropEffects.None;
                }
            }
        }
        #endregion

        #region utilitaires
        /// <summary>
        /// Obtient le document proprétaire d'un noeud
        /// </summary>
        /// <param name="node">Noeud dont on souhaite récupérer le document</param>
        /// <returns></returns>
        public BaseDoc GetDocFromParentNode(TreeNode node)
        {
            if (node.Tag is DocumentElementNode)
            {
                return ((DocumentElementNode)node.Tag).Document;
            }
            else if (node.Parent != null)
            {
                return GetDocFromParentNode(node.Parent);
            }
            return null;
        }

        /// <summary>
        /// Met a jour le texte du noeud passé en paramètre et tous ses enfants
        /// à partir de l'élément stocké dans le tag
        /// </summary>
        /// <param name="node"></param>
        private void UpdateNodeFromTag(TreeNode node)
        {
            string strTmp = GetLabelFromTag(node.Tag);
            if (!string.IsNullOrEmpty(strTmp))
                node.Text = strTmp;
            foreach (TreeNode child in node.Nodes)
            {
                UpdateNodeFromTag(child);
            }
        }

        /// <summary>
        /// Obtient le texte à afficher pour un noeud à partir d'un objet
        /// (actuellement ne fonctionne que pour les BaseObject
        /// </summary>
        /// <param name="obj">tag du TreeNode</param>
        /// <returns>Texte à afficher</returns>
        public string GetLabelFromTag(object obj)
        {
            if (obj is BaseObject)
            {
                BaseObject bobj = obj as BaseObject;
                return bobj.Symbol;
            }
            else
                return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string GetToolTipFromTag(object obj)
        {
            string returnedText = string.Empty;
            if (obj is BTScreen)
            {
                BaseObject bobj = obj as BaseObject;
                returnedText = bobj.GetToolTipText();
                returnedText += Program.LangSys.C("Right click for properties, double click to edit");
            }
            else if (obj is BaseObject)
            {
                BaseObject bobj = obj as BaseObject;
                returnedText = bobj.GetToolTipText();
                returnedText += Program.LangSys.C("Right click to edit");
            }
            else if (obj is BaseGest
                  || obj is BaseGestGroup.Group
                  || obj is DocumentElementNode)
            {
                returnedText += Program.LangSys.C("Right click for more options");
            }
            return returnedText;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void chkViewTips_CheckedChanged(object sender, EventArgs e)
        {
            List<TreeNode> listNodes = new List<TreeNode>();
            ListExpandedNodes(listNodes, this.Nodes);
            this.ShowNodeToolTips = m_chkViewTips.Checked;
            foreach (TreeNode node in listNodes)
            {
                node.Expand();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listNodes"></param>
        /// <param name="curNodes"></param>
        private void ListExpandedNodes(List<TreeNode> listNodes, TreeNodeCollection curNodes)
        {
            foreach (TreeNode node in curNodes)
            {
                if (node.IsExpanded)
                {
                    listNodes.Add(node);
                }
                ListExpandedNodes(listNodes, node.Nodes);
            }
        }

        private TreeNode GetTreeNodeFromTag(TreeNode startNode, object tag)
        {
            TreeNode nodeFound = null;
            foreach (TreeNode node in startNode.Nodes)
            {
                if (node.Tag == tag)
                    nodeFound = node;
                else
                {
                    nodeFound = GetTreeNodeFromTag(node, tag);
                }
                if (nodeFound != null)
                    return nodeFound;
            }
            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections;
using System.Windows.Forms;
using System.Text;
using System.IO;
using CommonLib;

namespace SmartApp
{
    class SolutionTreeView : TreeView
    {
        TreeNode m_SolutionNode = new TreeNode();
        SolutionGest m_GestSolution;
        ContextMenuStrip m_CtxMenuSolution = new ContextMenuStrip();
        ContextMenuStrip m_CtxMenuProject = new ContextMenuStrip();
        ContextMenuStrip m_CtxMenuGestGroups = new ContextMenuStrip();
        ContextMenuStrip m_CtxMenuGest = new ContextMenuStrip();
        ContextMenuStrip m_CtxMenuGroups = new ContextMenuStrip();
        ContextMenuStrip m_CtxMenuObject = new ContextMenuStrip();
        //ContextMenuStrip m_CtxMenuStrip = new ContextMenuStrip();
        struct DocumentElementNode
        {
            public TreeNode DocNode;
            public BTDoc Document;
        }

        BasePropertiesDialog m_PropDialog = new BasePropertiesDialog();

        SortedList<string, DocumentElementNode> m_ListDocument = new SortedList<string, DocumentElementNode>();

        /// <summary>
        /// 
        /// </summary>
        public SolutionTreeView()
        {
            m_SolutionNode.Text = Program.LangSys.C("Solution");
            if (Resources.TreeViewGroupIcon != null)
            {
                this.ImageList = new ImageList();
                //this.StateImageList = this.ImageList;
                this.ImageList.Images.Add("node", new System.Drawing.Bitmap(1,1));
                this.ImageList.Images.Add("Document", Resources.AppIcon);
                this.ImageList.Images.Add("Group", Resources.TreeViewGroupIcon);
                this.ImageList.Images.Add("Screen", Resources.TreeViewScreenIcon);
                this.ImageList.Images.Add("Timer", Resources.TreeViewTimerIcon);
                this.ImageList.Images.Add("Frame", Resources.TreeViewFrameIcon);
                this.ImageList.Images.Add("Function", Resources.TreeViewFunctionIcon);
                this.ImageList.Images.Add("Logger", Resources.TreeViewLoggerIcon);
                this.ImageList.Images.Add("IO", Resources.TreeViewIOIcon);
            }
            InitContextMenu();
        }

        public void InitContextMenu()
        {
            // menu de l'item solution
            ToolStripMenuItem item = new ToolStripMenuItem(Program.LangSys.C("Add existing project"));
            item.Click += new EventHandler(CtxMenuAddExistingProj_Click);
            m_CtxMenuSolution.Items.Add(item);
            item = new ToolStripMenuItem(Program.LangSys.C("Add new project"));
            item.Click += new EventHandler(CtxMenuAddNewProj_Click);
            m_CtxMenuSolution.Items.Add(item);

            // menus des item document
            item = new ToolStripMenuItem(Program.LangSys.C("Remove project from solution"));
            item.Click += new EventHandler(CtxMenuRemoveProj_Click);
            m_CtxMenuProject.Items.Add(item);
            item = new ToolStripMenuItem(Program.LangSys.C("Properties"));
            item.Click += new EventHandler(CtxMenuProjProperties_Click);
            m_CtxMenuProject.Items.Add(item);

            // menu pour un gestionnaire de group
            item = new ToolStripMenuItem(Program.LangSys.C("Add group"));
            item.Click += new EventHandler(CtxMenuGroupProperties_Click);
            m_CtxMenuGestGroups.Items.Add(item);
            item = new ToolStripMenuItem(Program.LangSys.C("Add item"));
            item.Click += new EventHandler(CtxMenuObjectNew_Click);
            m_CtxMenuGestGroups.Items.Add(item);

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
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (e.Button == MouseButtons.Right)
            {
                this.SelectedNode = this.GetNodeAt(e.X, e.Y);
                TreeNode selNode = this.SelectedNode;
                if (selNode == null)
                    return;

                if (selNode == m_SolutionNode)
                {
                    this.ContextMenuStrip = m_CtxMenuSolution;
                }
                else if (selNode.Tag is BaseObject)
                {
                    this.ContextMenuStrip = m_CtxMenuObject;
                }
                else if (selNode.Tag is BaseGestGroup.Group)
                {
                    this.ContextMenuStrip = m_CtxMenuGestGroups;
                }
                else if (selNode.Tag is DocumentElementNode)
                {
                    this.ContextMenuStrip = m_CtxMenuProject;
                }
                else if (selNode.Tag is BaseGest)
                {
                    this.ContextMenuStrip = m_CtxMenuGest;
                }
                else
                {
                    this.ContextMenuStrip = null;
                }
                if (this.ContextMenuStrip != null)
                    this.ContextMenuStrip.Show(e.Location);
            }
        }

        #region handler context menu
        void CtxMenuAddExistingProj_Click(object sender, EventArgs e)
        {
        }

        void CtxMenuAddNewProj_Click(object sender, EventArgs e)
        {
        }

        void CtxMenuRemoveProj_Click(object sender, EventArgs e)
        {
        }

        void CtxMenuProjProperties_Click(object sender, EventArgs e)
        {
        }

        void CtxMenuGroupProperties_Click(object sender, EventArgs e)
        {
        }

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
                bobj = gest.AddNewObject();
                imgKey = selNode.ImageKey;
                NewItemParentNode = selNode.FirstNode;
            }
            else if (selNode.Tag is BaseGestGroup.Group)
            {
                // le noeud parent possède un tag qui est le gestionnaire
                BaseGestGroup gest = selNode.Parent.Tag as BaseGestGroup;
                bobj = gest.AddNewObject(selNode.Text);
                imgKey = selNode.Parent.ImageKey;
                NewItemParentNode = selNode;
            }
            else if (selNode.Tag is BaseGest)
            {
                BaseGest gest = selNode.Tag as BaseGest;
                bobj = gest.AddNewObject();
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
                NewItemParentNode.Nodes.Add(newNode);
            }
        }

        void CtxMenuObjectDelete_Click(object sender, EventArgs e)
        {
        }

        void CtxMenuObjectProperties_Click(object sender, EventArgs e)
        {
            TreeNode selNode = this.SelectedNode;
            if (selNode.Tag is BaseObject)
            {
                m_PropDialog.ConfiguredItem = selNode.Tag as BaseObject;
                m_PropDialog.CurrentScreen = null;
                m_PropDialog.Document = GetDocFromParentNode(selNode);
                m_PropDialog.Initialize();
                m_PropDialog.ShowDialog();
            }
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            TreeNode selNode = this.SelectedNode;
            if (selNode.Tag is BaseObject && !(selNode.Tag is BTScreen))
            {
                CtxMenuObjectProperties_Click(this, null);
            }
        }

        public BTDoc GetDocFromParentNode(TreeNode node)
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
        #endregion

        #region attributs
        /// <summary>
        /// 
        /// </summary>
        public SolutionGest SolutionGest
        {
            get { return m_GestSolution; }
            set 
            { 
                m_GestSolution = value;
                if (m_GestSolution != null)
                {
                    m_GestSolution.OnDocOpened += new SolutionGest.DocumentOpenCloseEventHandler(AddDocument);
                    m_GestSolution.OnDocClosed += new SolutionGest.DocumentOpenCloseEventHandler(RemoveDocument);
                }
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc"></param>
        public void AddDocument(BTDoc doc)
        {
            if (!this.Nodes.Contains(m_SolutionNode))
            {
                this.Nodes.Add(m_SolutionNode);
            }

            TreeNode newNode = new TreeNode();
            DocumentElementNode docNode = new DocumentElementNode();
            docNode.Document = doc;
            docNode.DocNode = newNode;
            newNode.ImageKey = "Document";
            newNode.StateImageKey = "Document";
            newNode.SelectedImageKey = "Document";
            m_SolutionNode.Nodes.Add(newNode);
            newNode.Tag = docNode;
            newNode.Text = Path.GetFileName(doc.FileName);
            m_ListDocument.Add(newNode.Text, docNode);
            AddDocumentNodeGestsNodes(newNode.Text);
            doc.OnDocumentModified += new DocumentModifiedEvent(OnDocumentModified);
            docNode.DocNode.Expand();
            m_SolutionNode.Expand();
        }

        protected void OnDocumentModified()
        {
            UpdateNodeFromTag(m_SolutionNode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc"></param>
        public void RemoveDocument(BTDoc doc)
        {
            string docName = Path.GetFileName(doc.FileName);
            DocumentElementNode docElem = m_ListDocument[docName];
            m_SolutionNode.Nodes.Remove(docElem.DocNode);
            m_ListDocument.Remove(docName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="docName"></param>
        public void AddDocumentNodeGestsNodes(string docName)
        {
            DocumentElementNode docElem = m_ListDocument[docName];
            AddBaseGestNode(docName, docElem.Document.GestData, Program.LangSys.C("Datas"), "IO");
            AddBaseGestNode(docName, docElem.Document.GestScreen, Program.LangSys.C("Screens"), "Screen");
            AddBaseGestNode(docName, docElem.Document.GestTrame, Program.LangSys.C("Frames"), "Frame");
            AddBaseGestNode(docName, docElem.Document.GestTimer, Program.LangSys.C("Timers"), "Timer");
            AddBaseGestNode(docName, docElem.Document.GestFunction, Program.LangSys.C("Functions"), "Function");
            AddBaseGestNode(docName, docElem.Document.GestLogger, Program.LangSys.C("Loggers"), "Logger");
        }

        #region Ajout des gestionnaires
        public void AddBaseGestNode(string docName, BaseGest gest, string Label, string imageKey)
        {
            DocumentElementNode docElem = m_ListDocument[docName];
            TreeNode GestNode = new TreeNode(Label);
            GestNode.ImageKey = imageKey;
            GestNode.StateImageKey = imageKey;
            GestNode.SelectedImageKey = imageKey;
            GestNode.Tag = gest;
            docElem.DocNode.Nodes.Add(GestNode);
            if (gest is BaseGestGroup)
                AddGestGroupContent(GestNode, gest as BaseGestGroup, imageKey);
            else
                AddBaseGestContent(GestNode, gest, imageKey);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="gest"></param>
        /// <param name="imageKey"></param>
        private void AddGestGroupContent(TreeNode parentNode, BaseGestGroup gest, string imageKey)
        {
            for (int i = 0; i < gest.GroupCount; i++)
            {
                BaseGestGroup.Group CurrentGroup = gest.Groups[i];
                TreeNode grpNode = new TreeNode(CurrentGroup.GroupName);
                grpNode.Tag = CurrentGroup;
                grpNode.ImageKey = "Group";
                grpNode.StateImageKey = "Group";
                grpNode.SelectedImageKey ="Group";
                parentNode.Nodes.Add(grpNode);
                foreach (BaseObject item in CurrentGroup.Items)
                {
                    TreeNode ItemNode = new TreeNode(item.Symbol);
                    ItemNode.Tag = item;
                    ItemNode.ImageKey = imageKey;
                    ItemNode.StateImageKey = imageKey;
                    ItemNode.SelectedImageKey = imageKey;
                    grpNode.Nodes.Add(ItemNode);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="gest"></param>
        /// <param name="imageKey"></param>
        private void AddBaseGestContent(TreeNode parentNode, BaseGest gest, string imageKey)
        {
            for (int i = 0; i < gest.Count; i++ )
            {
                BaseObject item = gest[i];
                TreeNode ItemNode = new TreeNode(item.Symbol);
                ItemNode.ImageKey = imageKey;
                ItemNode.StateImageKey = imageKey;
                ItemNode.SelectedImageKey = imageKey;
                ItemNode.Tag = item;
                parentNode.Nodes.Add(ItemNode);
            }
        }
        #endregion

        /// <summary>
        /// 
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
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
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
    }
}

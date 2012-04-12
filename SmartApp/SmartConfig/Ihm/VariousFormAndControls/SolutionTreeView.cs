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
        #region données membres
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
        #endregion

        #region constructeur et init
        /// <summary>
        /// Constructeur
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

        /// <summary>
        /// initialise les menu contextuels
        /// </summary>
        private void InitContextMenu()
        {
            // menu de l'item solution
            ToolStripMenuItem item = new ToolStripMenuItem(Program.LangSys.C("Add existing project"));
            item.Click += new EventHandler(CtxMenuAddExistingProj_Click);
            m_CtxMenuSolution.Items.Add(item);
            item = new ToolStripMenuItem(Program.LangSys.C("Add new project"));
            item.Click += new EventHandler(CtxMenuAddNewProj_Click);
            item = new ToolStripMenuItem(Program.LangSys.C("Open solution directory"));
            item.Click += new EventHandler(CtxMenuOpenSolutionDir_Click);
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
            // gestion du clic souris avec le bouton droit
            // on fixe le menu contextuel approprié au type d'objet
            // et l'affiche si il y a lieu d'être
            if (e.Button == MouseButtons.Right)
            {
                this.SelectedNode = this.GetNodeAt(e.X, e.Y);
                TreeNode selNode = this.SelectedNode;
                if (selNode == null)
                    return;

                // la solution
                if (selNode == m_SolutionNode)
                {
                    this.ContextMenuStrip = m_CtxMenuSolution;
                }
                // un objet quelconque
                else if (selNode.Tag is BaseObject)
                {
                    this.ContextMenuStrip = m_CtxMenuObject;
                }
                // un groupe
                else if (selNode.Tag is BaseGestGroup.Group)
                {
                    this.ContextMenuStrip = m_CtxMenuGestGroups;
                }
                // un document
                else if (selNode.Tag is DocumentElementNode)
                {
                    this.ContextMenuStrip = m_CtxMenuProject;
                }
                // un gestionnaire de base
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

        /// <summary>
        /// gestion de l'évènement double clic sur un noeud
        /// </summary>
        /// <param name="e">Argument de l'évènement souris</param>
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            TreeNode selNode = this.SelectedNode;
            if (selNode.Tag is BaseObject && !(selNode.Tag is BTScreen))
            {
                CtxMenuObjectProperties_Click(this, e);
            }
            else if (selNode.Tag is BaseObject && selNode.Tag is BTScreen)
            {
                BTScreen scr = selNode.Tag as BTScreen;
                m_GestSolution.OpenScreenEditor(scr.Symbol, GetDocFromParentNode(selNode));
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

        }

        /// <summary>
        /// handler du menu propriété du projet
        /// </summary>
        /// <param name="sender">standard</param>
        /// <param name="e">standard</param>
        void CtxMenuProjProperties_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// handler du menu proprété d'un groupe
        /// </summary>
        /// <param name="sender">standard</param>
        /// <param name="e">standard</param>
        void CtxMenuGroupProperties_Click(object sender, EventArgs e)
        {

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
                bobj = gest.AddNewObject(GetDocFromParentNode(selNode));
                imgKey = selNode.ImageKey;
                NewItemParentNode = selNode.FirstNode;
            }
            else if (selNode.Tag is BaseGestGroup.Group)
            {
                // le noeud parent possède un tag qui est le gestionnaire
                BaseGestGroup gest = selNode.Parent.Tag as BaseGestGroup;
                bobj = gest.AddNewObject(GetDocFromParentNode(selNode), selNode.Text);
                imgKey = selNode.Parent.ImageKey;
                NewItemParentNode = selNode;
            }
            else if (selNode.Tag is BaseGest)
            {
                BaseGest gest = selNode.Tag as BaseGest;
                bobj = gest.AddNewObject(GetDocFromParentNode(selNode));
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

        /// <summary>
        /// handler du menu supprimer un élément
        /// </summary>
        /// <param name="sender">standard</param>
        /// <param name="e">standard</param>
        void CtxMenuObjectDelete_Click(object sender, EventArgs e)
        {

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
                m_PropDialog.ConfiguredItem = selNode.Tag as BaseObject;
                m_PropDialog.CurrentScreen = null;
                m_PropDialog.Document = GetDocFromParentNode(selNode);
                m_PropDialog.Initialize();
                m_PropDialog.ShowDialog();
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
                m_GestSolution = value;
                if (m_GestSolution != null)
                {
                    m_GestSolution.OnDocOpened += new SolutionGest.DocumentOpenCloseEventHandler(AddDocument);
                    m_GestSolution.OnDocClosed += new SolutionGest.DocumentOpenCloseEventHandler(RemoveDocument);
                }
            }
        }
        #endregion

        #region gestion de l'ajout/suppression/modification d'un document
        /// <summary>
        /// Ajout un document à l'arbre
        /// </summary>
        /// <param name="doc">Document à ajouter</param>
        public void AddDocument(BTDoc doc)
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
        public void RemoveDocument(BTDoc doc)
        {
            string docName = Path.GetFileName(doc.FileName);
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
            AddBaseGestNode(docName, docElem.Document.GestData, Program.LangSys.C("Datas"), "IO");
            AddBaseGestNode(docName, docElem.Document.GestScreen, Program.LangSys.C("Screens"), "Screen");
            AddBaseGestNode(docName, docElem.Document.GestTrame, Program.LangSys.C("Frames"), "Frame");
            AddBaseGestNode(docName, docElem.Document.GestTimer, Program.LangSys.C("Timers"), "Timer");
            AddBaseGestNode(docName, docElem.Document.GestFunction, Program.LangSys.C("Functions"), "Function");
            AddBaseGestNode(docName, docElem.Document.GestLogger, Program.LangSys.C("Loggers"), "Logger");
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
            docElem.DocNode.Nodes.Add(GestNode);
            if (gest is BaseGestGroup)
                AddGestGroupContent(GestNode, gest as BaseGestGroup, imageKey);
            else
                AddBaseGestContent(GestNode, gest, imageKey);
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
                TreeNode ItemNode = new TreeNode(item.Symbol);
                ItemNode.ImageKey = imageKey;
                ItemNode.StateImageKey = imageKey;
                ItemNode.SelectedImageKey = imageKey;
                ItemNode.Tag = item;
                parentNode.Nodes.Add(ItemNode);
            }
        }
        #endregion

        #region utilitaires
        /// <summary>
        /// Obtient le document proprétaire d'un noeud
        /// </summary>
        /// <param name="node">Noeud dont on souhaite récupérer le document</param>
        /// <returns></returns>
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
        #endregion
    }
}

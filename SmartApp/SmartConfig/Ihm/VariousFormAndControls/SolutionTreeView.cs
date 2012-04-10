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

        struct DocumentElementNode
        {
            public TreeNode DocNode;
            public BTDoc Document;
        }


        SortedList<string, DocumentElementNode> m_ListDocument = new SortedList<string, DocumentElementNode>();

        public SolutionTreeView()
        {
            m_SolutionNode.Text = Program.LangSys.C("Solution");
            if (Resources.TreeViewGroupIcon != null)
            {
                this.ImageList = new ImageList();
                //this.StateImageList = this.ImageList;
                this.ImageList.Images.Add("node", new System.Drawing.Bitmap(1,1));
                this.ImageList.Images.Add("Group", Resources.TreeViewGroupIcon);
                this.ImageList.Images.Add("Screen", Resources.TreeViewScreenIcon);
                this.ImageList.Images.Add("Timer", Resources.TreeViewTimerIcon);
                this.ImageList.Images.Add("Frame", Resources.TreeViewFrameIcon);
                this.ImageList.Images.Add("Function", Resources.TreeViewFunctionIcon);
                this.ImageList.Images.Add("Logger", Resources.TreeViewLoggerIcon);
                this.ImageList.Images.Add("IO", Resources.TreeViewIOIcon);
                
            }
        }

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
            m_SolutionNode.Nodes.Add(newNode);
            newNode.Text = Path.GetFileName(doc.FileName);
            m_ListDocument.Add(newNode.Text, docNode);
            AddDocumentNodeGestsNodes(newNode.Text);
            docNode.DocNode.Expand();
            m_SolutionNode.Expand();
        }

        public void RemoveDocument(BTDoc doc)
        {
            string docName = Path.GetFileName(doc.FileName);
            DocumentElementNode docElem = m_ListDocument[docName];
            m_SolutionNode.Nodes.Remove(docElem.DocNode);
            m_ListDocument.Remove(docName);
        }

        public void AddDocumentNodeGestsNodes(string docName)
        {
            AddDocumentGestData(docName);
            AddDocumentGestScreen(docName);
            AddDocumentGestFrame(docName);
            AddDocumentGestTimer(docName);
            AddDocumentGestFunction(docName);
            AddDocumentGestLogger(docName);
        }

        #region Ajout des gestionnaires
        private void AddDocumentGestData(string docName)
        {
            DocumentElementNode docElem = m_ListDocument[docName];
            TreeNode GestNode = new TreeNode(Program.LangSys.C("Datas"));
            GestNode.ImageKey = "IO";
            GestNode.StateImageKey = "IO";
            GestNode.SelectedImageKey = "IO";
            GestNode.Tag = docElem.Document.GestData;
            docElem.DocNode.Nodes.Add(GestNode);
            AddGestGroupContent(GestNode, docElem.Document.GestData, "IO");
        }

        private void AddDocumentGestScreen(string docName)
        {
            DocumentElementNode docElem = m_ListDocument[docName];
            TreeNode GestNode = new TreeNode(Program.LangSys.C("Screens"));
            GestNode.ImageKey = "Screen";
            GestNode.StateImageKey = "Screen";
            GestNode.SelectedImageKey = "Screen";
            GestNode.Tag = docElem.Document.GestScreen;
            docElem.DocNode.Nodes.Add(GestNode);
            AddBaseGestContent(GestNode, docElem.Document.GestScreen, "Screen");
        }

        private void AddDocumentGestFrame(string docName)
        {
            DocumentElementNode docElem = m_ListDocument[docName];
            TreeNode GestNode = new TreeNode(Program.LangSys.C("Frames"));
            GestNode.ImageKey = "Frame";
            GestNode.StateImageKey = "Frame";
            GestNode.SelectedImageKey = "Frame";
            GestNode.Tag = docElem.Document.GestTrame;
            docElem.DocNode.Nodes.Add(GestNode);
            AddBaseGestContent(GestNode, docElem.Document.GestTrame, "Frame");
        }

        private void AddDocumentGestTimer(string docName)
        {
            DocumentElementNode docElem = m_ListDocument[docName];
            TreeNode GestNode = new TreeNode(Program.LangSys.C("Timers"));
            GestNode.ImageKey = "Timer";
            GestNode.StateImageKey = "Timer";
            GestNode.SelectedImageKey = "Timer";
            GestNode.Tag = docElem.Document.GestTimer;
            docElem.DocNode.Nodes.Add(GestNode);
            AddBaseGestContent(GestNode, docElem.Document.GestTimer, "Timer");
        }

        private void AddDocumentGestFunction(string docName)
        {
            DocumentElementNode docElem = m_ListDocument[docName];
            TreeNode GestNode = new TreeNode(Program.LangSys.C("Functions"));
            GestNode.Tag = docElem.Document.GestFunction;
            GestNode.ImageKey = "Function";
            GestNode.StateImageKey = "Function";
            GestNode.SelectedImageKey = "Function";
            docElem.DocNode.Nodes.Add(GestNode);
            AddBaseGestContent(GestNode, docElem.Document.GestFunction, "Function");
        }

        private void AddDocumentGestLogger(string docName)
        {
            DocumentElementNode docElem = m_ListDocument[docName];
            TreeNode GestNode = new TreeNode(Program.LangSys.C("Loggers"));
            GestNode.Tag = docElem.Document.GestLogger;
            GestNode.ImageKey = "Logger";
            GestNode.StateImageKey = "Logger";
            GestNode.SelectedImageKey = "Logger";
            docElem.DocNode.Nodes.Add(GestNode);
            AddBaseGestContent(GestNode, docElem.Document.GestLogger, "Logger");
        }

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

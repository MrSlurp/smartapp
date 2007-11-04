using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartApp.Datas;
using SmartApp.Gestionnaires;

namespace SmartApp.Ihm
{
    public partial class ManageGroupForm : Form
    {
        #region données membres
        // gestionaire de donnée du document courant
        private GestData m_GestData = null;
        private List<BaseGestGroup.Group> m_GestGroupDest = new List<BaseGestGroup.Group>();

        #endregion

        public ManageGroupForm()
        {
            InitializeComponent();
        }

        #region attributs de la classe
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        public GestData GestData
        {
            get
            {
                return m_GestData;
            }
            set
            {
                m_GestData = value;
            }
        }
        #endregion

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        public void Initialize()
        {
            if (m_GestData == null)
                return;

            m_cboGroupSrc.DisplayMember = "GroupName";
            m_cboGroupSrc.ValueMember = "GroupSymbol";
            m_cboGroupSrc.DataSource = GestData.Groups;
            m_cboGroupSrc.SelectedIndex = 0;

            // si on utilise la meme data source pour les deux combo,
            // le changement de combo devienent synchronisé et donc la séléction est identique dans les deux
            // comme ce n'est pas ce qu'on veux, on crée une deuxième data source avec un contenu identique
            for (int i = 0; i < GestData.GroupCount; i++)
            {
                m_GestGroupDest.Add(GestData.Groups[i]);
            }
            m_cboGroupDest.DisplayMember = "GroupName";
            m_cboGroupDest.ValueMember = "GroupSymbol";
            m_cboGroupDest.DataSource = m_GestGroupDest;
            m_cboGroupDest.SelectedIndex = 0;

            InitListViewSrcFromGroup();
            InitListViewDestFromGroup();
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        public void InitListViewSrcFromGroup()
        {
            m_listViewDataSrc.Items.Clear();
            string strGroupName = (string)m_cboGroupSrc.SelectedValue;
            BaseGestGroup.Group gr = GestData.GetGroupFromSymbol(strGroupName);
            if (gr == null)
                return;
            ArrayList Lst = gr.Items;
            if (Lst != null)
            {
                for (int i = 0; i < Lst.Count; i++)
                {
                    Data dt = (Data)Lst[i];
                    if (dt.IsUserVisible)
                    {
                        ListViewItem lviData = new ListViewItem(dt.Symbol);
                        lviData.Tag = dt;
                        string strValues = string.Format("Def: {0}, Min: {1}, Max: {2}", dt.DefaultValue, dt.Minimum, dt.Maximum);
                        lviData.SubItems.Add(dt.IsConstant.ToString());
                        lviData.SubItems.Add(dt.Size.ToString());
                        lviData.SubItems.Add(strValues);
                        m_listViewDataSrc.Items.Add(lviData);
                    }
                }
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        public void InitListViewDestFromGroup()
        {
            m_ListViewDataDest.Items.Clear();
            string strGroupName = (string)m_cboGroupDest.SelectedValue;
            BaseGestGroup.Group gr = GestData.GetGroupFromSymbol(strGroupName);
            if (gr == null)
                return;
            ArrayList Lst = gr.Items;
            if (Lst != null)
            {
                for (int i = 0; i < Lst.Count; i++)
                {
                    Data dt = (Data)Lst[i];
                    if (dt.IsUserVisible)
                    {
                        ListViewItem lviData = new ListViewItem(dt.Symbol);
                        lviData.Tag = dt;
                        string strValues = string.Format("Def: {0}, Min: {1}, Max: {2}", dt.DefaultValue, dt.Minimum, dt.Maximum);
                        lviData.SubItems.Add(dt.IsConstant.ToString());
                        lviData.SubItems.Add(dt.Size.ToString());
                        lviData.SubItems.Add(strValues);
                        m_ListViewDataDest.Items.Add(lviData);
                    }
                }
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void OnlistViewSrcItemDrag(object sender, ItemDragEventArgs e)
        {
            try
            {
                Data lviData = (Data)((ListViewItem)e.Item).Tag;
                DoDragDrop(lviData, DragDropEffects.All);
            }
            catch (Exception)
            {

            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void OnListViewDestDragEnter(object sender, DragEventArgs e)
        {
            Data dt = (Data)e.Data.GetData(typeof(Data));
            bool bFind = false;
            for (int i = 0; i < m_ListViewDataDest.Items.Count; i++)
            {
                if (m_ListViewDataDest.Items[i].Text == dt.Symbol)
                {
                    bFind = true;
                    break;
                }
            }

            if (bFind)
            {
                e.Effect = DragDropEffects.None;
            }
            else
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void OnListViewFrameDataDragDrop(object sender, DragEventArgs e)
        {
            Data DropedItem = (Data)e.Data.GetData(typeof(Data));
            if (DropedItem != null)
            {
                bool bFind = false;
                ListViewItem LvFoundItem = null;
                for (int i = 0; i < m_ListViewDataDest.Items.Count; i++)
                {
                    if (m_ListViewDataDest.Items[i].Text == DropedItem.Symbol)
                    {
                        bFind = true;
                        LvFoundItem = m_ListViewDataDest.Items[i];
                        break;
                    }
                }

                if (bFind)
                {
                    // nothing to do
                    // normalement c'est interdit puisque dropeffects = none;
                }
                else
                {
                    // on ne travail pas directement sur les listes mais sur les groupes
                    string strSrcGroupName = (string)m_cboGroupSrc.SelectedValue;
                    ArrayList SrcLst = GestData.GetGroupFromSymbol(strSrcGroupName).Items;
                    string strDestGroupName = (string)m_cboGroupDest.SelectedValue;
                    ArrayList DestLst = GestData.GetGroupFromSymbol(strDestGroupName).Items;
                    if (SrcLst.Contains(DropedItem))
                    {
                        SrcLst.Remove(DropedItem);
                        DestLst.Add(DropedItem);
                        InitListViewSrcFromGroup();
                        InitListViewDestFromGroup();
                    }
                }
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void OnGroupSrcSelectedIndexChanged(object sender, EventArgs e)
        {
            InitListViewSrcFromGroup();
            string strGroupName = (string)m_cboGroupSrc.SelectedValue;
            BaseGestGroup.Group gr = GestData.GetGroupFromSymbol(strGroupName);
            if (gr == null)
                return;
            m_TextGroupText.Text = gr.GroupName;
            m_TextGroupColor.BackColor = gr.m_GroupColor;
            if (gr.GroupSymbol == BaseGestGroup.STR_DEFAULT_GROUP_SYMB)
            {
                m_btnDeleteGroup.Enabled = false;
                m_BtnSelectColor.Enabled = false;
                m_TextGroupText.Enabled = false;
            }
            else
            {
                m_btnDeleteGroup.Enabled = true;
                m_BtnSelectColor.Enabled = true;
                m_TextGroupText.Enabled = true;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void OnGroupDestSelectedIndexChanged(object sender, EventArgs e)
        {
            InitListViewDestFromGroup();
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void OnTextGroupTextValidating(object sender, CancelEventArgs e)
        {
            m_listViewDataSrc.Items.Clear();
            int SrcCboCurIndex = m_cboGroupSrc.SelectedIndex;
            int DestCboCurIndex = m_cboGroupDest.SelectedIndex;
            string strGroupName = (string)m_cboGroupSrc.SelectedValue;
            BaseGestGroup.Group gr = GestData.GetGroupFromSymbol(strGroupName);
            gr.m_strGroupName = m_TextGroupText.Text;
            InitCombosGroups();
            m_cboGroupSrc.SelectedIndex = SrcCboCurIndex;
            m_cboGroupDest.SelectedIndex = DestCboCurIndex;
        }

        private void OnBtnSelectColorClick(object sender, EventArgs e)
        {
            string strGroupName = (string)m_cboGroupSrc.SelectedValue;
            BaseGestGroup.Group gr = GestData.GetGroupFromSymbol(strGroupName);
            m_clrDlg.Color = gr.m_GroupColor;
            DialogResult DlgRes = m_clrDlg.ShowDialog();
            if (DlgRes == DialogResult.OK)
            {
                gr.m_GroupColor = m_clrDlg.Color;
                m_TextGroupColor.BackColor = gr.m_GroupColor;
            }
        }

        private void OnbtnDeleteGroupClick(object sender, EventArgs e)
        {
            string strGroupName = (string)m_cboGroupSrc.SelectedValue;
            BaseGestGroup.Group gr = GestData.GetGroupFromSymbol(strGroupName);
            GestData.DeleteGroup(gr.GroupSymbol);
            m_GestGroupDest.Clear();// on vide la copie et on la reconstruit
            for (int i = 0; i < GestData.GroupCount; i++)
            {
                m_GestGroupDest.Add(GestData.Groups[i]);
            }
            InitCombosGroups();
        }

        private void OnbtnNewGroupClick(object sender, EventArgs e)
        {
            string strNewGroupText = GestData.GetNextDefaultGroupText();
            BaseGestGroup.Group gr = GestData.CreateNewGroup(strNewGroupText, Color.White);
            // le groupe a été automatiquement au gestionaire au moment de sa création
            // on met a jour la copie de la liste
            m_GestGroupDest.Add(gr);
            InitCombosGroups();
        }

        private void InitCombosGroups()
        {
            // il faut remettre l'index selectionné a 0 avant de changer la data source
            // sinon ca merde (mais je sais pas trop pouquoi)
            m_cboGroupSrc.SelectedIndex = 0;
            m_cboGroupSrc.DataSource = null;
            m_cboGroupSrc.DisplayMember = "GroupName";
            m_cboGroupSrc.ValueMember = "GroupSymbol";
            m_cboGroupSrc.DataSource = GestData.Groups;

            m_cboGroupDest.SelectedIndex = 0;
            m_cboGroupDest.DataSource = null;
            m_cboGroupDest.DisplayMember = "GroupName";
            m_cboGroupDest.ValueMember = "GroupSymbol";
            m_cboGroupDest.DataSource = m_GestGroupDest;
        }
    }
}
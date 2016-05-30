/*
    This file is part of SmartApp.

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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommonLib;


namespace SmartApp
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
            Program.LangSys.Initialize(this);
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
                        lviData.SubItems.Add(dt.SizeInBits.ToString());
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
                        lviData.SubItems.Add(dt.SizeInBits.ToString());
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnbtnNewGroupClick(object sender, EventArgs e)
        {
            string strNewGroupText = GestData.GetNextDefaultGroupText();
            BaseGestGroup.Group gr = GestData.CreateNewGroup(strNewGroupText, Color.White);
            // le groupe a été automatiquement au gestionaire au moment de sa création
            // on met a jour la copie de la liste
            m_GestGroupDest.Add(gr);
            InitCombosGroups();
        }

        /// <summary>
        /// 
        /// </summary>
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

        private void m_ListViewDataDest_ItemDrag(object sender, ItemDragEventArgs e)
        {

        }

        private void btnMoveRight_Click(object sender, EventArgs e)
        {
            if (m_listViewDataSrc.SelectedItems.Count > 0)
            {
                string strSrcGroupName = (string)m_cboGroupSrc.SelectedValue;
                ArrayList SrcLst = GestData.GetGroupFromSymbol(strSrcGroupName).Items;
                string strDestGroupName = (string)m_cboGroupDest.SelectedValue;
                ArrayList DestLst = GestData.GetGroupFromSymbol(strDestGroupName).Items;
                foreach (ListViewItem item in m_listViewDataSrc.SelectedItems)
                {
                    Data data = item.Tag as Data;
                    if (SrcLst.Contains(data))
                    {
                        SrcLst.Remove(data);
                        DestLst.Add(data);
                    }
                }
                InitListViewSrcFromGroup();
                InitListViewDestFromGroup();
            }
        }

        private void btnMoveLeft_Click(object sender, EventArgs e)
        {
            if (m_ListViewDataDest.SelectedItems.Count > 0)
            {
                string strSrcGroupName = (string)m_cboGroupSrc.SelectedValue;
                ArrayList SrcLst = GestData.GetGroupFromSymbol(strSrcGroupName).Items;
                string strDestGroupName = (string)m_cboGroupDest.SelectedValue;
                ArrayList DestLst = GestData.GetGroupFromSymbol(strDestGroupName).Items;
                foreach (ListViewItem item in m_ListViewDataDest.SelectedItems)
                {
                    Data data = item.Tag as Data;
                    if (DestLst.Contains(data))
                    {
                        DestLst.Remove(data);
                        SrcLst.Add(data);
                    }
                }
                InitListViewSrcFromGroup();
                InitListViewDestFromGroup();
            }
        }
    }
}
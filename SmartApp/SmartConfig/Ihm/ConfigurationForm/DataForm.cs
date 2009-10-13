using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using CommonLib;

namespace SmartApp.Ihm
{
    public partial class DataForm : Form
    {
        #region données membres
        // gestionaire de donnée du document courant
        private BTDoc m_Document = null;
        // index séléctionnné dans la listview
        private int m_CurSelectedIndex = -1;
        #endregion

        #region attributs de la classe
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        public BTDoc Doc
        {
            get
            {
                return m_Document;
            }
            set
            {
                m_Document = value;
                // on assigne au control des propriété des données le meme gestionaire de donnée
                m_DataPropertyPage.Doc = m_Document;
            }
        }

        public GestData GestData
        {
            get
            {
                return m_Document.GestData;
            }
        }
        #endregion

        #region constructeurs et init
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        public DataForm()
        {
            InitializeComponent();
            m_DataPropertyPage.DataPropertiesChanged += new DataPropertiesChange(this.OnDataPropertiesChange);            
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        public void Initialize()
        {
            if (this.GestData == null)
                return;

            m_cboGroups.DataSource = null;
            m_cboGroups.DisplayMember = "GroupName";
            m_cboGroups.ValueMember = "GroupSymbol";
            m_cboGroups.DataSource = GestData.Groups;
            m_cboGroups.SelectedIndex = 0;

            InitListViewFromGroup();
        }

        #endregion

        #region Gestion de la listview
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        public void InitListViewFromGroup()
        {
            m_listViewData.Items.Clear();
            string strGroupName = (string)m_cboGroups.SelectedValue;
            if (string.IsNullOrEmpty(strGroupName))
                return;
            BaseGestGroup.Group gr = GestData.GetGroupFromSymbol(strGroupName);
            ArrayList Lst = gr.Items;
            if (gr == null)
                return;
            if (Lst != null)
            {
                for (int i = 0; i < Lst.Count; i++)
                {
                    Data dt = (Data)Lst[i];
                    if (dt.IsUserVisible)
                    {
                        ListViewItem lviData = new ListViewItem(dt.Symbol);
                        lviData.BackColor = gr.m_GroupColor;
                        lviData.Tag = dt;
                        string strValues = string.Format("Def: {0}, Min: {1}, Max: {2}", dt.DefaultValue, dt.Minimum, dt.Maximum);
                        lviData.SubItems.Add(dt.IsConstant.ToString());
                        lviData.SubItems.Add(dt.SizeInBits.ToString());
                        lviData.SubItems.Add(strValues);
                        m_listViewData.Items.Add(lviData);
                    }
                }
                m_DataPropertyPage.Data = null;
                m_CurSelectedIndex = -1;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        public void UpdateListView()
        {
            for (int i = 0; i < m_listViewData.Items.Count; i++)
            {
                ListViewItem lviData = m_listViewData.Items[i];
                Data dt = (Data)lviData.Tag;
                lviData.Text = ((Data)lviData.Tag).Symbol;
                string strGroupName = (string)m_cboGroups.SelectedValue;
                BaseGestGroup.Group gr = GestData.GetGroupFromSymbol(strGroupName);
                if (gr != null)
                {
                    lviData.BackColor = gr.m_GroupColor;
                }
                lviData.SubItems[1].Text = dt.IsConstant.ToString();
                lviData.SubItems[2].Text = dt.SizeInBits.ToString();
                string strValues = string.Format("Def: {0}, Min: {1}, Max: {2}", dt.DefaultValue, dt.Minimum, dt.Maximum);
                lviData.SubItems[3].Text = strValues;
                
            }
        }
        #endregion

        #region évènements de la combo et de la listview
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void OnSelectednGroupsChanged(object sender, EventArgs e)
        {
            InitListViewFromGroup();
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void listViewSelectedDataChanged(object sender, EventArgs e)
        {
            ListViewItem lviData = null;
            if (m_listViewData.SelectedItems.Count>0)
                lviData = m_listViewData.SelectedItems[0];
            if (lviData != null)
            {
                if (lviData.Index == m_CurSelectedIndex)
                    return;

                if (m_DataPropertyPage.IsDataValuesValid)
                {
                    m_CurSelectedIndex = lviData.Index;
                    string strDataSymb = lviData.Text;
                    BaseObject Dt = GestData.GetFromSymbol(strDataSymb);
                    m_DataPropertyPage.Data = (Data)Dt;
                }
                else
                {
                    lviData.Focused = false;
                    m_listViewData.Items[m_CurSelectedIndex].Selected = true;
                }
            }
            else
            {
                m_CurSelectedIndex = -1;
                m_DataPropertyPage.Data = null;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void OnlistViewItemDrag(object sender, ItemDragEventArgs e)
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
        private void OnListViewDataKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete
                || e.KeyCode == Keys.Back)
            {
                ListViewItem lviData = null;
                if (m_listViewData.SelectedItems.Count > 0)
                    lviData = m_listViewData.SelectedItems[0];
                if (lviData != null)
                {
                    if (GestData.RemoveObj((BaseObject)lviData.Tag))
                    {
                        //m_listViewData.Items.Remove(lviData);
                        if (m_DataPropertyPage.Data == (Data)lviData.Tag)
                        {
                            m_DataPropertyPage.Data = null;
                        }
                        InitListViewFromGroup();
                    }
                }
            }
        }
        #endregion

        #region handlers d'évènements divers
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        public void OnDataPropertiesChange(Data dat)
        {
            UpdateListView();
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void OnBtnNewClick(object sender, EventArgs e)
        {
            string strSymbol = GestData.GetNextDefaultSymbol();
            if (string.IsNullOrEmpty(strSymbol))
                return;
            Data NewDat = new Data();
            NewDat.Symbol = strSymbol;
            NewDat.SizeAndSign = (int)DATA_SIZE.DATA_SIZE_1B;
            NewDat.Maximum = 1;
            NewDat.Minimum = 0;
            NewDat.DefaultValue = 0;
            string strGroupName = (string)m_cboGroups.SelectedValue;
            GestData.AddObjAtGroup(NewDat, strGroupName);
            InitListViewFromGroup();
            m_Document.Modified = true;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (m_DataPropertyPage.IsDataValuesValid)
                {
                    this.WindowState = FormWindowState.Minimized;
                }
                e.Cancel = true;
            }
            else if (e.CloseReason == CloseReason.MdiFormClosing)
            {
                if (!m_DataPropertyPage.IsDataValuesValid)
                    e.Cancel = true;
            }
        }

        #endregion

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void OnManageGroupsClick(object sender, EventArgs e)
        {
            ManageGroupForm form = new ManageGroupForm();
            form.GestData = this.GestData;
            form.Initialize();
            form.ShowDialog();
            this.Initialize();
        }
    }
}
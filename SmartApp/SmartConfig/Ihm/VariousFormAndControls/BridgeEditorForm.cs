﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using CommonLib;

namespace SmartApp
{
    public partial class BridgeEditorForm : Form
    {
        protected SolutionGest m_Solution;
        protected DataBridgeInfo m_BridgeInfo;

        /// <summary>
        /// 
        /// </summary>
        public SolutionGest Solution
        {
            get { return m_Solution; }
            set 
            { 
                m_Solution = value;
                if (m_Solution != null)
                {
                    InitProjectList(cboSourceProj);
                    InitProjectList(cboTargetProj);
                    UpdateControlsStates();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DataBridgeInfo BridgeInfo
        {
            get { return m_BridgeInfo; }
            set 
            { 
                m_BridgeInfo = value;
                if (m_BridgeInfo != null)
                    InitFromBridgeInfo();

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Solution"></param>
        public BridgeEditorForm()
        {
            InitializeComponent();
            Program.LangSys.Initialize(this);
            btnAddDstData.Image = Resources.SimpleArrowLeft;
            btnRemDstData.Image = Resources.SimpleArrowRight;
            btnAddSrcData.Image = Resources.SimpleArrowRight;
            btnRemSrcData.Image = Resources.SimpleArrowLeft;

            btnDownDstData.Image = Resources.SimpleArrowDown;
            btnDownSrcData.Image = Resources.SimpleArrowDown;
            btnUpDstData.Image = Resources.SimpleArrowUp;
            btnUpSrcData.Image = Resources.SimpleArrowUp;

            //InitProjectList(cboSourceProj);
            //InitProjectList(cboTargetProj);
            UpdateControlsStates();
        }

        /// <summary>
        /// 
        /// </summary>
        protected void InitFromBridgeInfo()
        {
            if (m_BridgeInfo == null)
                return;

            numBridgePeriod.Value = m_BridgeInfo.ExecTimerPeriod;
            for (int i = 0; i< cboSourceProj.Items.Count; i++)
            {
                if (cboSourceProj.GetItemText(cboSourceProj.Items[i]) == m_BridgeInfo.SrcDoc)
                    cboSourceProj.SelectedIndex = i;
            }

            for (int i = 0; i < cboTargetProj.Items.Count; i++)
            {
                if (cboTargetProj.GetItemText(cboTargetProj.Items[i]) == m_BridgeInfo.DstDoc)
                    cboTargetProj.SelectedIndex = i;
            }

            foreach (string symbol in m_BridgeInfo.SrcDataList)
            {
                lstViewBridge.Items.Add(symbol).SubItems.Add(string.Empty);
            }
            foreach (string symbol in m_BridgeInfo.DstDataList)
            {
                bool bEmptyItemFound = false;
                foreach (ListViewItem lviBri in lstViewBridge.Items)
                {
                    if (string.IsNullOrEmpty(lviBri.SubItems[1].Text))
                    {
                        lviBri.SubItems[1].Text = symbol;
                        bEmptyItemFound = true;
                        break;
                    }
                }
                if (!bEmptyItemFound)
                    lstViewBridge.Items.Add(string.Empty).SubItems.Add(symbol);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected void SaveToBridgeInfo()
        {
            m_BridgeInfo.SrcDoc = cboSourceProj.SelectedText;
            m_BridgeInfo.DstDoc = cboTargetProj.SelectedText;
            m_BridgeInfo.SrcDataList.Clear();
            m_BridgeInfo.DstDataList.Clear();
            foreach (ListViewItem lvi in lstViewBridge.Items)
            {
                m_BridgeInfo.SrcDataList.Add(lvi.SubItems[0].Text);
                m_BridgeInfo.SrcDataList.Add(lvi.SubItems.Count >1? lvi.SubItems[1].Text : string.Empty);
            }
        }


        /// <summary>
        /// met a jour l'état des différents en éléments en fonction des sélection courantes
        /// </summary>
        protected void UpdateControlsStates()
        {
            bool bSrcItemEnabled = cboSourceProj.SelectedValue != null;
            cboSourceGrpFilter.Enabled = bSrcItemEnabled;
            lstViewSourceDatas.Enabled = bSrcItemEnabled;
            btnAddSrcData.Enabled = bSrcItemEnabled;
            btnRemSrcData.Enabled = bSrcItemEnabled;
            btnDownSrcData.Enabled = bSrcItemEnabled;
            btnUpSrcData.Enabled = bSrcItemEnabled;
            bool bDstItemEnabled = cboTargetProj.SelectedValue != null;
            cboTargetGrpFilter.Enabled = bDstItemEnabled;
            lstViewTargetDatas.Enabled = bDstItemEnabled;
            btnAddDstData.Enabled = bDstItemEnabled;
            btnRemDstData.Enabled = bDstItemEnabled;
            btnDownDstData.Enabled = bDstItemEnabled;
            btnUpDstData.Enabled = bDstItemEnabled;
            if (!bDstItemEnabled || !bDstItemEnabled)
                lstViewBridge.Enabled = false;
            else
                lstViewBridge.Enabled = true;

            if (lstViewBridge.Items.Count > 0)
            {
                cboSourceProj.Enabled = false;
                cboTargetProj.Enabled = false;
            }
            else
            {
                cboSourceProj.Enabled = true;
                cboTargetProj.Enabled = true;
            }
        }

        /// <summary>
        /// initialise une liste déroulante à partir de la liste des projets de la solution
        /// </summary>
        /// <param name="cboProjList"></param>
        protected void InitProjectList(ComboBox cboProjList)
        {
            cboProjList.DataSource = null;
            List<CComboData> listItem = new List<CComboData>();
            listItem.Add(new CComboData(Program.LangSys.C("none"), null));
            foreach (string key in m_Solution.Keys)
            {
                if (m_Solution[key] is BTDoc)
                {
                    listItem.Add(new CComboData(Path.GetFileNameWithoutExtension(key), m_Solution[key]));
                }
            }
            cboProjList.ValueMember = "Object";
            cboProjList.DisplayMember = "DisplayedString";
            cboProjList.DataSource = listItem;
            if (cboProjList.Items.Count != 0)
                cboProjList.SelectedIndex = 0;
        }

        /// <summary>
        /// initialise une liste déroulante avec la liste des groupes de donnée
        /// </summary>
        /// <param name="cboGrpList"></param>
        /// <param name="cboProj"></param>
        protected void InitProjectGroupsList(ComboBox cboGrpList, ComboBox cboProj)
        {
            cboGrpList.DataSource = null;
            if (cboProj.SelectedValue == null)
                return;
            BTDoc document = cboProj.SelectedValue as BTDoc;
            if (document != null)
            {
                List<CComboData> listItem = new List<CComboData>();
                listItem.Add(new CComboData(Program.LangSys.C("all"), null));
                GestData gdata = document.GestData;
                for (int i = 0; i < gdata.GroupCount; i++)
                {
                    BaseGestGroup.Group grp = gdata.Groups[i];
                    listItem.Add(new CComboData(grp.GroupName, grp));
                }
                cboGrpList.ValueMember = "Object";
                cboGrpList.DisplayMember = "DisplayedString";
                cboGrpList.DataSource = listItem;
                if (cboGrpList.Items.Count != 0)
                    cboGrpList.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lvDataList"></param>
        /// <param name="cboProj"></param>
        /// <param name="cboGroup"></param>
        protected void InitProjDataList(ListView lvDataList, ComboBox cboProj, ComboBox cboGroup)
        {
            // aucun projet seléctionné, on sort
            lvDataList.Items.Clear();
            if (cboProj.SelectedValue == null)
                return;
            if (cboGroup.SelectedValue == null)
            {
                //toutes les données triées par groupe
                BTDoc doc = cboProj.SelectedValue as BTDoc;
                if (doc != null)
                {
                    GestData gdata = doc.GestData;
                    for (int i = 0; i < gdata.GroupCount; i++)
                    {
                        BaseGestGroup.Group grp = gdata.Groups[i];
                        for (int j = 0; j < grp.Items.Count; j++)
                        {
                            Data dt = grp.Items[j] as Data;
                            ListViewItem lvi = lvDataList.Items.Add(dt.Symbol);
                            lvi.SubItems.Add(dt.SizeInBits.ToString());
                        }
                    }
                }
            }
            else
            {
                //toutes les données d'un groupe
                BTDoc document = cboProj.SelectedValue as BTDoc;
                if (document != null)
                {
                    BaseGestGroup.Group grp = cboGroup.SelectedValue as BaseGestGroup.Group;
                    if (grp != null)
                    {
                        for (int j = 0; j < grp.Items.Count; j++)
                        {
                            Data dt = grp.Items[j] as Data;
                            ListViewItem lvi = lvDataList.Items.Add(dt.Symbol);
                            lvi.SubItems.Add(dt.SizeInBits.ToString());
                        }
                    }
                }
            }
        }

        #region combo selected index change
        private void cboSourceProj_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitProjectGroupsList(cboSourceGrpFilter, cboSourceProj);
            UpdateControlsStates();
        }

        private void cboTargetProj_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitProjectGroupsList(cboTargetGrpFilter, cboTargetProj);
            UpdateControlsStates();
        }

        private void cboSourceGrpFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitProjDataList(lstViewSourceDatas, cboSourceProj, cboSourceGrpFilter);
        }

        private void cboTargetGrpFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitProjDataList(lstViewTargetDatas, cboTargetProj, cboTargetGrpFilter);
        }
        #endregion

        #region Add/remove datas
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddRemSrcData_Click(object sender, EventArgs e)
        {
            if (sender == btnAddSrcData)
            {
                foreach (ListViewItem lviSrc in lstViewSourceDatas.SelectedItems)
                {
                    // si on a un item avec la source vide, on comble d'abord le vide
                    // sinon on ajoute un item
                    bool bEmptyItemFound = false;
                    foreach (ListViewItem lviBri in lstViewBridge.Items)
                    {
                        if (lviBri.Text == string.Empty)
                        {
                            lviBri.Text = lviSrc.Text;
                            bEmptyItemFound = true;
                            break;
                        }
                    }
                    if (!bEmptyItemFound)
                    {
                        lstViewBridge.Items.Add(lviSrc.Text).SubItems.Add(string.Empty);
                    }
                }
            }
            else if (sender == btnRemSrcData)
            {

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddRemDstData_Click(object sender, EventArgs e)
        {
            if (sender == btnAddDstData)
            {
                foreach (ListViewItem lviSrc in lstViewTargetDatas.SelectedItems)
                {
                    // si on a un item avec la source vide, on comble d'abord le vide
                    // sinon on ajoute un item
                    bool bEmptyItemFound = false;
                    foreach (ListViewItem lviBri in lstViewBridge.Items)
                    {
                        if (lviBri.SubItems[1].Text == string.Empty)
                        {
                            lviBri.SubItems[1].Text = lviSrc.Text;
                            bEmptyItemFound = true;
                            break;
                        }
                    }
                    if (!bEmptyItemFound)
                    {
                        lstViewBridge.Items.Add(string.Empty).SubItems.Add(lviSrc.Text);
                    }
                }
            }
            else if (sender == btnRemDstData)
            {

            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpDownSrcData_Click(object sender, EventArgs e)
        {
            if (sender == btnUpSrcData)
            {

            }
            else if (sender == btnDownSrcData)
            {

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpDownDstData_Click(object sender, EventArgs e)
        {
            if (sender == btnUpDstData)
            {

            }
            else if (sender == btnDownDstData)
            {

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            this.SaveToBridgeInfo();
        }
    }
}

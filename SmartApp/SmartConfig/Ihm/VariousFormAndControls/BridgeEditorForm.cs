using System;
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

            btnDownData.Image = Resources.SimpleArrowDown;
            btnUpData.Image = Resources.SimpleArrowUp;

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

            txtSymbol.Text = m_BridgeInfo.Symbol;
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

            for (int i = 0; i < cboPostFunc.Items.Count; i++)
            {
                if (cboPostFunc.GetItemText(cboPostFunc.Items[i]) == m_BridgeInfo.PostExecFunction)
                    cboPostFunc.SelectedIndex = i;
            }
            foreach (string symbol in m_BridgeInfo.SrcDataList)
            {
                gridViewBridge.Rows.Add(symbol, string.Empty);
            }
            foreach (string symbol in m_BridgeInfo.DstDataList)
            {
                bool bEmptyItemFound = false;
                foreach (DataGridViewRow row in gridViewBridge.Rows)
                {
                    if (string.IsNullOrEmpty(row.Cells[1].Value.ToString()))
                    {
                        row.Cells[1].Value = symbol;
                        bEmptyItemFound = true;
                        break;
                    }
                }
                if (!bEmptyItemFound)
                    gridViewBridge.Rows.Add(string.Empty, symbol);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected void SaveToBridgeInfo()
        {
            m_BridgeInfo.Symbol = txtSymbol.Text;
            CComboData objSrcProj = cboSourceProj.SelectedItem as CComboData;
            CComboData objDstProj = cboTargetProj.SelectedItem as CComboData;
            if (objSrcProj != null)
                m_BridgeInfo.SrcDoc = objSrcProj.DisplayedString;

            if (objDstProj != null)
                m_BridgeInfo.DstDoc = objDstProj.DisplayedString;

            m_BridgeInfo.SrcDataList.Clear();
            m_BridgeInfo.DstDataList.Clear();
            foreach (DataGridViewRow row in gridViewBridge.Rows)
            {
                m_BridgeInfo.SrcDataList.Add(row.Cells[0].Value.ToString());
                m_BridgeInfo.DstDataList.Add(row.Cells.Count > 1 ? row.Cells[1].Value.ToString() : string.Empty);
            }
            CComboData objPostFunc = cboPostFunc.SelectedItem as CComboData;
            m_BridgeInfo.PostExecFunction = objPostFunc.DisplayedString;
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
            bool bDstItemEnabled = cboTargetProj.SelectedValue != null;
            cboTargetGrpFilter.Enabled = bDstItemEnabled;
            lstViewTargetDatas.Enabled = bDstItemEnabled;
            cboPostFunc.Enabled = bDstItemEnabled;
            btnAddDstData.Enabled = bDstItemEnabled;
            if (!bDstItemEnabled || !bDstItemEnabled)
                gridViewBridge.Enabled = false;
            else
                gridViewBridge.Enabled = true;

            if (gridViewBridge.Rows.Count > 0)
            {
                cboSourceProj.Enabled = false;
                cboTargetProj.Enabled = false;
            }
            else
            {
                cboSourceProj.Enabled = true;
                cboTargetProj.Enabled = true;
            }

            bool bCellsSelectedInSources = false;
            bool bCellsSelectedInDest = false;
            bool bFirstRowSelected = false;
            bool bLastRowSelected = false;
            foreach (DataGridViewCell cell in gridViewBridge.SelectedCells)
            {
                if (cell.ColumnIndex == 0)
                    bCellsSelectedInSources = true;
                if (cell.ColumnIndex == 1)
                    bCellsSelectedInDest = true;

                if (cell.RowIndex == 0)
                    bFirstRowSelected = true;
                if (cell.RowIndex == gridViewBridge.Rows.Count - 1)
                    bLastRowSelected = true;

            }

            btnRemSrcData.Enabled = bCellsSelectedInSources;
            btnDownData.Enabled = (bCellsSelectedInSources || bCellsSelectedInDest) && !bLastRowSelected;
            btnUpData.Enabled = (bCellsSelectedInSources || bCellsSelectedInDest) && !bFirstRowSelected;
            btnRemDstData.Enabled = bCellsSelectedInDest;
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

        protected void InitCboPostScript(ComboBox cboProjList)
        {
            cboPostFunc.DataSource = null;
            if (cboProjList.SelectedValue == null)
                return;

            List<CComboData> listItem = new List<CComboData>();
            listItem.Add(new CComboData(Program.LangSys.C("none"), null));
            BTDoc document = cboProjList.SelectedValue as BTDoc;
            if (document != null)
            {
                for (int i = 0; i < document.GestFunction.Count; i++)
                {
                    BaseObject obj = document.GestFunction[i];
                    listItem.Add(new CComboData(obj.Symbol, obj));
                }
            }
            cboPostFunc.ValueMember = "Object";
            cboPostFunc.DisplayMember = "DisplayedString";
            cboPostFunc.DataSource = listItem;
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
            InitCboPostScript(cboTargetProj);
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
                    foreach (DataGridViewRow row in gridViewBridge.Rows)
                    {
                        if (row.Cells[0].Value.ToString() == string.Empty)
                        {
                            row.Cells[0].Value = lviSrc.Text;
                            bEmptyItemFound = true;
                            break;
                        }
                    }
                    if (!bEmptyItemFound)
                    {
                        gridViewBridge.Rows.Add(lviSrc.Text, string.Empty);
                    }
                }
            }
            else if (sender == btnRemSrcData)
            {
                foreach (DataGridViewCell cell in gridViewBridge.SelectedCells)
                {
                    cell.Value = string.Empty;
                }
                CleanBridgeEmptyLine();
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
                    foreach (DataGridViewRow row in gridViewBridge.Rows)
                    {
                        if (row.Cells[1].Value.ToString() == string.Empty)
                        {
                            row.Cells[1].Value = lviSrc.Text;
                            bEmptyItemFound = true;
                            break;
                        }
                    }
                    if (!bEmptyItemFound)
                    {
                        gridViewBridge.Rows.Add(string.Empty, lviSrc.Text);
                    }
                }
            }
            else if (sender == btnRemDstData)
            {
                foreach (DataGridViewCell cell in gridViewBridge.SelectedCells)
                {
                    cell.Value = string.Empty;
                }
                CleanBridgeEmptyLine();
            }
        }
        #endregion

        protected void CleanBridgeEmptyLine()
        {
            List<DataGridViewRow> rowsToDel = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in gridViewBridge.Rows)
            {
                if (string.IsNullOrEmpty(row.Cells[0].Value.ToString())
                    && string.IsNullOrEmpty(row.Cells[1].Value.ToString()) )
                {
                    rowsToDel.Add(row);
                }
            }
            foreach (DataGridViewRow r in rowsToDel)
            {
                gridViewBridge.Rows.Remove(r);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpDownData_Click(object sender, EventArgs e)
        {
            DataGridViewCell cell = gridViewBridge.SelectedCells.Count > 0 ? gridViewBridge.SelectedCells[0] : null;
            if (sender == btnUpData)
            {
                if (cell != null && (cell.RowIndex - 1) >= 0)
                {
                    string selCellValue = cell.Value.ToString();
                    string replacedCellValue = gridViewBridge.Rows[cell.RowIndex - 1].Cells[cell.ColumnIndex].Value.ToString();
                    gridViewBridge.Rows[cell.RowIndex - 1].Cells[cell.ColumnIndex].Value = selCellValue;
                    cell.Value = replacedCellValue;
                    gridViewBridge.Rows[cell.RowIndex - 1].Cells[cell.ColumnIndex].Selected = true; ;
                }
            }
            else if (sender == btnDownData)
            {
                if (cell != null && (cell.RowIndex + 1) <= gridViewBridge.Rows.Count - 1)
                {
                    string selCellValue = cell.Value.ToString();
                    string replacedCellValue = gridViewBridge.Rows[cell.RowIndex + 1].Cells[cell.ColumnIndex].Value.ToString();
                    gridViewBridge.Rows[cell.RowIndex + 1].Cells[cell.ColumnIndex].Value = selCellValue;
                    cell.Value = replacedCellValue;
                    gridViewBridge.Rows[cell.RowIndex + 1].Cells[cell.ColumnIndex].Selected = true;
                }
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

        private void gridViewBridge_SelectionChanged(object sender, EventArgs e)
        {
            UpdateControlsStates();
        }
    }
}

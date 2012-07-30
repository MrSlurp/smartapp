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
        public SolutionGest m_Solution;

        List<string> m_listProjects = new List<string>();
        SortedList<string, List<string>> m_listProjToDatas = new SortedList<string, List<string>>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Solution"></param>
        public BridgeEditorForm(SolutionGest Solution)
        {
            m_Solution = Solution;
            InitializeComponent();
            m_listProjects.Add(Program.LangSys.C("<empty>"));
            foreach (BaseDoc baseDoc in m_Solution.Values)
            {
                if (baseDoc is BTDoc)
                {
                    BTDoc doc = baseDoc as BTDoc;
                    string projName = Path.GetFileNameWithoutExtension(baseDoc.FileName);
                    m_listProjects.Add(projName);
                    if (!m_listProjToDatas.ContainsKey(projName))
                    {
                        m_listProjToDatas.Add(projName, new List<string>());
                    }
                    BaseGest gest = doc.GestData;
                    for (int i = 0; i < gest.Count; i++ )
                    {
                        BaseObject item = gest[i];
                        if (item.IsUserVisible)
                        {
                            m_listProjToDatas[projName].Add(item.Symbol);
                        }
                    }
                }
            }
            InitExchangeGridProjectCells();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridExchangeTable_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gridExchangeTable_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //gridExchangeTable.sele
            /*
            if (e.Control != null)
            {
                DataGridViewComboBoxEditingControl combo = e.Control as DataGridViewComboBoxEditingControl;
                combo.DataSource = 
            }*/
            /*
            if (e. >= 0 && e.RowIndex < gridExchangeTable.Rows.Count
                && e.ColumnIndex >= 0 && e.ColumnIndex < gridExchangeTable.ColumnCount)
            {
                DataGridViewComboBoxCell cell = gridExchangeTable.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewComboBoxCell;
                if (e.ColumnIndex == colSrcProj.Index)
                {
                    //cell.Items = m_solution.Keys;
                }
                else if (e.ColumnIndex == colSrcData.Index)
                {
                    DataGridViewComboBoxCell projCell = gridExchangeTable.Rows[e.RowIndex].Cells[colSrcProj.Index] as DataGridViewComboBoxCell;
                    if (m_listProjToDatas.ContainsKey(projCell.Value))
                        cell.Items = m_listProjToDatas[projCell.Value];
                }
                else if (e.ColumnIndex == colDstProj.Index)
                {

                }
                else if (e.ColumnIndex == colDstData.Index)
                {
                    DataGridViewComboBoxCell projCell = gridExchangeTable.Rows[e.RowIndex].Cells[colDstProj.Index] as DataGridViewComboBoxCell;
                    if (m_listProjToDatas.ContainsKey(projCell.Value))
                        cell.Items = m_listProjToDatas[projCell.Value];

                }
            }*/
        }

        private void InitExchangeGridProjectCells()
        {
            for (int i = 0; i < gridExchangeTable.Rows.Count; i++)
            {
                DataGridViewComboBoxCell srcProjCell = gridExchangeTable.Rows[i].Cells[colSrcProj.Index] as DataGridViewComboBoxCell;
                DataGridViewComboBoxCell dstProjCell = gridExchangeTable.Rows[i].Cells[colDstProj.Index] as DataGridViewComboBoxCell;
                if (srcProjCell.DataSource == null)
                {
                    srcProjCell.DataSource = m_listProjects;
                    srcProjCell.Value = m_listProjects[0];
                }
                if (dstProjCell.DataSource == null)
                {
                    dstProjCell.DataSource = m_listProjects;
                    dstProjCell.Value = m_listProjects[0];
                }
                if (i > 1)
                {
                    DataGridViewComboBoxCell prevSrcProjCell = gridExchangeTable.Rows[i].Cells[colSrcProj.Index] as DataGridViewComboBoxCell;
                    DataGridViewComboBoxCell prevDstProjCell = gridExchangeTable.Rows[i].Cells[colDstProj.Index] as DataGridViewComboBoxCell;
                    srcProjCell.Value = prevDstProjCell.Value;
                    dstProjCell.Value = prevDstProjCell.Value;
                }

            }
        }

        private void gridExchangeTable_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            InitExchangeGridProjectCells();
        }
    }
}

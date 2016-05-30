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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CommonLib
{
    public partial class PickDataForm : Form
    {
        BTDoc m_Document = null;

        Data[] m_SelectedDatas = null;

        bool m_HaveEmptyItem = true;

        bool m_bAllowMultipleSelect = false;

        /// <summary>
        /// 
        /// </summary>
        public bool AllowMultipleSelect
        {
            get
            {
                return m_bAllowMultipleSelect;
            }
            set
            {
                m_bAllowMultipleSelect = value;
                if (m_bAllowMultipleSelect)
                    m_listViewData.MultiSelect = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public PickDataForm()
        {
            Lang.LangSys.Initialize(this);
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        public BTDoc Document
        {
            set
            {
                m_Document = value;
            }
            get
            {
                return m_Document;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Data SelectedData
        {
            get
            {
                if (m_SelectedDatas != null && m_SelectedDatas.Length > 0)
                    return m_SelectedDatas[0];
                else
                    return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Data[] SelectedDatas
        {
            get
            {
                return m_SelectedDatas;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            InitListViewData();
        }

        /// <summary>
        /// 
        /// </summary>
        public void InitListViewData()
        {
            m_listViewData.Items.Clear();
            if (m_Document == null)
                return;

            // on ajoute un élément vide
            if (m_HaveEmptyItem)
            {
                ListViewItem lviData = new ListViewItem(Lang.LangSys.C("No Data"));
                lviData.SubItems.Add("NA");
                m_listViewData.Items.Add(lviData);
            }

            GestData DataGest = Document.GestData;
            for (int i = 0; i < DataGest.GroupCount; i++)
            {

                BaseGestGroup.Group grp = DataGest.Groups[i];
                for (int j = 0; j < grp.Items.Count; j++)
                {
                    Data dt = (Data)grp.Items[j];
                    if (dt == null)
                    {
                        System.Diagnostics.Debug.Assert(false);
                        continue;
                    }
                    if (((dt.IsConstant && cboShowConst.Checked) || !dt.IsConstant ) && dt.IsUserVisible)
                    {
                        ListViewItem lviData = new ListViewItem(dt.Symbol);
                        lviData.Tag = dt;
                        BaseGestGroup.Group gr = DataGest.GetGroupFromObject(dt);
                        if (gr != null)
                        {
                            lviData.BackColor = gr.m_GroupColor;
                        }
                        lviData.SubItems.Add(dt.SizeInBits.ToString());
                        m_listViewData.Items.Add(lviData);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_listViewData_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_listViewData.SelectedItems.Count == 0)
            {
                m_SelectedDatas = null;
            }
            else 
            {
                m_SelectedDatas = new Data[m_listViewData.SelectedItems.Count];
                for (int i = 0; i < m_listViewData.SelectedItems.Count; i++)
                {
                    m_SelectedDatas[i] = (Data)m_listViewData.SelectedItems[i].Tag;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboShowConst_CheckedChanged(object sender, EventArgs e)
        {
            InitListViewData();
        }
    }
}
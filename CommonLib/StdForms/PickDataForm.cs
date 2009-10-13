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

        public PickDataForm()
        {
            InitializeComponent();
        }

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

        public Data[] SelectedDatas
        {
            get
            {
                return m_SelectedDatas;
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            InitListViewData();
        }

        public void InitListViewData()
        {
            m_listViewData.Items.Clear();
            if (m_Document == null)
                return;

            // on ajoute un élément vide
            if (m_HaveEmptyItem)
            {
                ListViewItem lviData = new ListViewItem("No Data");
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
    }
}
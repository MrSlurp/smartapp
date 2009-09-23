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

        Data m_SelectedData = null;

        bool m_HaveEmptyItem = true;

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
                return m_SelectedData;
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
            ListViewItem lviData = null;
            if (m_listViewData.SelectedItems.Count>0)
                lviData = m_listViewData.SelectedItems[0];
            if (lviData != null)
            {
                m_SelectedData = (Data)lviData.Tag;
            }
        }
    }
}
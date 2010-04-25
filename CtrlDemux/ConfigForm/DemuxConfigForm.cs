using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommonLib;

namespace CtrlDemux
{
    internal partial class DemuxConfigForm : Form
    {
        private BTDoc m_Document = null;
        DllCtrlDemuxProp m_Props = new DllCtrlDemuxProp();

        #region attributs
        public BTDoc Doc
        {
            get
            {
                return m_Document;
            }
            set
            {
                m_Document = value;
            }
        }

        public DllCtrlDemuxProp Props
        {
            get
            {
                CopyOutputDataFromListView(m_Props.ListDemuxData);
                m_Props.AdressData = edtAddrData.Text;
                m_Props.ValueData = edtValueData.Text;
                return m_Props;
            }
            set
            {
                if (value != null)
                {
                    m_Props.CopyParametersFrom(value, false);
                    edtAddrData.Text = m_Props.AdressData;
                    edtValueData.Text = m_Props.ValueData;
                    CopyOutputDataToListView(m_Props.ListDemuxData);
                }
                else
                {
                    m_listViewData.Items.Clear();
                    edtAddrData.Text = String.Empty;
                    edtValueData.Text = String.Empty;
                }
            }
        }
        #endregion

        public DemuxConfigForm()
        {
            DllEntryClass.LangSys.Initialize(this);
            InitializeComponent();
            m_listViewData_SelectedIndexChanged(m_listViewData, null);
        }

        private void CopyOutputDataFromListView(StringCollection stringCollection)
        {
            stringCollection.Clear();
            for (int i = 0; i < m_listViewData.Items.Count; i++)
            {
                Data dt = (Data)m_listViewData.Items[i].Tag;
                stringCollection.Add(dt.Symbol);
            }
        }

        private void CopyOutputDataToListView(StringCollection stringCollection)
        {
            m_listViewData.Items.Clear();
            if (m_Document == null)
                return;

            for (int i = 0; i < stringCollection.Count; i++)
            {
                Data dt = (Data)Doc.GestData.GetFromSymbol(stringCollection[i]);
                ListViewItem lviData = new ListViewItem(dt.Symbol);
                lviData.Tag = dt;
                lviData.SubItems.Add(dt.SizeInBits.ToString());
                m_listViewData.Items.Add(lviData);
            }
        }

        private void btnPickAddr_Click(object sender, EventArgs e)
        {
            PickDataForm PickData = new PickDataForm();
            PickData.Document = this.Doc;
            if (PickData.ShowDialog() == DialogResult.OK)
            {
                if (PickData.SelectedData != null)
                    edtAddrData.Text = PickData.SelectedData.Symbol;
                else
                    edtAddrData.Text = string.Empty;
            }
        }

        private void btnPickValue_Click(object sender, EventArgs e)
        {
            PickDataForm PickData = new PickDataForm();
            PickData.Document = this.Doc;
            if (PickData.ShowDialog() == DialogResult.OK)
            {
                if (PickData.SelectedData != null)
                    edtValueData.Text = PickData.SelectedData.Symbol;
                else
                    edtValueData.Text = string.Empty;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            PickDataForm PickData = new PickDataForm();
            PickData.AllowMultipleSelect = true;
            PickData.Document = this.Doc;
            if (PickData.ShowDialog() == DialogResult.OK)
            {
                if (PickData.SelectedDatas != null)
                {
                    for (int i = 0; i < PickData.SelectedDatas.Length; i++)
                    {
                        if (PickData.SelectedDatas[i] != null)
                        {
                            Data dt = PickData.SelectedDatas[i];
                            ListViewItem lviData = new ListViewItem(dt.Symbol);
                            lviData.Tag = dt;
                            lviData.SubItems.Add(dt.SizeInBits.ToString());
                            m_listViewData.Items.Add(lviData);
                        }
                    }
                }
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            m_listViewData_KeyDown(m_listViewData, null);
        }

        private void m_listViewData_SelectedIndexChanged(object sender, EventArgs e)
        {
                ListViewItem lviData = null;
                if (m_listViewData.SelectedItems.Count > 0)
                    lviData = m_listViewData.SelectedItems[0];
                if (lviData != null)
                    btnRemove.Enabled = true;
                else
                    btnRemove.Enabled = false;
        }

        private void m_listViewData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete
                || e.KeyCode == Keys.Back)
            {
                ListViewItem lviData = null;
                if (m_listViewData.SelectedItems.Count > 0)
                    lviData = m_listViewData.SelectedItems[0];
                if (lviData != null)
                {
                    m_listViewData.Items.Remove(lviData);
                }
            }
        }
    }
}
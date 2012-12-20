using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CommonLib
{
    public partial class VirtualDataPanel : UserControl
    {
        GestDataVirtual m_GestVirtualData = null;
        GestData m_GestData = null;
        string m_strSymbolGroup = null;
        BTDoc m_Document = null;
        private int m_DisplayedDataCount;

        public VirtualDataPanel(BTDoc doc, GestDataVirtual GestVirtualData, GestData GestData, string strSymbolGroup)
        {
            Lang.LangSys.Initialize(this);
            m_Document = doc;
            InitializeComponent();
            m_GestVirtualData = GestVirtualData;
            m_GestData = GestData;
            m_strSymbolGroup = strSymbolGroup;
        }

        public void Initialize()
        {
            BaseGestGroup.Group group = m_GestVirtualData.GetGroupFromSymbol(m_strSymbolGroup);
            for (int i = 0; i < group.Items.Count; i++)
            {
                VirtualData vData = (VirtualData)group.Items[i];
                bool bDataUsedInFrame = false;
                for (int j = 0; j < m_Document.GestTrame.Count; j++)
                {
                    Trame tr = m_Document.GestTrame[j] as Trame;
                    if (tr.FrameDatas.Contains(vData.Symbol))
                        bDataUsedInFrame = true;
                }
                if (!vData.Symbol.Contains(Cste.STR_SUFFIX_CTRLDATA) && bDataUsedInFrame && !vData.IsConstant)
                {
                    m_DisplayedDataCount++;
                    AddDataToDataGrid(vData);
                    vData.VirtualDataValueChanged += new EventVirtualDataValueChange(VirtualDataValueChange);
                    vData.VirtualDataSaveStateChange += new EventVirtualDataSaveStateChange(VirtualDataSaveStateChange);
                }
            }
        }

        public bool HaveDisplayedData
        {
            get { return m_DisplayedDataCount > 0; }
        }

        protected void AddDataToDataGrid(VirtualData vData)
        {
            string[] TabValues = new string[6];
            TabValues[0] = vData.Symbol;
            TabValues[1] = vData.Description.Replace("\n", " ");
            TabValues[2] = vData.Minimum.ToString();
            TabValues[3] = vData.Maximum.ToString();
            TabValues[4] = vData.Value.ToString();
            TabValues[5] = vData.SaveInCliche.ToString();
            m_dataGrid.Rows.Add(TabValues);
        }

        protected void VirtualDataValueChange(VirtualData vData)
        {
            if (this.InvokeRequired)
            {
                EventVirtualDataValueChange d = new EventVirtualDataValueChange(VirtualDataValueChange);
                this.Invoke(d, vData);
            }
            else
            {
                for (int i = 0; i < m_dataGrid.Rows.Count; i++)
                {
                    string RowDataSymbol = (string)m_dataGrid.Rows[i].Cells[0].Value;
                    if (RowDataSymbol == vData.Symbol)
                    {
                        m_dataGrid.Rows[i].Cells["Value"].Value = vData.Value.ToString();
                        return;
                    }
                }
            }
        }

        void VirtualDataSaveStateChange(VirtualData vData)
        {
            for (int i = 0; i < m_dataGrid.Rows.Count; i++)
            {
                string RowDataSymbol = (string)m_dataGrid.Rows[i].Cells[0].Value;
                if (RowDataSymbol == vData.Symbol)
                {
                    m_dataGrid.Rows[i].Cells["m_colSaved"].Value = vData.SaveInCliche;
                    return;
                }
            }
        }

        private void m_dataGrid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == m_dataGrid.Columns["Value"].DisplayIndex)
            {
                string DataSymbol = (string)m_dataGrid.Rows[e.RowIndex].Cells[0].Value;
                VirtualData vData = (VirtualData)m_GestVirtualData.GetFromSymbol(DataSymbol);
                int NewValue = 0;
                bool bRet = int.TryParse((string)e.FormattedValue, out NewValue);
                if (!bRet)
                {
                    MessageBox.Show("Value must be integer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                    return;
                }
                if (!vData.TestValue(NewValue))
                {
                    MessageBox.Show("Value must be between minimum and maximum", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                    return;
                }
            }
            else if (e.ColumnIndex == m_dataGrid.Columns["m_colSaved"].DisplayIndex)
            {

            }
        }

        private void m_dataGrid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == m_dataGrid.Columns["Value"].DisplayIndex)
            {
                string DataSymbol = (string)m_dataGrid.Rows[e.RowIndex].Cells[0].Value;
                VirtualData vData = (VirtualData)m_GestVirtualData.GetFromSymbol(DataSymbol);
                if (vData.IsConstant)
                {
                    m_dataGrid.CancelEdit();
                }
            }
            else if (e.ColumnIndex == m_dataGrid.Columns["m_colSaved"].DisplayIndex)
            {
            }
        }

        private void m_dataGrid_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == m_dataGrid.Columns["Value"].DisplayIndex)
            {
                string DataSymbol = (string)m_dataGrid.Rows[e.RowIndex].Cells[0].Value;
                VirtualData vData = (VirtualData)m_GestVirtualData.GetFromSymbol(DataSymbol);
                int NewValue = 0;
                bool bRet = int.TryParse((string)m_dataGrid.Rows[e.RowIndex].Cells["Value"].Value, out NewValue);
                if (vData.Value != NewValue)
                    vData.SaveInCliche = true;
                    
                vData.Value = NewValue;
                return;
            }
            if (e.ColumnIndex == m_dataGrid.Columns["m_colSaved"].DisplayIndex)
            {
                string DataSymbol = (string)m_dataGrid.Rows[e.RowIndex].Cells[0].Value;
                string Value = m_dataGrid.Rows[e.RowIndex].Cells["m_colSaved"].Value.ToString();
                VirtualData vData = (VirtualData)m_GestVirtualData.GetFromSymbol(DataSymbol);
                vData.SaveInCliche = bool.Parse(Value);
                return;
            }
        }

        private void m_dataGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void btn_checkAll_Click(object sender, EventArgs e)
        {
            m_GestVirtualData.SetAllSaveInCliche(m_strSymbolGroup, true);
        }

        private void btn_uncheckAll_Click(object sender, EventArgs e)
        {
            m_GestVirtualData.SetAllSaveInCliche(m_strSymbolGroup, false);
        }
    }
}

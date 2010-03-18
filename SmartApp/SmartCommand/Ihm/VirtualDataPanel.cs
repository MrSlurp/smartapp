using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CommonLib;

namespace SmartApp
{
    public partial class VirtualDataPanel : UserControl
    {
        GestDataVirtual m_GestVirtualData = null;
        GestData m_GestData = null;
        string m_strSymbolGroup = null;
        public VirtualDataPanel(GestDataVirtual GestVirtualData, GestData GestData, string strSymbolGroup)
        {
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
                if (!vData.Symbol.Contains(Cste.STR_SUFFIX_CTRLDATA))
                {
                    AddDataToDataGrid(vData);
                    vData.VirtualDataValueChanged += new EventVirtualDataValueChange(VirtualDataValueChange);
                }
            }
        }

        protected void AddDataToDataGrid(VirtualData vData)
        {
            string[] TabValues = new string[5];
            TabValues[0] = vData.Symbol;
            TabValues[1] = vData.Description.Replace("\n", " ");
            TabValues[2] = vData.Minimum.ToString();
            TabValues[3] = vData.Maximum.ToString();
            TabValues[4] = vData.Value.ToString();
            m_dataGrid.Rows.Add(TabValues);
        }

        protected void VirtualDataValueChange(VirtualData vData)
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
        }

        private void m_dataGrid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            string DataSymbol = (string)m_dataGrid.Rows[e.RowIndex].Cells[0].Value;
            VirtualData vData = (VirtualData)m_GestVirtualData.GetFromSymbol(DataSymbol);
            if (vData.IsConstant)
            {
                m_dataGrid.CancelEdit();
            }
        }

        private void m_dataGrid_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            string DataSymbol = (string)m_dataGrid.Rows[e.RowIndex].Cells[0].Value;
            VirtualData vData = (VirtualData)m_GestVirtualData.GetFromSymbol(DataSymbol);
            int NewValue = 0;
            bool bRet = int.TryParse((string)m_dataGrid.Rows[e.RowIndex].Cells["Value"].Value, out NewValue);
            vData.Value = NewValue;
            vData.SaveInCliche = true;
        }
    }
}

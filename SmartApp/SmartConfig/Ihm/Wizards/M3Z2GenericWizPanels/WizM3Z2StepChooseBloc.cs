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
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SmartApp.Wizards;

namespace SmartApp.Ihm.Wizards
{
    public partial class WizM3Z2StepChooseBloc : UserControl, IWizConfigForm
    {
        WizardConfigData m_WizConfig;

        /// <summary>
        /// 
        /// </summary>
        public WizardConfigData WizConfig
        {
            set { m_WizConfig = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public WizM3Z2StepChooseBloc()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (Visible)
            {
                InitGrids();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitGrids()
        {
            m_dataGridIn.Rows.Clear();
            m_dataGridOut.Rows.Clear();

            BlocConfig[] BlocConfig = m_WizConfig.GetBlocListByType(BlocsType.IN);
            for (int NbIOBloc = 0; NbIOBloc < BlocConfig.Length; NbIOBloc++)
            {
                int index = m_dataGridIn.Rows.Add();
                m_dataGridIn.Rows[index].Cells[0].Value = BlocConfig[NbIOBloc].Name;
                m_dataGridIn.Rows[index].Cells[0].Tag = NbIOBloc;
                DataGridViewCheckBoxCell Cell = m_dataGridIn.Rows[index].Cells[1] as DataGridViewCheckBoxCell;
                Cell.Value = BlocConfig[NbIOBloc].IsUsed;
            }

            BlocConfig = m_WizConfig.GetBlocListByType(BlocsType.OUT);
            for (int NbIOBloc = 0; NbIOBloc < BlocConfig.Length; NbIOBloc++)
            {
                int index = m_dataGridOut.Rows.Add();
                m_dataGridOut.Rows[index].Cells[0].Value = BlocConfig[NbIOBloc].Name;
                m_dataGridOut.Rows[index].Cells[0].Tag = NbIOBloc;
                DataGridViewCheckBoxCell Cell = m_dataGridOut.Rows[index].Cells[1] as DataGridViewCheckBoxCell;
                Cell.Value = BlocConfig[NbIOBloc].IsUsed;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in m_dataGridIn.Rows)
            {
                row.Cells[1].Value = true;
            }
            foreach (DataGridViewRow row in m_dataGridOut.Rows)
            {
                row.Cells[1].Value = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNone_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in m_dataGridIn.Rows)
            {
                row.Cells[1].Value = false;
            }
            foreach (DataGridViewRow row in m_dataGridOut.Rows)
            {
                row.Cells[1].Value = false;
            }
        }

        public void HmiToData()
        {
            foreach (DataGridViewRow row in m_dataGridIn.Rows)
            {
                int idx = (int)row.Cells[0].Tag;
                m_WizConfig.SetBlocUsed(BlocsType.IN, idx, (bool)row.Cells[1].Value);
            }
            foreach (DataGridViewRow row in m_dataGridOut.Rows)
            {
                int idx = (int)row.Cells[0].Tag;
                m_WizConfig.SetBlocUsed(BlocsType.OUT, idx, (bool)row.Cells[1].Value);
            }
        }
    }
}

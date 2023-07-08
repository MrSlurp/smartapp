/*
    This file is part of SmartApp.
    Copyright (C) 2007-2016  Pascal Bigot

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
using CommonLib;

namespace SmartApp.Wizards
{
    public partial class SJWizSelectDataStep : UserControl, ISJWizForm
    {
        // on affichera que les données présentes dans les trames et qui ne sont pas des constantes
        public SJWizSelectDataStep()
        {
            InitializeComponent();
        }

        SJDataWizardManager m_Manager;
        public SJDataWizardManager SJManager
        { get { return m_Manager; } set { m_Manager = value; } }

        public void HmiToData()
        {

        }

        public void DataToHmi()
        {
            dataGrid.Rows.Clear();
            if (m_Manager.RequiredSelectionCount == 1)
                dataGrid.MultiSelect = false;
            else
                dataGrid.MultiSelect = true;

            this.lblRequiredSelectionCount.Text = string.Format(Program.LangSys.C("Please select {0} datas"), m_Manager.RequiredSelectionCount);

            GestData gestdt = m_Manager.Document.GestData as GestData;
            GestTrame gesttr = m_Manager.Document.GestTrame as GestTrame;
            for (int i = 0; i < gestdt.Count; i++)
            {
                Data dt = gestdt[i] as Data;
                bool bDataUsedInFrame = false;
                for (int j = 0; j < gesttr.Count; j++)
                {
                    Trame tr = gesttr[j] as Trame;
                    if (tr.FrameDatas.Contains(dt.Symbol))
                        bDataUsedInFrame = true;
                }
                if (bDataUsedInFrame 
                    && dt.IsConstant == false
                    && dt.SizeInBits >= m_Manager.MinSrcSize 
                    && dt.SizeInBits <= m_Manager.MaxSrcSize)
                {
                    AddDataToGrid(dt);
                }
            }
            // initialiser la grille en fonction du type d'operation
            // filtrer pour affichier que les données qui sont dans les trames
            // (parce que c'est ca qu'est compliqué pour les non informaticien)
            // 
        }

        protected void AddDataToGrid(Data dt)
        {
            int newRowIndex = this.dataGrid.Rows.Add(dt.Symbol, dt.SizeInBits.ToString());
            DataGridViewRow newRow = this.dataGrid.Rows[newRowIndex];
            newRow.Tag = dt;
        }

        private void dataGrid_SelectionChanged(object sender, EventArgs e)
        {
            // mettre a jour les lignes ok/pas ok
            if (dataGrid.SelectedRows.Count != 0 && dataGrid.SelectedRows[0].Tag != null)
            {
                DataGridViewRow firstRow = dataGrid.SelectedRows[0];
                Data firstDt = firstRow.Tag as Data;
                //firstDt.SizeInBits
                foreach (DataGridViewRow row in dataGrid.Rows)
                {
                    Data dt = row.Tag as Data;
                    if (dt.SizeInBits != firstDt.SizeInBits)
                        row.Visible = false;
                    else
                        row.Visible = true;
                }
            }
            else
            {
                foreach (DataGridViewRow row in dataGrid.Rows)
                {
                    row.Visible = true;
                }
            }
            // vérifier la compatibilité de la selection avec l'opération en cours
            // (continuité des données pour une jonction)
        }

        public bool AllowGoNext 
        { 
            get 
            {
                return dataGrid.SelectedRows.Count == m_Manager.RequiredSelectionCount; 
            } 
        }

        private void btnResetSelection_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGrid.Rows)
            {
                row.Selected = false;
            }
        }
    }
}

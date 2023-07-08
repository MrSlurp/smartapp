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
using SmartApp.Wizards;
using CommonLib;

namespace SmartApp.Ihm.Wizards
{
    public partial class WizM3Z2StepConfigIOSplit : UserControl, IWizConfigForm
    {
        private BlocsType m_TypeBlocConfigured = BlocsType.IN;

        private WizardConfigData m_WizConfig;

        private CComboData[] m_ListCboChoicesIN;
        private CComboData[] m_ListCboChoicesOUT;

        public WizardConfigData WizConfig
        {
            set { m_WizConfig = value; }
        }

        public WizM3Z2StepConfigIOSplit(BlocsType type)
        {
            InitializeComponent();
            m_TypeBlocConfigured = type;
            m_ListCboChoicesIN = new CComboData[4];
            m_ListCboChoicesIN[0] = new CComboData(Program.LangSys.C("None"), IOSplitFormat.SplitNone);
            m_ListCboChoicesIN[1] = new CComboData(Program.LangSys.C("Split to 16"), IOSplitFormat.SplitBy16);
            m_ListCboChoicesIN[2] = new CComboData(Program.LangSys.C("Split to 2"), IOSplitFormat.SplitBy2);
            m_ListCboChoicesIN[3] = new CComboData(Program.LangSys.C("Split to 4"), IOSplitFormat.SplitBy4);

            m_ListCboChoicesOUT = new CComboData[4];
            m_ListCboChoicesOUT[0] = new CComboData(Program.LangSys.C("None"), IOSplitFormat.SplitNone);
            m_ListCboChoicesOUT[1] = new CComboData(Program.LangSys.C("Split to 16"), IOSplitFormat.SplitBy16);
            m_ListCboChoicesOUT[2] = new CComboData(Program.LangSys.C("Split to 2"), IOSplitFormat.SplitBy2);
            m_ListCboChoicesOUT[3] = new CComboData(Program.LangSys.C("Split to 4"), IOSplitFormat.SplitBy4);
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (Visible)
            {
                if (!m_WizConfig.HaveIOTypeUsed(m_TypeBlocConfigured))
                {
                    if (m_TypeBlocConfigured == BlocsType.IN)
                        lblTitle.Text = Program.LangSys.C("None of input blocs were configured, you can go to the next step");
                    else
                        lblTitle.Text = Program.LangSys.C("None of ouput blocs were configured, you can go to the next step");

                    m_dataGrid.Enabled = false;
                }
                else
                {
                    if (m_TypeBlocConfigured == BlocsType.IN)
                        lblTitle.Text = Program.LangSys.C("Here you can configure the way you use your input blocs");
                    else
                        lblTitle.Text = Program.LangSys.C("Here you can configure the way you use your output blocs");

                    m_dataGrid.Enabled = true;
                }
                InitGrid();
            }
        }

        private void InitGrid()
        {
            m_dataGrid.Rows.Clear();

            BlocConfig[] BlocConfig = m_WizConfig.GetBlocListByType(m_TypeBlocConfigured);
            for (int NbIOBloc = 0; NbIOBloc < BlocConfig.Length; NbIOBloc++)
            {
                if (BlocConfig[NbIOBloc].IsUsed)
                {
                    IOConfig[] listIO = BlocConfig[NbIOBloc].ListIO;
                    for (int iNbIO = 0; iNbIO < listIO.Length; iNbIO++)
                    {
                        int index = m_dataGrid.Rows.Add();
                        m_dataGrid.Rows[index].Cells[0].Value = BlocConfig[NbIOBloc].Name;
                        m_dataGrid.Rows[index].Cells[0].Tag = NbIOBloc;
                        m_dataGrid.Rows[index].Cells[1].Value = listIO[iNbIO].Name;
                        m_dataGrid.Rows[index].Cells[1].Tag = iNbIO;
                        DataGridViewComboBoxCell Cell = m_dataGrid.Rows[index].Cells[2] as DataGridViewComboBoxCell;
                        Cell.DisplayMember = "DisplayedString";
                        Cell.ValueMember = "Object";
                        if (m_TypeBlocConfigured == BlocsType.IN)
                            Cell.DataSource = m_ListCboChoicesIN;
                        else
                            Cell.DataSource = m_ListCboChoicesOUT;

                        Cell.Value = listIO[iNbIO].SplitFormat;
                    }
                }
            }
        }

        public void HmiToData()
        {
            BlocConfig[] BlocConfig = m_WizConfig.GetBlocListByType(m_TypeBlocConfigured);
            for (int iRow = 0; iRow < m_dataGrid.Rows.Count; iRow++)
            {
                int BlocIdx = (int)m_dataGrid.Rows[iRow].Cells[0].Tag;
                IOConfig[] listIO = BlocConfig[BlocIdx].ListIO;
                int IOIdx = (int)m_dataGrid.Rows[iRow].Cells[1].Tag;
                DataGridViewComboBoxCell Cell = m_dataGrid.Rows[iRow].Cells[2] as DataGridViewComboBoxCell;
                if (listIO[IOIdx].SplitFormat != (IOSplitFormat)Cell.Value)
                {
                    listIO[IOIdx].SplitFormat = (IOSplitFormat)Cell.Value;
                    listIO[IOIdx].ListSymbol.Clear();
                }
                
            }
        }

        private void m_dataGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            Console.WriteLine(e.Exception.Message);
        }

        public string TypeSplitLabel1
        {
            set { lblTypeSplit1.Text = value; }
        }
        public string TypeSplitLabel2
        {
            set { lblTypeSplit2.Text = value; }
        }
        public string TypeSplitLabel3
        {
            set { lblTypeSplit3.Text = value; }
        }

        public Image TypeSplitImage1
        {
            set { picTypeSplit1.Image = value; }
        }
        public Image TypeSplitImage2
        {
            set { picTypeSplit2.Image = value; }
        }
        public Image TypeSplitImage3
        {
            set { picTypeSplit3.Image = value; }
        }

    }
}

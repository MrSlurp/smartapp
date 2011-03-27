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
    public partial class WizM3Z2StepConfigIOName : UserControl, IWizConfigForm
    {
        private BlocsType m_TypeBlocConfigured = BlocsType.IN;

        private WizardConfigData m_WizConfig;

        public WizardConfigData WizConfig
        {
            set { m_WizConfig = value; }
        }

        public WizM3Z2StepConfigIOName(BlocsType type)
        {
            m_TypeBlocConfigured = type;
            InitializeComponent();
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
                        lblTitle.Text = Program.LangSys.C("Here you can configure symbols for your inputs blocs outputs");
                    else
                        lblTitle.Text = Program.LangSys.C("Here you can configure symbols for your output blocs inputs");

                    m_dataGrid.Enabled = true;
                }
                InitGrid();
            }
        }

        private void InitGrid()
        {
            m_dataGrid.Rows.Clear();
            string FormatString = "{0}_{1}{2}";
            BlocConfig[] BlocConfig = m_WizConfig.GetBlocListByType(m_TypeBlocConfigured);
            for (int NbIOBloc = 0; NbIOBloc < BlocConfig.Length; NbIOBloc++)
            {
                if (BlocConfig[NbIOBloc].IsUsed)
                {
                    IOConfig[] listIO = BlocConfig[NbIOBloc].ListIO;
                    for (int iNbIO = 0; iNbIO < listIO.Length; iNbIO++)
                    {
                        int nbSubData = 1;
                        string dataSubName = string.Empty;
                        string dataSubPartFormat = "NA";
                        string slBlocName = BlocConfig[NbIOBloc].Name.Replace(' ', '_').ToUpper();
                        switch (listIO[iNbIO].SplitFormat)
                        {
                            case IOSplitFormat.SplitBy16:
                                nbSubData = 16;
                                dataSubName = "_B{0}";
                                dataSubPartFormat = Program.LangSys.C("Bit {0}");
                                break;
                            case IOSplitFormat.SplitBy4:
                                nbSubData = 4;
                                dataSubName = "_S{0}";
                                dataSubPartFormat = Program.LangSys.C("Sub part {0}");
                                break;
                            case IOSplitFormat.SplitBy2:
                                nbSubData = 2;
                                dataSubName = "_S{0}";
                                dataSubPartFormat = Program.LangSys.C("Sub part {0}");
                                break;
                            default:
                            case IOSplitFormat.SplitNone:
                                break;
                        }

                        for (int i = 1; i<= nbSubData; i++)
                        {
                            string subPartLabel = string.Format(dataSubPartFormat, i);
                            string DataDefaultSymbol = string.Format(FormatString, slBlocName, listIO[iNbIO].ShortName, string.Format(dataSubName, i)); 
                            int index = m_dataGrid.Rows.Add();
                            m_dataGrid.Rows[index].Cells[0].Value = BlocConfig[NbIOBloc].Name;
                            m_dataGrid.Rows[index].Cells[0].Tag = NbIOBloc;
                            m_dataGrid.Rows[index].Cells[1].Value = listIO[iNbIO].Name;
                            m_dataGrid.Rows[index].Cells[1].Tag = iNbIO;
                            m_dataGrid.Rows[index].Cells[2].Value = subPartLabel;
                            m_dataGrid.Rows[index].Cells[2].Tag = i;
                            m_dataGrid.Rows[index].Cells[3].Value = DataDefaultSymbol;
                            if (i <= listIO[iNbIO].ListSymbol.Count && DataDefaultSymbol != listIO[iNbIO].ListSymbol[i-1])
                                m_dataGrid.Rows[index].Cells[4].Value = listIO[iNbIO].ListSymbol[i-1];
                        }
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
                int subIdx = (int)m_dataGrid.Rows[iRow].Cells[2].Tag;
                if (subIdx >= listIO[IOIdx].ListSymbol.Count)
                {
                    if (!string.IsNullOrEmpty((string)m_dataGrid.Rows[iRow].Cells[4].Value))
                        listIO[IOIdx].ListSymbol.Add((string)m_dataGrid.Rows[iRow].Cells[4].Value);
                    else
                        listIO[IOIdx].ListSymbol.Add((string)m_dataGrid.Rows[iRow].Cells[3].Value);
                }
                else
                {
                    if (!string.IsNullOrEmpty((string)m_dataGrid.Rows[iRow].Cells[4].Value))
                        listIO[IOIdx].ListSymbol[subIdx-1] = (string)m_dataGrid.Rows[iRow].Cells[3].Value;
                    else
                        listIO[IOIdx].ListSymbol[subIdx-1] = (string)m_dataGrid.Rows[iRow].Cells[4].Value;
                 }
            }
        }

        private void m_dataGrid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == colUserSymbol.Index)
            {
                string cellValue = (string)e.FormattedValue;
                string AuthorizedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_0123456789-";
                foreach (char c in cellValue)
                {
                    string strCurChar = c.ToString();
                    if (!AuthorizedChars.Contains(strCurChar))
                    {
                        string Message = Program.LangSys.C("Characters {0} can't be used in symbol");
                        Message = string.Format(Message, strCurChar);
                        MessageBox.Show(Message, Program.LangSys.C("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Cancel = true;
                        break;
                    }
                }
            }
        }

        private void m_dataGrid_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == colUserSymbol.Index)
            {
                string cellValue = (string)m_dataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                if (!string.IsNullOrEmpty(cellValue))
                {
                    string userSymbol = cellValue.Replace(' ', '_').ToUpper();
                    m_dataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = userSymbol;
                }
            }
        }
    }
}

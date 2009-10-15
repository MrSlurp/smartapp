using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CommonLib;

namespace CtrlGraph
{
    public partial class CurveParam : UserControl
    {
        private BTDoc m_Document = null;

        int m_iDataIndex = 0;
        public CurveParam()
        {
            InitializeComponent();
        }

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

        public int DataIndex
        {
            get
            {
                return m_iDataIndex;
            }
            set
            {
                m_iDataIndex = value;
                lblSymb1.Text = string.Format("Data {0} Symbol", m_iDataIndex + 1);
                lblAlias1.Text = string.Format("Data {0} Alias", m_iDataIndex + 1);
            }
        }

        public string DataSymbol
        {
            get
            {
                return edtSymb1.Text;
            }
            set
            {
                edtSymb1.Text = value;
            }
        }

        public string Alias
        {
            get
            {
                return edtAlias1.Text;
            }
            set
            {
                edtAlias1.Text = value;
            }
        }

        public Color CurveColor
        {
            get
            {
                return edtColor1.BackColor;
            }
            set
            {
                edtColor1.BackColor = value;
            }
        }

        private void btnPickData1_Click(object sender, EventArgs e)
        {
            PickDataForm PickData = new PickDataForm();
            PickData.Document = this.Doc;
            if (PickData.ShowDialog() == DialogResult.OK)
            {
                if (PickData.SelectedData != null)
                    edtSymb1.Text = PickData.SelectedData.Symbol;
                else
                    edtSymb1.Text = string.Empty;
            }
        }

        private void bntPickColor1_Click(object sender, EventArgs e)
        {
            ColorDialog clrDlg = new ColorDialog();
            DialogResult DlgRes = clrDlg.ShowDialog();
            if (DlgRes == DialogResult.OK)
            {
                edtColor1.BackColor = clrDlg.Color;
            }
        }
    }
}

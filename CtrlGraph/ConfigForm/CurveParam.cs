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

namespace CtrlGraph
{
    public partial class CurveParam : UserControl
    {
        private BTDoc m_Document = null;

        int m_iDataIndex = 0;
        public CurveParam()
        {
            DllEntryClass.LangSys.Initialize(this);
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
                lblSymb1.Text = string.Format(DllEntryClass.LangSys.C("Data {0} Symbol"), m_iDataIndex + 1);
                lblAlias1.Text = string.Format(DllEntryClass.LangSys.C("Data {0} Alias"), m_iDataIndex + 1);
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

        public int Divisor
        {
            get { return (int)edtDivisor.Value; }
            set { edtDivisor.Value = value; }
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

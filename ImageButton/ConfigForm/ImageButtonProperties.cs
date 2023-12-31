﻿/*
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
using System.IO;
using System.Windows.Forms;
using CommonLib;

namespace ImageButton
{
    internal partial class ImageButtonProperties : BaseControlPropertiesPanel, ISpecificPanel
    {
        private CComboData[] m_ListCboStyles;


        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        public ImageButtonProperties()
        {
            DllEntryClass.LangSys.Initialize(this);
            InitializeComponent();

            m_ListCboStyles = new CComboData[3];
            m_ListCboStyles[0] = new CComboData(DllEntryClass.LangSys.C("Standard"), 0);
            m_ListCboStyles[1] = new CComboData(DllEntryClass.LangSys.C("Flat"), 1);
            m_ListCboStyles[2] = new CComboData(DllEntryClass.LangSys.C("Borderless flat"), 2);

            cboStyle.ValueMember = "Object";
            cboStyle.DisplayMember = "DisplayedString";
            cboStyle.DataSource = m_ListCboStyles;
            cboStyle.SelectedIndex = 0;

        }

        #region validation des données
        /// <summary>
        /// Accesseur de validité des propriétés
        /// renvoie true si les propriété sont valides, sinon false
        /// </summary>
        public override bool IsObjectPropertiesValid
        {
            get
            {
                if (this.ConfiguredItem == null)
                    return true;

                bool bRet = true;
                Data dt = null;
                if (!string.IsNullOrEmpty(this.edtInputData.Text))
                {
                    dt = (Data)this.Document.GestData.GetFromSymbol(this.edtInputData.Text);
                    if (dt == null)
                    {
                        bRet = false;
                    }
                }
                return bRet;
            }
        }

        /// <summary>
        /// validitation des propriétés
        /// </summary>
        /// <returns>true si les propriété sont valides, sinon false</returns>
        public override bool ValidateProperties()
        {

            if (this.ConfiguredItem == null)
                return true;

            bool bRet = true;
            string strMessage = "";

            if (!string.IsNullOrEmpty(this.edtInputData.Text))
            {
                Data dt = null;
                dt = (Data)this.Document.GestData.GetFromSymbol(this.edtInputData.Text);
                if (dt == null)
                {
                    bRet = false;
                    strMessage = string.Format(DllEntryClass.LangSys.C("Associate Input data {0} is not valid"), this.edtInputData.Text);
                }
            }
            if (!bRet)
            {
                MessageBox.Show(strMessage, DllEntryClass.LangSys.C("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return bRet;
            }
            return bRet;

        }

        public void ObjectToPanel()
        {
            DllImageButtonProp SpecProp = (DllImageButtonProp)m_Control.SpecificProp;
            m_txtBoxImg1.Text = SpecProp.ReleasedImage;
            m_txtBoxImg2.Text = SpecProp.PressedImage;
            edtInputData.Text = SpecProp.InputData;
            chkBistable.Checked = SpecProp.IsBistable;
            if (SpecProp.Style == FlatStyle.Flat && SpecProp.BorderSize == 1)
                cboStyle.SelectedValue = 1;
            else if (SpecProp.Style == FlatStyle.Standard)
                cboStyle.SelectedValue = 0;
            else
                cboStyle.SelectedValue = 2;

        }
        public void PanelToObject()
        {
            bool bDataPropChange = false;
            DllImageButtonProp SpecProps = (DllImageButtonProp)m_Control.SpecificProp;
            if (m_txtBoxImg1.Text != SpecProps.ReleasedImage)
                bDataPropChange = true;

            if (m_txtBoxImg2.Text != SpecProps.PressedImage)
                bDataPropChange = true;

            if (chkBistable.Checked != SpecProps.IsBistable)
                bDataPropChange = true;

            if (this.edtInputData.Text != SpecProps.InputData)
                bDataPropChange = true;

            int curStyle = (int)cboStyle.SelectedValue;
            FlatStyle finalStyle = SpecProps.Style;
            int finalBorderSize = SpecProps.BorderSize;
            if (curStyle == 0 && SpecProps.Style != FlatStyle.Standard)
            {
                bDataPropChange = true;
                finalStyle = FlatStyle.Standard;
                finalBorderSize = 1;
            }
            else if (curStyle == 1 && (SpecProps.Style != FlatStyle.Flat || SpecProps.BorderSize != 1))
            {
                bDataPropChange = true;
                finalStyle = FlatStyle.Flat;
                finalBorderSize = 1;
            }
            else if (curStyle == 2 && (SpecProps.Style != FlatStyle.Flat || SpecProps.BorderSize != 0))
            {
                bDataPropChange = true;
                finalStyle = FlatStyle.Flat;
                finalBorderSize = 0;
            }

            if (bDataPropChange)
            {
                ((DllImageButtonProp)m_Control.SpecificProp).ReleasedImage = m_txtBoxImg1.Text;
                ((DllImageButtonProp)m_Control.SpecificProp).PressedImage = m_txtBoxImg2.Text;
                ((DllImageButtonProp)m_Control.SpecificProp).IsBistable = chkBistable.Checked;
                ((DllImageButtonProp)m_Control.SpecificProp).Style = finalStyle;
                ((DllImageButtonProp)m_Control.SpecificProp).BorderSize = finalBorderSize;
                ((DllImageButtonProp)m_Control.SpecificProp).InputData = this.edtInputData.Text;
                Document.Modified = true;
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="bf"></param>
        private void m_btnImg1_Click(object sender, EventArgs e, BrowseFileBtn.BrowseFrom bf)
        {
            DialogResult dlgRes = CentralizedFileDlg.ShowImageFileDilaog(bf);
            if (dlgRes == DialogResult.OK)
            {

                m_txtBoxImg1.Text = PathTranslator.LinuxVsWindowsPathStore(
                                    m_Document.PathTr.AbsolutePathToRelative(CentralizedFileDlg.ImgFileName));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="bf"></param>
        private void m_btnImg2_Click(object sender, EventArgs e, BrowseFileBtn.BrowseFrom bf)
        {
            DialogResult dlgRes = CentralizedFileDlg.ShowImageFileDilaog(bf);
            if (dlgRes == DialogResult.OK)
            {
                m_txtBoxImg2.Text = PathTranslator.LinuxVsWindowsPathStore(
                                    m_Document.PathTr.AbsolutePathToRelative(CentralizedFileDlg.ImgFileName));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPickInput_Click(object sender, EventArgs e)
        {
            PickDataForm PickData = new PickDataForm();
            PickData.Document = this.Document;
            if (PickData.ShowDialog() == DialogResult.OK)
            {
                if (PickData.SelectedData != null)
                    edtInputData.Text = PickData.SelectedData.Symbol;
                else
                    edtInputData.Text = string.Empty;
            }
        }

        private void m_txtBoxImg1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
                m_txtBoxImg1.Text = string.Empty;
        }

        private void m_txtBoxImg2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
                m_txtBoxImg2.Text = string.Empty;
        }
    }
}

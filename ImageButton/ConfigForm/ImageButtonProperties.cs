﻿using System;
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
    internal partial class ImageButtonProperties : UserControl, ISpecificPanel
    {
        // controle dont on édite les propriété
        BTControl m_Control = null;
        // document courant
        private BTDoc m_Document = null;
        private CComboData[] m_ListCboStyles;

        #region events
        public event ControlPropertiesChange ControlPropertiesChanged;
        #endregion

        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        public ImageButtonProperties()
        {
            DllEntryClass.LangSys.Initialize(this);
            InitializeComponent();

            if (!string.IsNullOrEmpty(PathTranslator.BTDocPath))
                m_openFileDialog.InitialDirectory = PathTranslator.BTDocPath;
            else
                m_openFileDialog.InitialDirectory = Application.StartupPath;

            m_openFileDialog.Filter = DllEntryClass.LangSys.C("Image Files (jpeg, gif, bmp, png)|*.jpg;*.jpeg;*.gif;*.bmp;*.png|JPEG Files(*.jpg;*.jpeg)|*.jpg;*.jpeg|GIF Files(*.gif)|*.gif|BMP Files(*.bmp)|*.bmp|PNG Files(*.png)|*.png");
            m_ListCboStyles = new CComboData[3];
            m_ListCboStyles[0] = new CComboData(DllEntryClass.LangSys.C("Standard"), 0);
            m_ListCboStyles[1] = new CComboData(DllEntryClass.LangSys.C("Flat"), 1);
            m_ListCboStyles[2] = new CComboData(DllEntryClass.LangSys.C("Borderless flat"), 2);

            cboStyle.ValueMember = "Object";
            cboStyle.DisplayMember = "DisplayedString";
            cboStyle.DataSource = m_ListCboStyles;
            cboStyle.SelectedIndex = 0;

        }

        /// <summary>
        /// Accesseur du control
        /// </summary>
        public BTControl BTControl
        {
            get
            {
                return m_Control;
            }
            set
            {
                if (value != null && value.SpecificProp.GetType() == typeof(DllImageButtonProp))
                    m_Control = value;
                else
                    m_Control = null;
                if (m_Control != null)
                {
                    this.Enabled = true;
                    DllImageButtonProp SpecProp = (DllImageButtonProp)m_Control.SpecificProp;
                    m_txtBoxImg1.Text = SpecProp.ReleasedImage;
                    m_txtBoxImg2.Text = SpecProp.PressedImage;
                    chkBistable.Checked = SpecProp.IsBistable;
                    if (SpecProp.Style == FlatStyle.Flat && SpecProp.BorderSize == 1)
                        cboStyle.SelectedValue = 1;
                    else if (SpecProp.Style == FlatStyle.Standard)
                        cboStyle.SelectedValue = 0;
                    else
                        cboStyle.SelectedValue = 2;

                }
                else
                {
                    this.Enabled = false;
                    m_txtBoxImg1.Text = "";
                    m_txtBoxImg2.Text = "";
                    chkBistable.Checked = false;
                    cboStyle.SelectedValue = 0;
                }
            }
        }

        /// <summary>
        /// Accesseur du document
        /// </summary>
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

        #region validation des données
        /// <summary>
        /// Accesseur de validité des propriétés
        /// renvoie true si les propriété sont valides, sinon false
        /// </summary>
        public bool IsDataValuesValid
        {
            get
            {
                if (this.BTControl == null)
                    return true;

                return true;
            }
        }

        /// <summary>
        /// validitation des propriétés
        /// </summary>
        /// <returns>true si les propriété sont valides, sinon false</returns>
        public bool ValidateValues()
        {
            bool bDataPropChange = false;
            DllImageButtonProp SpecProps = (DllImageButtonProp)m_Control.SpecificProp;
            if (m_txtBoxImg1.Text != SpecProps.ReleasedImage)
                bDataPropChange = true;

            if (m_txtBoxImg2.Text != SpecProps.PressedImage)
                bDataPropChange = true;

            if (chkBistable.Checked != SpecProps.IsBistable)
                bDataPropChange = true;

            int curStyle = (int)cboStyle.SelectedValue;
            FlatStyle finalStyle = FlatStyle.Standard;
            int finalBorderSize = 1;
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
                Doc.Modified = true;
                m_Control.IControl.Refresh();
            }
            if (bDataPropChange && ControlPropertiesChanged != null)
                ControlPropertiesChanged(m_Control);
            return true;
        }
        #endregion

        private void m_btnImg1_Click(object sender, EventArgs e)
        {
            DialogResult dlgRes = m_openFileDialog.ShowDialog();
            if (dlgRes == DialogResult.OK)
            {

                m_txtBoxImg1.Text = PathTranslator.LinuxVsWindowsPathStore(
                                    PathTranslator.AbsolutePathToRelative(m_openFileDialog.FileName));
            }
        }

        private void m_btnImg2_Click(object sender, EventArgs e)
        {
            DialogResult dlgRes = m_openFileDialog.ShowDialog();
            if (dlgRes == DialogResult.OK)
            {
                m_txtBoxImg2.Text = PathTranslator.LinuxVsWindowsPathStore(
                                    PathTranslator.AbsolutePathToRelative(m_openFileDialog.FileName));
            }
        }

    }
}

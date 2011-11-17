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
    internal partial class ImageButtonProperties : UserControl, ISpecificPanel
    {
        // controle dont on édite les propriété
        BTControl m_Control = null;
        // document courant
        private BTDoc m_Document = null;

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
                    m_txtBoxImg1.Text = ((DllImageButtonProp)m_Control.SpecificProp).ReleasedImage;
                    m_txtBoxImg2.Text = ((DllImageButtonProp)m_Control.SpecificProp).PressedImage;
                    chkBistable.Checked = ((DllImageButtonProp)m_Control.SpecificProp).IsBistable;
                }
                else
                {
                    this.Enabled = false;
                    m_txtBoxImg1.Text = "";
                    m_txtBoxImg2.Text = "";
                    chkBistable.Checked = false;
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
            if (m_txtBoxImg1.Text != ((DllImageButtonProp)m_Control.SpecificProp).ReleasedImage)
                bDataPropChange = true;

            if (m_txtBoxImg2.Text != ((DllImageButtonProp)m_Control.SpecificProp).PressedImage)
                bDataPropChange = true;

            if (chkBistable.Checked != ((DllImageButtonProp)m_Control.SpecificProp).IsBistable)
                bDataPropChange = true;

            if (bDataPropChange)
            {
                ((DllImageButtonProp)m_Control.SpecificProp).ReleasedImage = m_txtBoxImg1.Text;
                ((DllImageButtonProp)m_Control.SpecificProp).PressedImage = m_txtBoxImg2.Text;
                ((DllImageButtonProp)m_Control.SpecificProp).IsBistable = chkBistable.Checked;
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

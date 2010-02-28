using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.IO;
using System.Windows.Forms;
using CommonLib;

namespace CtrlTwoBitmap
{
    public partial class TwoBitmapProperties : UserControl , ISpecificPanel
    {
        BTControl m_Control = null;
        private BTDoc m_Document = null;

        #region events
        public event ControlPropertiesChange ControlPropertiesChanged;
        #endregion

        public TwoBitmapProperties()
        {
            DllEntryClass.LangSys.Initialize(this);
            InitializeComponent();
        }

        public BTControl BTControl
        {
            get
            {
                return m_Control;
            }
            set
            {
                if (value != null && value.SpecificProp.GetType() == typeof(TwoBitmapProp))
                    m_Control = value;
                else
                    m_Control = null;
                if (m_Control != null)
                {
                    this.Enabled = true;
                    m_txtBoxImg1.Text = ((TwoBitmapProp)m_Control.SpecificProp).NomFichierInactif;
                    m_txtBoxImg2.Text = ((TwoBitmapProp)m_Control.SpecificProp).NomFichierActif;
                }
                else
                {
                    this.Enabled = false;
                    m_txtBoxImg1.Text = "";
                    m_txtBoxImg2.Text = "";
                }
            }
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

        #region validation des données
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public bool IsDataValuesValid
        {
            get
            {
                if (this.BTControl == null)
                    return true;

                return true;
            }
        }


        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public bool ValidateValues()
        {
            if (this.BTControl == null)
                return true;

            bool bDataPropChange = false;
            if (m_txtBoxImg1.Text != ((TwoBitmapProp)m_Control.SpecificProp).NomFichierInactif)
                bDataPropChange = true;

            if (m_txtBoxImg2.Text != ((TwoBitmapProp)m_Control.SpecificProp).NomFichierActif)
                bDataPropChange = true;

            if (bDataPropChange)
            {
                ((TwoBitmapProp)m_Control.SpecificProp).NomFichierInactif = m_txtBoxImg1.Text;
                ((TwoBitmapProp)m_Control.SpecificProp).NomFichierActif = m_txtBoxImg2.Text;
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
            m_openFileDialog.Filter = DllEntryClass.LangSys.C("Image Files (jpeg, gif, bmp, png)|*.jpg;*.jpeg;*.gif;*.bmp;*.png|JPEG Files(*.jpg;*.jpeg)|*.jpg;*.jpeg|GIF Files(*.gif)|*.gif|BMP Files(*.bmp)|*.bmp|PNG Files(*.png)|*.png");
            if (!string.IsNullOrEmpty(PathTranslator.BTDocPath))
                m_openFileDialog.InitialDirectory = PathTranslator.BTDocPath;
            else
                m_openFileDialog.InitialDirectory = Application.StartupPath;

            DialogResult dlgRes = m_openFileDialog.ShowDialog();
            if (dlgRes == DialogResult.OK)
            {

                m_txtBoxImg1.Text = PathTranslator.LinuxVsWindowsPathStore(
                                    PathTranslator.AbsolutePathToRelative(m_openFileDialog.FileName));
            }

        }

        private void m_btnImg2_Click(object sender, EventArgs e)
        {
            m_openFileDialog.Filter = DllEntryClass.LangSys.C("Image Files (jpeg, gif, bmp, png)|*.jpg;*.jpeg;*.gif;*.bmp;*.png|JPEG Files(*.jpg;*.jpeg)|*.jpg;*.jpeg|GIF Files(*.gif)|*.gif|BMP Files(*.bmp)|*.bmp|PNG Files(*.png)|*.png");
            if (!string.IsNullOrEmpty(PathTranslator.BTDocPath))
                m_openFileDialog.InitialDirectory = PathTranslator.BTDocPath;
            else
                m_openFileDialog.InitialDirectory = Application.StartupPath;
            DialogResult dlgRes = m_openFileDialog.ShowDialog();
            if (dlgRes == DialogResult.OK)
            {
                m_txtBoxImg2.Text = PathTranslator.LinuxVsWindowsPathStore(
                                    PathTranslator.AbsolutePathToRelative(m_openFileDialog.FileName));
            }
        }
    }
}

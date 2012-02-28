using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.IO;
using System.Windows.Forms;
using CommonLib;

namespace FourBitmap
{
    public partial class FourBitmapProperties : UserControl, ISpecificPanel
    {
        BTControl m_Control = null;
        private BTDoc m_Document = null;

        #region events
        public event ControlPropertiesChange ControlPropertiesChanged;
        #endregion

        public FourBitmapProperties()
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
                if (value != null && value.SpecificProp.GetType() == typeof(DllFourBitmapProp))
                    m_Control = value;
                else
                    m_Control = null;
                if (m_Control != null)
                {
                    this.Enabled = true;
                    m_txtBoxImg0.Text = ((DllFourBitmapProp)m_Control.SpecificProp).NomFichier0;
                    m_txtBoxImg1.Text = ((DllFourBitmapProp)m_Control.SpecificProp).NomFichier1;
                    m_txtBoxImg2.Text = ((DllFourBitmapProp)m_Control.SpecificProp).NomFichier2;
                    m_txtBoxImg3.Text = ((DllFourBitmapProp)m_Control.SpecificProp).NomFichier3;
                }
                else
                {
                    this.Enabled = false;
                    m_txtBoxImg0.Text = "";
                    m_txtBoxImg1.Text = "";
                    m_txtBoxImg2.Text = "";
                    m_txtBoxImg3.Text = "";
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

            if (m_txtBoxImg0.Text != ((DllFourBitmapProp)m_Control.SpecificProp).NomFichier0)
                bDataPropChange = true;

            if (m_txtBoxImg1.Text != ((DllFourBitmapProp)m_Control.SpecificProp).NomFichier1)
                bDataPropChange = true;

            if (m_txtBoxImg2.Text != ((DllFourBitmapProp)m_Control.SpecificProp).NomFichier2)
                bDataPropChange = true;

            if (m_txtBoxImg3.Text != ((DllFourBitmapProp)m_Control.SpecificProp).NomFichier3)
                bDataPropChange = true;

            if (bDataPropChange)
            {
                ((DllFourBitmapProp)m_Control.SpecificProp).NomFichier0 = m_txtBoxImg0.Text;
                ((DllFourBitmapProp)m_Control.SpecificProp).NomFichier1 = m_txtBoxImg1.Text;
                ((DllFourBitmapProp)m_Control.SpecificProp).NomFichier2 = m_txtBoxImg2.Text;
                ((DllFourBitmapProp)m_Control.SpecificProp).NomFichier3 = m_txtBoxImg3.Text;
                Doc.Modified = true;
                m_Control.IControl.Refresh();
            }
            if (bDataPropChange && ControlPropertiesChanged != null)
                ControlPropertiesChanged(m_Control);
            return true;
        }
        #endregion

        private void m_btnImg0_Click(object sender, EventArgs e)
        {
            DialogResult dlgRes = CentralizedFileDlg.ShowImageFileDilaog();
            if (dlgRes == DialogResult.OK)
            {
                m_txtBoxImg0.Text = PathTranslator.LinuxVsWindowsPathStore(
                                    PathTranslator.AbsolutePathToRelative(CentralizedFileDlg.ImgFileName));
            }
        }

        private void m_btnImg1_Click(object sender, EventArgs e)
        {
            DialogResult dlgRes = CentralizedFileDlg.ShowImageFileDilaog();
            if (dlgRes == DialogResult.OK)
            {
                m_txtBoxImg1.Text = PathTranslator.LinuxVsWindowsPathStore(
                                    PathTranslator.AbsolutePathToRelative(CentralizedFileDlg.ImgFileName));
            }
        }

        private void m_btnImg2_Click(object sender, EventArgs e)
        {
            DialogResult dlgRes = CentralizedFileDlg.ShowImageFileDilaog();
            if (dlgRes == DialogResult.OK)
            {
                m_txtBoxImg2.Text = PathTranslator.LinuxVsWindowsPathStore(
                                    PathTranslator.AbsolutePathToRelative(CentralizedFileDlg.ImgFileName));
            }
        }

        private void m_btnImg3_Click(object sender, EventArgs e)
        {
            DialogResult dlgRes = CentralizedFileDlg.ShowImageFileDilaog();
            if (dlgRes == DialogResult.OK)
            {
                m_txtBoxImg3.Text = PathTranslator.LinuxVsWindowsPathStore(
                                    PathTranslator.AbsolutePathToRelative(CentralizedFileDlg.ImgFileName));
            }
        }
    }
}

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
    public partial class FourBitmapProperties : BaseControlPropertiesPanel, ISpecificPanel
    {
        public FourBitmapProperties()
        {
            DllEntryClass.LangSys.Initialize(this);
            InitializeComponent();
        }

        #region validation des données
        public void ObjectToPanel()
        {
            m_txtBoxImg0.Text = ((DllFourBitmapProp)m_Control.SpecificProp).NomFichier0;
            m_txtBoxImg1.Text = ((DllFourBitmapProp)m_Control.SpecificProp).NomFichier1;
            m_txtBoxImg2.Text = ((DllFourBitmapProp)m_Control.SpecificProp).NomFichier2;
            m_txtBoxImg3.Text = ((DllFourBitmapProp)m_Control.SpecificProp).NomFichier3;
        }

        public void PanelToObject()
        {
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
                Document.Modified = true;
            }
        }
        #endregion

        private void m_btnImg0_Click(object sender, EventArgs e, BrowseFileBtn.BrowseFrom bf)
        {
            DialogResult dlgRes = CentralizedFileDlg.ShowImageFileDilaog(bf);
            if (dlgRes == DialogResult.OK)
            {
                m_txtBoxImg0.Text = PathTranslator.LinuxVsWindowsPathStore(
                                    Document.PathTr.AbsolutePathToRelative(CentralizedFileDlg.ImgFileName));
            }
        }

        private void m_btnImg1_Click(object sender, EventArgs e, BrowseFileBtn.BrowseFrom bf)
        {
            DialogResult dlgRes = CentralizedFileDlg.ShowImageFileDilaog(bf);
            if (dlgRes == DialogResult.OK)
            {
                m_txtBoxImg1.Text = PathTranslator.LinuxVsWindowsPathStore(
                                    Document.PathTr.AbsolutePathToRelative(CentralizedFileDlg.ImgFileName));
            }
        }

        private void m_btnImg2_Click(object sender, EventArgs e, BrowseFileBtn.BrowseFrom bf)
        {
            DialogResult dlgRes = CentralizedFileDlg.ShowImageFileDilaog(bf);
            if (dlgRes == DialogResult.OK)
            {
                m_txtBoxImg2.Text = PathTranslator.LinuxVsWindowsPathStore(
                                    Document.PathTr.AbsolutePathToRelative(CentralizedFileDlg.ImgFileName));
            }
        }

        private void m_btnImg3_Click(object sender, EventArgs e, BrowseFileBtn.BrowseFrom bf)
        {
            DialogResult dlgRes = CentralizedFileDlg.ShowImageFileDilaog(bf);
            if (dlgRes == DialogResult.OK)
            {
                m_txtBoxImg3.Text = PathTranslator.LinuxVsWindowsPathStore(
                                    Document.PathTr.AbsolutePathToRelative(CentralizedFileDlg.ImgFileName));
            }
        }
    }
}

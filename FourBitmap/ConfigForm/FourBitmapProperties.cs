/*
    This file is part of SmartApp.

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

        private void m_txtBoxImgn_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox senderTB = sender as TextBox;
            if (senderTB != null)
                senderTB.Text = string.Empty;
        }
    }
}

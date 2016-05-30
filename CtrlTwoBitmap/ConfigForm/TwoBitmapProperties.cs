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

namespace CtrlTwoBitmap
{
    public partial class TwoBitmapProperties : BaseControlPropertiesPanel, ISpecificPanel
    {
        public TwoBitmapProperties()
        {
            DllEntryClass.LangSys.Initialize(this);
            InitializeComponent();
        }

        #region validation des données
        public void PanelToObject()
        {
            bool bDataPropChange = false;
            if (m_txtBoxImg1.Text != ((TwoBitmapProp)m_Control.SpecificProp).NomFichierInactif)
                bDataPropChange = true;

            if (m_txtBoxImg2.Text != ((TwoBitmapProp)m_Control.SpecificProp).NomFichierActif)
                bDataPropChange = true;

            if (bDataPropChange)
            {
                ((TwoBitmapProp)m_Control.SpecificProp).NomFichierInactif = m_txtBoxImg1.Text;
                ((TwoBitmapProp)m_Control.SpecificProp).NomFichierActif = m_txtBoxImg2.Text;
                Document.Modified = true;
            }
        }

        public void ObjectToPanel()
        {
            m_txtBoxImg1.Text = ((TwoBitmapProp)m_Control.SpecificProp).NomFichierInactif;
            m_txtBoxImg2.Text = ((TwoBitmapProp)m_Control.SpecificProp).NomFichierActif;
        }
        #endregion

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
    }
}

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

namespace SoundPlayer
{
    internal partial class SoundPlayerProperties : BaseControlPropertiesPanel, ISpecificPanel
    {

        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        public SoundPlayerProperties()
        {
            DllEntryClass.LangSys.Initialize(this);
            InitializeComponent();
        }

        public string FilePath
        {
            get
            {
                return m_txtBoxFile.Text;
            }
            set
            {
                m_txtBoxFile.Text = value;
            }
        }
        #region validation des données

        public void ObjectToPanel()
        {
            DllSoundPlayerProp props = m_Control.SpecificProp as DllSoundPlayerProp;
            this.FilePath = props.SoundFile;
        }

        public void PanelToObject()
        {
            bool bDataPropChange = false;
            DllSoundPlayerProp props = m_Control.SpecificProp as DllSoundPlayerProp;
            if (props.SoundFile != this.FilePath)
                bDataPropChange = true;

            if (bDataPropChange)
            {
                Document.Modified = true;
                props.SoundFile = this.FilePath;
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_btnBrowse_Click(object sender, EventArgs e, BrowseFileBtn.BrowseFrom bf)
        {
            DialogResult dlgRes = CentralizedFileDlg.ShowSndFileDilaog(bf);
            if (dlgRes == DialogResult.OK)
            {
                this.FilePath = PathTranslator.LinuxVsWindowsPathStore(
                                    Document.PathTr.AbsolutePathToRelative(CentralizedFileDlg.SndFileName));
            }
        }
    }
}

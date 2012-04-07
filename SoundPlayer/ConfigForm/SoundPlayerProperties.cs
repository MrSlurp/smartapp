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
        // controle dont on édite les propriété
        #region events
        public event ControlPropertiesChange ControlPropertiesChanged;
        #endregion

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
                m_Control.IControl.Refresh();
            }
            if (bDataPropChange && ControlPropertiesChanged != null)
                ControlPropertiesChanged(m_Control);
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
                string path = Path.GetDirectoryName(CentralizedFileDlg.SndFileName);
                this.FilePath = CentralizedFileDlg.SndFileName;
            }
        }

    }
}

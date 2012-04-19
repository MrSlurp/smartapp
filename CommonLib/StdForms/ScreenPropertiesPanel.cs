using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CommonLib;


namespace CommonLib
{
    public partial class ScreenPropertiesPanel : BaseObjectPropertiesPanel, IObjectPropertyPanel
    {
        #region données membres
        private BTScreen m_Screen = null;

        /// <summary>
        /// 
        /// </summary>
        private GestScreen m_GestScreen = null;


        #endregion

        #region Events
        public event ScreenPropertiesChange ScreenPropertiesChanged;
        #endregion

        #region constructeur
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public ScreenPropertiesPanel()
        {
            InitializeComponent();
        }
        #endregion

        #region attributs
        /// <summary>
        /// 
        /// </summary>
        public BaseGest ConfiguredItemGest
        {
            get { return m_GestScreen; }
            set { m_GestScreen = value as GestScreen; }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public BaseObject ConfiguredItem
        {
            get
            {
                return m_Screen;
            }
            set
            {
                m_Screen = value as BTScreen;
                if (m_Screen != null)
                {
                    this.Enabled = true;
                }
                else
                {
                    this.Title = string.Empty;
                    this.BackPictureFile = string.Empty;
                    this.Enabled = false;
                }
            }
        }
        #endregion

        #region attribut d'accès aux valeurs de la page de propriété

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public string Title
        {
            get
            {
                return m_textBoxTitle.Text;
            }
            set
            {
                m_textBoxTitle.Text = value;
            }
        }

        public string BackPictureFile
        {
            get
            {
                return m_textBkgndFile.Text;
            }
            set
            {
                m_textBkgndFile.Text = value;
            }
        }
        #endregion

        #region validation des données


        public void ObjectToPanel()
        {
            this.Title = m_Screen.Title;
            this.BackPictureFile = m_Screen.BackPictureFile;
            edtPosX.Value = m_Screen.ScreenPosition.X;
            edtPosY.Value = m_Screen.ScreenPosition.Y;
            edtSizeW.Value = m_Screen.ScreenSize.Width;
            edtSizeH.Value = m_Screen.ScreenSize.Height;
            chkShowTitleBar.Checked = m_Screen.StyleShowTitleBar;
            chkShowInTaskBar.Checked = m_Screen.StyleVisibleInTaskBar;
        }

        public void PanelToObject()
        {
            bool bDataPropChange = false;
            if (m_Screen.Title != this.Title)
                bDataPropChange |= true;
            if (m_Screen.BackPictureFile != this.BackPictureFile)
                bDataPropChange |= true;
            Point pos = new Point((int)edtPosX.Value, (int)edtPosY.Value);
            if (m_Screen.ScreenPosition != pos)
                bDataPropChange |= true;

            Size sz = new Size((int)edtSizeW.Value, (int)edtSizeH.Value);
            if (m_Screen.ScreenSize != sz)
                bDataPropChange |= true;

            if (m_Screen.StyleShowTitleBar != chkShowTitleBar.Checked)
                bDataPropChange |= true;
            if (m_Screen.StyleVisibleInTaskBar != chkShowInTaskBar.Checked)
                bDataPropChange |= true;

            if (bDataPropChange)
            {
                m_Screen.Title = this.Title;
                m_Screen.BackPictureFile = this.BackPictureFile;
                m_Screen.ScreenPosition = pos;
                m_Screen.ScreenSize = sz;
                m_Screen.StyleShowTitleBar = chkShowTitleBar.Checked;
                m_Screen.StyleVisibleInTaskBar = chkShowInTaskBar.Checked;
                Document.Modified = true;
            }
            if (bDataPropChange && ScreenPropertiesChanged != null)
                ScreenPropertiesChanged(m_Screen);

        }

        #endregion

        private void m_btnBrowseBkFile_Click(object sender, EventArgs e, BrowseFileBtn.BrowseFrom bf)
        {
            
            DialogResult dlgRes = CentralizedFileDlg.ShowImageFileDilaog(bf);
            if (dlgRes == DialogResult.OK)
            {
                BackPictureFile = Document.PathTr.AbsolutePathToRelative(CentralizedFileDlg.ImgFileName);
                BackPictureFile = PathTranslator.LinuxVsWindowsPathStore(BackPictureFile);
            }
        }

        private void m_btnRemoveFile_Click(object sender, EventArgs e)
        {
            BackPictureFile = string.Empty;
        }

    }
}

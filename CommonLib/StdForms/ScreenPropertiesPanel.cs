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
    public partial class ScreenPropertiesPanel : UserControl, IObjectPropertyPanel
    {
        #region données membres
        private BTDoc m_Document = null;
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
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public BTDoc Document
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

        /// <summary>
        /// 
        /// </summary>
        public BaseGest ConfiguredItemGest
        {
            get { return m_GestScreen; }
            set { m_GestScreen = value as GestScreen; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Control Panel
        {
            get { return this; }
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
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public bool IsObjectPropertiesValid
        {
            get
            {
                // aucun cas d'invalidité pour cet item
                return true;
            }
        }


        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public bool ValidateProperties()
        {
            // aucun cas d'invalidité pour cet item
            return true;
        }

        public void ObjectToPanel()
        {
            this.Title = m_Screen.Title;
            this.BackPictureFile = m_Screen.BackPictureFile;
        }

        public void PanelToObject()
        {
            bool bDataPropChange = false;
            bDataPropChange |= true;
            if (m_Screen.Title != this.Title)
                bDataPropChange |= true;
            if (m_Screen.BackPictureFile != this.BackPictureFile)
                bDataPropChange |= true;

            if (bDataPropChange)
            {
                m_Screen.Title = this.Title;
                m_Screen.BackPictureFile = this.BackPictureFile;
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
                BackPictureFile = PathTranslator.AbsolutePathToRelative(CentralizedFileDlg.ImgFileName);
                BackPictureFile = PathTranslator.LinuxVsWindowsPathStore(BackPictureFile);
            }
        }

        private void m_btnRemoveFile_Click(object sender, EventArgs e)
        {
            BackPictureFile = string.Empty;
        }

    }
}

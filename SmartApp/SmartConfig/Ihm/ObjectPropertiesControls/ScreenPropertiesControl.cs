using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CommonLib;


namespace SmartApp.Ihm
{
    public partial class ScreenPropertiesControl : UserControl
    {
        #region données membres
        private BTDoc m_Document = null;
        private BTScreen m_Screen = null;
        #endregion

        #region Events
        public event ScreenPropertiesChange ScreenPropertiesChanged;
        #endregion

        #region constructeur
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public ScreenPropertiesControl()
        {
            InitializeComponent();
        }
        #endregion

        #region attributs
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
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

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public GestScreen GestScreen
        {
            get
            {
                return m_Document.GestScreen;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public BTScreen BTScreen
        {
            get
            {
                return m_Screen;
            }
            set
            {
                m_Screen = value;
                if (m_Screen != null)
                {
                    this.Enabled = true;
                    this.Description = m_Screen.Description;
                    this.Symbol = m_Screen.Symbol;
                    this.Title = m_Screen.Title;
                    this.BackPictureFile = m_Screen.BackPictureFile;
                }
                else
                {
                    this.Description = "";
                    this.Symbol = "";
                    this.Title = "";
                    this.BackPictureFile = "";
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
        public string Description
        {
            get
            {
                return m_richTextDesc.Text;
            }
            set
            {
                m_richTextDesc.Text = value;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public string Symbol
        {
            get
            {
                return m_textBoxSymbol.Text;
            }
            set
            {
                m_textBoxSymbol.Text = value;
            }
        }

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
        public bool IsDataValuesValid
        {
            get
            {
                if (this.BTScreen == null)
                    return true;

                if (string.IsNullOrEmpty(this.Symbol))
                    return false;
                bool bRet = true;
                BTScreen dt = (BTScreen)this.GestScreen.GetFromSymbol(this.Symbol);
                if (dt != null && dt != this.BTScreen)
                    bRet = false;

                return bRet;
            }
        }


        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public bool ValidateValues()
        {
            if (this.BTScreen == null)
                return true;

            bool bRet = true;
            string strMessage = "";
            if (string.IsNullOrEmpty(this.Symbol))
            {
                strMessage = "Symbol must not be empty";
                bRet = false;
            }
            BTScreen Sc = (BTScreen)GestScreen.GetFromSymbol(this.Symbol);
            if (bRet && Sc != null && Sc != this.BTScreen)
            {
                strMessage = string.Format("A Screen with symbol {0} already exist", Symbol);
                bRet = false;
            }
            if (!bRet)
            {
                MessageBox.Show(strMessage);
                return bRet;
            }
            bool bDataPropChange = false;
            if (m_Screen.Description != this.Description)
                bDataPropChange |= true;
            if (m_Screen.Symbol != this.Symbol)
                bDataPropChange |= true;
            if (m_Screen.Title != this.Title)
                bDataPropChange |= true;
            if (m_Screen.BackPictureFile != this.BackPictureFile)
                bDataPropChange |= true;

            if (bDataPropChange)
            {
                m_Screen.Description = this.Description;
                m_Screen.Symbol = this.Symbol;
                m_Screen.Title = this.Title;
                m_Screen.BackPictureFile = this.BackPictureFile;
                Doc.Modified = true;
            }
            if (bDataPropChange && ScreenPropertiesChanged != null)
                ScreenPropertiesChanged(m_Screen);
            return true;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void PropertiesControlValidating(object sender, CancelEventArgs e)
        {
            if (!ValidateValues())
                e.Cancel = true;
        }

        #endregion

        private void m_btnBrowseBkFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (JPEG,GIF,BMP, PNG)|*.jpg;*.jpeg;*.gif;*.bmp;*.png|JPEG Files(*.jpg;*.jpeg)|*.jpg;*.jpeg|GIF Files(*.gif)|*.gif|BMP Files(*.bmp)|*.bmp|PNG Files(*.png)|*.png";
            openFileDialog.InitialDirectory = Application.StartupPath;
            DialogResult dlgRes = openFileDialog.ShowDialog();
            if (dlgRes == DialogResult.OK)
            {
                string FileName;
                if (openFileDialog.FileName.StartsWith(Application.StartupPath))
                {
                    FileName = openFileDialog.FileName.Replace(Application.StartupPath, @".");
                }
                else
                    FileName = openFileDialog.FileName;

                BackPictureFile = FileName;
            }
        }

    }
}

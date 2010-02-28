using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommonLib;

namespace SmartApp.Ihm
{
    public partial class ProgramForm : Form
    {
        #region données membres
        private BTDoc m_Document = null;
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
                m_PanelProgFunc.Doc = m_Document;
                m_PanelProgTimer.Doc = m_Document;
                m_PanelProgLogger.Doc = m_Document;
            }
        }
        #endregion

        #region constructeur et initialiseur
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        public ProgramForm()
        {
            Program.LangSys.Initialize(this);
            InitializeComponent();
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        public void Initialize()
        {
            m_PanelProgFunc.Initialize();
            m_PanelProgTimer.Initialize();
            m_PanelProgLogger.Initialize();
        }
        #endregion

        #region handlers d'évènements
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (m_PanelProgFunc.IsItemValueValid
                    && m_PanelProgTimer.IsItemValueValid
                    && m_PanelProgLogger.IsItemValueValid
                    )
                {
                    this.WindowState = FormWindowState.Minimized;
                }
                e.Cancel = true;
            }
            else if (e.CloseReason == CloseReason.MdiFormClosing)
            {
                if (!m_PanelProgFunc.IsItemValueValid
                    || !m_PanelProgTimer.IsItemValueValid
                    || !m_PanelProgLogger.IsItemValueValid
                    )
                    e.Cancel = true;
            }
        }
        #endregion
    }
}
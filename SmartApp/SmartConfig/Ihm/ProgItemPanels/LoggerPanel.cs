using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CommonLib;


namespace SmartApp.Ihm.ProgItemPanels
{
    public partial class LoggerPanel : UserControl
    {
        private BTDoc m_Document = null;
        private int m_CurSelectedIndex = -1;

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
                m_panelLogProp.Doc = m_Document;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public GestLogger GestLogger
        {
            get
            {
                return m_Document.GestLogger;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public bool IsItemValueValid
        {
            get
            {
                return m_panelLogProp.IsDataValuesValid;
            }
        }

        #endregion

        #region constructeur et init
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public LoggerPanel()
        {
            InitializeComponent();
            m_panelLogProp.LoggerPropChange += new LoggerPropertiesChange(this.OnLoggerPropertiesChange);
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public void Initialize()
        {
            if (m_Document == null)
                return;

            m_panelLogProp.Logger = null;
            InitListView();
        }
        #endregion

        #region gestion de la listview
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public void InitListView()
        {
            m_listViewLogger.Items.Clear();

            if (this.GestLogger != null)
            {
                for (int i = 0; i < this.GestLogger.Count; i++)
                {
                    Logger Fc = (Logger)GestLogger[i];
                    ListViewItem lviData = new ListViewItem(Fc.Symbol);
                    lviData.Tag = Fc;
                    m_listViewLogger.Items.Add(lviData);
                }
                m_panelLogProp.Logger = null;
                m_CurSelectedIndex = -1;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public void UpdateListView()
        {
            for (int i = 0; i < m_listViewLogger.Items.Count; i++)
            {
                ListViewItem lviData = m_listViewLogger.Items[i];
                Logger Fc = (Logger)lviData.Tag;
                lviData.Text = Fc.Symbol;
            }
        }
        #endregion

        #region handlers d'event
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void listViewSelectedLoggerChanged(object sender, EventArgs e)
        {
            ListViewItem lviData = null;
            if (m_listViewLogger.SelectedItems.Count > 0)
                lviData = m_listViewLogger.SelectedItems[0];
            if (lviData != null)
            {
                if (lviData.Index == m_CurSelectedIndex)
                    return;

                if (m_panelLogProp.IsDataValuesValid)
                {
                    m_CurSelectedIndex = lviData.Index;
                    string strDataSymb = lviData.Text;
                    BaseObject Fc = this.GestLogger.GetFromSymbol(strDataSymb);
                    m_panelLogProp.Logger = (Logger)Fc;
                }
                else
                {
                    lviData.Focused = false;
                    m_listViewLogger.Items[m_CurSelectedIndex].Selected = true;
                }
            }
            else
            {
                m_panelLogProp.Logger = null;
                m_CurSelectedIndex = -1;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void OnListViewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete
                || e.KeyCode == Keys.Back)
            {
                ListViewItem lviData = null;
                if (m_listViewLogger.SelectedItems.Count > 0)
                    lviData = m_listViewLogger.SelectedItems[0];
                if (lviData != null)
                {
                    if (this.GestLogger.RemoveObj((BaseObject)lviData.Tag))
                    {
                        m_listViewLogger.Items.Remove(lviData);
                        if (m_panelLogProp.Logger == (Logger)lviData.Tag)
                        {
                            m_panelLogProp.Logger = null;
                            InitListView();
                        }
                    }
                }
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void OnbtnNewClick(object sender, EventArgs e)
        {
            string strNewSymb = this.GestLogger.GetNextDefaultSymbol();
            Logger Scr = new Logger();
            Scr.Symbol = strNewSymb;
            this.GestLogger.AddObj(Scr);
            m_Document.Modified = true;
            InitListView();
        }
        
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void OnLoggerPropertiesChange(Logger Scr)
        {
            UpdateListView();
        }

        #endregion
    }
}

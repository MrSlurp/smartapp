using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SmartApp.Datas;
using SmartApp.Gestionnaires;

namespace SmartApp.Ihm.ProgItemPanels
{
    public partial class TimerPanel : UserControl
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
                m_PanelTimerProp.Doc = m_Document;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public GestTimer GestTimer
        {
            get
            {
                return m_Document.GestTimer;
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
                return m_PanelTimerProp.IsDataValuesValid;
            }
        }
        #endregion

        #region constructeur et init
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public TimerPanel()
        {
            InitializeComponent();
            m_PanelTimerProp.TimerPropChange += new TimerPropertiesChange(this.OnTimerPropertiesChange);
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public void Initialize()
        {
            if (m_Document == null)
                return;

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
            m_listView.Items.Clear();

            if (this.GestTimer != null)
            {
                for (int i = 0; i < this.GestTimer.Count; i++)
                {
                    BTTimer Fc = (BTTimer)this.GestTimer[i];
                    ListViewItem lviData = new ListViewItem(Fc.Symbol);
                    lviData.SubItems.Add(Fc.Period.ToString());
                    lviData.Tag = Fc;
                    m_listView.Items.Add(lviData);
                }
                m_PanelTimerProp.Timer = null;
                m_CurSelectedIndex = -1;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public void UpdateListView()
        {
            for (int i = 0; i < m_listView.Items.Count; i++)
            {
                ListViewItem lviData = m_listView.Items[i];
                BTTimer Fc = (BTTimer)lviData.Tag;
                lviData.Text = Fc.Symbol;
                lviData.SubItems[1].Text = Fc.Period.ToString();
            }
        }
        #endregion

        #region handlers d'event
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void listViewSelectedTimerChanged(object sender, EventArgs e)
        {
            ListViewItem lviData = null;
            if (m_listView.SelectedItems.Count > 0)
                lviData = m_listView.SelectedItems[0];
            if (lviData != null)
            {
                if (lviData.Index == m_CurSelectedIndex)
                    return;

                if (m_PanelTimerProp.IsDataValuesValid)
                {
                    m_CurSelectedIndex = lviData.Index;
                    string strDataSymb = lviData.Text;
                    BaseObject Fc = this.GestTimer.GetFromSymbol(strDataSymb);
                    m_PanelTimerProp.Timer = (BTTimer)Fc;
                }
                else
                {
                    lviData.Focused = false;
                    m_listView.Items[m_CurSelectedIndex].Selected = true;
                }
            }
            else
            {
                m_PanelTimerProp.Timer = null;
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
                if (m_listView.SelectedItems.Count > 0)
                    lviData = m_listView.SelectedItems[0];
                if (lviData != null)
                {
                    if (this.GestTimer.RemoveObj((BaseObject)lviData.Tag))
                    {
                        m_listView.Items.Remove(lviData);
                        if (m_PanelTimerProp.Timer == (BTTimer)lviData.Tag)
                        {
                            m_PanelTimerProp.Timer = null;
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
            string strNewSymb = this.GestTimer.GetNextDefaultSymbol();
            BTTimer Scr = new BTTimer();
            Scr.Symbol = strNewSymb;
            this.GestTimer.AddObj(Scr);
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

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void OnTimerPropertiesChange(BTTimer Timer)
        {
            UpdateListView();
        }
        #endregion
    }
}

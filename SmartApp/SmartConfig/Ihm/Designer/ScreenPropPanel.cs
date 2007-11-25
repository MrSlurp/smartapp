using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SmartApp.Gestionnaires;
using SmartApp.Datas;

namespace SmartApp.Ihm
{
    public partial class ScreenPropPanel : UserControl
    {
        #region données membres
        private BTDoc m_Document = null;
        private int m_CurSelectedIndex = -1;
        //private bool m_bListViewIngnoreNextSignificantChange = false;
        #endregion

        #region Events
        public event ScreenPropertiesChange SelectedScreenChange;
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
                m_PanelScreenProperties.Doc = m_Document;
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
        #endregion

        #region constructeur et init
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public ScreenPropPanel()
        {
            InitializeComponent();
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public void Initialize()
        {
            if (GestScreen == null)
                return;

            InitListView();
            m_PanelScreenProperties.ScreenPropertiesChanged += new ScreenPropertiesChange(this.OnScreenPropertiesChange);
        }
        #endregion

        #region gestion de la listview
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public void InitListView()
        {
            m_ListViewScreens.Items.Clear();

            if (GestScreen != null)
            {
                for (int i = 0; i < GestScreen.Count; i++)
                {
                    BTScreen Sc = (BTScreen)GestScreen[i];
                    ListViewItem lviData = new ListViewItem(Sc.Symbol);
                    lviData.Tag = Sc;
                    lviData.SubItems.Add(Sc.Title);
                    m_ListViewScreens.Items.Add(lviData);
                }
                m_PanelScreenProperties.BTScreen = null;
                if (SelectedScreenChange != null)
                    SelectedScreenChange(null);
                m_CurSelectedIndex = -1;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public void UpdateListView()
        {
            for (int i = 0; i < m_ListViewScreens.Items.Count; i++)
            {
                ListViewItem lviData = m_ListViewScreens.Items[i];
                BTScreen Sc = (BTScreen)lviData.Tag;
                lviData.Text = Sc.Symbol;
                lviData.SubItems[1].Text = Sc.Title;
            }
        }
        #endregion

        #region handlers d'event
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void listViewSelectedScreenChanged(object sender, EventArgs e)
        {
            ListViewItem lviData = null;
            if (m_ListViewScreens.SelectedItems.Count > 0)
                lviData = m_ListViewScreens.SelectedItems[0];
            if (lviData != null)
            {
                //if (lviData.Index == m_CurSelectedIndex)
                //    return;

                if (m_PanelScreenProperties.IsDataValuesValid )
                {
                    m_CurSelectedIndex = lviData.Index;
                    string strDataSymb = lviData.Text;
                    BaseObject Scr = GestScreen.GetFromSymbol(strDataSymb);
                    m_PanelScreenProperties.BTScreen = (BTScreen)Scr;
                    if (SelectedScreenChange != null)
                        SelectedScreenChange((BTScreen)Scr);
                }
                else
                {
                    lviData.Focused = false;
                    m_ListViewScreens.Items[m_CurSelectedIndex].Selected = true;
                }
            }
            else
            {
                m_PanelScreenProperties.BTScreen = null;
                m_CurSelectedIndex = -1;
                if (SelectedScreenChange != null)
                    SelectedScreenChange(null);
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
                if (m_ListViewScreens.SelectedItems.Count > 0)
                    lviData = m_ListViewScreens.SelectedItems[0];
                if (lviData != null)
                {
                    if (GestScreen.RemoveObj((BaseObject)lviData.Tag))
                    {
                        m_ListViewScreens.Items.Remove(lviData);
                        if (this.SelectedScreenChange != null)
                        {
                            SelectedScreenChange(null);
                        }
                    }
                }
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void OnScreenPropertiesChange(BTScreen Scr)
        {
            UpdateListView();
            if (this.SelectedScreenChange != null)
            {
                SelectedScreenChange(Scr);
            }

        }
        #endregion

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void OnbtnNewScreenClick(object sender, EventArgs e)
        {
            string strNewSymb = this.GestScreen.GetNextDefaultSymbol();
            BTScreen Scr = new BTScreen();
            Scr.Symbol = strNewSymb;
            this.GestScreen.AddObj(Scr);
            m_Document.Modified = true;
            InitListView();
        }
    }
}

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
    public partial class FunctionPanel : UserControl
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
                m_panelFunc.Doc = m_Document;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public GestFunction GestFunction
        {
            get
            {
                return m_Document.GestFunction;
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
                return m_panelFunc.IsDataValuesValid;
            }
        }
        #endregion

        #region constructeur et init
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public FunctionPanel()
        {
            InitializeComponent();
            m_panelFunc.FunctionPropChange += new FunctionPropertiesChange(this.OnFunctionPropertiesChange);
        }
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public void Initialize()
        {
            if (m_Document == null)
                return;
            m_panelFunc.Function = null;
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
            m_listViewFunc.Items.Clear();

            if (GestFunction != null)
            {
                for (int i = 0; i < GestFunction.Count; i++)
                {
                    Function Fc = (Function)GestFunction[i];
                    ListViewItem lviData = new ListViewItem(Fc.Symbol);
                    lviData.Tag = Fc;
                    m_listViewFunc.Items.Add(lviData);
                }
                m_panelFunc.Function = null;
                m_CurSelectedIndex = -1;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public void UpdateListView()
        {
            for (int i = 0; i < m_listViewFunc.Items.Count; i++)
            {
                ListViewItem lviData = m_listViewFunc.Items[i];
                Function Fc = (Function)lviData.Tag;
                lviData.Text = Fc.Symbol;
            }
        }
        #endregion

        #region handlers d'event
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void listViewSelectedFunctionChanged(object sender, EventArgs e)
        {
            ListViewItem lviData = null;
            if (m_listViewFunc.SelectedItems.Count > 0)
                lviData = m_listViewFunc.SelectedItems[0];
            if (lviData != null)
            {
                if (lviData.Index == m_CurSelectedIndex)
                    return;

                if (m_panelFunc.IsDataValuesValid)
                {
                    m_CurSelectedIndex = lviData.Index;
                    string strDataSymb = lviData.Text;
                    BaseObject Fc = this.GestFunction.GetFromSymbol(strDataSymb);
                    m_panelFunc.Function = (Function)Fc;
                }
                else
                {
                    lviData.Focused = false;
                    m_listViewFunc.Items[m_CurSelectedIndex].Selected = true;
                }
            }
            else
            {
                m_panelFunc.Function = null;
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
                if (m_listViewFunc.SelectedItems.Count > 0)
                    lviData = m_listViewFunc.SelectedItems[0];
                if (lviData != null)
                {
                    if (this.GestFunction.RemoveObj((BaseObject)lviData.Tag))
                    {
                        m_listViewFunc.Items.Remove(lviData);
                        if (m_panelFunc.Function == (Function)lviData.Tag)
                        {
                            m_panelFunc.Function = null;
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
            string strNewSymb = this.GestFunction.GetNextDefaultSymbol();
            Function Scr = new Function();
            Scr.Symbol = strNewSymb;
            this.GestFunction.AddObj(Scr);
            InitListView();
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void OnFunctionPropertiesChange(Function Scr)
        {
            UpdateListView();
        }
        #endregion
    }
}

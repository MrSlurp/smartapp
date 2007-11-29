using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartApp.Gestionnaires;
using SmartApp.Datas;

namespace SmartApp.Ihm
{
    public delegate void AsyncUpdateHMI(MessNeedUpdate Mess);

    public partial class FrameForm : Form
    {
        #region données membres de la classe
        private BTDoc m_Document = null;

        // index séléctionnné dans la listview
        private int m_CurSelectedFrameIndex = -1;
        #endregion

        #region constructeur et initialiseur
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public FrameForm()
        {
            InitializeComponent();
            m_PanelFrameProp.BeforeDataListChange += new BeforeCurrentDataListChange(OnTrameDataListWillChange);
            m_PanelFrameProp.DataListChange += new CurrentDataListChanged(OnTrameDataListChanged);
            m_PanelFrameProp.FramePropertiesChanged += new TramePropertiesChange(FramePropertiesChanged);
        }

        void FramePropertiesChanged(Trame Trame)
        {
            UpdateListView();
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public void Initialize()
        {
            InitListViewFrame();
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
                m_PanelFrameProp.Doc = m_Document;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public GestTrame GestTrame
        {
            get
            {
                return m_Document.GestTrame;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public GestData GestData
        {
            get
            {
                return m_Document.GestData;
            }
        }
        #endregion

        #region Gestion de la listview trame
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        
        public void InitListViewFrame()
        {
            m_listViewFrames.Items.Clear();
            if (this.GestTrame != null)
            {
                for (int i = 0; i < this.GestTrame.Count; i++)
                {
                    Trame dt = (Trame)this.GestTrame[i];
                    ListViewItem lviData = new ListViewItem(dt.Symbol);
                    lviData.Tag = dt;
                    lviData.SubItems.Add(dt.FrameDatas.Count.ToString());
                    m_listViewFrames.Items.Add(lviData);
                }
                m_PanelFrameProp.Trame = null;
                InitListViewFrameData();
                m_CurSelectedFrameIndex = -1;
            }
        }
        
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        public void UpdateListView()
        {
            for (int i = 0; i < m_listViewFrames.Items.Count; i++)
            {
                ListViewItem lviData = m_listViewFrames.Items[i];
                Trame dt = (Trame)lviData.Tag;
                lviData.Text = dt.Symbol;
                lviData.SubItems[1].Text = dt.FrameDatas.Count.ToString();
            }
        }
        #endregion

        #region Gestion de la listview data
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        
        public void InitListViewFrameData()
        {
            m_ListViewFrameData.Items.Clear();
            if (m_PanelFrameProp.Trame == null)
                return;
            StringCollection Lst = m_PanelFrameProp.Trame.FrameDatas;
            if (Lst != null)
            {
                for (int i = 0; i < Lst.Count; i++)
                {
                    string dtsymb = Lst[i];
                    Data dt = (Data)this.GestData.GetFromSymbol(dtsymb);
                    if (dt == null)
                    {
                        System.Diagnostics.Debug.Assert(false);
                        continue;
                    }

                    ListViewItem lviData = new ListViewItem(dt.Symbol);
                    lviData.Tag = dt;
                    BaseGestGroup.Group gr = GestData.GetGroupFromObject(dt);
                    if (gr != null)
                    {
                        lviData.BackColor = gr.m_GroupColor;
                    }
                    lviData.SubItems.Add(dt.Size.ToString());
                    lviData.SubItems.Add(dt.IsConstant.ToString());
                    lviData.SubItems.Add(dt.DefaultValue.ToString());
                    m_ListViewFrameData.Items.Add(lviData);
                }
            }
        }
        
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        
        public void UpdateListViewFrameData()
        {
            for (int i = 0; i < m_ListViewFrameData.Items.Count; i++)
            {
                ListViewItem lviData = m_ListViewFrameData.Items[i];
                Data dt = (Data)lviData.Tag;
                lviData.Text = dt.Symbol;
                BaseGestGroup.Group gr = GestData.GetGroupFromObject(dt);
                if (gr != null)
                {
                    lviData.BackColor = gr.m_GroupColor;
                }

                lviData.SubItems[1].Text = dt.Size.ToString();
                lviData.SubItems[2].Text = dt.IsConstant.ToString();
                lviData.SubItems[3].Text = dt.DefaultValue.ToString();
            }
        }
        
        #endregion

        #region handlers d'évènements

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        
        private void listViewSelectedFrameChanged(object sender, EventArgs e)
        {
            ListViewItem lviData = null;
            if (m_listViewFrames.SelectedItems.Count > 0)
                lviData = m_listViewFrames.SelectedItems[0];
            if (lviData != null)
            {
                if (m_PanelFrameProp.IsTrameValuesValid)
                {
                    m_CurSelectedFrameIndex = lviData.Index;
                    string strDataSymb = lviData.Text;
                    BaseObject Dt = GestTrame.GetFromSymbol(strDataSymb);
                    UpdateFrameDataListFromListView();
                    m_PanelFrameProp.Trame = (Trame)Dt;
                    InitListViewFrameData();
                }
                else
                {
                    lviData.Focused = false;
                    m_listViewFrames.Items[m_CurSelectedFrameIndex].Selected = true;
                }
            }
            else
            {
                m_PanelFrameProp.Trame = null;
                m_CurSelectedFrameIndex = -1;
                InitListViewFrameData();
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
                if (m_listViewFrames.SelectedItems.Count > 0)
                    lviData = m_listViewFrames.SelectedItems[0];
                if (lviData != null)
                {
                    if (GestTrame.RemoveObj((BaseObject)lviData.Tag))
                    {
                        m_listViewFrames.Items.Remove(lviData);
                        if (m_PanelFrameProp.Trame == (Trame)lviData.Tag)
                        {
                            m_PanelFrameProp.Trame = null;
                            InitListViewFrameData();
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
            string strNewSymb = this.GestTrame.GetNextDefaultSymbol();
            Trame Scr = new Trame();
            Scr.Symbol = strNewSymb;
            this.GestTrame.AddObj(Scr);
            InitListViewFrame();
            InitListViewFrameData();
            m_Document.Modified = true;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void OnListViewFrameDataKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete
                || e.KeyCode == Keys.Back)
            {
                ListViewItem lviData = null;
                if (m_ListViewFrameData.SelectedItems.Count > 0)
                    lviData = m_ListViewFrameData.SelectedItems[0];
                if (lviData != null)
                {
                    if (!((Data)lviData.Tag).Symbol.EndsWith(Cste.STR_SUFFIX_CTRLDATA))
                    {
                        m_ListViewFrameData.Items.Remove(lviData);
                        UpdateFrameDataListFromListView();
                        UpdateListView();
                        m_Document.Modified = true;
                    }
                    else
                        MessageBox.Show("You can't remove frame control data");
                }
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (m_PanelFrameProp.IsTrameValuesValid)
                {
                    this.WindowState = FormWindowState.Minimized;
                }
                e.Cancel = true;
            }
            else if (e.CloseReason == CloseReason.MdiFormClosing)
            {
                if (!m_PanelFrameProp.IsTrameValuesValid)
                    e.Cancel = true;
            }
        }
        #endregion

        #region gestion du drag drop dans la liste des données de la trame
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void OnListViewFrameDataDragEnter(object sender, DragEventArgs e)
        {
            Data dt = (Data)e.Data.GetData(typeof(Data));
            if (m_PanelFrameProp.Trame != null && dt != null)
            {
                bool bFind = false;
                for (int i = 0; i< m_ListViewFrameData.Items.Count; i++)
                {
                    if (m_ListViewFrameData.Items[i].Text == dt.Symbol)
                    {
                        bFind = true;
                        break;
                    }
                }

                if (bFind)
                {
                    e.Effect = DragDropEffects.Move;
                }
                else
                {
                    e.Effect = DragDropEffects.Copy;
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void OnListViewFrameDataItemDrag(object sender, ItemDragEventArgs e)
        {
            try
            {
                Data lviData = (Data)((ListViewItem)e.Item).Tag;
                DoDragDrop(lviData, DragDropEffects.All);
            }
            catch (Exception)
            {

            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void OnListViewFrameDataDragDrop(object sender, DragEventArgs e)
        {
            Data DropedItem = (Data)e.Data.GetData(typeof(Data));
            if (DropedItem != null)
            {
                //this.AssociateData = DropedItem.Symbol;
                bool bFind = false;
                ListViewItem LvFoundItem = null;
                for (int i = 0; i < m_ListViewFrameData.Items.Count; i++)
                {
                    if (m_ListViewFrameData.Items[i].Text == DropedItem.Symbol)
                    {
                        bFind = true;
                        LvFoundItem = m_ListViewFrameData.Items[i];
                        break;
                    }
                }

                if (bFind)
                {
                    Point prCurInListView = m_ListViewFrameData.PointToClient(new Point(e.X, e.Y));
                    ListViewItem lviAtCursor = m_ListViewFrameData.GetItemAt(prCurInListView.X, prCurInListView.Y);
                    m_ListViewFrameData.Items.Remove(LvFoundItem);
                    if (lviAtCursor != null)
                        m_ListViewFrameData.Items.Insert(lviAtCursor.Index, LvFoundItem);
                    else
                        m_ListViewFrameData.Items.Add(LvFoundItem);
                    m_Document.Modified = true;
                    UpdateListView();
                }
                else
                {
                    Point prCurInListView = m_ListViewFrameData.PointToClient(new Point(e.X, e.Y));
                    ListViewItem lviAtCursor = m_ListViewFrameData.GetItemAt(prCurInListView.X, prCurInListView.Y);

                    ListViewItem lviData = new ListViewItem(DropedItem.Symbol);
                    lviData.Tag = DropedItem;
                    lviData.SubItems.Add(DropedItem.Size.ToString());
                    lviData.SubItems.Add(DropedItem.IsConstant.ToString());
                    lviData.SubItems.Add(DropedItem.DefaultValue.ToString());
                    //m_ListViewFrameData.Items.Add(lviData);

                    if (lviAtCursor != null)
                        m_ListViewFrameData.Items.Insert(lviAtCursor.Index, lviData);
                    else
                        m_ListViewFrameData.Items.Add(lviData);
                    m_Document.Modified = true;
                    UpdateListView();
                }
            }
            UpdateListViewFrameData();
            UpdateFrameDataListFromListView();
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void OnListViewFrameDataDragOver(object sender, DragEventArgs e)
        {
            Data DropItem = (Data)e.Data.GetData(typeof(Data));
            if (DropItem != null)
            {
                Point prCurInListView = m_ListViewFrameData.PointToClient(new Point(e.X, e.Y));
                ListViewItem lviAtCursor = m_ListViewFrameData.GetItemAt(prCurInListView.X, prCurInListView.Y);
                for (int i = 0; i < m_ListViewFrameData.Items.Count; i++)
                {
                    if (m_ListViewFrameData.Items[i] != lviAtCursor)
                    {
                        BaseGestGroup.Group gr = this.GestData.GetGroupFromObject((BaseObject)m_ListViewFrameData.Items[i].Tag);
                        if (gr != null)
                            m_ListViewFrameData.Items[i].BackColor = gr.m_GroupColor;
                        else
                            m_ListViewFrameData.Items[i].BackColor = m_ListViewFrameData.BackColor;
                    }
                }
                if (lviAtCursor != null)
                    lviAtCursor.BackColor = Color.Gray;
            }
        }

        private void OnListViewFrameDataDragLeave(object sender, EventArgs e)
        {
            UpdateListViewFrameData();
        }

        #endregion


        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void UpdateFrameDataListFromListView()
        {
            if (m_PanelFrameProp.Trame == null)
                return;

            m_PanelFrameProp.Trame.FrameDatas.Clear();
            for (int i = 0; i < m_ListViewFrameData.Items.Count; i++)
            {
                m_PanelFrameProp.Trame.FrameDatas.Add(m_ListViewFrameData.Items[i].Text);
            }

        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void OnTrameDataListWillChange()
        {
            UpdateFrameDataListFromListView();
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void OnTrameDataListChanged()
        {
            InitListViewFrameData();
        }
    }
}
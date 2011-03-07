using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.IO;
using System.Windows.Forms;
using CommonLib;


namespace SmartApp.Ihm
{

    public partial class LoggerPropertiesControl : UserControl, ILangReloadable
    {
        #region données membres
        private Logger m_Logger = null;
        private BTDoc m_Document = null;
        CComboData[] m_TabCboLogType;
        CComboData[] m_TabCboSeparator;
        CComboData[] m_TabNamingType;
        #endregion

        #region Events
        public event LoggerPropertiesChange LoggerPropChange;
        #endregion

        #region attributs de la classe
        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        public GestLogger GestLogger
        {
            get
            {
                return m_Document.GestLogger;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Logger Logger
        {
            get
            {
                return m_Logger;
            }
            set
            {
                m_Logger = value;
                if (m_Logger != null)
                {
                    this.Enabled = true;
                    this.Description = m_Logger.Description;
                    this.Symbol = m_Logger.Symbol;
                    this.LogType = m_Logger.LogType;
                    this.Period = m_Logger.Period;
                    this.LogFile = m_Logger.LogFile;
                    this.AutoStart = m_Logger.AutoStart;
                    this.CsvSeperator = m_Logger.CsvSeperator;
                    this.LoggerMode = m_Logger.LoggerMode;
                    this.DateFormatString = m_Logger.DateFormatString;
                }
                else
                {
                    this.Description = "";
                    this.Symbol = "";
                    this.Enabled = false;
                    this.LogType = LOGGER_TYPE.STANDARD.ToString();
                    this.Period = 200;
                    this.LogFile = "";
                    this.AutoStart = false;
                    this.CsvSeperator = '\t';
                    this.LoggerMode = Logger.LogMode.none;
                    this.DateFormatString = "";
                }
                this.InitListViewData();
                this.UpdateFromLogType();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Period
        {
            get
            {
                return (int)m_LoggerPeriod.Value;
            }
            set
            {
                m_LoggerPeriod.Value = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string LogFile
        {
            get
            {
                return m_txtFileName.Text + ".csv";
            }
            set
            {
                if (value != null)
                    m_txtFileName.Text = value.Replace(".csv", "");
                else
                    m_txtFileName.Text = "";
            }
        }

        #endregion

        #region constructeur
        /// <summary>
        /// 
        /// </summary>
        public LoggerPropertiesControl()
        {
            InitializeComponent();
            LoadNonStandardLang();
            m_LoggerPeriod.Minimum = 200;
            m_LoggerPeriod.Maximum = 3600000;

            m_txtFileName.CharacterCasing = CharacterCasing.Normal;
        }

        /// <summary>
        /// 
        /// </summary>
        public void LoadNonStandardLang()
        {
            m_TabCboLogType = new CComboData[2];
            m_TabCboLogType[0] = new CComboData(Program.LangSys.C("Normal"), (object)LOGGER_TYPE.STANDARD.ToString());
            m_TabCboLogType[1] = new CComboData(Program.LangSys.C("Auto"), (object)LOGGER_TYPE.AUTO.ToString());
            m_cboLogType.ValueMember = "Object";
            m_cboLogType.DisplayMember = "DisplayedString";
            m_cboLogType.DataSource = m_TabCboLogType;
            m_cboLogType.SelectedIndex = 0;

            m_TabCboSeparator = new CComboData[3];
            m_TabCboSeparator[0] = new CComboData(Program.LangSys.C("Tabulation"), '\t');
            m_TabCboSeparator[1] = new CComboData(Program.LangSys.C("Semi colon"), ';');
            m_TabCboSeparator[2] = new CComboData(Program.LangSys.C("Coma"), ',');

            m_cboSeparator.ValueMember = "Object";
            m_cboSeparator.DisplayMember = "DisplayedString";
            m_cboSeparator.DataSource = m_TabCboSeparator;
            m_cboSeparator.SelectedIndex = 0;

            m_TabNamingType = new CComboData[3];
            m_TabNamingType[0] = new CComboData(Program.LangSys.C("None"), Logger.LogMode.none);
            m_TabNamingType[1] = new CComboData(Program.LangSys.C("Auto number"), Logger.LogMode.autoNum);
            m_TabNamingType[2] = new CComboData(Program.LangSys.C("Auto date time"), Logger.LogMode.autoDated);

            m_cboNaming.ValueMember = "Object";
            m_cboNaming.DisplayMember = "DisplayedString";
            m_cboNaming.DataSource = m_TabNamingType;
            m_cboNaming.SelectedIndex = 0;
        }
        #endregion

        #region attribut d'accès aux valeurs de la page de propriété
        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        public string Symbol
        {
            get
            {
                return m_textSymbol.Text;
            }
            set
            {
                m_textSymbol.Text = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string LogType
        {
            get
            {
                if (this.m_cboLogType != null && this.m_cboLogType.SelectedValue != null)
                    return (string)this.m_cboLogType.SelectedValue;
                else
                    return "";
            }
            set
            {
                if (this.m_cboLogType != null)
                    this.m_cboLogType.SelectedValue = (string)value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public char CsvSeperator
        {
            get
            {
                if (this.m_cboSeparator != null && this.m_cboSeparator.SelectedValue != null)
                    return (char)this.m_cboSeparator.SelectedValue;
                else
                    return '\t';
            }
            set
            {
                if (this.m_cboSeparator != null)
                    this.m_cboSeparator.SelectedValue = (char)value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool AutoStart
        {
            get
            {
                return m_chkAutoStart.Checked;
            }
            set
            {
                m_chkAutoStart.Checked = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Logger.LogMode LoggerMode
        {
            get
            {
                if (this.m_cboNaming != null && this.m_cboNaming.SelectedValue != null)
                    return (Logger.LogMode) this.m_cboNaming.SelectedValue;
                else
                    return Logger.LogMode.none;
            }
            set
            {
                if (this.m_cboNaming != null)
                    this.m_cboNaming.SelectedValue = value;
            }
        }

        public string DateFormatString
        {
            get
            {
                return edtDateFormat.Text;
            }
            set
            {
                edtDateFormat.Text = value;
            }
        }
        #endregion

        #region validation des données
        /// <summary>
        /// 
        /// </summary>
        public bool IsDataValuesValid
        {
            get
            {
                if (this.Logger == null)
                    return true;

                if (string.IsNullOrEmpty(this.Symbol))
                    return false;
                bool bRet = true;
                Logger dt = (Logger)this.GestLogger.GetFromSymbol(this.Symbol);
                if (dt != null && dt != this.Logger)
                    bRet = false;
                if (string.IsNullOrEmpty(this.LogFile))
                    bRet = false;

                return bRet;
            }
        }

       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
        public bool ValidateValues()
        {
            if (this.Logger == null)
                return true;

            bool bRet = true;
            string strMessage = "";
            if (string.IsNullOrEmpty(this.Symbol))
            {
                strMessage = Program.LangSys.C("Symbol must not be empty");
                bRet = false;
            }
            Logger Sc = (Logger)GestLogger.GetFromSymbol(this.Symbol);
            if (bRet && Sc != null && Sc != this.Logger)
            {
                strMessage = string.Format(Program.LangSys.C("A Logger with symbol {0} already exist"), Symbol);
                bRet = false;
            }
            if (bRet && string.IsNullOrEmpty(this.LogFile))
            {
                strMessage = string.Format(Program.LangSys.C("Logger output filename must not be empty"));
                bRet = false;
            }
            if (bRet && (this.LoggerMode == Logger.LogMode.autoDated))
            {
                if (string.IsNullOrEmpty(this.DateFormatString))
                {
                    strMessage = string.Format(Program.LangSys.C("Logger date time format must not be empty"));
                    bRet = false;
                }
                else
                {
                    string illegal = this.DateFormatString;
                    string illegachars = new string(Path.GetInvalidFileNameChars());
                    foreach (char c in illegachars)
                    {
                        if (illegal.Contains(c.ToString()))
                        {
                            strMessage = string.Format(Program.LangSys.C("Following chars can't be used for file names : ") + illegachars);
                            bRet = false;
                        }
                    }
                }
            }

            if (!bRet)
            {
                MessageBox.Show(strMessage, Program.LangSys.C("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return bRet;
            }

            bool bDataPropChange = false;
            if (m_Logger.Description != this.Description)
                bDataPropChange |= true;
            if (m_Logger.Symbol != this.Symbol)
                bDataPropChange |= true;

            if (m_Logger.LogType != this.LogType)
                bDataPropChange |= true;

            if (m_Logger.Period != this.Period)
                bDataPropChange |= true;

            if (m_Logger.LogFile != this.LogFile)
                bDataPropChange |= true;

            if (m_Logger.AutoStart != this.AutoStart)
                bDataPropChange |= true;

            if (m_Logger.CsvSeperator != this.CsvSeperator)
                bDataPropChange |= true;

            if (bDataPropChange)
            {
                m_Logger.Description = this.Description;
                m_Logger.Symbol = this.Symbol;
                m_Logger.LogType = this.LogType;
                m_Logger.Period = this.Period;
                m_Logger.LogFile = this.LogFile;
                m_Logger.AutoStart = this.AutoStart;
                m_Logger.CsvSeperator = this.CsvSeperator;
                Doc.Modified = true;
            }
            
            if (bDataPropChange && LoggerPropChange != null)
                LoggerPropChange(m_Logger);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PropertiesControlValidating(object sender, CancelEventArgs e)
        {
            if (!ValidateValues())
                e.Cancel = true;
        }
        #endregion

        #region Gestion de la listview data
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        public void InitListViewData()
        {
            m_ListViewData.Items.Clear();
            if (this.Logger == null)
                return;
            StringCollection Lst = this.Logger.LoggerDatas;
            if (Lst != null)
            {
                for (int i = 0; i < Lst.Count; i++)
                {
                    string dtsymb = Lst[i];
                    Data dt = (Data)Doc.GestData.GetFromSymbol(dtsymb);
                    if (dt == null)
                    {
                        System.Diagnostics.Debug.Assert(false);
                        continue;
                    }

                    ListViewItem lviData = new ListViewItem(dt.Symbol);
                    lviData.Tag = dt;
                    BaseGestGroup.Group gr = Doc.GestData.GetGroupFromObject(dt);
                    if (gr != null)
                    {
                        lviData.BackColor = gr.m_GroupColor;
                    }
                    lviData.SubItems.Add(dt.SizeInBits.ToString());
                    lviData.SubItems.Add(dt.IsConstant.ToString());
                    lviData.SubItems.Add(dt.DefaultValue.ToString());
                    m_ListViewData.Items.Add(lviData);
                }
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        public void UpdateListViewData()
        {
            for (int i = 0; i < m_ListViewData.Items.Count; i++)
            {
                ListViewItem lviData = m_ListViewData.Items[i];
                Data dt = (Data)lviData.Tag;
                lviData.Text = dt.Symbol;
                BaseGestGroup.Group gr = Doc.GestData.GetGroupFromObject(dt);
                if (gr != null)
                {
                    lviData.BackColor = gr.m_GroupColor;
                }
                lviData.SubItems[1].Text = dt.SizeInBits.ToString();
                lviData.SubItems[2].Text = dt.IsConstant.ToString();
                lviData.SubItems[3].Text = dt.DefaultValue.ToString();

            }
        }

        #endregion

        #region handlers d'events
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void OnListViewDataKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete
                || e.KeyCode == Keys.Back)
            {
                ListViewItem lviData = null;
                if (m_ListViewData.SelectedItems.Count > 0)
                    lviData = m_ListViewData.SelectedItems[0];
                if (lviData != null)
                {
                    m_ListViewData.Items.Remove(lviData);
                    UpdateLoggerDataListFromListView();
                }
            }
        }
        #endregion

        #region update divers
        /// <summary>
        /// 
        /// </summary>
        private void UpdateLoggerDataListFromListView()
        {
            if (this.Logger == null)
                return;

            this.Logger.LoggerDatas.Clear();
            for (int i = 0; i < m_ListViewData.Items.Count; i++)
            {
                this.Logger.LoggerDatas.Add(m_ListViewData.Items[i].Text);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public void UpdateFromLogType()
        {
            if (this.LogType == LOGGER_TYPE.AUTO.ToString())
            {
                m_LoggerPeriod.Enabled = true;
                m_chkAutoStart.Enabled = true;
            }
            else
            {
                m_LoggerPeriod.Enabled = false;
                m_chkAutoStart.Enabled = false;
                m_chkAutoStart.Checked = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void UpdateFormatEdtFromMode()
        {
            switch(this.LoggerMode)
            {
                default : 
                    edtDateFormat.Enabled = false;
                    lblFormatHelp.Text = "";
                    break;
                case Logger.LogMode.none:
                    edtDateFormat.Enabled = false;
                    lblFormatHelp.Text= "";
                    break;
                case Logger.LogMode.autoNum:
                    edtDateFormat.Enabled = false;
                    lblFormatHelp.Text= 
                    Program.LangSys.C(@"Add automaticaly an auto incremented number to the file name.")+ "\n" +
                    Program.LangSys.C(@"The choosen number will be the first available (no existing file) from 1 to N");
                    break;
                case Logger.LogMode.autoDated:
                    edtDateFormat.Enabled = true;
                    lblFormatHelp.Text= 
                    Program.LangSys.C(@"Add automaticaly date and time according to the specified format string.") + "\n" +
                    Program.LangSys.C(@"YYYY : years, MM : months, dd : days, HH : hours (24h format), hh : hours (12h format) mm : minutes, ss : seconds, tt : PM/AM");
                    break;
            }
        }

        #endregion

        #region gestion du drag drop dans la liste des données de la trame
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnListViewDataDragEnter(object sender, DragEventArgs e)
        {
            Data dt = (Data)e.Data.GetData(typeof(Data));
            if (this.Logger != null && dt != null)
            {
                bool bFind = false;
                for (int i = 0; i < m_ListViewData.Items.Count; i++)
                {
                    if (m_ListViewData.Items[i].Text == dt.Symbol)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnListViewDataItemDrag(object sender, ItemDragEventArgs e)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnListViewDataDragDrop(object sender, DragEventArgs e)
        {
            Data DropedItem = (Data)e.Data.GetData(typeof(Data));
            if (DropedItem != null)
            {
                //this.AssociateData = DropedItem.Symbol;
                bool bFind = false;
                ListViewItem LvFoundItem = null;
                for (int i = 0; i < m_ListViewData.Items.Count; i++)
                {
                    if (m_ListViewData.Items[i].Text == DropedItem.Symbol)
                    {
                        bFind = true;
                        LvFoundItem = m_ListViewData.Items[i];
                        break;
                    }
                }

                if (bFind)
                {
                    Point prCurInListView = m_ListViewData.PointToClient(new Point(e.X, e.Y));
                    ListViewItem lviAtCursor = m_ListViewData.GetItemAt(prCurInListView.X, prCurInListView.Y);
                    if (lviAtCursor != LvFoundItem)
                    {
                        m_ListViewData.Items.Remove(LvFoundItem);
                        if (lviAtCursor != null)
                            m_ListViewData.Items.Insert(lviAtCursor.Index, LvFoundItem);
                        else
                            m_ListViewData.Items.Add(LvFoundItem);
                    }
                }
                else
                {
                    Point prCurInListView = m_ListViewData.PointToClient(new Point(e.X, e.Y));
                    ListViewItem lviAtCursor = m_ListViewData.GetItemAt(prCurInListView.X, prCurInListView.Y);

                    ListViewItem lviData = new ListViewItem(DropedItem.Symbol);
                    lviData.Tag = DropedItem;
                    lviData.SubItems.Add(DropedItem.SizeInBits.ToString());
                    lviData.SubItems.Add(DropedItem.IsConstant.ToString());
                    lviData.SubItems.Add(DropedItem.DefaultValue.ToString());
                    //m_ListViewData.Items.Add(lviData);

                    if (lviAtCursor != null)
                        m_ListViewData.Items.Insert(lviAtCursor.Index, lviData);
                    else
                        m_ListViewData.Items.Add(lviData);
                }
            }
            UpdateLoggerDataListFromListView();
            UpdateListViewData();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnListViewDataDragOver(object sender, DragEventArgs e)
        {
            Data DropItem = (Data)e.Data.GetData(typeof(Data));
            if (DropItem != null)
            {
                Point prCurInListView = m_ListViewData.PointToClient(new Point(e.X, e.Y));
                ListViewItem lviAtCursor = m_ListViewData.GetItemAt(prCurInListView.X, prCurInListView.Y);
                for (int i = 0; i < m_ListViewData.Items.Count; i++)
                {
                    if (m_ListViewData.Items[i] != lviAtCursor)
                    {
                        BaseGestGroup.Group gr = Doc.GestData.GetGroupFromObject((BaseObject)m_ListViewData.Items[i].Tag);
                        if (gr != null)
                            m_ListViewData.Items[i].BackColor = gr.m_GroupColor;
                        else
                            m_ListViewData.Items[i].BackColor = m_ListViewData.BackColor;
                    }
                }
                if (lviAtCursor != null)
                    lviAtCursor.BackColor = Color.Gray;
            }
        }

        private void OnListViewDataDragLeave(object sender, EventArgs e)
        {
            UpdateListViewData();
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_cboLogType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFromLogType();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_cboNaming_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFormatEdtFromMode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddData_Click(object sender, EventArgs e)
        {
            PickDataForm PickData = new PickDataForm();
            PickData.AllowMultipleSelect = true;
            PickData.Document = this.Doc;
            if (PickData.ShowDialog() == DialogResult.OK)
            {
                if (PickData.SelectedDatas != null)
                {
                    for (int i = 0; i < PickData.SelectedDatas.Length; i++)
                    {
                        if (PickData.SelectedDatas[i] != null)
                        {
                            Data dt = PickData.SelectedDatas[i];
                            ListViewItem lviData = new ListViewItem(dt.Symbol);
                            m_ListViewData.Items.Add(lviData);
                        }
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CommonLib
{
    public partial class BasePropertiesDialog : Form
    {
        BTDoc m_Document = null;
        BaseObject m_baseObjectItem = null;

        StandardObjectPropPanel m_stdPropPanel = new StandardObjectPropPanel();

        List<IObjectPropertyPanel> m_listPropsPanels = new List<IObjectPropertyPanel>();
        List<string> m_listTitle = new List<string>();

        public event EventHandler ObjectPropertiesChanged;

        BTScreen m_CurrentScreen;
        /// <summary>
        /// 
        /// </summary>
        public BasePropertiesDialog()
        {
            InitializeComponent();
            if (Lang.LangSys != null)
                Lang.LangSys.Initialize(this);
        }

        /// <summary>
        /// 
        /// </summary>
        public BaseObject ConfiguredItem 
        {
            get { return m_baseObjectItem; }
            set 
            { 
                m_baseObjectItem = value;
                this.tabControl1.Enabled = m_baseObjectItem == null ? false : true;
                this.btnOK.Enabled = m_baseObjectItem == null ? false : true;
                if (m_baseObjectItem == null)
                {
                    Initialize();
                }
            } 
        }

        public BTScreen CurrentScreen
        {
            get { return m_CurrentScreen; }
            set { m_CurrentScreen = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public BTDoc Document
        {
            get { return m_Document; }
            set { m_Document = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public void Initialize()
        {
            m_listPropsPanels.Clear();
            m_listTitle.Clear();
            tabControl1.SuspendLayout();
            tabControl1.Visible = false;
            tabControl1.TabPages.Clear();
            m_listPropsPanels.Add(m_stdPropPanel);
            m_listTitle.Add(Lang.LangSys.C("Symbol & Description"));
            if (m_baseObjectItem != null)
            {
                m_stdPropPanel.Enabled = true;
                this.Text = Lang.LangSys.C("Properties of ") + m_baseObjectItem.Symbol;
                if (m_baseObjectItem != null && m_Document != null)
                {
                    if (m_baseObjectItem.StdConfigPanel != null)
                    {
                        m_listPropsPanels.Add(m_baseObjectItem.StdConfigPanel as IObjectPropertyPanel);
                        m_listTitle.Add(Lang.LangSys.C("Properties"));
                    }
                    if (m_baseObjectItem is IScriptable)
                    {
                        IScriptable obj = m_baseObjectItem as IScriptable;
                        foreach (string scriptType in obj.ItemScripts.ScriptKeys)
                        {
                            BTControl objCtrl = m_baseObjectItem as BTControl;
                            if (objCtrl != null)
                            {
                                ISpecificControl ctrl = objCtrl.IControl as ISpecificControl;
                                if (ctrl != null
                                    && scriptType == "EvtScript"
                                    && ctrl.StdPropEnabling.m_bCtrlEventScriptEnabled == false)
                                    continue;
                            }
                            ScriptEditorControl editor = new ScriptEditorControl();
                            editor.ScriptType = scriptType;
                            m_listPropsPanels.Add(editor as IObjectPropertyPanel);
                            m_listTitle.Add(Lang.LangSys.C(scriptType));
                        }
                    }

                    for (int i = 0; i < m_listPropsPanels.Count; i++)
                    {
                        m_listPropsPanels[i].Document = this.Document;
                        m_listPropsPanels[i].ConfiguredItem = m_baseObjectItem;
                        if (m_baseObjectItem is Data)
                        {
                            m_listPropsPanels[i].ConfiguredItemGest = m_Document.GestData;
                        }
                        else if (m_baseObjectItem is BTScreen)
                        {
                            m_listPropsPanels[i].ConfiguredItemGest = m_Document.GestScreen;
                        }
                        else if (m_baseObjectItem is Function)
                        {
                            m_listPropsPanels[i].ConfiguredItemGest = m_Document.GestFunction;
                        }
                        else if (m_baseObjectItem is BTTimer)
                        {
                            m_listPropsPanels[i].ConfiguredItemGest = m_Document.GestTimer;
                        }
                        else if (m_baseObjectItem is Trame)
                        {
                            m_listPropsPanels[i].ConfiguredItemGest = m_Document.GestTrame;
                        }
                        else if (m_baseObjectItem is Logger)
                        {
                            m_listPropsPanels[i].ConfiguredItemGest = m_Document.GestLogger;
                        }
                        else if (m_baseObjectItem is BTControl)
                        {
                            if (m_CurrentScreen != null)
                            {
                                m_listPropsPanels[i].ConfiguredItemGest = m_CurrentScreen.Controls;
                            }
                            //System.Diagnostics.Debug.Assert(false); // TODO
                            //m_listPropsPanels[i].ConfiguredItemGest = m_Document.GestScreen.GetFromsymbol(screenSymb).GestControl;
                            // ici il faut savoir dans quel écran on se trouve
                        }

                        AddPropertyTab(m_listTitle[i], m_listPropsPanels[i]);
                        m_listPropsPanels[i].ObjectToPanel();
                    }
                    //
                    if (this.tabControl1.TabPages.Count >= 2)
                    {
                        this.tabControl1.SelectedIndex = 1;
                    }
                }
            }
            else
            {
                this.Text = Lang.LangSys.C("Selection is empty");
                m_stdPropPanel.Enabled = false;
                m_stdPropPanel.ConfiguredItem = null;
                m_stdPropPanel.ObjectToPanel();
                AddPropertyTab(m_listTitle[0], m_listPropsPanels[0]);
            }
            tabControl1.ResumeLayout();
            tabControl1.Visible = true;
        }

        private void AddPropertyTab(string title, IObjectPropertyPanel panel)
        {
            this.tabControl1.TabPages.Add(title);
            TabPage newPage = this.tabControl1.TabPages[this.tabControl1.TabPages.Count - 1];
            newPage.SuspendLayout();
            newPage.AutoScroll = true;
            panel.Panel.Location = new Point(0, 0);
            panel.Panel.Dock = DockStyle.Fill;
            newPage.Controls.Add(panel.Panel);
            newPage.ResumeLayout();
        }

        bool ValidateProperties()
        {
            bool retVal = true;
            foreach (IObjectPropertyPanel panel in m_listPropsPanels)
            {
                if (!panel.ValidateProperties())
                {
                    retVal = false;
                    for (int i = 0; i < tabControl1.TabPages.Count; i++ )
                    {
                        if (tabControl1.TabPages[i].Controls.Contains(panel as Control))
                            tabControl1.SelectedIndex = i;
                    }
                    break;
                }
            }
            return retVal;
        }



        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidateProperties())
            {
                foreach (IObjectPropertyPanel panel in m_listPropsPanels)
                {
                    panel.PanelToObject();
                }
                m_baseObjectItem.NotifyPropertiesChanged();
                if (ObjectPropertiesChanged != null)
                    ObjectPropertiesChanged(this, new EventArgs());
                //this.DialogResult = DialogResult.OK;
            }
        }

        private void BasePropertiesDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.Hide();
                e.Cancel = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            foreach (IObjectPropertyPanel pan in m_listPropsPanels)
            {
                pan.ObjectToPanel();
            }
        }

        private void BasePropertiesDialog_Enter(object sender, EventArgs e)
        {
            this.Opacity = 1;
        }

        private void BasePropertiesDialog_Leave(object sender, EventArgs e)
        {
            this.Opacity = 0.85;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}

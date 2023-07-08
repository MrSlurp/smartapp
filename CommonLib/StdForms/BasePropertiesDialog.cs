/*
    This file is part of SmartApp.
    Copyright (C) 2007-2016  Pascal Bigot

    SmartApp is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    SmartApp is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with Foobar.  If not, see <http://www.gnu.org/licenses/>.
*/
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
        BaseObject m_baseObjectItemMem = null;

        StandardObjectPropPanel m_stdPropPanel = new StandardObjectPropPanel();

        List<IObjectPropertyPanel> m_listPropsPanels = new List<IObjectPropertyPanel>();
        List<string> m_listTitle = new List<string>();

        public event EventHandler ObjectPropertiesChanged;

        BTScreen m_CurrentScreen;

        int m_iPageIndexMemory = 0;
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
            bool bSkipPageRebuild = false;
            if (m_baseObjectItem == null || m_baseObjectItemMem != m_baseObjectItem)
            {
                // on évite le rebuild complet si c'est le même objet, ou si il est du même type
                if (m_baseObjectItemMem != null &&
                    m_baseObjectItem != null && 
                    m_baseObjectItemMem.GetType() == m_baseObjectItem.GetType())
                    bSkipPageRebuild = true;

                m_baseObjectItemMem = m_baseObjectItem;
            }
            else
            {
                bSkipPageRebuild = true;
                Traces.LogAddDebug(TraceCat.SmartConfig, "Config page rebuild skipped");
            }
            // on fait la mémo que si on a plus d'une page
            if (this.tabControl1.TabPages.Count > 1)
                m_iPageIndexMemory = this.tabControl1.SelectedIndex;
            else
                m_iPageIndexMemory = -1;

            if (!bSkipPageRebuild)
            {
                // on reconstruit l'ensemble des pages parce que le type d'objet à changé
                m_listPropsPanels.Clear();
                m_listTitle.Clear();
                tabControl1.SuspendLayout();
                tabControl1.Visible = false;
                tabControl1.TabPages.Clear();
                // d'abord le panel de base
                m_listPropsPanels.Add(m_stdPropPanel);
                m_listTitle.Add(Lang.LangSys.C("Symbol & Description"));

                if (m_baseObjectItem != null)
                {
                    // si il y a un panneau de configuration standard sur l'objet, on l'ajoute
                    if (m_baseObjectItem.StdConfigPanel != null)
                    {
                        m_listPropsPanels.Add(m_baseObjectItem.StdConfigPanel as IObjectPropertyPanel);
                        m_listTitle.Add(Lang.LangSys.C("Properties"));
                    }
                    // on fait la liste de tout les script et on ajoute un onglet par script
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
                    // on finir par toues les ajouter 
                    for (int i = 0; i < m_listPropsPanels.Count; i++)
                    {
                        AddPropertyTab(m_listTitle[i], m_listPropsPanels[i]);
                    }
                }
                else
                {
                    // l'objet est null, donc la selection est vide, on met juste le panel de base
                    AddPropertyTab(m_listTitle[0], m_listPropsPanels[0]);
                }
            }
            // on fait le parcours de tout les onglet pour les initialiser
            if (m_baseObjectItem != null && m_Document != null)
            {
                m_stdPropPanel.Enabled = true;
                this.Text = Lang.LangSys.C("Properties of ") + m_baseObjectItem.Symbol;
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
                    }
                    m_listPropsPanels[i].ObjectToPanel();
                }
            }
            else
            {
                this.Text = Lang.LangSys.C("Selection is empty");
                m_stdPropPanel.Enabled = false;
                m_stdPropPanel.ConfiguredItem = null;
            }

            tabControl1.ResumeLayout();
            tabControl1.Visible = true;
            if (m_iPageIndexMemory != -1 && m_iPageIndexMemory < tabControl1.TabPages.Count)
                tabControl1.SelectedIndex = m_iPageIndexMemory;
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

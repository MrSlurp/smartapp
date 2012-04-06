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

        BTScreen m_CurrentScreen;
        /// <summary>
        /// 
        /// </summary>
        public BasePropertiesDialog()
        {
            InitializeComponent();
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
            tabControl1.TabPages.Clear();
            if (m_baseObjectItem != null)
            {
                m_listPropsPanels.Add(m_stdPropPanel);
                m_listTitle.Add(Lang.LangSys.C("Symbol & Description"));
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
                            ScriptEditorControl editor = new ScriptEditorControl();
                            editor.ScriptType = scriptType;
                            m_listPropsPanels.Add(editor as IObjectPropertyPanel);
                            m_listTitle.Add(Lang.LangSys.C(scriptType));
                        }
                    }

                    for (int i = 0; i < m_listPropsPanels.Count; i++)
                    {
                        m_listPropsPanels[i].ConfiguredItem = m_baseObjectItem;
                        m_listPropsPanels[i].Document = this.Document;
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
                }
                // Ajouter tous les panels nécessaires au paramétrage de l'objet
                // et leur demander de s'intialiser
            }
        }

        private void AddPropertyTab(string title, IObjectPropertyPanel panel)
        {
            this.tabControl1.TabPages.Add(title);
            panel.Panel.Location = new Point(0, 0);
            panel.Panel.Dock = DockStyle.Fill;
            this.tabControl1.TabPages[this.tabControl1.TabPages.Count - 1].SuspendLayout();
            this.tabControl1.TabPages[this.tabControl1.TabPages.Count - 1].Controls.Add(panel.Panel);
            this.tabControl1.TabPages[this.tabControl1.TabPages.Count - 1].ResumeLayout();
        }

        bool ValidateProperties()
        {
            bool retVal = true;
            foreach (IObjectPropertyPanel panel in m_listPropsPanels)
            {
                if (!panel.ValidateProperties())
                {
                    retVal = false;
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
                this.Close();
            }
        }
    


        
    }
}

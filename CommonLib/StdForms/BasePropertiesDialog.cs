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

        List<IObjectPropertyPanel> m_listPropsPanels = new List<IObjectPropertyPanel>();

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
        BaseObject ConfiguredItem 
        {
            get { return m_baseObjectItem; }
            set { m_baseObjectItem = value; } 
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
            m_listPropsPanels.Add(this.standardObjectPropPanel1);
            this.standardObjectPropPanel1.ConfiguredItem = m_baseObjectItem;
            if (m_baseObjectItem != null && m_Document != null)
            {
                if (m_baseObjectItem is Data)
                {
                    this.standardObjectPropPanel1.ConfiguredItemGest = m_Document.GestData;
                }
                else if (m_baseObjectItem is BTScreen)
                {
                    this.standardObjectPropPanel1.ConfiguredItemGest = m_Document.GestScreen;
                }
                else if (m_baseObjectItem is Function)
                {
                    this.standardObjectPropPanel1.ConfiguredItemGest = m_Document.GestFunction;
                }
                else if (m_baseObjectItem is BTTimer)
                {
                    this.standardObjectPropPanel1.ConfiguredItemGest = m_Document.GestTimer;
                }
                else if (m_baseObjectItem is Trame)
                {
                    this.standardObjectPropPanel1.ConfiguredItemGest = m_Document.GestTrame;
                }
                else if (m_baseObjectItem is Logger)
                {
                    this.standardObjectPropPanel1.ConfiguredItemGest = m_Document.GestLogger;
                }
                else if (m_baseObjectItem is BTControl)
                {
                    System.Diagnostics.Debug.Assert(false); // TODO
                    //this.standardObjectPropPanel1.ConfiguredItemGest = m_Document.GestTrame;
                    // ici il faut savoir dans quel écran on se trouve
                }
                //
            }
            // Ajouter tous les panels nécessaires au paramétrage de l'objet
            // et leur demander de s'intialiser
        }

        private void AddPropertyTab(string title, IObjectPropertyPanel panel)
        {
            m_listPropsPanels.Add(panel);
            this.tabControl1.TabPages.Add(title);
            panel.Panel.Location = new Point(0, 0);
            panel.Panel.Dock = DockStyle.Fill;
            this.tabControl1.TabPages[this.tabControl1.TabPages.Count - 1].SuspendLayout();
            this.tabControl1.TabPages[this.tabControl1.TabPages.Count - 1].Controls.Add(panel.Panel);
            this.tabControl1.TabPages[this.tabControl1.TabPages.Count - 1].ResumeLayout();
        }

        bool ValidateProperties()
        {
            foreach (IObjectPropertyPanel panel in m_listPropsPanels)
            {
                if (panel.ValidateProperties())
                {
                    panel.PanelToObject();
                }
            }
            return true;
        }
    


        
    }
}

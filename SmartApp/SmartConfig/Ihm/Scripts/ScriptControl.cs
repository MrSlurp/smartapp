using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CommonLib;
using SmartApp.Scripts;

namespace SmartApp.Ihm
{
    public partial class ScriptControl : UserControl
    {
        IScriptable m_ScriptableItem;
        IInitScriptable m_InitScriptableItem;

        private BTDoc m_Document = null;

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public ScriptControl()
        {
            InitializeComponent();
        }

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
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public IScriptable ScriptableItem
        {
            get
            {
                return m_ScriptableItem;
            }
            set
            {
                m_ScriptableItem = value;
                m_InitScriptableItem = null;
                if (m_ScriptableItem != null)
                {
                    this.ScriptLines = m_ScriptableItem.ScriptLines;
                    this.Enabled = true;
                    m_BtnEditScript.Enabled = true;
                }
                else
                {
                    this.ScriptLines = null;
                    this.Enabled = false;
                    m_BtnEditScript.Enabled = false;
                }
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public IInitScriptable InitScriptableItem
        {
            get
            {
                return m_InitScriptableItem;
            }
            set
            {
                m_InitScriptableItem = value;
                m_ScriptableItem = null;
                if (m_InitScriptableItem != null)
                {
                    this.ScriptLines = m_InitScriptableItem.InitScriptLines;
                    this.Enabled = true;
                    m_BtnEditScript.Enabled = true;
                }
                else
                {
                    this.ScriptLines = null;
                    this.Enabled = false;
                    m_BtnEditScript.Enabled = false;
                }

            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public string Title
        {
            get
            {
                return m_labelTitle.Text;
            }
            set
            {
                m_labelTitle.Text = value;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public string[] ScriptLines
        {
            get
            {
                return m_TextScript.Lines;
            }
            set
            {
                if (value != null)
                    m_TextScript.Lines = value;
                else
                {
                    string[] strTabTmp = new string[1];
                    strTabTmp[0] = string.Empty;
                    m_TextScript.Lines = strTabTmp;
                }
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void m_BtnEditScript_Click(object sender, EventArgs e)
        {
            string[] script;
            if (m_ScriptableItem != null)
            {
                script = m_ScriptableItem.ScriptLines;
            }
            else if (m_InitScriptableItem != null)
            {
                script = m_InitScriptableItem.InitScriptLines;
            }
            else 
                return;

            ScriptEditordialog DlgScript = new ScriptEditordialog();
            DlgScript.Doc = this.m_Document;
            DlgScript.ScriptLines = script;
            DialogResult dlgRes = DlgScript.ShowDialog();
            if (dlgRes == DialogResult.OK)
            {
                script = DlgScript.ScriptLines;
                script = DlgScript.ScriptLines;
                if (m_ScriptableItem != null)
                {
                    m_ScriptableItem.ScriptLines = script;
                }
                else if (m_InitScriptableItem != null)
                {
                    m_InitScriptableItem.InitScriptLines = script;
                }
                else
                    return;

                this.ScriptLines = script;
            }
        }
    }
}

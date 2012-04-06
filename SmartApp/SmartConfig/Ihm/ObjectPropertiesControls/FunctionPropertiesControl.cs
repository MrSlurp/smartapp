using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CommonLib;

namespace SmartApp.Ihm
{
    public partial class FunctionPropertiesControl : UserControl
    {
        #region données membres
        private Function m_Function = null;
        private BTDoc m_Document = null;
        #endregion

        #region Events
        public event FunctionPropertiesChange FunctionPropChange;
        #endregion

        #region attributs de la classe
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
        public Function Function
        {
            get
            {
                return m_Function;
            }
            set
            {
                m_Function = value;
                if (m_Function != null)
                {
                    this.Enabled = true;
                    this.Description = m_Function.Description;
                    this.Symbol = m_Function.Symbol;
                    this.ScriptLines = m_Function.ItemScripts["FuncScript"];
                }
                else
                {
                    this.Description = "";
                    this.Symbol = "";
                    this.ScriptLines = null;
                    this.Enabled = false;
                }

            }
        }
        #endregion

        #region constructeur
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public FunctionPropertiesControl()
        {
            InitializeComponent();
        }
        #endregion

        #region attribut d'accès aux valeurs de la page de propriété
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
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

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
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

        public string[] ScriptLines
        {
            get
            {
                return m_EditScript.Lines;
            }
            set
            {
                if (value != null)
                    m_EditScript.Lines = value;
                else
                {
                    string[] strTabTmp = new string[1];
                    strTabTmp[0] = string.Empty;
                    m_EditScript.Lines = strTabTmp;
                }
            }
        }
        #endregion

        #region validation des données
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public bool IsDataValuesValid
        {
            get
            {
                if (this.Function == null)
                    return true;

                if (string.IsNullOrEmpty(this.Symbol))
                    return false;

                bool bRet = true;
                Function dt = (Function)this.GestFunction.GetFromSymbol(this.Symbol);
                if (dt != null && dt != this.Function)
                    bRet = false;

                return bRet;
            }
        }


        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public bool ValidateValues()
        {
            if (this.Function == null)
                return true;

            bool bRet = true;
            string strMessage = "";
            if (string.IsNullOrEmpty(this.Symbol))
            {
                strMessage = Program.LangSys.C("Symbol must not be empty");
                bRet = false;
            }
            Function Sc = (Function)GestFunction.GetFromSymbol(this.Symbol);
            if (bRet && Sc != null && Sc != this.Function)
            {
                strMessage = string.Format(Program.LangSys.C("A Function with symbol {0} already exist"), Symbol);
                bRet = false;
            }
            if (!bRet)
            {
                MessageBox.Show(strMessage, Program.LangSys.C("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return bRet;
            }
            
            bool bDataPropChange = false;
            if (m_Function.Description != this.Description)
                bDataPropChange |= true;
            if (m_Function.Symbol != this.Symbol)
                bDataPropChange |= true;

            if (bDataPropChange)
            {
                m_Function.Description = this.Description;
                m_Function.Symbol = this.Symbol;
                Doc.Modified = true;
            }
            
            if (bDataPropChange && FunctionPropChange != null)
                FunctionPropChange(m_Function);
            return true;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void PropertiesControlValidating(object sender, CancelEventArgs e)
        {
            if (!ValidateValues())
                e.Cancel = true;
        }
        #endregion

        #region edition du script
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void OnBtnEditScriptClick(object sender, EventArgs e)
        {
            ScriptEditordialog DlgScript = new ScriptEditordialog();
            DlgScript.Doc = this.m_Document;
            DlgScript.ScriptLines = m_Function.ItemScripts["FuncScript"];
            DialogResult dlgRes = DlgScript.ShowDialog();
            if (dlgRes == DialogResult.OK)
            {
                m_Function.ItemScripts["FuncScript"] = DlgScript.ScriptLines;
                this.ScriptLines = DlgScript.ScriptLines;
            }
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CommonLib;

namespace SmartApp.Ihm
{
    public partial class TimerPropertiesControl : UserControl
    {
        #region données membres
        private BTTimer m_Timer = null;
        private BTDoc m_Document = null;
        #endregion

        #region Events
        public event TimerPropertiesChange TimerPropChange;
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
        public GestTimer GestTimer
        {
            get
            {
                return m_Document.GestTimer;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public BTTimer Timer
        {
            get
            {
                return m_Timer;
            }
            set
            {
                m_Timer = value;
                if (m_Timer != null)
                {
                    this.Enabled = true;
                    this.Description = m_Timer.Description;
                    this.Symbol = m_Timer.Symbol;
                    this.ScriptLines = m_Timer.ItemScripts["TimerScript"];
                    this.Period = m_Timer.Period;
                    this.AutoStart = m_Timer.AutoStart;
                }
                else
                {
                    this.Description = "";
                    this.Symbol = "";
                    this.ScriptLines = null;
                    this.Period = 50;
                    this.Enabled = false;
                    this.AutoStart = false;
                }

            }
        }
        #endregion

        #region constructeur
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public TimerPropertiesControl()
        {
            InitializeComponent();
            m_NumPeriod.Minimum = 20;
            m_NumPeriod.Maximum = 3600000;
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
                if (value != null)
                    m_richTextDesc.Text = value;
                else
                    m_richTextDesc.Text = string.Empty;
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
                if (value != null)
                    m_textSymbol.Text = value;
                else
                    m_textSymbol.Text = string.Empty;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public int Period
        {
            get
            {
                return (int)m_NumPeriod.Value;
            }
            set
            {
                m_NumPeriod.Value = value;
            }
        }

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


        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
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
                if (this.Timer == null)
                    return true;

                if (string.IsNullOrEmpty(this.Symbol))
                    return false;
                bool bRet = true;
                BTTimer dt = (BTTimer)this.GestTimer.GetFromSymbol(this.Symbol);
                if (dt != null && dt != this.Timer)
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
            if (this.Timer == null)
                return true;

            bool bRet = true;
            string strMessage = "";
            if (string.IsNullOrEmpty(this.Symbol))
            {
                strMessage = Program.LangSys.C("Symbol must not be empty");
                bRet = false;
            }
            BTTimer Sc = (BTTimer)GestTimer.GetFromSymbol(this.Symbol);
            if (bRet && Sc != null && Sc != this.Timer)
            {
                strMessage = string.Format(Program.LangSys.C("A timer with symbol {0} already exist"), Symbol);
                bRet = false;
            }
            if (!bRet)
            {
                MessageBox.Show(strMessage, Program.LangSys.C("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return bRet;
            }

            bool bDataPropChange = false;
            if (m_Timer.Description != this.Description)
                bDataPropChange |= true;
            if (m_Timer.Symbol != this.Symbol)
                bDataPropChange |= true;
            if (m_Timer.Period != this.Period)
                bDataPropChange |= true;

            if (m_Timer.AutoStart != this.AutoStart)
                bDataPropChange |= true;

            if (bDataPropChange)
            {
                m_Timer.Description = this.Description;
                m_Timer.Symbol = this.Symbol;
                m_Timer.Period = this.Period;
                m_Timer.AutoStart = this.AutoStart;
                Doc.Modified = true;
            }
            if (bDataPropChange && TimerPropChange != null)
                TimerPropChange(m_Timer);
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
            DlgScript.ScriptLines = m_Timer.ItemScripts["TimerScript"];
            DialogResult dlgRes = DlgScript.ShowDialog();
            if (dlgRes == DialogResult.OK)
            {
                m_Timer.ItemScripts["TimerScript"] = DlgScript.ScriptLines;
                this.ScriptLines = DlgScript.ScriptLines;
            }
        }
        #endregion
    }
}

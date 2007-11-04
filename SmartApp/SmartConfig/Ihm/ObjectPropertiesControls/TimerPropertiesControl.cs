using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SmartApp.Datas;
using SmartApp.Gestionnaires;
using SmartApp.Scripts;

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
                    this.ScriptLines = m_Timer.ScriptLines;
                    this.Period = m_Timer.Period;
                }
                else
                {
                    this.Description = "";
                    this.Symbol = "";
                    this.ScriptLines = null;
                    this.Period = 50;
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
        public TimerPropertiesControl()
        {
            InitializeComponent();
            m_NumPeriod.Minimum = 50;
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
                m_EditScript.Lines = value;
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
                strMessage = "Symbol must not be empty";
                bRet = false;
            }
            BTTimer Sc = (BTTimer)GestTimer.GetFromSymbol(this.Symbol);
            if (bRet && Sc != null && Sc != this.Timer)
            {
                strMessage = string.Format("A timer with symbol {0} already exist", Symbol);
                bRet = false;
            }
            if (!bRet)
            {
                MessageBox.Show(strMessage);
                return bRet;
            }

            bool bDataPropChange = false;
            if (m_Timer.Description != this.Description)
                bDataPropChange |= true;
            if (m_Timer.Symbol != this.Symbol)
                bDataPropChange |= true;
            if (m_Timer.Period != this.Period)
                bDataPropChange |= true;


            if (bDataPropChange)
            {
                m_Timer.Description = this.Description;
                m_Timer.Symbol = this.Symbol;
                m_Timer.Period = this.Period;
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
            DlgScript.ScriptLines = m_Timer.ScriptLines;
            DialogResult dlgRes = DlgScript.ShowDialog();
            if (dlgRes == DialogResult.OK)
            {
                m_Timer.ScriptLines = DlgScript.ScriptLines;
                this.ScriptLines = DlgScript.ScriptLines;
            }
        }
        #endregion
    }
}

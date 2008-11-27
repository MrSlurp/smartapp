using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.IO;
using System.Windows.Forms;
using CommonLib;

namespace FormatedDisplay
{
    public partial class FormatedDisplayProperties : UserControl, ISpecificPanel
    {
        BTControl m_Control = null;
        private BTDoc m_Document = null;
        private string m_FormatString = ":F0";

        #region events
        public event ControlPropertiesChange ControlPropertiesChanged;
        #endregion

        public FormatedDisplayProperties()
        {
            InitializeComponent();
        }

        public BTControl BTControl
        {
            get
            {
                return m_Control;
            }
            set
            {
                if (value != null && value.SpecificProp.GetType() == typeof(DllFormatedDisplayProp))
                    m_Control = value;
                else
                    m_Control = null;
                if (m_Control != null)
                {
                    this.Enabled = true;
                    m_FormatString = ((DllFormatedDisplayProp)m_Control.SpecificProp).FormatString;
                    switch (m_FormatString)
                    {
                        case ":F0":
                            m_rdBtn0.Checked = true;
                            m_rdBtn1.Checked = false;
                            m_rdBtn2.Checked = false;
                            m_rdBtn3.Checked = false;
                            break;
                        case ":F1":
                            m_rdBtn0.Checked = false;
                            m_rdBtn1.Checked = true;
                            m_rdBtn2.Checked = false;
                            m_rdBtn3.Checked = false;
                            break;
                        case ":F2":
                            m_rdBtn0.Checked = false;
                            m_rdBtn1.Checked = false;
                            m_rdBtn2.Checked = true;
                            m_rdBtn3.Checked = false;
                            break;
                        case ":F3":
                            m_rdBtn0.Checked = false;
                            m_rdBtn1.Checked = false;
                            m_rdBtn2.Checked = true;
                            m_rdBtn3.Checked = false;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    this.Enabled = false;
                    m_rdBtn0.Checked = true;
                    m_rdBtn1.Checked = false;
                    m_rdBtn2.Checked = false;
                    m_rdBtn3.Checked = false;
                }
            }
        }

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

        #region validation des données
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public bool IsDataValuesValid
        {
            get
            {
                if (this.BTControl == null)
                    return true;

                return true;
            }
        }


        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public bool ValidateValues()
        {
            if (this.BTControl == null)
                return true;

            bool bDataPropChange = false;

            // testez ici si les paramètres ont changé en les comparant avec ceux contenu dans les propriété
            // spécifiques du BTControl
            // si c'est le cas, assignez bDataPropChange à true;
            if (m_FormatString != ((DllFormatedDisplayProp)m_Control.SpecificProp).FormatString)
                bDataPropChange = true;
            if (bDataPropChange)
            {
                ((DllFormatedDisplayProp)m_Control.SpecificProp).FormatString = m_FormatString;
                Doc.Modified = true;
                m_Control.IControl.Refresh();
            }
            if (bDataPropChange && ControlPropertiesChanged != null)
                ControlPropertiesChanged(m_Control);
            return true;
        }
        #endregion

        private void m_rdBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (m_rdBtn0.Checked)
            {
                m_FormatString = ":F0";
            }
            else if (m_rdBtn1.Checked)
            {
                m_FormatString = ":F1";
            }
            else if (m_rdBtn2.Checked)
            {
                m_FormatString = ":F2";
            }
            else if (m_rdBtn3.Checked)
            {
                m_FormatString = ":F3";
            }
        }
    }
}

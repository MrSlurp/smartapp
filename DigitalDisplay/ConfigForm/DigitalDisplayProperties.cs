using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.IO;
using System.Windows.Forms;
using CommonLib;

namespace DigitalDisplay
{
    public partial class DigitalDisplayProperties : UserControl, ISpecificPanel
    {
        BTControl m_Control = null;
        private BTDoc m_Document = null;
        private string m_FormatString = ":F0";

        #region events
        public event ControlPropertiesChange ControlPropertiesChanged;
        #endregion

        public DigitalDisplayProperties()
        {
            DllEntryClass.LangSys.Initialize(this);
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
                if (value != null && value.SpecificProp.GetType() == typeof(DllDigitalDisplayProp))
                    m_Control = value;
                else
                    m_Control = null;
                if (m_Control != null)
                {
                    this.Enabled = true;
                    m_FormatString = ((DllDigitalDisplayProp)m_Control.SpecificProp).FormatString;
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
                    m_TextColor.BackColor = CtrlProp.DigitColor;
                    m_TextBackColor.BackColor = CtrlProp.BackColor;
                }
                else
                {
                    this.Enabled = false;
                    m_rdBtn0.Checked = true;
                    m_rdBtn1.Checked = false;
                    m_rdBtn2.Checked = false;
                    m_rdBtn3.Checked = false;
                    m_TextColor.BackColor = Color.GreenYellow;
                    m_TextBackColor.BackColor = Color.Black;
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

        private DllDigitalDisplayProp CtrlProp
        {
            get
            { return ((DllDigitalDisplayProp)m_Control.SpecificProp); }
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
            if (m_FormatString != CtrlProp.FormatString)
                bDataPropChange = true;
            if (m_TextColor.BackColor != CtrlProp.DigitColor)
                bDataPropChange = true;
            if (m_TextBackColor.BackColor != CtrlProp.BackColor)
                bDataPropChange = true;

            if (bDataPropChange)
            {
                ((DllDigitalDisplayProp)m_Control.SpecificProp).FormatString = m_FormatString;
                ((DllDigitalDisplayProp)m_Control.SpecificProp).DigitColor = m_TextColor.BackColor;
                ((DllDigitalDisplayProp)m_Control.SpecificProp).BackColor = m_TextBackColor.BackColor;
                Doc.Modified = true;
                m_Control.IControl.Refresh();
            }
            if (bDataPropChange && ControlPropertiesChanged != null)
                ControlPropertiesChanged(m_Control);
            return true;
        }
        #endregion

        private void m_BtnSelectColor_Click(object sender, EventArgs e)
        {
            m_clrDlg.Color = m_TextColor.BackColor;
            DialogResult DlgRes = m_clrDlg.ShowDialog();
            if (DlgRes == DialogResult.OK)
            {
                m_TextColor.BackColor = m_clrDlg.Color;
            }
        }

        private void m_BtnSelectBackColor_Click(object sender, EventArgs e)
        {
            m_clrDlg.Color = m_TextBackColor.BackColor;
            DialogResult DlgRes = m_clrDlg.ShowDialog();
            if (DlgRes == DialogResult.OK)
            {
                m_TextBackColor.BackColor = m_clrDlg.Color;
            }
        }


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

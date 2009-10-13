using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.IO;
using System.Windows.Forms;
using CommonLib;

namespace CtrlJauge
{
    public partial class CtrlJaugeProperties : UserControl, ISpecificPanel
    {
        BTControl m_Control = null;
        private BTDoc m_Document = null;

        private eOrientationJauge m_Orientation = eOrientationJauge.eHorizontaleDG;

        #region events
        public event ControlPropertiesChange ControlPropertiesChanged;
        #endregion

        public CtrlJaugeProperties()
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
                if (value != null && value.SpecificProp.GetType() == typeof(DllCtrlJaugeProp))
                    m_Control = value;
                else
                    m_Control = null;
                if (m_Control != null)
                {
                    this.Enabled = true;
                    m_Orientation = CtrlProp.Orientation;
                    switch (m_Orientation)
                    {
                        case eOrientationJauge.eHorizontaleDG:
                            m_rdBtn0.Checked = true;
                            m_rdBtn1.Checked = false;
                            m_rdBtn2.Checked = false;
                            m_rdBtn3.Checked = false;
                            break;
                        case eOrientationJauge.eHorizontaleGD:
                            m_rdBtn0.Checked = false;
                            m_rdBtn1.Checked = true;
                            m_rdBtn2.Checked = false;
                            m_rdBtn3.Checked = false;
                            break;
                        case eOrientationJauge.eVerticaleTB:
                            m_rdBtn0.Checked = false;
                            m_rdBtn1.Checked = false;
                            m_rdBtn2.Checked = false;
                            m_rdBtn3.Checked = true;
                            break;
                        case eOrientationJauge.eVerticaleBT:
                            m_rdBtn0.Checked = false;
                            m_rdBtn1.Checked = false;
                            m_rdBtn2.Checked = true;
                            m_rdBtn3.Checked = false;
                            break;
                        default:
                            break;
                    }
                    m_TextMinColor.BackColor = CtrlProp.ColorMin;
                    m_TextMaxColor.BackColor = CtrlProp.ColorMax;
                }
                else
                {
                    this.Enabled = false;
                    m_rdBtn0.Checked = true;
                    m_rdBtn1.Checked = false;
                    m_rdBtn2.Checked = false;
                    m_rdBtn3.Checked = false;
                    m_TextMinColor.BackColor = Color.Blue;
                    m_TextMaxColor.BackColor = Color.White;
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

        private DllCtrlJaugeProp CtrlProp
        {
            get
            { return ((DllCtrlJaugeProp)m_Control.SpecificProp); }
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
            if (CtrlProp.Orientation != m_Orientation)
                bDataPropChange = true;

            if (CtrlProp.ColorMin != m_TextMinColor.BackColor)
                bDataPropChange = true;

            if (CtrlProp.ColorMax != m_TextMaxColor.BackColor)
                bDataPropChange = true;

            if (bDataPropChange)
            {
                ((DllCtrlJaugeProp)m_Control.SpecificProp).ColorMin = m_TextMinColor.BackColor;
                ((DllCtrlJaugeProp)m_Control.SpecificProp).ColorMax = m_TextMaxColor.BackColor;
                ((DllCtrlJaugeProp)m_Control.SpecificProp).Orientation= m_Orientation;
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
                m_Orientation = eOrientationJauge.eHorizontaleDG;
            }
            else if (m_rdBtn1.Checked)
            {
                m_Orientation = eOrientationJauge.eHorizontaleGD;
            }
            else if (m_rdBtn2.Checked)
            {
                m_Orientation = eOrientationJauge.eVerticaleBT;
            }
            else if (m_rdBtn3.Checked)
            {
                m_Orientation = eOrientationJauge.eVerticaleTB;
            }
        }

        private void m_BtnSelectMinColor_Click(object sender, EventArgs e)
        {
            m_clrDlg.Color = m_TextMinColor.BackColor;
            DialogResult DlgRes = m_clrDlg.ShowDialog();
            if (DlgRes == DialogResult.OK)
            {
                m_TextMinColor.BackColor = m_clrDlg.Color;
            }
        }

        private void m_BtnSelecMaxColor_Click(object sender, EventArgs e)
        {
            m_clrDlg.Color = m_TextMaxColor.BackColor;
            DialogResult DlgRes = m_clrDlg.ShowDialog();
            if (DlgRes == DialogResult.OK)
            {
                m_TextMaxColor.BackColor = m_clrDlg.Color;
            }
        }
    }
}

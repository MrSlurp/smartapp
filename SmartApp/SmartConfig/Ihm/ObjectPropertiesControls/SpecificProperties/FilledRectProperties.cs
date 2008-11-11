using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SmartApp.Datas;
using SmartApp.Gestionnaires;
using SmartApp.Ihm.Designer;

namespace SmartApp.Ihm
{
    public partial class FilledRectProperties : UserControl
    {
        #region données membres
        private BTControl m_Control = null;

        private GestControl m_GestControl = null;

        private BTDoc m_Document = null;
        #endregion

        #region events
        public event ControlPropertiesChange ControlPropertiesChanged;
        #endregion

        public FilledRectProperties()
        {
            InitializeComponent();
        }

        #region attribut d'accès aux valeurs de la page de propriété
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public BTControl BTControl
        {
            get
            {
                return m_Control;
            }
            set
            {
                if (value.IControl.ControlType == InteractiveControlType.FilledRect)
                    m_Control = value;
                else
                    m_Control = null;
                if (m_Control != null)
                {
                    this.Visible = true;
                    this.Enabled = true;
                    m_TextActiveColor.BackColor = ((TwoColorProp)m_Control.SpecificProp).ColorActive;
                    m_TextInactiveColor.BackColor = ((TwoColorProp)m_Control.SpecificProp).ColorInactive;
                }
                else
                {
                    this.Visible = false;
                    this.Enabled = false;
                    m_TextActiveColor.BackColor = Color.White;
                    m_TextInactiveColor.BackColor = Color.White;
                }
                UpdateStateFromControlType();
            }
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
        public GestControl GestControl
        {
            get
            {
                return m_GestControl;
            }
            set
            {
                m_GestControl = value;
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

            if (bDataPropChange)
            {
                Doc.Modified = true;
            }
            if (bDataPropChange && ControlPropertiesChanged != null)
                ControlPropertiesChanged(m_Control);
            return true;
        }
        #endregion

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void UpdateStateFromControlType()
        {
            if (m_Control == null)
                return;

            switch (m_Control.IControl.ControlType)
            {
                case InteractiveControlType.Button:
                case InteractiveControlType.CheckBox:
                case InteractiveControlType.Combo:
                case InteractiveControlType.Text:
                case InteractiveControlType.NumericUpDown:
                case InteractiveControlType.Slider:
                    break;
                case InteractiveControlType.FilledRect:
                    break;
                default:
                    System.Diagnostics.Debug.Assert(false);
                    break;
            }
        }

        private void m_BtnSelectActiveColor_Click(object sender, EventArgs e)
        {
            m_clrDlg.Color = ((TwoColorProp)m_Control.SpecificProp).ColorActive;
            DialogResult DlgRes = m_clrDlg.ShowDialog();
            if (DlgRes == DialogResult.OK)
            {
                ((TwoColorProp)m_Control.SpecificProp).ColorActive = m_clrDlg.Color;
                m_TextActiveColor.BackColor = ((TwoColorProp)m_Control.SpecificProp).ColorActive;
            }
        }

        private void m_BtnSelectInactiveColor_Click(object sender, EventArgs e)
        {
            m_clrDlg.Color = ((TwoColorProp)m_Control.SpecificProp).ColorInactive;
            DialogResult DlgRes = m_clrDlg.ShowDialog();
            if (DlgRes == DialogResult.OK)
            {
                ((TwoColorProp)m_Control.SpecificProp).ColorInactive = m_clrDlg.Color;
                m_TextInactiveColor.BackColor = ((TwoColorProp)m_Control.SpecificProp).ColorInactive;
            }
        }
    }
}

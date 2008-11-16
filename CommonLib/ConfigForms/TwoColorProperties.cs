using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CommonLib
{
    public partial class TwoColorProperties : UserControl, ISpecificPanel
    {
        #region données membres
        private BTControl m_Control = null;
        private BTDoc m_Document = null;
        #endregion

        #region events
        public event ControlPropertiesChange ControlPropertiesChanged;
        #endregion

        public TwoColorProperties()
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
                if (value != null && value.SpecificProp.GetType() == typeof(TwoColorProp))
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
            if (m_TextActiveColor.BackColor != ((TwoColorProp)m_Control.SpecificProp).ColorActive)
                bDataPropChange = true;
            if (m_TextInactiveColor.BackColor != ((TwoColorProp)m_Control.SpecificProp).ColorInactive)
                bDataPropChange = true;

            if (bDataPropChange)
            {
                Doc.Modified = true;
                m_Control.IControl.Refresh();
            }
            if (bDataPropChange && ControlPropertiesChanged != null)
                ControlPropertiesChanged(m_Control);
            return true;
        }
        #endregion


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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CommonLib;

using SmartApp.Ihm.Designer;

namespace SmartApp.Ihm
{
    public partial class TwoColorProperties : BaseControlPropertiesPanel, ISpecificPanel
    {
        #region events
        public event ControlPropertiesChange ControlPropertiesChanged;
        #endregion

        public TwoColorProperties()
        {
            InitializeComponent();
        }

        #region validation des données

        public void ObjectToPanel()
        {
            m_TextActiveColor.BackColor = ((TwoColorProp)m_Control.SpecificProp).ColorActive;
            m_TextInactiveColor.BackColor = ((TwoColorProp)m_Control.SpecificProp).ColorInactive;
        }

        public void PanelToObject()
        {

            bool bPropChange = false;
            if (((TwoColorProp)m_Control.SpecificProp).ColorActive != m_TextActiveColor.BackColor)
                bPropChange = true;
            if (((TwoColorProp)m_Control.SpecificProp).ColorInactive != m_TextInactiveColor.BackColor)
                bPropChange = true;
            if (bPropChange)
            {
                ((TwoColorProp)m_Control.SpecificProp).ColorActive = m_TextActiveColor.BackColor;
                ((TwoColorProp)m_Control.SpecificProp).ColorInactive = m_TextInactiveColor.BackColor;

                if (ControlPropertiesChanged != null)
                    ControlPropertiesChanged(m_Control);
            }

        }

        #endregion


        private void m_BtnSelectActiveColor_Click(object sender, EventArgs e)
        {
            m_clrDlg.Color = ((TwoColorProp)m_Control.SpecificProp).ColorActive;
            DialogResult DlgRes = m_clrDlg.ShowDialog();
            if (DlgRes == DialogResult.OK)
            {
                m_TextActiveColor.BackColor = ((TwoColorProp)m_Control.SpecificProp).ColorActive;
            }
        }

        private void m_BtnSelectInactiveColor_Click(object sender, EventArgs e)
        {
            m_clrDlg.Color = ((TwoColorProp)m_Control.SpecificProp).ColorInactive;
            DialogResult DlgRes = m_clrDlg.ShowDialog();
            if (DlgRes == DialogResult.OK)
            {
                m_TextInactiveColor.BackColor = ((TwoColorProp)m_Control.SpecificProp).ColorInactive;
            }
        }
    }
}

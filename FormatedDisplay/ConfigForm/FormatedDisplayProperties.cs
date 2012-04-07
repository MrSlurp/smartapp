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
    public partial class FormatedDisplayProperties : BaseControlPropertiesPanel, ISpecificPanel
    {
        private string m_FormatString = ":F0";

        #region events
        public event ControlPropertiesChange ControlPropertiesChanged;
        #endregion

        public FormatedDisplayProperties()
        {
            DllEntryClass.LangSys.Initialize(this);
            InitializeComponent();
        }

        #region validation des données

        public void PanelToObject()
        {
            bool bDataPropChange = false;

            // testez ici si les paramètres ont changé en les comparant avec ceux contenu dans les propriété
            // spécifiques du BTControl
            // si c'est le cas, assignez bDataPropChange à true;
            if (m_FormatString != ((DllFormatedDisplayProp)m_Control.SpecificProp).FormatString)
                bDataPropChange = true;
            if (bDataPropChange)
            {
                ((DllFormatedDisplayProp)m_Control.SpecificProp).FormatString = m_FormatString;
                Document.Modified = true;
                m_Control.IControl.Refresh();
            }
            if (bDataPropChange && ControlPropertiesChanged != null)
                ControlPropertiesChanged(m_Control);

        }

        public void ObjectToPanel()
        {
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

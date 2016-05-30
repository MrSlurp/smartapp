/*
    This file is part of SmartApp.

    SmartApp is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    SmartApp is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with Foobar.  If not, see <http://www.gnu.org/licenses/>.
*/
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
    public partial class CtrlJaugeProperties : BaseControlPropertiesPanel, ISpecificPanel
    {
        private eOrientationJauge m_Orientation = eOrientationJauge.eHorizontaleDG;

        public CtrlJaugeProperties()
        {
            DllEntryClass.LangSys.Initialize(this);
            InitializeComponent();
        }

        private DllCtrlJaugeProp CtrlProp
        {
            get
            { return ((DllCtrlJaugeProp)m_Control.SpecificProp); }
        }

        #region validation des données

        public void PanelToObject()
        {
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
                ((DllCtrlJaugeProp)m_Control.SpecificProp).Orientation = m_Orientation;
                Document.Modified = true;
            }
        }

        public void ObjectToPanel()
        {
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

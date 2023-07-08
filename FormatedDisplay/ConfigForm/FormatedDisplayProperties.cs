/*
    This file is part of SmartApp.
    Copyright (C) 2007-2016  Pascal Bigot

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

namespace FormatedDisplay
{
    public partial class FormatedDisplayProperties : BaseControlPropertiesPanel, ISpecificPanel
    {
        private string m_FormatString = ":F0";

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
            }
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

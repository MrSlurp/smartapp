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
using System.Windows.Forms;
using SmartApp.Wizards;
using CommonLib;

namespace SmartApp.Ihm.Wizards
{
    public partial class WizM3Z2StepWelcome : UserControl, IWizConfigForm
    {
        WizardConfigData m_WizConfig;

        public WizardConfigData WizConfig
        {
            set { m_WizConfig = value; }
        }

        public WizM3Z2StepWelcome()
        {
            InitializeComponent();

        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (Visible)
            {
                lblWelcome.Text = m_WizConfig.GetWelcomeSpeech();

                pic_m3.Image = m_WizConfig.GetWelcomeImage();
            }
        }


        public void HmiToData()
        {
        }
    }
}

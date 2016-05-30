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

namespace CtrlCnxManager
{
    internal partial class CtrlCnxManagerProperties : BaseControlPropertiesPanel, ISpecificPanel
    {
        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        public CtrlCnxManagerProperties()
        {
            DllEntryClass.LangSys.Initialize(this);
            InitializeComponent();
        }
        #region validation des données

        public void PanelToObject()
        {
            bool bDataPropChange = false;

            DllCtrlCnxManagerProp SpecProps = this.m_Control.SpecificProp as DllCtrlCnxManagerProp;

            if (SpecProps.RetryCnxPeriod != (int)edtDelay.Value)
                bDataPropChange = true;
            // testez ici si les paramètres ont changé en les comparant avec ceux contenu dans les propriété
            // spécifiques du BTControl
            // si c'est le cas, assignez bDataPropChange à true;

            if (bDataPropChange)
            {
                Document.Modified = true;
                SpecProps.RetryCnxPeriod = (int)edtDelay.Value;
            }
        }

        public void ObjectToPanel()
        {
            DllCtrlCnxManagerProp SpecProps = this.m_Control.SpecificProp as DllCtrlCnxManagerProp;
            this.edtDelay.Value = SpecProps.RetryCnxPeriod;
        }
        #endregion
    }
}

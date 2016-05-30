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
using CommonLib;

namespace CommonLib
{
    public partial class TimerPropertiesPanel : BaseObjectPropertiesPanel, IObjectPropertyPanel
    {
        #region données membres
        private BTTimer m_Timer = null;
        #endregion

        #region attributs de la classe

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public BaseGest ConfiguredItemGest
        {
            get
            {
                return m_Document.GestTimer;
            }
            set {}
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public BaseObject ConfiguredItem
        {
            get
            {
                return m_Timer;
            }
            set
            {
                m_Timer = value as BTTimer;
            }
        }
        #endregion

        #region constructeur
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public TimerPropertiesPanel()
        {
            InitializeComponent();
            m_NumPeriod.Minimum = 20;
            m_NumPeriod.Maximum = 3600000;
        }
        #endregion

        #region attribut d'accès aux valeurs de la page de propriété
        /// <summary>
        /// 
        /// </summary>
        public int Period
        {
            get
            {
                return (int)m_NumPeriod.Value;
            }
            set
            {
                m_NumPeriod.Value = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool AutoStart
        {
            get
            {
                return m_chkAutoStart.Checked;
            }
            set
            {
                m_chkAutoStart.Checked = value;
            }
        }
        #endregion

        #region validation des données
        public void ObjectToPanel()
        {
            if (m_Timer != null)
            {
                this.Period = m_Timer.Period;
                this.AutoStart = m_Timer.AutoStart;
            }
        }

        public void PanelToObject()
        {
            bool bDataPropChange = false;
            if (m_Timer.Period != this.Period)
                bDataPropChange |= true;

            if (m_Timer.AutoStart != this.AutoStart)
                bDataPropChange |= true;

            if (bDataPropChange)
            {
                m_Timer.Period = this.Period;
                m_Timer.AutoStart = this.AutoStart;
                Document.Modified = true;
            }
        }

        #endregion
    }
}

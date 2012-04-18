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

        #region Events
        public event TimerPropertiesChange TimerPropChange;
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
            if (bDataPropChange && TimerPropChange != null)
                TimerPropChange(m_Timer);
        }

        #endregion
    }
}

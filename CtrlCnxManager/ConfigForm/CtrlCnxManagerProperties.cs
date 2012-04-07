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
        #region events
        public event ControlPropertiesChange ControlPropertiesChanged;
        #endregion

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
                m_Control.IControl.Refresh();
            }
            if (bDataPropChange && ControlPropertiesChanged != null)
                ControlPropertiesChanged(m_Control);

        }

        public void ObjectToPanel()
        {
            DllCtrlCnxManagerProp SpecProps = this.m_Control.SpecificProp as DllCtrlCnxManagerProp;
            this.edtDelay.Value = SpecProps.RetryCnxPeriod;
        }
        #endregion
    }
}

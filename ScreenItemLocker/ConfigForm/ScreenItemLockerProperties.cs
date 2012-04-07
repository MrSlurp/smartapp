using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.IO;
using System.Windows.Forms;
using CommonLib;

namespace ScreenItemLocker
{
    internal partial class ScreenItemLockerProperties : BaseControlPropertiesPanel, ISpecificPanel
    {
        #region events
        public event ControlPropertiesChange ControlPropertiesChanged;
        #endregion

        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        public ScreenItemLockerProperties()
        {
            DllEntryClass.LangSys.Initialize(this);
            InitializeComponent();
        }

        #region validation des données
        /// <summary>
        /// Accesseur de validité des propriétés
        /// renvoie true si les propriété sont valides, sinon false
        /// </summary>
        public override bool IsObjectPropertiesValid
        {
            get
            {
                if (this.m_Control == null)
                    return true;

                return true;
            }
        }

        /// <summary>
        /// validitation des propriétés
        /// </summary>
        /// <returns>true si les propriété sont valides, sinon false</returns>
        public override bool ValidateProperties()
        {
            if (this.m_Control == null)
                return true;

            bool bDataPropChange = false;

            // testez ici si les paramètres ont changé en les comparant avec ceux contenu dans les propriété
            // spécifiques du BTControl
            // si c'est le cas, assignez bDataPropChange à true;

            if (bDataPropChange)
            {
                Document.Modified = true;
                m_Control.IControl.Refresh();
            }
            if (bDataPropChange && ControlPropertiesChanged != null)
                ControlPropertiesChanged(m_Control);
            return true;
        }

        public void ObjectToPanel()
        {

        }

        public void PanelToObject()
        {

        }
        #endregion

        private void btncfg_Click(object sender, EventArgs e)
        {
            SelectControlsForm CfgForm = new SelectControlsForm();
            CfgForm.Doc = this.Document;
            CfgForm.BTControl = this.m_Control;
            CfgForm.Props = (DllScreenItemLockerProp)this.m_Control.SpecificProp;

            if (CfgForm.ShowDialog() == DialogResult.OK)
            {
                // si OK on recopie les param
                this.m_Control.SpecificProp.CopyParametersFrom(CfgForm.Props, false);
            }
        }
    }
}

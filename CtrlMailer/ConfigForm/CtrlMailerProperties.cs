using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.IO;
using System.Windows.Forms;
using CommonLib;

namespace CtrlMailer
{
    internal partial class CtrlMailerProperties : BaseControlPropertiesPanel, ISpecificPanel
    {
        #region events
        public event ControlPropertiesChange ControlPropertiesChanged;
        #endregion

        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        public CtrlMailerProperties()
        {
            DllEntryClass.LangSys.Initialize(this);
            InitializeComponent();
        }

        /// <summary>
        /// Accesseur du document
        /// </summary>
        public BTDoc Doc
        {
            get
            {
                return m_Document;
            }
            set
            {
                m_Document = value;
            }
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
                return true;
            }
        }

        /// <summary>
        /// validitation des propriétés
        /// </summary>
        /// <returns>true si les propriété sont valides, sinon false</returns>
        public override bool ValidateProperties()
        {
            if (this.ConfiguredItem == null)
                return true;

            bool bDataPropChange = false;

            // testez ici si les paramètres ont changé en les comparant avec ceux contenu dans les propriété
            // spécifiques du BTControl
            // si c'est le cas, assignez bDataPropChange à true;

            if (bDataPropChange)
            {
                Doc.Modified = true;
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

        private void btnCfgMail_Click(object sender, EventArgs e)
        {
            MailEditionForm form = new MailEditionForm();
            form.Doc = this.Doc;
            form.Props = (DllCtrlMailerProp)this.m_Control.SpecificProp;
            if (form.ShowDialog() == DialogResult.OK)
            {
                Doc.Modified = true;
                this.m_Control.SpecificProp.CopyParametersFrom(form.Props, false);
            }
        }

        private void btnCfgSMTP_Click(object sender, EventArgs e)
        {
            ConfigSMTP form = new ConfigSMTP();
            form.ShowDialog();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.IO;
using System.Windows.Forms;
using CommonLib;

namespace PasswordControler
{
    internal partial class PasswordControlerProperties : UserControl, ISpecificPanel
    {
        // controle dont on édite les propriété
        BTControl m_Control = null;
        // document courant
        private BTDoc m_Document = null;

        #region events
        public event ControlPropertiesChange ControlPropertiesChanged;
        #endregion

        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        public PasswordControlerProperties()
        {
            DllEntryClass.LangSys.Initialize(this);
            InitializeComponent();
        }

        /// <summary>
        /// Accesseur du control
        /// </summary>
        public BTControl BTControl
        {
            get
            {
                return m_Control;
            }
            set
            {
                if (value != null && value.SpecificProp.GetType() == typeof(DllPasswordControlerProp))
                    m_Control = value;
                else
                    m_Control = null;
                if (m_Control != null)
                {
                    this.Enabled = true;
                    // assignez ici les valeur des propriété spécifiques du control
                }
                else
                {
                    this.Enabled = false;
                    // mettez ici les valeur par défaut pour le panel de propriété spécifiques
                }
            }
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
        public bool IsDataValuesValid
        {
            get
            {
                if (this.BTControl == null)
                    return true;

                return true;
            }
        }

        /// <summary>
        /// validitation des propriétés
        /// </summary>
        /// <returns>true si les propriété sont valides, sinon false</returns>
        public bool ValidateValues()
        {
            if (this.BTControl == null)
                return true;

            bool bDataPropChange = false;

            // testez ici si les paramètres ont changé en les comparant avec ceux contenu dans les propriété
            // spécifiques du BTControl
            // si c'est le cas, assignez bDataPropChange à true;

            if (bDataPropChange)
            {
                Doc.Modified = true;
                m_Control.IControl.Refresh();
            }
            if (bDataPropChange && ControlPropertiesChanged != null)
                ControlPropertiesChanged(m_Control);
            return true;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CommonLib
{
    public partial class TwoColorProperties : UserControl, ISpecificPanel
    {
        #region données membres
        private BTControl m_Control = null;
        private BTDoc m_Document = null;
        #endregion

        #region events
        public event ControlPropertiesChange ControlPropertiesChanged;
        #endregion

        #region constructeur
        /// <summary>
        /// constructeur par défaut
        /// </summary>
        public TwoColorProperties()
        {
            InitializeComponent();
        }
        #endregion

        #region attribut d'accès aux valeurs de la page de propriété
        /// <summary>
        /// assigne ou récupère le BT control à paramétrer
        /// </summary>
        public BTControl BTControl
        {
            get
            {
                return m_Control;
            }
            set
            {
                if (value != null && value.SpecificProp.GetType() == typeof(TwoColorProp))
                    m_Control = value;
                else
                    m_Control = null;
                if (m_Control != null)
                {
                    this.Enabled = true;
                    m_TextActiveColor.BackColor = ((TwoColorProp)m_Control.SpecificProp).ColorActive;
                    m_TextInactiveColor.BackColor = ((TwoColorProp)m_Control.SpecificProp).ColorInactive;
                }
                else
                {
                    this.Enabled = false;
                    m_TextActiveColor.BackColor = Color.White;
                    m_TextInactiveColor.BackColor = Color.White;
                }
            }
        }

        /// <summary>
        /// assigne ou obtiens le document courant
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
        #endregion

        #region validation des données
        /// <summary>
        /// attribut en lecture seul qui indique si les propriété courantes sont valides
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
        /// valide les paramètres et affiche un message en cas d'erreur
        /// </summary>
        /// <returns>true si les paramètres sont valides</returns>
        public bool ValidateValues()
        {
            if (this.BTControl == null)
                return true;

            bool bDataPropChange = false;
            if (m_TextActiveColor.BackColor != ((TwoColorProp)m_Control.SpecificProp).ColorActive)
                bDataPropChange = true;
            if (m_TextInactiveColor.BackColor != ((TwoColorProp)m_Control.SpecificProp).ColorInactive)
                bDataPropChange = true;

            if (bDataPropChange)
            {
                ((TwoColorProp)m_Control.SpecificProp).ColorActive = m_TextActiveColor.BackColor;
                ((TwoColorProp)m_Control.SpecificProp).ColorInactive = m_TextInactiveColor.BackColor;
                Doc.Modified = true;
                m_Control.IControl.Refresh();
            }
            if (bDataPropChange && ControlPropertiesChanged != null)
                ControlPropertiesChanged(m_Control);
            return true;
        }
        #endregion

        #region méthodes privées
        /// <summary>
        /// callback de choix de la couleur active
        /// </summary>
        /// <param name="sender">objet ayant envoyé l'event</param>
        /// <param name="e">paramètres de l'event</param>
        private void m_BtnSelectActiveColor_Click(object sender, EventArgs e)
        {
            m_clrDlg.Color = ((TwoColorProp)m_Control.SpecificProp).ColorActive;
            DialogResult DlgRes = m_clrDlg.ShowDialog();
            if (DlgRes == DialogResult.OK)
            {
                m_TextActiveColor.BackColor = m_clrDlg.Color;
            }
        }

        /// <summary>
        /// callback de choix de la couleur inactive
        /// </summary>
        /// <param name="sender">objet ayant envoyé l'event</param>
        /// <param name="e">paramètres de l'event</param>
        private void m_BtnSelectInactiveColor_Click(object sender, EventArgs e)
        {
            m_clrDlg.Color = ((TwoColorProp)m_Control.SpecificProp).ColorInactive;
            DialogResult DlgRes = m_clrDlg.ShowDialog();
            if (DlgRes == DialogResult.OK)
            {
                m_TextInactiveColor.BackColor = m_clrDlg.Color;
            }
        }
        #endregion
    }
}

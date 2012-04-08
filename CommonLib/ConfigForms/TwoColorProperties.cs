using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CommonLib
{
    public partial class TwoColorProperties : BaseControlPropertiesPanel, ISpecificPanel
    {
        #region events
        public event ControlPropertiesChange ControlPropertiesChanged;
        #endregion

        #region constructeur
        /// <summary>
        /// constructeur par défaut
        /// </summary>
        public TwoColorProperties()
        {
            Lang.LangSys.Initialize(this);
            InitializeComponent();
        }
        #endregion

        #region validation des données

        public void ObjectToPanel()
        {
            TwoColorProp specProp = m_Control.SpecificProp as TwoColorProp;
            m_TextActiveColor.BackColor = specProp.ColorActive;
            m_TextInactiveColor.BackColor = specProp.ColorInactive;
        }

        public void PanelToObject()
        {
            bool bDataPropChange = false;
            if (m_TextActiveColor.BackColor != ((TwoColorProp)m_Control.SpecificProp).ColorActive)
                bDataPropChange = true;
            if (m_TextInactiveColor.BackColor != ((TwoColorProp)m_Control.SpecificProp).ColorInactive)
                bDataPropChange = true;

            if (bDataPropChange)
            {
                ((TwoColorProp)m_Control.SpecificProp).ColorActive = m_TextActiveColor.BackColor;
                ((TwoColorProp)m_Control.SpecificProp).ColorInactive = m_TextInactiveColor.BackColor;
                Document.Modified = true;
                m_Control.IControl.Refresh();
            }
            if (bDataPropChange && ControlPropertiesChanged != null)
                ControlPropertiesChanged(m_Control);

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

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

namespace CommonLib
{
    public partial class TwoColorProperties : BaseControlPropertiesPanel, ISpecificPanel
    {
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
            }

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

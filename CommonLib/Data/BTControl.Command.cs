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
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace CommonLib
{
    /// <summary>
    /// suite de la classe BTControl, contient les fonction spéficique à leur fonctionnement
    /// dans SmartCommand
    /// </summary>
    public partial class BTControl
    {
        #region Déclaration des données de la classe pour BTCommand
        protected Control m_Ctrl;
        protected Data m_AssociateData;
        protected BTScreen m_Parent;
        protected Rectangle m_RectControl;
        protected QuickExecuter m_Executer = null;

        #endregion

        #region attributs
        public virtual bool DisabledOnStop
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// assigne ou obtient le script executer de l'objet
        /// </summary>
        public QuickExecuter Executer
        {
            get
            {
                return m_Executer;
            }
        }

        /// <summary>
        /// obtient l'objet affiché par le control
        /// </summary>
        public Control DisplayedControl
        {
            get
            {
                return m_Ctrl;
            }
        }

        #endregion

        #region Fonctions pour BTCommand
        /// <summary>
        /// définit l'écran possédant le control
        /// </summary>
        /// <param name="btScreen">écran parent</param>
        public BTScreen Parent
        {
            get { return m_Parent; }
            set { m_Parent = value; }
        }

        /// <summary>
        /// définit le rectangle de l'objet a partir du control intéractif de smart Config
        /// </summary>
        public void SetControlRect()
        {
            m_RectControl = new Rectangle(this.IControl.Location, this.IControl.Size);
        }

        /// <summary>
        /// crée l'objet affiché en mode commande et le paramètre
        /// </summary>
        public virtual void CreateControl() { }

        /// <summary>
        /// 
        /// </summary>
        public virtual void ApplyControlFont() 
        {
            if (m_Ctrl != null)
            {
                m_Ctrl.Font = this.TextFont;
                m_Ctrl.ForeColor = this.TextColor;
            }
        }

        /// <summary>
        /// callback appelé lors que le control affiché déclenche un évènement
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="Args"></param>
        public virtual void OnControlEvent(Object Sender, EventArgs Args) { }

        /// <summary>
        /// 
        /// </summary>
        public virtual void UpdateFromDataDelegate()
        {
            if (m_Ctrl.InvokeRequired)
            {
                EventDataValueChange AsyncCall = new EventDataValueChange(UpdateFromData);
                m_Ctrl.Invoke(AsyncCall);
            }
            else
                UpdateFromData();

        }

        /// <summary>
        /// met a jour le controle affiché lorsque sa donnée associée à changé
        /// </summary>
        public virtual void UpdateFromData() { }

        #endregion
    }
}

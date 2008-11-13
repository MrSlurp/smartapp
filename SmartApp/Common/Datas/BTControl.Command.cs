using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using SmartApp.Scripts;

namespace SmartApp.Datas
{
    public partial class BTControl
    {
        #region Déclaration des données de la classe pour BTCommand
        protected Control m_Ctrl;
        protected Data m_AssociateData;
        protected BTScreen m_Parent;
        protected Rectangle m_RectControl;
        protected ScriptExecuter m_Executer = null;
        #endregion

        #region attributs
        public ScriptExecuter Executer
        {
            get
            {
                return m_Executer;
            }
            set
            {
                m_Executer = value;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public Control DisplayedControl
        {
            get
            {
                return m_Ctrl;
            }
        }

        #endregion

        #region Fonction pour BTCommand
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public void SetParent(BTScreen btScreen)
        {
            m_Parent = btScreen;
        }

        public void SetControlRect()
        {
            m_RectControl = new Rectangle(this.IControl.Location, this.IControl.Size);
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public virtual void CreateControl() { }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public virtual void OnControlEvent(Object Sender, EventArgs Args) { }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public virtual void UpdateFromData() { }
        #endregion
    }
}

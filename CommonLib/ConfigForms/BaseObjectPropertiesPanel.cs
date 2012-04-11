using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CommonLib
{
    public class BaseObjectPropertiesPanel : UserControl
    {
        // document courant
        protected BTDoc m_Document = null;

        /// <summary>
        /// assigne ou obtiens le document courant
        /// </summary>
        public virtual BTDoc Document
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

        public Control Panel 
        { 
            get { return this; } 
        }

        /// <summary>
        /// attribut en lecture seul qui indique si les propriété courantes sont valides
        /// </summary>
        public virtual bool IsObjectPropertiesValid
        {
            get
            {
                // aucun cas d'invalidité pour cet item
                return true;
            }
        }

        /// <summary>
        /// valide les paramètres et affiche un message en cas d'erreur
        /// </summary>
        /// <returns>true si les paramètres sont valides</returns>
        public virtual bool ValidateProperties()
        {
            // aucun cas d'invalidité pour cet item
            return true;
        }

    }
}

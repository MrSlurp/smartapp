using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;

namespace CommonLib
{
    public class BaseControlPropertiesPanel : UserControl
    {
        // controle dont on édite les propriété
        protected BTControl m_Control = null;

        // document courant
        protected BTDoc m_Document = null;

        protected GestControl m_GestControl = null;

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

        /// <summary>
        /// 
        /// </summary>
        public virtual BaseObject ConfiguredItem
        {
            get { return m_Control; }
            set { m_Control = value as BTControl; }
        }


        public Control Panel { get { return this; } }

        /// <summary>
        /// 
        /// </summary>
        public BaseGest ConfiguredItemGest
        {
            get { return m_GestControl; }
            set { m_GestControl = value as GestControl; }
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

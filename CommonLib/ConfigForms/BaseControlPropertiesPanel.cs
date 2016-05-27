using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;

namespace CommonLib
{
    public class BaseControlPropertiesPanel : BaseObjectPropertiesPanel
    {
        // controle dont on édite les propriété
        protected BTControl m_Control = null;

        protected GestControl m_GestControl = null;

        /// <summary>
        /// 
        /// </summary>
        public virtual BaseObject ConfiguredItem
        {
            get { return m_Control; }
            set { m_Control = value as BTControl; }
        }

        /// <summary>
        /// 
        /// </summary>
        public BaseGest ConfiguredItemGest
        {
            get { return m_GestControl; }
            set { m_GestControl = value as GestControl; }
        }


    }
}

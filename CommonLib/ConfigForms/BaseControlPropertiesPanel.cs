/*
    This file is part of SmartApp.
    Copyright (C) 2007-2016  Pascal Bigot

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

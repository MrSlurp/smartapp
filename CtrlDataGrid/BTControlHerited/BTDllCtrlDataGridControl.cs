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
using System.Xml;
using CommonLib;

namespace CtrlDataGrid
{
    internal class BTDllCtrlDataGridControl : BasePluginBTControl
    {
        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        public BTDllCtrlDataGridControl(BTDoc document)
            : base(document)
        {
            m_IControl = new InteractiveCtrlDataGridDllControl();
            if (m_IControl != null)
                m_IControl.SourceBTControl = this;

            m_SpecificProp = new DllCtrlDataGridProp(this.ItemScripts);
        }

        /// <summary>
        /// Constructeur de la à partir d'un control interactif
        /// </summary>
        public BTDllCtrlDataGridControl(BTDoc document, InteractiveControl Ctrl)
            : base(document, Ctrl)
        {
            m_SpecificProp = new DllCtrlDataGridProp(this.ItemScripts);
        }

        /// <summary>
        /// Renvoie l'ID de la DLL
        /// </summary>
        public override uint DllControlID
        {
            get
            {
                return CtrlDataGrid.DllEntryClass.DLL_Control_ID;
            }
        }
    }
}

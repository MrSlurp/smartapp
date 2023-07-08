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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SmartApp.Ihm
{
    /// <summary>
    /// cette classe n'a qu'une seul raison d'éxister, éviter le scroll automatique dans le designer
    /// elle est contenue par le splitter container en dock fill, et contient le ScreenDesigner à sa taille max
    /// avec la propriété AutoScroll pour avoir les scroll bar
    /// </summary>
    public partial class InteractivePanelContainer : Panel
    {
        public InteractivePanelContainer()
        {
        }

        protected override Point ScrollToControl(Control activeControl)
        {

            return this.AutoScrollPosition;
        }
    }
}

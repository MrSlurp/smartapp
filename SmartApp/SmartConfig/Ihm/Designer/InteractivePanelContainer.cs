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

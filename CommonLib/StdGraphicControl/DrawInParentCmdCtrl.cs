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
using System.Drawing;
using System.Windows.Forms;

namespace CommonLib
{
    public delegate void NeedSuperposedRefresh(Control ctrl);
    public class DrawInParentCmdCtrl : UserControl
    {
        protected bool m_bDrawInParent = false;
        public DrawInParentCmdCtrl()
        {
            SetStyle(
                        ControlStyles.UserPaint |
                        ControlStyles.AllPaintingInWmPaint |
                        ControlStyles.DoubleBuffer, true);
            BackColor = Color.Transparent;
        }

        public bool DrownInParent
        {
            get
            {
                return m_bDrawInParent;
            }
        }

        public event NeedSuperposedRefresh RefreshSuperposedItem;

        public void SetDelgateRefresh(NeedSuperposedRefresh dg)
        {
            RefreshSuperposedItem += dg;
        }

        protected void CallEventRefresh()
            {
            if (RefreshSuperposedItem != null)
                RefreshSuperposedItem(this);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
        }

        public virtual void OnPaintInParent(Graphics gr, Rectangle rect)
        {
        }

    }
}

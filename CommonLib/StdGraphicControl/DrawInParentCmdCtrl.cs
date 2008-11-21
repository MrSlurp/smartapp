using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace CommonLib
{
    public delegate void NeedSuperposedRefresh();
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
                RefreshSuperposedItem();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
        }

        public virtual void OnPaintInParent(Graphics gr, Rectangle rect)
        {
        }

    }
}

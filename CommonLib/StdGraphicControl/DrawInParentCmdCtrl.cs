using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace CommonLib
{
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


        protected override void OnPaint(PaintEventArgs e)
        {
            /*
            if (m_bDrawInParent && this.Parent != null)
            {
                Graphics gr = this.Parent.CreateGraphics();
                Rectangle drawRect = Parent.RectangleToClient(this.RectangleToScreen(this.ClientRectangle));
                OnPaintInParent(gr, drawRect);
                gr.Dispose();
            }
             * */
        }

        /*
    protected override void  OnPaintBackground(PaintEventArgs e)
    {
        if (m_bDrawInParent && this.Parent != null)
        {
            Graphics gr = this.Parent.CreateGraphics();
            Rectangle drawRect = Parent.RectangleToClient(this.RectangleToScreen(this.ClientRectangle));
            OnPaintInParent(gr, drawRect);
            gr.Dispose();
        }
    }
         * */

        public virtual void OnPaintInParent(Graphics gr, Rectangle rect)
        {
        }

    }
}

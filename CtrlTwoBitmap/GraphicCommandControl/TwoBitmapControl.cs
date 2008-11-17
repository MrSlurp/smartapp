using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using CommonLib;

namespace CtrlTwoBitmap
{
    internal class TwoBitmapControl : BTTwoBitmapControl
    {
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public TwoBitmapControl()
        {

        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public override void CreateControl()
        {
            if (m_Ctrl == null)
            {
                m_Ctrl = new TwoBitmap();
                m_Ctrl.Location = m_RectControl.Location;
                m_Ctrl.Name = m_strSymbol;
                m_Ctrl.Size = m_RectControl.Size;
                m_Ctrl.BackColor = Color.Transparent;
                ((TwoBitmap)m_Ctrl).strBmpAct = ((TwoBitmapProp)this.m_SpecificProp).NomFichierActif;
                ((TwoBitmap)m_Ctrl).strBmpInact = ((TwoBitmapProp)this.m_SpecificProp).NomFichierInactif;
            }
        }

        public override void OnControlEvent(object Sender, EventArgs Args)
        {
            return;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public override void UpdateFromData()
        {
            if (m_AssociateData != null && m_Ctrl != null)
            {
                if (m_AssociateData.Value != 0)
                    ((TwoBitmap)m_Ctrl).IsActive = true;
                else
                    ((TwoBitmap)m_Ctrl).IsActive = false;

                m_Ctrl.Refresh();
            }
        }
    }

    public class TwoBitmap : UserControl
    {
        private bool m_bIsActive = false;
        private string m_strBmpInact;
        private string m_strBmpAct;
        private Bitmap m_BmpInact;
        private Bitmap m_BmpAct;

        public TwoBitmap()
        {
            SetStyle(
                        ControlStyles.UserPaint |
                        ControlStyles.AllPaintingInWmPaint |
                        ControlStyles.DoubleBuffer, true);

        }

        public bool IsActive
        {
            get
            {
                return m_bIsActive;
            }
            set
            {
                m_bIsActive = value;
            }
        }

        public string strBmpInact
        {
            get
            {
                return m_strBmpInact;
            }
            set
            {
                m_strBmpInact = value;
                m_BmpInact = new Bitmap(m_strBmpInact);
                if (ImageAnimator.CanAnimate(m_BmpInact))
                {
                    ImageAnimator.Animate(m_BmpInact, new EventHandler(OnFrameChanged));
                }
            }
        }
        public string strBmpAct
        {
            get
            {
                return m_strBmpAct;
            }
            set
            {
                m_strBmpAct = value;
                m_BmpAct = new Bitmap(m_strBmpAct);
                if (ImageAnimator.CanAnimate(m_BmpAct))
                {
                    ImageAnimator.Animate(m_BmpAct, new EventHandler(OnFrameChanged));
                }
            }
        }

        private void OnFrameChanged(object o, EventArgs e)
        {
            //Force a call to the Paint event handler.
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap UsedBmp = m_bIsActive ? this.m_BmpAct : this.m_BmpInact;
            e.Graphics.DrawImage(UsedBmp, new Rectangle(new Point(0, 0), this.Size));
            ImageAnimator.UpdateFrames();
        }
    }
}

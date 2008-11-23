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
                
                string strBmpInact = ((TwoBitmapProp)this.m_SpecificProp).NomFichierInactif;
                string strImageFullPath = strBmpInact.Replace(@".\", Application.StartupPath + @"\");
                try
                {
                    ((TwoBitmap)m_Ctrl).BmpInact = new Bitmap(strImageFullPath);
                }
                catch (Exception)
                {
                    LogEvent log = new LogEvent(LOG_EVENT_TYPE.WARNING, string.Format("Control {0} Failed to load file {1}", Symbol, strImageFullPath));
                    AddLogEvent(log);
                }
                
                string strBmpAct = ((TwoBitmapProp)this.m_SpecificProp).NomFichierActif;
                strImageFullPath = strBmpAct.Replace(@".\", Application.StartupPath + @"\");
                try
                {
                    ((TwoBitmap)m_Ctrl).BmpAct = new Bitmap(strImageFullPath);
                }
                catch (Exception)
                {
                    LogEvent log = new LogEvent(LOG_EVENT_TYPE.WARNING, string.Format("Control {0} Failed to load file {1}", Symbol, strImageFullPath));
                    AddLogEvent(log);
                }
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

        public override void TraiteMessage(MESSAGE Mess, object obj, TYPE_APP TypeApp)
        {
            base.TraiteMessage(Mess, obj, TypeApp);
            if (TypeApp == TYPE_APP.SMART_COMMAND)
            {
                switch (Mess)
                {
                    // message de requête sur les conséquence d'une supression
                    case MESSAGE.MESS_CMD_STOP:
                        ((TwoBitmap)m_Ctrl).IsRunning = false;
                        break;
                    case MESSAGE.MESS_CMD_RUN:
                        ((TwoBitmap)m_Ctrl).IsRunning = true;
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public class TwoBitmap : UserControl
    {
        private bool m_bIsActive = false;
        private Bitmap m_BmpInact;
        private Bitmap m_BmpAct;
        private bool m_bIsRunning = false;

        public TwoBitmap()
        {
            SetStyle(
                        ControlStyles.UserPaint |
                        ControlStyles.AllPaintingInWmPaint |
                        ControlStyles.SupportsTransparentBackColor |
                        ControlStyles.DoubleBuffer, true);
        }

        public bool IsRunning
        {
            get
            {
                return m_bIsRunning;
            }
            set
            {
                if (m_bIsRunning != value)
                {
                    m_bIsRunning = value;
                    TraiteAnimation();
                }
            }
        }

        public void TraiteAnimation()
        {
            if (m_BmpInact != null && ImageAnimator.CanAnimate(m_BmpInact))
            {
                if (m_bIsRunning && m_bIsActive)
                    ImageAnimator.Animate(m_BmpInact, new EventHandler(OnFrameChanged));
                else
                    ImageAnimator.StopAnimate(m_BmpInact, new EventHandler(OnFrameChanged));

            }
            if (m_BmpAct != null && ImageAnimator.CanAnimate(m_BmpAct))
            {
                if (m_bIsRunning && m_bIsActive)
                    ImageAnimator.Animate(m_BmpAct, new EventHandler(OnFrameChanged));
                else
                    ImageAnimator.StopAnimate(m_BmpAct, new EventHandler(OnFrameChanged));
            }

        }

        public bool IsActive
        {
            get
            {
                return m_bIsActive;
            }
            set
            {
                if (m_bIsActive != value)
                {
                    m_bIsActive = value;
                    TraiteAnimation();
                    Refresh();
                }
            }
        }

        public Bitmap BmpInact
        {
            get
            {
                return m_BmpInact;
            }
            set
            {
                m_BmpInact = value;
                if (!ImageAnimator.CanAnimate(m_BmpInact))
                    m_BmpInact.MakeTransparent(Color.Magenta);
            }
        }
        public Bitmap BmpAct
        {
            get
            {
                return m_BmpAct;
            }
            set
            {
                m_BmpAct = value;
                if (!ImageAnimator.CanAnimate(m_BmpAct))
                    m_BmpAct.MakeTransparent(Color.Magenta);

            }
        }

        private void OnFrameChanged(object o, EventArgs e)
        {
            //Force a call to the Paint event handler.
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (m_bIsActive)
            {
                if (m_BmpAct != null )
                {
                    if (ImageAnimator.CanAnimate(m_BmpAct))
                        ImageAnimator.UpdateFrames(m_BmpAct);
                    
                    e.Graphics.DrawImage(m_BmpAct, new Rectangle(new Point(0, 0), this.Size));
                }
                else
                    e.Graphics.DrawImage(TwoImageRes.DefaultImg, new Rectangle(new Point(0, 0), this.Size));
            }
            else
            {
                if (m_BmpInact != null)
                {
                    if (ImageAnimator.CanAnimate(m_BmpInact))
                        ImageAnimator.UpdateFrames(m_BmpInact);

                    e.Graphics.DrawImage(m_BmpInact, new Rectangle(new Point(0, 0), this.Size));
                }
                else
                    e.Graphics.DrawImage(TwoImageRes.DefaultImg, new Rectangle(new Point(0, 0), this.Size));
            }
        }

    }
}

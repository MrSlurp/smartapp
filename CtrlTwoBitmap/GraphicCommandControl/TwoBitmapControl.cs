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
                
                string strImageFullPath = PathTranslator.RelativePathToAbsolute(((TwoBitmapProp)this.m_SpecificProp).NomFichierInactif);
                strImageFullPath = PathTranslator.LinuxVsWindowsPathUse(strImageFullPath);
                try
                {
                    ((TwoBitmap)m_Ctrl).BmpInact = new Bitmap(strImageFullPath);
                }
                catch (Exception)
                {
                    LogEvent log = new LogEvent(LOG_EVENT_TYPE.WARNING, string.Format("Control {0} Failed to load file {1}", Symbol, strImageFullPath));
                    AddLogEvent(log);
                }
                
                strImageFullPath = PathTranslator.RelativePathToAbsolute(((TwoBitmapProp)this.m_SpecificProp).NomFichierActif);
                strImageFullPath = PathTranslator.LinuxVsWindowsPathUse(strImageFullPath);
                try
                {
                    ((TwoBitmap)m_Ctrl).BmpAct = new Bitmap(strImageFullPath);
                }
                catch (Exception)
                {
                    if (m_AssociateData != null)
                    {
                        LogEvent log = new LogEvent(LOG_EVENT_TYPE.WARNING, string.Format("Control {0} Failed to load file {1}", Symbol, strImageFullPath));
                        AddLogEvent(log);
                    }
                }
                UpdateFromData();
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

    public class TwoBitmap : PictureBox
    {
        private bool m_bIsActive = false;
        private bool m_bIsRunning = false;

        private Bitmap m_BmpInact;
        private Bitmap m_BmpAct;

        //private bool m_BmpInactAnimated = false;          
        //private bool m_BmpActAnimated = false;          
        //private bool IsAnimating = false;

        public TwoBitmap()
        {
            SetStyle(
                        ControlStyles.UserPaint |
                        ControlStyles.AllPaintingInWmPaint |
                        ControlStyles.SupportsTransparentBackColor |
                        ControlStyles.DoubleBuffer, true);
            this.SizeMode = PictureBoxSizeMode.StretchImage;
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
                    
                }
                else
                {
                    if (BmpInact != null)
                        this.Image = BmpInact;
                }
            }
        }

        /*
        public void TraiteAnimation()
        {
            if (m_BmpInact != null && ImageAnimator.CanAnimate(m_BmpInact))
            {
                if (m_bIsRunning && m_bIsActive)
                {
                    ImageAnimator.Animate(m_BmpInact, new EventHandler(OnFrameChanged));
                    IsAnimating = true;
                }
                else
                    ImageAnimator.StopAnimate(m_BmpInact, new EventHandler(OnFrameChanged));

            }
            if (m_BmpAct != null && ImageAnimator.CanAnimate(m_BmpAct))
            {
                if (m_bIsRunning && m_bIsActive)
                {
                    ImageAnimator.Animate(m_BmpAct, new EventHandler(OnFrameChanged));
                    IsAnimating = true;
                }
                else
                    ImageAnimator.StopAnimate(m_BmpAct, new EventHandler(OnFrameChanged));
            }

        }
        */
        
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
                    //TraiteAnimation();
                }
                if (m_bIsActive)
                {
                    if (m_BmpAct != null)
                        this.Image = m_BmpAct;
                    else
                        this.Image = null;
                }
                else
                {
                    if (BmpInact != null)
                        this.Image = BmpInact;
                    else
                        this.Image = null;
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
                if (this.Image == null && m_BmpInact != null)
                    this.Image = m_BmpInact;
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

        /*
        private void OnFrameChanged(object o, EventArgs e)
        {
            //Force a call to the Paint event handler.
            if (IsAnimating)
                this.Invalidate();
        }
        */

        /*
        protected override void OnPaint(PaintEventArgs e)
        {
            CommonLib.PerfChrono theChrono = new PerfChrono();
            if (m_bIsActive)
            {
                if (m_BmpAct != null )
                {
					Bitmap disp = null;
                    if (ImageAnimator.CanAnimate(m_BmpAct))
					{
                        ImageAnimator.UpdateFrames(m_BmpAct);
						disp = new Bitmap(m_BmpAct, m_BmpAct.Width, m_BmpAct.Height);
						disp.MakeTransparent(Color.Magenta);
					}
					else
					{
						disp = m_BmpAct;
					}
				
                    e.Graphics.DrawImage(disp, new Rectangle(new Point(0, 0), this.Size));
                }
                else
                    e.Graphics.DrawImage(TwoImageRes.DefaultImg, new Rectangle(new Point(0, 0), this.Size));
            }
            else
            {
                if (m_BmpInact != null)
                {
					Bitmap disp = null;
                    if (ImageAnimator.CanAnimate(m_BmpInact))
					{
                        ImageAnimator.UpdateFrames(m_BmpInact);
						disp = new Bitmap(m_BmpInact, m_BmpInact.Width, m_BmpInact.Height);
						disp.MakeTransparent(Color.Magenta);
					}
					else
					{
						disp = m_BmpInact;
					}
					
                    e.Graphics.DrawImage(disp, new Rectangle(new Point(0, 0), this.Size));
                }
                else
                    e.Graphics.DrawImage(TwoImageRes.DefaultImg, new Rectangle(new Point(0, 0), this.Size));
            }
            theChrono.EndMeasure("InstanceName = " + this.Name );
        }
        */
        protected override void OnPaint(PaintEventArgs e)
        {
            //CommonLib.PerfChrono theChrono = new PerfChrono();
            base.OnPaint(e);
            //theChrono.EndMeasure("InstanceName = " + this.Name);
        }


    }
}

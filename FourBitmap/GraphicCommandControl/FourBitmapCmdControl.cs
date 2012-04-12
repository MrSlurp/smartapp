using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using CommonLib;

namespace FourBitmap
{
    internal class FourBitmapCmdControl : BTDllFourBitmapControl
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="document"></param>
        public FourBitmapCmdControl(BTDoc document)
            : base(document)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public override void CreateControl()
        {
            if (m_Ctrl == null)
            {
                m_Ctrl = new FourBitmapDispCtrl();
                m_Ctrl.Location = m_RectControl.Location;
                m_Ctrl.Name = m_strSymbol;
                m_Ctrl.Size = m_RectControl.Size;
                m_Ctrl.BackColor = Color.Transparent;

                string strImageFullPath = m_Document.PathTr.RelativePathToAbsolute(((DllFourBitmapProp)this.m_SpecificProp).NomFichier0);
                strImageFullPath = PathTranslator.LinuxVsWindowsPathUse(strImageFullPath);
                try
                {
                    ((FourBitmapDispCtrl)m_Ctrl).Bmp0 = new Bitmap(strImageFullPath);
                }
                catch (Exception)
                {
                    LogEvent log = new LogEvent(LOG_EVENT_TYPE.WARNING, string.Format(DllEntryClass.LangSys.C("Control {0} Failed to load file {1}"), Symbol, strImageFullPath));
                    AddLogEvent(log);
                }

                strImageFullPath = m_Document.PathTr.RelativePathToAbsolute(((DllFourBitmapProp)this.m_SpecificProp).NomFichier1);
                strImageFullPath = PathTranslator.LinuxVsWindowsPathUse(strImageFullPath);
                try
                {
                    ((FourBitmapDispCtrl)m_Ctrl).Bmp1 = new Bitmap(strImageFullPath);
                }
                catch (Exception)
                {
                    if (m_AssociateData != null)
                    {
                        LogEvent log = new LogEvent(LOG_EVENT_TYPE.WARNING, string.Format(DllEntryClass.LangSys.C("Control {0} Failed to load file {1}"), Symbol, strImageFullPath));
                        AddLogEvent(log);
                    }
                }

                strImageFullPath = m_Document.PathTr.RelativePathToAbsolute(((DllFourBitmapProp)this.m_SpecificProp).NomFichier2);
                strImageFullPath = PathTranslator.LinuxVsWindowsPathUse(strImageFullPath);
                try
                {
                    ((FourBitmapDispCtrl)m_Ctrl).Bmp2 = new Bitmap(strImageFullPath);
                }
                catch (Exception)
                {
                    if (m_AssociateData != null)
                    {
                        LogEvent log = new LogEvent(LOG_EVENT_TYPE.WARNING, string.Format(DllEntryClass.LangSys.C("Control {0} Failed to load file {1}"), Symbol, strImageFullPath));
                        AddLogEvent(log);
                    }
                }

                strImageFullPath = m_Document.PathTr.RelativePathToAbsolute(((DllFourBitmapProp)this.m_SpecificProp).NomFichier3);
                strImageFullPath = PathTranslator.LinuxVsWindowsPathUse(strImageFullPath);
                try
                {
                    ((FourBitmapDispCtrl)m_Ctrl).Bmp3 = new Bitmap(strImageFullPath);
                }
                catch (Exception)
                {
                    if (m_AssociateData != null)
                    {
                        LogEvent log = new LogEvent(LOG_EVENT_TYPE.WARNING, string.Format(DllEntryClass.LangSys.C("Control {0} Failed to load file {1}"), Symbol, strImageFullPath));
                        AddLogEvent(log);
                    }
                }
                UpdateFromData();
            }
        }

        public override void OnControlEvent(object Sender, EventArgs Args)
        {
            // traitez ici les évènement déclenché par le control (click souris par exemple)
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
                if (m_AssociateData.Value >= 0 && m_AssociateData.Value < 4)
                    ((FourBitmapDispCtrl)m_Ctrl).State = m_AssociateData.Value;
                else
                    ((FourBitmapDispCtrl)m_Ctrl).State = 0;
            }
            else
                ((FourBitmapDispCtrl)m_Ctrl).State = 0;
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
                        ((FourBitmapDispCtrl)m_Ctrl).IsRunning = false;
                        break;
                    case MESSAGE.MESS_CMD_RUN:
                        ((FourBitmapDispCtrl)m_Ctrl).IsRunning = true;
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public class FourBitmapDispCtrl : PictureBox
    {
        private int m_State = 0;
        private Bitmap m_Bmp0;
        private Bitmap m_Bmp1;
        private Bitmap m_Bmp2;
        private Bitmap m_Bmp3;
        private bool m_bIsRunning = false;

        public FourBitmapDispCtrl()
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
                    //TraiteAnimation();
                }
                else
                {
                    if (m_Bmp0 != null)
                        this.Image = m_Bmp0;
                }

            }
        }

        /*
        public void TraiteAnimation()
        {
            if (m_Bmp0 != null && ImageAnimator.CanAnimate(m_Bmp0))
            {
                if (m_bIsRunning)
                    ImageAnimator.Animate(m_Bmp0, new EventHandler(OnFrameChanged));
                else
                    ImageAnimator.StopAnimate(m_Bmp0, new EventHandler(OnFrameChanged));

            }
            if (m_Bmp1 != null && ImageAnimator.CanAnimate(m_Bmp1))
            {
                if (m_bIsRunning)
                    ImageAnimator.Animate(m_Bmp1, new EventHandler(OnFrameChanged));
                else
                    ImageAnimator.StopAnimate(m_Bmp1, new EventHandler(OnFrameChanged));
            }
            if (m_Bmp2 != null && ImageAnimator.CanAnimate(m_Bmp2))
            {
                if (m_bIsRunning)
                    ImageAnimator.Animate(m_Bmp2, new EventHandler(OnFrameChanged));
                else
                    ImageAnimator.StopAnimate(m_Bmp2, new EventHandler(OnFrameChanged));
            }
            if (m_Bmp3 != null && ImageAnimator.CanAnimate(m_Bmp3))
            {
                if (m_bIsRunning)
                    ImageAnimator.Animate(m_Bmp3, new EventHandler(OnFrameChanged));
                else
                    ImageAnimator.StopAnimate(m_Bmp3, new EventHandler(OnFrameChanged));
            }
            this.Refresh();
        }
         * */

        public int State
        {
            get
            {
                return m_State;
            }
            set
            {
                if (m_State != value)
                {
                    m_State = value;
                    Refresh();
                    //TraiteAnimation();
                }
                Bitmap FinalyDisp;
                switch (m_State)
                {
                    default:
                    case 0:
                        FinalyDisp = Bmp0;
                        break;
                    case 1:
                        FinalyDisp = Bmp1;
                        break;
                    case 2:
                        FinalyDisp = Bmp2;
                        break;
                    case 3:
                        FinalyDisp = Bmp3;
                        break;
                }
                if (FinalyDisp != null)
                    this.Image = FinalyDisp;

            }
        }

        public Bitmap Bmp0
        {
            get
            {
                return m_Bmp0;
            }
            set
            {
                m_Bmp0 = value;
                if (!ImageAnimator.CanAnimate(m_Bmp0))
                    m_Bmp0.MakeTransparent(Color.Magenta);
            }
        }

        public Bitmap Bmp1
        {
            get
            {
                return m_Bmp1;
            }
            set
            {
                m_Bmp1 = value;
                if (!ImageAnimator.CanAnimate(m_Bmp1))
                    m_Bmp1.MakeTransparent(Color.Magenta);
            }
        }

        public Bitmap Bmp2
        {
            get
            {
                return m_Bmp2;
            }
            set
            {
                m_Bmp2 = value;
                if (!ImageAnimator.CanAnimate(m_Bmp2))
                    m_Bmp2.MakeTransparent(Color.Magenta);
            }
        }

        public Bitmap Bmp3
        {
            get
            {
                return m_Bmp3;
            }
            set
            {
                m_Bmp3 = value;
                if (!ImageAnimator.CanAnimate(m_Bmp3))
                    m_Bmp3.MakeTransparent(Color.Magenta);
            }
        }

        /*
        private void OnFrameChanged(object o, EventArgs e)
        {
            //Force a call to the Paint event handler.
            this.Invalidate();
        }
         * */

        /*
        protected override void OnPaint(PaintEventArgs e)
        {
			Bitmap DisplayedBitmap = null;
            if (FinalyDisp != null && ImageAnimator.CanAnimate(FinalyDisp))
			{
                ImageAnimator.UpdateFrames(FinalyDisp);
				DisplayedBitmap = new Bitmap(FinalyDisp, FinalyDisp.Width, FinalyDisp.Height);
				DisplayedBitmap.MakeTransparent(Color.Magenta);
			}
			if (DisplayedBitmap != null)
			{
				e.Graphics.DrawImage(DisplayedBitmap, new Rectangle(new Point(0, 0), this.Size));
			}
            if (Bmp1 != null && ImageAnimator.CanAnimate(Bmp1))
                ImageAnimator.UpdateFrames(Bmp1);
            if (Bmp2 != null && ImageAnimator.CanAnimate(Bmp2))
                ImageAnimator.UpdateFrames(Bmp2);
            if (Bmp3 != null && ImageAnimator.CanAnimate(Bmp3))
                ImageAnimator.UpdateFrames(Bmp3);
				*/
			/*	
            switch (State)
            {
                default:
                case 0:
                    e.Graphics.DrawImage(Bmp0, new Rectangle(new Point(0, 0), this.Size));
                    break;
                case 1:
                    e.Graphics.DrawImage(Bmp1, new Rectangle(new Point(0, 0), this.Size));
                    break;
                case 2:
                    e.Graphics.DrawImage(Bmp2, new Rectangle(new Point(0, 0), this.Size));
                    break;
                case 3:
                    e.Graphics.DrawImage(Bmp3, new Rectangle(new Point(0, 0), this.Size));
                    break;
            }
        }
             * */
    }
}

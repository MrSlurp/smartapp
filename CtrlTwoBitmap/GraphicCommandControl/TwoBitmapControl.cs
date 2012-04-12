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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="document"></param>
        public TwoBitmapControl(BTDoc document)
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
                m_Ctrl = new TwoBitmap();
                m_Ctrl.Location = m_RectControl.Location;
                m_Ctrl.Name = m_strSymbol;
                m_Ctrl.Size = m_RectControl.Size;
                m_Ctrl.BackColor = Color.Transparent;

                string strImageFullPath = m_Document.PathTr.RelativePathToAbsolute(((TwoBitmapProp)this.m_SpecificProp).NomFichierInactif);
                strImageFullPath = PathTranslator.LinuxVsWindowsPathUse(strImageFullPath);
                try
                {
                    ((TwoBitmap)m_Ctrl).BmpInact = new Bitmap(strImageFullPath);
                }
                catch (Exception)
                {
                    LogEvent log = new LogEvent(LOG_EVENT_TYPE.WARNING, string.Format(DllEntryClass.LangSys.C("Control {0} Failed to load file {1}"), Symbol, strImageFullPath));
                    AddLogEvent(log);
                }

                strImageFullPath = m_Document.PathTr.RelativePathToAbsolute(((TwoBitmapProp)this.m_SpecificProp).NomFichierActif);
                strImageFullPath = PathTranslator.LinuxVsWindowsPathUse(strImageFullPath);
                try
                {
                    ((TwoBitmap)m_Ctrl).BmpAct = new Bitmap(strImageFullPath);
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

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Mess"></param>
        /// <param name="obj"></param>
        /// <param name="TypeApp"></param>
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
    }
}

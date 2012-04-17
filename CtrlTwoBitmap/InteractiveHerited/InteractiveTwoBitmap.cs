using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CommonLib;

namespace CtrlTwoBitmap
{
    public partial class InteractiveTwoBitmap : InteractiveControl, ISpecificControl
    {
        static UserControl m_SpecificPropPanel = new TwoBitmapProperties();
        StandardPropEnabling m_stdPropEnabling = new StandardPropEnabling();
        SpecificGraphicProp m_SpecGraphicProp = new SpecificGraphicProp();

        private string m_strImgInactive = "";
        private Bitmap m_BmpInact;
        private Bitmap m_BmpDefault = TwoImageRes.DefaultImg;
        public InteractiveTwoBitmap()
        {
            InitializeComponent();
            m_SpecificPropPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            m_SpecificPropPanel.AutoSize = true;
            m_SpecificPropPanel.Name = "TwoBitmapProperties";
            m_SpecificPropPanel.Text = "";

            m_stdPropEnabling.m_bcheckReadOnlyChecked = false;
            m_stdPropEnabling.m_bcheckReadOnlyEnabled = false;
            m_stdPropEnabling.m_bcheckScreenEventEnabled = false;
            m_stdPropEnabling.m_bcheckScreenEventChecked = false;
            m_stdPropEnabling.m_bEditAssociateDataEnabled = true;
            m_stdPropEnabling.m_bEditTextEnabled = false;
            m_stdPropEnabling.m_bCtrlEventScriptEnabled = false;

            m_SpecGraphicProp.m_bcanResizeWidth = true;
            m_SpecGraphicProp.m_bcanResizeHeight = true;
            m_SpecGraphicProp.m_MinSize = new Size(5, 5);
            this.ControlType = InteractiveControlType.DllControl;
        }

        public override InteractiveControl CreateNew()
        {
            return new InteractiveTwoBitmap();
        }

        //*****************************************************************************************************
        // Description: accesseur pour le type, modifie atomatiquement les propriété de redimensionement et 
        // la taille du control quand le type change
        //*****************************************************************************************************
        public override InteractiveControlType ControlType
        {
            get
            {
                return InteractiveControlType.DllControl;
            }
        }

        public UserControl SpecificPropPanel
        {
            get
            {
                return m_SpecificPropPanel;
            }
        }

        public StandardPropEnabling StdPropEnabling
        {
            get
            {
                return m_stdPropEnabling;
            }
        }

        public SpecificGraphicProp SpecGraphicProp
        {
            get
            {
                return m_SpecGraphicProp;
            }
        }

        public void SelfPaint(Graphics gr, Control ctrl)
        {
            if (this.SourceBTControl != null)
            {
                try
                {
                    TwoBitmapProp specProps = this.SourceBTControl.SpecificProp as TwoBitmapProp;
                    if (!string.IsNullOrEmpty(specProps.NomFichierInactif))
                    {
                        if (specProps.NomFichierInactif != m_strImgInactive)
                        {
                            m_strImgInactive = SourceBTControl.Document.PathTr.RelativePathToAbsolute(((TwoBitmapProp)this.SourceBTControl.SpecificProp).NomFichierInactif);
                            m_strImgInactive = PathTranslator.LinuxVsWindowsPathUse(m_strImgInactive);
                            string strImageFullPath = m_strImgInactive;
                            m_BmpInact = new Bitmap(strImageFullPath);
                            m_BmpInact.MakeTransparent(Color.Magenta);
                        }
                        gr.DrawImage(m_BmpInact, new Rectangle(new Point(0, 0), this.Size));
                        return;
                    }
                }
                catch (Exception e)
                {
                }
            }
            gr.DrawImage(m_BmpDefault, new Rectangle(new Point(0, 0), this.Size));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            SelfPaint(e.Graphics, this);
            ControlPainter.DrawPresenceAssociateData(e.Graphics, this);
            DrawSelRect(e.Graphics);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
        }

        public override bool IsDllControl
        {
            get
            {
                return true;
            }
        }

        public override uint DllControlID
        {
            get
            {
                return DllEntryClass.TwoBitmap_Control_ID;
            }
        }

    }
}

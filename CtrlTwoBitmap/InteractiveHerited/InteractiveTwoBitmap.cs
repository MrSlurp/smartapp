using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        UserControl m_SpecificPropPanel = new TwoBitmapProperties();
        TwoBitmapProp m_SpecificControlProp = new TwoBitmapProp();
        StandardPropEnabling m_stdPropEnabling = new StandardPropEnabling();
        SpecificGraphicProp m_SpecGraphicProp = new SpecificGraphicProp();

        private string m_strImgInactive = "";
        private Bitmap m_BmpInact;
        private Bitmap m_BmpDefault = CommonLib.Resources.Empty;
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

        public SpecificControlProp SpecificControlProp
        {
            get
            {
                return m_SpecificControlProp;
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
                    if (((TwoBitmapProp)this.SourceBTControl.SpecificProp).NomFichierInactif != m_strImgInactive)
                    {
                        m_strImgInactive = ((TwoBitmapProp)this.SourceBTControl.SpecificProp).NomFichierInactif;
                        m_BmpInact = new Bitmap(m_strImgInactive);
                    }
                    gr.DrawImage(m_BmpInact, new Rectangle(new Point(0, 0), m_BmpInact.Size));
                    return;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    //LogEvent ev = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format("Failed to load image file \" {0} \"", m_strImgInactive));
                }
            }
            gr.DrawImage(m_BmpDefault, new Rectangle(new Point(0, 0), m_BmpDefault.Size));
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

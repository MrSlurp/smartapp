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

namespace DigitalDisplay
{
    public partial class InteractiveDigitalDisplayDllControl : InteractiveControl, ISpecificControl
    {
        static UserControl m_SpecificPropPanel = new DigitalDisplayProperties();
        StandardPropEnabling m_stdPropEnabling = new StandardPropEnabling();
        SpecificGraphicProp m_SpecGraphicProp = new SpecificGraphicProp();

        public InteractiveDigitalDisplayDllControl()
        {
            InitializeComponent();
            m_SpecificPropPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            m_SpecificPropPanel.AutoSize = true;
            m_SpecificPropPanel.Name = "DllControlPropPanel";
            m_SpecificPropPanel.Text = "";

            // modifiez ici les valeur afin que le control rende disponibles les champs standard souhaités
            m_stdPropEnabling.m_bcheckReadOnlyChecked = false;
            m_stdPropEnabling.m_bcheckReadOnlyEnabled = false;
            m_stdPropEnabling.m_bcheckScreenEventEnabled = false;
            m_stdPropEnabling.m_bcheckScreenEventChecked = false;
            m_stdPropEnabling.m_bEditAssociateDataEnabled = true;
            m_stdPropEnabling.m_bEditTextEnabled = false;
            m_stdPropEnabling.m_bCtrlEventScriptEnabled = false;

            // modifiez ici les valeur afin que le control ai la taille min souhaité et ses possibilité de redimensionnement
            m_SpecGraphicProp.m_bcanResizeWidth = true;
            m_SpecGraphicProp.m_bcanResizeHeight = true;
            m_SpecGraphicProp.m_MinSize = new Size(5, 5);
            this.ControlType = InteractiveControlType.DllControl;
        }

        public override InteractiveControl CreateNew()
        {
            return new InteractiveDigitalDisplayDllControl();
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
            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Color crBack = Color.Black;
            if (this.SourceBTControl != null)
            {
                crBack = ((DllDigitalDisplayProp)this.SourceBTControl.SpecificProp).BackColor;
            }
            using (SolidBrush brushBack = new SolidBrush(crBack))
            {
                gr.FillRectangle(brushBack, this.ClientRectangle);
            }
            Color cr = Color.GreenYellow;
            SevenSegmentHelper sevenSegmentHelper = new SevenSegmentHelper(gr);

            string dispText = "1234.5";
            if (this.SourceBTControl != null)
            {
                float diviseur = 1;
                string FormatString = ((DllDigitalDisplayProp)this.SourceBTControl.SpecificProp).FormatString;
                switch (FormatString)
                {
                    case ":F0":
                        break;
                    case ":F1":
                        diviseur = 10;
                        break;
                    case ":F2":
                        diviseur = 100;
                        break;
                    case ":F3":
                        diviseur = 1000;
                        break;
                    default:
                        break;
                }
                float sampleValue = 12345F / diviseur;
                dispText = string.Format("{0" + FormatString + "}", sampleValue);
                cr = ((DllDigitalDisplayProp)this.SourceBTControl.SpecificProp).DigitColor;
            }
            SizeF digitSizeF = sevenSegmentHelper.GetStringSize(dispText, Font);
            float scaleFactor = Math.Min(ClientSize.Width / digitSizeF.Width, ClientSize.Height / digitSizeF.Height);
            Font font = new Font(Font.FontFamily, scaleFactor * Font.SizeInPoints);
            digitSizeF = sevenSegmentHelper.GetStringSize(dispText, font);

            using (SolidBrush brush = new SolidBrush(cr))
            {
                using (SolidBrush lightBrush = new SolidBrush(Color.FromArgb(20, cr)))
                {
                    sevenSegmentHelper.DrawDigits(
                        dispText, font, brush, lightBrush,
                        (ClientSize.Width - digitSizeF.Width) / 2,
                        (ClientSize.Height - digitSizeF.Height) / 2);
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            SelfPaint(e.Graphics, this);
            ControlPainter.DrawPresenceAssociateData(e.Graphics, this);
            DrawSelRect(e.Graphics);
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
                return DigitalDisplay.DllEntryClass.DLL_Control_ID;
            }
        }

    }
}

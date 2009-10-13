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

namespace FormatedDisplay
{
    public partial class InteractiveFormatedDisplayDllControl : InteractiveControl, ISpecificControl
    {
        UserControl m_SpecificPropPanel = new FormatedDisplayProperties();
        StandardPropEnabling m_stdPropEnabling = new StandardPropEnabling();
        SpecificGraphicProp m_SpecGraphicProp = new SpecificGraphicProp();

        public InteractiveFormatedDisplayDllControl()
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
            m_SpecGraphicProp.m_bcanResizeHeight = false;
            m_SpecGraphicProp.m_MinSize = new Size(32, 20);
            this.ControlType = InteractiveControlType.DllControl;
        }

        public override InteractiveControl CreateNew()
        {
            return new InteractiveFormatedDisplayDllControl();
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
            Rectangle DrawRect = ctrl.ClientRectangle;
            DrawRect.Inflate(-2, -2);
            Pen pen = new Pen(Color.FromArgb(127, 157, 185), 1);
            gr.FillRectangle(Brushes.White, DrawRect);
            gr.DrawRectangle(pen, DrawRect);
            string dispText = "1234.5";
            if (this.SourceBTControl != null)
            {
                float diviseur = 1;
                string FormatString = ((DllFormatedDisplayProp)this.SourceBTControl.SpecificProp).FormatString;
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
            }
            SizeF SizeText = gr.MeasureString(dispText, SystemFonts.DefaultFont);
            PointF PtText = new PointF(ctrl.ClientRectangle.Left + 2, (ctrl.Height - SizeText.Height) / 2);
            gr.DrawString(dispText, SystemFonts.DefaultFont, Brushes.Black, PtText);
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
                return FormatedDisplay.DllEntryClass.DLL_Control_ID;
            }
        }

    }
}

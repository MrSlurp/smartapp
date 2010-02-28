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

namespace GradientBaloon
{
    public partial class InteractiveGradientBaloonDllControl : InteractiveControl, ISpecificControl
    {
        UserControl m_SpecificPropPanel = new GradientBaloonProperties();
        StandardPropEnabling m_stdPropEnabling = new StandardPropEnabling();
        SpecificGraphicProp m_SpecGraphicProp = new SpecificGraphicProp();

        public InteractiveGradientBaloonDllControl()
        {
            InitializeComponent();
            m_SpecificPropPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            m_SpecificPropPanel.AutoSize = true;
            m_SpecificPropPanel.Name = "DllControlPropPanel";
            m_SpecificPropPanel.Text = "***.*";

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
            return new InteractiveGradientBaloonDllControl();
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
                
                LinearGradientBrush grBrush = new LinearGradientBrush(this.ClientRectangle, Color.Red, Color.Blue, LinearGradientMode.Vertical);
                RoundRectangle rr = new RoundRectangle(this.ClientRectangle, RoundedCorner.All, 15);
                Pen pn = new Pen(Color.Black, 4);
                GraphicsPath path = rr.ToGraphicsPath();
                gr.FillPath(grBrush, path);
                rr.Inflate(-1, -1);
                path = rr.ToGraphicsPath();
                gr.DrawPath(pn, path);
            }
            else
            {
                LinearGradientBrush grBrush = new LinearGradientBrush(this.ClientRectangle, Color.Red, Color.Blue, LinearGradientMode.Vertical);
                RoundRectangle rr = new RoundRectangle(this.ClientRectangle, RoundedCorner.All, 15);
                Pen pn = new Pen(Color.Black, 4);
                GraphicsPath path = rr.ToGraphicsPath();
                gr.FillPath(grBrush, path);
                rr.Inflate(-1,-1);
                path = rr.ToGraphicsPath();
                gr.DrawPath(pn, path);
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
                return GradientBaloon.DllEntryClass.DLL_Control_ID;
            }
        }
    }
}

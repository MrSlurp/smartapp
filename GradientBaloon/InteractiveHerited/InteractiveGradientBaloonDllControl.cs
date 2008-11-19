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
        DllGradientBaloonProp m_SpecificControlProp = new DllGradientBaloonProp();
        StandardPropEnabling m_stdPropEnabling = new StandardPropEnabling();
        SpecificGraphicProp m_SpecGraphicProp = new SpecificGraphicProp();

        public InteractiveGradientBaloonDllControl()
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
            /*
            Point[] pts = new Point[] { new Point(0, 0), 
                                        new Point(this.Width/4, this.Height/4), 
                                        new Point(this.Width/2, this.Height/2), 
                                        new Point(3*(this.Width/4), 3*(this.Height/4)), 
                                        new Point(this.Width, this.Height) };
             * */
            if (this.SourceBTControl != null)
            {
                // mettez ici le code de dessin du control lorsqu'il est posé dans la surface de dessin
                LinearGradientBrush grBrush = new LinearGradientBrush(this.ClientRectangle, Color.Red, Color.Blue, LinearGradientMode.BackwardDiagonal);
                //PathGradientBrush grBrush = new PathGradientBrush(pts, WrapMode.);
                gr.FillRectangle(grBrush, this.ClientRectangle);
            }
            else
            {
                // mettez ici le code de dessin du control lorsqu'il est dans la barre d'outil
                LinearGradientBrush grBrush = new LinearGradientBrush(this.ClientRectangle, Color.Red, Color.Blue, 90, true);
                //PathGradientBrush grBrush = new PathGradientBrush(pts, WrapMode.Clamp);
                gr.FillRectangle(grBrush, this.ClientRectangle);
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

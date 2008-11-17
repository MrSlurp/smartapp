﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CommonLib;

namespace CtrlSerpentin
{
    public partial class InteractiveSerpentin : InteractiveControl, ISpecificControl
    {
        UserControl m_SpecificPropPanel = new TwoColorProperties();
        TwoColorProp m_SpecificControlProp = new TwoColorProp();
        StandardPropEnabling m_stdPropEnabling = new StandardPropEnabling();
        SpecificGraphicProp m_SpecGraphicProp = new SpecificGraphicProp();

        private Point[] m_ListPoint = new Point[1];

        public InteractiveSerpentin()
        {
            InitializeComponent();
            m_SpecificPropPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            m_SpecificPropPanel.AutoSize = true;
            m_SpecificPropPanel.Name = "TwoColorProperties";
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
            return new InteractiveSerpentin();
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
            Color crbr = Color.Black;
            if (SourceBTControl != null)
                crbr = ((TwoColorProp)SourceBTControl.SpecificProp).ColorInactive;
            else
                crbr = Color.Blue;
            Pen pn = new Pen(crbr, 4);
            gr.SmoothingMode = SmoothingMode.AntiAlias;
            gr.DrawLines(pn, m_ListPoint);
            ControlPainter.DrawPresenceAssociateData(gr, ctrl);
            pn.Dispose();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            SelfPaint(e.Graphics, this);
            DrawSelRect(e.Graphics);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            SerpentinPtsCalculator.CalcSerpentinPts(out m_ListPoint, this.Size);
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
                return DllEntryClass.Serpentin_Control_ID;
            }
        }

    }
}
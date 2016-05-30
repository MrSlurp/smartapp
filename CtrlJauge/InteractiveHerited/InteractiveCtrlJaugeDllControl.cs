/*
    This file is part of SmartApp.

    SmartApp is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    SmartApp is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with Foobar.  If not, see <http://www.gnu.org/licenses/>.
*/
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

namespace CtrlJauge
{
    public partial class InteractiveCtrlJaugeDllControl : InteractiveControl, ISpecificControl
    {
        static UserControl m_SpecificPropPanel = new CtrlJaugeProperties();
        StandardPropEnabling m_stdPropEnabling = new StandardPropEnabling();
        SpecificGraphicProp m_SpecGraphicProp = new SpecificGraphicProp();

        public InteractiveCtrlJaugeDllControl()
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
            m_SpecGraphicProp.m_MinSize = new Size(15, 15);
            this.ControlType = InteractiveControlType.DllControl;
        }

        public override InteractiveControl CreateNew()
        {
            return new InteractiveCtrlJaugeDllControl();
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
                Rectangle Rect = this.ClientRectangle;
                Color RectMaxColor = ((DllCtrlJaugeProp)this.SourceBTControl.SpecificProp).ColorMax;
                Color RectMinColor = ((DllCtrlJaugeProp)this.SourceBTControl.SpecificProp).ColorMin;
                Rectangle RectMin = new Rectangle();
                Rectangle RectMax = new Rectangle();
                switch (((DllCtrlJaugeProp)this.SourceBTControl.SpecificProp).Orientation)
                {
                    default:
                    case eOrientationJauge.eHorizontaleDG:
                        RectMin.Width = this.ClientRectangle.Width / 2;
                        RectMax.Width = this.ClientRectangle.Width / 2;
                        RectMin.Height = this.ClientRectangle.Height;
                        RectMax.Height = this.ClientRectangle.Height;
                        RectMin.X = this.ClientRectangle.X;
                        RectMax.X = this.ClientRectangle.X + RectMin.Width;
                        RectMin.Y = RectMax.Y = this.ClientRectangle.Y;
                        break;
                    case eOrientationJauge.eHorizontaleGD:
                        RectMin.Width = this.ClientRectangle.Width / 2;
                        RectMax.Width = this.ClientRectangle.Width / 2;
                        RectMin.Height = this.ClientRectangle.Height;
                        RectMax.Height = this.ClientRectangle.Height;
                        RectMin.X = this.ClientRectangle.X + RectMax.Width;
                        RectMax.X = this.ClientRectangle.X;
                        RectMin.Y = RectMax.Y = this.ClientRectangle.Y;
                        break;
                    case eOrientationJauge.eVerticaleTB:
                        RectMin.Width = this.ClientRectangle.Width;
                        RectMax.Width = this.ClientRectangle.Width;
                        RectMin.Height = this.ClientRectangle.Height / 2;
                        RectMax.Height = this.ClientRectangle.Height / 2;
                        RectMin.Y = this.ClientRectangle.Y;
                        RectMax.Y = this.ClientRectangle.Y + RectMin.Height;
                        RectMin.X = RectMax.X = this.ClientRectangle.X;
                        break;
                    case eOrientationJauge.eVerticaleBT:
                        RectMin.Width = this.ClientRectangle.Width;
                        RectMax.Width = this.ClientRectangle.Width;
                        RectMin.Height = this.ClientRectangle.Height / 2;
                        RectMax.Height = this.ClientRectangle.Height / 2;
                        RectMin.Y = this.ClientRectangle.Y + RectMax.Height;
                        RectMax.Y = this.ClientRectangle.Y;
                        RectMin.X = RectMax.X = this.ClientRectangle.X;
                        break;
                }
                gr.FillRectangle(new SolidBrush(RectMinColor), RectMin);
                gr.FillRectangle(new SolidBrush(RectMaxColor), RectMax);

                Pen pn = new Pen(Color.Black, 4);
                Rect.Inflate(-1, -1);
                gr.DrawRectangle(pn, Rect);
            }
            else
            {
                Rectangle Rect = this.ClientRectangle;
                Color RectMaxColor = Color.White;
                Color RectMinColor = Color.Blue;
                Rectangle RectLeft = new Rectangle(this.ClientRectangle.Location, this.ClientRectangle.Size);
                Rectangle RectRight = new Rectangle(this.ClientRectangle.Location, this.ClientRectangle.Size);
                RectRight.Width = RectLeft.Width = this.ClientRectangle.Width/2;
                RectRight.X = this.ClientRectangle.X + RectRight.Width;
                gr.FillRectangle(new SolidBrush(RectMinColor), RectLeft);
                gr.FillRectangle(new SolidBrush(RectMaxColor), RectRight);
                Pen pn = new Pen(Color.Black, 4);
                Rect.Inflate(-1, -1);
                gr.DrawRectangle(pn, Rect);
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
                return CtrlJauge.DllEntryClass.DLL_Control_ID;
            }
        }

    }
}

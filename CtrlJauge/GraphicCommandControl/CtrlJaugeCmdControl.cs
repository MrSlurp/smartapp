using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using CommonLib;

namespace CtrlJauge
{
    internal class CtrlJaugeCmdControl : BTDllCtrlJaugeControl
    {
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public CtrlJaugeCmdControl(BTDoc document)
            : base(document)
        {

        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public override void CreateControl()
        {
            if (m_Ctrl == null)
            {
                m_Ctrl = new CtrlJaugeDispCtrl();
                m_Ctrl.Location = m_RectControl.Location;
                m_Ctrl.Name = m_strSymbol;
                m_Ctrl.Size = m_RectControl.Size;
                m_Ctrl.BackColor = Color.Transparent;
                if (m_AssociateData != null)
                {
                    ((CtrlJaugeDispCtrl)m_Ctrl).Value = m_AssociateData.Value;
                    ((CtrlJaugeDispCtrl)m_Ctrl).MinVal = m_AssociateData.Minimum;
                    ((CtrlJaugeDispCtrl)m_Ctrl).MaxVal = m_AssociateData.Maximum;
                }
                ((CtrlJaugeDispCtrl)m_Ctrl).Orientation = ((DllCtrlJaugeProp)SpecificProp).Orientation;
                ((CtrlJaugeDispCtrl)m_Ctrl).ColorMin = ((DllCtrlJaugeProp)SpecificProp).ColorMin;
                ((CtrlJaugeDispCtrl)m_Ctrl).ColorMax = ((DllCtrlJaugeProp)SpecificProp).ColorMax;
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
                ((CtrlJaugeDispCtrl)m_Ctrl).Value = m_AssociateData.Value;
            }
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
                        // traitez ici le passage en mode stop du control si nécessaire
                        break;
                    case MESSAGE.MESS_CMD_RUN:
                        // traitez ici le passage en mode run du control si nécessaire
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public class CtrlJaugeDispCtrl : UserControl
    {
        // ajouter ici les données membres du control affiché
        int m_Value = 50;
        int m_MinVal = 0;
        int m_MaxVal = 100;
        eOrientationJauge m_Orientation = eOrientationJauge.eHorizontaleDG;
        Color m_ColorMax = Color.White;
        Color m_ColorMin = Color.Blue;

        public int Value
        {
            get
            {
                return m_Value;
            }
            set
            {
                if (m_Value != value)
                {
                    m_Value = value;
                    Refresh();
                }
            }
        }

        public int MinVal
        {
            get
            {
                return m_MinVal;
            }
            set
            {
                m_MinVal = value;
            }
        }

        public int MaxVal
        {
            get
            {
                return m_MaxVal;
            }
            set
            {
                m_MaxVal = value;
            }
        }

        public eOrientationJauge Orientation
        {
            get
            { return m_Orientation; }
            set
            { m_Orientation = value; }
        }

        public Color ColorMin
        {
            get
            { return m_ColorMin; }
            set
            { m_ColorMin = value; }
        }

        public Color ColorMax
        {
            get
            { return m_ColorMax; }
            set
            { m_ColorMax = value; }
        }

        public CtrlJaugeDispCtrl()
        {
            this.DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Brush grBrushMin = new SolidBrush(ColorMin);
            Brush grBrushMax = new SolidBrush(ColorMax);
            Rectangle RectMin;
            Rectangle RectMax;
            CalcParts(this.ClientRectangle, out RectMin, out RectMax);
            e.Graphics.FillRectangle(grBrushMin, RectMin);
            e.Graphics.FillRectangle(grBrushMax, RectMax);
            Rectangle rect = this.ClientRectangle;
            Pen pn = new Pen(Color.Black, 4);
            rect.Inflate(-1, -1);
            e.Graphics.DrawRectangle(pn, rect);
        }

        private void CalcParts(Rectangle fullRect, out Rectangle RectMin, out Rectangle RectMax)
        {
            float Delta = m_MaxVal - m_MinVal;
            float nbPixelPercent = ((float)this.Height / Delta);

            float ValPercent = 0;
            if (m_MinVal < 0 && m_MaxVal >= 0)
                ValPercent = (float)(((float)m_Value - (float)m_MinVal) / (float)Delta) * 100;
            else
                ValPercent = (float)(((float)m_Value - (float)m_MinVal) / (float)Delta) * 100;

            float TmpMin = (ValPercent / (float)100);
            float TmpMax = (100 - ValPercent) / (float)100;
            if (TmpMin < 0.001F)
                TmpMin = 0.001F;
            if (TmpMin > 0.999F)
                TmpMin = 0.999F;

            if (TmpMax < 0.001F)
                TmpMax = 0.001F;
            if (TmpMax > 0.999F)
                TmpMax = 0.999F;
            float sum = TmpMin + TmpMax;
            int HeightWidthMin = 0;
            int HeightWidthMax = 0;
            RectMin = new Rectangle();
            RectMax = new Rectangle();
            switch (Orientation)
            {
                default:
                case eOrientationJauge.eHorizontaleDG:
                    HeightWidthMin = (int)(this.Width * TmpMin);
                    HeightWidthMax = (int)(this.Width * TmpMax);
                    RectMin.Width = HeightWidthMin;
                    RectMax.Width = HeightWidthMax;
                    RectMin.Height = this.ClientRectangle.Height;
                    RectMax.Height = this.ClientRectangle.Height;
                    RectMin.X = this.ClientRectangle.X;
                    RectMax.X = this.ClientRectangle.X + RectMin.Width;
                    RectMin.Y = RectMax.Y = this.ClientRectangle.Y;
                    break;
                case eOrientationJauge.eHorizontaleGD:
                    HeightWidthMin = (int)(this.Width * TmpMin);
                    HeightWidthMax = (int)(this.Width * TmpMax);
                    RectMin.Width = HeightWidthMin;
                    RectMax.Width = HeightWidthMax;
                    RectMin.Height = this.ClientRectangle.Height;
                    RectMax.Height = this.ClientRectangle.Height;
                    RectMin.X = this.ClientRectangle.X + RectMax.Width;
                    RectMax.X = this.ClientRectangle.X;
                    RectMin.Y = RectMax.Y = this.ClientRectangle.Y;
                    break;
                case eOrientationJauge.eVerticaleTB:
                    HeightWidthMin = (int)(this.Height * TmpMin);
                    HeightWidthMax = (int)(this.Height * TmpMax);
                    RectMin.Width = this.ClientRectangle.Width;
                    RectMax.Width = this.ClientRectangle.Width;
                    RectMin.Height = HeightWidthMin;
                    RectMax.Height = HeightWidthMax;
                    RectMin.Y = this.ClientRectangle.Y;
                    RectMax.Y = this.ClientRectangle.Y + RectMin.Height;
                    RectMin.X = RectMax.X = this.ClientRectangle.X;
                    break;
                case eOrientationJauge.eVerticaleBT:
                    HeightWidthMin = (int)(this.Height * TmpMin);
                    HeightWidthMax = (int)(this.Height * TmpMax);
                    RectMin.Width = this.ClientRectangle.Width;
                    RectMax.Width = this.ClientRectangle.Width;
                    RectMin.Height = HeightWidthMin;
                    RectMax.Height = HeightWidthMax;
                    RectMin.Y = this.ClientRectangle.Y + RectMax.Height;
                    RectMax.Y = this.ClientRectangle.Y;
                    RectMin.X = RectMax.X = this.ClientRectangle.X;
                    break;
            }
        }
    }
}

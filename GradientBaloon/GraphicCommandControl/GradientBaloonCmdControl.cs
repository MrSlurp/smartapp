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
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using CommonLib;

namespace GradientBaloon
{
    internal class GradientBaloonCmdControl : BTDllGradientBaloonControl
    {
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public GradientBaloonCmdControl(BTDoc document)
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
                m_Ctrl = new GradientBaloonDispCtrl();
                m_Ctrl.Location = m_RectControl.Location;
                m_Ctrl.Name = m_strSymbol;
                m_Ctrl.Size = m_RectControl.Size;
                m_Ctrl.BackColor = Color.Transparent;
                if (m_AssociateData != null)
                {
                    ((GradientBaloonDispCtrl)m_Ctrl).Value = m_AssociateData.Value;
                    ((GradientBaloonDispCtrl)m_Ctrl).MinVal = m_AssociateData.Minimum;
                    ((GradientBaloonDispCtrl)m_Ctrl).MaxVal = m_AssociateData.Maximum;
                }
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
                ((GradientBaloonDispCtrl)m_Ctrl).Value = m_AssociateData.Value;
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

    public class GradientBaloonDispCtrl : DrawInParentCmdCtrl
    {
        // ajouter ici les données membres du control affiché
        int m_Value = 50;
        int m_MinVal = 0;
        int m_MaxVal = 100;

        const int PercentCenterPart = 10;

        public GradientBaloonDispCtrl()
        {
            m_bDrawInParent = true;
        }

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
                    Invalidate();
                    this.CallEventRefresh();
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

        public override void OnPaintInParent(Graphics gr, Rectangle rect)
        {
            Brush grBrushTop = new SolidBrush(Color.Red);
            Brush grBrushBottom = new SolidBrush(Color.Blue);
            RoundRectangle rr = new RoundRectangle(rect, RoundedCorner.All, 15);
            RoundRectangle TopRRect;
            Rectangle      CenterRect;
            RoundRectangle BottomRRect;
            Calc3Parts(rr, out TopRRect, out CenterRect, out BottomRRect, 15);
            LinearGradientBrush grGradientBrush = new LinearGradientBrush(CenterRect, Color.Red, Color.Blue, LinearGradientMode.Vertical);
            gr.FillPath(grBrushBottom, BottomRRect.ToGraphicsPath());
            gr.FillRectangle(grGradientBrush, CenterRect);
            gr.FillPath(grBrushTop, TopRRect.ToGraphicsPath());
            Pen pn = new Pen(Color.Black, 4);
            rr.Inflate(-1, -1);
            gr.DrawPath(pn, rr.ToGraphicsPath());
        }

        private void Calc3Parts(RoundRectangle fullRect, out RoundRectangle partTop, out Rectangle partCenter, out RoundRectangle partBottom, float radius)
        {
            float Delta = m_MaxVal - m_MinVal;
            float nbPixelPercent = ((float)this.Height / Delta);

            float ValPercent = 0; 
            if (m_MinVal < 0 && m_MaxVal >= 0)
                ValPercent = (float)(((float)m_Value - (float)m_MinVal) / (float)Delta) * 100;
            else
                ValPercent = (float)(((float)m_Value - (float)m_MinVal) / (float)Delta) * 100;

            float Tmp1 = (ValPercent / 100) - ((float)(PercentCenterPart / 2) / (float)100);
            float Tmp2 = (float)PercentCenterPart/(float)100;
            float Tmp3 = (100 - ValPercent) / 100 - ((float)(PercentCenterPart/2) / (float)100);
            // on laisse 10% car c'est la taille prise par le dégradé, il est placé en dessous de la zone supérieur
            if (Tmp1 < 0.10f)
                Tmp1 = 0.10F;
            if (Tmp1 > 0.80f)
                Tmp1 = 0.80F;

            if (Tmp3 < 0.10f)
                Tmp3 = 0.10F;
            if (Tmp3 > 0.80f)
                Tmp3 = 0.80F;
            float sum = Tmp1 + Tmp2 + Tmp3;
            int heightTop = (int)(this.Height * Tmp1);
            int heightCenter = (int)(this.Height * Tmp2);
            int heightBottom = (int)(this.Height * Tmp3);
            int TotalHeight = heightTop + heightCenter + heightBottom;
            int offsetVertGradient = 0;
            if (TotalHeight < fullRect.Height)
            {
                heightCenter += fullRect.Height - TotalHeight;
                offsetVertGradient = -((fullRect.Height - TotalHeight) / 2);
            }
            partTop = new RoundRectangle(new Point(fullRect.X, fullRect.Y),
                                         new Size(fullRect.Width, heightTop+1),
                                         RoundedCorner.TopLeft | RoundedCorner.TopRight, 
                                         radius);
            partCenter = new Rectangle(fullRect.X, fullRect.Y + heightTop + offsetVertGradient, fullRect.Width, heightCenter);
            int yPartBottom = (fullRect.Y + heightTop + heightCenter);
            if (partCenter.Bottom < yPartBottom)
            {
                yPartBottom = partCenter.Bottom;
                heightBottom += (fullRect.Y + heightTop + heightCenter) - partCenter.Bottom;
            }
            partBottom = new RoundRectangle(new Point(fullRect.X, yPartBottom),
                                            new Size(fullRect.Width, heightBottom),
                                            RoundedCorner.BottomLeft | RoundedCorner.BottomRight,
                                            radius);
        }
    }
}

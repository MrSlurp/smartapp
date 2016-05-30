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
using System.Windows.Forms;
using System.Drawing;

namespace CommonLib
{
    //*****************************************************************************************************
    // Description: cette classe static intègre les fonctions permettant de dessiner les apects des InteractiveControls
    //*****************************************************************************************************
    public static class ControlPainter
    {
        private static Color ColorTourCombo = Color.FromArgb(127, 157, 185);
        public static Color TransparencyColor = Color.FromArgb(255, 0, 255);

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public static void DrawComboBox(Graphics gr, Control ctrl)
        {
            DrawBaseComboNumUpDown(gr, ctrl);

            Rectangle DrawRect = ctrl.ClientRectangle;
            DrawRect.Inflate(-1, -1);
            Bitmap BtnImage = Resources.DropBtn;
            if (BtnImage != null)
            {
                BtnImage.MakeTransparent(TransparencyColor);
                Rectangle rect = new Rectangle(DrawRect.Right - BtnImage.Width, DrawRect.Top + 1,
                    BtnImage.Width, DrawRect.Height - 1);
                gr.DrawImage(BtnImage, rect);

                SizeF SizeText = gr.MeasureString(ctrl.Text, ctrl.Font);
                PointF PtText = new PointF(ctrl.ClientRectangle.Left + 2, (ctrl.Height - SizeText.Height) / 2);
                Brush br = new SolidBrush(ctrl.ForeColor);
                gr.DrawString(ctrl.Text, ctrl.Font, br, PtText);
                DrawPresenceAssociateData(gr, ctrl);
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public static void DrawCheckBox(Graphics gr, Control ctrl)
        {
            
            Rectangle DrawRect = ctrl.ClientRectangle;
            DrawRect.Inflate(-1, -1);
            Bitmap CheckImage = Resources.CheckBox;
            if (CheckImage != null)
            {
                CheckImage.MakeTransparent(TransparencyColor);
                Rectangle rect = new Rectangle(DrawRect.Left, (DrawRect.Height - CheckImage.Height)/2,
                                               CheckImage.Width, CheckImage.Height);
                gr.DrawImage(CheckImage, rect);

                SizeF SizeText = gr.MeasureString(ctrl.Text, ctrl.Font);
                PointF PtText = new PointF(ctrl.ClientRectangle.Left + CheckImage.Width + 2, (ctrl.Height - SizeText.Height) / 2);
                Brush br = new SolidBrush(ctrl.ForeColor);
                gr.DrawString(ctrl.Text, ctrl.Font, br, PtText);
                DrawPresenceAssociateData(gr, ctrl);
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public static void DrawSlider(Graphics gr, Control ctrl)
        {
            Rectangle DrawRect = ctrl.ClientRectangle;
            DrawRect.Inflate(-1, -1);
            Bitmap SliderBar = Resources.SliderBar;
            if (SliderBar != null)
            {
                SliderBar.MakeTransparent(TransparencyColor);
                Bitmap SliderCursor = Resources.SliderCursor;
                SliderCursor.MakeTransparent(TransparencyColor);
                Rectangle SliderBarRect = new Rectangle(DrawRect.Top + 7, DrawRect.Left + 7, DrawRect.Width - 14, SliderBar.Height);
                Rectangle SliderCursorRect = new Rectangle(DrawRect.Top + 15, DrawRect.Left + 0, SliderCursor.Width, SliderCursor.Height);
                gr.DrawImage(SliderBar, SliderBarRect);
                gr.DrawImage(SliderCursor, SliderCursorRect);
                DrawPresenceAssociateData(gr, ctrl);
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public static void DrawButton(Graphics gr, Control ctrl)
        {
            Rectangle DrawRect = ctrl.ClientRectangle;
            DrawRect.Inflate(-1, -1);

            gr.FillRectangle(Brushes.White, DrawRect);

            Bitmap BtnTop = Resources.TopBtn;
            Bitmap BtnLeft = Resources.LeftBtn;
            Bitmap BtnRight = Resources.RightBtn;
            Bitmap BtnBottom = Resources.BottomBtn;
            Bitmap BtnTopLeft = Resources.TopLeftBtn;
            Bitmap BtnTopRight = Resources.TopRightBtn;
            Bitmap BtnBottomRight = Resources.BottomRightBtn;
            Bitmap BtnBottomLeft = Resources.BottomLeftBtn;
            if (BtnTop != null
                && BtnLeft != null
                && BtnRight != null
                && BtnBottom != null
                && BtnTopLeft != null
                && BtnTopRight != null
                && BtnBottomRight != null
                && BtnBottomLeft != null
                )
            {
                Rectangle BtnTopRect = new Rectangle(DrawRect.Left + BtnTopLeft.Width, DrawRect.Top, DrawRect.Width - BtnTopLeft.Width - BtnTopRight.Width, BtnTop.Height);
                Rectangle BtnLeftRect = new Rectangle(DrawRect.Left, DrawRect.Top + BtnTopLeft.Height, BtnLeft.Width, DrawRect.Height - BtnTopLeft.Height - BtnBottomLeft.Height);
                Rectangle BtnRightRect = new Rectangle(DrawRect.Right - BtnRight.Width, DrawRect.Top + BtnTopRight.Height, BtnRight.Width, DrawRect.Height - BtnTopRight.Height - BtnBottomRight.Height);
                Rectangle BtnBottomRect = new Rectangle(DrawRect.Left + BtnBottomLeft.Width, DrawRect.Bottom - BtnBottom.Height, DrawRect.Width - BtnBottomLeft.Width - BtnBottomRight.Width, BtnBottom.Height);
                Rectangle BtnTopLeftRect = new Rectangle(DrawRect.Left, DrawRect.Top, BtnTopLeft.Width, BtnTopLeft.Height);
                Rectangle BtnTopRightRect = new Rectangle(DrawRect.Right - BtnTopRight.Width, DrawRect.Top, BtnTopRight.Width, BtnTopRight.Height);
                Rectangle BtnBottomRightRect = new Rectangle(DrawRect.Right - BtnBottomRight.Width, DrawRect.Bottom - BtnBottomRight.Height, BtnBottomRight.Width, BtnBottomRight.Height);
                Rectangle BtnBottomLeftRect = new Rectangle(DrawRect.Left, DrawRect.Bottom - BtnBottomLeft.Height, BtnBottomLeft.Width, BtnBottomLeft.Height);
                gr.DrawImage(BtnTop, BtnTopRect);
                gr.DrawImage(BtnLeft, BtnLeftRect);
                gr.DrawImage(BtnRight, BtnRightRect);
                gr.DrawImage(BtnBottom, BtnBottomRect);
                gr.DrawImage(BtnTopLeft, BtnTopLeftRect);
                gr.DrawImage(BtnTopRight, BtnTopRightRect);
                gr.DrawImage(BtnBottomRight, BtnBottomRightRect);
                gr.DrawImage(BtnBottomLeft, BtnBottomLeftRect);
                SizeF SizeText = gr.MeasureString(ctrl.Text, ctrl.Font);
                PointF PtText = new PointF((ctrl.Width - SizeText.Width) / 2, (ctrl.Height - SizeText.Height) / 2);
                Brush br = new SolidBrush(ctrl.ForeColor);
                gr.DrawString(ctrl.Text, ctrl.Font, br, PtText);
                DrawPresenceAssociateData(gr, ctrl);
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public static void DrawText(Graphics gr, Control ctrl)
        {
            SizeF SizeText = gr.MeasureString(ctrl.Text, ctrl.Font);
            PointF PtText = new PointF(ctrl.ClientRectangle.Left, (ctrl.Height - SizeText.Height) / 2);
            Brush br = new SolidBrush(ctrl.ForeColor);
            gr.DrawString(ctrl.Text, ctrl.Font, br, PtText);
            DrawPresenceAssociateData(gr, ctrl);
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public static void DrawNumUpDown(Graphics gr, Control ctrl)
        {
            DrawBaseComboNumUpDown(gr, ctrl);

            Rectangle DrawRect = ctrl.ClientRectangle;
            DrawRect.Inflate(-1, -1);
            Bitmap BtnImage = Resources.UpDownBtn;
            if (BtnImage != null)
            {
                BtnImage.MakeTransparent(TransparencyColor);
                Rectangle rect = new Rectangle(DrawRect.Right - BtnImage.Width, DrawRect.Top + 1,
                    BtnImage.Width, DrawRect.Height - 1);
                gr.DrawImage(BtnImage, rect);

                SizeF SizeText = gr.MeasureString("1234", ctrl.Font);
                PointF PtText = new PointF(ctrl.ClientRectangle.Left + 2, (ctrl.Height - SizeText.Height) / 2);
                Brush br = new SolidBrush(ctrl.ForeColor);
                gr.DrawString("1234", ctrl.Font, br, PtText);
                DrawPresenceAssociateData(gr, ctrl);
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private static void DrawBaseComboNumUpDown(Graphics gr, Control ctrl)
        {
            Rectangle DrawRect = ctrl.ClientRectangle;
            DrawRect.Inflate(-1, -1);
            Pen pen = new Pen(ColorTourCombo, 1);
            gr.FillRectangle(Brushes.White, DrawRect);
            gr.DrawRectangle(pen, DrawRect);
        }


        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public static void DrawPresenceAssociateData(Graphics gr, Control ctrl)
        {
            if (DropableItems.AllowedItem(ctrl.GetType())
                && ((InteractiveControl)ctrl).SourceBTControl != null)
            {
                InteractiveControl iCtrl = (InteractiveControl)ctrl;
                if (!string.IsNullOrEmpty(iCtrl.SourceBTControl.AssociateData))
                {
                    Rectangle DrawRect = ctrl.ClientRectangle;
                    Bitmap Image = Resources.PresAssData;
                    if (Image != null)
                    {
                        Image.MakeTransparent(TransparencyColor);
                        Rectangle rect = new Rectangle(DrawRect.Right - Image.Width, DrawRect.Top,
                            Image.Width, Image.Height);
                        gr.DrawImage(Image, rect);
                    }
                }
            }
        }
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public static void DrawPresenceAssociateData(Graphics gr, Control ctrl, bool AssocOK)
        {
            if (DropableItems.AllowedItem(ctrl.GetType())
                && ((InteractiveControl)ctrl).SourceBTControl != null)
            {
                InteractiveControl iCtrl = (InteractiveControl)ctrl;
                if (AssocOK)
                {
                    Rectangle DrawRect = ctrl.ClientRectangle;
                    Bitmap Image = Resources.PresAssData;
                    if (Image != null)
                    {
                        Image.MakeTransparent(TransparencyColor);
                        Rectangle rect = new Rectangle(DrawRect.Right - Image.Width, DrawRect.Top,
                            Image.Width, Image.Height);
                        gr.DrawImage(Image, rect);
                    }
                }
            }
        }

    }
}

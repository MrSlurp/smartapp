using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace SmartApp.Ihm.Designer
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
            Bitmap BtnImage = global::SmartApp.Properties.Resources.DropBtn;
            BtnImage.MakeTransparent(TransparencyColor);
            Rectangle rect = new Rectangle(DrawRect.Right - BtnImage.Width, DrawRect.Top + 1,
                BtnImage.Width, DrawRect.Height-1);
            gr.DrawImage(BtnImage, rect);

            SizeF SizeText = gr.MeasureString(ctrl.Text, SystemFonts.DefaultFont);
            PointF PtText = new PointF(ctrl.ClientRectangle.Left + 2, (ctrl.Height - SizeText.Height) / 2);
            gr.DrawString(ctrl.Text, SystemFonts.DefaultFont, Brushes.Black, PtText);
            DrawPresenceAssociateData(gr, ctrl);
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public static void DrawCheckBox(Graphics gr, Control ctrl)
        {
            
            Rectangle DrawRect = ctrl.ClientRectangle;
            DrawRect.Inflate(-1, -1);
            Bitmap CheckImage = global::SmartApp.Properties.Resources.CheckBox;
            CheckImage.MakeTransparent(TransparencyColor);
            Rectangle rect = new Rectangle(DrawRect.Left, DrawRect.Top + 2,
                                           CheckImage.Width, CheckImage.Height);
            gr.DrawImage(CheckImage, rect);
            
            SizeF SizeText = gr.MeasureString(ctrl.Text, SystemFonts.DefaultFont);
            PointF PtText = new PointF(ctrl.ClientRectangle.Left+ CheckImage.Width + 2, (ctrl.Height - SizeText.Height) / 2);
            gr.DrawString(ctrl.Text, SystemFonts.DefaultFont, Brushes.Black, PtText);
            DrawPresenceAssociateData(gr, ctrl);

        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public static void DrawSlider(Graphics gr, Control ctrl)
        {
            Rectangle DrawRect = ctrl.ClientRectangle;
            DrawRect.Inflate(-1, -1);
            Bitmap SliderBar = global::SmartApp.Properties.Resources.SliderBar;
            SliderBar.MakeTransparent(TransparencyColor);
            Bitmap SliderCursor = global::SmartApp.Properties.Resources.SliderCursor;
            SliderCursor.MakeTransparent(TransparencyColor);
            Rectangle SliderBarRect = new Rectangle(DrawRect.Top + 7, DrawRect.Left + 7, DrawRect.Width - 14, SliderBar.Height);
            Rectangle SliderCursorRect = new Rectangle(DrawRect.Top + 15, DrawRect.Left + 0, SliderCursor.Width, SliderCursor.Height);
            gr.DrawImage(SliderBar, SliderBarRect);
            gr.DrawImage(SliderCursor, SliderCursorRect);
            DrawPresenceAssociateData(gr, ctrl);
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public static void DrawButton(Graphics gr, Control ctrl)
        {
            Rectangle DrawRect = ctrl.ClientRectangle;
            DrawRect.Inflate(-1, -1);

            Pen pen = new Pen(ColorTourCombo, 1);
            gr.FillRectangle(Brushes.White, DrawRect);

            Bitmap BtnTop = global::SmartApp.Properties.Resources.TopBtn;
            Bitmap BtnLeft = global::SmartApp.Properties.Resources.LeftBtn;
            Bitmap BtnRight = global::SmartApp.Properties.Resources.RightBtn;
            Bitmap BtnBottom = global::SmartApp.Properties.Resources.BottomBtn;
            Bitmap BtnTopLeft = global::SmartApp.Properties.Resources.TopLeftBtn;
            Bitmap BtnTopRight = global::SmartApp.Properties.Resources.TopRightBtn;
            Bitmap BtnBottomRight = global::SmartApp.Properties.Resources.BottomRightBtn;
            Bitmap BtnBottomLeft = global::SmartApp.Properties.Resources.BottomLeftBtn;
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
            SizeF SizeText = gr.MeasureString(ctrl.Text, SystemFonts.DefaultFont);
            PointF PtText = new PointF((ctrl.Width - SizeText.Width) / 2, (ctrl.Height - SizeText.Height) / 2);
            gr.DrawString(ctrl.Text, SystemFonts.DefaultFont, Brushes.Black,PtText);
            DrawPresenceAssociateData(gr, ctrl);
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public static void DrawText(Graphics gr, Control ctrl)
        {
            SizeF SizeText = gr.MeasureString(ctrl.Text, SystemFonts.DefaultFont);
            PointF PtText = new PointF(ctrl.ClientRectangle.Left, (ctrl.Height - SizeText.Height) / 2);
            gr.DrawString(ctrl.Text, SystemFonts.DefaultFont, Brushes.Black, PtText);
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
            Bitmap BtnImage = global::SmartApp.Properties.Resources.UpDownBtn;
            BtnImage.MakeTransparent(TransparencyColor);
            Rectangle rect = new Rectangle(DrawRect.Right - BtnImage.Width, DrawRect.Top + 1,
                BtnImage.Width, DrawRect.Height - 1);
            gr.DrawImage(BtnImage, rect);

            SizeF SizeText = gr.MeasureString("0", SystemFonts.DefaultFont);
            PointF PtText = new PointF(ctrl.ClientRectangle.Left + 2, (ctrl.Height - SizeText.Height) / 2);
            gr.DrawString("1234", SystemFonts.DefaultFont, Brushes.Black, PtText);
            DrawPresenceAssociateData(gr, ctrl);
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
        private static void DrawPresenceAssociateData(Graphics gr, Control ctrl)
        {
            if (ctrl.GetType() == typeof(InteractiveControl)
                && ((InteractiveControl)ctrl).SourceBTControl != null)
            {
                InteractiveControl iCtrl = (InteractiveControl)ctrl;
                if (!string.IsNullOrEmpty(iCtrl.SourceBTControl.AssociateData))
                {
                    Rectangle DrawRect = ctrl.ClientRectangle;
                    Bitmap Image = global::SmartApp.Properties.Resources.PresAssData;
                    Image.MakeTransparent(TransparencyColor);
                    Rectangle rect = new Rectangle(DrawRect.Right - Image.Width, DrawRect.Top,
                        Image.Width, Image.Height);
                    switch (iCtrl.ControlType)
                    {
                        case InteractiveControlType.Button:
                        case InteractiveControlType.CheckBox:
                        case InteractiveControlType.Combo:
                        case InteractiveControlType.NumericUpDown:
                        case InteractiveControlType.Slider:
                            gr.DrawImage(Image, rect);
                            break;
                        case InteractiveControlType.Text:
                            // rien a faire, pas de donnée associée possible
                            break;
                    }
                }
            }
        }
    }
}

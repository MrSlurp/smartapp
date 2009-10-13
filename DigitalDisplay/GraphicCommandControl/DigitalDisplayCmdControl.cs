using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using CommonLib;

namespace DigitalDisplay
{
    internal class DigitalDisplayCmdControl : BTDllDigitalDisplayControl
    {
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public DigitalDisplayCmdControl()
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
                m_Ctrl = new DigitalDisplayDispCtrl();
                m_Ctrl.Location = m_RectControl.Location;
                m_Ctrl.Name = m_strSymbol;
                m_Ctrl.Size = m_RectControl.Size;
                m_Ctrl.BackColor = Color.Transparent;
                ((DigitalDisplayDispCtrl)m_Ctrl).FormatString = ((DllDigitalDisplayProp)this.m_SpecificProp).FormatString;
                ((DigitalDisplayDispCtrl)m_Ctrl).DigitColor = ((DllDigitalDisplayProp)this.m_SpecificProp).DigitColor;
                ((DigitalDisplayDispCtrl)m_Ctrl).LocalBackColor = ((DllDigitalDisplayProp)this.m_SpecificProp).BackColor;
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
                ((DigitalDisplayDispCtrl)m_Ctrl).Value = m_AssociateData.Value;
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

    public class DigitalDisplayDispCtrl : UserControl
    {
		private Color m_digitColor = Color.GreenYellow;
        Color m_BackColor = Color.Black;
        string m_FormatString = ":F0";
        private string m_digitText = "0";
        int m_Value = 0;

        public int Value
        {
            get
            {
                return m_Value;
            }
            set
            {
                m_Value = value;
                string dispText = "0";
                float diviseur = 1;
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
                float DispValue = m_Value / diviseur;
                dispText = string.Format("{0" + FormatString + "}", DispValue);
                DigitText = dispText;
            }
        }

        public string FormatString
        {
            get
            { return m_FormatString; }
            set
            { m_FormatString = value; }
        }

		public Color DigitColor
		{
			get 
            { return m_digitColor; }
			set 
            { m_digitColor = value; Invalidate(); }
		}

        public Color LocalBackColor
        {
            get
            { return m_BackColor; }
            set
            { m_BackColor = value; }
        }


		protected string DigitText
		{
			get 
            { return m_digitText; }
			set 
            { m_digitText = value; Refresh(); }
		}

        public DigitalDisplayDispCtrl()
		{
			this.SetStyle(ControlStyles.DoubleBuffer, true);
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			this.SetStyle(ControlStyles.ResizeRedraw, true);
			this.SetStyle(ControlStyles.UserPaint, true);
			this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			this.BackColor = Color.Transparent;
		}

		protected override void  OnPaint(PaintEventArgs e)
		{
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using (SolidBrush brushBack = new SolidBrush(LocalBackColor))
            {
                e.Graphics.FillRectangle(brushBack, this.ClientRectangle);
            }

			SevenSegmentHelper sevenSegmentHelper = new SevenSegmentHelper(e.Graphics);

			SizeF digitSizeF = sevenSegmentHelper.GetStringSize(m_digitText, Font);
			float scaleFactor = Math.Min(ClientSize.Width / digitSizeF.Width, ClientSize.Height / digitSizeF.Height);
			Font font = new Font(Font.FontFamily, scaleFactor * Font.SizeInPoints);
			digitSizeF = sevenSegmentHelper.GetStringSize(m_digitText, font);

			using (SolidBrush brush = new SolidBrush(m_digitColor))
			{
				using (SolidBrush lightBrush = new SolidBrush(Color.FromArgb(20, m_digitColor)))
				{
					sevenSegmentHelper.DrawDigits(
						m_digitText, font, brush, lightBrush,
						(ClientSize.Width - digitSizeF.Width) / 2,
						(ClientSize.Height - digitSizeF.Height) / 2);
				}
			}
		}
	}
}

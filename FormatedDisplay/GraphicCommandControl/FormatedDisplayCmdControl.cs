﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using CommonLib;

namespace FormatedDisplay
{
    internal class FormatedDisplayCmdControl : BTDllFormatedDisplayControl
    {
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public FormatedDisplayCmdControl()
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
                m_Ctrl = new FormatedDisplayDispCtrl();
                m_Ctrl.Location = m_RectControl.Location;
                m_Ctrl.Name = m_strSymbol;
                m_Ctrl.Size = m_RectControl.Size;
                ((FormatedDisplayDispCtrl)m_Ctrl).FormatString = ((DllFormatedDisplayProp)this.m_SpecificProp).FormatString;
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
                // effectuez ici le traitement à executer lorsque la valeur change
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

    public class FormatedDisplayDispCtrl : UserControl
    {
        // ajouter ici les données membres du control affiché
        int m_Value = 0;
        string m_FormatString = ":F0";

        public int Value
        {
            get
            {
                return m_Value;
            }
            set
            {
                m_Value = value;
                Refresh();
            }
        }

        public string FormatString
        {
            get
            {
                return m_FormatString;
            }
            set
            {
                m_FormatString = value;
            }
        }

        public FormatedDisplayDispCtrl()
        {
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle DrawRect = ClientRectangle;
            DrawRect.Inflate(-2, -2);
            Pen pen = new Pen(Color.FromArgb(127, 157, 185), 1);
            e.Graphics.FillRectangle(Brushes.White, DrawRect);
            e.Graphics.DrawRectangle(pen, DrawRect);
            string dispText = "1234.5";
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
            float DispValue = Value / diviseur;
            dispText = string.Format("{0" + FormatString + "}", DispValue);
            SizeF SizeText = e.Graphics.MeasureString(dispText, SystemFonts.DefaultFont);
            PointF PtText = new PointF(ClientRectangle.Left + 2, (Height - SizeText.Height) / 2);
            e.Graphics.DrawString(dispText, SystemFonts.DefaultFont, Brushes.Black, PtText);
        }
    }
}
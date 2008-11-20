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
        public GradientBaloonCmdControl()
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
                // faites ici les initialisation spécifiques du control affiché
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

    public class GradientBaloonDispCtrl : DrawInParentCmdCtrl
    {
        // ajouter ici les données membres du control affiché
        public GradientBaloonDispCtrl()
        {
            m_bDrawInParent = true;
        }

        public override void OnPaintInParent(Graphics gr, Rectangle rect)
        {
            LinearGradientBrush grBrush = new LinearGradientBrush(rect, Color.Red, Color.Blue, LinearGradientMode.Vertical);
            gr.FillRectangle(grBrush, rect);
        }
    }
}

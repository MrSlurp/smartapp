/***************************************************************************/
// PROJET : BTCommand : system de commande param�trable pour �quipement
// ayant une m�canisme de commande par liaison s�rie/ethernet/http
/***************************************************************************/
// Fichier : 
/***************************************************************************/
// description :
// 
/***************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using SmartApp.Datas;
using SmartApp.Ihm;
namespace SmartApp.Controls
{
    public class UpDownControl : BaseControl
    {
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public UpDownControl()
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
                m_Ctrl = new NumericUpDown();
                m_Ctrl.Location = m_RectControl.Location;
                m_Ctrl.Name = m_strSymbol;
                m_Ctrl.Size = m_RectControl.Size;
                if (m_AssociateData != null)
                {
                    ((NumericUpDown)m_Ctrl).Minimum = m_AssociateData.Minimum;
                    ((NumericUpDown)m_Ctrl).Maximum = m_AssociateData.Maximum;
                    ((NumericUpDown)m_Ctrl).Value = m_AssociateData.Value;
                    ((NumericUpDown)m_Ctrl).Increment = 1;
                }
                ((NumericUpDown)m_Ctrl).ValueChanged += new System.EventHandler(this.OnControlEvent);
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public override void OnControlEvent(Object Sender, EventArgs Args)
        {
            if (m_AssociateData != null)
            {
                m_AssociateData.Value = (int)((NumericUpDown)m_Ctrl).Value;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public override void UpdateFromData()
        {
            if (m_AssociateData != null && m_Ctrl != null)
            {
                ((NumericUpDown)m_Ctrl).Value = m_AssociateData.Value;
            }
        }
    }
}

/***************************************************************************/
// PROJET : BTCommand : system de commande paramétrable pour équipement
// ayant une mécanisme de commande par liaison série/ethernet/http
/***************************************************************************/
// Fichier : 
/***************************************************************************/
// description :
// 
/***************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace CommonLib
{
    public class SliderControl : BTControl
    {
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public SliderControl(BTDoc document)
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
                m_Ctrl = new TrackBar();
                m_Ctrl.Location = m_RectControl.Location;
                m_Ctrl.Name = m_strSymbol;
                m_Ctrl.Size = m_RectControl.Size;
                if (m_AssociateData != null)
                {
                    ((TrackBar)m_Ctrl).Minimum = m_AssociateData.Minimum;
                    ((TrackBar)m_Ctrl).Maximum = m_AssociateData.Maximum;
                    ((TrackBar)m_Ctrl).Value = m_AssociateData.Value;

                    int TotalRange = m_AssociateData.Maximum - m_AssociateData.Minimum;
                    int TickFreq = 20;
                    int TotalTick = TotalRange / TickFreq;
                    while (TotalTick > 20)
                    {
                        TickFreq += 20;
                        TotalTick = TotalRange / TickFreq;
                    }

                    ((TrackBar)m_Ctrl).TickFrequency = TickFreq;
                    ((TrackBar)m_Ctrl).LargeChange = TickFreq;
                }
                ((TrackBar)m_Ctrl).ValueChanged += new System.EventHandler(this.OnControlEvent);
                UpdateFromData();
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
                m_AssociateData.Value = ((TrackBar)m_Ctrl).Value;
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
                ((TrackBar)m_Ctrl).Value = m_AssociateData.Value;
            }
        }
    }
}

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
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using SmartApp.Datas;
using SmartApp.Ihm;

namespace SmartApp.Controls
{
    public class ButtonControl : BaseControl
    {
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public ButtonControl()
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
                m_Ctrl = new Button();
                m_Ctrl.Location = m_RectControl.Location;
                m_Ctrl.Name = m_strSymbol;
                m_Ctrl.Size = m_RectControl.Size;
                m_Ctrl.Text = this.IControl.Text;
                m_Ctrl.Click += new System.EventHandler(this.OnControlEvent);
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
                if (m_AssociateData.Value != 0)
                 m_AssociateData.Value = 0;
                else
                 m_AssociateData.Value = 1;
            }
            if (m_ScriptLines.Count != 0)
            {
                m_Executer.ExecuteScript(this.ScriptLines);
            }

            if (m_bUseScreenEvent)
            {
                m_Parent.ControlEvent();
            }
        }
    }
}

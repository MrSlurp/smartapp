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
    public class CheckControl : BaseControl
    {
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public CheckControl()
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
                m_Ctrl = new CheckBox();
                m_Ctrl.Location = m_RectControl.Location;
                m_Ctrl.Name = m_strSymbol;
                m_Ctrl.Size = m_RectControl.Size;
                m_Ctrl.Text = this.IControl.Text;
                if (m_AssociateData != null)
                {
                    if (m_AssociateData.Value != 0)
                        ((CheckBox)m_Ctrl).Checked = true;
                    else
                        ((CheckBox)m_Ctrl).Checked = false;
                }
                m_Ctrl.BackColor = Color.Transparent;
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
                if (((CheckBox)m_Ctrl).Checked == true)
                    m_AssociateData.Value = 1;
                else
                    m_AssociateData.Value = 0;
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

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public override void UpdateFromData()
        {
            if (m_AssociateData != null && m_Ctrl != null)
            {
                if (m_AssociateData.Value != 0)
                    ((CheckBox)m_Ctrl).Checked = true;
                else
                    ((CheckBox)m_Ctrl).Checked = false;
            }
        }
    }
}
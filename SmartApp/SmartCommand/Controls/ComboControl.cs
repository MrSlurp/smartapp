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
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using SmartApp.Datas;
using SmartApp.Ihm;

namespace SmartApp.Controls
{
    public class ComboControl : BaseControl
    {
        private ArrayList m_ArrayComboValue;
        private string[] m_ListStrValues;

        /// <summary>
        /// constructeur
        /// </summary>
        public ComboControl()
        {
            m_ArrayComboValue = new ArrayList();
            m_ListStrValues = new string[Cste.MAX_COMBO_ITEMS];
        }

        /// <summary>
        /// crée et initialise le control et l'ajoute dans la DynamicForm donnée
        /// </summary>
        /// <param name="itemRect">rectangle du control</param>
        /// <param name="HandlingForm">fenêtre dans laquelle le control doit être crée</param>
        /// <returns>la taille réèlement utilisée par le control</returns>
        public override void CreateControl()
        {
            if (m_Ctrl == null)
            {
                m_Ctrl = new ComboBox();
                m_Ctrl.Location = m_RectControl.Location;
                m_Ctrl.Name = m_strSymbol;
                m_Ctrl.Size = m_RectControl.Size;
                ((ComboBox)m_Ctrl).DataSource = m_ArrayComboValue;
                if (m_ArrayComboValue.Count >0)
                    ((ComboBox)m_Ctrl).SelectedIndex = 0;

                char[] Separator = new char[1];
                Separator[0] = ';';

                m_ListStrValues = this.IControl.Text.Split(Separator);
                ((ComboBox)m_Ctrl).DataSource = m_ListStrValues;
                if (m_AssociateData != null)
                {
                    if (m_AssociateData.Value < ((ComboBox)m_Ctrl).Items.Count)
                        ((ComboBox)m_Ctrl).SelectedIndex = m_AssociateData.Value;
                    else
                        ((ComboBox)m_Ctrl).SelectedIndex = 0;

                }

                ((ComboBox)m_Ctrl).SelectedIndexChanged += new System.EventHandler(this.OnControlEvent);
                ((ComboBox)m_Ctrl).DropDownStyle = ComboBoxStyle.DropDownList;
            }
        }

        /// <summary>
        /// callback appelé par le control lorsqu'une action est effectuée sur celui ci
        /// appel également l'évènement ControlEvent() sur le parent si necessaire
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="Args"></param>
        public override void OnControlEvent(Object Sender, EventArgs Args)
        {
            if (m_AssociateData != null)
                m_AssociateData.Value = ((ComboBox)m_Ctrl).SelectedIndex;

            if (m_ScriptLines.Count != 0)
            {
                m_Executer.ExecuteScript(this.ScriptLines);
            }

            if (m_bUseScreenEvent)
            {
                m_Parent.ControlEvent();
            }
        }

        /// <summary>
        /// met a jour l'état du control a partir de la valeur de sa donnée associée
        /// </summary>
        public override void UpdateFromData()
        {
            if (m_AssociateData != null && m_Ctrl != null)
            {
                if (m_AssociateData.Value < ((ComboBox)m_Ctrl).Items.Count)
                    ((ComboBox)m_Ctrl).SelectedIndex = m_AssociateData.Value;
                else
                    ((ComboBox)m_Ctrl).SelectedIndex = 0;
            }
        }

    }
}

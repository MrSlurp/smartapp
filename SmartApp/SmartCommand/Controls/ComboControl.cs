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
        /// cr�e et initialise le control et l'ajoute dans la DynamicForm donn�e
        /// </summary>
        /// <param name="itemRect">rectangle du control</param>
        /// <param name="HandlingForm">fen�tre dans laquelle le control doit �tre cr�e</param>
        /// <returns>la taille r��lement utilis�e par le control</returns>
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
        /// callback appel� par le control lorsqu'une action est effectu�e sur celui ci
        /// appel �galement l'�v�nement ControlEvent() sur le parent si necessaire
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
        /// met a jour l'�tat du control a partir de la valeur de sa donn�e associ�e
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

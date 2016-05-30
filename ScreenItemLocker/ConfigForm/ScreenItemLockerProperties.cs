/*
    This file is part of SmartApp.

    SmartApp is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    SmartApp is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with Foobar.  If not, see <http://www.gnu.org/licenses/>.
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommonLib;

namespace ScreenItemLocker
{
    internal partial class ScreenItemLockerProperties : BaseControlPropertiesPanel, ISpecificPanel
    {
        DllScreenItemLockerProp m_Props = new DllScreenItemLockerProp(null);
        public ScreenItemLockerProperties()
        {
            DllEntryClass.LangSys.Initialize(this);
            InitializeComponent();
            lstSelected_SelectedIndexChanged(null, null);
            lstScreen_SelectedIndexChanged(null, null);
        }

        #region validation des données
        /// <summary>
        /// Accesseur de validité des propriétés
        /// renvoie true si les propriété sont valides, sinon false
        /// </summary>
        public override bool IsObjectPropertiesValid
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// validitation des propriétés
        /// </summary>
        /// <returns>true si les propriété sont valides, sinon false</returns>
        public override bool ValidateProperties()
        {
            return true;
        }

        public void PanelToObject()
        {
            m_Props.ListItemSymbol.Clear();
            ((DllScreenItemLockerProp)m_Control.SpecificProp).ListItemSymbol.Clear();
            for (int i = 0; i < lstSelected.Items.Count; i++)
            {
                m_Props.ListItemSymbol.Add(lstSelected.Items[i].ToString());
                ((DllScreenItemLockerProp)m_Control.SpecificProp).ListItemSymbol.Add(lstSelected.Items[i].ToString());
            }
            //m_Control.SpecificProp.CopyParametersFrom(m_Props, false, this.Document);
            Document.Modified = true;
        }

        public void ObjectToPanel()
        {
            m_Props.ListItemSymbol.Clear();
            for (int i = 0; i < ((DllScreenItemLockerProp)m_Control.SpecificProp).ListItemSymbol.Count; i++)
            {
                m_Props.ListItemSymbol.Add(((DllScreenItemLockerProp)m_Control.SpecificProp).ListItemSymbol[i]);
            }
            //m_Props.CopyParametersFrom(m_Control.SpecificProp, false, this.Document);
            InitCurrentSelectionList();
            InitScreenItemList();
        }


        #endregion


        /// <summary>
        /// 
        /// </summary>
        private void InitScreenItemList()
        {
            lstScreen.Items.Clear();
            if (m_Control == null || m_Control.Parent == null)
                return;
            
            for (int i = 0; i < m_Control.Parent.Controls.Count; i++)
            {
                // on présente tout le monde sauf lui même & ce qui est déjà sélectionné
                if (m_Control.Parent.Controls[i].Symbol != m_Control.Symbol
                    && !m_Props.ListItemSymbol.Contains(m_Control.Parent.Controls[i].Symbol))
                    lstScreen.Items.Add(m_Control.Parent.Controls[i].Symbol);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitCurrentSelectionList()
        {
            lstSelected.Items.Clear();
            if (m_Props == null)
                return;

            foreach (string stSymbol in m_Props.ListItemSymbol)
            {
                lstSelected.Items.Add(stSymbol);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            while (lstScreen.SelectedIndices.Count > 0)
            {
                int idx = lstScreen.SelectedIndices[0];
                string symbol = lstScreen.Items[idx].ToString();
                lstScreen.Items.Remove(symbol);
                lstSelected.Items.Add(symbol);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddAll_Click(object sender, EventArgs e)
        {
            while (lstScreen.Items.Count > 0)
            {
                string symbol = lstScreen.Items[0].ToString();
                lstScreen.Items.Remove(symbol);
                lstSelected.Items.Add(symbol);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemove_Click(object sender, EventArgs e)
        {
            while (lstSelected.SelectedIndices.Count> 0)
            {
                int idx = lstSelected.SelectedIndices[0];
                string symbol = lstSelected.Items[idx].ToString();
                lstSelected.Items.Remove(symbol);
                lstScreen.Items.Add(symbol);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            m_Props.ListItemSymbol.Clear();
            InitCurrentSelectionList();
            InitScreenItemList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstSelected_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstSelected.SelectedIndices.Count > 0)
                btnAdd.Enabled = false;
            else
                btnAdd.Enabled = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstScreen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstScreen.SelectedIndices.Count > 0)
                btnRemove.Enabled = false;
            else
                btnRemove.Enabled = true;
        }
    }
}

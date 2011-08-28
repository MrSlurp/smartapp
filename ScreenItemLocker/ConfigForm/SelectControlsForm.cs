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
    internal partial class SelectControlsForm : Form
    {
        // controle dont on édite les propriété
        BTControl m_Control = null;
        // document courant
        private BTDoc m_Document = null;

        DllScreenItemLockerProp m_Props = new DllScreenItemLockerProp();


        public SelectControlsForm()
        {
            InitializeComponent();
            lstSelected_SelectedIndexChanged(null, null);
            lstScreen_SelectedIndexChanged(null, null);
        }

        /// <summary>
        /// Accesseur du control
        /// </summary>
        public BTControl BTControl
        {
            get
            {
                return m_Control;
            }
            set
            {
                if (value != null && value.SpecificProp.GetType() == typeof(DllScreenItemLockerProp))
                    m_Control = value;
                else
                    m_Control = null;
            }
        }

        /// <summary>
        /// Accesseur du document
        /// </summary>
        public BTDoc Doc
        {
            get { return m_Document; }
            set { m_Document = value; }
        }

        public DllScreenItemLockerProp Props
        {
            get
            {
                m_Props.ListItemSymbol.Clear();
                for (int i = 0; i < lstSelected.Items.Count; i++)
                {
                    m_Props.ListItemSymbol.Add(lstSelected.Items[i].ToString());
                }
                return m_Props;
            }
            set
            {
                if (value != null)
                    m_Props.CopyParametersFrom(value, false);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitScreenItemList()
        {
            lstScreen.Items.Clear();
            if (m_Control == null)
                return;

            for (int i = 0; i < BTControl.Parent.Controls.Count; i++)
            {
                // on présente tout le monde sauf lui même & ce qui est déjà sélectionné
                if (BTControl.Parent.Controls[i].Symbol != m_Control.Symbol
                    && !m_Props.ListItemSymbol.Contains(BTControl.Parent.Controls[i].Symbol))
                    lstScreen.Items.Add(BTControl.Parent.Controls[i].Symbol);
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
        private void btnOK_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectControlsForm_Shown(object sender, EventArgs e)
        {
            InitCurrentSelectionList();
            InitScreenItemList();

        }
    }
}

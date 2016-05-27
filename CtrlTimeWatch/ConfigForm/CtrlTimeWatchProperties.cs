using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.IO;
using System.Windows.Forms;
using CommonLib;

namespace CtrlTimeWatch
{
    internal partial class CtrlTimeWatchProperties : BaseControlPropertiesPanel, ISpecificPanel
    {
        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        public CtrlTimeWatchProperties()
        {
            DllEntryClass.LangSys.Initialize(this);
            InitializeComponent();
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
                bool bRet = true;
                if (this.m_Control == null)
                    return true;

                Data dt = (Data)this.Document.GestData.GetFromSymbol(this.edtHours.Text);
                if (dt == null)
                    bRet = false;

                dt = (Data)this.Document.GestData.GetFromSymbol(this.edtMinutes.Text);
                if (dt == null)
                    bRet = false;

                dt = (Data)this.Document.GestData.GetFromSymbol(this.edtSeconds.Text);
                if (dt == null)
                    bRet = false;

                return bRet;
            }
        }

        /// <summary>
        /// validitation des propriétés
        /// </summary>
        /// <returns>true si les propriété sont valides, sinon false</returns>
        public override bool ValidateProperties()
        {
            if (this.ConfiguredItem == null)
                return true;

            bool bRet = true;
            string strMessage = "";

            Data dt = null;
            dt = (Data)this.Document.GestData.GetFromSymbol(this.edtHours.Text);
            if (dt == null && !string.IsNullOrEmpty(this.edtHours.Text))
            {
                bRet = false;
                strMessage = string.Format(DllEntryClass.LangSys.C("Associate data {0} is not valid"), this.edtHours.Text);
            }
            dt = (Data)this.Document.GestData.GetFromSymbol(this.edtMinutes.Text);
            if (dt == null && !string.IsNullOrEmpty(this.edtMinutes.Text))
            {
                bRet = false;
                strMessage = string.Format(DllEntryClass.LangSys.C("Associate data {0} is not valid"), this.edtMinutes.Text);
            }
            dt = (Data)this.Document.GestData.GetFromSymbol(this.edtSeconds.Text);
            if (dt == null && !string.IsNullOrEmpty(this.edtSeconds.Text))
            {
                bRet = false;
                strMessage = string.Format(DllEntryClass.LangSys.C("Associate data {0} is not valid"), this.edtSeconds.Text);
            }

            if (!bRet)
            {
                MessageBox.Show(strMessage, DllEntryClass.LangSys.C("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return bRet;
            }


            return true;
        }

        public void PanelToObject()
        {
            DllCtrlTimeWatchProp prop = (DllCtrlTimeWatchProp)m_Control.SpecificProp;

            bool bDataPropChange = false;

            if (this.edtHours.Text != prop.DataHours)
                bDataPropChange = true;

            if (this.edtMinutes.Text != prop.DataMinutes)
                bDataPropChange = true;

            if (this.edtSeconds.Text != prop.DataSecond)
                bDataPropChange = true;

            if (bDataPropChange)
            {
                Document.Modified = true;
                prop.DataHours = this.edtHours.Text;
                prop.DataMinutes = this.edtMinutes.Text;
                prop.DataSecond = this.edtSeconds.Text;
            }
        }

        public void ObjectToPanel()
        {
            DllCtrlTimeWatchProp prop = (DllCtrlTimeWatchProp)m_Control.SpecificProp;
            this.edtHours.Text = prop.DataHours;
            this.edtMinutes.Text = prop.DataMinutes;
            this.edtSeconds.Text = prop.DataSecond;
        }
        #endregion

        private void btnPickHours_Click(object sender, EventArgs e)
        {
            PickDataForm PickData = new PickDataForm();
            PickData.Document = this.Document;
            if (PickData.ShowDialog() == DialogResult.OK)
            {
                if (PickData.SelectedData != null)
                    edtHours.Text = PickData.SelectedData.Symbol;
                else
                    edtHours.Text = string.Empty;
            }
        }

        private void btnPickMinutes_Click(object sender, EventArgs e)
        {
            PickDataForm PickData = new PickDataForm();
            PickData.Document = this.Document;
            if (PickData.ShowDialog() == DialogResult.OK)
            {
                if (PickData.SelectedData != null)
                    edtMinutes.Text = PickData.SelectedData.Symbol;
                else
                    edtMinutes.Text = string.Empty;
            }
        }

        private void btnPickSecond_Click(object sender, EventArgs e)
        {
            PickDataForm PickData = new PickDataForm();
            PickData.Document = this.Document;
            if (PickData.ShowDialog() == DialogResult.OK)
            {
                if (PickData.SelectedData != null)
                    edtSeconds.Text = PickData.SelectedData.Symbol;
                else
                    edtSeconds.Text = string.Empty;
            }
        }
    }
}

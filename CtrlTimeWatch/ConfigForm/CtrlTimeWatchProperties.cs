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
    internal partial class CtrlTimeWatchProperties : UserControl, ISpecificPanel
    {
        // controle dont on édite les propriété
        BTControl m_Control = null;
        // document courant
        private BTDoc m_Document = null;

        #region events
        public event ControlPropertiesChange ControlPropertiesChanged;
        #endregion

        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        public CtrlTimeWatchProperties()
        {
            DllEntryClass.LangSys.Initialize(this);
            InitializeComponent();
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
                if (value != null && value.SpecificProp.GetType() == typeof(DllCtrlTimeWatchProp))
                    m_Control = value;
                else
                    m_Control = null;

                if (m_Control != null)
                {
                    DllCtrlTimeWatchProp Props = (DllCtrlTimeWatchProp)m_Control.SpecificProp;
                    this.Enabled = true;
                    this.edtHours.Text = Props.DataHours;
                    this.edtMinutes.Text = Props.DataMinutes;
                    this.edtSeconds.Text = Props.DataSecond;
                }
                else
                {
                    this.Enabled = false;
                    // mettez ici les valeur par défaut pour le panel de propriété spécifiques
                    this.edtHours.Text = string.Empty;
                    this.edtMinutes.Text = string.Empty;
                    this.edtSeconds.Text = string.Empty;
                }
            }
        }

        /// <summary>
        /// Accesseur du document
        /// </summary>
        public BTDoc Doc
        {
            get
            {
                return m_Document;
            }
            set
            {
                m_Document = value;
            }
        }

        #region validation des données
        /// <summary>
        /// Accesseur de validité des propriétés
        /// renvoie true si les propriété sont valides, sinon false
        /// </summary>
        public bool IsDataValuesValid
        {
            get
            {
                bool bRet = true;
                if (this.BTControl == null)
                    return true;

                Data dt = (Data)this.Doc.GestData.GetFromSymbol(this.edtHours.Text);
                if (dt == null)
                    bRet = false;

                dt = (Data)this.Doc.GestData.GetFromSymbol(this.edtMinutes.Text);
                if (dt == null)
                    bRet = false;

                dt = (Data)this.Doc.GestData.GetFromSymbol(this.edtSeconds.Text);
                if (dt == null)
                    bRet = false;

                return bRet;
            }
        }

        /// <summary>
        /// validitation des propriétés
        /// </summary>
        /// <returns>true si les propriété sont valides, sinon false</returns>
        public bool ValidateValues()
        {
            if (this.BTControl == null)
                return true;

            bool bRet = true;
            string strMessage = "";

            Data dt = null;
            dt = (Data)this.Doc.GestData.GetFromSymbol(this.edtHours.Text);
            if (dt == null)
            {
                bRet = false;
                strMessage = string.Format(DllEntryClass.LangSys.C("Associate data {0} is not valid"), this.edtHours.Text);
            }
            dt = (Data)this.Doc.GestData.GetFromSymbol(this.edtMinutes.Text);
            if (dt == null)
            {
                bRet = false;
                strMessage = string.Format(DllEntryClass.LangSys.C("Associate data {0} is not valid"), this.edtMinutes.Text);
            }
            dt = (Data)this.Doc.GestData.GetFromSymbol(this.edtSeconds.Text);
            if (dt == null)
            {
                bRet = false;
                strMessage = string.Format(DllEntryClass.LangSys.C("Associate data {0} is not valid"), this.edtSeconds.Text);
            }

            if (!bRet)
            {
                MessageBox.Show(strMessage, DllEntryClass.LangSys.C("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return bRet;
            }

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
                Doc.Modified = true;
                prop.DataHours = this.edtHours.Text;
                prop.DataMinutes = this.edtMinutes.Text;
                prop.DataSecond = this.edtSeconds.Text;
                m_Control.IControl.Refresh();
            }
            if (bDataPropChange && ControlPropertiesChanged != null)
                ControlPropertiesChanged(m_Control);
            return true;
        }
        #endregion

        private void btnPickHours_Click(object sender, EventArgs e)
        {
            PickDataForm PickData = new PickDataForm();
            PickData.Document = this.Doc;
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
            PickData.Document = this.Doc;
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
            PickData.Document = this.Doc;
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

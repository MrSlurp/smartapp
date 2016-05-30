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
using System.Drawing;
using System.Data;
using System.Text;
using System.IO;
using System.Windows.Forms;
using CommonLib;

namespace CtrlDataComp
{
    internal partial class CtrlDataCompProperties : BaseControlPropertiesPanel, ISpecificPanel
    {
        RadioButton[] m_listBtnCompareMode;
        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        public CtrlDataCompProperties()
        {
            DllEntryClass.LangSys.Initialize(this);
            InitializeComponent();
            rdoASupB.Tag = eCompareMode.cmp_ASupB;
            rdoASupEqB.Tag = eCompareMode.cmp_ASupEqB;
            rdoAInfB.Tag = eCompareMode.cmp_AInfB;
            rdoAInfEqB.Tag = eCompareMode.cmp_AInfEqB;
            rdoASupBSupC.Tag = eCompareMode.cmp_ASupBSupC;
            rdoASupEqBSupEqC.Tag = eCompareMode.cmp_ASupEqBSupEqC;
            m_listBtnCompareMode = new RadioButton[] {rdoASupB, rdoASupEqB, rdoAInfB, rdoAInfEqB,
                                                      rdoASupBSupC, rdoASupEqBSupEqC};

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
                if (value != null && value.SpecificProp.GetType() == typeof(DllCtrlDataCompProp))
                    m_Control = value;
                else
                    m_Control = null;
                if (m_Control != null)
                {
                    this.Enabled = true;
                    // assignez ici les valeur des propriété spécifiques du control
                }
                else
                {
                    this.Enabled = false;
                    // mettez ici les valeur par défaut pour le panel de propriété spécifiques
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
        public override bool IsObjectPropertiesValid
        {
            get
            {
                if (this.BTControl == null)
                    return true;

                if (!CheckDataOrInt(this.edtDataA.Text))
                    return false;
                if (!CheckDataOrInt(this.edtDataB.Text))
                    return false;
                if (!CheckDataOrInt(this.edtDataC.Text))
                    return false;

                return true;
            }
        }

        private bool CheckDataOrInt(string SymbolOrValue)
        {
            bool bRet = true;
            if (!string.IsNullOrEmpty(SymbolOrValue))
            {
                int dummy;
                bool parseRes = int.TryParse(SymbolOrValue, out dummy);
                if (!parseRes)
                {
                    Data dt = Document.GestData.GetFromSymbol(SymbolOrValue) as Data;
                    if (dt == null)
                        bRet = false;
                }
            }
            return bRet;
        }

        /// <summary>
        /// validitation des propriétés
        /// </summary>
        /// <returns>true si les propriété sont valides, sinon false</returns>
        public override bool ValidateProperties()
        {
            if (this.BTControl == null)
                return true;

            bool bRet = true;
            string strMessage = "";

            if (!CheckDataOrInt(this.edtDataA.Text))
            {
                strMessage = string.Format(DllEntryClass.LangSys.C("Associate data {0} is not valid"), this.edtDataA.Text);
                bRet = false;
            }
            if (!CheckDataOrInt(this.edtDataB.Text))
            {
                strMessage = string.Format(DllEntryClass.LangSys.C("Associate data {0} is not valid"), this.edtDataB.Text);
                bRet = false;
            }
            if (!CheckDataOrInt(this.edtDataC.Text))
            {
                strMessage = string.Format(DllEntryClass.LangSys.C("Associate data {0} is not valid"), this.edtDataC.Text);
                bRet = false;
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
            bool bDataPropChange = false;

            // testez ici si les paramètres ont changé en les comparant avec ceux contenu dans les propriété
            // spécifiques du BTControl
            // si c'est le cas, assignez bDataPropChange à true;
            DllCtrlDataCompProp SpecProp = m_Control.SpecificProp as DllCtrlDataCompProp;
            if (edtDataA.Text != SpecProp.DataA)
                bDataPropChange = true;
            if (edtDataB.Text != SpecProp.DataB)
                bDataPropChange = true;
            if (edtDataC.Text != SpecProp.DataC)
                bDataPropChange = true;

            eCompareMode cmpMode = BtnToCompMode();
            if (cmpMode != SpecProp.CompMode)
                bDataPropChange = true;

            if (bDataPropChange)
            {
                SpecProp.DataA = edtDataA.Text;
                SpecProp.DataB = edtDataB.Text;
                if ((int)cmpMode >= (int)eCompareMode.cmp_ASupBSupC)
                    SpecProp.DataC = edtDataC.Text;
                else
                    SpecProp.DataC = string.Empty;
                SpecProp.CompMode = cmpMode;
                Doc.Modified = true;
            }
        }

        public void ObjectToPanel()
        {
            DllCtrlDataCompProp SpecProp = m_Control.SpecificProp as DllCtrlDataCompProp;
            CompModeToBtn(SpecProp.CompMode);
            edtDataA.Text = SpecProp.DataA;
            edtDataB.Text = SpecProp.DataB;
            edtDataC.Text = SpecProp.DataC;
        }
        #endregion

        private void rdoCompMode_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoASupBSupC.Checked || rdoASupEqBSupEqC.Checked)
            {
                edtDataC.Enabled = true;
                btnPickC.Enabled = true;
            }
            else
            {
                edtDataC.Enabled = false;
                btnPickC.Enabled = false;
            }
        }

        private void CompModeToBtn(eCompareMode mode)
        {
            foreach (RadioButton btn in m_listBtnCompareMode)
            {
                if ((eCompareMode)btn.Tag == mode)
                    btn.Checked = true;
            }
        }

        private eCompareMode BtnToCompMode()
        {
            foreach (RadioButton btn in m_listBtnCompareMode)
            {
                if (btn.Checked)
                    return (eCompareMode)btn.Tag ;
            }
            return eCompareMode.cmp_ASupB;
        }

        private void btnPickA_Click(object sender, EventArgs e)
        {
            PickDataForm PickData = new PickDataForm();
            PickData.Document = this.Document;
            if (PickData.ShowDialog() == DialogResult.OK)
            {
                if (PickData.SelectedData != null)
                    edtDataA.Text = PickData.SelectedData.Symbol;
                else
                    edtDataA.Text = string.Empty;
            }
        }

        private void btnPickB_Click(object sender, EventArgs e)
        {
            PickDataForm PickData = new PickDataForm();
            PickData.Document = this.Document;
            if (PickData.ShowDialog() == DialogResult.OK)
            {
                if (PickData.SelectedData != null)
                    edtDataB.Text = PickData.SelectedData.Symbol;
                else
                    edtDataB.Text = string.Empty;
            }
        }

        private void btnPickC_Click(object sender, EventArgs e)
        {
            PickDataForm PickData = new PickDataForm();
            PickData.Document = this.Document;
            if (PickData.ShowDialog() == DialogResult.OK)
            {
                if (PickData.SelectedData != null)
                    edtDataC.Text = PickData.SelectedData.Symbol;
                else
                    edtDataC.Text = string.Empty;
            }
        }
    }
}

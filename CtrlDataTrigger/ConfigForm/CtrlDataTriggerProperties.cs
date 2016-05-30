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

namespace CtrlDataTrigger
{
    public partial class CtrlDataTriggerProperties : BaseControlPropertiesPanel, ISpecificPanel
    {
        bool bScriptChange = false;

        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        public CtrlDataTriggerProperties()
        {
            DllEntryClass.LangSys.Initialize(this);
            InitializeComponent();
        }

        void UpdateScriptPresenceLabels()
        {
            /*
            if (m_Control != null)
            {
                DllCtrlDataTriggerProp Props = (DllCtrlDataTriggerProp)m_Control.SpecificProp;
                if (Props.ScriptOffToOn.Length == 0)
                    m_lblScriptOffToOnComm.Text = DllEntryClass.LangSys.C("Empty");
                else
                    m_lblScriptOffToOnComm.Text = DllEntryClass.LangSys.C("Not empty");

                if (Props.ScriptOnToOff.Length == 0)
                    m_lblScriptOnToOffComm.Text = DllEntryClass.LangSys.C("Empty");
                else
                    m_lblScriptOnToOffComm.Text = DllEntryClass.LangSys.C("Not empty");
            }
            else
            {
                m_lblScriptOffToOnComm.Text = DllEntryClass.LangSys.C("Empty");
                m_lblScriptOnToOffComm.Text = DllEntryClass.LangSys.C("Empty");
            }
            */
        }

        void AdvPropEnable(bool bEnable)
        {
            this.edtOffToOn.Enabled = bEnable;
            this.edtOnToOff.Enabled = bEnable;
            //this.btnOffToOnScript.Enabled = bEnable;
            //this.btnOnToOffScript.Enabled = bEnable;
            this.btnPickOffToOn.Enabled = bEnable;
            this.btnPickOnToOff.Enabled = bEnable;
        }


        #region validation des données
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public bool IsDataValuesValid
        {
            get
            {
                bool bRet = true;
                if (this.ConfiguredItem == null)
                    return true;

                if (this.cbxSchmitt.Checked)
                {
                    int dummy;
                    bool parseResOffOn = int.TryParse(this.edtOffToOn.Text, out dummy);
                    if (!parseResOffOn)
                    {
                        Data dt = (Data)this.Document.GestData.GetFromSymbol(this.edtOffToOn.Text);
                        if (dt == null)
                            bRet = false;
                    }

                    bool parseResOnOff = int.TryParse(this.edtOnToOff.Text, out dummy);
                    if (!parseResOnOff)
                    {
                        Data dt = (Data)this.Document.GestData.GetFromSymbol(this.edtOnToOff.Text);
                        if (dt == null)
                            bRet = false;

                    }
                    return bRet;
                }
                return true;
            }
        }


        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public bool ValidateValues()
        {
            if (this.ConfiguredItem == null)
                return true;

            bool bRet = true;
            string strMessage = "";

            if (this.cbxSchmitt.Checked)
            {
                int dummy;
                Data dt = null;
                bool parseResOffOn = int.TryParse(this.edtOffToOn.Text, out dummy);
                if (!parseResOffOn)
                {
                    dt = (Data)this.Document.GestData.GetFromSymbol(this.edtOffToOn.Text);
                    if (dt == null)
                    {
                        bRet = false;
                        strMessage = string.Format(DllEntryClass.LangSys.C("Associate Off to On data {0} is not valid"), this.edtOffToOn.Text);
                    }
                }

                bool parseResOnOff = int.TryParse(this.edtOnToOff.Text, out dummy);
                if (!parseResOnOff)
                {
                    dt = (Data)this.Document.GestData.GetFromSymbol(this.edtOnToOff.Text);
                    if (dt == null)
                    {
                        bRet = false;
                        strMessage = string.Format(DllEntryClass.LangSys.C("Associate On to Off data {0} is not valid"), this.edtOnToOff.Text);
                    }
                }
            }
            if (!bRet)
            {
                MessageBox.Show(strMessage, DllEntryClass.LangSys.C("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return bRet;
            }



            return true;
        }

        public void ObjectToPanel()
        {
            DllCtrlDataTriggerProp Props = (DllCtrlDataTriggerProp)m_Control.SpecificProp;
            this.cbxSchmitt.Checked = Props.BehaveLikeTrigger;
            this.AdvPropEnable(this.cbxSchmitt.Checked);
            this.edtOffToOn.Text = Props.DataOffToOn;
            this.edtOnToOff.Text = Props.DataOnToOff;
            UpdateScriptPresenceLabels();
        }

        public void PanelToObject()
        {
            DllCtrlDataTriggerProp prop = (DllCtrlDataTriggerProp)m_Control.SpecificProp;

            bool bDataPropChange = false;
            if (this.cbxSchmitt.Checked != prop.BehaveLikeTrigger)
                bDataPropChange = true;

            if (this.edtOffToOn.Text != prop.DataOffToOn)
                bDataPropChange = true;

            if (this.edtOnToOff.Text != prop.DataOnToOff)
                bDataPropChange = true;

            if (bScriptChange)
                bDataPropChange = true;

            if (bDataPropChange)
            {
                Document.Modified = true;
                prop.BehaveLikeTrigger = this.cbxSchmitt.Checked;
                prop.DataOnToOff = this.edtOnToOff.Text;
                prop.DataOffToOn = this.edtOffToOn.Text;
            }
        }
        #endregion

        private void cbxSchmitt_CheckedChanged(object sender, EventArgs e)
        {
            AdvPropEnable(this.cbxSchmitt.Checked);
        }

        private void btnPickOnToOff_Click(object sender, EventArgs e)
        {
            PickDataForm PickData = new PickDataForm();
            PickData.Document = this.Document;
            if (PickData.ShowDialog() == DialogResult.OK)
            {
                if (PickData.SelectedData != null)
                    edtOnToOff.Text = PickData.SelectedData.Symbol;
                else
                    edtOnToOff.Text = string.Empty;
            }
        }

        private void btnPickOffToOn_Click(object sender, EventArgs e)
        {
            PickDataForm PickData = new PickDataForm();
            PickData.Document = this.Document;
            if (PickData.ShowDialog() == DialogResult.OK)
            {
                if (PickData.SelectedData != null)
                    edtOffToOn.Text = PickData.SelectedData.Symbol;
                else
                    edtOffToOn.Text = string.Empty;
            }
        }

        /*
        private void btnOnToOffScript_Click(object sender, EventArgs e)
        {
            DllCtrlDataTriggerProp prop = (DllCtrlDataTriggerProp)m_Control.SpecificProp;
            ScriptEditordialog DlgScript = new ScriptEditordialog();
            DlgScript.Doc = this.Document;
            DlgScript.ScriptLines = prop.ScriptOnToOff;
            DialogResult dlgRes = DlgScript.ShowDialog();
            if (dlgRes == DialogResult.OK)
            {
                prop.ScriptOnToOff = DlgScript.ScriptLines;
                bScriptChange = true;
            }
            UpdateScriptPresenceLabels();
        }

        private void btnOffToOnScript_Click(object sender, EventArgs e)
        {
            DllCtrlDataTriggerProp prop = (DllCtrlDataTriggerProp)m_Control.SpecificProp;
            ScriptEditordialog DlgScript = new ScriptEditordialog();
            DlgScript.Doc = this.Document;
            DlgScript.ScriptLines = prop.ScriptOffToOn;
            DialogResult dlgRes = DlgScript.ShowDialog();
            if (dlgRes == DialogResult.OK)
            {
                prop.ScriptOffToOn = DlgScript.ScriptLines;
                bScriptChange = true;
            }
            UpdateScriptPresenceLabels();        
        }*/
    }
}

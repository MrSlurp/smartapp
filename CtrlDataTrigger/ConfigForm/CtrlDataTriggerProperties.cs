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
    public partial class CtrlDataTriggerProperties : UserControl, ISpecificPanel
    {
        BTControl m_Control = null;
        private BTDoc m_Document = null;
        bool bScriptChange = false;

        #region events
        public event ControlPropertiesChange ControlPropertiesChanged;
        #endregion

        public CtrlDataTriggerProperties()
        {
            DllEntryClass.LangSys.Initialize(this);
            InitializeComponent();
        }

        public BTControl BTControl
        {
            get
            {
                return m_Control;
            }
            set
            {
                if (value != null && value.SpecificProp.GetType() == typeof(DllCtrlDataTriggerProp))
                    m_Control = value;
                else
                    m_Control = null;

                if (m_Control != null)
                {
                    DllCtrlDataTriggerProp Props = (DllCtrlDataTriggerProp)m_Control.SpecificProp;
                    this.Enabled = true;
                    this.cbxSchmitt.Checked = Props.BehaveLikeTrigger;
                    this.AdvPropEnable(this.cbxSchmitt.Checked);
                    this.edtOffToOn.Text = Props.DataOffToOn;
                    this.edtOnToOff.Text = Props.DataOnToOff;
                    
                }
                else
                {
                    this.Enabled = false;
                    this.cbxSchmitt.Checked = false;
                    this.edtOffToOn.Text = string.Empty;
                    this.edtOnToOff.Text = string.Empty;
                }
                UpdateScriptPresenceLabels();
            }
        }
    
        void UpdateScriptPresenceLabels()
        {
            if (m_Control != null)
            {
                DllCtrlDataTriggerProp Props = (DllCtrlDataTriggerProp)m_Control.SpecificProp;
                if (Props.ScriptOffToOn.Length == 0)
                    m_lblScriptOffToOnComm.Text = DllEntryClass.LangSys.C("Empty");
                else
                    m_lblScriptOffToOnComm.Text = DllEntryClass.LangSys.C("No empty");

                if (Props.ScriptOnToOff.Length == 0)
                    m_lblScriptOnToOffComm.Text = DllEntryClass.LangSys.C("Empty");
                else
                    m_lblScriptOnToOffComm.Text = DllEntryClass.LangSys.C("No empty");
            }
            else
            {
                m_lblScriptOffToOnComm.Text = DllEntryClass.LangSys.C("Empty");
                m_lblScriptOnToOffComm.Text = DllEntryClass.LangSys.C("Empty");
            }
            
        }

        void AdvPropEnable(bool bEnable)
        {
            this.edtOffToOn.Enabled = bEnable;
            this.edtOnToOff.Enabled = bEnable;
            this.btnOffToOnScript.Enabled = bEnable;
            this.btnOnToOffScript.Enabled = bEnable;
            this.btnPickOffToOn.Enabled = bEnable;
            this.btnPickOnToOff.Enabled = bEnable;
        }


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
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public bool IsDataValuesValid
        {
            get
            {
                bool bRet = true;
                if (this.BTControl == null)
                    return true;

                if (this.cbxSchmitt.Checked)
                {
                    int dummy;
                    bool parseResOffOn = int.TryParse(this.edtOffToOn.Text, out dummy);
                    if (!parseResOffOn)
                    {
                        Data dt = (Data)this.Doc.GestData.GetFromSymbol(this.edtOffToOn.Text);
                        if (dt == null)
                            bRet = false;
                    }

                    bool parseResOnOff = int.TryParse(this.edtOnToOff.Text, out dummy);
                    if (!parseResOnOff)
                    {
                        Data dt = (Data)this.Doc.GestData.GetFromSymbol(this.edtOnToOff.Text);
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
            if (this.BTControl == null)
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
                    dt = (Data)this.Doc.GestData.GetFromSymbol(this.edtOffToOn.Text);
                    if (dt == null)
                    {
                        bRet = false;
                        strMessage = string.Format(DllEntryClass.LangSys.C("Associate Off to On data {0} is not valid"), this.edtOffToOn.Text);
                    }
                }

                bool parseResOnOff = int.TryParse(this.edtOnToOff.Text, out dummy);
                if (!parseResOnOff)
                {
                    dt = (Data)this.Doc.GestData.GetFromSymbol(this.edtOnToOff.Text);
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
                Doc.Modified = true;
                prop.BehaveLikeTrigger = this.cbxSchmitt.Checked;
                prop.DataOnToOff = this.edtOnToOff.Text;
                prop.DataOffToOn = this.edtOffToOn.Text;
                m_Control.IControl.Refresh();
            }
            if (bDataPropChange && ControlPropertiesChanged != null)
                ControlPropertiesChanged(m_Control);
            return true;
        }
        #endregion

        private void cbxSchmitt_CheckedChanged(object sender, EventArgs e)
        {
            AdvPropEnable(this.cbxSchmitt.Checked);
        }

        private void btnPickOnToOff_Click(object sender, EventArgs e)
        {
            PickDataForm PickData = new PickDataForm();
            PickData.Document = this.Doc;
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
            PickData.Document = this.Doc;
            if (PickData.ShowDialog() == DialogResult.OK)
            {
                if (PickData.SelectedData != null)
                    edtOffToOn.Text = PickData.SelectedData.Symbol;
                else
                    edtOffToOn.Text = string.Empty;
            }
        }

        private void btnOnToOffScript_Click(object sender, EventArgs e)
        {
            DllCtrlDataTriggerProp prop = (DllCtrlDataTriggerProp)m_Control.SpecificProp;
            ScriptEditordialog DlgScript = new ScriptEditordialog();
            DlgScript.Doc = this.m_Document;
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
            DlgScript.Doc = this.m_Document;
            DlgScript.ScriptLines = prop.ScriptOffToOn;
            DialogResult dlgRes = DlgScript.ShowDialog();
            if (dlgRes == DialogResult.OK)
            {
                prop.ScriptOffToOn = DlgScript.ScriptLines;
                bScriptChange = true;
            }
            UpdateScriptPresenceLabels();        
        }
    }
}

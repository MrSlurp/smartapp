using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SmartApp.Datas;
using SmartApp.Gestionnaires;
using SmartApp.Ihm.Designer;

namespace SmartApp.Ihm
{
    public partial class ScreenItemPropertiesControl : UserControl
    {
        #region données membres
        private BTControl m_Control = null;

        private GestControl m_GestControl = null;

        private BTDoc m_Document = null;


        private AutoCompleteStringCollection m_BeginWithDataList;
        #endregion

        #region events
        public event ControlPropertiesChange ControlPropertiesChanged;
        #endregion

        #region constructeur
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public ScreenItemPropertiesControl()
        {
            InitializeComponent();
            m_BeginWithDataList = new AutoCompleteStringCollection();
            // c'est bien beau l'auto completion mais encore faut t'il que ca ne plante pas
            // trouvé ou est le problem
            //m_EditAssociateData.AutoCompleteCustomSource = m_BeginWithDataList;
        }
        #endregion

        #region attribut d'accès aux valeurs de la page de propriété
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public BTControl BTControl
        {
            get
            {
                return m_Control;
            }
            set
            {
                m_Control = value;
                if (m_Control != null)
                {
                    this.Enabled = true;
                    this.Description = m_Control.Description;
                    this.Symbol = m_Control.Symbol;
                    this.AssociateData = m_Control.AssociateData; ;
                    this.IsReadOnly = m_Control.IsReadOnly;
                    this.UseScreenEvent = m_Control.UseScreenEvent;
                    this.Txt = m_Control.IControl.Text;
                    this.m_LabelCurControl.Text = m_Control.Symbol;
                }
                else
                {
                    this.Description = "";
                    this.Symbol = "";
                    this.AssociateData = "";
                    this.IsReadOnly = false;
                    this.UseScreenEvent = false;
                    this.Enabled = false;
                    this.Txt = "";
                    this.m_LabelCurControl.Text = "No selection";
                }
                UpdateStateFromControlType();
            }
        }
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public string Description
        {
            get
            {
                return m_richTextBoxDesc.Text;
            }
            set
            {
                m_richTextBoxDesc.Text = value;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public string Symbol
        {
            get
            {
                return m_textSymbol.Text;
            }
            set
            {
                m_textSymbol.Text = value;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public string Txt
        {
            get
            {
                return m_EditText.Text;
            }
            set
            {
                m_EditText.Text = value;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public string AssociateData
        {
            get
            {
                return m_EditAssociateData.Text;
            }
            set
            {
                m_EditAssociateData.Text = value;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public bool IsReadOnly
        {
            get
            {
                return m_checkReadOnly.Checked;
            }
            set
            {
                m_checkReadOnly.Checked = value;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public bool UseScreenEvent
        {
            get
            {
                return m_checkScreenEvent.Checked;
            }
            set
            {
                m_checkScreenEvent.Checked = value;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
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

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public GestData GestData
        {
            get
            {
                return m_Document.GestData;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public GestControl GestControl
        {
            get
            {
                return m_GestControl;
            }
            set
            {
                m_GestControl = value;
            }
        }
        #endregion

        #region validation des données
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public bool IsDataValuesValid
        {
            get
            {
                if (this.BTControl == null)
                    return true;

                if (string.IsNullOrEmpty(this.Symbol))
                    return false;
                bool bRet = true;
                BTControl dt = (BTControl)this.GestControl.GetFromSymbol(this.Symbol);
                if (dt != null && dt != this.BTControl)
                    bRet = false;
                if (!string.IsNullOrEmpty(this.AssociateData))
                {
                    Data dat = (Data)this.GestData.GetFromSymbol(this.AssociateData);
                    if (dat == null)
                        bRet = false;
                }

                return bRet;
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
            if (string.IsNullOrEmpty(this.Symbol))
            {
                strMessage = "Symbol must not be empty";
                bRet = false;
            }
            BTControl Sc = (BTControl)GestControl.GetFromSymbol(this.Symbol);
            if (bRet && Sc != null && Sc != this.BTControl)
            {
                strMessage = string.Format("A control with symbol {0} already exist", Symbol);
                bRet = false;
            }
            if (!string.IsNullOrEmpty(this.AssociateData))
            {
                Data dat = (Data)this.GestData.GetFromSymbol(this.AssociateData);
                if (bRet && dat == null)
                {
                    strMessage = string.Format("Associate data {0} is not valid", this.AssociateData);
                    bRet = false;
                }
            }
            if (!bRet)
            {
                MessageBox.Show(strMessage);
                return bRet;
            }
            bool bDataPropChange = false;
            if (m_Control.Description != this.Description)
                bDataPropChange |= true;
            if (m_Control.Symbol != this.Symbol)
                bDataPropChange |= true;

            if (m_Control.AssociateData != this.AssociateData)
                bDataPropChange |= true;
            if (m_Control.IsReadOnly != this.IsReadOnly)
                bDataPropChange |= true;
            if (m_Control.UseScreenEvent != this.UseScreenEvent)
                bDataPropChange |= true;
            if (this.Txt != m_Control.IControl.Text)
                bDataPropChange |= true;

            if (bDataPropChange)
            {
                m_Control.Description = this.Description;
                m_Control.Symbol = this.Symbol;
                m_Control.AssociateData = this.AssociateData;
                m_Control.IsReadOnly = this.IsReadOnly;
                m_Control.UseScreenEvent = this.UseScreenEvent;
                m_Control.IControl.Text = this.Txt;
                Doc.Modified = true;
            }
            if (bDataPropChange && ControlPropertiesChanged != null)
                ControlPropertiesChanged(m_Control);
            return true;
        }
        #endregion

        #region handlers d'évènement
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void OnEditAssocDataEdited(object sender, EventArgs e)
        {
            
            try
            {
                string strValue = m_EditAssociateData.Text;
                m_BeginWithDataList.Clear();
                for (int i = 0; i < GestData.Count; i++)
                {
                    Data dt = (Data)GestData[i];
                    if (strValue.Length < dt.Symbol.Length
                        && dt.Symbol.StartsWith(strValue, StringComparison.CurrentCultureIgnoreCase))
                    {
                        m_BeginWithDataList.Add(dt.Symbol);
                    }
                }
            }
            catch (Exception)
            {
                return;
            }            
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void OnEditAssociateDataDragDrop(object sender, DragEventArgs e)
        {
            Data DropedItem = (Data)e.Data.GetData(typeof(Data));
            if (DropedItem != null)
            {
                this.AssociateData = DropedItem.Symbol;
                m_EditAssociateData.Focus();
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void OnEditAssociateDataDragEnter(object sender, DragEventArgs e)
        {
            Data dt = (Data)e.Data.GetData(typeof(Data));
            if (dt != null)
                e.Effect = DragDropEffects.All;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void PropertiesControlValidating(object sender, CancelEventArgs e)
        {
            if (!ValidateValues())
                e.Cancel = true;
        }
        #endregion

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void UpdateStateFromControlType()
        {
            if (m_Control == null)
                return;

            switch (m_Control.IControl.ControlType)
            {
                case InteractiveControlType.Button:
                    m_EditText.Enabled = true;
                    m_EditAssociateData.Enabled = true;
                    m_checkReadOnly.Enabled = false;
                    m_checkScreenEvent.Enabled = true;
                    break;
                case InteractiveControlType.CheckBox:
                case InteractiveControlType.Combo:
                    m_EditText.Enabled = true;
                    m_EditAssociateData.Enabled = true;
                    m_checkReadOnly.Enabled = true;
                    m_checkScreenEvent.Enabled = true;
                    break;
                case InteractiveControlType.Text:
                    m_EditText.Enabled = true;
                    this.AssociateData = "";
                    m_EditAssociateData.Enabled = false;
                    m_checkReadOnly.Enabled = false;
                    m_checkReadOnly.Checked = false;
                    m_checkScreenEvent.Enabled = false;
                    m_checkScreenEvent.Checked = false;
                    break;
                case InteractiveControlType.NumericUpDown:
                case InteractiveControlType.Slider:
                    m_EditText.Enabled = false;
                    this.Txt = "";
                    m_EditAssociateData.Enabled = true;
                    m_checkReadOnly.Enabled = true;
                    m_checkScreenEvent.Enabled = false;
                    m_checkScreenEvent.Checked = false;
                    break;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CommonLib;


namespace SmartApp.Ihm
{
    public partial class DataPropertiesControl : UserControl
    {
        #region données membres
        CComboData[] m_TabCboDataStruct;
        Data m_Data = null;
        bool bLockUpdateRange = false;

        private BTDoc m_Document = null;
        #endregion

        #region events
        public event DataPropertiesChange DataPropertiesChanged;
        #endregion

        #region constructeur
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public DataPropertiesControl()
        {
            m_TabCboDataStruct = new CComboData[6];
            m_TabCboDataStruct[0] = new CComboData("1 bit data",DATA_SIZE.DATA_SIZE_1B);
            m_TabCboDataStruct[1] = new CComboData("2 bits data", DATA_SIZE.DATA_SIZE_2B);
            m_TabCboDataStruct[2] = new CComboData("4 bits data", DATA_SIZE.DATA_SIZE_4B);
            m_TabCboDataStruct[3] = new CComboData("8 bits data", DATA_SIZE.DATA_SIZE_8B);
            m_TabCboDataStruct[4] = new CComboData("16 bits data (signed)", DATA_SIZE.DATA_SIZE_16B);
            m_TabCboDataStruct[5] = new CComboData("16 bits data (unsigned)", DATA_SIZE.DATA_SIZE_16BU);

            InitializeComponent();
            m_cboSize.ValueMember = "Object";
            m_cboSize.DisplayMember = "DisplayedString";
            m_cboSize.DataSource = m_TabCboDataStruct;
            m_cboSize.SelectedIndex = 0;
            this.Enabled = false;
        }
        #endregion

        #region attributs
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public Data Data
        {
            get
            {
                return m_Data;
            }
            set
            {
                m_Data = value;
                if (m_Data != null)
                {
                    this.Enabled = true;
                    this.Description = m_Data.Description;
                    this.Symbol = m_Data.Symbol;
                    bLockUpdateRange = true;
                    //this.DataSize = m_Data.Size;
                    SelectComboSizeIndexAccordingToData();
                    bLockUpdateRange = false;
                    UpdateRangeFromDataSize();
                    this.MinValue = m_Data.Minimum;
                    this.MaxValue = m_Data.Maximum;
                    this.DefaultValue = m_Data.DefaultValue;
                    this.IsConstant = m_Data.IsConstant;
                }
                else
                {
                    this.Description = "";
                    this.Symbol = "";
                    this.DataSizeAndSign = 8;
                    this.MinValue = 0;
                    this.MaxValue = 0;
                    this.DefaultValue = 0;
                    this.IsConstant = false;
                    this.Enabled = false;
                }
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
        #endregion

        #region validation des données
        //*****************************************************************************************************
        // Description: test si les valeurs saisies sont valides et renvoie false si elle ne le sont pas
        // sinon renvoie true
        // Return: /
        //*****************************************************************************************************
        public bool IsDataValuesValid
        {
            get
            {
                if (Data == null)
                    return true;

                if (string.IsNullOrEmpty(this.Symbol))
                    return false;

                bool bRet = true;
                if (DefaultValue < MinValue)
                    bRet = false;
                if (DefaultValue > MaxValue)
                    bRet = false;
                Data dt = (Data)GestData.GetFromSymbol(this.Symbol);
                if (dt != null && dt != Data)
                    bRet = false;

                return bRet;
            }
        }

        //*****************************************************************************************************
        // Description: test si les valeurs saisies sont valides et affiche un message d'erreur concernant
        // ce qui n'est pas valide
        // si les données sont valides, les propriété de la données sont mises a jour, et si elles ont changé
        // l'évènement DataPropertiesChanged est appelé
        // Return: /
        //*****************************************************************************************************
        public bool ValidateValues()
        {
            if (Data == null)
                return true;

            bool bRet = true;
            string strMessage = "";
            if (string.IsNullOrEmpty(this.Symbol))
            {
                strMessage = "Symbol must not be empty";
                bRet = false;
            }

            if (bRet && DefaultValue < MinValue)
            {
                strMessage = "Default value must be superior to minimum value";
                bRet = false;
            }
            if (bRet && DefaultValue > MaxValue)
            {
                strMessage = "Default value must be inferior to maximum value";
                bRet = false;
            }
            Data dt = (Data)GestData.GetFromSymbol(this.Symbol);
            if (bRet && dt != null && dt != Data)
            {
                strMessage = string.Format("A data with symbol {0} already exist", Symbol);
                bRet = false;
            }
            if (!bRet)
            {
                MessageBox.Show(strMessage);
                return bRet;
            }
            bool bDataPropChange = false;
            if (m_Data.Description != this.Description)
                bDataPropChange |= true;
            if (m_Data.Symbol != this.Symbol)
                bDataPropChange |= true;
            if (m_Data.SizeAndSign != this.DataSizeAndSign)
                bDataPropChange |= true;
            if (m_Data.Minimum != this.MinValue)
                bDataPropChange |= true;
            if (m_Data.Maximum != this.MaxValue)
                bDataPropChange |= true;
            if (m_Data.DefaultValue != this.DefaultValue)
                bDataPropChange |= true;
            if (m_Data.IsConstant != this.IsConstant)
                bDataPropChange |= true;

            if (bDataPropChange)
            {
                m_Data.Description = this.Description;
                m_Data.Symbol = this.Symbol;
                m_Data.SizeAndSign = this.DataSizeAndSign;
                m_Data.Minimum = this.MinValue;
                m_Data.Maximum = this.MaxValue;
                m_Data.DefaultValue = this.DefaultValue;
                m_Data.IsConstant = this.IsConstant;
                Doc.Modified = true;
            }
            if (bDataPropChange && DataPropertiesChanged != null)
                DataPropertiesChanged(m_Data);
            return true;
        }
        #endregion

        #region attribut d'accès aux valeurs de la page de propriété
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public string Description
        {
            get
            {
                return m_richTextDesc.Text;
            }
            set
            {
                m_richTextDesc.Text = value;
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
        public int DataSizeAndSign
        {
            get
            {
                if (this.m_cboSize != null && this.m_cboSize.SelectedValue != null)
                    return (int)this.m_cboSize.SelectedValue;
                else
                    return 0;
            }
            set
            {
                if (this.m_cboSize != null)
                    this.m_cboSize.SelectedValue = (int)value;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public int MinValue
        {
            get
            {
                return (int) this.m_numUDMin.Value;
            }
            set
            {
                this.m_numUDMin.Value = (int) value;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public int MaxValue
        {
            get
            {
                return (int) this.m_numUDMax.Value;
            }
            set
            {
                this.m_numUDMax.Value = (int) value;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public int DefaultValue
        {
            get
            {
                return (int) this.m_numUDDefault.Value;
            }
            set
            {
                this.m_numUDDefault.Value = (int)value;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public bool IsConstant
        {
            get
            {
                return this.m_checkConstant.Checked;
            }
            set
            {
                this.m_checkConstant.Checked = value;
            }
        }
        #endregion

        #region handlers d'events
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void OnSizeSelectedIndexChanged(object sender, EventArgs e)
        {
            if (bLockUpdateRange)
                return;
            UpdateRangeFromDataSize();
        }
        #endregion

        #region update de l'IHM
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void UpdateRangeFromDataSize()
        {
            if (m_Data == null)
                return;
            m_Data.SizeAndSign = this.DataSizeAndSign;
            switch ((DATA_SIZE)m_Data.SizeAndSign)
            {
                case DATA_SIZE.DATA_SIZE_1B:
                    m_numUDMax.Maximum = 1;
                    m_numUDMax.Minimum = 0;
                    m_numUDMax.Maximum = 1;
                    m_numUDMin.Minimum = 0;
                    m_numUDDefault.Maximum = 1;
                    m_numUDDefault.Minimum = 0;
                    m_numUDMax.Value = 1;
                    m_numUDMin.Value = 0;
                    m_numUDDefault.Value = 0;
                    break;
                case DATA_SIZE.DATA_SIZE_2B:
                    m_numUDMax.Maximum = 3;
                    m_numUDMax.Minimum = 0;
                    m_numUDMax.Maximum = 3;
                    m_numUDMin.Minimum = 0;
                    m_numUDDefault.Maximum = 3;
                    m_numUDDefault.Minimum = 0;
                    m_numUDMax.Value = 3;
                    m_numUDMin.Value = 0;
                    m_numUDDefault.Value = 0;
                    break;
                case DATA_SIZE.DATA_SIZE_4B:
                    m_numUDMax.Maximum = 15;
                    m_numUDMax.Minimum = 0;
                    m_numUDMax.Maximum = 15;
                    m_numUDMin.Minimum = 0;
                    m_numUDDefault.Maximum = 15;
                    m_numUDDefault.Minimum = 0;
                    m_numUDMax.Value = 15;
                    m_numUDMin.Value = 0;
                    m_numUDDefault.Value = 0;
                    break;
                case DATA_SIZE.DATA_SIZE_8B:
                    m_numUDMax.Maximum = 255;
                    m_numUDMax.Minimum = 0;
                    m_numUDMax.Maximum = 255;
                    m_numUDMin.Minimum = 0;
                    m_numUDDefault.Maximum = 255;
                    m_numUDDefault.Minimum = 0;
                    m_numUDMax.Value = 255;
                    m_numUDMin.Value = 0;
                    m_numUDDefault.Value = 0;
                    break;
                case DATA_SIZE.DATA_SIZE_16B:
                    m_numUDMax.Maximum = 32767;
                    m_numUDMax.Minimum = -32768;
                    m_numUDMax.Maximum = 32767;
                    m_numUDMin.Minimum = -32768;
                    m_numUDDefault.Maximum = 32767;
                    m_numUDDefault.Minimum = -32768;
                    m_numUDMax.Value = 32767;
                    m_numUDMin.Value = -32768;
                    m_numUDDefault.Value = 0;
                    break;
                case DATA_SIZE.DATA_SIZE_16BU:
                    m_numUDMax.Maximum = 0xFFFF;
                    m_numUDMax.Minimum = 0;
                    m_numUDMax.Maximum = 0xFFFF;
                    m_numUDMin.Minimum = 0;
                    m_numUDDefault.Maximum = 0xFFFF;
                    m_numUDDefault.Minimum = 0;
                    m_numUDMax.Value = 0xFFFF;
                    m_numUDMin.Value = 0;
                    break;
                default:
                    System.Diagnostics.Debug.Assert(false);
                    break;
            }
            if (m_numUDDefault.Value < m_numUDMin.Value
                || m_numUDDefault.Value > m_numUDMax.Value)
            {
                m_numUDDefault.Value = 0;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void SelectComboSizeIndexAccordingToData()
        {
            if (m_Data == null)
                return;
            
            switch ((DATA_SIZE)m_Data.SizeAndSign)
            {
                case DATA_SIZE.DATA_SIZE_1B:
                    this.m_cboSize.SelectedIndex = 0;
                    break;
                case DATA_SIZE.DATA_SIZE_2B:
                    this.m_cboSize.SelectedIndex = 1;
                    break;
                case DATA_SIZE.DATA_SIZE_4B:
                    this.m_cboSize.SelectedIndex = 2;
                    break;
                case DATA_SIZE.DATA_SIZE_8B:
                    this.m_cboSize.SelectedIndex = 3;
                    break;
                case DATA_SIZE.DATA_SIZE_16B:
                    this.m_cboSize.SelectedIndex = 4;
                    break;
                case DATA_SIZE.DATA_SIZE_16BU:
                    this.m_cboSize.SelectedIndex = 5;
                    break;
                default:
                    System.Diagnostics.Debug.Assert(false);
                    break;
            }
            
        }
        #endregion

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void PropertiesControlValidating(object sender, CancelEventArgs e)
        {
            if(!ValidateValues())
                e.Cancel = true;
        }
    }
}

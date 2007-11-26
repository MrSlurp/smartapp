using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SmartApp.Gestionnaires;
using SmartApp.Datas;

namespace SmartApp.Ihm
{

    public partial class FramePropertiesControl : UserControl
    {
        #region données membres
        CComboData[] m_TabCboCtrlDataSize;
        CComboData[] m_TabCboCtrlDataType;
        CComboData[] m_TabCboConvType;

        Trame m_Trame = null;
        private BTDoc m_Document = null;
        private bool bLockComboCtrlDataTypeEvent = false;
        #endregion

        #region events
        public event TramePropertiesChange FramePropertiesChanged;
        public event CurrentDataListChanged DataListChange;
        public event BeforeCurrentDataListChange BeforeDataListChange;
        #endregion

        #region constructeur et init
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        public FramePropertiesControl()
        {
            InitializeComponent();
            m_TabCboCtrlDataSize = new CComboData[3];
            m_TabCboCtrlDataSize[0] = new CComboData("8 bits", (int)DATA_SIZE.DATA_SIZE_8B);
            m_TabCboCtrlDataSize[1] = new CComboData("16 bits", (int)DATA_SIZE.DATA_SIZE_16B);
            m_TabCboCtrlDataSize[2] = new CComboData("32 bits", (int)DATA_SIZE.DATA_SIZE_32B);
            m_cboCtrlDataSize.ValueMember = "Object";
            m_cboCtrlDataSize.DisplayMember = "DisplayedString";
            m_cboCtrlDataSize.DataSource = m_TabCboCtrlDataSize;
            m_cboCtrlDataSize.SelectedIndex = 0;

            m_TabCboCtrlDataType = new CComboData[2/*3*/];
            m_TabCboCtrlDataType[0] = new CComboData("None", CTRLDATA_TYPE.NONE.ToString());
            m_TabCboCtrlDataType[1] = new CComboData("Millenium3 SL Bloc CheckSum", CTRLDATA_TYPE.SUM_COMPL_P1.ToString());
            //m_TabCboCtrlDataType[2] = new CComboData("Zelio2 SL Bloc CheckSum", CTRLDATA_TYPE.SUM_COMPL_P2.ToString());
            m_cboCtrlDataType.ValueMember = "Object";
            m_cboCtrlDataType.DisplayMember = "DisplayedString";
            m_cboCtrlDataType.DataSource = m_TabCboCtrlDataType;
            m_cboCtrlDataType.SelectedIndex = 0;

            m_TabCboConvType = new CComboData[2];
            m_TabCboConvType[0] = new CComboData("None", CONVERT_TYPE.NONE.ToString());
            m_TabCboConvType[1] = new CComboData("ASCII", CONVERT_TYPE.ASCII.ToString());
            m_cboConvType.ValueMember = "Object";
            m_cboConvType.DisplayMember = "DisplayedString";
            m_cboConvType.DataSource = m_TabCboConvType;
            m_cboConvType.SelectedIndex = 0;
        }
        #endregion

        #region attributs
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public Trame Trame
        {
            get
            {
                return m_Trame;
            }
            set
            {
                m_Trame= value;
                if (Trame != null)
                {
                    UpdateComboFromTo();
                    this.Enabled = true;
                    this.Description = Trame.Description;
                    this.Symbol = Trame.Symbol;
                    bLockComboCtrlDataTypeEvent = true;
                    this.CtrlDataType = m_Trame.CtrlDataType;
                    bLockComboCtrlDataTypeEvent = false;
                    this.CtrlDataFrom = m_Trame.CtrlDataFrom;
                    this.CtrlDataTo = m_Trame.CtrlDataTo;
                    this.ConvType = m_Trame.ConvType;
                    this.ConvFrom = m_Trame.ConvFrom;
                    this.ConvTo = m_Trame.ConvTo;
                    this.CtrlDataSize = m_Trame.CtrlDataSize;
                }
                else
                {
                    UpdateComboFromTo();
                    this.Description = "";
                    this.Symbol = "";
                    this.CtrlDataType = "";
                    this.CtrlDataSize = 8;
                    this.CtrlDataFrom = 0;
                    this.CtrlDataTo = 0;
                    this.ConvType = "";
                    this.ConvFrom = 0;
                    this.ConvTo = 0;
                    this.Enabled = false;
                }
                UpdateComboFromToEnabling();
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

        public GestData GestData
        {
            get
            {
                return m_Document.GestData;
            }
        }

        public GestTrame GestTrame
        {
            get
            {
                return m_Document.GestTrame;
            }
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

        #region Donnée de control
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public string CtrlDataType
        {
            get
            {
                if (this.m_cboCtrlDataType != null && this.m_cboCtrlDataType.SelectedValue != null)
                    return (string)this.m_cboCtrlDataType.SelectedValue;
                else
                    return "";
            }
            set
            {
                if (this.m_cboCtrlDataType != null && value != null)
                    this.m_cboCtrlDataType.SelectedValue = (string)value;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public int CtrlDataSize
        {
            get
            {
                if (this.m_cboCtrlDataSize != null && this.m_cboCtrlDataSize.SelectedValue != null)
                    return (int)this.m_cboCtrlDataSize.SelectedValue;
                else
                    return 0;
            }
            set
            {
                if (this.m_cboCtrlDataSize != null )
                    this.m_cboCtrlDataSize.SelectedValue = (int)value;

            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public int CtrlDataFrom
        {
            get
            {
                return (int)this.m_cboCtrlDataFrom.SelectedIndex;
            }
            set
            {
                if (this.m_cboCtrlDataFrom.Items.Count > 0 && value < this.m_cboCtrlDataFrom.Items.Count)
                    this.m_cboCtrlDataFrom.SelectedIndex = (int)value;
                else if (value > this.m_cboCtrlDataFrom.Items.Count)
                    this.m_cboCtrlDataFrom.SelectedIndex = (int)value - 1;
                else
                    this.m_cboCtrlDataFrom.SelectedIndex = -1;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public int CtrlDataTo
        {
            get
            {
                return (int)this.m_cboCtrlDataTo.SelectedIndex;
            }
            set
            {
                if (this.m_cboCtrlDataTo.Items.Count > 0 && value < this.m_cboCtrlDataTo.Items.Count)
                    this.m_cboCtrlDataTo.SelectedIndex = (int)value;
                else if ( value > this.m_cboCtrlDataTo.Items.Count)
                    this.m_cboCtrlDataTo.SelectedIndex = (int)value-1;
                else
                    this.m_cboCtrlDataTo.SelectedIndex = -1;
            }
        }
        #endregion

        #region type de conversion
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public string ConvType
        {
            get
            {
                if (this.m_cboConvType != null && this.m_cboConvType.SelectedValue != null)
                    return (string)this.m_cboConvType.SelectedValue;
                else
                    return "";
            }
            set
            {
                if (this.m_cboConvType != null && value != null)
                    this.m_cboConvType.SelectedValue = (string)value;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public int ConvFrom
        {
            get
            {
                return (int)this.m_cboConvFrom.SelectedIndex;
            }
            set
            {
                if (this.m_cboConvFrom.Items.Count > 0 && value < this.m_cboConvFrom.Items.Count)
                    this.m_cboConvFrom.SelectedIndex = (int)value;
                else if (value > this.m_cboConvFrom.Items.Count)
                    this.m_cboConvFrom.SelectedIndex = (int)value - 1;
                else
                    this.m_cboConvFrom.SelectedIndex = -1;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public int ConvTo
        {
            get
            {
                return (int)this.m_cboConvTo.SelectedIndex;
            }
            set
            {
                if (this.m_cboConvTo.Items.Count > 0 && value < this.m_cboConvTo.Items.Count)
                    this.m_cboConvTo.SelectedIndex = (int)value;
                else if (value > this.m_cboConvTo.Items.Count)
                    this.m_cboConvTo.SelectedIndex = (int)value - 1;
                else
                    this.m_cboConvTo.SelectedIndex = -1;
            }
        }
        #endregion
        #endregion

        #region validation des données
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public bool IsTrameValuesValid
        {
            get
            {
                if (m_Trame == null)
                    return true;

                if (string.IsNullOrEmpty(this.Symbol))
                    return false;

                bool bRet = true;
                Trame dt = (Trame)this.GestTrame.GetFromSymbol(this.Symbol);
                if (dt != null && dt != this.Trame)
                    bRet = false;

                return bRet;
            }
        }

        public bool ValidateValues()
        {
            if (m_Trame == null)
                return true;

            string strMessage = "";
            bool bRet = true;
            if (string.IsNullOrEmpty(this.Symbol))
            {
                strMessage = "Symbol must not be empty";
                bRet = false;
            }
            Trame dt = (Trame)this.GestTrame.GetFromSymbol(this.Symbol);
            if (bRet && dt != null && dt != this.Trame)
            {
                strMessage = string.Format("A Frame with symbol {0} already exist", Symbol);
                bRet = false;
            }
            if (!bRet)
            {
                MessageBox.Show(strMessage);
                return bRet;
            }

            bool bChange = false;
            if (m_Trame.Description != this.Description)
                bChange |= true;
            if (m_Trame.Symbol != this.Symbol)
                bChange |= true;

            if (m_Trame.CtrlDataType != this.CtrlDataType)
                bChange |= true;
            if (m_Trame.CtrlDataFrom != this.CtrlDataFrom)
                bChange |= true;
            if (m_Trame.CtrlDataTo != this.CtrlDataTo)
                bChange |= true;

            if (m_Trame.ConvType != this.ConvType)
                bChange |= true;
            if (m_Trame.ConvFrom != this.ConvFrom)
                bChange |= true;
            if (m_Trame.ConvTo != this.ConvTo)
                bChange |= true;

            if (m_Trame.Description != this.Description)
                bChange |= true;
            if (m_Trame.Symbol != this.Symbol)
                bChange |= true;

            if (bChange)
            {
                m_Trame.Description = this.Description;
                m_Trame.Symbol = this.Symbol;
                m_Trame.CtrlDataType = this.CtrlDataType;
                m_Trame.CtrlDataFrom = this.CtrlDataFrom;
                m_Trame.CtrlDataTo = this.CtrlDataTo;
                m_Trame.ConvType = this.ConvType;
                m_Trame.ConvFrom = this.ConvFrom;
                m_Trame.ConvTo = this.ConvTo;
                Doc.Modified = true;
            }

            if (bChange && FramePropertiesChanged != null)
                FramePropertiesChanged(dt);
            return true;
        }

        #endregion

        #region fonction d'update diverses
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void UpdateComboFromTo()
        {
            if (m_Trame == null)
            {
                m_cboConvFrom.Items.Clear();
                m_cboConvTo.Items.Clear();
                m_cboCtrlDataFrom.Items.Clear();
                m_cboCtrlDataTo.Items.Clear();
                return;
            }

            string[] ArrayCtrlData = GetListStringCtrlDataFromto();
            m_cboCtrlDataFrom.Items.Clear();
            m_cboCtrlDataFrom.Items.AddRange(ArrayCtrlData);
            m_cboCtrlDataTo.Items.Clear();
            m_cboCtrlDataTo.Items.AddRange(ArrayCtrlData);
            string[] ArrayConv = GetListStringConvFromto();
            m_cboConvFrom.Items.Clear();
            m_cboConvFrom.Items.AddRange(ArrayConv);
            m_cboConvTo.Items.Clear();
            m_cboConvTo.Items.AddRange(ArrayConv);
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        public void UpdateComboFromToEnabling()
        {
            if (this.CtrlDataType != CTRLDATA_TYPE.NONE.ToString())
            {
                m_cboCtrlDataFrom.Enabled = true;
                m_cboCtrlDataTo.Enabled = true;
                if (this.CtrlDataType == CTRLDATA_TYPE.SUM_COMPL_P1.ToString()
                    || this.CtrlDataType == CTRLDATA_TYPE.SUM_COMPL_P2.ToString())
                {
                    m_cboCtrlDataSize.Enabled = false;
                    this.CtrlDataSize = 8;
                }
                else
                {
                    m_cboCtrlDataSize.Enabled = true;
                }
            }
            else
            {
                m_cboCtrlDataSize.Enabled = false;
                m_cboCtrlDataFrom.Enabled = false;
                m_cboCtrlDataTo.Enabled = false;
            }
            if (this.ConvType != CONVERT_TYPE.NONE.ToString())
            {
                m_cboConvFrom.Enabled = true;
                m_cboConvTo.Enabled = true;
            }
            else
            {
                m_cboConvFrom.Enabled = false;
                m_cboConvTo.Enabled = false;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private string[] GetListStringCtrlDataFromto()
        {
            if (m_Trame == null)
                return null;
            List<string> tempList = new List<string>();
            for (int i = 0; i < m_Trame.FrameDatas.Count; i++)
            {
                if (m_Trame.FrameDatas[i].EndsWith(Cste.STR_SUFFIX_CTRLDATA))
                    break;
                tempList.Add(m_Trame.FrameDatas[i]);
            }
            string[] strArray = new string[tempList.Count];
            for (int i = 0; i < tempList.Count; i++)
            {
                strArray[i] = tempList[i];
            }
            return strArray;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private string[] GetListStringConvFromto()
        {
            if (m_Trame == null)
                return null;
            string[] strArray = new string[m_Trame.FrameDatas.Count];
            for (int i = 0; i < m_Trame.FrameDatas.Count; i++)
            {
                strArray[i] = m_Trame.FrameDatas[i];
            }
            return strArray;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void UpdateControlData()
        {
            // on préviens que la liste des données va changer 
            // la frame form va updater la liste des données de la trame a partir de la 
            // list view
            if (BeforeDataListChange != null)
                BeforeDataListChange();

            // traitement de l'ajout / suppression de la donnée de control
            if (this.CtrlDataType != CTRLDATA_TYPE.NONE.ToString())
            {
                GestData.UpdateAllControlDatas(GestTrame);
                m_Trame.FrameDatas.Add(this.Symbol + Cste.STR_SUFFIX_CTRLDATA);
            }
            else
            {
                for (int i = 0; i < m_Trame.FrameDatas.Count; i++)
                {
                    if (m_Trame.FrameDatas[i].EndsWith(Cste.STR_SUFFIX_CTRLDATA))
                        m_Trame.FrameDatas.Remove(m_Trame.FrameDatas[i]);
                }
            }
            // on préviens que la liste des données a changé
            // la frame form va updater la listview des données de la trame
            if (DataListChange != null)
                DataListChange();
        }
        #endregion

        #region handlers d'events
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void OnComboConvChanged(object sender, EventArgs e)
        {
            UpdateComboFromToEnabling();
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void OnComboDataCtrlChanged(object sender, EventArgs e)
        {
            // meme si on est locké on peux faire de la mise a jour IHM, ca évitera de rester dans un état tordu
            UpdateComboFromToEnabling();
            if (bLockComboCtrlDataTypeEvent)
                return;
            if (m_Trame == null)
                return;
            if (!this.ValidateValues())
            {
                bLockComboCtrlDataTypeEvent = true;
                this.CtrlDataType = m_Trame.CtrlDataType;
                bLockComboCtrlDataTypeEvent = false;
                return;
            }

            UpdateControlData();
            UpdateComboFromTo();
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
    }
}

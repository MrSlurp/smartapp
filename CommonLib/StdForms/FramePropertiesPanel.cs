using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;


namespace CommonLib
{

    public partial class FramePropertiesPanel : BaseObjectPropertiesPanel, IObjectPropertyPanel
    {
        #region données membres
        CComboData[] m_TabCboCtrlDataSize;
        CComboData[] m_TabCboCtrlDataType;
        CComboData[] m_TabCboConvType;

        Trame m_Trame = null;
        private bool bLockComboCtrlDataTypeEvent = false;

        bool m_bLockEvent = false;
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
        public FramePropertiesPanel()
        {
            InitializeComponent();
            LoadNonStandardLang();
            this.m_cboCtrlDataSize.SelectedIndexChanged += new System.EventHandler(this.OncboCtrlDataSizeSelectedIndexChanged);

        }

        public void LoadNonStandardLang()
        {
            m_TabCboCtrlDataSize = new CComboData[4];
            m_TabCboCtrlDataSize[0] = new CComboData(Lang.LangSys.C("8 bits"), (int)DATA_SIZE.DATA_SIZE_8B);
            m_TabCboCtrlDataSize[1] = new CComboData(Lang.LangSys.C("16 bits"), (int)DATA_SIZE.DATA_SIZE_16B);
            m_TabCboCtrlDataSize[2] = new CComboData(Lang.LangSys.C("16 bits (unsigned)"), (int)DATA_SIZE.DATA_SIZE_16BU);
            m_TabCboCtrlDataSize[3] = new CComboData(Lang.LangSys.C("32 bits"), (int)DATA_SIZE.DATA_SIZE_32B);
            m_cboCtrlDataSize.ValueMember = "Object";
            m_cboCtrlDataSize.DisplayMember = "DisplayedString";
            m_cboCtrlDataSize.DataSource = m_TabCboCtrlDataSize;

            m_TabCboCtrlDataType = new CComboData[4];
            m_TabCboCtrlDataType[0] = new CComboData(Lang.LangSys.C("None"), CTRLDATA_TYPE.NONE.ToString());
            m_TabCboCtrlDataType[1] = new CComboData(Lang.LangSys.C("Millenium3 SL Bloc CheckSum"), CTRLDATA_TYPE.SUM_COMPL_P1.ToString());
            m_TabCboCtrlDataType[3] = new CComboData(Lang.LangSys.C("Zelio2 SL Bloc CheckSum"), CTRLDATA_TYPE.SUM_COMPL_P2.ToString());
            m_TabCboCtrlDataType[2] = new CComboData(Lang.LangSys.C("Modbus CRC 16"), CTRLDATA_TYPE.MODBUS_CRC.ToString());
            m_cboCtrlDataType.ValueMember = "Object";
            m_cboCtrlDataType.DisplayMember = "DisplayedString";
            m_cboCtrlDataType.DataSource = m_TabCboCtrlDataType;

            m_TabCboConvType = new CComboData[2];
            m_TabCboConvType[0] = new CComboData(Lang.LangSys.C("None"), CONVERT_TYPE.NONE.ToString());
            m_TabCboConvType[1] = new CComboData(Lang.LangSys.C("ASCII"), CONVERT_TYPE.ASCII.ToString());
            m_cboConvType.ValueMember = "Object";
            m_cboConvType.DisplayMember = "DisplayedString";
            m_cboConvType.DataSource = m_TabCboConvType;
            
            m_bLockEvent = true;
            m_cboCtrlDataSize.SelectedIndex = 0;
            m_bLockEvent = false;
            m_cboCtrlDataType.SelectedIndex = 0;
            m_cboConvType.SelectedIndex = 0;
        }    
        #endregion

        #region attributs
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public BaseObject ConfiguredItem
        {
            get
            {
                return m_Trame;
            }
            set
            {
                m_Trame= value as Trame;
            }
        }

        public BaseGest ConfiguredItemGest
        {
            get { return m_Document.GestTrame; }
            set {}
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
                m_bLockEvent = true;
                if (this.m_cboCtrlDataSize != null )
                    this.m_cboCtrlDataSize.SelectedValue = (int)value;
                m_bLockEvent = false;

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

        public void PanelToObject()
        {
            bool bChange = false;
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

            if (bChange)
            {
                m_Trame.CtrlDataType = this.CtrlDataType;
                m_Trame.CtrlDataFrom = this.CtrlDataFrom;
                m_Trame.CtrlDataSize = this.CtrlDataSize;
                m_Trame.CtrlDataTo = this.CtrlDataTo;
                m_Trame.ConvType = this.ConvType;
                m_Trame.ConvFrom = this.ConvFrom;
                m_Trame.ConvTo = this.ConvTo;

                Document.Modified = true;
            }

            if (bChange && FramePropertiesChanged != null)
                FramePropertiesChanged(m_Trame);
        }

        public void ObjectToPanel()
        {
            if (m_Trame != null)
            {
                UpdateComboFromTo();
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
            UpdateComboFromToEnabling();
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
                    this.CtrlDataSize = (int)DATA_SIZE.DATA_SIZE_8B;
                }
                else if (this.CtrlDataType == CTRLDATA_TYPE.MODBUS_CRC.ToString())
                {
                    m_cboCtrlDataSize.Enabled = false;
                    this.CtrlDataSize = (int)DATA_SIZE.DATA_SIZE_16BU;
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
            if (m_Trame == null)
                return;
            // on préviens que la liste des données va changer 
            // la frame form va updater la liste des données de la trame a partir de la 
            // list view
            if (BeforeDataListChange != null)
                BeforeDataListChange();

            // traitement de l'ajout / suppression de la donnée de control
            if (this.CtrlDataType != CTRLDATA_TYPE.NONE.ToString())
            {
                m_Trame.CtrlDataSize = this.CtrlDataSize;
                GestData.UpdateAllControlDatas(GestTrame);
                // on ne l'ajoute que si la donnée n'y est pas déja
                if (!m_Trame.FrameDatas.Contains(m_Trame.Symbol + Cste.STR_SUFFIX_CTRLDATA))
                    m_Trame.FrameDatas.Add(m_Trame.Symbol + Cste.STR_SUFFIX_CTRLDATA);
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
            if (!this.ValidateProperties())
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
        private void OncboCtrlDataSizeSelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_bLockEvent)
                UpdateControlData();
        }
        #endregion

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartApp.Datas;
using SmartApp.Ihm.Designer;
using SmartApp.Properties;

namespace SmartApp.Ihm.Wizards
{
    public partial class WizardTcpModbusForm : Form
    {
        public BTDoc m_Document;
        CComboData[] m_TabCboFrameType;
        CComboData[] m_TabCboOrderType;
        protected ArrayList m_ListUserData = new ArrayList();
        public const int STANDARD_REGISTER_SIZE = 16;


        public WizardTcpModbusForm()
        {
            InitializeComponent();
            Initialize();
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public void Initialize()
        {
            m_TabCboFrameType = new CComboData[2];
            m_TabCboFrameType[0] = new CComboData("Input Register", TCPMODBUS_REG_TYPE.INPUT_REGISTER);
            m_TabCboFrameType[1] = new CComboData("Output Register", TCPMODBUS_REG_TYPE.OUTPUT_REGISTER);
            m_cboFrameType.DisplayMember = "DisplayedString";
            m_cboFrameType.ValueMember = "Object";
            m_cboFrameType.DataSource = m_TabCboFrameType;
            m_cboFrameType.SelectedIndex = 0;
            InitOrderCombo();

            //InitAdressRangeCombo();
            //m_checkGenerateResp.Checked = false;
            //m_textGeneFramesymb.Enabled = false;
            UpdateDefaultFrameSymbol();
            DefaultInitUserDatas();
            InitListViewData();
            UpdateSplitJoinButtons();
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public void InitOrderCombo()
        {
            if (((TCPMODBUS_REG_TYPE)m_cboFrameType.SelectedValue) == TCPMODBUS_REG_TYPE.OUTPUT_REGISTER)
            {
                m_TabCboOrderType = new CComboData[1];
                m_TabCboOrderType[0] = new CComboData("Read Holding resgister(s)", MODBUS_ORDER_TYPE.READ_HOLDING_REGISTER);
                m_cboReadWrite.SelectedValue = MODBUS_ORDER_TYPE.READ_HOLDING_REGISTER;
            }
            else
            {
                m_TabCboOrderType = new CComboData[3];
                m_TabCboOrderType[0] = new CComboData("Write simple resgister", MODBUS_ORDER_TYPE.WRITE_SINGLE_REGISTER);
                m_TabCboOrderType[1] = new CComboData("Write multiple resgisters", MODBUS_ORDER_TYPE.WRITE_MULTIPLE_REGISTER);
                m_TabCboOrderType[2] = new CComboData("Read Holding resgister(s)", MODBUS_ORDER_TYPE.READ_HOLDING_REGISTER);
            }
            m_cboReadWrite.DisplayMember = "DisplayedString";
            m_cboReadWrite.ValueMember = "Object";
            m_cboReadWrite.DataSource = m_TabCboOrderType;
            if (m_cboReadWrite.SelectedValue == null)
                m_cboReadWrite.SelectedIndex = 0;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public void UpdateRegAddrAndCountFields()
        {
            switch ((MODBUS_ORDER_TYPE)m_cboReadWrite.SelectedValue)
            {
                case MODBUS_ORDER_TYPE.READ_HOLDING_REGISTER:
                    m_numNumberOfReg.Enabled = true;
                    break;
                case MODBUS_ORDER_TYPE.WRITE_MULTIPLE_REGISTER:
                    m_numNumberOfReg.Enabled = true;
                    break;
                case MODBUS_ORDER_TYPE.WRITE_SINGLE_REGISTER:
                    m_numNumberOfReg.Value = 1;
                    m_numNumberOfReg.Enabled = false;
                    break;
                default:
                    System.Diagnostics.Debug.Assert(false);
                    break;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void OncboFrameTypeSelectedIndexChanged(object sender, EventArgs e)
        {
            InitOrderCombo();
            DefaultInitUserDatas();
            InitListViewData();
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void OncheckGenerateRespCheckedChanged(object sender, EventArgs e)
        {
            UpdateControlsStates();
            UpdateDefaultFrameSymbol();
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void OnbtnCancelClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void m_numStartingRegAddr_ValueChanged(object sender, EventArgs e)
        {
            DefaultInitUserDatas();
            InitListViewData();
            UpdateDefaultFrameSymbol();
            UpdateSplitJoinButtons();
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void m_numNumberOfReg_ValueChanged(object sender, EventArgs e)
        {
            DefaultInitUserDatas();
            InitListViewData();
            UpdateDefaultFrameSymbol();
            UpdateSplitJoinButtons();
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void OncboReadWriteSelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDefaultFrameSymbol();
            UpdateControlsStates();
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void UpdateControlsStates()
        {
            m_checkGenerateResp.Checked = true;
            m_checkGenerateResp.Enabled = false;
            m_ListViewDatas.Enabled = true;
            m_textGeneFramesymb.Enabled = true;

            UpdateRegAddrAndCountFields();
        }


        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void m_ListViewDatas_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSplitJoinButtons();
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void UpdateDefaultFrameSymbol()
        {
            string strFrame = GetDefaultFrameSymbol(false);
            m_textFramesymb.Text = strFrame;
            if (m_checkGenerateResp.Checked)
            {
                m_textGeneFramesymb.Text = GetDefaultFrameSymbol(true);
            }
            else
                m_textGeneFramesymb.Text = "";
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public void DefaultInitUserDatas()
        {
            m_ListUserData.Clear();
            if (m_cboFrameType.SelectedValue == null)
                return;

            m_ListUserData = GenerateDefaultFrameData((TCPMODBUS_REG_TYPE)m_cboFrameType.SelectedValue,
                                                         (int)m_numStartingRegAddr.Value,
                                                         (int)m_numNumberOfReg.Value);
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public void InitListViewData()
        {
            m_ListViewDatas.Items.Clear();
            int CurDataBit = 0;
            for (int i = 0; i < m_ListUserData.Count; i++)
            {
                Data dt = (Data)m_ListUserData[i];
                ListViewItem lviData = m_ListViewDatas.Items.Add(dt.Symbol);
                lviData.SubItems.Add(dt.SizeInBits.ToString());
                int CurByte = CurDataBit / STANDARD_REGISTER_SIZE + 1;
                lviData.SubItems.Add(CurByte.ToString());
                lviData.Tag = dt;
                CurDataBit += dt.SizeInBits;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        static public ArrayList GenerateDefaultFrameData(TCPMODBUS_REG_TYPE RgType,
                                                         int StartAddr,
                                                         int nbReg)
        {
            int CurBitPos = 0;
            ArrayList ListData = new ArrayList();

            string strSymb;
            for (int i = 0; i < nbReg; i++)
            {
                strSymb = GetDefaultSymbolTCPModbus(RgType, StartAddr, CurBitPos, STANDARD_REGISTER_SIZE);
                ListData.Add(new Data(strSymb, 0, STANDARD_REGISTER_SIZE, false));
                CurBitPos += STANDARD_REGISTER_SIZE; // non, pas dans le cas du TCP modbus
                StartAddr += 1; // on incrément pour changer le nom de regristre en fonction de son adresse
            }
            return ListData;
        }

        #region obtention des symbols par défaut des trames et données
        //*****************************************************************************************************
        // Description: renvoie le symbol par défaut d'un donnée en fonction des paramètres de la trame
        // prend en entrée le type de trame (in out), l'adresse, la DataBit pos valeur comprise entre 1 et 16
        // et la taille de la donnée en bits
        // Return: /
        //*****************************************************************************************************
        static private string GetDefaultSymbolTCPModbus(TCPMODBUS_REG_TYPE RgType,
                                                     int StartAddr,
                                                     int DataBitPos,
                                                     int DataSize)
        {
            string InOrOutBloc = "";
            switch (RgType)
            {
                case TCPMODBUS_REG_TYPE.INPUT_REGISTER:
                    InOrOutBloc = "IN";
                    break;
                case TCPMODBUS_REG_TYPE.OUTPUT_REGISTER:
                    InOrOutBloc = "OUT";
                    break;
                default:
                    System.Diagnostics.Debug.Assert(false);
                    break;
            }

            int iCurrentIO = (DataBitPos / STANDARD_REGISTER_SIZE);
            string strRegAddr = string.Format("{0}", StartAddr);

            string FormatString = "TCPMB_{0}_REG_{1}"; // IN/OUT, adresse de registre
            // si la donnée ne fait pas 16 bits, on ajoute une info sur ses bits
            string FinFormatForDataOther = "_B{0}-{1}"; // 1 à 16, 1 à 16 (noméro de bit de debut, numéro du bit de fin
            string strBaseSmbol = string.Format(FormatString, InOrOutBloc, strRegAddr);
            int DataBitPosInCurByte = DataBitPos - ((iCurrentIO) * STANDARD_REGISTER_SIZE);
            if (DataSize == (int)DATA_SIZE.DATA_SIZE_16B || DataSize == (int)DATA_SIZE.DATA_SIZE_16BU)
                return strBaseSmbol;
            else if (DataSize == (int)DATA_SIZE.DATA_SIZE_8B)
            {
                string strLSBorMSB = "";
                if (DataBitPosInCurByte == 0)
                    strLSBorMSB = "_MSB";
                else
                    strLSBorMSB = "_LSB";

                return strBaseSmbol + strLSBorMSB;
            }
            else if (DataSize == (int)DATA_SIZE.DATA_SIZE_1B)
            {
                return strBaseSmbol + string.Format("_B{0}", STANDARD_REGISTER_SIZE - (DataBitPosInCurByte)); // 1 à 16, 1 à 16 (noméro de bit)
            }
            else // donnée de moin de 1 octet
            {
                return strBaseSmbol + string.Format(FinFormatForDataOther, STANDARD_REGISTER_SIZE - (DataBitPosInCurByte + DataSize) + 1, STANDARD_REGISTER_SIZE - (DataBitPosInCurByte)); // 1 à 16, 1 à 16 (noméro de bit de debut, numéro du bit de fin
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private string GetDefaultFrameSymbol(bool bResp)
        {
            string FormatString = "TCPMB_{0}_{1}_{2}_REG_FROM_AD{3}";// READ/WRITE, nombre de registre, IN/OUT, adresse de départ
            string RespFormatString = "TCPMB_RESP_{0}_{1}_{2}_REG_FROM_AD{3}";// READ/WRITE, nombre de registre, IN/OUT, adresse de départ
            if (m_cboReadWrite.SelectedValue == null
                || m_cboFrameType.SelectedValue == null
                )
                return "";

            string ReadOrWrite = "";
            switch ((MODBUS_ORDER_TYPE)m_cboReadWrite.SelectedValue)
            {
                case MODBUS_ORDER_TYPE.READ_HOLDING_REGISTER:
                    ReadOrWrite = "READ";
                    break;
                case MODBUS_ORDER_TYPE.WRITE_MULTIPLE_REGISTER:
                case MODBUS_ORDER_TYPE.WRITE_SINGLE_REGISTER:
                    ReadOrWrite = "WRITE";
                    break;
                default:
                    System.Diagnostics.Debug.Assert(false);
                    break;
            }

            string InOrOut = "";
            switch ((TCPMODBUS_REG_TYPE)m_cboFrameType.SelectedValue)
            {
                case TCPMODBUS_REG_TYPE.INPUT_REGISTER:
                    InOrOut = "IN";
                    break;
                case TCPMODBUS_REG_TYPE.OUTPUT_REGISTER:
                    InOrOut = "OUT";
                    break;
                default:
                    System.Diagnostics.Debug.Assert(false);
                    break;
            }

            int FromAddr = (int)m_numStartingRegAddr.Value;
            int NumberOfReg = (int)m_numNumberOfReg.Value;

            if (bResp)
                return string.Format(RespFormatString, ReadOrWrite, NumberOfReg, InOrOut, FromAddr);
            else
                return string.Format(FormatString, ReadOrWrite, NumberOfReg, InOrOut, FromAddr);

        }
        #endregion

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void UpdateSplitJoinButtons()
        {
            if (m_ListViewDatas.SelectedItems.Count == 0
                || m_ListViewDatas.SelectedItems.Count > 2
                || m_ListViewDatas.Enabled == false)
            {
                m_btnJoin.Enabled = false;
                m_btnSplit.Enabled = false;
            }
            else if (m_ListViewDatas.SelectedItems.Count == 1)
            {
                Data dt = (Data)m_ListViewDatas.SelectedItems[0].Tag;
                if (dt.SizeInBits > 1)
                {
                    m_btnSplit.Enabled = true;
                }
                else
                    m_btnSplit.Enabled = false;

                m_btnJoin.Enabled = false;
            }
            else if (m_ListViewDatas.SelectedItems.Count == 2)
            {

                m_btnSplit.Enabled = false;
                ListViewItem lviData1 = m_ListViewDatas.SelectedItems[0];
                ListViewItem lviData2 = m_ListViewDatas.SelectedItems[1];
                //la colonne 2 correspond au numéro du registre de la donnée
                // on ne peux relier deux données que si elle sont sur le meme registre
                // et consécutives
                // et si les deux données font la meme taille
                if ((lviData1.Index == lviData2.Index + 1 || lviData1.Index + 1 == lviData2.Index)
                    && lviData1.SubItems[2].Text == lviData2.SubItems[2].Text
                    && lviData1.SubItems[1].Text == lviData2.SubItems[1].Text)
                {
                    m_btnJoin.Enabled = true;
                }
                else
                    m_btnJoin.Enabled = false;

            }
        }
        
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void m_btnSplit_Click(object sender, EventArgs e)
        {
            TCPMODBUS_REG_TYPE frType = (TCPMODBUS_REG_TYPE)m_cboFrameType.SelectedValue;
            int StartAddr = (int) m_numStartingRegAddr.Value;
            int nbReg  = (int) m_numNumberOfReg.Value;
            int CurStartAddr = StartAddr;
            if (m_ListViewDatas.SelectedItems.Count == 1)
            {
                Data dt = (Data)m_ListViewDatas.SelectedItems[0].Tag;
                if (dt.SizeInBits > 1 && ((dt.SizeInBits % 2) == 0)) // la taille de la donnée doit être paire
                {
                    int iCurUserDataBit = 0;
                    for (int indexSplit = 0; indexSplit < m_ListUserData.Count; indexSplit++)
                    {
                        Data uData = (Data)m_ListUserData[indexSplit];
                        if (uData.Symbol == dt.Symbol)
                        {
                            int newDataSize = dt.SizeInBits / 2;
                            string strSymb1 = GetDefaultSymbolTCPModbus(frType, CurStartAddr, iCurUserDataBit, newDataSize);
                            string strSymb2 = GetDefaultSymbolTCPModbus(frType, CurStartAddr, iCurUserDataBit + newDataSize, newDataSize);
                            Data newData1 = new Data(strSymb1, 0, newDataSize, false);
                            Data newData2 = new Data(strSymb2, 0, newDataSize, false);
                            m_ListUserData[indexSplit] = newData2;
                            m_ListUserData.Insert(indexSplit, newData1);
                            this.InitListViewData();
                            break;
                        }
                        iCurUserDataBit += uData.SizeInBits;
                        CurStartAddr = StartAddr + (iCurUserDataBit/STANDARD_REGISTER_SIZE); // on incrément pour changer le nom de regristre en fonction de son adresse
                    }
                }
            }
            UpdateSplitJoinButtons();
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void m_btnJoin_Click(object sender, EventArgs e)
        {
            TCPMODBUS_REG_TYPE frType = (TCPMODBUS_REG_TYPE)m_cboFrameType.SelectedValue;
            int StartAddr = (int)m_numStartingRegAddr.Value;
            int nbReg = (int)m_numNumberOfReg.Value;
            int CurStartAddr = StartAddr;

            if (m_ListViewDatas.SelectedItems.Count == 2)
            {
                Data dt1 = (Data)m_ListViewDatas.SelectedItems[0].Tag;
                Data dt2 = (Data)m_ListViewDatas.SelectedItems[1].Tag;
                if (dt1.SizeInBits == dt2.SizeInBits && ((dt1.SizeInBits + dt2.SizeInBits) % 2 == 0)) // la taille totale de la donnée doit être paire
                {
                    Data FirstData = null;
                    if (m_ListViewDatas.SelectedItems[0].Index < m_ListViewDatas.SelectedItems[1].Index)
                        FirstData = dt1;
                    else
                        FirstData = dt2;

                    int iCurUserDataBit = 0;
                    for (int indexJoin = 0; indexJoin < m_ListUserData.Count; indexJoin++)
                    {
                        Data uData = (Data)m_ListUserData[indexJoin];
                        if (uData.Symbol == FirstData.Symbol)
                        {
                            int newDataSize = dt1.SizeInBits + dt2.SizeInBits;
                            string strSymb1 = GetDefaultSymbolTCPModbus(frType, CurStartAddr, iCurUserDataBit, newDataSize);
                            Data newData1 = new Data(strSymb1, 0, newDataSize, false);
                            m_ListUserData[indexJoin] = newData1;
                            m_ListUserData.RemoveAt(indexJoin + 1);
                            this.InitListViewData();
                            break;
                        }
                        iCurUserDataBit += uData.SizeInBits;
                        CurStartAddr = StartAddr + (iCurUserDataBit / STANDARD_REGISTER_SIZE); // on incrément pour changer le nom de regristre en fonction de son adresse
                    }
                }
            }
            UpdateSplitJoinButtons();
        }
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void OnbtnOkClick(object sender, EventArgs e)
        {
            if (m_cboReadWrite.SelectedValue == null)
                return;

            MODBUS_ORDER_TYPE OrderType = (MODBUS_ORDER_TYPE)m_cboReadWrite.SelectedValue;
            if (OrderType == MODBUS_ORDER_TYPE.READ_HOLDING_REGISTER)
            {
                ArrayList ListFrameData = WizardFrameGenerator.GenerateTCPMBReqReadFrameDatas(m_textFramesymb.Text,
                                                                                              OrderType,
                                                                                              (int)m_numStartingRegAddr.Value,
                                                                                              (int)m_numNumberOfReg.Value,
                                                                                              m_ListUserData);
                Trame tr = WizardFrameGenerator.CreateTCPMBFrameObject(m_textFramesymb.Text, ListFrameData);
                WizardFrameGenerator.InsertFrameInDoc(this.m_Document, tr, ListFrameData);
                if (this.m_checkGenerateResp.Checked == true)
                {
                    ArrayList ListRespFrameData = WizardFrameGenerator.GenerateTCPMBRespReqReadFrameDatas(m_textGeneFramesymb.Text,
                                                                                                  OrderType,
                                                                                                  (int)m_numStartingRegAddr.Value,
                                                                                                  (int)m_numNumberOfReg.Value,
                                                                                                  m_ListUserData);
                    Trame Resptr = WizardFrameGenerator.CreateTCPMBFrameObject(m_textGeneFramesymb.Text, ListFrameData);
                    WizardFrameGenerator.InsertFrameInDoc(this.m_Document, Resptr, ListRespFrameData);
                }
            }
            else
            {
                ArrayList ListFrameData = WizardFrameGenerator.GenerateTCPMBWriteFrameDatas(m_textFramesymb.Text,
                                                                                              OrderType,
                                                                                              (int)m_numStartingRegAddr.Value,
                                                                                              (int)m_numNumberOfReg.Value,
                                                                                              m_ListUserData);
                Trame tr = WizardFrameGenerator.CreateTCPMBFrameObject(m_textFramesymb.Text, ListFrameData);
                WizardFrameGenerator.InsertFrameInDoc(this.m_Document, tr, ListFrameData);
                // dans le cas ou on lit un seul registre, la trame de réponse est égale a celle envoyée
                if (this.m_checkGenerateResp.Checked == true && OrderType == MODBUS_ORDER_TYPE.WRITE_SINGLE_REGISTER)
                {
                    ArrayList ListRespFrameData = WizardFrameGenerator.GenerateTCPMBWriteFrameDatas(m_textGeneFramesymb.Text,
                                                                                                  OrderType,
                                                                                                  (int)m_numStartingRegAddr.Value,
                                                                                                  (int)m_numNumberOfReg.Value,
                                                                                                  m_ListUserData);
                    Trame Resptr = WizardFrameGenerator.CreateTCPMBFrameObject(m_textGeneFramesymb.Text, ListFrameData);
                    WizardFrameGenerator.InsertFrameInDoc(this.m_Document, Resptr, ListRespFrameData);
                }
                // dans le cas ou c'est une écriture multiple, la trame est différente
                else if (this.m_checkGenerateResp.Checked == true && OrderType == MODBUS_ORDER_TYPE.WRITE_MULTIPLE_REGISTER)
                {
                    ArrayList ListRespFrameData = WizardFrameGenerator.GenerateTCPMBRespWriteMRFrameDatas(m_textGeneFramesymb.Text,
                                                                                                  OrderType,
                                                                                                  (int)m_numStartingRegAddr.Value,
                                                                                                  (int)m_numNumberOfReg.Value,
                                                                                                  m_ListUserData);
                    Trame Resptr = WizardFrameGenerator.CreateTCPMBFrameObject(m_textGeneFramesymb.Text, ListFrameData);
                    WizardFrameGenerator.InsertFrameInDoc(this.m_Document, Resptr, ListRespFrameData);
                }
            }
            this.DialogResult = DialogResult.OK;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void m_ListViewDatas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 's')
            {
                m_btnSplit_Click(null, null);
            }
            else if (e.KeyChar == 'j')
            {
                m_btnJoin_Click(null, null);
            }
        }
    }
}
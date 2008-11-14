using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommonLib;
using SmartApp.Ihm.Designer;

namespace SmartApp.Ihm.Wizards
{
    public partial class WizardSLForm : Form
    {
        public BTDoc m_Document;
        CComboData[] m_TabCboFrameType;
        CComboData[] m_TabCboOrderType;
        CComboData[] m_TabCboAdressRange;
        protected ArrayList m_ListUserData = new ArrayList();
        public const int M3_SL_IO_SIZE = 16;

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public WizardSLForm()
        {
            InitializeComponent();
            Initialize();
        }

        #region init & update functions
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public void Initialize()
        {
            m_TabCboFrameType = new CComboData[2];
            m_TabCboFrameType[0] = new CComboData("M3 SL Input Bloc", WIZ_M3SL_FRAME_TYPE.SL_INPUT_BLOC);
            m_TabCboFrameType[1] = new CComboData("M3 SL Output Bloc", WIZ_M3SL_FRAME_TYPE.SL_OUTPUT_BLOC);
            m_cboFrameType.DisplayMember = "DisplayedString";
            m_cboFrameType.ValueMember = "Object";
            m_cboFrameType.DataSource = m_TabCboFrameType;
            m_cboFrameType.SelectedIndex = 0;
            InitOrderCombo();

            InitAdressRangeCombo();
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
            m_TabCboOrderType = new CComboData[2];
            m_TabCboOrderType[0] = new CComboData("Read", WIZ_M3SL_ORDER_TYPE.READ);
            m_TabCboOrderType[1] = new CComboData("Write", WIZ_M3SL_ORDER_TYPE.WRITE);
            m_cboReadWrite.DisplayMember = "DisplayedString";
            m_cboReadWrite.ValueMember = "Object";
            m_cboReadWrite.DataSource = m_TabCboOrderType;
            if (((WIZ_M3SL_FRAME_TYPE)m_cboFrameType.SelectedValue) == WIZ_M3SL_FRAME_TYPE.SL_OUTPUT_BLOC)
            {
                m_cboReadWrite.SelectedValue = WIZ_M3SL_ORDER_TYPE.READ;
                m_cboReadWrite.Enabled = false;
            }
            else
            {
                if (m_cboReadWrite.SelectedValue == null)
                    m_cboReadWrite.SelectedIndex = 0;

                m_cboReadWrite.Enabled = true;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public void InitAdressRangeCombo()
        {
            // trois sufisent car on en affiche jamais plus de trois a la fois
            m_TabCboAdressRange = new CComboData[3];
            if (((WIZ_M3SL_FRAME_TYPE)m_cboFrameType.SelectedValue) == WIZ_M3SL_FRAME_TYPE.SL_INPUT_BLOC)
            {
                m_TabCboAdressRange[0] = new CComboData("1-8", WIZ_M3SL_ADRESS_RANGE.ADDR_1_8);
                m_TabCboAdressRange[1] = new CComboData("9-16", WIZ_M3SL_ADRESS_RANGE.ADDR_9_16);
                m_TabCboAdressRange[2] = new CComboData("17-24", WIZ_M3SL_ADRESS_RANGE.ADDR_17_24);
            }
            else if (((WIZ_M3SL_FRAME_TYPE)m_cboFrameType.SelectedValue) == WIZ_M3SL_FRAME_TYPE.SL_OUTPUT_BLOC)
            {
                m_TabCboAdressRange[0] = new CComboData("25-32", WIZ_M3SL_ADRESS_RANGE.ADDR_25_32);
                m_TabCboAdressRange[1] = new CComboData("33-40", WIZ_M3SL_ADRESS_RANGE.ADDR_33_40);
                m_TabCboAdressRange[2] = new CComboData("41-48", WIZ_M3SL_ADRESS_RANGE.ADDR_41_48);
            }

            m_cboAdressRange.DisplayMember = "DisplayedString";
            m_cboAdressRange.ValueMember = "Object";
            m_cboAdressRange.DataSource = m_TabCboAdressRange;
            if (m_cboAdressRange.Items.Count > 0)
                m_cboAdressRange.SelectedIndex = 0;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void UpdateSLBlocBitmap()
        {
            if (m_cboAdressRange.SelectedValue != null)
            {
                Bitmap ImgBlocSL = null;
                switch ((WIZ_M3SL_ADRESS_RANGE)m_cboAdressRange.SelectedValue)
                {
                    case WIZ_M3SL_ADRESS_RANGE.ADDR_1_8:
                        ImgBlocSL = Resources.SLI1;
                        break;
                    case WIZ_M3SL_ADRESS_RANGE.ADDR_9_16:
                        ImgBlocSL = Resources.SLI2;
                        break;
                    case WIZ_M3SL_ADRESS_RANGE.ADDR_17_24:
                        ImgBlocSL = Resources.SLI3;
                        break;
                    case WIZ_M3SL_ADRESS_RANGE.ADDR_25_32:
                        ImgBlocSL = Resources.SLO1;
                        break;
                    case WIZ_M3SL_ADRESS_RANGE.ADDR_33_40:
                        ImgBlocSL = Resources.SLO2;
                        break;
                    case WIZ_M3SL_ADRESS_RANGE.ADDR_41_48:
                        ImgBlocSL = Resources.SLO3;
                        break;
                    default:
                        System.Diagnostics.Debug.Assert(false, "adresse de registre invalide");
                        break;
                }
                if (ImgBlocSL != null)
                    ImgBlocSL.MakeTransparent(ControlPainter.TransparencyColor);
                m_PicSLBloc.Image = ImgBlocSL;
            }
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
            if (m_cboFrameType.SelectedValue == null
                || m_cboAdressRange.SelectedValue == null)
                return;

            m_ListUserData = GenerateDefaultM3SLBlocData((WIZ_M3SL_FRAME_TYPE) m_cboFrameType.SelectedValue,
                                                         (WIZ_M3SL_ADRESS_RANGE) m_cboAdressRange.SelectedValue);

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
                int CurByte = CurDataBit / M3_SL_IO_SIZE +1;
                lviData.SubItems.Add(CurByte.ToString());
                lviData.Tag = dt;

                CurDataBit += dt.SizeInBits;
            }
        }
        #endregion

        #region Handlers d'events
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void OncboFrameTypeSelectedIndexChanged(object sender, EventArgs e)
        {
            InitOrderCombo();
            InitAdressRangeCombo();
            DefaultInitUserDatas();
            InitListViewData();
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void OnbtnOkClick(object sender, EventArgs e)
        {
            if (m_cboReadWrite.SelectedValue == null
                || m_cboAdressRange.SelectedValue == null)
                return;

            WIZ_M3SL_ORDER_TYPE OrderType = (WIZ_M3SL_ORDER_TYPE)m_cboReadWrite.SelectedValue;
            if (OrderType == WIZ_M3SL_ORDER_TYPE.READ)
            {
                ArrayList ListFrameData = WizardFrameGenerator.GenerateM3ReqReadSLFrameDatas(m_textFramesymb.Text,
                                                               (WIZ_M3SL_ADRESS_RANGE) m_cboAdressRange.SelectedValue
                                                               );
                Trame tr = WizardFrameGenerator.CreateM3FrameObject(m_textFramesymb.Text, ListFrameData);
                WizardFrameGenerator.InsertFrameInDoc(this.m_Document, tr, ListFrameData);
                if (this.m_checkGenerateResp.Checked == true)
                {
                    ArrayList ListRespFrameData = WizardFrameGenerator.GenerateM3RespReqReadSLFrameDatas(m_textGeneFramesymb.Text,
                                                                   (WIZ_M3SL_ADRESS_RANGE)m_cboAdressRange.SelectedValue,
                                                                   m_ListUserData);
                    Trame Resptr = WizardFrameGenerator.CreateM3FrameObject(m_textGeneFramesymb.Text, ListRespFrameData);
                    WizardFrameGenerator.InsertFrameInDoc(this.m_Document, Resptr, ListRespFrameData);
                }
            }
            else
            {
                ArrayList ListFrameData = WizardFrameGenerator.GenerateM3WriteSLFrameDatas(m_textFramesymb.Text,
                                                               (WIZ_M3SL_ADRESS_RANGE)m_cboAdressRange.SelectedValue,
                                                               m_ListUserData);
                Trame tr = WizardFrameGenerator.CreateM3FrameObject(m_textFramesymb.Text, ListFrameData);
                WizardFrameGenerator.InsertFrameInDoc(this.m_Document, tr, ListFrameData);
                if (this.m_checkGenerateResp.Checked == true)
                {
                    ArrayList ListRespFrameData = WizardFrameGenerator.GenerateM3RespWriteSLFrameDatas(m_textGeneFramesymb.Text,
                                                                   (WIZ_M3SL_ADRESS_RANGE)m_cboAdressRange.SelectedValue,
                                                                   m_ListUserData);
                    Trame Resptr = WizardFrameGenerator.CreateM3FrameObject(m_textGeneFramesymb.Text, ListRespFrameData);
                    WizardFrameGenerator.InsertFrameInDoc(this.m_Document, Resptr, ListRespFrameData);
                }

            }
            this.DialogResult = DialogResult.OK;
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
        private void OncboAdressRangeSelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSLBlocBitmap();
            UpdateDefaultFrameSymbol();
            DefaultInitUserDatas();
            InitListViewData();
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
        private void OncheckGenerateRespCheckedChanged(object sender, EventArgs e)
        {
            UpdateControlsStates();
            UpdateDefaultFrameSymbol();
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

        }
        #endregion

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        static public ArrayList GenerateDefaultM3SLBlocData(WIZ_M3SL_FRAME_TYPE frType,
                                                     WIZ_M3SL_ADRESS_RANGE AddrRange)
        {
            int CurBitPos = 0;
            ArrayList ListData = new ArrayList();
            string strSymb = GetDefaultSymbolM3Data(frType, AddrRange, CurBitPos, 16);
            ListData.Add(new Data(strSymb, 0, 16, false));
            CurBitPos += 16;
            strSymb = GetDefaultSymbolM3Data(frType, AddrRange, CurBitPos, 16);
            ListData.Add(new Data(strSymb, 0, 16, false));
            CurBitPos += 16;
            strSymb = GetDefaultSymbolM3Data(frType, AddrRange, CurBitPos, 16);
            ListData.Add(new Data(strSymb, 0, 16, false));
            CurBitPos += 16;
            strSymb = GetDefaultSymbolM3Data(frType, AddrRange, CurBitPos, 16);
            ListData.Add(new Data(strSymb, 0, 16, false));
            CurBitPos += 16;
            strSymb = GetDefaultSymbolM3Data(frType, AddrRange, CurBitPos, 16);
            ListData.Add(new Data(strSymb, 0, 16, false));
            CurBitPos += 16;
            strSymb = GetDefaultSymbolM3Data(frType, AddrRange, CurBitPos, 16);
            ListData.Add(new Data(strSymb, 0, 16, false));
            CurBitPos += 16;
            strSymb = GetDefaultSymbolM3Data(frType, AddrRange, CurBitPos, 16);
            ListData.Add(new Data(strSymb, 0, 16, false));
            CurBitPos += 16;
            strSymb = GetDefaultSymbolM3Data(frType, AddrRange, CurBitPos, 16);
            ListData.Add(new Data(strSymb, 0, 16, false));
            return ListData;
        }

        #region obtention des symbols par défaut des trames et données
        //*****************************************************************************************************
        // Description: renvoie le symbol par défaut d'un donnée en fonction des paramètres de la trame
        // prend en entrée le type de trame (in out), l'adresse, la DataBit pos valeur comprise entre 1 et 16
        // et la taille de la donnée en bits
        // Return: /
        //*****************************************************************************************************
        static private string GetDefaultSymbolM3Data(WIZ_M3SL_FRAME_TYPE frType,
                                                     WIZ_M3SL_ADRESS_RANGE AddrRange,
                                                     int DataBitPos,
                                                     int DataSize)
        {
            string InOrOutBloc = "";
            string InOrOutData = "";
            switch (frType)
            {
                case WIZ_M3SL_FRAME_TYPE.SL_INPUT_BLOC:
                    InOrOutBloc = "IN";
                    InOrOutData = "O";
                    break;
                case WIZ_M3SL_FRAME_TYPE.SL_OUTPUT_BLOC:
                    InOrOutBloc = "OUT";
                    InOrOutData = "I";
                    break;
                default:
                    System.Diagnostics.Debug.Assert(false);
                    break;
            }

            int indexSLBloc = 0;
            switch (AddrRange)
            {
                case WIZ_M3SL_ADRESS_RANGE.ADDR_1_8:
                case WIZ_M3SL_ADRESS_RANGE.ADDR_25_32:
                    indexSLBloc = 1;
                    break;
                case WIZ_M3SL_ADRESS_RANGE.ADDR_9_16:
                case WIZ_M3SL_ADRESS_RANGE.ADDR_33_40:
                    indexSLBloc = 2;
                    break;
                case WIZ_M3SL_ADRESS_RANGE.ADDR_17_24:
                case WIZ_M3SL_ADRESS_RANGE.ADDR_41_48:
                    indexSLBloc = 3;
                    break;
                default:
                    System.Diagnostics.Debug.Assert(false, "adresse de registre invalide");
                    break;
            }

            int iCurrentIO = (DataBitPos / M3_SL_IO_SIZE) + 1;
            string FormatString = "M3_SL{0}{1}_{2}{3}"; // IN/OUT, 1/2/3, I/O, 1 à 8 (numéro de l'E/S du bloc)
            // si la donnée ne fait pas 16 bits, on ajoute une info sur ses bits
            string FinFormatForDataOther = "_B{0}-{1}"; // 1 à 16, 1 à 16 (noméro de bit de debut, numéro du bit de fin
            string strBaseSmbol = string.Format(FormatString, InOrOutBloc, indexSLBloc, InOrOutData, iCurrentIO);
            int DataBitPosInCurByte = DataBitPos - ((iCurrentIO - 1) * M3_SL_IO_SIZE);
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
                return strBaseSmbol + string.Format("_B{0}", M3_SL_IO_SIZE - (DataBitPosInCurByte)); // 1 à 16, 1 à 16 (noméro de bit)
            }
            else // donnée de moin de 1 octet
            {
                return strBaseSmbol + string.Format(FinFormatForDataOther, M3_SL_IO_SIZE-(DataBitPosInCurByte + DataSize)+1, M3_SL_IO_SIZE - (DataBitPosInCurByte) ); // 1 à 16, 1 à 16 (noméro de bit de debut, numéro du bit de fin
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private string GetDefaultFrameSymbol(bool bResp)
        {
            string FormatString = "M3_{0}_SL{1}_BLOC_{2}";// READ/WRITE, IN_OUT, 1/2/3 selon la plage d'adresse;
            string RespFormatString = "M3_RESP_{0}_SL{1}_BLOC_{2}";// READ/WRITE, IN_OUT, 1/2/3 selon la plage d'adresse;
            if (m_cboReadWrite.SelectedValue == null
                || m_cboFrameType.SelectedValue == null
                || m_cboAdressRange.SelectedValue == null
                )
                return "";

            string ReadOrWrite = ((WIZ_M3SL_ORDER_TYPE)m_cboReadWrite.SelectedValue).ToString();
            string InOrOut = "";
            switch ((WIZ_M3SL_FRAME_TYPE)m_cboFrameType.SelectedValue)
            {
                case WIZ_M3SL_FRAME_TYPE.SL_INPUT_BLOC:
                    InOrOut = "IN";
                    break;
                case WIZ_M3SL_FRAME_TYPE.SL_OUTPUT_BLOC:
                    InOrOut = "OUT";
                    break;
                default:
                    System.Diagnostics.Debug.Assert(false);
                    break;
            }

            int indexSLBloc = 0;
            switch ((WIZ_M3SL_ADRESS_RANGE)m_cboAdressRange.SelectedValue)
            {
                case WIZ_M3SL_ADRESS_RANGE.ADDR_1_8:
                case WIZ_M3SL_ADRESS_RANGE.ADDR_25_32:
                    indexSLBloc = 1;
                    break;
                case WIZ_M3SL_ADRESS_RANGE.ADDR_9_16:
                case WIZ_M3SL_ADRESS_RANGE.ADDR_33_40:
                    indexSLBloc = 2;
                    break;
                case WIZ_M3SL_ADRESS_RANGE.ADDR_17_24:
                case WIZ_M3SL_ADRESS_RANGE.ADDR_41_48:
                    indexSLBloc = 3;
                    break;
                default:
                    System.Diagnostics.Debug.Assert(false, "adresse de registre invalide");
                    break;
            }
            if (bResp)
                return string.Format(RespFormatString, ReadOrWrite, InOrOut, indexSLBloc);
            else
                return string.Format(FormatString, ReadOrWrite, InOrOut, indexSLBloc);

        }
        #endregion

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
        private void UpdateSplitJoinButtons()
        {
            if (m_ListViewDatas.SelectedItems.Count == 0
                ||m_ListViewDatas.SelectedItems.Count > 2
                ||m_ListViewDatas.Enabled == false)
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
                if ((lviData1.Index == lviData2.Index + 1 || lviData1.Index+1 == lviData2.Index)
                    &&lviData1.SubItems[2].Text == lviData2.SubItems[2].Text)
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
            WIZ_M3SL_FRAME_TYPE frType = (WIZ_M3SL_FRAME_TYPE) m_cboFrameType.SelectedValue;
            WIZ_M3SL_ADRESS_RANGE AddrRange = (WIZ_M3SL_ADRESS_RANGE) m_cboAdressRange.SelectedValue;

            if (m_ListViewDatas.SelectedItems.Count == 1)
            {
                Data dt = (Data)m_ListViewDatas.SelectedItems[0].Tag;
                if (dt.SizeInBits > 1 && ((dt.SizeInBits %2) ==0)) // la taille de la donnée doit être paire
                {
                    int iCurUserDataBit = 0;
                    for (int indexSplit = 0; indexSplit < m_ListUserData.Count; indexSplit++)
                    {
                        Data uData = (Data)m_ListUserData[indexSplit];
                        if (uData.Symbol == dt.Symbol)
                        {
                            int newDataSize = dt.SizeInBits /2;
                            string strSymb1 = GetDefaultSymbolM3Data(frType, AddrRange, iCurUserDataBit, newDataSize);
                            string strSymb2 = GetDefaultSymbolM3Data(frType, AddrRange, iCurUserDataBit+newDataSize, newDataSize);
                            Data newData1 = new Data(strSymb1, 0, newDataSize, false);  
                            Data newData2 = new Data(strSymb2, 0, newDataSize, false);
                            m_ListUserData[indexSplit] = newData2;
                            m_ListUserData.Insert(indexSplit, newData1);
                            this.InitListViewData();
                            break;
                        }
                        iCurUserDataBit += uData.SizeInBits;
                    }
                }
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void m_btnJoin_Click(object sender, EventArgs e)
        {
            WIZ_M3SL_FRAME_TYPE frType = (WIZ_M3SL_FRAME_TYPE)m_cboFrameType.SelectedValue;
            WIZ_M3SL_ADRESS_RANGE AddrRange = (WIZ_M3SL_ADRESS_RANGE)m_cboAdressRange.SelectedValue;

            if (m_ListViewDatas.SelectedItems.Count == 2)
            {
                Data dt1 = (Data)m_ListViewDatas.SelectedItems[0].Tag;
                Data dt2 = (Data)m_ListViewDatas.SelectedItems[1].Tag;
                if (dt1.SizeInBits == dt2.SizeInBits && ((dt1.SizeInBits + dt2.SizeInBits)%2 == 0)) // la taille totale de la donnée doit être paire
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
                            string strSymb1 = GetDefaultSymbolM3Data(frType, AddrRange, iCurUserDataBit, newDataSize);
                            Data newData1 = new Data(strSymb1, 0, newDataSize, false);
                            m_ListUserData[indexJoin] = newData1;
                            m_ListUserData.RemoveAt(indexJoin + 1);
                            this.InitListViewData();
                            break;
                        }
                        iCurUserDataBit += uData.SizeInBits;
                    }
                }
            }
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
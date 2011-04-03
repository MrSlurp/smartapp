using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using CommonLib;


namespace SmartApp.Wizards
{
    public static partial class WizardFrameGenerator
    {
        #region fonction specifiques au TCP Modbus
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        static public ArrayList GenerateTCPMBWriteFrameDatas(string FrameSymbol, 
                                                             MODBUS_ORDER_TYPE WriteOrder,  
                                                             int StartAddress, 
                                                             int NbOfRegisters,
                                                             ArrayList ListUserDatas)
        {
            ArrayList FrameDataList = new ArrayList();

            FrameDataList.Add(new Data("TRANSACTION_ID", 1, 16, true));
            FrameDataList.Add(new Data("PROTOCO_ID", 0, 16, true));
            string strFollowingByte = string.Empty;
            int ByteCount = 0;
            if (WriteOrder == MODBUS_ORDER_TYPE.WRITE_MULTIPLE_REGISTER)
            {
                ByteCount = NbOfRegisters * 2 + 7;
            }
            else if (WriteOrder == MODBUS_ORDER_TYPE.WRITE_SINGLE_REGISTER)
            {
                ByteCount = NbOfRegisters * 2 + 4;
            }
            else
                System.Diagnostics.Debug.Assert(false);

            strFollowingByte = string.Format("FOLLOWING_{0}_BYTES", ByteCount);

            FrameDataList.Add(new Data(strFollowingByte, ByteCount, 16, true));
            FrameDataList.Add(new Data("MODBUS_SLAVE_ADDR", 1, 8, true));
            if (WriteOrder == MODBUS_ORDER_TYPE.WRITE_MULTIPLE_REGISTER)
            {
                FrameDataList.Add(new Data("MODBUS_WRITE_MR", 16, 8, true));
            }
            else if (WriteOrder == MODBUS_ORDER_TYPE.WRITE_SINGLE_REGISTER)
            {
                FrameDataList.Add(new Data("MODBUS_WRITE_SR", 6, 8, true));
            }
            else
                System.Diagnostics.Debug.Assert(false);


            string strAddrReg = string.Format("TCPMB_REG_ADDR_{0}", StartAddress);
            FrameDataList.Add(new Data(strAddrReg, (int)StartAddress, 16, true));

            if (WriteOrder == MODBUS_ORDER_TYPE.WRITE_MULTIPLE_REGISTER)
            {
                string strNbRegByte = string.Format("TCPMB_WRITE_{0}_REG", NbOfRegisters);
                FrameDataList.Add(new Data(strNbRegByte, NbOfRegisters, 16, true));
                strNbRegByte = string.Format("BYTE_COUNT_{0}_REG", NbOfRegisters*2);
                FrameDataList.Add(new Data(strNbRegByte, NbOfRegisters*2, 8, true));
            }

            for (int i = 0; i < ListUserDatas.Count; i++)
            {
                FrameDataList.Add(ListUserDatas[i]);
            }

            return FrameDataList;
        }
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        static public ArrayList GenerateTCPMBRespWriteMRFrameDatas(string FrameSymbol,
                                                             MODBUS_ORDER_TYPE WriteOrder,
                                                             int StartAddress,
                                                             int NbOfRegisters,
                                                             ArrayList ListUserDatas)
        {
            ArrayList FrameDataList = new ArrayList();

            FrameDataList.Add(new Data("TRANSACTION_ID", 1, 16, true));
            FrameDataList.Add(new Data("PROTOCO_ID", 0, 16, true));
            string strFollowingByte = string.Format("FOLLOWING_{0}_BYTES", 6);
            FrameDataList.Add(new Data(strFollowingByte, 6, 16, true));
/*
            string strFollowingByte = string.Format("FOLLOWING_{0}_BYTES", NbOfRegisters * 2 + 4);
            FrameDataList.Add(new Data(strFollowingByte, NbOfRegisters * 2 + 4, 16, true));
*/
            FrameDataList.Add(new Data("MODBUS_SLAVE_ADDR", 1, 8, true));
            if (WriteOrder == MODBUS_ORDER_TYPE.WRITE_MULTIPLE_REGISTER)
            {
                FrameDataList.Add(new Data("MODBUS_WRITE_MR", 16, 8, true));
            }
            else
                System.Diagnostics.Debug.Assert(false);


            string strAddrReg = string.Format("TCPMB_REG_ADDR_{0}", StartAddress);
            FrameDataList.Add(new Data(strAddrReg, (int)StartAddress, 16, true));

            if (WriteOrder == MODBUS_ORDER_TYPE.WRITE_MULTIPLE_REGISTER)
            {
              string strRegCount = string.Format("TCPMB_WRITE_{0}_REG", NbOfRegisters);
              FrameDataList.Add(new Data(strRegCount, (int)NbOfRegisters, 16, true));
          }

            return FrameDataList;
        }
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        static public ArrayList GenerateTCPMBReqReadFrameDatas(string FrameSymbol, 
                                                               MODBUS_ORDER_TYPE WriteOrder, 
                                                               int StartAddress, 
                                                               int NbOfRegisters, 
                                                               ArrayList ListUserDatas)
        {
            ArrayList FrameDataList = new ArrayList();
            FrameDataList.Add(new Data("TRANSACTION_ID", 1, 16, true));
            FrameDataList.Add(new Data("PROTOCO_ID", 0, 16, true));
            string strFollowingByte = string.Format("FOLLOWING_{0}_BYTES", 6);
            FrameDataList.Add(new Data(strFollowingByte, 6, 16, true));
            FrameDataList.Add(new Data("MODBUS_SLAVE_ADDR", 1, 8, true));
            FrameDataList.Add(new Data("MODBUS_READ_REG", 3, 8, true));
            string strAddrReg = string.Format("TCPMB_REG_ADDR_{0}", StartAddress);
            FrameDataList.Add(new Data(strAddrReg, (int)StartAddress, 16, true));
            string strNbRegByte = string.Format("BYTE_READ_{0}_REG", NbOfRegisters);
            FrameDataList.Add(new Data(strNbRegByte, NbOfRegisters, 16, true));

            return FrameDataList;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        static public ArrayList GenerateTCPMBRespReqReadFrameDatas(string FrameSymbol, 
                                                                   MODBUS_ORDER_TYPE WriteOrder, 
                                                                   int StartAddress, 
                                                                   int NbOfRegisters, 
                                                                   ArrayList ListUserDatas)
        {
            ArrayList FrameDataList = new ArrayList();
            FrameDataList.Add(new Data("TRANSACTION_ID", 1, 16, true));
            FrameDataList.Add(new Data("PROTOCO_ID", 0, 16, true));
            string strFollowingByte = string.Format("FOLLOWING_{0}_BYTES", NbOfRegisters*2 + 3);
            FrameDataList.Add(new Data(strFollowingByte, NbOfRegisters * 2 + 3, 16, true));
            FrameDataList.Add(new Data("MODBUS_SLAVE_ADDR", 1, 8, true));
            FrameDataList.Add(new Data("MODBUS_READ_REG", 3, 8, true));
            string strNbRegByte = string.Format("READ_RECIEVE_{0}_BYTES", NbOfRegisters*2);
            FrameDataList.Add(new Data(strNbRegByte, NbOfRegisters*2, 8, true));
            for (int i = 0; i < ListUserDatas.Count; i++)
            {
                FrameDataList.Add(ListUserDatas[i]);
            }

            return FrameDataList;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        static public Trame CreateTCPMBFrameObject(string FrameSymbol, ArrayList ListFrameDatas)
        {
            Trame tr = new Trame();
            tr.Symbol = FrameSymbol;
            tr.Description = Program.LangSys.C("Auto generated TCP Modbus frame");
            tr.ConvType = CONVERT_TYPE.NONE.ToString();
            tr.ConvFrom = 0;
            tr.ConvTo = 0; 
            tr.CtrlDataType = CTRLDATA_TYPE.NONE.ToString();
            tr.CtrlDataSize = (int)DATA_SIZE.DATA_SIZE_8B;
            tr.CtrlDataFrom = 0;
            tr.CtrlDataTo = 0;
            return tr;
        }
        #endregion

    }
}

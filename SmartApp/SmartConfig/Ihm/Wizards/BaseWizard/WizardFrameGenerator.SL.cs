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
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using CommonLib;


namespace SmartApp.Wizards
{

    public static partial class WizardFrameGenerator
    {
        #region fonction specifiques aux blox SL M3
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        static public ArrayList GenerateM3WriteSLFrameDatas(string FrameSymbol, WIZ_SL_ADRESS_RANGE addrRange, ArrayList ListUserDatas)
        {
            ArrayList FrameDataList = new ArrayList();
            FrameDataList.Add(new Data("ASCII_COMMA", 58, 8, true));
            FrameDataList.Add(new Data("M3_SLAVE_ADDR", 4, 8, true));
            FrameDataList.Add(new Data("M3_MODBUS_WRITE_MR", 16, 8, true));
            FrameDataList.Add(new Data("M3_DEST_ADDR_PART1", 0, 16, true));
            FrameDataList.Add(new Data("M3_DEST_ADDR_PART2", 255, 8, true));
            string strAddrReg = GetM3AdressRegisterSymbol(addrRange);
            FrameDataList.Add(new Data(strAddrReg, (int)addrRange, 8, true));
            FrameDataList.Add(new Data("SIZE_WRITE_8_REG", 16, 8, true));
            for (int i = 0; i < ListUserDatas.Count; i++)
            {
                FrameDataList.Add(ListUserDatas[i]);
            }
            string strCtrlDataName = FrameSymbol + Cste.STR_SUFFIX_CTRLDATA;
            FrameDataList.Add(new Data(strCtrlDataName, 0, 8, false));
            FrameDataList.Add(new Data("M3_END_OF_FRAME1", 13, 8, true));
            FrameDataList.Add(new Data("M3_END_OF_FRAME2", 10, 8, true));

            return FrameDataList;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        static public ArrayList GenerateM3RespWriteSLFrameDatas(string FrameSymbol, WIZ_SL_ADRESS_RANGE addrRange, ArrayList ListUserDatas)
        {
            ArrayList FrameDataList = new ArrayList();
            FrameDataList.Add(new Data("ASCII_COMMA", 58, 8, true));
            FrameDataList.Add(new Data("M3_SLAVE_ADDR", 4, 8, true));
            FrameDataList.Add(new Data("M3_MODBUS_WRITE_MR", 16, 8, true));
            FrameDataList.Add(new Data("M3_DEST_ADDR_PART1", 0, 16, true));
            FrameDataList.Add(new Data("M3_DEST_ADDR_PART2", 255, 8, true));
            string strAddrReg = GetM3AdressRegisterSymbol(addrRange);
            FrameDataList.Add(new Data(strAddrReg, (int)addrRange, 8, true));
            FrameDataList.Add(new Data("SIZE_WRITE_8_REG", 16, 8, true));
            string strCtrlDataName = FrameSymbol + Cste.STR_SUFFIX_CTRLDATA;
            FrameDataList.Add(new Data(strCtrlDataName, 0, 8, false));
            FrameDataList.Add(new Data("M3_END_OF_FRAME1", 13, 8, true));
            FrameDataList.Add(new Data("M3_END_OF_FRAME2", 10, 8, true));

            return FrameDataList;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        static public ArrayList GenerateM3ReqReadSLFrameDatas(string FrameSymbol, WIZ_SL_ADRESS_RANGE addrRange)
        {
            ArrayList FrameDataList = new ArrayList();
            FrameDataList.Add(new Data("ASCII_COMMA", 58, 8, true));
            FrameDataList.Add(new Data("M3_SLAVE_ADDR", 4, 8, true));
            FrameDataList.Add(new Data("M3_MODBUS_READ", 3, 8, true));
            FrameDataList.Add(new Data("M3_DEST_ADDR_PART1", 0, 16, true));
            FrameDataList.Add(new Data("M3_DEST_ADDR_PART2", 255, 8, true));
            string strAddrReg = GetM3AdressRegisterSymbol(addrRange);
            FrameDataList.Add(new Data(strAddrReg, (int)addrRange, 8, true));
            FrameDataList.Add(new Data("SIZE_WRITE_8_REG", 16, 8, true));
            string strCtrlDataName = FrameSymbol + Cste.STR_SUFFIX_CTRLDATA;
            FrameDataList.Add(new Data(strCtrlDataName, 0, 8, false));
            FrameDataList.Add(new Data("M3_END_OF_FRAME1", 13, 8, true));
            FrameDataList.Add(new Data("M3_END_OF_FRAME2", 10, 8, true));

            return FrameDataList;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        static public ArrayList GenerateM3RespReqReadSLFrameDatas(string FrameSymbol, WIZ_SL_ADRESS_RANGE addrRange, ArrayList ListUserDatas)
        {
            ArrayList FrameDataList = new ArrayList();
            FrameDataList.Add(new Data("ASCII_COMMA", 58, 8, true));
            FrameDataList.Add(new Data("M3_SLAVE_ADDR", 4, 8, true));
            FrameDataList.Add(new Data("M3_MODBUS_READ", 3, 8, true));
            FrameDataList.Add(new Data("SIZE_WRITE_8_REG", 16, 8, true));
            for (int i = 0; i < ListUserDatas.Count; i++)
            {
                FrameDataList.Add(ListUserDatas[i]);
            }
            string strCtrlDataName = FrameSymbol + Cste.STR_SUFFIX_CTRLDATA;
            FrameDataList.Add(new Data(strCtrlDataName, 0, 8, false));
            FrameDataList.Add(new Data("M3_END_OF_FRAME1", 13, 8, true));
            FrameDataList.Add(new Data("M3_END_OF_FRAME2", 10, 8, true));

            return FrameDataList;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        static private string GetM3AdressRegisterSymbol(WIZ_SL_ADRESS_RANGE addrRange)
        {
            string strRet = "";
            switch (addrRange)
            {
                case WIZ_SL_ADRESS_RANGE.ADDR_1_8:
                    strRet = "M3_REGISTER_0_ADDR";
                    break;
                case WIZ_SL_ADRESS_RANGE.ADDR_9_16:
                    strRet = "M3_REGISTER_8_ADDR";
                    break;
                case WIZ_SL_ADRESS_RANGE.ADDR_17_24:
                    strRet = "M3_REGISTER_16_ADDR";
                    break;
                case WIZ_SL_ADRESS_RANGE.ADDR_25_32:
                    strRet = "M3_REGISTER_24_ADDR";
                    break;
                case WIZ_SL_ADRESS_RANGE.ADDR_33_40:
                    strRet = "M3_REGISTER_32_ADDR";
                    break;
                case WIZ_SL_ADRESS_RANGE.ADDR_41_48:
                    strRet = "M3_REGISTER_40_ADDR";
                    break;
                default:
                    System.Diagnostics.Debug.Assert(false, "adresse de registre invalide");
                    break;
            }
            return strRet;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        static public Trame CreateM3FrameObject(string FrameSymbol, ArrayList ListFrameDatas)
        {
            Trame tr = new Trame();
            tr.Symbol = FrameSymbol;
            tr.Description = Program.LangSys.C("Auto generated M3 frame");
            // dans le cas d'un trame M3:
            // il y a conversion ASCII
            // - elle commence a la seconde donnée
            // - elle se termine a la donnée n-2 (avant \r \n)
            tr.ConvType = CONVERT_TYPE.ASCII.ToString();
            tr.ConvFrom = 1; //!\ index basé a 0
            tr.ConvTo = ListFrameDatas.Count - 3; //!\ index basé a 0
            // le type de donné de control est le checksum M3
            // Somme complémentée plus 1
            // - le calcule commence a la seconde donnée
            // - et se termine avant la donnée de control
            // - la donnée de control se trouve toujours juste avant les 2 caractères de fin de trame
            tr.CtrlDataType = CTRLDATA_TYPE.SUM_COMPL_P1.ToString();
            tr.CtrlDataSize = (int)DATA_SIZE.DATA_SIZE_8B;
            tr.CtrlDataFrom = 1; //!\ index basé a 0
            tr.CtrlDataTo = ListFrameDatas.Count - 4;//!\ index basé a 0
            return tr;
        }
        #endregion

        #region fonction specifiques aux blox SL Z2
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        static public ArrayList GenerateZ2WriteSLFrameDatas(string FrameSymbol, WIZ_SL_ADRESS_RANGE addrRange, ArrayList ListUserDatas)
        {
            ArrayList FrameDataList = new ArrayList();
            FrameDataList.Add(new Data("ASCII_COMMA", 58, 8, true));
            FrameDataList.Add(new Data("Z2_SLAVE_ADDR", 1, 8, true));
            FrameDataList.Add(new Data("Z2_MODBUS_WRITE_MR", 16, 8, true));
            FrameDataList.Add(new Data("Z2_DEST_ADDR_PART1", 0, 16, true));
            FrameDataList.Add(new Data("Z2_DEST_ADDR_PART2", 255, 8, true));
            string strAddrReg = GetM3AdressRegisterSymbol(addrRange);
            FrameDataList.Add(new Data(strAddrReg, (int)addrRange, 8, true));
            FrameDataList.Add(new Data("SIZE_WRITE_8_REG", 16, 8, true));
            for (int i = 0; i < ListUserDatas.Count; i++)
            {
                FrameDataList.Add(ListUserDatas[i]);
            }
            string strCtrlDataName = FrameSymbol + Cste.STR_SUFFIX_CTRLDATA;
            FrameDataList.Add(new Data(strCtrlDataName, 0, 8, false));
            FrameDataList.Add(new Data("Z2_END_OF_FRAME1", 13, 8, true));
            FrameDataList.Add(new Data("Z2_END_OF_FRAME2", 10, 8, true));

            return FrameDataList;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        static public ArrayList GenerateZ2RespWriteSLFrameDatas(string FrameSymbol, WIZ_SL_ADRESS_RANGE addrRange, ArrayList ListUserDatas)
        {
            ArrayList FrameDataList = new ArrayList();
            FrameDataList.Add(new Data("ASCII_COMMA", 58, 8, true));
            FrameDataList.Add(new Data("Z2_SLAVE_ADDR", 1, 8, true));
            FrameDataList.Add(new Data("Z2_MODBUS_WRITE_MR", 16, 8, true));
            FrameDataList.Add(new Data("Z2_DEST_ADDR_PART1", 0, 16, true));
            FrameDataList.Add(new Data("Z2_DEST_ADDR_PART2", 255, 8, true));
            string strAddrReg = GetZ2AdressRegisterSymbol(addrRange);
            FrameDataList.Add(new Data(strAddrReg, (int)addrRange, 8, true));
            FrameDataList.Add(new Data("SIZE_WRITE_8_REG", 16, 8, true));
            string strCtrlDataName = FrameSymbol + Cste.STR_SUFFIX_CTRLDATA;
            FrameDataList.Add(new Data(strCtrlDataName, 0, 8, false));
            FrameDataList.Add(new Data("Z2_END_OF_FRAME1", 13, 8, true));
            FrameDataList.Add(new Data("Z2_END_OF_FRAME2", 10, 8, true));

            return FrameDataList;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        static public ArrayList GenerateZ2ReqReadSLFrameDatas(string FrameSymbol, WIZ_SL_ADRESS_RANGE addrRange)
        {
            ArrayList FrameDataList = new ArrayList();
            FrameDataList.Add(new Data("ASCII_COMMA", 58, 8, true));
            FrameDataList.Add(new Data("Z2_SLAVE_ADDR", 1, 8, true));
            FrameDataList.Add(new Data("Z2_MODBUS_READ", 3, 8, true));
            FrameDataList.Add(new Data("Z2_DEST_ADDR_PART1", 0, 16, true));
            FrameDataList.Add(new Data("Z2_DEST_ADDR_PART2", 255, 8, true));
            string strAddrReg = GetM3AdressRegisterSymbol(addrRange);
            FrameDataList.Add(new Data(strAddrReg, (int)addrRange, 8, true));
            FrameDataList.Add(new Data("SIZE_WRITE_8_REG", 16, 8, true));
            string strCtrlDataName = FrameSymbol + Cste.STR_SUFFIX_CTRLDATA;
            FrameDataList.Add(new Data(strCtrlDataName, 0, 8, false));
            FrameDataList.Add(new Data("Z2_END_OF_FRAME1", 13, 8, true));
            FrameDataList.Add(new Data("Z2_END_OF_FRAME2", 10, 8, true));

            return FrameDataList;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        static public ArrayList GenerateZ2RespReqReadSLFrameDatas(string FrameSymbol, WIZ_SL_ADRESS_RANGE addrRange, ArrayList ListUserDatas)
        {
            ArrayList FrameDataList = new ArrayList();
            FrameDataList.Add(new Data("ASCII_COMMA", 58, 8, true));
            FrameDataList.Add(new Data("Z2_SLAVE_ADDR", 1, 8, true));
            FrameDataList.Add(new Data("Z2_MODBUS_READ", 3, 8, true));
            FrameDataList.Add(new Data("SIZE_WRITE_8_REG", 16, 8, true));
            for (int i = 0; i < ListUserDatas.Count; i++)
            {
                FrameDataList.Add(ListUserDatas[i]);
            }
            string strCtrlDataName = FrameSymbol + Cste.STR_SUFFIX_CTRLDATA;
            FrameDataList.Add(new Data(strCtrlDataName, 0, 8, false));
            FrameDataList.Add(new Data("Z2_END_OF_FRAME1", 13, 8, true));
            FrameDataList.Add(new Data("Z2_END_OF_FRAME2", 10, 8, true));

            return FrameDataList;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        static private string GetZ2AdressRegisterSymbol(WIZ_SL_ADRESS_RANGE addrRange)
        {
            string strRet = "";
            switch (addrRange)
            {
                case WIZ_SL_ADRESS_RANGE.ADDR_1_8:
                    strRet = "Z2_REGISTER_0_ADDR";
                    break;
                case WIZ_SL_ADRESS_RANGE.ADDR_9_16:
                    strRet = "Z2_REGISTER_8_ADDR";
                    break;
                case WIZ_SL_ADRESS_RANGE.ADDR_17_24:
                    strRet = "Z2_REGISTER_16_ADDR";
                    break;
                case WIZ_SL_ADRESS_RANGE.ADDR_25_32:
                    strRet = "Z2_REGISTER_24_ADDR";
                    break;
                case WIZ_SL_ADRESS_RANGE.ADDR_33_40:
                    strRet = "Z2_REGISTER_32_ADDR";
                    break;
                case WIZ_SL_ADRESS_RANGE.ADDR_41_48:
                    strRet = "Z2_REGISTER_40_ADDR";
                    break;
                default:
                    System.Diagnostics.Debug.Assert(false, "adresse de registre invalide");
                    break;
            }
            return strRet;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        static public Trame CreateZ2FrameObject(string FrameSymbol, ArrayList ListFrameDatas)
        {
            Trame tr = new Trame();
            tr.Symbol = FrameSymbol;
            tr.Description = Program.LangSys.C("Auto generated Z2 frame");
            // dans le cas d'un trame M3:
            // il y a conversion ASCII
            // - elle commence a la seconde donnée
            // - elle se termine a la donnée n-2 (avant \r \n)
            tr.ConvType = CONVERT_TYPE.ASCII.ToString();
            tr.ConvFrom = 1; //!\ index basé a 0
            tr.ConvTo = ListFrameDatas.Count - 3; //!\ index basé a 0
            // le type de donné de control est le checksum M3
            // Somme complémentée plus 1
            // - le calcule commence a la seconde donnée
            // - et se termine avant la donnée de control
            // - la donnée de control se trouve toujours juste avant les 2 caractères de fin de trame
            tr.CtrlDataType = CTRLDATA_TYPE.SUM_COMPL_P2.ToString();
            tr.CtrlDataSize = (int)DATA_SIZE.DATA_SIZE_8B;
            tr.CtrlDataFrom = 1; //!\ index basé a 0
            tr.CtrlDataTo = ListFrameDatas.Count - 4;//!\ index basé a 0
            return tr;
        }
        #endregion

    }
}

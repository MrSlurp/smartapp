using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using CommonLib;

namespace SmartApp.Wizards
{
    /// <summary>
    /// 
    /// </summary>
    public class ETHM3ProjectCreator : BaseM3Z2ProjectCreator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Document"></param>
        /// <param name="WizConfig"></param>
        public ETHM3ProjectCreator(BTDoc Document):
            base(Document)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public override void CreateProjectFromWizConfig()
        {
            StringCollection EthInReadFrameName = new StringCollection();
            StringCollection EthInWriteFrameName = new StringCollection();
            StringCollection EthOutReadFrameName = new StringCollection();
            BlocConfig[] InBlocs = m_WizConfig.GetBlocListByType(BlocsType.IN);
            BlocConfig[] OutBlocs = m_WizConfig.GetBlocListByType(BlocsType.OUT);
            // dans tous les cas, on fait des trames qui vont lire
            // les registres configurés et qui sont consécutifs
            CreateMBFramesForBlocs(InBlocs, false, ref EthInReadFrameName);
            CreateMBFramesForBlocs(InBlocs, true, ref EthInWriteFrameName);
            CreateMBFramesForBlocs(OutBlocs, false, ref EthOutReadFrameName);
            string readETHInFunc = CreateFrameFunction("READ_ETH_IN", EthInReadFrameName);
            CreateFrameFunction("WRITE_ETH_IN", EthInWriteFrameName);
            string readSLOutFunc = CreateFrameFunction("READ_ETH_OUT", EthOutReadFrameName);
            CreateReadSLOutTimer("REFRESH_OUT", readSLOutFunc);
            CreateDefaultScreen("MAIN_SCREEN", readETHInFunc);
        }

        private void CreateMBFramesForBlocs(BlocConfig[] blocConfig, bool bWrite, ref StringCollection FrameNames)
        {
            List<List<BlocConfig>> TabConsecBloc = new List<List<BlocConfig>>();
            List<BlocConfig> tmpList = new List<BlocConfig>();
            for (int i = 0; i < blocConfig.Length; i++)
            {
                if (!blocConfig[i].IsUsed)
                {
                    if (tmpList.Count != 0)
                        TabConsecBloc.Add(tmpList);

                    tmpList = new List<BlocConfig>();
                }
                else
                {
                    tmpList.Add(blocConfig[i]);
                }
            }
            TabConsecBloc.Add(tmpList);

            for (int iConsecBloc = 0; iConsecBloc < TabConsecBloc.Count; iConsecBloc++)
            {
                List<BlocConfig> CurList = TabConsecBloc[iConsecBloc];
                M3XN05BlocConfig FirstBloc = CurList[0] as M3XN05BlocConfig;
                M3XN05BlocConfig LasttBloc = CurList[CurList.Count - 1] as M3XN05BlocConfig;
                string FrameBaseName = "XN05";
                string FrameName = FrameBaseName + (bWrite? "_WRITE" : "_READ");
                FrameName += string.Format("{0}REG_FROM_{1}", CurList.Count, FirstBloc.Name.Replace(' ', '_').ToUpper());
                string retFrameName = FrameName + "_RET";
                FrameNames.Add(FrameName);
                FrameNames.Add(retFrameName);

                ArrayList UserDataList = new ArrayList();
                for (int iBloc = 0; iBloc < CurList.Count; iBloc++)
                {
                    BlocConfig bloc = CurList[iBloc];
                    for (int iData = bloc.ListIO[0].ListSymbol.Count -1 ; iData>= 0 ; iData--)
                    {
                        UserDataList.Add(new Data(bloc.ListIO[0].ListSymbol[iData], 0, 16 / bloc.ListIO[0].ListSymbol.Count, false));
                    }
                }
                MODBUS_ORDER_TYPE mbOrder = MODBUS_ORDER_TYPE.READ_HOLDING_REGISTER;
                if (bWrite)
                    mbOrder = CurList.Count > 1 ? MODBUS_ORDER_TYPE.WRITE_MULTIPLE_REGISTER
                                                 : MODBUS_ORDER_TYPE.WRITE_SINGLE_REGISTER;

                ArrayList Frame1Data;
                ArrayList Frame2Data;
                Trame tr1, tr2;

                if (!bWrite)
                {
                    Frame1Data = WizardFrameGenerator.GenerateTCPMBReqReadFrameDatas(FrameName, mbOrder, FirstBloc.MbRegAddr, FirstBloc.MbRegAddr - LasttBloc.MbRegAddr, UserDataList);
                    Frame2Data = WizardFrameGenerator.GenerateTCPMBRespReqReadFrameDatas(retFrameName, mbOrder, FirstBloc.MbRegAddr, FirstBloc.MbRegAddr - LasttBloc.MbRegAddr, UserDataList);
                }
                else
                {
                    if (mbOrder == MODBUS_ORDER_TYPE.WRITE_MULTIPLE_REGISTER)
                    {
                        Frame1Data = WizardFrameGenerator.GenerateTCPMBWriteFrameDatas(FrameName, mbOrder, FirstBloc.MbRegAddr, FirstBloc.MbRegAddr - LasttBloc.MbRegAddr, UserDataList);
                        Frame2Data = WizardFrameGenerator.GenerateTCPMBRespWriteMRFrameDatas(retFrameName, mbOrder, FirstBloc.MbRegAddr, FirstBloc.MbRegAddr - LasttBloc.MbRegAddr, UserDataList);
                    }
                    else
                    {
                        Frame1Data = WizardFrameGenerator.GenerateTCPMBWriteFrameDatas(FrameName, mbOrder, FirstBloc.MbRegAddr, FirstBloc.MbRegAddr - LasttBloc.MbRegAddr, UserDataList);
                        Frame2Data = WizardFrameGenerator.GenerateTCPMBWriteFrameDatas(retFrameName, mbOrder, FirstBloc.MbRegAddr, FirstBloc.MbRegAddr - LasttBloc.MbRegAddr, UserDataList);
                    }
                }
                tr1 = WizardFrameGenerator.CreateTCPMBFrameObject(FrameName, Frame1Data);
                WizardFrameGenerator.InsertFrameInDoc(m_Document, tr1, Frame1Data);
                tr2 = WizardFrameGenerator.CreateTCPMBFrameObject(retFrameName, Frame2Data);
                WizardFrameGenerator.InsertFrameInDoc(m_Document, tr2, Frame2Data);
            }
        }
    }
}

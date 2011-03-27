using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using CommonLib;

namespace SmartApp.Wizards
{
    public class SLM3ProjectCreator : BaseM3Z2ProjectCreator
    {

        public SLM3ProjectCreator(BTDoc Document):
            base(Document)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public override void CreateProjectFromWizConfig()
        {
            StringCollection SLInReadFrameName = new StringCollection();
            StringCollection SLInWriteFrameName = new StringCollection();
            StringCollection SLOutReadFrameName = new StringCollection();
            BlocConfig[] InBlocs = m_WizConfig.GetBlocListByType(BlocsType.IN);
            for (int i = 0; i < InBlocs.Length; i++)
            {
                if (InBlocs[i].IsUsed)
                {
                    CreateSLFrames(InBlocs[i], false, ref SLInReadFrameName);
                    CreateSLFrames(InBlocs[i], true, ref SLInWriteFrameName);
                }
            }

            BlocConfig[] OutBlocs = m_WizConfig.GetBlocListByType(BlocsType.OUT);
            for (int i = 0; i < OutBlocs.Length; i++)
            {
                if (OutBlocs[i].IsUsed)
                    CreateSLFrames(OutBlocs[i], false, ref SLOutReadFrameName);
            }
            string readSLInFunc = CreateFrameFunction("READ_SL_IN", SLInReadFrameName);
            CreateFrameFunction("WRITE_SL_IN", SLInWriteFrameName);
            string readSLOutFunc = CreateFrameFunction("READ_SL_OUT", SLOutReadFrameName);
            CreateReadSLOutTimer("REFRESH_OUT", readSLOutFunc);
            CreateDefaultScreen("MAIN_SCREEN", readSLInFunc);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Bloc"></param>
        /// <param name="bWrite"></param>
        /// <param name="FrameNames"></param>
        private void CreateSLFrames(BlocConfig Bloc, bool bWrite, ref StringCollection FrameNames)
        {
            string slBlocName = Bloc.Name.Replace(' ', '_').ToUpper();
            string FrameName = slBlocName + (bWrite? "_WRITE" : "_READ");
            string retFrameName = FrameName + "_RET";
            FrameNames.Add(FrameName);
            FrameNames.Add(retFrameName);
            ArrayList UserDataList = new ArrayList();
            for (int i = 0; i < Bloc.ListIO.Length; i++)
            {
                for (int iData = Bloc.ListIO[i].ListSymbol.Count - 1; iData >= 0; iData--)
                {
                    UserDataList.Add(new Data(Bloc.ListIO[i].ListSymbol[iData], 0, 16 / Bloc.ListIO[i].ListSymbol.Count, false));
                }
            }

            WIZ_SL_ADRESS_RANGE addrRange = GetAddrRangeFromBloc(Bloc);
            ArrayList Frame1Datas;
            ArrayList Frame2Datas;
            if (bWrite)
                Frame1Datas = WizardFrameGenerator.GenerateM3WriteSLFrameDatas(FrameName, addrRange, UserDataList);
            else
                Frame1Datas = WizardFrameGenerator.GenerateM3ReqReadSLFrameDatas(FrameName, addrRange);

            Trame Frame1 = WizardFrameGenerator.CreateM3FrameObject(FrameName, Frame1Datas);
            WizardFrameGenerator.InsertFrameInDoc(m_Document, Frame1, Frame1Datas);

            if (bWrite)
                Frame2Datas = WizardFrameGenerator.GenerateM3RespWriteSLFrameDatas(retFrameName, addrRange, UserDataList);
            else
                Frame2Datas = WizardFrameGenerator.GenerateM3RespReqReadSLFrameDatas(retFrameName, addrRange, UserDataList);
            Trame Frame2 = WizardFrameGenerator.CreateM3FrameObject(retFrameName, Frame2Datas);
            WizardFrameGenerator.InsertFrameInDoc(m_Document, Frame2, Frame2Datas);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using CommonLib;

namespace SmartApp.Wizards
{
    public class SLM3ProjectCreator
    {
        WizardConfigData m_WizConfig;

        protected BTDoc m_Document;

        public SLM3ProjectCreator(BTDoc Document, WizardConfigData WizConfig)
        {
            m_Document = Document;
            m_WizConfig = WizConfig;
        }

        public void CreateProjectFromWizConfig()
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
            CreateFrameFunction("READ_SL_IN", SLInReadFrameName);
            CreateFrameFunction("WRITE_SL_IN", SLInWriteFrameName);
            CreateFrameFunction("READ_SL_OUT", SLOutReadFrameName);
        }

        private string CreateFrameFunction(string FuncName, StringCollection FrameNames)
        {
            Function func = new Function();
            func.Symbol = FuncName;
            string[] functionScripLines = new string[FrameNames.Count];
            for (int i = 0; i < FrameNames.Count; i++)
            {
                string FrameFuncToUse = ((i%2) != 0)? FRAME_FUNC.RECEIVE.ToString() : FRAME_FUNC.SEND.ToString();
                functionScripLines[i] = SCR_OBJECT.FRAMES.ToString() + "." +
                                        FrameNames[i] + "." +
                                        FrameFuncToUse + "()";
                                        
            }
            func.ScriptLines = functionScripLines;
            BaseObject obj = m_Document.GestFunction.GetFromSymbol(func.Symbol);
            if (obj == null)
            {
                m_Document.GestFunction.AddObj(func);
            }
            else
            {
                Function existingFunc = obj as Function;
                if (existingFunc.ScriptLines != func.ScriptLines)
                {
                    for (int indexFormat = 0; indexFormat < BaseGest.MAX_DEFAULT_ITEM_SYMBOL; indexFormat++)
                    {
                        //      - on crée une chaine temporaire = symbole + suffix de type "_AG{0}"
                        string strTempSymb = func.Symbol + string.Format("_AG{0}", indexFormat);
                        obj = m_Document.GestFunction.GetFromSymbol(func.Symbol);
                        if (obj == null)
                        {
                            func.Symbol = strTempSymb;
                            m_Document.GestFunction.AddObj(func);
                            break;
                        }
                    }
                }
                else
                {
                    // les fonctions sont identiques, rien a faire
                }
            }
            return func.Symbol;
        }

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

        private WIZ_SL_ADRESS_RANGE GetAddrRangeFromBloc(BlocConfig Bloc)
        {
            WIZ_SL_ADRESS_RANGE addrRange = WIZ_SL_ADRESS_RANGE.ADDR_1_8;
            switch (Bloc.Indice)
            {
                case 0:
                    addrRange = (Bloc.BlocType == BlocsType.IN)? WIZ_SL_ADRESS_RANGE.ADDR_1_8 : WIZ_SL_ADRESS_RANGE.ADDR_25_32;
                    break;
                case 1:
                    addrRange = (Bloc.BlocType == BlocsType.IN)? WIZ_SL_ADRESS_RANGE.ADDR_9_16 : WIZ_SL_ADRESS_RANGE.ADDR_33_40;
                    break;
                case 2:
                    addrRange = (Bloc.BlocType == BlocsType.IN) ? WIZ_SL_ADRESS_RANGE.ADDR_17_24 : WIZ_SL_ADRESS_RANGE.ADDR_41_48;
                    break;
                default:
                    System.Diagnostics.Debug.Assert(false);
                    break;
            }
            return addrRange;
        }

        private void CreateReadSLOutTimer()
        {

        }

        private void CreateDefaultScreen()
        {

        }

    }
}

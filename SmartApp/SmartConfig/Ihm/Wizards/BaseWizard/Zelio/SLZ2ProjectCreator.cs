using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using CommonLib;

namespace SmartApp.Wizards
{

    /// <summary>
    /// classe sp�cifique de g�n�ration de projet SL Z2
    /// </summary>
    public class SLZ2ProjectCreator : BaseM3Z2ProjectCreator
    {
        /// <summary>
        /// constructeur par d�faut
        /// </summary>
        /// <param name="Document"></param>
        public SLZ2ProjectCreator(BTDoc Document) :
            base(Document)
        {
        }

        /// <summary>
        /// fonction charg� de la cr�ation de tout les objets configur� dans le wizard
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
        /// cr�e les trames liaison s�rie en fonction de ce qui est utilis�.
        /// </summary>
        /// <param name="Bloc">bloc � lire/�crire</param>
        /// <param name="bWrite">indique si c'est pour une requ�te de lecture ou d'�criture</param>
        /// <param name="FrameNames">nom des trames cr�es</param>
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
                Frame1Datas = WizardFrameGenerator.GenerateZ2WriteSLFrameDatas(FrameName, addrRange, UserDataList);
            else
                Frame1Datas = WizardFrameGenerator.GenerateZ2ReqReadSLFrameDatas(FrameName, addrRange);

            Trame Frame1 = WizardFrameGenerator.CreateZ2FrameObject(FrameName, Frame1Datas);
            WizardFrameGenerator.InsertFrameInDoc(m_Document, Frame1, Frame1Datas);

            if (bWrite)
                Frame2Datas = WizardFrameGenerator.GenerateZ2RespWriteSLFrameDatas(retFrameName, addrRange, UserDataList);
            else
                Frame2Datas = WizardFrameGenerator.GenerateZ2RespReqReadSLFrameDatas(retFrameName, addrRange, UserDataList);
            Trame Frame2 = WizardFrameGenerator.CreateZ2FrameObject(retFrameName, Frame2Datas);
            WizardFrameGenerator.InsertFrameInDoc(m_Document, Frame2, Frame2Datas);
        }

        /// <summary>
        /// renvoie l'adresse des bloc Liaison s�rie en fonction du bloc
        /// </summary>
        /// <param name="Bloc">bloc pour lequel on souhaite r�cup�rer l'adresse</param>
        /// <returns>l'adresse SL</returns>
        protected WIZ_SL_ADRESS_RANGE GetAddrRangeFromBloc(BlocConfig Bloc)
        {
            WIZ_SL_ADRESS_RANGE addrRange = WIZ_SL_ADRESS_RANGE.ADDR_1_8;
            switch (Bloc.Indice)
            {
                case 1:
                    addrRange = (Bloc.BlocType == BlocsType.IN) ? WIZ_SL_ADRESS_RANGE.ADDR_1_8 : WIZ_SL_ADRESS_RANGE.ADDR_25_32;
                    break;
                case 2:
                    addrRange = (Bloc.BlocType == BlocsType.IN) ? WIZ_SL_ADRESS_RANGE.ADDR_9_16 : WIZ_SL_ADRESS_RANGE.ADDR_33_40;
                    break;
                case 3:
                    addrRange = (Bloc.BlocType == BlocsType.IN) ? WIZ_SL_ADRESS_RANGE.ADDR_17_24 : WIZ_SL_ADRESS_RANGE.ADDR_41_48;
                    break;
                default:
                    System.Diagnostics.Debug.Assert(false);
                    break;
            }
            return addrRange;
        }
    }
}

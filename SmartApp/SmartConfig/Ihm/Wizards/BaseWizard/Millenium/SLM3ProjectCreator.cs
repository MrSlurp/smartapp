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
using System.Collections.Specialized;
using System.Text;
using CommonLib;

namespace SmartApp.Wizards
{

    /// <summary>
    /// classe spécifique de génération de projet SL M3
    /// </summary>
    public class SLM3ProjectCreator : BaseM3Z2ProjectCreator
    {
        /// <summary>
        /// constructeur par défaut
        /// </summary>
        /// <param name="Document"></param>
        public SLM3ProjectCreator(BTDoc Document):
            base(Document)
        {
        }

        /// <summary>
        /// fonction chargé de la création de tout les objets configuré dans le wizard
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
        /// crée les trames liaison série en fonction de ce qui est utilisé.
        /// </summary>
        /// <param name="Bloc">bloc à lire/écrire</param>
        /// <param name="bWrite">indique si c'est pour une requête de lecture ou d'écriture</param>
        /// <param name="FrameNames">nom des trames crées</param>
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

        /// <summary>
        /// renvoie l'adresse des bloc Liaison série en fonction du bloc
        /// </summary>
        /// <param name="Bloc">bloc pour lequel on souhaite récupérer l'adresse</param>
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

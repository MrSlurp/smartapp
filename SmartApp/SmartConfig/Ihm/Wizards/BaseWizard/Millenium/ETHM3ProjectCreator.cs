/*
    This file is part of SmartApp.
    Copyright (C) 2007-2016  Pascal Bigot

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
    /// classe spécifique de génération de projet ETH M3
    /// </summary>
    public class ETHM3ProjectCreator : BaseM3Z2ProjectCreator
    {
        /// <summary>
        /// constructeur par défaut
        /// </summary>
        /// <param name="Document">document dans lequel créer les objets</param>
        public ETHM3ProjectCreator(BTDoc Document):
            base(Document)
        {

        }

        /// <summary>
        /// fonction chargé de la création de tout les objets configuré dans le wizard
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

        /// <summary>
        /// crée les trames TCP modbus en fonction de ce qui est utilisé.
        /// </summary>
        /// <param name="blocConfig">blocs à lire/écrire</param>
        /// <param name="bWrite">indique si c'est pour une requête de lecture ou d'écriture</param>
        /// <param name="FrameNames">nom des trames crées</param>
        private void CreateMBFramesForBlocs(BlocConfig[] blocConfig, bool bWrite, ref StringCollection FrameNames)
        {
            // pour le modbus on fait en sorte de gérer les blocs consécutifs (tables)
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

            // pour chaque table de bloc consécutifs
            for (int iConsecBloc = 0; iConsecBloc < TabConsecBloc.Count; iConsecBloc++)
            {
                List<BlocConfig> CurList = TabConsecBloc[iConsecBloc];
                if (CurList.Count == 0)
                    continue;
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
                    Frame1Data = WizardFrameGenerator.GenerateTCPMBReqReadFrameDatas(FrameName, mbOrder, FirstBloc.MbRegAddr, CurList.Count, UserDataList);
                    Frame2Data = WizardFrameGenerator.GenerateTCPMBRespReqReadFrameDatas(retFrameName, mbOrder, FirstBloc.MbRegAddr, CurList.Count, UserDataList);
                }
                else
                {
                    if (mbOrder == MODBUS_ORDER_TYPE.WRITE_MULTIPLE_REGISTER)
                    {
                        Frame1Data = WizardFrameGenerator.GenerateTCPMBWriteFrameDatas(FrameName, mbOrder, FirstBloc.MbRegAddr, CurList.Count, UserDataList);
                        Frame2Data = WizardFrameGenerator.GenerateTCPMBRespWriteMRFrameDatas(retFrameName, mbOrder, FirstBloc.MbRegAddr, CurList.Count, UserDataList);
                    }
                    else
                    {
                        Frame1Data = WizardFrameGenerator.GenerateTCPMBWriteFrameDatas(FrameName, mbOrder, FirstBloc.MbRegAddr, CurList.Count, UserDataList);
                        Frame2Data = WizardFrameGenerator.GenerateTCPMBWriteFrameDatas(retFrameName, mbOrder, FirstBloc.MbRegAddr, CurList.Count, UserDataList);
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

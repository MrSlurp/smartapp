using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using SmartApp.Datas;
using SmartApp.Gestionnaires;

namespace SmartApp.Ihm.Wizards
{
    public enum TCPMODBUS_REG_TYPE
    {
        INPUT_REGISTER,
        OUTPUT_REGISTER,
    }

    public enum MODBUS_ORDER_TYPE
    {
        WRITE_SINGLE_REGISTER = 0x06,
        READ_HOLDING_REGISTER = 0x03,
        WRITE_MULTIPLE_REGISTER =0x10,
    }

    public enum WIZ_M3SL_FRAME_TYPE
    {
        SL_INPUT_BLOC,
        SL_OUTPUT_BLOC,
    }

    public enum WIZ_M3SL_ORDER_TYPE
    {
        READ,
        WRITE,
    }

    public enum WIZ_M3SL_ADRESS_RANGE
    {
        // les valeurs correspondent aux adresse des bloc SL
        ADDR_1_8 = 0,
        ADDR_9_16 = 8,
        ADDR_17_24 = 16,
        ADDR_25_32 = 24,
        ADDR_33_40 = 32,
        ADDR_41_48 = 40,
    }


    public static class WizardFrameGenerator
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
            string strFollowingByte = string.Format("FOLLOWING_{0}_BYTES", NbOfRegisters*2 + 4);
            FrameDataList.Add(new Data(strFollowingByte, NbOfRegisters*2 + 4, 16, true));
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
                string strNbRegByte = string.Format("BYTE_WRITE_{0}_REG", NbOfRegisters);
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
            string strFollowingByte = string.Format("FOLLOWING_{0}_BYTES", NbOfRegisters * 2 + 4);
            FrameDataList.Add(new Data(strFollowingByte, NbOfRegisters * 2 + 4, 16, true));
            FrameDataList.Add(new Data("MODBUS_SLAVE_ADDR", 1, 8, true));
            if (WriteOrder == MODBUS_ORDER_TYPE.WRITE_MULTIPLE_REGISTER)
            {
                FrameDataList.Add(new Data("MODBUS_WRITE_MR", 16, 8, true));
            }
            else
                System.Diagnostics.Debug.Assert(false);


            string strAddrReg = string.Format("TCPMB_REG_ADDR_{0}", StartAddress);
            FrameDataList.Add(new Data(strAddrReg, (int)StartAddress, 16, true));
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
            tr.Description = "Auto generated TCP Modbus frame";
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

        #region fonction specifiques aux blox SL M3
                            

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        static public ArrayList GenerateM3WriteSLFrameDatas(string FrameSymbol, WIZ_M3SL_ADRESS_RANGE addrRange, ArrayList ListUserDatas)
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
        static public ArrayList GenerateM3RespWriteSLFrameDatas(string FrameSymbol, WIZ_M3SL_ADRESS_RANGE addrRange, ArrayList ListUserDatas)
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
        static public ArrayList GenerateM3ReqReadSLFrameDatas(string FrameSymbol, WIZ_M3SL_ADRESS_RANGE addrRange)
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
        static public ArrayList GenerateM3RespReqReadSLFrameDatas(string FrameSymbol, WIZ_M3SL_ADRESS_RANGE addrRange, ArrayList ListUserDatas)
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
        static private string GetM3AdressRegisterSymbol(WIZ_M3SL_ADRESS_RANGE addrRange)
        {
            string strRet = "";
            switch (addrRange)
            {
                case WIZ_M3SL_ADRESS_RANGE.ADDR_1_8:
                    strRet = "M3_REGISTER_0_ADDR";
                    break;
                case WIZ_M3SL_ADRESS_RANGE.ADDR_9_16:
                    strRet = "M3_REGISTER_8_ADDR";
                    break;
                case WIZ_M3SL_ADRESS_RANGE.ADDR_17_24:
                    strRet = "M3_REGISTER_16_ADDR";
                    break;
                case WIZ_M3SL_ADRESS_RANGE.ADDR_25_32:
                    strRet = "M3_REGISTER_24_ADDR";
                    break;
                case WIZ_M3SL_ADRESS_RANGE.ADDR_33_40:
                    strRet = "M3_REGISTER_32_ADDR";
                    break;
                case WIZ_M3SL_ADRESS_RANGE.ADDR_41_48:
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
            tr.Description = "Auto generated M3 frame";
            // dans le cas d'un trame M3:
            // il y a conversion ASCII
            // - elle commence a la seconde donn�e
            // - elle se termine a la donn�e n-2 (avant \r \n)
            tr.ConvType = CONVERT_TYPE.ASCII.ToString();
            tr.ConvFrom = 1; //!\ index bas� a 0
            tr.ConvTo = ListFrameDatas.Count - 3; //!\ index bas� a 0
            // le type de donn� de control est le checksum M3
            // Somme compl�ment�e plus 1
            // - le calcule commence a la seconde donn�e
            // - et se termine avant la donn�e de control
            // - la donn�e de control se trouve toujours juste avant les 2 caract�res de fin de trame
            tr.CtrlDataType = CTRLDATA_TYPE.SUM_COMPL_P1.ToString();
            tr.CtrlDataSize = (int)DATA_SIZE.DATA_SIZE_8B;
            tr.CtrlDataFrom = 1; //!\ index bas� a 0
            tr.CtrlDataTo = ListFrameDatas.Count - 4;//!\ index bas� a 0
            return tr;
        }
        #endregion

        //*****************************************************************************************************
        // Description: cette fonction effectue l'insertion d''une trame dans le document
        // la trame doit �tre configur� avec les bon param�tres et sa liste des donn�e doit �tre vide
        // elle sera remplie dans cette fonction
        // Return: /
        //*****************************************************************************************************
        static public bool InsertFrameInDoc(BTDoc Doc, Trame tr, ArrayList ListFrameDatas)
        {
            #region cr�ation des listes des donn�es 
            // Bon, la trame est cr�e, c'est bien, mais faut l'ins�rer
            // alors on commence par v�rifier si les symbols de donn�es ne sont pas d�ja
            // utilis�. 
            ArrayList ListDataDifferents = new ArrayList();
            ArrayList ListDataWithoutProblem = new ArrayList();
            for (int i = 0; i < ListFrameDatas.Count; i++)
            {
                Data dt = (Data)ListFrameDatas[i];
                Data ConflictData = (Data)Doc.GestData.GetFromSymbol(dt.Symbol);
                // si la donn�e n'est pas utilis�e
                if (ConflictData == null)
                {
                    // on l'ajoute a la listes des donn�es inexistantes et donc a ajouter
                    ListDataWithoutProblem.Add(dt);
                }
                else // sinon, on test si les param�tres sont les m�mes
                {
                    // si il y a une diff�rence
                    if (dt.DefaultValue != ConflictData.DefaultValue
                        || dt.IsConstant != ConflictData.IsConstant
                        || dt.IsUserVisible != ConflictData.IsUserVisible
                        || dt.Maximum != ConflictData.Maximum
                        || dt.Minimum != ConflictData.Minimum
                        || dt.Size != ConflictData.Size
                        )
                    {
                        // on l'ajoute la liste des donn�es qui posent probl�me
                        ListDataDifferents.Add(dt);
                    }
                    else
                    {
                        // la donn�e est rigoureusement identique, on la "laisse pisser"....bye bye
                    }
                }
            }
            #endregion

            bool bModeOverwrite = false;
            // Si il y a des donn�es, avec le meme symbol mais avec des param�tres diff�rents
            if (ListDataDifferents.Count != 0)
            {
                // on offre le choix a l'utilisateur:
                // - soit on �crase les param�tres de l'existante
                // - soit le symbol des donn�es cr�e va changer
                string strMessage = "Some generated datas have the same symbol as existing datas but with differents parameters. Do you want to overwrite existing data parameters?\n If no, Generated datas will be renamed";
                DialogResult dlgRes = MessageBox.Show(strMessage, "Warning", MessageBoxButtons.YesNo);
                if (dlgRes == DialogResult.Yes)
                    bModeOverwrite = true;
            }

            #region insertion des donn�es dans le document
            // pour ins�rer les donn�es:
            // on a une liste des donn�es qu'on peux mettre dans le document (celle qui n'existaient pas)
            // et une liste des donn�e qui posent probl�mes
            // rappelons qu'on a pas de liste des donn�es qui sont purement identiques

            // - Pour la liste de celle qui qu'on peux envoyer "a la barbar"...bah on le fait
            for (int i = 0; i < ListDataWithoutProblem.Count; i++)
            {
                Doc.GestData.AddObj((BaseObject)ListDataWithoutProblem[i]);
            }

            // - pour la liste des donn�es qui posent probl�me
            for (int i = 0; i < ListDataDifferents.Count; i++)
            {
                //   pour chaque donn�e:
                Data dat = (Data)ListDataDifferents[i];
                //   si on est pas en mode "Overwrite"
                if (!bModeOverwrite)
                {
                    for (int indexFormat = 0; indexFormat < BaseGest.MAX_DEFAULT_ITEM_SYMBOL; indexFormat++)
                    {
                        //      - on cr�e une chaine temporaire = nom de donn�e + suffix de type "_AG{0}"
                        string strTempSymb = dat.Symbol + string.Format("_AG{0}", indexFormat);
                        //      - on test si le nouveau nom n'est pas d�ja utilis�
                        Data pbData = (Data)Doc.GestData.GetFromSymbol(strTempSymb);
                        if (pbData == null)
                        {
                            dat.Symbol = strTempSymb;
                            Doc.GestData.AddObj(dat);
                            break;
                        }
                        else // si elle est utilis�, on test si les param�tres sont identiques
                        {
                            if (dat.DefaultValue != pbData.DefaultValue
                                || dat.IsConstant != pbData.IsConstant
                                || dat.IsUserVisible != pbData.IsUserVisible
                                || dat.Maximum != pbData.Maximum
                                || dat.Minimum != pbData.Minimum
                                || dat.Size != pbData.Size
                                )
                            {
                                // on ne fais rien, on test le prochain symbol
                            }
                            else
                            {
                                // on arr�te, la donn�e existe d�ja, donc pas besoin de l'ajouter
                                break;
                            }
                        }
                    }
                }
                else // on ecrase les param�tres de l'existante
                {
                    Data pbData = (Data)Doc.GestData.GetFromSymbol(dat.Symbol);
                    pbData.DefaultValue = dat.DefaultValue;
                    pbData.IsConstant = dat.IsConstant;
                    pbData.IsUserVisible = dat.IsUserVisible;
                    pbData.Maximum = dat.Maximum;
                    pbData.Minimum = dat.Minimum;
                    pbData.Size = dat.Size;
                    //La data cr�es est poubellis�e
                }

            }

            #endregion
            // voila, mainetnant on peux les mettre dans la trame
            for (int i = 0; i < ListFrameDatas.Count; i++)
            {
                tr.FrameDatas.Add(((BaseObject)ListFrameDatas[i]).Symbol);
            }

            // On applique un principe similaire pour la trame
            // On verifie l'existance d'une trame avec le meme nom
            // si oui on renomme la trame
            // on s'en cogne de l'avis de l'utilisteur, il est la pr cr�e sa trame
            Trame pbTr = (Trame)Doc.GestTrame.GetFromSymbol(tr.Symbol);
            if (pbTr == null)
            {
                Doc.GestTrame.AddObj(tr);
            }
            else
            {
                for (int indexFormat = 0; indexFormat < BaseGest.MAX_DEFAULT_ITEM_SYMBOL; indexFormat++)
                {
                    //      - on cr�e une chaine temporaire = nom de donn�e + suffix de type "_AG{0}"
                    string strTempSymb = tr.Symbol + string.Format("_AG{0}", indexFormat);
                    //      - on test si le nouveau nom n'est pas d�ja utilis�
                    Trame pbTrame = (Trame)Doc.GestTrame.GetFromSymbol(strTempSymb);
                    if (pbTrame == null)
                    {
                        tr.Symbol = strTempSymb;
                        Doc.GestTrame.AddObj(tr);
                        break;
                    }
                    else // si elle est utilis�
                    {
                        //on ne fais rien, trop chiant de comparer les trames
                        // on tente le suivant
                    }
                }                
            }

            return true;
        }
    }
}
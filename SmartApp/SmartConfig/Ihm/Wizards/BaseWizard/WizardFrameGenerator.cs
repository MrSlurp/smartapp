using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using CommonLib;


namespace SmartApp.Wizards
{
    /// <summary>
    /// type de registre modbus
    /// </summary>
    public enum TCPMODBUS_REG_TYPE
    {
        INPUT_REGISTER,
        OUTPUT_REGISTER,
    }

    /// <summary>
    /// commandes modbus
    /// </summary>
    public enum MODBUS_ORDER_TYPE
    {
        WRITE_SINGLE_REGISTER = 0x06,
        READ_HOLDING_REGISTER = 0x03,
        WRITE_MULTIPLE_REGISTER =0x10,
    }

    /// <summary>
    /// type de bloc liaison série
    /// </summary>
    public enum WIZ_SL_FRAME_TYPE
    {
        SL_INPUT_BLOC,
        SL_OUTPUT_BLOC,
    }

    /// <summary>
    /// ordre SL link
    /// </summary>
    public enum WIZ_SL_ORDER_TYPE
    {
        READ,
        WRITE,
    }

    /// <summary>
    /// plages d'adresses SL
    /// </summary>
    public enum WIZ_SL_ADRESS_RANGE
    {
        // les valeurs correspondent aux adresse des bloc SL
        ADDR_1_8 = 0,
        ADDR_9_16 = 8,
        ADDR_17_24 = 16,
        ADDR_25_32 = 24,
        ADDR_33_40 = 32,
        ADDR_41_48 = 40,
    }


    public static partial class WizardFrameGenerator
    {
        /// <summary>
        /// cette fonction effectue l'insertion d''une trame dans le document
        /// la trame doit être configuré avec les bon paramètres et sa liste des donnée doit être vide
        /// elle sera remplie dans cette fonction
        /// </summary>
        /// <param name="Doc">Docuement ou la trame sera insérée</param>
        /// <param name="tr">trame à insérer</param>
        /// <param name="ListFrameDatas">liste des donnée de la trame</param>
        /// <returns>true si tout s'est bien passé</returns>
        static public bool InsertFrameInDoc(BTDoc Doc, Trame tr, ArrayList ListFrameDatas)
        {
            #region création des listes des données 
            // Bon, la trame est crée, c'est bien, mais faut l'insérer
            // alors on commence par vérifier si les symbols de données ne sont pas déja
            // utilisé. 
            ArrayList ListDataDifferents = new ArrayList();
            ArrayList ListDataWithoutProblem = new ArrayList();
            for (int i = 0; i < ListFrameDatas.Count; i++)
            {
                Data dt = (Data)ListFrameDatas[i];
                Data ConflictData = (Data)Doc.GestData.GetFromSymbol(dt.Symbol);
                // si la donnée n'est pas utilisée
                if (ConflictData == null)
                {
                    // on l'ajoute a la listes des données inexistantes et donc a ajouter
                    ListDataWithoutProblem.Add(dt);
                }
                else // sinon, on test si les paramètres sont les mêmes
                {
                    // si il y a une différence
                    if (dt.DefaultValue != ConflictData.DefaultValue
                        || dt.IsConstant != ConflictData.IsConstant
                        || dt.IsUserVisible != ConflictData.IsUserVisible
                        || dt.Maximum != ConflictData.Maximum
                        || dt.Minimum != ConflictData.Minimum
                        || dt.SizeInBits != ConflictData.SizeInBits
                        )
                    {
                        // on l'ajoute la liste des données qui posent problème
                        ListDataDifferents.Add(dt);
                    }
                    else
                    {
                        // la donnée est rigoureusement identique, on la "laisse pisser"....bye bye
                    }
                }
            }
            #endregion

            bool bModeOverwrite = false;
            // Si il y a des données, avec le meme symbol mais avec des paramètres différents
            if (ListDataDifferents.Count != 0)
            {
                // on offre le choix a l'utilisateur:
                // - soit on écrase les paramètres de l'existante
                // - soit le symbol des données crée va changer
                string strMessage = Program.LangSys.C("Some generated datas have the same symbol as "
                                                      +"existing datas but with differents parameters. "
                                                      +"Do you want to overwrite existing data parameters?") 
                                                      + "\n"+ 
                                                      Program.LangSys.C("If no, Generated datas will be renamed");
                DialogResult dlgRes = MessageBox.Show(strMessage, Program.LangSys.C("Warning"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgRes == DialogResult.Yes)
                    bModeOverwrite = true;
            }

            #region insertion des données dans le document
            // pour insérer les données:
            // on a une liste des données qu'on peux mettre dans le document (celle qui n'existaient pas)
            // et une liste des donnée qui posent problèmes
            // rappelons qu'on a pas de liste des données qui sont purement identiques

            // - Pour la liste de celle qui qu'on peux envoyer "a la barbar"...bah on le fait
            for (int i = 0; i < ListDataWithoutProblem.Count; i++)
            {
                Doc.GestData.AddObj((BaseObject)ListDataWithoutProblem[i]);
            }

            // - pour la liste des données qui posent problème
            for (int i = 0; i < ListDataDifferents.Count; i++)
            {
                //   pour chaque donnée:
                Data dat = (Data)ListDataDifferents[i];
                //   si on est pas en mode "Overwrite"
                if (!bModeOverwrite)
                {
                    for (int indexFormat = 0; indexFormat < BaseGest.MAX_DEFAULT_ITEM_SYMBOL; indexFormat++)
                    {
                        //      - on crée une chaine temporaire = nom de donnée + suffix de type "_AG{0}"
                        string strTempSymb = dat.Symbol + string.Format("_AG{0}", indexFormat);
                        //      - on test si le nouveau nom n'est pas déja utilisé
                        Data pbData = (Data)Doc.GestData.GetFromSymbol(strTempSymb);
                        if (pbData == null)
                        {
                            dat.Symbol = strTempSymb;
                            Doc.GestData.AddObj(dat);
                            break;
                        }
                        else // si elle est utilisé, on test si les paramètres sont identiques
                        {
                            if (dat.DefaultValue != pbData.DefaultValue
                                || dat.IsConstant != pbData.IsConstant
                                || dat.IsUserVisible != pbData.IsUserVisible
                                || dat.Maximum != pbData.Maximum
                                || dat.Minimum != pbData.Minimum
                                || dat.SizeInBits != pbData.SizeInBits
                                )
                            {
                                // on ne fais rien, on test le prochain symbol
                            }
                            else
                            {
                                // on arrète, la donnée existe déja, donc pas besoin de l'ajouter
                                break;
                            }
                        }
                    }
                }
                else // on ecrase les paramètres de l'existante
                {
                    Data pbData = (Data)Doc.GestData.GetFromSymbol(dat.Symbol);
                    pbData.DefaultValue = dat.DefaultValue;
                    pbData.IsConstant = dat.IsConstant;
                    pbData.IsUserVisible = dat.IsUserVisible;
                    pbData.Maximum = dat.Maximum;
                    pbData.Minimum = dat.Minimum;
                    pbData.SizeAndSign = dat.SizeAndSign;
                    //La data crées est poubellisée
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
            // on s'en cogne de l'avis de l'utilisteur, il est la pr crée sa trame
            Trame pbTr = (Trame)Doc.GestTrame.GetFromSymbol(tr.Symbol);
            if (pbTr == null)
            {
                Doc.GestTrame.AddObj(tr);
            }
            else
            {
                for (int indexFormat = 0; indexFormat < BaseGest.MAX_DEFAULT_ITEM_SYMBOL; indexFormat++)
                {
                    //      - on crée une chaine temporaire = nom de donnée + suffix de type "_AG{0}"
                    string strTempSymb = tr.Symbol + string.Format("_AG{0}", indexFormat);
                    //      - on test si le nouveau nom n'est pas déja utilisé
                    Trame pbTrame = (Trame)Doc.GestTrame.GetFromSymbol(strTempSymb);
                    if (pbTrame == null)
                    {
                        tr.Symbol = strTempSymb;
                        Doc.GestTrame.AddObj(tr);
                        break;
                    }
                    else // si elle est utilisé
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

/***************************************************************************/
// PROJET : BTCommand : system de commande paramétrable pour équipement
// ayant une mécanisme de commande par liaison série/ethernet/http
/***************************************************************************/
// Fichier : 
/***************************************************************************/
// description :
// 
/***************************************************************************/
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Xml;

namespace CommonLib
{
    public class GestData : BaseGestGroup
    {
        #region constructeur
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public GestData()
        {

        }
        #endregion

        #region ReadIn  / WriteOut
        /// <summary>
        /// Lit les données de l'objet a partir de son noeud XML
        /// </summary>
        /// <param name="Node">Noeud Xml de l'objet</param>
        /// <param name="TypeApp">type d'application courante</param>
        /// <returns>true si la lecture s'est bien passé</returns>
        public override bool ReadIn(XmlNode Node, TYPE_APP TypeApp)
        {
            // relecture des données
            if (!ReadForGestData(Node, TypeApp))
                return false;

            // relecture des groupes
            if (!base.ReadIn(Node, TypeApp))
                return false;

            return true;
        }

        /// <summary>
        /// effectue la lecture spécialement pour le gestionnaire de donnée
        /// </summary>
        /// <param name="Node">Noeud Xml de l'objet</param>
        /// <param name="TypeApp">type d'application courante</param>
        /// <returns>true si la lecture s'est bien passé</returns>
        private bool ReadForGestData(XmlNode Node, TYPE_APP TypeApp)
        {
            for (int i = 0; i < Node.ChildNodes.Count; i++)
            {
                XmlNode ChildNode = Node.ChildNodes[i];
                if (ChildNode.Name != XML_CF_TAG.Data.ToString())
                    continue;

                Data NewData = new Data();
                if (NewData != null)
                {
                    if (!NewData.ReadIn(ChildNode, TypeApp))
                        return false;
                    NewData.UpdateUserVisibility();
                    this.AddObj(NewData);
                }
            }
            return true;
        }

        /// <summary>
        /// écrit les données de l'objet dans le fichier XML
        /// </summary>
        /// <param name="XmlDoc">Document XML courant</param>
        /// <param name="Node">Noeud parent du controle dans le document</param>
        /// <returns>true si l'écriture s'est déroulée avec succès</returns>
        public override bool WriteOut(XmlDocument XmlDoc, XmlNode Node)
        {
            for (int i = 0; i < this.m_ListObject.Count; i++)
            {
                Data dt = (Data)m_ListObject[i];

                XmlNode XmlData = XmlDoc.CreateElement(XML_CF_TAG.Data.ToString());
                dt.WriteOut(XmlDoc, XmlData);
                Node.AppendChild(XmlData);
            }
            base.WriteOut(XmlDoc, Node);
            return true;
        }
        #endregion

        #region fonction "utilitaires"
        /// <summary>
        /// renvoie le prochain symbol libre pour une nouvelle donnée
        /// </summary>
        /// <returns>prochaine symbol libre</returns>
        public override string GetNextDefaultSymbol()
        {
            for (int i = 0; i < MAX_DEFAULT_ITEM_SYMBOL; i++)
            {
                string strSymbolTemp = string.Format(DEFAULT_DATA_SYMB, i);
                if (GetFromSymbol(strSymbolTemp) == null)
                {
                    return strSymbolTemp;
                }
            }
            return "";
        }

        /// <summary>
        /// met a jour les données de control de l'application
        /// ne fait que créer les données nécessaire et les maintenanir a jour en fonctions des paramètres des trames
        /// n'ajoute pas, ni ne supprime les données dans la trame
        /// </summary>
        /// <param name="GestTr">Gestionnaire de trame de l'application</param>
        public void UpdateAllControlDatas(GestTrame GestTr)
        {
            //Liste des données de control nécessaires
            StringCollection ListNeededCtrlDatas = new StringCollection();
            // liste des trames ayant besoin d'une donnée de controls
            List<Trame> ListTrameNeedCtrlDatas = new List<Trame>();
            // on crée une liste syncronisée des noms des 
            // données de control nécessaire et des trames correspondantes
            for (int i = 0; i < GestTr.Count; i++)
            {
                Trame Tr = (Trame)GestTr[i];
                // si la trame possède une donnée de control
                if (Tr.CtrlDataType != CTRLDATA_TYPE.NONE.ToString())
                {
                    // et si il n'existe pas déja une donnée de control pour cette trame
                    if (GetFromSymbol(Tr.Symbol + Cste.STR_SUFFIX_CTRLDATA) == null)
                    {
                        string strNeeded = Tr.Symbol + Cste.STR_SUFFIX_CTRLDATA;
                        // alors on enregistre le nom de la donnée nécessaire
                        ListNeededCtrlDatas.Add(strNeeded);
                        // et la trame qui en a besoin
                        ListTrameNeedCtrlDatas.Add(Tr);
                    }
                }
            }

            // on fait une liste des données de control unitiles
            List<string> ListUnknownCtrlData = new List<string>();
            for (int i = 0; i < this.Count; i++)
            {
                Data dt = (Data)this[i];
                // si la donnée est une donnée de control (référence au suffix)
                // et qu'elle n'est pas dans la liste de celle dont on a besoin
                if (dt.Symbol.EndsWith(Cste.STR_SUFFIX_CTRLDATA)
                    && !ListNeededCtrlDatas.Contains(dt.Symbol))
                {
                    // on parcour la liste des trames et on vérfie si 
                    bool bFindCorrespFrame = false;
                    for (int j = 0; j < GestTr.Count; j++)
                    {
                        if (dt.Symbol == (GestTr[j].Symbol + Cste.STR_SUFFIX_CTRLDATA))
                        {
                            bFindCorrespFrame = true;
                            break;
                        }
                    }
                    if (bFindCorrespFrame == false)
                      ListUnknownCtrlData.Add(dt.Symbol);
                }
            }
            // on supprime les données inutiles
            for (int i = 0; i < ListUnknownCtrlData.Count; i++)
            {
                this.RemoveObj(ListUnknownCtrlData[i]);
            }

            // on crée les données de control manquantes
            for (int i = 0; i < ListTrameNeedCtrlDatas.Count; i++)
            {
                Data NewData = new Data();
                Trame tr = ListTrameNeedCtrlDatas[i];
                NewData.Symbol = tr.Symbol + Cste.STR_SUFFIX_CTRLDATA;
                NewData.SizeAndSign = tr.CtrlDataSize;
                this.AddObj(NewData);
            }
            // on met a jour leurs paramètres
            for (int i = 0; i < GestTr.Count; i++)
            {
                Data dt = (Data)GetFromSymbol(GestTr[i].Symbol + Cste.STR_SUFFIX_CTRLDATA);
                if (dt != null)
                {
                    dt.SizeAndSign = ((Trame)GestTr[i]).CtrlDataSize;
                    dt.IsUserVisible = false;
                    switch ((DATA_SIZE)dt.SizeAndSign)
                    {
                        case DATA_SIZE.DATA_SIZE_8B:
                            dt.Minimum = 0;
                            dt.Maximum = 255;
                            break;
                        case DATA_SIZE.DATA_SIZE_16B:
                            dt.Minimum = -32768;
                            dt.Maximum = 32767;
                            break;
                        case DATA_SIZE.DATA_SIZE_16BU:
                            dt.Minimum = 0;
                            dt.Maximum = 0xFFFF;
                            break;
                        case DATA_SIZE.DATA_SIZE_32B:
                            dt.Minimum = int.MinValue;
                            dt.Maximum = int.MaxValue;
                            break;
                        default:
                            System.Diagnostics.Debug.Assert(false);
                            break;
                    }
                }
            }
        }
        #endregion

    }
}

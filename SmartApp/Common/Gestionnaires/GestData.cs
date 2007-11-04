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
using SmartApp.Datas;
using System.Windows.Forms;
using SmartApp.Ihm;

namespace SmartApp.Gestionnaires
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
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public override bool ReadIn(XmlNode Node)
        {
            for (int i = 0; i < Node.ChildNodes.Count; i++)
            {
                XmlNode ChildNode = Node.ChildNodes[i];
                if (ChildNode.Name != XML_CF_TAG.Data.ToString())
                    continue;

                Data NewData = new Data();
                if (NewData != null)
                {
                    if (!NewData.ReadIn(ChildNode))
                        return false;
                    NewData.UpdateUserVisibility();
                    this.AddObj(NewData);
                }
            }
            if (!base.ReadIn(Node))
                return false;

            return true;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
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
        //*****************************************************************************************************
        // Description: renvoie le prochain symbol par défaut pour une donnée
        // Return: /
        //*****************************************************************************************************
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

        //*****************************************************************************************************
        // Description: met a jour les données de control de l'application
        // Return: /
        //*****************************************************************************************************
        public void UpdateAllControlDatas(GestTrame GestTr)
        {
            StringCollection ListNeededCtrlDatas = new StringCollection();
            List<Trame> ListTrameNeedCtrlDatas = new List<Trame>();
            // on crée une liste syncronisée des noms des 
            // données de control nécessaire et des trames correspondantes
            for (int i = 0; i < GestTr.Count; i++)
            {
                Trame Tr = (Trame)GestTr[i];
                if (Tr.CtrlDataType != CTRLDATA_TYPE.NONE.ToString())
                {
                    if (GetFromSymbol(Tr.Symbol + Cste.STR_SUFFIX_CTRLDATA) == null)
                    {
                        string strNeeded = Tr.Symbol + Cste.STR_SUFFIX_CTRLDATA;
                        ListNeededCtrlDatas.Add(strNeeded);
                        ListTrameNeedCtrlDatas.Add(Tr);
                    }
                }
            }

            // on fait une liste des données de control unitiles
            List<string> ListUnknownCtrlData = new List<string>();
            for (int i = 0; i < this.Count; i++)
            {
                Data dt = (Data)this[i];
                if (dt.Symbol.EndsWith(Cste.STR_SUFFIX_CTRLDATA)
                    && !ListNeededCtrlDatas.Contains(dt.Symbol))
                {
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
                NewData.Size = tr.CtrlDataSize;
                NewData.IsUserVisible = false;
                switch ((DATA_SIZE)NewData.Size)
                {
                    case DATA_SIZE.DATA_SIZE_8B:
                        NewData.Minimum = 0;
                        NewData.Maximum = 255;
                        break;
                    case DATA_SIZE.DATA_SIZE_16B:
                        NewData.Minimum = -32768;
                        NewData.Maximum = 32767;
                        break;
                    case DATA_SIZE.DATA_SIZE_32B:
                        NewData.Minimum = int.MinValue;
                        NewData.Maximum = int.MaxValue;
                        break;
                    default:
                        System.Diagnostics.Debug.Assert(false);
                        break;
                }
                this.AddObj(NewData);
            }
        }
        #endregion

    }
}

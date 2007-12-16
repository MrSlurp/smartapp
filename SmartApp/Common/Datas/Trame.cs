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
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Xml;
using SmartApp.AppEventLog;

namespace SmartApp.Datas
{
    public class Trame : BaseObject
    {
        #region déclaration des données membres
        // liste des symbols des données de la trame
        private StringCollection m_ListStrDatas;
        // liste des références vers les données de la trame
        private ArrayList m_ListRefDatas;
        // type de conversion de la trame
        private string m_strConvertType;
        // index de la donnée ou commence la conversion
        private int m_iConvertFrom;
        // index de la donnée ou se termine la conversion
        private int m_iConvertTo;

        // ces valeur sont initialisées lors du fin serilalise et ne son plus modifiées ensuite
        // représente les octets ou commence et finis la conversion
        private int m_iByteConvertFrom = -1;
        private int m_iByteConvertTo = -1;

        // méthode de calcule du checksum / CRC
        private string m_strDataClcType;
        // taille de la donnée de control
        private int m_iDataClcSize;
        // index de la donnée ou commence le calcule de la donnée de control
        private int m_iDataClcFrom;
        // index de la donnée ou se termine le calcule de la donnée de control
        private int m_iDataClcTo;

        // buffer contenant l'ensemble du header de la trame
        // le header est défini a partir de la première donnée de la trame si elle est constante
        // jusqu'a la dernier donnée constante rencontrée
        // il permet lors de la récéption des trames de tester les trames reçues pour "piocher la bonne"
        private byte[] m_FrameHeader;
        // longueur du header de la trame 
        private int m_iHeaderLenght = 0;
        #endregion

        #region constructeur
        //*****************************************************************************************************
        // Description: constructeur
        // Return: /
        //*****************************************************************************************************
        public Trame()
        {
            m_ListStrDatas = new StringCollection();
            m_ListRefDatas = new ArrayList();
            m_iDataClcSize = (int)DATA_SIZE.DATA_SIZE_8B;
            m_strConvertType = CONVERT_TYPE.NONE.ToString();
            m_strDataClcType = CTRLDATA_TYPE.NONE.ToString();
        }
        #endregion

        #region attribut
        //*****************************************************************************************************
        // Description: accesseur vers la list des données de la trame
        // Return: /
        //*****************************************************************************************************
        public StringCollection FrameDatas
        {
            get
            {
                return m_ListStrDatas;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public byte[] FrameHeader
        {
            get
            {
                return m_FrameHeader;
            }
        }

        #region Donnée de control
        //*****************************************************************************************************
        // Description: accesseur sur le type de la donnée de control
        // Return: /
        //*****************************************************************************************************
        public string CtrlDataType
        {
            get
            {
                return m_strDataClcType;
            }
            set
            {
                m_strDataClcType = value;
            }
        }

        //*****************************************************************************************************
        // Description: accesseur sur la taille de la donnée de control
        // Return: /
        //*****************************************************************************************************
        public int CtrlDataSize
        {
            get
            {
                return m_iDataClcSize;
            }
            set
            {
                m_iDataClcSize = value;
            }
        }

        //*****************************************************************************************************
        // Description: accesseur sur l'index de début du calcul de la donnée de control
        // Return: /
        //*****************************************************************************************************
        public int CtrlDataFrom
        {
            get
            {
                return m_iDataClcFrom;
            }
            set
            {
                m_iDataClcFrom = value;
            }
        }

        //*****************************************************************************************************
        // Description: accesseur sur l'index de fin du calcul de la donnée de control
        // Return: /
        //*****************************************************************************************************
        public int CtrlDataTo
        {
            get
            {
                return m_iDataClcTo;
            }
            set
            {
                m_iDataClcTo = value;
            }
        }
        #endregion

        #region type de conversion
        //*****************************************************************************************************
        // Description: accesseur sur le type de conversion
        // Return: /
        //*****************************************************************************************************
        public string ConvType
        {
            get
            {
                return m_strConvertType;
            }
            set
            {
                m_strConvertType = value;
            }
        }

        //*****************************************************************************************************
        // Description: accesseur sur l'index de début de la conversion
        // Return: /
        //*****************************************************************************************************
        public int ConvFrom
        {
            get
            {
                return m_iConvertFrom;
            }
            set
            {
                m_iConvertFrom = value;
            }
        }

        //*****************************************************************************************************
        // Description: accesseur sur l'index de fin de la conversion
        // Return: /
        //*****************************************************************************************************
        public int ConvTo
        {
            get
            {
                return m_iConvertTo;
            }
            set
            {
                m_iConvertTo = value;
            }
        }
        #endregion
        #endregion

        #region ReadIn / WriteOut
        //*****************************************************************************************************
        // Description: Lit les données de l'objet a partir de son noeud XML
        // Return: /
        //*****************************************************************************************************
        public override bool ReadIn(XmlNode Node)
        {
            base.ReadIn(Node);
            for (int i = 0; i < Node.ChildNodes.Count; i++)
            {
                XmlNode ChildNode = Node.ChildNodes[i];
                XML_CF_TAG TypeId = 0;
                try
                {
                    TypeId = (XML_CF_TAG)Enum.Parse(typeof(XML_CF_TAG), ChildNode.Name, true);
                }
                catch (Exception)
                {
                    // en cas de tag non reconne dans l'enum, une exeption est levée, 
                    // on la récupère car ca peut arriver
                    continue;
                }
                switch (TypeId)
                {
                    case XML_CF_TAG.DataList:
                        {
                            for (int j = 0; j < ChildNode.ChildNodes.Count; j++)
                            {
                                XmlNode NodeData = ChildNode.ChildNodes[j];
                                if (NodeData.Name != XML_CF_TAG.Data.ToString())
                                    continue;
                                XmlNode SymbAttr = NodeData.Attributes.GetNamedItem(XML_CF_ATTRIB.strSymbol.ToString());
                                m_ListStrDatas.Add(SymbAttr.Value);
                            }
                            break;
                        }
                    case XML_CF_TAG.DataConvert:
                        {
                            XmlNode TypeAttr = ChildNode.Attributes.GetNamedItem(XML_CF_ATTRIB.Type.ToString());
                            m_strConvertType = TypeAttr.Value;

                            XmlNode FromAttr = ChildNode.Attributes.GetNamedItem(XML_CF_ATTRIB.From.ToString());
                            m_iConvertFrom = int.Parse(FromAttr.Value);

                            XmlNode ToAttr = ChildNode.Attributes.GetNamedItem(XML_CF_ATTRIB.To.ToString());
                            m_iConvertTo = int.Parse(ToAttr.Value);
                            break;
                        }
                    case XML_CF_TAG.ControlData:
                        {
                            XmlNode TypeAttr = ChildNode.Attributes.GetNamedItem(XML_CF_ATTRIB.Type.ToString());
                            m_strDataClcType = TypeAttr.Value;

                            XmlNode FromAttr = ChildNode.Attributes.GetNamedItem(XML_CF_ATTRIB.From.ToString());
                            m_iDataClcFrom = int.Parse(FromAttr.Value);

                            XmlNode ToAttr = ChildNode.Attributes.GetNamedItem(XML_CF_ATTRIB.To.ToString());
                            m_iDataClcTo = int.Parse(ToAttr.Value);
                            break;
                        }
                    default:
                        break;
                }
            } 
            return true;
        }

        //*****************************************************************************************************
        // Description: ecrit les données de l'objet a partir de son noeud XML
        // Return: /
        //*****************************************************************************************************
        public override bool WriteOut(XmlDocument XmlDoc, XmlNode Node)
        {
            base.WriteOut(XmlDoc, Node);
            XmlNode NodeDataList = XmlDoc.CreateElement(XML_CF_TAG.DataList.ToString());
            Node.AppendChild(NodeDataList);
            for (int i = 0; i < m_ListStrDatas.Count; i++)
            {
                string strSymb = (string)m_ListStrDatas[i];
                XmlNode NodeData = XmlDoc.CreateElement(XML_CF_TAG.Data.ToString());
                XmlAttribute attrSymb = XmlDoc.CreateAttribute(XML_CF_ATTRIB.strSymbol.ToString());
                attrSymb.Value = strSymb;
                NodeData.Attributes.Append(attrSymb);
                NodeDataList.AppendChild(NodeData);
            }
            // noeud donnée de control
            XmlNode NodeCtrlData = XmlDoc.CreateElement(XML_CF_TAG.ControlData.ToString());
            XmlAttribute attrCRCType = XmlDoc.CreateAttribute(XML_CF_ATTRIB.Type.ToString());
            XmlAttribute attrCRCFrom = XmlDoc.CreateAttribute(XML_CF_ATTRIB.From.ToString());
            XmlAttribute attrCRCTo = XmlDoc.CreateAttribute(XML_CF_ATTRIB.To.ToString());
            attrCRCType.Value = m_strDataClcType.ToString(); ;
            attrCRCFrom.Value = m_iDataClcFrom.ToString(); ;
            attrCRCTo.Value = m_iDataClcTo.ToString(); ;
            NodeCtrlData.Attributes.Append(attrCRCType);
            NodeCtrlData.Attributes.Append(attrCRCFrom);
            NodeCtrlData.Attributes.Append(attrCRCTo);
            Node.AppendChild(NodeCtrlData);
            // noeud propriété de conversion
            XmlNode NodeConv = XmlDoc.CreateElement(XML_CF_TAG.DataConvert.ToString());
            XmlAttribute attrConvType = XmlDoc.CreateAttribute(XML_CF_ATTRIB.Type.ToString());
            XmlAttribute attrConvFrom = XmlDoc.CreateAttribute(XML_CF_ATTRIB.From.ToString());
            XmlAttribute attrConvTo = XmlDoc.CreateAttribute(XML_CF_ATTRIB.To.ToString());
            attrConvType.Value = m_strConvertType;
            attrConvFrom.Value = m_iConvertFrom.ToString();
            attrConvTo.Value = m_iConvertTo.ToString();
            NodeConv.Attributes.Append(attrConvType);
            NodeConv.Attributes.Append(attrConvFrom);
            NodeConv.Attributes.Append(attrConvTo);
            Node.AppendChild(NodeConv);
            return true;
        }

        //*****************************************************************************************************
        // Description: termine la lecture de l'objet. utilisé en mode Commande pour récupérer les référence
        // vers les objets utilisés
        // Return: /
        //*****************************************************************************************************
        public override bool FinalizeRead(BTDoc Doc)
        {
            for (int i = 0; i < m_ListStrDatas.Count; i++)
            {
                string strData = (string)m_ListStrDatas[i];
                Data Dat = (Data)Doc.GestData.GetFromSymbol(strData);
                if (Dat == null)
                {
                    string strMessage;
                    strMessage = string.Format("Frame Data not found (Frame {0}, Data {1}", m_strSymbol, strData);
                    LogEvent log = new LogEvent(LOG_EVENT_TYPE.INFO, strMessage);
                    MDISmartCommandMain.EventLogger.AddLogEvent(log);
                    continue;
                }
                m_ListRefDatas.Add(Dat);
            }

            // cette première passe permet d'initialiser le buffer contenant le header de la trame
            int Taille = GetTrameSizeInByte();
            // crée le buffer
            Byte[] buffer = new Byte[Taille];
            // on initialise le buffer
            for (int i = 0; i < buffer.Length; i++)
                buffer[i] = 0;

            CalcBeginAndEndConversion();
            //certaines choses sont initialisée par ces deux fonctions, comme le hearder de trame
            PrepareSendBuffer(buffer);
            TreatWriteConversion(buffer);
            return true;
        }
        #endregion

        #region Gestion des AppMessages
        //*****************************************************************************************************
        // Description: effectue les opération nécessaire lors de la récéption d'un message
        // Return: /
        //*****************************************************************************************************
        public override void TraiteMessage(MESSAGE Mess, object obj)
        {
            switch (Mess)
            {
                case MESSAGE.MESS_ASK_ITEM_DELETE:
                    if (((MessAskDelete)obj).TypeOfItem == typeof(Data))
                    {
                        MessAskDelete MessParam = (MessAskDelete)obj;
                        for (int i = 0; i < m_ListStrDatas.Count; i++)
                        {
                            if (m_ListStrDatas[i] == MessParam.WantDeletetItemSymbol)
                            {
                                string strMess = string.Format("Frame {0} will lost data", Symbol);
                                MessParam.ListStrReturns.Add(strMess);
                            }
                        }
                    }
                    break;
                case MESSAGE.MESS_ITEM_DELETED:
                    if (((MessDeleted)obj).TypeOfItem == typeof(Data))
                    {
                        MessDeleted MessParam = (MessDeleted)obj;
                        for (int i = 0; i < m_ListStrDatas.Count; i++)
                        {
                            if (m_ListStrDatas[i] == MessParam.DeletetedItemSymbol)
                            {
                                m_ListStrDatas.RemoveAt(i);
                            }
                        }
                    }
                    break;
                case MESSAGE.MESS_ITEM_RENAMED:
                    if (((MessItemRenamed)obj).TypeOfItem == typeof(Data))
                    {
                        MessItemRenamed MessParam = (MessItemRenamed)obj;
                        for (int i = 0; i < m_ListStrDatas.Count; i++)
                        {
                            if (m_ListStrDatas[i] == MessParam.OldItemSymbol)
                            {
                                m_ListStrDatas[i] = MessParam.NewItemSymbol;
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region fonction utilisées dans SmartCommand pour créer et lire les trames
        //*****************************************************************************************************
        // Description: crée la trame a envoyer a partir des données qu'elle contien et renvoie le buffer
        // Return: /
        //*****************************************************************************************************
        public Byte[] CreateTrameToSend()
        {
            // Obtenir la taille totale en octets
            int Taille = GetTrameSizeInByte();
            // crée le buffer
            Byte[] buffer = new Byte[Taille];
            // on initialise le buffer
            for (int i = 0; i < buffer.Length; i++)
                buffer[i] = 0;

            //la donnée de control est calculé dans cette fonction juste avant d'être placé dans la trame
            PrepareSendBuffer(buffer);
            // traiter la conversion
            buffer = TreatWriteConversion(buffer);
            return buffer;
        }

        //*****************************************************************************************************
        // Description: a partir d'une trame donnée, met a jour les valeurs des données
        // effectue les controls sur la taille de la trame et la donnée de control
        // Return: /
        //*****************************************************************************************************
        public bool TreatRecieveTrame(Byte[] buffer)
        {
            // vérifier que les tailles sont identiques
            int SizeofTrameToRecieve = GetConvertedTrameSizeInByte();
            if (buffer.Length != SizeofTrameToRecieve)
            {
                string strmess = string.Format("Error reading frame {0}, {1} bytes recieved, {2} exepected", Symbol, buffer.Length, SizeofTrameToRecieve);
                LogEvent log = new LogEvent(LOG_EVENT_TYPE.ERROR, strmess);
                MDISmartCommandMain.EventLogger.AddLogEvent(log);              
                return false;
            }

            // traiter d'abord la conversion
            buffer = TreatReadConversion(buffer);

            // vérifier la cohérence du checksum
            if (!CheckCtrlData(buffer))
                return false;
            
            // relire les donnée de la trame
            if (!ReadRecievedBuffer(buffer))
                return false;
            else
                return true;
        }

        //*****************************************************************************************************
        // Description: effectue la première étape de création de la trame
        // en prenant les données une par une, reconstruit la trame sous forme de données binaires.
        // si une donnée de control existe, celle ci est calculée avant d'être assignée dans la trame
        // Return: /
        //*****************************************************************************************************
        private void PrepareSendBuffer(byte[] buffer)
        {
            int CurrentDataPos = 0;
            int ByteWhereBeginDataCtrl = 0;
            int ByteWhereEndDataCtrl = 0;
            bool bNonConstantDataFound = false;
            int iTempHeaderLenght = 0;
            for (int i = 0; i < m_ListRefDatas.Count; i++)
            {
                Data dat = (Data)m_ListRefDatas[i];
                int DataSz = dat.SizeInBits;
                int BeginData = CurrentDataPos;
                // -1 car l'index des bits est basé a 0;
                int EndData = (CurrentDataPos - 1) + DataSz;
                int byteBegin = BeginData / Cste.SIZE_1_BYTE;
                int byteEnd = EndData / Cste.SIZE_1_BYTE;

                #region Traitement de la donnée de controle
                // on se fou du fait qu'on utilise une donnée de control ou non
                // car si on ne l'utilise pas, ses valeurs ne seront pas utilisées
                if (i == m_iDataClcFrom)
                {
                    ByteWhereBeginDataCtrl = byteBegin;
                }
                if (i == m_iDataClcTo)
                {
                    ByteWhereEndDataCtrl = byteEnd;
                }

                // si il doit y avoir donnée de control, et qu'on est dessus,
                // alors on la calcul avant de la positionner

                if (m_strDataClcType != CTRLDATA_TYPE.NONE.ToString() && dat.Symbol.EndsWith(Cste.STR_SUFFIX_CTRLDATA)) 
                {
                    // en toutes logique la donnée de control ne doit pas etre positionnée n'importe comment
                    int leftOffset = CurrentDataPos % Cste.SIZE_1_BYTE;
                    if (leftOffset != 0)
                        System.Diagnostics.Debug.Assert(false, "erreur sur la position de la donnée de control");

                    Byte[] tempBuffer = new Byte[(ByteWhereEndDataCtrl - ByteWhereBeginDataCtrl)+1];
                    int tempIndex = 0;
                    for (int k = ByteWhereBeginDataCtrl; k <= ByteWhereEndDataCtrl; k++)
                    {
                        tempBuffer[tempIndex] = buffer[k];
                        tempIndex ++;
                    }
                    CalcCtrlData(tempBuffer, dat);
                }
                #endregion // traitement de la donnée de control

                int DataValue = dat.Value;

                // si le header de trame n'est encore crée, regarde (en octet) sa taille (non convertie)
                if (m_FrameHeader == null)
                {
                    // si on a pos encore été coupé par une donné non constante
                    if (!bNonConstantDataFound && dat.IsConstant)
                    {
                        // la taille = la taille de l'octet de fin de la donnée
                        iTempHeaderLenght = byteEnd;
                    }
                    else
                        bNonConstantDataFound = true;
                }
                // la fin dans la conversion...

                //si la donnée n'est pas comprise sur une seul octet, on la répartie sur la place qu'elle doit prendre
                if (byteBegin != byteEnd)
                {
                    #region traitement de la répartition de la donnée sur plusieurs octets
                    //on calcule le nombres de coupures
                    // pour une donnée 32 bits on peut être a cheval sur 5 octets
                    int nbCut = byteEnd - byteBegin;
                    int NextByteBegin = ((byteEnd + 1) * Cste.SIZE_1_BYTE);
                    int CurrentPosInBit = BeginData;
                    int CurrentByte = byteBegin;
                    int bitsWriten = 0;

                    for (int j = 0; j <= nbCut; j++)
                    {
                        //on est au début de la donnée, on peux avoir a décaler la valeur vers la droite
                        // on ne prend que les derniers bits de la donnée
                        int TmpValue  = 0;
                        if (CurrentByte == byteBegin)
                        {
                            int leftOffset = CurrentPosInBit % Cste.SIZE_1_BYTE;
                            byte mask1 = (byte)(0xFF >> leftOffset);
                            int nbAvailableBits = Cste.SIZE_1_BYTE - leftOffset;
                            // on ne prend que le nombre de bits qu'on peu écrire sur la donnée
                            TmpValue = DataValue >> (DataSz - nbAvailableBits);
                            TmpValue = TmpValue & mask1;
                            buffer[CurrentByte] |= (byte)TmpValue;
                            CurrentPosInBit += nbAvailableBits;
                            bitsWriten = nbAvailableBits;
                        }
                        // on est a la fin de la donnée, on peut avoir a décaler la valeur vers la gauche
                        else if (CurrentByte == byteEnd)
                        {
                            int nbBitsRestants = DataSz - bitsWriten;
                            //int rightOffset = ((byteEnd + 1) * 8) - (CurrentPosInBit + nbBitsRestants);
                            int rightOffset = Cste.SIZE_1_BYTE - nbBitsRestants;
                            int tmpValue = DataValue;
                            int mask1 = (int) (0xFFFFFFFF >> (DataSz - bitsWriten));
                            tmpValue = tmpValue & mask1;
                            tmpValue = tmpValue << rightOffset;
                            buffer[CurrentByte] |= (byte)tmpValue;
                            CurrentPosInBit += nbBitsRestants;
                        }
                        // on est "au mileu", on prend juste les 8 bits a écrire
                        else
                        {
                            TmpValue = DataValue >> (DataSz - (bitsWriten + Cste.SIZE_1_BYTE));
                            buffer[CurrentByte] |= (byte)TmpValue;
                            CurrentPosInBit += Cste.SIZE_1_BYTE;
                            bitsWriten += Cste.SIZE_1_BYTE;
                        }
                        CurrentByte++;
                    }
                    #endregion
                }
                else
                {
                    #region traitement du positionement de la donnée sur un seul octet
                    // on calcule le décallage de la donnée a gauche par rapport a sa place
                    int leftOffset = BeginData % Cste.SIZE_1_BYTE;
                    // on calcule le décallage de droite par rapport au prochain octet 
                    // (d'ou le byteEnd+1)
                    int rightOffset = ((byteEnd + 1) * Cste.SIZE_1_BYTE) - (CurrentDataPos + DataSz);
                    // on crée deux masques complémentaires
                    // mask1 s'occuper de "couper" ce qui est a gauche
                    // mask2 s'occuper de "couper" ce qui est droite
                    byte mask1 = (byte)(0xFF >> (BeginData % Cste.SIZE_1_BYTE));
                    byte mask2 = (byte)(0xFF << rightOffset);
                    // on crée le mask complet
                    byte Mask = (byte)(mask1 & mask2);
                    // on obtien une donnée ou les seul bits qui peuvent etre a 1,
                    // sont ceux qui représentent la valeur dans l'octet de la trame;
                    byte byteVal = (byte)((DataValue << rightOffset ) & Mask);

                    buffer[byteBegin] |= byteVal;
                    #endregion

                }
                CurrentDataPos += DataSz;
            }
            if (m_FrameHeader == null)
            {
                m_iHeaderLenght = iTempHeaderLenght;
            }
        }

        //*****************************************************************************************************
        // Description: a partir d'une trame binaire, relis les données qui y sont contenu
        //              les données constantes ne sont pas modifiées
        // Return: /
        //*****************************************************************************************************
        private bool ReadRecievedBuffer(Byte[] buffer)
        {
            int CurrentDataPos = 0;
            for (int i = 0; i < m_ListRefDatas.Count; i++)
            {
                Data dat = (Data)m_ListRefDatas[i];
                int DataSz = dat.SizeInBits;
                int BeginData = CurrentDataPos;
                // -1 car l'index des bits est basé a 0;
                int EndData = (CurrentDataPos - 1) + DataSz;
                int byteBegin = BeginData / Cste.SIZE_1_BYTE;
                int byteEnd = EndData / Cste.SIZE_1_BYTE;

                int DataValue = 0;
                //si la donnée n'est pas comprise sur une seul octet, on la répartie sur la place qu'elle doit prendre
                if (byteBegin != byteEnd)
                {
                    #region traitement de la répartition de la donnée sur plusieurs octets
                    //on calcule le nombres de coupures
                    // pour une donnée 32 bits on peut être a cheval sur 5 octets
                    int nbCut = byteEnd - byteBegin;
                    int NextByteBegin = ((byteEnd + 1) * Cste.SIZE_1_BYTE);
                    int CurrentPosInBit = BeginData;
                    int CurrentByte = byteBegin;
                    int bitsRead = 0;
                    // la valeur temporaire est un entier car c'est la taille de donnée la plus grande qu'on peu avoir
                    for (int j = 0; j <= nbCut; j++)
                    {
                        // on est au début de la donnée, on prend les bits qui nous interresse a l'index courant
                        // su buffer, et on les met au bon endroit dans la donnée
                        int TmpValue = 0;
                        if (CurrentByte == byteBegin)
                        {
                            int leftOffset = CurrentPosInBit % Cste.SIZE_1_BYTE;
                            byte mask1 = (byte)(0xFF >> leftOffset);
                            int nbAvailableBits = Cste.SIZE_1_BYTE - leftOffset;
                            // on ne prend que le nombre de bits qu'on peu lire sur la donnée
                            TmpValue = buffer[CurrentByte];
                            TmpValue = TmpValue >> leftOffset;
                            TmpValue = TmpValue & mask1;
                            CurrentPosInBit += nbAvailableBits;
                            bitsRead = nbAvailableBits;
                            DataValue |= TmpValue << (DataSz - nbAvailableBits);
                        }
                        // on est a la fin de la donnée, on peut avoir a décaler la valeur vers la gauche
                        else if (CurrentByte == byteEnd)
                        {
                            int nbBitsRestants = DataSz - bitsRead;
                            //int rightOffset = ((byteEnd + 1) * 8) - (CurrentPosInBit + nbBitsRestants);
                            int rightOffset = Cste.SIZE_1_BYTE - nbBitsRestants;
                            int mask1 = (int)(0xFFFFFFFF >> (DataSz - bitsRead));
                            int tmpValue = buffer[CurrentByte] >> rightOffset;
                            tmpValue = tmpValue & mask1;
                            DataValue |= (byte)tmpValue;
                            CurrentPosInBit += nbBitsRestants;
                        }
                        // on est "au mileu", on prend juste les 8 bits a écrire
                        else
                        {
                            TmpValue = buffer[CurrentByte] ;
                            DataValue |= (byte)(TmpValue << (DataSz - (bitsRead + Cste.SIZE_1_BYTE)));
                            CurrentPosInBit += Cste.SIZE_1_BYTE;
                            bitsRead += Cste.SIZE_1_BYTE;
                        }
                        CurrentByte++;
                    }
                    #endregion
                }
                else
                {
                    #region traitement d'une donnée positionée sur un seul octet
                    // on calcule le décallage de la donnée a gauche par rapport a sa place
                    int leftOffset = BeginData % Cste.SIZE_1_BYTE;
                    // on calcule le décallage de droite par rapport au prochain octet 
                    // (d'ou le byteEnd+1)
                    int rightOffset = ((byteEnd + 1) * Cste.SIZE_1_BYTE) - (CurrentDataPos + DataSz);
                    // on crée deux masques complémentaires
                    // mask1 s'occuper de "couper" ce qui est a gauche
                    // mask2 s'occuper de "couper" ce qui est droite
                    byte mask1 = (byte)(0xFF >> (BeginData % Cste.SIZE_1_BYTE));
                    byte mask2 = (byte)(0xFF << rightOffset);
                    // on crée le mask complet
                    byte Mask = (byte)(mask1 & mask2);
                    // on extrait du buffer les bits qui nous interessent
                    byte byteVal = (byte)(buffer[byteBegin] & Mask);
                    // on remet les bits extraits à leur place
                    DataValue = (byte)(byteVal >> rightOffset);
                    #endregion
                }
                CurrentDataPos += DataSz;
                // une valeur constance ne peux pas être assignée
                if (dat.IsConstant)
                {
                    if (DataValue != dat.Value)
                    {
                        return false;
                    }
                }
                else
                {
                    if (dat.SizeInBits == Cste.SIZE_2_BYTE) // S16
                    {
                        short S16Value = (short)DataValue;
                        dat.Value = S16Value;
                    }
                    else
                        dat.Value = DataValue;
                }
            }
            return true;
        }

        //*****************************************************************************************************
        // Description: traite la conversion de la trame après qu'elle ai été crée avec PrepareSendBuffer(...)
        // Return: /
        //*****************************************************************************************************
        private Byte[] TreatWriteConversion(Byte[] buffer)
        {
            Byte[] FinalBuffer;
            // facteur de conversion: 1 = pas d'octets supplémentaire
            // 2= chaque octet converti prend deux octets
            // etc...
            //int CurrentDataPos = 0;
            //int ByteWhereBeginConvert = 0;
            //int ByteWhereEndConvert = 0;

            // +1 car la première donnée est comprise, et la dernière aussi
            int NbByteToConvert = (m_iByteConvertTo - m_iByteConvertFrom) + 1;
            FinalBuffer = new Byte[GetConvertedTrameSizeInByte()];

            int indexInFinalBuffer = 0;
            //
            for (int i = 0; i < buffer.Length; i++)
            {
                // conversion ascii
                if (m_strConvertType == CONVERT_TYPE.ASCII.ToString())
                {
                    if (i >= m_iByteConvertFrom && i <= m_iByteConvertTo)
                    {
                        byte Tmp = Bin2Ascii((byte)((buffer[i] & 0xf0) >> 4));
                        FinalBuffer[indexInFinalBuffer++] = Tmp;
                        Tmp = Bin2Ascii((byte)(buffer[i] & 0x0f));
                        FinalBuffer[indexInFinalBuffer++] = Tmp;
                    }
                    else
                    {
                        FinalBuffer[indexInFinalBuffer++] = buffer[i];
                    }
                }
                // pas de conversion
                else
                    FinalBuffer[i] = buffer[i];
            }
            // si on a pas encore initialisé le header
            if (m_FrameHeader == null)
            {
                m_FrameHeader = new byte[m_iHeaderLenght];
                for (int i = 0; i < m_FrameHeader.Length; i++)
                {
                    m_FrameHeader[i] = FinalBuffer[i];
                }
            }

            return FinalBuffer;
        }

        //*****************************************************************************************************
        // Description: traite la conversion de la trame avant qu'elle soit traitée par ReadRecievedBuffer(...)
        // Return: /
        //*****************************************************************************************************
        private Byte[] TreatReadConversion(Byte[] buffer)
        {
            Byte[] FinalBuffer;
            // facteur de conversion: 1 = pas d'octets supplémentaire
            // 2= chaque octet converti prend deux octets
            // etc...
            int ConversionFactor = 1;
            if (m_strConvertType == CONVERT_TYPE.ASCII.ToString())
            {
                ConversionFactor = 2;
            }
            int CurrentDataPos = 0;
            int ByteWhereBeginConvert = 0;
            int ByteWhereEndConvert = 0;
            #region calcule des octets ou commence et ou fini la conversion
            for (int i = 0; i < m_ListRefDatas.Count; i++)
            {
                Data dat = (Data)m_ListRefDatas[i];
                int DataSz = dat.SizeInBits;
                int BeginData = CurrentDataPos;
                // -1 car l'index des bits est basé a 0;
                int EndData = (CurrentDataPos - 1) + DataSz;
                int byteBegin = BeginData / Cste.SIZE_1_BYTE;
                int byteEnd = EndData / Cste.SIZE_1_BYTE;
                if (i == m_iConvertFrom)
                {
                    ByteWhereBeginConvert = byteBegin;
                }
                if (i == m_iConvertTo)
                {
                    ByteWhereEndConvert = byteEnd;
                }
                CurrentDataPos += DataSz;
            }
            #endregion

            // +1 car la première donnée est comprise, et la dernière aussi
            FinalBuffer = new Byte[GetTrameSizeInByte()];
            int NbByteToConvert = (ByteWhereEndConvert - ByteWhereBeginConvert) + 1;

            //dans le cas decimal la conversion en lecture, l'octet de fin de conversion est égal a
            // l'octet de début + le nombre d'octets a convertir fois le facteur de conversion
            ByteWhereEndConvert = ByteWhereBeginConvert + (NbByteToConvert * ConversionFactor);
            int indexInFinalBuffer = 0;
            //
            for (int i = 0; i < buffer.Length; i++)
            {
                // conversion ascii
                if (m_strConvertType == CONVERT_TYPE.ASCII.ToString())
                {
                    if (i >= ByteWhereBeginConvert && i < ByteWhereEndConvert)
                    {
                        FinalBuffer[indexInFinalBuffer] = 0;
                        byte Tmp = Ascii2Bin(buffer[i]);
                        i++;
                        FinalBuffer[indexInFinalBuffer] = (byte)(Tmp<<4);
                        System.Diagnostics.Debug.Assert(i < buffer.Length, "Dépassement de buffer dans la conversion lecture de trame");
                        Tmp = Ascii2Bin(buffer[i]);
                        FinalBuffer[indexInFinalBuffer++] += Tmp;
                    }
                    else
                    {
                        FinalBuffer[indexInFinalBuffer++] = buffer[i];
                    }
                }
                // pas de conversion
                else
                    FinalBuffer[i] = buffer[i];
            }
            return FinalBuffer;
        }

        //*****************************************************************************************************
        // Description: calcule la taille en octets de la trame (taille brute de la trame)
        // Return: /
        //*****************************************************************************************************
        private int GetTrameSizeInByte()
        {
            int sizeInBits = 0;
            for (int i = 0; i < m_ListRefDatas.Count; i++)
            {
                Data dat = (Data)m_ListRefDatas[i];
                sizeInBits += dat.SizeInBits;
            }
            int sizeInByte = sizeInBits / Cste.SIZE_1_BYTE;
            int rest = sizeInBits % Cste.SIZE_1_BYTE;
            if (rest != 0)
            {
                System.Diagnostics.Debug.Assert(false, "Taille de la trame pas multiple d'octets");
            }
            return sizeInByte;
        }

        //*****************************************************************************************************
        // Description: calcule la taille en octets de la trame (taille convertie de la trame)
        // Return: /
        //*****************************************************************************************************
        public int GetConvertedTrameSizeInByte()
        {
            int sizeInBytes = GetTrameSizeInByte();
            int ConversionFactor = 1;
            if (m_strConvertType == CONVERT_TYPE.ASCII.ToString())
            {
                ConversionFactor = 2;
            }
            int CurrentDataPos = 0;
            int ByteWhereBeginConvert = 0;
            int ByteWhereEndConvert = 0;
            #region calcule des octets ou commence et ou fini la conversion
            for (int i = 0; i < m_ListRefDatas.Count; i++)
            {
                Data dat = (Data)m_ListRefDatas[i];
                int DataSz = dat.SizeInBits;
                int BeginData = CurrentDataPos;
                // -1 car l'index des bits est basé a 0;
                int EndData = (CurrentDataPos - 1) + DataSz;
                int byteBegin = BeginData / Cste.SIZE_1_BYTE;
                int byteEnd = EndData / Cste.SIZE_1_BYTE;
                if (i == m_iConvertFrom)
                {
                    ByteWhereBeginConvert = byteBegin;
                }
                if (i == m_iConvertTo)
                {
                    ByteWhereEndConvert = byteEnd;
                }
                CurrentDataPos += DataSz;
            }
            #endregion

            // +1 car la première donnée est comprise, et la dernière aussi
            int NbByteToConvert = (ByteWhereEndConvert - ByteWhereBeginConvert) + 1;
            int FinalSize = sizeInBytes + (NbByteToConvert * (ConversionFactor - 1));
            return FinalSize;
        }

        //*****************************************************************************************************
        // Description: calcule la donnée de control "dat" a partir des octets passés dans "buffer"
        //              le buffer ne dois contenir que les octets qui sont utilisé pour le calcule de la donnée de control
        // Return: /
        //*****************************************************************************************************
        private void CalcCtrlData(Byte[] buffer, Data dat)
        {
            if (m_strDataClcType == CTRLDATA_TYPE.SUM_COMPL_P1.ToString())
            {
                int Value = 0;
                for (int i = 0; i < buffer.Length; i++)
                {
                    Value += buffer[i];
                }
                if (dat.SizeInBits == Cste.SIZE_1_BYTE)
                {
                    byte ByteValue = (byte)(Value & 0xFF);
                    ByteValue = (byte)(-ByteValue);
                    dat.Value = ByteValue;
                }
                else
                    System.Diagnostics.Debug.Assert(false);
            }
            else if (m_strDataClcType == CTRLDATA_TYPE.MODBUS_CRC.ToString())
            {
                if (dat.SizeInBits == Cste.SIZE_2_BYTE)
                {
                    uint Crc = CalculCRC(buffer);
                    int TempValue = (int)(Crc >> 8) & 0x00FF;
                    TempValue |= (int)(Crc << 8) & 0xFF00;
                    dat.Value = TempValue;
                }
                else
                    System.Diagnostics.Debug.Assert(false);
            }
            else // pas prévu
                System.Diagnostics.Debug.Assert(false, "Type de calcul de donnée de control pas codé");
        }

        //*****************************************************************************************************
        // Description: En lecture, avant de commencer a relire les valeurs, on vérifie la valididtée de la donnée de control
        // Return: /
        //*****************************************************************************************************
        private bool CheckCtrlData(Byte[] buffer)
        {
            int RecievedCtrlDataVal = 0;
            bool bCtrlDataHaveBeenCalc = false;
            int CurrentDataPos = 0;
            int ByteWhereBeginDataCtrl = 0;
            int ByteWhereEndDataCtrl = 0;
            Data dat = null;
            Data ControlData = null;
            for (int i = 0; i < m_ListRefDatas.Count; i++)
            {
                dat = (Data)m_ListRefDatas[i];
                int DataSz = dat.SizeInBits;
                int BeginData = CurrentDataPos;
                // -1 car l'index des bits est basé a 0;
                int EndData = (CurrentDataPos - 1) + DataSz;
                int byteBegin = BeginData / Cste.SIZE_1_BYTE;
                int byteEnd = EndData / Cste.SIZE_1_BYTE;

                #region Traitement de la donnée de controle
                // on se fou du fait qu'on utilise une donnée de control ou non
                // car si on ne l'utilise pas, ses valeurs ne seront pas utilisées
                if (i == m_iDataClcFrom)
                {
                    ByteWhereBeginDataCtrl = byteBegin;
                }
                if (i == m_iDataClcTo)
                {
                    ByteWhereEndDataCtrl = byteEnd;
                }

                // si il doit y avoir donnée de control, et qu'on est dessus,
                // alors on la calcul avant de la positionner
                // dans le cas de la lecture, on la calcule pr vérifer si elle correspond bien a celle reçue
                if (m_strDataClcType != CTRLDATA_TYPE.NONE.ToString() && dat.Symbol.EndsWith(Cste.STR_SUFFIX_CTRLDATA))
                {
                    // en toutes logique la donnée de control ne doit pas etre positionnée n'importe comment
                    int leftOffset = CurrentDataPos % Cste.SIZE_1_BYTE;
                    if (leftOffset != 0)
                        System.Diagnostics.Debug.Assert(false, "erreur sur la position de la donnée de control");

                    Byte[] tempBuffer = new Byte[(ByteWhereEndDataCtrl - ByteWhereBeginDataCtrl) + 1];
                    int tempIndex = 0;
                    for (int k = ByteWhereBeginDataCtrl; k <= ByteWhereEndDataCtrl; k++)
                    {
                        tempBuffer[tempIndex] = buffer[k];
                        tempIndex++;
                    }
                    // on calcule la donnée de control directement dans la donnée
                    CalcCtrlData(tempBuffer, dat);
                    ControlData = dat;
                    bCtrlDataHaveBeenCalc = true;
                    // on relis la valeur de la donnée de control a partir du buffer dans un variable temporaire
                    // note : la taille est toujours multiple de 8 bits
                    int BegByteOfCtrlData = byteBegin;
                    int EndByteOfCtrlData = byteEnd;
                    for (int k = byteBegin; k <= byteEnd; k++)
                    {
                        int tmpVal = (buffer[k] << ((byteEnd - byteBegin) * Cste.SIZE_1_BYTE));
                        RecievedCtrlDataVal += tmpVal;
                    }
                    //RecievedCtrlDataVal
                }
                #endregion
                CurrentDataPos += DataSz;
            }
            // si la donnée de control a été calculée, elle peu ne pas l'avoir été losqu'il n'y en a pas
            // au quel cas on renvoie toujours vrai
            if (bCtrlDataHaveBeenCalc)
            {
                // incohérence de la donnée de control
                if (ControlData.Value != RecievedCtrlDataVal) 
                {
                    return false;
                }
            }
            return true;
        }

        private void CalcBeginAndEndConversion()
        {
            int CurrentDataPos = 0;
            int previousEndByte = 0;
            int sizeToAddToHeader = 0;
            for (int i = 0; i < m_ListRefDatas.Count; i++)
            {
                Data dat = (Data)m_ListRefDatas[i];
                int DataSz = dat.SizeInBits;
                int BeginData = CurrentDataPos;
                // -1 car l'index des bits est basé a 0;
                int EndData = (CurrentDataPos - 1) + DataSz;
                int byteBegin = BeginData / Cste.SIZE_1_BYTE;
                int byteEnd = EndData / Cste.SIZE_1_BYTE;
                if (m_FrameHeader == null
                    && m_strConvertType != CONVERT_TYPE.NONE.ToString()
                    && previousEndByte != byteEnd
                    && byteEnd <= m_iHeaderLenght)
                {
                    previousEndByte = byteEnd;
                    sizeToAddToHeader++;
                }
                if (i == m_iConvertFrom)
                {
                    m_iByteConvertFrom = byteBegin;
                }
                if (i == m_iConvertTo)
                {
                    m_iByteConvertTo = byteEnd;
                }
                CurrentDataPos += DataSz;
            }
            if (m_FrameHeader == null)
            {
                m_iHeaderLenght += sizeToAddToHeader;
            }
        }

        //*****************************************************************************************************
        // Description: convertir une donnée binaire en ascii
        // Return: /
        //*****************************************************************************************************
        private byte Bin2Ascii(byte Data)
        {
            byte Out = 0;

            // Masquage poids fort au cas ou ...
            Data &= 0x0f;
            // Conversion
            if (Data < 10) 
                Out = (byte)(0x30 + Data); // 0x30 = '0'
            else 
                Out = (byte)(0x41 + Data - 10);// 0x41 = 'A'

            return Out;
        }

        //*****************************************************************************************************
        // Description: concertie la donnée ascii en binaires
        // Return: /
        //*****************************************************************************************************
        private byte Ascii2Bin(byte Data)
        {
            byte Out;

            if ((Data >= 0x30) && (Data <= 0x39)) // 0x30 = '0' , 0x39 = '9'
                Out = (byte)(Data - 0x30);
            else 
                Out = (byte)(Data - 0x41 + 10); //0x41 = 'A'

            return Out;
        }
        #endregion

        #region fonction de calcul du CRC 16 modbus
        //*****************************************************************************************************
        // Description: Calcul du CRC
        // Return: /
        //*****************************************************************************************************
        uint CalculCRC(byte[] Buf)
        {
            ulong crccode = 0xFFFF;
            for (uint i = 0; i < Buf.Length; i++)
            {
                crccode = onecrc((uint)Buf[i], (uint)crccode);
            }
            return (uint)crccode; 
        }

        //*****************************************************************************************************
        // Description: Accumulation du CRC
        // Return: /
        //*****************************************************************************************************
        uint onecrc(uint item, uint start)
        {
            int i;
            uint accum = 0;
            uint mask;

            accum = start;

            mask = 0xA001;
            for (i = 8; i > 0; i--)
            {
                if (((item ^ accum) & 0x0001) != 0)
                    accum = (accum >> 1) ^ mask;
                else
                    accum >>= 1;

                item >>= 1;
            }
            return (accum);
        }

        #endregion
    }
}

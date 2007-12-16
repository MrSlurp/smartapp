/***************************************************************************/
// PROJET : BTCommand : system de commande param�trable pour �quipement
// ayant une m�canisme de commande par liaison s�rie/ethernet/http
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
        #region d�claration des donn�es membres
        // liste des symbols des donn�es de la trame
        private StringCollection m_ListStrDatas;
        // liste des r�f�rences vers les donn�es de la trame
        private ArrayList m_ListRefDatas;
        // type de conversion de la trame
        private string m_strConvertType;
        // index de la donn�e ou commence la conversion
        private int m_iConvertFrom;
        // index de la donn�e ou se termine la conversion
        private int m_iConvertTo;

        // ces valeur sont initialis�es lors du fin serilalise et ne son plus modifi�es ensuite
        // repr�sente les octets ou commence et finis la conversion
        private int m_iByteConvertFrom = -1;
        private int m_iByteConvertTo = -1;

        // m�thode de calcule du checksum / CRC
        private string m_strDataClcType;
        // taille de la donn�e de control
        private int m_iDataClcSize;
        // index de la donn�e ou commence le calcule de la donn�e de control
        private int m_iDataClcFrom;
        // index de la donn�e ou se termine le calcule de la donn�e de control
        private int m_iDataClcTo;

        // buffer contenant l'ensemble du header de la trame
        // le header est d�fini a partir de la premi�re donn�e de la trame si elle est constante
        // jusqu'a la dernier donn�e constante rencontr�e
        // il permet lors de la r�c�ption des trames de tester les trames re�ues pour "piocher la bonne"
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
        // Description: accesseur vers la list des donn�es de la trame
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

        #region Donn�e de control
        //*****************************************************************************************************
        // Description: accesseur sur le type de la donn�e de control
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
        // Description: accesseur sur la taille de la donn�e de control
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
        // Description: accesseur sur l'index de d�but du calcul de la donn�e de control
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
        // Description: accesseur sur l'index de fin du calcul de la donn�e de control
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
        // Description: accesseur sur l'index de d�but de la conversion
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
        // Description: Lit les donn�es de l'objet a partir de son noeud XML
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
                    // en cas de tag non reconne dans l'enum, une exeption est lev�e, 
                    // on la r�cup�re car ca peut arriver
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
        // Description: ecrit les donn�es de l'objet a partir de son noeud XML
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
            // noeud donn�e de control
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
            // noeud propri�t� de conversion
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
        // Description: termine la lecture de l'objet. utilis� en mode Commande pour r�cup�rer les r�f�rence
        // vers les objets utilis�s
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

            // cette premi�re passe permet d'initialiser le buffer contenant le header de la trame
            int Taille = GetTrameSizeInByte();
            // cr�e le buffer
            Byte[] buffer = new Byte[Taille];
            // on initialise le buffer
            for (int i = 0; i < buffer.Length; i++)
                buffer[i] = 0;

            CalcBeginAndEndConversion();
            //certaines choses sont initialis�e par ces deux fonctions, comme le hearder de trame
            PrepareSendBuffer(buffer);
            TreatWriteConversion(buffer);
            return true;
        }
        #endregion

        #region Gestion des AppMessages
        //*****************************************************************************************************
        // Description: effectue les op�ration n�cessaire lors de la r�c�ption d'un message
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

        #region fonction utilis�es dans SmartCommand pour cr�er et lire les trames
        //*****************************************************************************************************
        // Description: cr�e la trame a envoyer a partir des donn�es qu'elle contien et renvoie le buffer
        // Return: /
        //*****************************************************************************************************
        public Byte[] CreateTrameToSend()
        {
            // Obtenir la taille totale en octets
            int Taille = GetTrameSizeInByte();
            // cr�e le buffer
            Byte[] buffer = new Byte[Taille];
            // on initialise le buffer
            for (int i = 0; i < buffer.Length; i++)
                buffer[i] = 0;

            //la donn�e de control est calcul� dans cette fonction juste avant d'�tre plac� dans la trame
            PrepareSendBuffer(buffer);
            // traiter la conversion
            buffer = TreatWriteConversion(buffer);
            return buffer;
        }

        //*****************************************************************************************************
        // Description: a partir d'une trame donn�e, met a jour les valeurs des donn�es
        // effectue les controls sur la taille de la trame et la donn�e de control
        // Return: /
        //*****************************************************************************************************
        public bool TreatRecieveTrame(Byte[] buffer)
        {
            // v�rifier que les tailles sont identiques
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

            // v�rifier la coh�rence du checksum
            if (!CheckCtrlData(buffer))
                return false;
            
            // relire les donn�e de la trame
            if (!ReadRecievedBuffer(buffer))
                return false;
            else
                return true;
        }

        //*****************************************************************************************************
        // Description: effectue la premi�re �tape de cr�ation de la trame
        // en prenant les donn�es une par une, reconstruit la trame sous forme de donn�es binaires.
        // si une donn�e de control existe, celle ci est calcul�e avant d'�tre assign�e dans la trame
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
                // -1 car l'index des bits est bas� a 0;
                int EndData = (CurrentDataPos - 1) + DataSz;
                int byteBegin = BeginData / Cste.SIZE_1_BYTE;
                int byteEnd = EndData / Cste.SIZE_1_BYTE;

                #region Traitement de la donn�e de controle
                // on se fou du fait qu'on utilise une donn�e de control ou non
                // car si on ne l'utilise pas, ses valeurs ne seront pas utilis�es
                if (i == m_iDataClcFrom)
                {
                    ByteWhereBeginDataCtrl = byteBegin;
                }
                if (i == m_iDataClcTo)
                {
                    ByteWhereEndDataCtrl = byteEnd;
                }

                // si il doit y avoir donn�e de control, et qu'on est dessus,
                // alors on la calcul avant de la positionner

                if (m_strDataClcType != CTRLDATA_TYPE.NONE.ToString() && dat.Symbol.EndsWith(Cste.STR_SUFFIX_CTRLDATA)) 
                {
                    // en toutes logique la donn�e de control ne doit pas etre positionn�e n'importe comment
                    int leftOffset = CurrentDataPos % Cste.SIZE_1_BYTE;
                    if (leftOffset != 0)
                        System.Diagnostics.Debug.Assert(false, "erreur sur la position de la donn�e de control");

                    Byte[] tempBuffer = new Byte[(ByteWhereEndDataCtrl - ByteWhereBeginDataCtrl)+1];
                    int tempIndex = 0;
                    for (int k = ByteWhereBeginDataCtrl; k <= ByteWhereEndDataCtrl; k++)
                    {
                        tempBuffer[tempIndex] = buffer[k];
                        tempIndex ++;
                    }
                    CalcCtrlData(tempBuffer, dat);
                }
                #endregion // traitement de la donn�e de control

                int DataValue = dat.Value;

                // si le header de trame n'est encore cr�e, regarde (en octet) sa taille (non convertie)
                if (m_FrameHeader == null)
                {
                    // si on a pos encore �t� coup� par une donn� non constante
                    if (!bNonConstantDataFound && dat.IsConstant)
                    {
                        // la taille = la taille de l'octet de fin de la donn�e
                        iTempHeaderLenght = byteEnd;
                    }
                    else
                        bNonConstantDataFound = true;
                }
                // la fin dans la conversion...

                //si la donn�e n'est pas comprise sur une seul octet, on la r�partie sur la place qu'elle doit prendre
                if (byteBegin != byteEnd)
                {
                    #region traitement de la r�partition de la donn�e sur plusieurs octets
                    //on calcule le nombres de coupures
                    // pour une donn�e 32 bits on peut �tre a cheval sur 5 octets
                    int nbCut = byteEnd - byteBegin;
                    int NextByteBegin = ((byteEnd + 1) * Cste.SIZE_1_BYTE);
                    int CurrentPosInBit = BeginData;
                    int CurrentByte = byteBegin;
                    int bitsWriten = 0;

                    for (int j = 0; j <= nbCut; j++)
                    {
                        //on est au d�but de la donn�e, on peux avoir a d�caler la valeur vers la droite
                        // on ne prend que les derniers bits de la donn�e
                        int TmpValue  = 0;
                        if (CurrentByte == byteBegin)
                        {
                            int leftOffset = CurrentPosInBit % Cste.SIZE_1_BYTE;
                            byte mask1 = (byte)(0xFF >> leftOffset);
                            int nbAvailableBits = Cste.SIZE_1_BYTE - leftOffset;
                            // on ne prend que le nombre de bits qu'on peu �crire sur la donn�e
                            TmpValue = DataValue >> (DataSz - nbAvailableBits);
                            TmpValue = TmpValue & mask1;
                            buffer[CurrentByte] |= (byte)TmpValue;
                            CurrentPosInBit += nbAvailableBits;
                            bitsWriten = nbAvailableBits;
                        }
                        // on est a la fin de la donn�e, on peut avoir a d�caler la valeur vers la gauche
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
                        // on est "au mileu", on prend juste les 8 bits a �crire
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
                    #region traitement du positionement de la donn�e sur un seul octet
                    // on calcule le d�callage de la donn�e a gauche par rapport a sa place
                    int leftOffset = BeginData % Cste.SIZE_1_BYTE;
                    // on calcule le d�callage de droite par rapport au prochain octet 
                    // (d'ou le byteEnd+1)
                    int rightOffset = ((byteEnd + 1) * Cste.SIZE_1_BYTE) - (CurrentDataPos + DataSz);
                    // on cr�e deux masques compl�mentaires
                    // mask1 s'occuper de "couper" ce qui est a gauche
                    // mask2 s'occuper de "couper" ce qui est droite
                    byte mask1 = (byte)(0xFF >> (BeginData % Cste.SIZE_1_BYTE));
                    byte mask2 = (byte)(0xFF << rightOffset);
                    // on cr�e le mask complet
                    byte Mask = (byte)(mask1 & mask2);
                    // on obtien une donn�e ou les seul bits qui peuvent etre a 1,
                    // sont ceux qui repr�sentent la valeur dans l'octet de la trame;
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
        // Description: a partir d'une trame binaire, relis les donn�es qui y sont contenu
        //              les donn�es constantes ne sont pas modifi�es
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
                // -1 car l'index des bits est bas� a 0;
                int EndData = (CurrentDataPos - 1) + DataSz;
                int byteBegin = BeginData / Cste.SIZE_1_BYTE;
                int byteEnd = EndData / Cste.SIZE_1_BYTE;

                int DataValue = 0;
                //si la donn�e n'est pas comprise sur une seul octet, on la r�partie sur la place qu'elle doit prendre
                if (byteBegin != byteEnd)
                {
                    #region traitement de la r�partition de la donn�e sur plusieurs octets
                    //on calcule le nombres de coupures
                    // pour une donn�e 32 bits on peut �tre a cheval sur 5 octets
                    int nbCut = byteEnd - byteBegin;
                    int NextByteBegin = ((byteEnd + 1) * Cste.SIZE_1_BYTE);
                    int CurrentPosInBit = BeginData;
                    int CurrentByte = byteBegin;
                    int bitsRead = 0;
                    // la valeur temporaire est un entier car c'est la taille de donn�e la plus grande qu'on peu avoir
                    for (int j = 0; j <= nbCut; j++)
                    {
                        // on est au d�but de la donn�e, on prend les bits qui nous interresse a l'index courant
                        // su buffer, et on les met au bon endroit dans la donn�e
                        int TmpValue = 0;
                        if (CurrentByte == byteBegin)
                        {
                            int leftOffset = CurrentPosInBit % Cste.SIZE_1_BYTE;
                            byte mask1 = (byte)(0xFF >> leftOffset);
                            int nbAvailableBits = Cste.SIZE_1_BYTE - leftOffset;
                            // on ne prend que le nombre de bits qu'on peu lire sur la donn�e
                            TmpValue = buffer[CurrentByte];
                            TmpValue = TmpValue >> leftOffset;
                            TmpValue = TmpValue & mask1;
                            CurrentPosInBit += nbAvailableBits;
                            bitsRead = nbAvailableBits;
                            DataValue |= TmpValue << (DataSz - nbAvailableBits);
                        }
                        // on est a la fin de la donn�e, on peut avoir a d�caler la valeur vers la gauche
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
                        // on est "au mileu", on prend juste les 8 bits a �crire
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
                    #region traitement d'une donn�e position�e sur un seul octet
                    // on calcule le d�callage de la donn�e a gauche par rapport a sa place
                    int leftOffset = BeginData % Cste.SIZE_1_BYTE;
                    // on calcule le d�callage de droite par rapport au prochain octet 
                    // (d'ou le byteEnd+1)
                    int rightOffset = ((byteEnd + 1) * Cste.SIZE_1_BYTE) - (CurrentDataPos + DataSz);
                    // on cr�e deux masques compl�mentaires
                    // mask1 s'occuper de "couper" ce qui est a gauche
                    // mask2 s'occuper de "couper" ce qui est droite
                    byte mask1 = (byte)(0xFF >> (BeginData % Cste.SIZE_1_BYTE));
                    byte mask2 = (byte)(0xFF << rightOffset);
                    // on cr�e le mask complet
                    byte Mask = (byte)(mask1 & mask2);
                    // on extrait du buffer les bits qui nous interessent
                    byte byteVal = (byte)(buffer[byteBegin] & Mask);
                    // on remet les bits extraits � leur place
                    DataValue = (byte)(byteVal >> rightOffset);
                    #endregion
                }
                CurrentDataPos += DataSz;
                // une valeur constance ne peux pas �tre assign�e
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
        // Description: traite la conversion de la trame apr�s qu'elle ai �t� cr�e avec PrepareSendBuffer(...)
        // Return: /
        //*****************************************************************************************************
        private Byte[] TreatWriteConversion(Byte[] buffer)
        {
            Byte[] FinalBuffer;
            // facteur de conversion: 1 = pas d'octets suppl�mentaire
            // 2= chaque octet converti prend deux octets
            // etc...
            //int CurrentDataPos = 0;
            //int ByteWhereBeginConvert = 0;
            //int ByteWhereEndConvert = 0;

            // +1 car la premi�re donn�e est comprise, et la derni�re aussi
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
            // si on a pas encore initialis� le header
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
        // Description: traite la conversion de la trame avant qu'elle soit trait�e par ReadRecievedBuffer(...)
        // Return: /
        //*****************************************************************************************************
        private Byte[] TreatReadConversion(Byte[] buffer)
        {
            Byte[] FinalBuffer;
            // facteur de conversion: 1 = pas d'octets suppl�mentaire
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
                // -1 car l'index des bits est bas� a 0;
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

            // +1 car la premi�re donn�e est comprise, et la derni�re aussi
            FinalBuffer = new Byte[GetTrameSizeInByte()];
            int NbByteToConvert = (ByteWhereEndConvert - ByteWhereBeginConvert) + 1;

            //dans le cas decimal la conversion en lecture, l'octet de fin de conversion est �gal a
            // l'octet de d�but + le nombre d'octets a convertir fois le facteur de conversion
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
                        System.Diagnostics.Debug.Assert(i < buffer.Length, "D�passement de buffer dans la conversion lecture de trame");
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
                // -1 car l'index des bits est bas� a 0;
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

            // +1 car la premi�re donn�e est comprise, et la derni�re aussi
            int NbByteToConvert = (ByteWhereEndConvert - ByteWhereBeginConvert) + 1;
            int FinalSize = sizeInBytes + (NbByteToConvert * (ConversionFactor - 1));
            return FinalSize;
        }

        //*****************************************************************************************************
        // Description: calcule la donn�e de control "dat" a partir des octets pass�s dans "buffer"
        //              le buffer ne dois contenir que les octets qui sont utilis� pour le calcule de la donn�e de control
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
            else // pas pr�vu
                System.Diagnostics.Debug.Assert(false, "Type de calcul de donn�e de control pas cod�");
        }

        //*****************************************************************************************************
        // Description: En lecture, avant de commencer a relire les valeurs, on v�rifie la valididt�e de la donn�e de control
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
                // -1 car l'index des bits est bas� a 0;
                int EndData = (CurrentDataPos - 1) + DataSz;
                int byteBegin = BeginData / Cste.SIZE_1_BYTE;
                int byteEnd = EndData / Cste.SIZE_1_BYTE;

                #region Traitement de la donn�e de controle
                // on se fou du fait qu'on utilise une donn�e de control ou non
                // car si on ne l'utilise pas, ses valeurs ne seront pas utilis�es
                if (i == m_iDataClcFrom)
                {
                    ByteWhereBeginDataCtrl = byteBegin;
                }
                if (i == m_iDataClcTo)
                {
                    ByteWhereEndDataCtrl = byteEnd;
                }

                // si il doit y avoir donn�e de control, et qu'on est dessus,
                // alors on la calcul avant de la positionner
                // dans le cas de la lecture, on la calcule pr v�rifer si elle correspond bien a celle re�ue
                if (m_strDataClcType != CTRLDATA_TYPE.NONE.ToString() && dat.Symbol.EndsWith(Cste.STR_SUFFIX_CTRLDATA))
                {
                    // en toutes logique la donn�e de control ne doit pas etre positionn�e n'importe comment
                    int leftOffset = CurrentDataPos % Cste.SIZE_1_BYTE;
                    if (leftOffset != 0)
                        System.Diagnostics.Debug.Assert(false, "erreur sur la position de la donn�e de control");

                    Byte[] tempBuffer = new Byte[(ByteWhereEndDataCtrl - ByteWhereBeginDataCtrl) + 1];
                    int tempIndex = 0;
                    for (int k = ByteWhereBeginDataCtrl; k <= ByteWhereEndDataCtrl; k++)
                    {
                        tempBuffer[tempIndex] = buffer[k];
                        tempIndex++;
                    }
                    // on calcule la donn�e de control directement dans la donn�e
                    CalcCtrlData(tempBuffer, dat);
                    ControlData = dat;
                    bCtrlDataHaveBeenCalc = true;
                    // on relis la valeur de la donn�e de control a partir du buffer dans un variable temporaire
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
            // si la donn�e de control a �t� calcul�e, elle peu ne pas l'avoir �t� losqu'il n'y en a pas
            // au quel cas on renvoie toujours vrai
            if (bCtrlDataHaveBeenCalc)
            {
                // incoh�rence de la donn�e de control
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
                // -1 car l'index des bits est bas� a 0;
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
        // Description: convertir une donn�e binaire en ascii
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
        // Description: concertie la donn�e ascii en binaires
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

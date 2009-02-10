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
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.IO;
using System.Windows.Forms;

namespace CommonLib
{
    public class SerialComm : BaseComm
    {
        #region donn�es membres
        private SerialPort m_PortSerie;
        List<byte[]> m_MessageList = new List<byte[]>();
        #endregion

        #region Events
        public event CommOpenedStateChange OnCommStateChange;
        #endregion

        #region cosntructeurs
        /////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// constructeur de la classe
        /// initialise par d�faut le port comm sur le port COM6(port BlueTooth PPC)
        /// a 57600 bauds, 7 bits de donn�e, parit�e paire
        /// </summary>
        public SerialComm()
        {
            m_PortSerie = new SerialPort();
            // conf par d�faut
            m_PortSerie.PortName = "COM6";
            m_PortSerie.BaudRate = (int)SERIAL_BAUD_RATE.BR_115200;
            m_PortSerie.DataBits = (int)NB_DATA_BITS.NB_DB7;
            m_PortSerie.DtrEnable = true;
            m_PortSerie.Parity = Parity.Even;
            m_PortSerie.ParityReplace = ((byte)(48));
            m_PortSerie.StopBits = StopBits.One;
            m_PortSerie.ReadTimeout = 5000;
            m_PortSerie.WriteTimeout = 5000;
            m_PortSerie.DataReceived += new SerialDataReceivedEventHandler(this.DataReceived);
            m_PortSerie.ErrorReceived += new SerialErrorReceivedEventHandler(this.ErrorReceived);

            Traces.LogAddDebug("Configuration du port com", "");
            Traces.LogAddDebug("Bits de donn�e", m_PortSerie.DataBits.ToString());
            Traces.LogAddDebug("DTR", m_PortSerie.DtrEnable.ToString());
            Traces.LogAddDebug("Bits de stop", m_PortSerie.StopBits.ToString());
            Traces.LogAddDebug("Read Timeout", m_PortSerie.ReadTimeout.ToString());
            Traces.LogAddDebug("Write Timeout", m_PortSerie.WriteTimeout.ToString());

            m_bDataAvailable = false;
            m_CommErrorCode = COMM_ERROR.ERROR_NONE;
        }
        #endregion

        #region attributs
        /////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// attribut permettant de d'assigner ou connaitre le nom du port configur�e sur le port
        /// </summary>
        public string ComPort
        {
            set
            {
                m_PortSerie.PortName = value;
                Traces.LogAddDebug("Port COM", value);
            }
            get
            {
                return m_PortSerie.PortName;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// attribut permettant de d'assigner ou connaitre le BaudRate configur�e sur le port
        /// </summary>
        public SERIAL_BAUD_RATE BaudRate
        {
            get
            {
                return (SERIAL_BAUD_RATE)m_PortSerie.BaudRate;
            }
            set
            {
                m_PortSerie.BaudRate = (int)value;
                Traces.LogAddDebug("BaudRate", value.ToString());
            }
        }

        /////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// attribut permettant de d'assigner ou connaitre la parit�e configur�e sur le port
        /// </summary>
        public Parity Parity
        {
            get
            {
                return m_PortSerie.Parity;
            }
            set
            {
                m_PortSerie.Parity = value;
                Traces.LogAddDebug("Parit�e", value.ToString());
            }
        }
        #endregion

        #region m�thodes publiques
        /////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// ouvre le port de communication
        /// </summary>
        /// <returns> vrai si le port a bien �t� ouvert</returns>
        public override bool OpenComm()
        {
            try
            {
                if (!m_PortSerie.IsOpen)
                    m_PortSerie.Open();
            }
            catch (Exception)
            {
                string str = "Unable to open comm, Check connection configuration";
                DialogResult dlgRes = MessageBox.Show(str,
                    "Error opening comm", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);

            }

            if (m_PortSerie.IsOpen)
            {
                if (OnCommStateChange != null)
                    OnCommStateChange();

                return true;
            }
            else
                return false;
        }

        /////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// ferme le port de communication
        /// </summary>
        /// <returns> vrai si le port est bien ferm�</returns>
        public override bool CloseComm()
        {
            m_PortSerie.Close();
            if (OnCommStateChange != null)
                OnCommStateChange();

            return true;
        }

        /////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// v�rifie l'�tat d'ouverture du port s�rie
        /// </summary>
        /// <returns> vrai si le port de comm est ouvert</returns>
        public override bool IsOpen()
        {
            return m_PortSerie.IsOpen;
        }

        /////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Envoie les donn�es contenues dans le buffer pass� en param�tres
        /// </summary>
        /// <param name="buffer">Buffer contenant les donn�es a envoyer</param>
        /// <returns>renvoie vrai si les donn�es ont �t� envoy�es</returns>
        public override bool SendData(Byte[] buffer)
        {
            bool bReturnValue = true;
            try
            {
                if (m_PortSerie.IsOpen)
                {
                    // on vide les buffers
                    m_PortSerie.DiscardInBuffer();
                    m_PortSerie.DiscardOutBuffer();
                    m_PortSerie.Write(buffer, 0, buffer.Length);
                }
                bReturnValue = false;
            }
            catch (TimeoutException te)
            {
                m_CommErrorCode = COMM_ERROR.ERROR_TIMEOUT;
                bReturnValue = false;
                Traces.LogAddDebug("SendData", "Erreur timeour exception : " + te.Message + "\n" + te.StackTrace);
            }
            catch (IOException ioe)
            {
                m_CommErrorCode = COMM_ERROR.ERROR_UNKNOWN;
                bReturnValue = false;
                Traces.LogAddDebug("SendData", "Erreur IO exception : " + ioe.Message + "\n" + ioe.StackTrace);
            }
            catch (Exception e)
            {
                m_CommErrorCode = COMM_ERROR.ERROR_UNKNOWN;
                bReturnValue = false;
                Traces.LogAddDebug("SendData", "Erreur exception : " + e.Message + "\n" + e.StackTrace);
            }
            if (!bReturnValue)
            {
                // on vide les buffers
                m_PortSerie.DiscardInBuffer();
                m_PortSerie.DiscardOutBuffer();
            }
            return bReturnValue;
        }

        /////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// renvoie les donn�es re�ues par le port s�rie
        /// (renvoie null si aucune donn�e n'a �t� re�u au moment de l'appel a cette fonction
        /// </summary>
        /// <returns>un tableau de Byte contenant les donn�es re�ues</returns>
        public override Byte[] GetRecievedData(int NumberOfByte, byte[] FrameHeader)
        {
            if (m_PortSerie.IsOpen && m_bDataAvailable)
            {
                Traces.LogAddDebug("GetRecievedData", "Nombre de messages dans la file = " + m_MessageList.Count);
                if (m_MessageList.Count != 0)
                {
                    // on parcour la liste des trames re�ues pour vois si par hasard on en aurai
                    // pas une de la bonne taille
                    int indexOfFrame = -1;
                    for (int i = 0; i < m_MessageList.Count; i++)
                    {
                        Traces.LogAddDebug("GetRecievedData", "Longueur de la trame n�" + i + " dans la file = " + m_MessageList[i].Length);
                        Traces.LogAddDebug("GetRecievedData", "Longueur de la trame attendue = " + NumberOfByte);
                        if (m_MessageList[i].Length == NumberOfByte)
                        {
                            indexOfFrame = i;
                            Traces.LogAddDebug("GetRecievedData", "Trame attendu trouv�e");
                            break;
                        }
                    }
                    // si on en a une, on la renvoie
                    if (indexOfFrame != -1)
                    {
                        byte[] buf = m_MessageList[indexOfFrame];
                        m_MessageList.RemoveAt(indexOfFrame);
                        if (m_MessageList.Count == 0)
                            m_bDataAvailable = false;
                        return buf;
                    }
                }
            }
            return null;
        }

        //*****************************************************************************************************
        // Description: 
        // Return: /
        //*****************************************************************************************************
        public override bool TestFrame(int FrameLenght, byte[] FrameHeader)
        {
            if (m_MessageList.Count != 0)
            {
                // on parcour la liste des trames re�ues pour vois si par hasard on en aurai
                // pas une de la bonne taille
                // si on en a une de la bonne taille, on v�rifie le header
                int indexOfFrame = -1;
                try
                {
                    Traces.LogAddDebug("TestFrame", "Nombre de messages dans la file = " + m_MessageList.Count);
                    for (int i = 0; i < m_MessageList.Count; i++)
                    {
                        Traces.LogAddDebug("TestFrame", "Longueur de la trame " + i + " dans la file = " + m_MessageList[i].Length);
                        Traces.LogAddDebug("TestFrame", "Longueur de la trame attendue = " + FrameLenght);
                        if (m_MessageList[i].Length == FrameLenght)
                        {
                            if (FrameHeader == null)
                            {
                                indexOfFrame = i;
                                break;
                            }
                            else
                            {
                                // on copare le header
                                bool FrameOk = true;
                                for (int indexHeader = 0; indexHeader < FrameHeader.Length; indexHeader++)
                                {
                                    if (FrameHeader[indexHeader] != m_MessageList[i][indexHeader])
                                    {
                                        FrameOk = false;
                                        break;
                                    }
                                }
                                if (FrameOk)
                                {
                                    indexOfFrame = i;
                                    break;
                                }
                            }
                        }
                    }
                    // si on en a une, on la renvoie
                    if (indexOfFrame != -1)
                    {
                        return true;
                    }
                    else
                    {
                        Traces.LogAddDebug("TestFrame", "Aucune trame correspondante");
                        Traces.LogAddDebug("TestFrame", "Dump des trames");
                        for (int i = 0; i < m_MessageList.Count; i++)
                        {
                            string trame = "";
                            for (int t = 0; t < m_MessageList[i].Length; t++ )
                            {
                                trame += m_MessageList[i][t];
                                trame += " ";
                            }
                            Traces.LogAddDebug("TestFrame", "trame" + i + " = "+ trame);
                        }
                    }
                }
                catch (NullReferenceException e)
                {
                    Console.WriteLine("========================================");
                    Console.WriteLine("* Erreur Dans Serial Comm TestFrame  *");
                    Console.WriteLine("{0}, Message Count = {1}", e.Message, m_MessageList.Count);
                    Console.WriteLine("========================================");
                }

            }
            return false;
        }
        #endregion

        #region m�thodes priv�es
        /////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Callback appel� lorsque des donn�es sont re�ues sur le port s�rie
        /// </summary>
        /// <param name="sender">objet ayant appel� la callback</param>
        /// <param name="e">Param�tres de r�c�ptions</param>
        private void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            bool bSuccess = false;
            try
            {
                string strDataRecieved = "";
                bool bFullTrameRecieved = false;
                bool bBeginFrameRecieved = false;
                bool bEndFrameRecieved = false;
                int count = 0;
                while (!bFullTrameRecieved && count < 1000)
                {
                    strDataRecieved += m_PortSerie.ReadExisting();
                    if (!bBeginFrameRecieved && strDataRecieved.Contains(":"))
                        bBeginFrameRecieved = true;

                    if (!bEndFrameRecieved && strDataRecieved.EndsWith("\r\n"))
                        bEndFrameRecieved = true;

                    if (bBeginFrameRecieved && bEndFrameRecieved)
                        bFullTrameRecieved = true;

                    System.Threading.Thread.Sleep(0);
                }
                Byte[] buffer = new Byte[strDataRecieved.Length];
                for (int i = 0; i < strDataRecieved.Length; i++)
                {
                    buffer[i] = (byte)strDataRecieved[i];
                }
                m_MessageList.Add(buffer);
                bSuccess = true;
                m_bDataAvailable = true;
            }
            catch (Exception)
            {
                bSuccess = false;
            }
            if (!bSuccess)
            {
                // on vide les buffers
                m_bDataAvailable = false;
                m_PortSerie.DiscardInBuffer();
                m_PortSerie.DiscardOutBuffer();
            }
        }

        /////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Callback appel� lorsqu'une erreur se produit sur le port s�rie
        /// </summary>
        /// <param name="sender">objet ayant appel� la callback</param>
        /// <param name="e">Param�tres de l'erreur</param>
        private void ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            switch (e.EventType)
            {
                case SerialError.RXOver:
                    m_CommErrorCode = COMM_ERROR.ERROR_RECIEVED_DATA;
                    break;
                case SerialError.RXParity:
                    m_CommErrorCode = COMM_ERROR.ERROR_PARITY;
                    break;
                case SerialError.TXFull:
                    m_CommErrorCode = COMM_ERROR.ERROR_SEND_DATA;
                    break;
                default:
                case SerialError.Frame:
                case SerialError.Overrun:
                    m_CommErrorCode = COMM_ERROR.ERROR_UNKNOWN;
                    break;
            }
            // on vide les buffers du port
            m_PortSerie.DiscardInBuffer();
            m_PortSerie.DiscardOutBuffer();

            if (m_PortSerie.IsOpen == false && OnCommStateChange != null)
                OnCommStateChange();

        }
        #endregion
    }
}

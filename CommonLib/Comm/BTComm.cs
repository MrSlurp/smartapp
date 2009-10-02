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
using System.Windows.Forms;
using System.Threading;


namespace CommonLib
{
    #region ENUM POUR LIAISON SERIE
    // enum used for serial link comm
    // Baud Rate used by serial comm
    public enum SERIAL_BAUD_RATE
    {
        BR_115200 = 115200,
        BR_57600 = 57600,
        BR_19200 = 19200,
        BR_9600 = 9600,
    }

    // enum used for serial link comm
    // number of data bit used by serial comm
    public enum NB_DATA_BITS
    {
        NB_DB7 = 7,
        NB_DB8 = 8,
    }
    #endregion

    #region enum des types de comm
    public enum TYPE_COMM
    {
        // communication de type liaison s�rie ou bluetooth
        SERIAL,
        // communication de type Http, (le jour  ou je ferai un serveur HTTP)
        HTTP,
        // communication de type TCP, pour le TCP modbus ou autre
        ETHERNET,
        // connexion virtuel pour test des fichiers de configuration
        VIRTUAL,
    }
    #endregion

    //*****************************************************************************************************
    // Description: classe g�rant la communication au travers de diff�rents protocoles
    //*****************************************************************************************************
    public class BTComm : Object
    {
        #region D�claration des donn�es de la classe
        /// <summary>
        /// peut etre soit un port com (COM1, COM2...) 
        /// soit une adresse IP (192.168.1.1)
        /// soit un nom r�solu par un DNS 
        /// </summary>
        private string m_strDestAdress;

        private BaseComm m_Comm;

        private TYPE_COMM m_TypeComm;

        private System.Windows.Forms.Timer m_TimerRecieveTimeout;

        #endregion

        #region Events
        public event CommOpenedStateChange OnCommStateChange;
        public event AddLogEventDelegate EventAddLogEvent;
        #endregion

        #region constructeur
        /// <summary>
        /// Constructeur par d�faut, initialise les param�tres pour une communication �thernet
        /// </summary>
        public BTComm()
        {
            // init par d�faut
            m_TypeComm = TYPE_COMM.ETHERNET;
            m_strDestAdress = "192.168.0.99:502";
            m_Comm = new EthernetComm();
            m_Comm.EventAddLogEvent += new AddLogEventDelegate(AddLogEvent);
            ((EthernetComm)m_Comm).IpAddr = "192.168.0.99";
            ((EthernetComm)m_Comm).Port = 502;
            ((EthernetComm)m_Comm).OnCommStateChange += new CommOpenedStateChange(ConnectionStateChangeEvent);

            m_TimerRecieveTimeout = new System.Windows.Forms.Timer();
            m_TimerRecieveTimeout.Interval = 5000;
            m_TimerRecieveTimeout.Tick += new EventHandler(this.OnRecieveTimeOut);
        }
        #endregion

        #region attributs
        /// <summary>
        /// attribut en lecture seule renvoyant les param�tres de comm (adresse/port)
        /// </summary>
        public string CommParam
        {
            get
            {
                return m_strDestAdress;
            }
        }

        /// <summary>
        /// attribut en lecture seule renvoyant le type de comm
        /// </summary>
        public TYPE_COMM CommType
        {
            get
            {
                return m_TypeComm;
            }
        }

        /// <summary>
        /// permet de connaitre l'�tat actuel de la comm
        /// </summary>
        public bool IsOpen
        {
            get
            {
                return m_Comm.IsOpen();
            }
        }

        /// <summary>
        /// renvoie le code d'erreur courant
        /// </summary>
        public COMM_ERROR ErrorCode
        {
            get
            {
                return m_Comm.ErrorCode;
            }
        }
        #endregion

        #region methodes publiques
        /// <summary>
        /// permet de d�finir le type de comm avec ses param�tres
        /// initialise la comm en fontion du type et des param�tres
        /// </summary>
        /// <param name="CommType">Type de communication</param>
        /// <param name="strParam">param�tres de la communication</param>
        /// <returns>true si les param�tres sont valides</returns>
        public bool SetCommTypeAndParam(TYPE_COMM CommType, string strParam)
        {
            if (!this.IsOpen)
            {
                if (string.IsNullOrEmpty(strParam))
                    return false;

                m_TypeComm = CommType;

                switch (m_TypeComm)
                {
                    case TYPE_COMM.SERIAL:
                        m_Comm = new SerialComm();
                        m_Comm.EventAddLogEvent += new AddLogEventDelegate(AddLogEvent);
                        ((SerialComm)m_Comm).OnCommStateChange += new CommOpenedStateChange(ConnectionStateChangeEvent);
                        m_strDestAdress = strParam;
                        ((SerialComm)m_Comm).ComPort = m_strDestAdress;
                        break;
                    case TYPE_COMM.ETHERNET:
                        m_Comm = new EthernetComm();
                        m_Comm.EventAddLogEvent += new AddLogEventDelegate(AddLogEvent);
                        ((EthernetComm)m_Comm).OnCommStateChange += new CommOpenedStateChange(ConnectionStateChangeEvent);
                        m_strDestAdress = strParam;
                        string[] strs = strParam.Split(':');
                        if (strs.Length != 2)
                            return false;
                        string strDestAdress = strs[0];
                        int port = int.Parse(strs[1]);
                        ((EthernetComm)m_Comm).IpAddr = strDestAdress;
                        ((EthernetComm)m_Comm).Port = port;
                        break;
                    case TYPE_COMM.VIRTUAL:
                        m_Comm = new VirtualComm();
                        ((VirtualComm)m_Comm).OnCommStateChange += new CommOpenedStateChange(ConnectionStateChangeEvent);
                        // aucun param�tres
                        break;
                    case TYPE_COMM.HTTP:
                    default:
                        // TODO �volution
                        System.Diagnostics.Debug.Assert(false);
                        return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Ouvre la communication courante
        /// </summary>
        /// <returns>true en cas de succ�s</returns>
        public bool OpenComm()
        {
            if (m_Comm.IsOpen())
                return true;

            WaitOpenCommForm WaitForm = new WaitOpenCommForm();
            //WaitForm.Parent = Program.CurrentMainForm;
            WaitForm.Show();
            Application.DoEvents();
            if (m_Comm.OpenComm())
            {
                WaitForm.Close();
                return true;
            }
            else
            {
                WaitForm.Close();
                return false;
            }
        }

        /// <summary>
        /// Ferme la communication courante
        /// </summary>
        /// <returns>true en cas de succ�s</returns>
        public bool CloseComm()
        {
            if (!m_Comm.IsOpen())
                return true;

            if (m_Comm.CloseComm())
                return true;
            else
                return false;
        }

        /// <summary>
        /// Envoie les donn�es du buffer sur la comm courante
        /// </summary>
        /// <param name="buffer">buffer des donn�es � envoyer</param>
        /// <returns>true en cas de succ�s</returns>
        public bool SendData(Byte[] buffer)
        {
            m_Comm.ResetError();
            if (m_Comm.SendData(buffer))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Envoie les donn�es du buffer sur le port de comm courant
        /// sp�ciale communication virtuelle
        /// </summary>
        /// <param name="TrameToSend">Objet trame � envoyer</param>
        /// <param name="DataGest">gestionnaire de donn�es</param>
        /// <param name="VirtualDataGest">gestionnaire de donn�es virtuelles</param>
        /// <returns></returns>
        public bool SendData(Trame TrameToSend, GestData DataGest, GestDataVirtual VirtualDataGest)
        {
            if (m_Comm.GetType() == typeof(VirtualComm))
                ((VirtualComm)m_Comm).SendData(TrameToSend, DataGest, VirtualDataGest);

            return true;
        }

        /// <summary>
        /// R�alise l'attente de r�c�ption d'un trame
        /// la sortie est automatique en cas de timeout
        /// </summary>
        /// <param name="FrameLenght">longueur de la trame en octet</param>
        /// <param name="FrameHeader">header de la trame</param>
        /// <returns>true si une trame � �t� re�ue</returns>
        public bool WaitTrameRecieved(int FrameLenght, byte[] FrameHeader)
        {
            m_TimerRecieveTimeout.Enabled = true;
            while (!m_Comm.TestFrame(FrameLenght, FrameHeader) && m_Comm.ErrorCode == COMM_ERROR.ERROR_NONE)
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(1);
            }
            m_TimerRecieveTimeout.Enabled = false;
            if (m_Comm.DataRecieved && m_Comm.ErrorCode == COMM_ERROR.ERROR_NONE)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// obtiens les donn�es re�ues sur la comm
        /// </summary>
        /// <param name="NumberOfByte">nombre d'octet � lire</param>
        /// <param name="FrameHeader">header de trame</param>
        /// <returns>donn�es re�ues sur la comm</returns>
        public Byte[] GetRecievedData(int NumberOfByte, byte[] FrameHeader)
        {
            Byte[] buffer = m_Comm.GetRecievedData(NumberOfByte, FrameHeader);
            return buffer;
        }

        /// <summary>
        /// obtiens les donn�es re�ues sur le port comm
        /// version sp�ciale communication virtuelle
        /// </summary>
        /// <param name="ConvertedSize">taille de la trame convertie</param>
        /// <param name="TrameToReturn">objet trame qui doit �tre re�u</param>
        /// <returns>donn�es de la trame re�ue</returns>
        public Byte[] GetRecievedData(int ConvertedSize, Trame TrameToReturn)
        {
            Byte[] buffer = null;
            if (m_Comm.GetType() == typeof (VirtualComm))
                buffer = ((VirtualComm)m_Comm).GetRecievedData(ConvertedSize, TrameToReturn);

            return buffer;
        }
        #endregion

        #region m�thodes priv�es et prot�g�es
        /// <summary>
        /// callback appel� par le timer de timeout
        /// </summary>
        /// <param name="sender">event sender object</param>
        /// <param name="e">event args</param>
        private void OnRecieveTimeOut(object sender, EventArgs e)
        {
            m_Comm.ErrorCode = COMM_ERROR.ERROR_TIMEOUT;
            m_TimerRecieveTimeout.Enabled = false;
        }

        /// <summary>
        /// handler de changement du type de comm
        /// </summary>
        private void ConnectionStateChangeEvent()
        {
            if (OnCommStateChange != null)
            {
                OnCommStateChange();
            }
        }

        /// <summary>
        /// ajoute un �v�nement au logger de SmartCommand
        /// </summary>
        /// <param name="Event"></param>
        protected void AddLogEvent(LogEvent Event)
        {
            if (EventAddLogEvent != null)
            {
                EventAddLogEvent(Event);
            }
        }
        #endregion
    }
}

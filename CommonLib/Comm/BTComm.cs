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
        // communication de type liaison série ou bluetooth
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
    // Description: classe gérant la communication au travers de différents protocoles
    //*****************************************************************************************************
    public class BTComm : Object
    {
        #region Déclaration des données de la classe
        /// <summary>
        /// peut etre soit un port com (COM1, COM2...) 
        /// soit une adresse IP (192.168.1.1)
        /// soit un nom résolu par un DNS 
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
        //*****************************************************************************************************
        // Description: constructeur de la classe
        // Return: /
        //*****************************************************************************************************
        public BTComm()
        {
            // init par défaut
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
        //*****************************************************************************************************
        // Description: attribut en lecture seule renvoyant les paramètres de comm (adresse)
        // Return: /
        //*****************************************************************************************************
        public string CommParam
        {
            get
            {
                return m_strDestAdress;
            }
        }

        //*****************************************************************************************************
        // Description: attribut en lecture seule renvoyant le type de comm
        // Return: /
        //*****************************************************************************************************
        public TYPE_COMM CommType
        {
            get
            {
                return m_TypeComm;
            }
        }

        //*****************************************************************************************************
        // Description: permet de connaitre l'état actuel de la comm
        // Return: /
        //*****************************************************************************************************
        public bool IsOpen
        {
            get
            {
                return m_Comm.IsOpen();
            }
        }

        //*****************************************************************************************************
        // Description: renvoie le code d'erreur courant
        // Return: /
        //*****************************************************************************************************
        public COMM_ERROR ErrorCode
        {
            get
            {
                return m_Comm.ErrorCode;
            }
        }
        #endregion

        #region methodes publiques
        //*****************************************************************************************************
        // Description: permet de définir le type de comm avec ses paramètres
        // initialise la comm en fontion du type et des paramètres
        // Return: /
        //*****************************************************************************************************
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
                        // aucun paramètres
                        break;
                    case TYPE_COMM.HTTP:
                    default:
                        // TODO évolution
                        System.Diagnostics.Debug.Assert(false);
                        return false;
                }
            }
            return true;
        }

        //*****************************************************************************************************
        // Description: Ouvre la communication courante
        // Return: /
        //*****************************************************************************************************
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

        //*****************************************************************************************************
        // Description: Ferme la communication courante
        // Return: /
        //*****************************************************************************************************
        public bool CloseComm()
        {
            if (!m_Comm.IsOpen())
                return true;

            if (m_Comm.CloseComm())
                return true;
            else
                return false;
        }

        //*****************************************************************************************************
        // Description: Envoie les données du buffer sur le port de comm courant
        // Return: /
        //*****************************************************************************************************
        public bool SendData(Byte[] buffer)
        {
            m_Comm.ResetError();
            if (m_Comm.SendData(buffer))
                return true;
            else
                return false;
        }

        //*****************************************************************************************************
        // Description: Envoie les données du buffer sur le port de comm courant 
        // version spéciale communication virtuelle
        // Return: /
        //*****************************************************************************************************
        public bool SendData(Trame TrameToSend, GestData DataGest, GestDataVirtual VirtualDataGest)
        {
            if (m_Comm.GetType() == typeof(VirtualComm))
                ((VirtualComm)m_Comm).SendData(TrameToSend, DataGest, VirtualDataGest);

            return true;
        }

        //*****************************************************************************************************
        // Description: Réalise l'attente de récéption d'un trame
        // la sortie est automatique en cas de timeout
        // Return: /
        //*****************************************************************************************************
        public bool WaitTrameRecieved(int FrameLenght, byte[] FrameHeader)
        {
            m_TimerRecieveTimeout.Enabled = true;
            while (!m_Comm.TestFrame(FrameLenght, FrameHeader) && m_Comm.ErrorCode == COMM_ERROR.ERROR_NONE)
            {
                //nan, car si on le laisse executer les action et qu'une nouvelle demande 
                // d'envoie de trame est postée, on crash
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

        //*****************************************************************************************************
        // Description: obtiens les données reçues sur le port comm
        // Return: /
        //*****************************************************************************************************
        public Byte[] GetRecievedData(int NumberOfByte, byte[] FrameHeader)
        {
            Byte[] buffer = m_Comm.GetRecievedData(NumberOfByte, FrameHeader);
            return buffer;
        }

        //*****************************************************************************************************
        // Description: obtiens les données reçues sur le port comm
        // version spéciale communication virtuelle
        // Return: /
        //*****************************************************************************************************
        public Byte[] GetRecievedData(int ConvertedSize, Trame TrameToReturn)
        {
            Byte[] buffer = null;
            if (m_Comm.GetType() == typeof (VirtualComm))
                buffer = ((VirtualComm)m_Comm).GetRecievedData(ConvertedSize, TrameToReturn);

            return buffer;
        }
        #endregion

        #region méthodes privées et protégées
        //*****************************************************************************************************
        // Description: callback appelé par le timer de timeout
        // Return: /
        //*****************************************************************************************************
        private void OnRecieveTimeOut(object sender, EventArgs e)
        {
            m_Comm.ErrorCode = COMM_ERROR.ERROR_TIMEOUT;
            m_TimerRecieveTimeout.Enabled = false;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void ConnectionStateChangeEvent()
        {
            if (OnCommStateChange != null)
            {
                OnCommStateChange();
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
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

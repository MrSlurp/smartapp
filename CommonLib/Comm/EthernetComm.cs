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
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;


namespace CommonLib
{
    public class EthernetComm : BaseComm
    {
        #region données membres
        private BTTcpClient m_TcpClient;
        //private Byte[] m_DataBuffer;
        private eClientConnectionSate m_ConnectionState;
        bool m_bUserDisconnectDemande = false;

        //Queue<byte[]> m_MessageQueue = new Queue<byte[]>();
        List<byte[]> m_MessageList = new List<byte[]>();
        #endregion

        #region Events
        public event CommOpenedStateChange OnCommStateChange;
        #endregion

        #region constructeurs
        /// <summary>
        /// constructeur par défaut
        /// </summary>
        public EthernetComm()
        {
            m_TcpClient = new BTTcpClient();
            m_TcpClient.ClientDataAvailable += new BTTcpClient.DataAvailable(OnDataRecieved);
            m_TcpClient.ConnectionStateEvent += new BTTcpClient.ConnectionStateHandler(OnConnectionStateChange);
            m_bDataAvailable = false;
            m_CommErrorCode = COMM_ERROR.ERROR_NONE;
            IpAddr = "192.168.0.99";
            Port = 502;
            m_ConnectionState = eClientConnectionSate.Disconnected;
        }
        #endregion

        #region attributs
        /// <summary>
        /// obteint ou assigne l'adresse IP 
        /// </summary>
        public string IpAddr
        {
            get
            {
                return m_TcpClient.Ip;
            }
            set
            {
                IPAddress IpAddr = new IPAddress(0);
                // si le parsing d'adresse IP echoue, on tente une resolution DNS
                if (!IPAddress.TryParse(value, out IpAddr))
                {
                    IPHostEntry hostEntry = Dns.GetHostEntry(value);
                    if (hostEntry.AddressList.Length == 1)
                    {
                        IpAddr = hostEntry.AddressList[0];
                    }
                }
                m_TcpClient.Ip = IpAddr.ToString();
            }
        }

        /// <summary>
        /// port de la connexion
        /// </summary>
        public int Port
        {
            get
            {
                return m_TcpClient.Port;
            }
            set
            {
                m_TcpClient.Port = value;
            }
        }
        #endregion

        #region methodes publiques
        /// <summary>
        /// Envoie les données du buffer sur la comm courante
        /// </summary>
        /// <param name="buffer">buffer des données à envoyer</param>
        /// <returns>true en cas de succès</returns>
        public override bool SendData(Byte[] buffer)
        {
            if (m_TcpClient.IsOpen)
            {
                //m_DataBuffer = null;
                if (Traces.IsLogLevelOK(TracesLevel.Debug))
                {
                    string strSendData = string.Empty; 
                    for (int i = 0; i < buffer.Length; i++)
                    {
                        strSendData += string.Format(" {0:x2}", buffer[i]);
                    }
                    Traces.LogAddDebug(TraceCat.Communication, "SendData", strSendData);
                }
                m_TcpClient.SendData(buffer);
                return true;
            }
            return false;
        }

        /// <summary>
        /// obtiens les données reçues sur la comm
        /// </summary>
        /// <param name="NumberOfByte">nombre d'octet à lire</param>
        /// <param name="FrameHeader">header de trame</param>
        /// <returns>données reçues sur la comm</returns>
        public override Byte[] GetRecievedData(int NumberOfByte, byte[] FrameHeader)
        {
            if (m_TcpClient.IsOpen && m_bDataAvailable)
            {
                if (m_MessageList.Count != 0)
                {
                    // on parcour la liste des trames reçues pour vois si par hasard on en aurai
                    // pas une de la bonne taille
                    int indexOfFrame = -1;
                    for (int i = 0; i < m_MessageList.Count; i++)
                    {
                        if (m_MessageList[i].Length== NumberOfByte)
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

        /// <summary>
        /// test si une trame reçu correspond en longueur et en header.
        /// </summary>
        /// <param name="FrameLenght">longueur de la trame recherchée</param>
        /// <param name="FrameHeader">header de la trame recherchée</param>
        /// <returns>true si une trame correpond</returns>
        public override bool TestFrame(int FrameLenght, byte[] FrameHeader)
        {
            if (m_MessageList.Count != 0)
            {
                // on parcour la liste des trames reçues pour vois si par hasard on en aurai
                // pas une de la bonne taille
                int indexOfFrame = -1;
                for (int i = 0; i < m_MessageList.Count; i++)
                {
                    try
                    {

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
                    catch (NullReferenceException e)
                    {
                        Console.WriteLine("========================================");
                        Console.WriteLine("* Erreur Dans Ethernet Comm TestFrame  *");
                        Console.WriteLine("{0}, Message Count = {1}", e.Message, m_MessageList.Count);
                        Console.WriteLine("========================================");
                    }
                }
                // si on en a une, on la renvoie
                if (indexOfFrame != -1)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Ouvre la communication ethernet
        /// </summary>
        /// <returns>true en cas de succès</returns>
        public override bool OpenComm()
        {
            m_TcpClient.Start();
            if (m_TcpClient.IsOpen)
            {
                /*
                string str = "Connection open";
                DialogResult dlgRes = MessageBox.Show(str,
                                    "", MessageBoxButtons.OK,
                                    MessageBoxIcon.None,
                                    MessageBoxDefaultButton.Button1);
                 * */
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Ferme la communication courante
        /// </summary>
        /// <returns>true en cas de succès</returns>
        public override bool CloseComm()
        {
            m_bUserDisconnectDemande = true;
            m_TcpClient.Stop();
            if (!m_TcpClient.IsOpen)
            {
                m_bUserDisconnectDemande = false;
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// indique si la communication est ouverte
        /// </summary>
        /// <returns>true si elle est ouverte</returns>
        public override bool IsOpen()
        {
            if (m_TcpClient.IsOpen)
                return true;
            else
                return false;
        }
        #endregion

        #region méthodes privées
        /// <summary>
        /// handler appelé lorsque des données sont reçues
        /// </summary>
        /// <param name="sender">objet ayant envoyé l'évènement</param>
        /// <param name="length">longueur de des données reçues</param>
        /// <param name="datas">données reçues</param>
        private void OnDataRecieved(object sender, int length, byte[] datas)
        {
            m_bDataAvailable = true;
            m_MessageList.Add(datas);
        }

        /// <summary>
        /// handler appelé lorsque l'état de la connexion change
        /// </summary>
        /// <param name="sender">objet ayant envoyé l'évènement</param>
        /// <param name="state">nouvel etat de la connexion</param>
        private void OnConnectionStateChange(object sender, eClientConnectionSate state)
        {
            eClientConnectionSate PreviousState = m_ConnectionState;
            m_ConnectionState = state;
            if (PreviousState == eClientConnectionSate.Connected
                && m_ConnectionState == eClientConnectionSate.Disconnected
                && !m_bUserDisconnectDemande)
            {
                string strmess = Lang.LangSys.C("Connexion with remote controller lost");
                LogEvent log = new LogEvent(LOG_EVENT_TYPE.ERROR, strmess); 
                AddLogEvent(log);
            }
            if (OnCommStateChange != null)
                OnCommStateChange();
        }
        #endregion
    }


    #region ENUMERATIONS

    public enum eClientConnectionSate
    {
        Disconnected = 0,
        Connected = 1,
        Error = 2
    }

    #endregion


    class BTTcpClient
    {
        #region DELEGUES AUX EVENEMENTS

        public delegate void ConnectionStateHandler(object sender, eClientConnectionSate state);
        public delegate void DataAvailable(object sender, int length, byte[] datas);

        #endregion

        #region VARIABLES

        private bool m_bBouclage = false;
        private bool m_bOpen = false;
        //private Thread th = null;
        private Thread m_readThread = null;
        private Socket ClientSocket = null;

        #endregion

        #region PROPRIETES PUBLIQUES

        private string m_UniqueID = "";
        //*****************************************************************************************************
        // Description: 
        // Return: /
        //*****************************************************************************************************
        public string UniqueID
        {
            get { return m_UniqueID; }
        }

        private int m_Port = 11000;
        //*****************************************************************************************************
        // Description: 
        // Return: /
        //*****************************************************************************************************
        public int Port
        {
            get { return m_Port; }
            set { m_Port = value; }
        }

        private string m_Ip = "127.0.0.1";
        //*****************************************************************************************************
        // Description: 
        // Return: /
        //*****************************************************************************************************
        public string Ip
        {
            get { return m_Ip; }
            set { m_Ip = value; }
        }

        #endregion

        #region EVENEMENTS

        public event ConnectionStateHandler ConnectionStateEvent;
        public event DataAvailable ClientDataAvailable;

        #endregion

        #region constructeurs
        //*****************************************************************************************************
        // Description: 
        // Return: /
        //*****************************************************************************************************
        public BTTcpClient()
        {
            m_UniqueID = System.Guid.NewGuid().ToString();
        }
        #endregion

        #region attributs
        //*****************************************************************************************************
        // Description: 
        // Return: /
        //*****************************************************************************************************
        public bool IsOpen
        {
            get
            {
                return m_bOpen;
            }
        }
        #endregion

        #region méthodes publiques
        //*****************************************************************************************************
        // Description: 
        // Return: /
        //*****************************************************************************************************
        public void Start()
        {
            // pour lancer l'ouverture de la connexion en tant que thread
            /*
            th = new Thread(new ThreadStart(StartClient));
            th.Name = "StartClient " + m_UniqueID;
            th.Priority = ThreadPriority.Normal;
            th.Start();
            */
            StartClient();
        }

        //*****************************************************************************************************
        // Description: 
        // Return: /
        //*****************************************************************************************************
        public void Stop()
        {
            try
            {
                m_bBouclage = false;
                if (ClientSocket != null)
                {
                    while (ClientSocket.Connected == true)
                    {
                        ClientSocket.Shutdown(SocketShutdown.Both);
                        ClientSocket.Close();
                    }
                }
                m_bOpen = false;
                ConnectionStateEvent(this, eClientConnectionSate.Disconnected);
            }
            catch
            {
                ConnectionStateEvent(this, eClientConnectionSate.Error);
            }
            Thread.Sleep(100);
        }

        //*****************************************************************************************************
        // Description: 
        // Return: /
        //*****************************************************************************************************
        public void SendData(byte[] datas)
        {
            try
            {
                ClientSocket.Send(datas, datas.Length, SocketFlags.None);
            }
            catch
            {
                Stop();
            }
        }
        #endregion

        #region LES METHODES LIEES AUX THREADS

        //*****************************************************************************************************
        // Description: 
        // Return: /
        //*****************************************************************************************************
        private void StartClient()
        {
            IPAddress ipa = IPAddress.Parse(m_Ip);
            IPEndPoint ipEnd = new IPEndPoint(ipa, m_Port);
            ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                ClientSocket.Connect(ipEnd);
                if (ClientSocket.Connected == true)
                {
                    m_bBouclage = true;
                    m_readThread = new Thread(new ThreadStart(ReceiveData));
                    m_readThread.Priority = ThreadPriority.BelowNormal;
                    m_readThread.Name = "ReceiveData " + m_UniqueID;
                    m_readThread.Start();
                    m_bOpen = true;
                    ConnectionStateEvent(this, eClientConnectionSate.Connected);
                }
            }
            catch
            {
                m_bOpen = false;
                ConnectionStateEvent(this, eClientConnectionSate.Error);
            }
        }

        //*****************************************************************************************************
        // Description: 
        // Return: /
        //*****************************************************************************************************
        private void ReceiveData()
        {
            try
            {
                while (m_bBouclage)
                {
                    if (ClientSocket.Connected == true)
                    {
                        
                        if (ClientSocket.Poll(10000, SelectMode.SelectRead) && ClientSocket.Available == 0)
                        {
                            Stop();
                        }
                        if (ClientSocket.Available > 0)
                        {
                            byte[] message = { };
                            byte[] tmpMsg;
                            while (ClientSocket.Available > 0)
                            {
                                try
                                {
                                    byte[] msg = new byte[ClientSocket.Available];
                                    ClientSocket.Receive(msg, msg.Length, SocketFlags.None);
                                    tmpMsg = new byte[message.Length + msg.Length];
                                    message.CopyTo(tmpMsg, 0);
                                    msg.CopyTo(tmpMsg, message.Length);
                                    message = tmpMsg;
                                }
                                catch
                                {
                                    Stop();
                                }
                            }
                            /*
                            bool cmd = false;
                            string msgString = System.Text.ASCIIEncoding.ASCII.GetString(message, 0, message.Length);
                            if (msgString.Length == 4)
                            {
                                switch (msgString.ToUpper())
                                {
                                    case "PING":
                                        SendData(System.Text.ASCIIEncoding.ASCII.GetBytes("PONG"));
                                        cmd = true;
                                        break;
                                }
                            }
                            if (!cmd)
                             * */
                            {
                                ClientDataAvailable(this, message.Length, message);
                            }
                        }
                    }
                    Thread.Sleep(1);
                }
            }
            catch
            {
                Stop();
            }
        }

        #endregion
    }

    #region ENUMERATIONS

    public enum eServerConnectionSate
    {
        Disconnected = 0,
        Listening = 1,
        Error = 2
    }

    public enum eClientsConnectionSate
    {
        Disconnected = 0,
        Connected = 1
    }

    #endregion

    #region DELEGUES AUX EVENEMENTS

    public delegate void ConnectionStateHandler(object sender, eServerConnectionSate state);
    public delegate void ClientsConnectionStateHandler(object sender, int hashcode, eClientsConnectionSate state);
    public delegate void DataAvailable(object sender, int hashcode, int length, byte[] datas);
    public delegate void PingPong(object sender, int hashcode);

    #endregion

    public class tcpServeur
    {

        #region VARIABLES PRIVEES

        private ArrayList acceptList = new ArrayList();
        private ArrayList readList = new ArrayList();
        private bool Bouclage = true;
        private bool readLock = false;
        private bool writeLock = false;
        private Thread th = null;

        #endregion

        #region PROPRIETES PUBLIQUES

        private string m_UniqueID = "";
        public string UniqueID
        {
            get { return m_UniqueID; }
        }

        private int m_Port = 11000;
        public int Port
        {
            get { return m_Port; }
            set { m_Port = value; }
        }

        private int m_Timeout = 10000;
        public int Timeout
        {
            get { return m_Timeout; }
            set { m_Timeout = value; }
        }

        private int m_MaxConnections = 99;
        private int MaxConnections
        {
            get { return m_MaxConnections; }
            set { m_MaxConnections = value; }
        }

        #endregion

        #region FONCTIONS PRIVEES

        private IPAddress GetLocalIp()
        {
            IPHostEntry ipHostEntry = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostEntry.AddressList[0];
            return ipAddress;
        }

        #endregion

        #region EVENEMENTS

        public event ConnectionStateHandler ConnectionStateEvent;
        public event ClientsConnectionStateHandler ClientsConnectionStateEvent;
        public event DataAvailable ClientDataAvailable;
        public event PingPong ClientPingPong;

        #endregion

        #region constructeur
        public tcpServeur()
        {
            m_UniqueID = System.Guid.NewGuid().ToString();
            Bouclage = true;
        }
        #endregion

        #region méthodes publiques
        public void Start()
        {
            th = new Thread(new ThreadStart(StartServeur));
            th.Name = "StartServer " + m_UniqueID;
            th.Priority = ThreadPriority.Normal;
            th.Start();
        }

        public void Stop()
        {
            Bouclage = false;
            readLock = false;
            writeLock = false;
            for (int i = 0; i < acceptList.Count; i++)
            {
                ClientsConnectionStateEvent(this, ((Socket)acceptList[i]).GetHashCode(), eClientsConnectionSate.Disconnected);
                ((Socket)acceptList[i]).Shutdown(SocketShutdown.Both);
                ((Socket)acceptList[i]).Close();
                acceptList.Remove(((Socket)acceptList[i]));
            }
            Thread.Sleep(100);
            ServerSocket.Close();
        }

        public bool SendData(int hashcode, byte[] datas)
        {
            int index = 0;
            try
            {
                for (int i = 0; i < acceptList.Count; i++)
                {
                    if (((Socket)acceptList[i]).GetHashCode() == hashcode)
                    {
                        index = i;
                        break;
                    }
                }
                writeLock = true;
                ((Socket)acceptList[index]).Send(datas, datas.Length, SocketFlags.None);
                writeLock = false;
                return true;
            }
            catch
            {
                writeLock = false;
                return false;
            }
        }
        #endregion

        #region LES METHODES LIEES AUC THREADS

        private Socket ServerSocket = null;
        private void StartServeur()
        {
            Socket CurrentClient = null;
            ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                ServerSocket.Bind(new IPEndPoint(GetLocalIp(), m_Port));
                ServerSocket.Listen(10);
                ConnectionStateEvent(this, eServerConnectionSate.Listening);
                Thread getReadClients = new Thread(new ThreadStart(ReceiveData));
                getReadClients.Start();
                Thread pingPongThread = new Thread(new ThreadStart(CheckIfStillConnected));
                pingPongThread.Priority = ThreadPriority.Lowest;
                pingPongThread.Name = "PingPong";
                pingPongThread.Start();
                while (Bouclage)
                {
                    if (acceptList.Count < m_MaxConnections)
                    {
                        CurrentClient = ServerSocket.Accept();
                        acceptList.Add(CurrentClient);
                        ClientsConnectionStateEvent(this, CurrentClient.GetHashCode(), eClientsConnectionSate.Connected);
                    }
                    else
                    {
                        Thread.Sleep(200);
                    }
                }
            }
            catch
            {
                Bouclage = false;
                ConnectionStateEvent(this, eServerConnectionSate.Error);
            }
            Thread.Sleep(200);
        }

        private void ReceiveData()
        {
            while (Bouclage)
            {
                readList.Clear();
                for (int i = 0; i < acceptList.Count; i++)
                {
                    readList.Add((Socket)acceptList[i]);
                }
                if (readList.Count > 0)
                {
                    Socket.Select(readList, null, null, 1000);
                    for (int i = 0; i < readList.Count; i++)
                    {
                        if (((Socket)readList[i]).Available > 0 && Bouclage)
                        {
                            readLock = true;
                            int paquetsReceived = 0;
                            byte[] message = { };
                            byte[] msgTemp;
                            byte[] msg;
                            while (((Socket)readList[i]).Available > 0 && Bouclage)
                            {
                                msg = new byte[((Socket)readList[i]).Available];
                                paquetsReceived = ((Socket)readList[i]).Receive(msg, msg.Length, SocketFlags.None);
                                msgTemp = new byte[message.Length + msg.Length];
                                message.CopyTo(msgTemp, 0);
                                msg.CopyTo(msgTemp, message.Length);
                                message = msgTemp;
                            }
                            ClientDataAvailable(this, ((Socket)readList[i]).GetHashCode(), message.Length, message);
                        }
                        readLock = false;
                    }
                }
            }
            Thread.Sleep(10);
        }

        private void CheckIfStillConnected()
        {
            while (Bouclage)
            {
                for (int i = 0; i < acceptList.Count; i++)
                {
                    if (!readLock && !writeLock)
                    {
                        try
                        {
                            ClientPingPong(this, ((Socket)acceptList[i]).GetHashCode());
                            byte[] ping = System.Text.ASCIIEncoding.ASCII.GetBytes("PING");
                            ((Socket)acceptList[i]).Send(ping, ping.Length, SocketFlags.None);
                        }
                        catch
                        {
                            ClientsConnectionStateEvent(this, ((Socket)acceptList[i]).GetHashCode(), eClientsConnectionSate.Disconnected);
                            ((Socket)acceptList[i]).Shutdown(SocketShutdown.Both);
                            ((Socket)acceptList[i]).Close();
                            acceptList.Remove(((Socket)acceptList[i]));
                        }
                    }
                }
                Thread.Sleep(m_Timeout);
            }
        }

        #endregion

    }
}

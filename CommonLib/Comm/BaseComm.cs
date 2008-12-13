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

namespace CommonLib
{
    public delegate void CommOpenedStateChange();

    #region enums
    public enum COMM_ERROR
    {
        ERROR_NONE = 0,
        ERROR_TIMEOUT,
        ERROR_PARITY,
        ERROR_RECIEVED_DATA,
        ERROR_SEND_DATA,
        ERROR_UNKNOWN,
    }
    #endregion

    /// <summary>
    /// classe de base dont doivent hériter les diverses classes de communication
    /// </summary>
    public abstract class BaseComm : Object
    {
        #region Déclaration des données de la classe
        protected COMM_ERROR m_CommErrorCode;
        protected bool m_bDataAvailable;
        #endregion

        #region Events
        public event AddLogEventDelegate EventAddLogEvent;
        #endregion

        #region constructeur
        /// <summary>
        /// 
        /// </summary>
        public BaseComm()
        {
            m_bDataAvailable = false;
            m_CommErrorCode = COMM_ERROR.ERROR_NONE;
        }
        #endregion

        #region methodes abstraites
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public abstract bool SendData(Byte[] buffer);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract byte[] GetRecievedData(int NumberOfByte, byte[] FrameHeader);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract bool OpenComm();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract bool CloseComm();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract bool IsOpen();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FrameLenght"></param>
        /// <param name="FrameHeader"></param>
        /// <returns></returns>
        public abstract bool TestFrame(int FrameLenght, byte[] FrameHeader);
        #endregion

        #region Attributs
        /// <summary>
        /// 
        /// </summary>
        public bool DataRecieved
        {
            get
            {
                return m_bDataAvailable;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public COMM_ERROR ErrorCode
        {
            get
            {
                return m_CommErrorCode;
            }
            set
            {
                m_CommErrorCode = value;
            }
        }
        #endregion

        #region methodes publiques
        /// <summary>
        /// 
        /// </summary>
        public void ResetError()
        {
            m_CommErrorCode = COMM_ERROR.ERROR_NONE;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Event"></param>
        public void AddLogEvent(LogEvent Event)
        {
            if (EventAddLogEvent != null)
            {
                EventAddLogEvent(Event);
            }
        }
        #endregion

    }
}

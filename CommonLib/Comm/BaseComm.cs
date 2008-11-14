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
    public enum COMM_ERROR
    {
        ERROR_NONE = 0,
        ERROR_TIMEOUT,
        ERROR_PARITY,
        ERROR_RECIEVED_DATA,
        ERROR_SEND_DATA,
        ERROR_UNKNOWN,
    }
    public abstract class BaseComm : Object
    {
        #region Déclaration des données de la classe
        protected COMM_ERROR m_CommErrorCode;
        protected bool m_bDataAvailable;
        #endregion

        public event AddLogEventDelegate EventAddLogEvent;

        /// <summary>
        /// 
        /// </summary>
        public BaseComm()
        {
            m_bDataAvailable = false;
            m_CommErrorCode = COMM_ERROR.ERROR_NONE;
        }

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

        /// <summary>
        /// 
        /// </summary>
        public void ResetError()
        {
            m_CommErrorCode = COMM_ERROR.ERROR_NONE;
        }

        public abstract bool TestFrame(int FrameLenght , byte[] FrameHeader);
        //public abstract void DiscardRecievedDatas();

        public void AddLogEvent(LogEvent Event)
        {
            if (EventAddLogEvent != null)
            {
                EventAddLogEvent(Event);
            }
        }

    }
}

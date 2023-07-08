/*
    This file is part of SmartApp.
    Copyright (C) 2007-2016  Pascal Bigot

    SmartApp is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    SmartApp is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with Foobar.  If not, see <http://www.gnu.org/licenses/>.
*/
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
        /// constructeur par défaut
        /// </summary>
        public BaseComm()
        {
            m_bDataAvailable = false;
            m_CommErrorCode = COMM_ERROR.ERROR_NONE;
        }
        #endregion

        #region methodes abstraites
        /// <summary>
        /// envoie des le buffer donné sur la communication
        /// </summary>
        /// <param name="buffer">buffer contenant les données à envoyer</param>
        /// <returns></returns>
        public abstract bool SendData(Byte[] buffer);

        /// <summary>
        /// obtiens les données reçu sur la communication
        /// </summary>
        /// <param name="NumberOfByte">Nombre d'octet à lire</param>
        /// <param name="FrameHeader">Header de trame (peut être null)</param>
        /// <returns>tableau contenant les données reçues</returns>
        public abstract byte[] GetRecievedData(int NumberOfByte, byte[] FrameHeader);

        /// <summary>
        /// ouvre la communication
        /// </summary>
        /// <returns>true en cas de succès</returns>
        public abstract bool OpenComm();

        /// <summary>
        /// ferme la communication
        /// </summary>
        /// <returns>true en cas de succès</returns>
        public abstract bool CloseComm();

        /// <summary>
        /// indique si la communication est ouverte
        /// </summary>
        /// <returns>true si elle est ouverte</returns>
        public abstract bool IsOpen();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FrameLenght"></param>
        /// <param name="FrameHeader"></param>
        /// <returns></returns>
        public abstract bool TestFrame(int FrameLenght, byte[] FrameHeader);

        #region Events
        public event CommOpenedStateChange OnCommStateChange;
        #endregion

        protected void NotifyComStateChange()
        {
            if (OnCommStateChange != null)
                OnCommStateChange();
        }

        #endregion

        #region Attributs
        /// <summary>
        /// indique si des données on été reçues sur la communication
        /// </summary>
        public bool DataRecieved
        {
            get
            {
                return m_bDataAvailable;
            }
        }

        /// <summary>
        /// dernier code d'erreur de la communication
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
        /// réinitialise la dernière erreur
        /// </summary>
        public void ResetError()
        {
            m_CommErrorCode = COMM_ERROR.ERROR_NONE;
        }

        /// <summary>
        /// envoie un évènement vers le logger de SmartCommand
        /// </summary>
        /// <param name="Event">objet évènement</param>
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

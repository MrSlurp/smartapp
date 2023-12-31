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
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLib
{
    /// <summary>
    /// classe virtuelle de communication qui permet de tester sans vraiment se connecter
    /// </summary>
    public class VirtualComm : BaseComm
    {
        #region données membres
        bool m_bIsCommOpenned = false;
        string m_commParam = "NA";
        #endregion


        #region cosntructeur
        /// <summary>
        /// 
        /// </summary>
        public VirtualComm()
        {
            m_bDataAvailable = true;
        }
        #endregion

        #region méthodes publiques
        /// <summary>
        /// Envoie une trame vers la connexion virtuelle
        /// cette version de doit pas être utilisé
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public override bool SendData(Byte[] buffer)
        {
            // ne doit pas être utilisé
            System.Diagnostics.Debug.Assert(false);
            return true;
        }

        /// <summary>
        /// Envoie une trame vers la connexion virtuelle
        /// </summary>
        /// <param name="TrameToSend">trame à envoyer</param>
        /// <param name="DataGest">gestionnaire de donnée</param>
        /// <param name="VirtualDataGest">gestionnaire de données virtuelles</param>
        /// <returns>true en cas de succès</returns>
        public bool SendData(Trame TrameToSend, GestData DataGest, GestDataVirtual VirtualDataGest)
        {
            for (int i = 0; i< TrameToSend.FrameDatas.Count; i++)
            {
                string strDataSymb = TrameToSend.FrameDatas[i];
                Data dt = (Data)DataGest.GetFromSymbol(strDataSymb);
                VirtualData vdt = (VirtualData)VirtualDataGest.GetFromSymbol(strDataSymb);
                if (dt != null && vdt != null && !dt.IsConstant)
                    vdt.Value = dt.Value;
            }
            return true;
        }

        /// <summary>
        /// NE PAS UTILISER DANS LA COMM VIRTUELLE
        /// </summary>
        /// <param name="NumberOfByte">nombre d'octet de la trame à extraire</param>
        /// <param name="FrameHeader">header de la trame à extraire</param>
        /// <returns>un tableau de Byte contenant les données reçues</returns>
        public override Byte[] GetRecievedData(int NumberOfByte, byte[] FrameHeader)
        {
            // ne doit pas être utilisé
            System.Diagnostics.Debug.Assert(false);
            return null;
        }

        /// <summary>
        /// renvoie les données reçues par le port série
        /// </summary>
        /// <param name="ConvertedSize">taille de la trame convertie</param>
        /// <param name="TrameToReturn">trame à renvoyer</param>
        /// <returns>buffer des données reçues</returns>
        public Byte[] GetRecievedData(int ConvertedSize, Trame TrameToReturn)
        {
            Byte[] retFrame = TrameToReturn.CreateTrameToSend(true);
            if (retFrame != null && ConvertedSize != retFrame.Length)
                System.Diagnostics.Debug.Assert(false);

            return retFrame;
        }

        /// <summary>
        /// renvoie toujours true dans le cas de la comm virtuelle
        /// </summary>
        /// <param name="FrameLenght">non utilisé</param>
        /// <param name="FrameHeader">non utilisé</param>
        /// <returns>toujours true</returns>
        public override bool TestFrame(int FrameLenght, byte[] FrameHeader)
        {
            return true;
        }

        /// <summary>
        /// ouvre le port de communication
        /// </summary>
        /// <returns> vrai si le port a bien été ouvert</returns>
        public override bool OpenComm()
        {
            m_bIsCommOpenned = true;
            NotifyComStateChange();
            return true;
        }

        /// <summary>
        /// ferme le port de communication
        /// </summary>
        /// <returns> vrai si le port est bien fermé</returns>
        public override bool CloseComm()
        {
            m_bIsCommOpenned = false;
            NotifyComStateChange();
            return true;
        }

        /// <summary>
        /// vérifie l'état d'ouverture du port série
        /// </summary>
        /// <returns> vrai si le port de comm est ouvert</returns>
        public override bool IsOpen()
        {
            return m_bIsCommOpenned;
        }
        #endregion

        public string ComParam
        {
            get { return m_commParam; }
            set { }
        }
    }
}

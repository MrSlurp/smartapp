using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLib
{
    public class VirtualComm : BaseComm
    {
        // classe virtuelle de comminucation qui permet de tester sans vraiment se connecter
        bool m_bIsCommOpenned = false;

        #region Events
        public event CommOpenedStateChange OnCommStateChange;
        #endregion

        public VirtualComm()
        {
            m_bDataAvailable = true;
        }

        public override bool SendData(Byte[] buffer)
        {
            // ne doit pas être utilisé
            System.Diagnostics.Debug.Assert(false);
            return true;
        }

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

        public override Byte[] GetRecievedData(int NumberOfByte, byte[] FrameHeader)
        {
            // ne doit pas être utilisé
            System.Diagnostics.Debug.Assert(false);
            return null;
        }

        public Byte[] GetRecievedData(int ConvertedSize, Trame TrameToReturn)
        {
            Byte[] retFrame = TrameToReturn.CreateTrameToSend(true);
            if (ConvertedSize != retFrame.Length)
                System.Diagnostics.Debug.Assert(false);
            return retFrame;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FrameLenght"></param>
        /// <param name="FrameHeader"></param>
        /// <returns></returns>
        public override bool TestFrame(int FrameLenght, byte[] FrameHeader)
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool OpenComm()
        {
            m_bIsCommOpenned = true;
            if (OnCommStateChange != null)
                OnCommStateChange();
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool CloseComm()
        {
            m_bIsCommOpenned = false;
            if (OnCommStateChange != null)
                OnCommStateChange();
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool IsOpen()
        {
            return m_bIsCommOpenned;
        }

    }
}

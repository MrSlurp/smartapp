using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace CommonLib
{
#if !QUICK_MOTOR
    public partial class ScriptExecuter
    {
        #region execution des trames
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void ParseExecuteFrame(string line)
        {
            string[] strTab = line.Split(ParseExecGlobals.TOKEN_SEPARATOR);
            if (strTab.Length > 2)
            {
                string strFrame = strTab[1];
                strFrame = strFrame.Trim();
                string strTemp = strTab[2];
                string strTempFull = strTemp;
                int posOpenParenthese = strTemp.LastIndexOf('(');
                if (posOpenParenthese >= 0)
                    strTemp = strTemp.Remove(posOpenParenthese);

                string strScrObject = strTemp;
                FRAME_FUNC SecondTokenType = FRAME_FUNC.INVALID;
                try
                {
                    SecondTokenType = (FRAME_FUNC)Enum.Parse(typeof(FRAME_FUNC), strScrObject);
                }
                catch (Exception)
                {
                    return;
                }
                switch (SecondTokenType)
                {
                    case FRAME_FUNC.RECEIVE:
                        ExecuteRecieveFrame(strFrame);
                        break;
                    case FRAME_FUNC.SEND:
                        ExecuteSendFrame(strFrame);
                        break;
                }
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void ExecuteSendFrame(string FrameSymbol)
        {
            Trame TrameToSend = (Trame)m_Document.GestTrame.QuickGetFromSymbol(FrameSymbol);
            if (TrameToSend != null)
            {
                Byte[] buffer = TrameToSend.CreateTrameToSend(false);
                if (m_Document.m_Comm.IsOpen)
                {
                    if (m_Document.m_Comm.CommType == TYPE_COMM.VIRTUAL)
                        m_Document.m_Comm.SendData(TrameToSend, m_Document.GestData, m_Document.GestDataVirtual);
                    else
                        m_Document.m_Comm.SendData(buffer);
                    
                }
                else
                {
                    LogEvent log = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format("Trying sending frame {0} while connection is closed", TrameToSend.Symbol));
                    AddLogEvent(log);
                }
            }
            else
            {
                LogEvent log = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format("Unknown Frame {0}", FrameSymbol));
                AddLogEvent(log);
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void ExecuteRecieveFrame(string FrameSymbol)
        {
            Trame TrameToRecieve = (Trame)m_Document.GestTrame.QuickGetFromSymbol(FrameSymbol);
            if (TrameToRecieve != null)
            {
                if (m_Document.m_Comm.IsOpen)
                {
                    byte[] FrameHeader = TrameToRecieve.FrameHeader;
                    int ConvertedSize = TrameToRecieve.GetConvertedTrameSizeInByte();
                    m_bIsWaiting = true;
                    if (!m_Document.m_Comm.WaitTrameRecieved(ConvertedSize, FrameHeader))
                    {
                        m_bIsWaiting = false;
                        //indiquer qu'une trame n'a pas été recu
                        // et demander a l'utilisateur si il souhaite continuer l'execution des actions
                        // si il ne veux pas, remonter au parent qu'il doit arrèter les actions
                        //COMM_ERROR Err = m_Doc.m_Comm.ErrorCode;
                        string strmess = string.Format("Message {0} have not been recieved (Timeout)", TrameToRecieve.Symbol);
                        LogEvent log = new LogEvent(LOG_EVENT_TYPE.ERROR, strmess);
                        AddLogEvent(log);
                        return;
                    }
                    m_bIsWaiting = false;
                    byte[] buffer = null;
                    if (m_Document.m_Comm.CommType == TYPE_COMM.VIRTUAL)
                        buffer = m_Document.m_Comm.GetRecievedData(ConvertedSize, TrameToRecieve);
                    else
                        buffer = m_Document.m_Comm.GetRecievedData(ConvertedSize, FrameHeader);

                    if (buffer == null || !TrameToRecieve.TreatRecieveTrame(buffer))
                    {
                        string strmess;

                        if (buffer == null)
                            strmess = string.Format("Error reading message {0} (Recieved frame is not the one expected)", TrameToRecieve.Symbol);
                        else
                            strmess = string.Format("Error reading message {0}", TrameToRecieve.Symbol);
                        LogEvent log = new LogEvent(LOG_EVENT_TYPE.ERROR, strmess);
                        AddLogEvent(log);
                    }
                    else
                    {
                        //Console.WriteLine("{1} Trame Lue {0}", FrameSymbol, DateTime.Now.ToLongTimeString() + ":" + DateTime.Now.Millisecond);
                    }
                }
                else
                {
                    LogEvent log = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format("Trying reading frame {0} while connection is closed", TrameToRecieve.Symbol));
                    AddLogEvent(log);
                }
            }
            else
            {
                LogEvent log = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format("Unknown Timer {0}", FrameSymbol));
                AddLogEvent(log);
            }
        }

        #endregion        
    }
#endif
}

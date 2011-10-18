using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using CommonLib;

namespace CtrlDemux
{
    internal class CtrlDemuxCmdControl : BTDllCtrlDemuxControl
    {
        #region donnée spécifiques aux fonctionement en mode Command

        // liste des données a loguer utilisé en mode Command
        private ArrayList m_ListRefDatas = new ArrayList();
        private Data AdressData = null;
        private Data ValueData = null;
        private Timer m_TimerUpdateData = new Timer();
        private bool DemuxParemtersOK = true;

        #endregion
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public CtrlDemuxCmdControl()
        {

        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public override void CreateControl()
        {
            if (m_Ctrl == null)
            {
                m_Ctrl = new CtrlDemuxDispCtrl();
                m_Ctrl.Location = m_RectControl.Location;
                m_Ctrl.Name = m_strSymbol;
                m_Ctrl.Size = m_RectControl.Size;
                m_Ctrl.BackColor = Color.Transparent;
                // faites ici les initialisation spécifiques du control affiché
            }
        }

        public override void OnControlEvent(object Sender, EventArgs Args)
        {
            // traitez ici les évènement déclenché par le control (click souris par exemple)
            return;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public override void UpdateFromData()
        {
            if (DemuxParemtersOK)
            {
                if (AdressData.Value < m_ListRefDatas.Count)
                {
                    ((Data)m_ListRefDatas[AdressData.Value]).Value = ValueData.Value;
                }
            }
        }

        public override bool FinalizeRead(BTDoc Doc)
        {
            DllCtrlDemuxProp Props = (DllCtrlDemuxProp)m_SpecificProp;

            // on fait une liste de références directe sur les données
            for (int i = 0; i < Props.ListDemuxData.Count; i++)
            {
                string strData = Props.ListDemuxData[i];
                Data Dat = (Data)Doc.GestData.GetFromSymbol(strData);
                if (Dat == null)
                {
                    if (string.IsNullOrEmpty(strData))
                        continue;

                    string strMessage;
                    strMessage = string.Format(DllEntryClass.LangSys.C("Demux Output Data not found (Demux {0}, Data {1})"), m_strSymbol, strData);
                    LogEvent log = new LogEvent(LOG_EVENT_TYPE.ERROR, strMessage);
                    AddLogEvent(log);
                    DemuxParemtersOK = false;
                    continue;
                }
                m_ListRefDatas.Add(Dat);
            }
            Data Dat2 = (Data)Doc.GestData.GetFromSymbol(Props.AdressData);
            if (Dat2 == null)
            {
                string strMessage;
                strMessage = string.Format(DllEntryClass.LangSys.C("Demux adress Data not found (Demux {0}, Data {1})"), m_strSymbol, Props.AdressData);
                LogEvent log = new LogEvent(LOG_EVENT_TYPE.ERROR, strMessage);
                AddLogEvent(log);
                DemuxParemtersOK = false;
            }
            AdressData = Dat2;
            //AdressData.DataValueChanged += new EventDataValueChange(UpdateFromData);

            Data Dat3 = (Data)Doc.GestData.GetFromSymbol(Props.ValueData);
            if (Dat3 == null)
            {
                string strMessage;
                strMessage = string.Format(DllEntryClass.LangSys.C("Demux value Data not found (Demux {0}, Data {1})"), m_strSymbol, Props.ValueData);
                LogEvent log = new LogEvent(LOG_EVENT_TYPE.ERROR, strMessage);
                AddLogEvent(log);
                DemuxParemtersOK = false;
            }
            ValueData = Dat3;
            ### on ne s'abonne pas au changement de valeur car cela ne fontionne pas lorsque deux valeur multiplexé consecutives
            ### on la même valeur
            ### à la place on va s'abonner à la notif de fin de lecture de trame généré par le moteur de script
            //if (ValueData != null)
            //{
                // ici on a pas besoin de vérifier InvokeRequired, car on ne change pas l'aspect du control
                ///ValueData.DataValueChanged += new EventDataValueChange(UpdateFromData);
            //}
            

            if (!DemuxParemtersOK)
            {
                string strMessage;
                strMessage = string.Format(DllEntryClass.LangSys.C("Demux {0} have some invalid parameters. Demux Disabled"), m_strSymbol, Props.ValueData);
                LogEvent log = new LogEvent(LOG_EVENT_TYPE.ERROR, strMessage);
                AddLogEvent(log);
            }

            return true;
        }

        public override void TraiteMessage(MESSAGE Mess, object obj, TYPE_APP TypeApp)
        {
            base.TraiteMessage(Mess, obj, TypeApp);
            if (TypeApp == TYPE_APP.SMART_COMMAND)
            {
                switch (Mess)
                {
                    // message de requête sur les conséquence d'une supression
                    case MESSAGE.MESS_CMD_STOP:
                        // traitez ici le passage en mode stop du control si nécessaire
                        break;
                    case MESSAGE.MESS_CMD_RUN:
                        // traitez ici le passage en mode run du control si nécessaire
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public class CtrlDemuxDispCtrl : UserControl
    {
        // ajouter ici les données membres du control affiché

        public CtrlDemuxDispCtrl()
        {
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // mettez ici le code de dessin du control
            base.OnPaint(e);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Net.Mail;
using CommonLib;

namespace CtrlMailer
{
    /// <summary>
    /// Cette classe serta  définir le comportement du control lorsqu'il est executé dans SmartCommand
    /// </summary>
    internal class CtrlMailerCmdControl : BTDllCtrlMailerControl
    {
        int m_iPreviousDataValue = 0;
        GestData m_DocGestData;
        SmtpClient SmtpServer = new SmtpClient();
        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        public CtrlMailerCmdControl()
        {
            SmtpServer.SendCompleted += new SendCompletedEventHandler(SmtpServer_SendCompleted);
        }

        void SmtpServer_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (!e.Cancelled && e.Error == null)
            {
                LogEvent log = new LogEvent(LOG_EVENT_TYPE.INFO, string.Format(DllEntryClass.LangSys.C("Mailer {0} : mail posted"), this.Symbol));
                AddLogEvent(log);
            }
            else if (e.Error != null)
            {
                LogEvent log = new LogEvent(LOG_EVENT_TYPE.INFO, string.Format(DllEntryClass.LangSys.C("Mailer {0} : error sending mail. {1}"), this.Symbol, e.Error.Message));
                AddLogEvent(log);
            }
        }

        /// <summary>
        /// Constructeur de l'objet graphique affiché dans les écrans de supervision
        /// </summary>
        public override void CreateControl()
        {
            // on vérifie qu'il n'y a pas déja un control graphique (cette méthode ne doit être appelé qu'une seul fois)
            if (m_Ctrl == null)
            {
                // on crée l'objet graphique qui sera affiché
                m_Ctrl = new CtrlMailerDispCtrl();
                // on définit sa position dans l'écran
                m_Ctrl.Location = m_RectControl.Location;
                // son nom est le symbol de l'objet courant
                m_Ctrl.Name = m_strSymbol;
                // on définit sa taille
                m_Ctrl.Size = m_RectControl.Size;
                // on définit son fond comme étant transparent (peut être changé)
                m_Ctrl.BackColor = Color.Transparent;
                // faites ici les initialisation spécifiques du control affiché

                // par exemple la liaison du click souris à un handler d'event
                //m_Ctrl.Click += new System.EventHandler(this.OnControlEvent);
            }
        }

        /// <summary>
        /// Fonction existant par défaut pour gérer les évènement déclenché par le control affiché
        /// il est tout a fait possible d'en crée d'autre 
        /// </summary>
        /// <param name="Sender">objet ayant envoyé l'event</param>
        /// <param name="EventArgs">arguments l'event</param>
        public override void OnControlEvent(object Sender, EventArgs Args)
        {
            // traitez ici les évènement déclenché par le control (click souris par exemple)
            return;
        }

        /// <summary>
        /// Handler de l'évènement "DataValueChanged" déclenché par la donnée associée par défaut
        /// permet de mettre a jour l'état du control associé en fonction de celle ci.
        /// Pour chaque donnée qui serait utilisé dans les propriété, il est possible de réutiliser cet handler
        /// ou d'en crée d'autres (voir FinalizeRead pour la liaison des évènements)
        /// </summary>
        public override void UpdateFromData()
        {
            if (m_AssociateData != null && m_Ctrl != null)
            {
                // effectuez ici le traitement à executer lorsque la valeur change
                if (m_iPreviousDataValue == 0 && m_AssociateData.Value != 0)
                {
                    // envoyer le mail
                    DoSendMail();
                }
                m_iPreviousDataValue = m_AssociateData.Value;
            }
        }

        /// <summary>
        /// Traite les message intra applicatif de SmartConfig
        /// Ces messages informes de : 
        /// - passage en RUN de la supervision
        /// - passage en STOP de la supervision
        /// </summary>
        /// <param name="Mess">Type du message</param>
        /// <param name="obj">paramètre du message (aucun paramètre dans SmartCommand)</param>
        /// <param name="TypeApp">Type de l'application courante (SmartConfig / SmartCommand)</param>
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
                        m_iPreviousDataValue = m_AssociateData.Value;
                        // traitez ici le passage en mode run du control si nécessaire
                        break;
                    default:
                        break;
                }
            }
        }

        private void DoSendMail()
        {
            if (DllEntryClass.SMTP_Param.IsConfigured())
            {
                DllCtrlMailerProp props = this.SpecificProp as DllCtrlMailerProp;
                MailMessage message = new MailMessage();
                message.From = new MailAddress(DllEntryClass.SMTP_Param.userMail);
                string[] listTo = props.ListToMail.Split(',', ';');
                for (int dest = 0; dest < listTo.Length; dest++)
                {
                    message.To.Add(new MailAddress(listTo[dest]));
                }
                message.Subject = props.MailSubject;
                string mailBody = props.MailBody;
                int iPosDebutVar = 0;
                while (iPosDebutVar != -1 && iPosDebutVar < mailBody.Length)
                {
                    iPosDebutVar = mailBody.IndexOf("<DATA.", iPosDebutVar);
                    if (iPosDebutVar != -1)
                    {
                        int iPosFinVar = mailBody.IndexOf(">", iPosDebutVar);
                        if (iPosFinVar == -1)
                        {
                            LogEvent log = new LogEvent(LOG_EVENT_TYPE.WARNING, string.Format(DllEntryClass.LangSys.C("Mailer {0} : Can't replace data, bad message format"), this.Symbol));
                            AddLogEvent(log);
                            continue;
                        }
                        string varSubString = mailBody.Substring(iPosDebutVar, (iPosFinVar+1) - iPosDebutVar);
                        string dataSymbol = varSubString.Replace("<DATA.", "").Replace(">", "");
                        Data dt = m_DocGestData.GetFromSymbol(dataSymbol) as Data;
                        if (dt == null)
                        {
                            LogEvent log = new LogEvent(LOG_EVENT_TYPE.WARNING, string.Format(DllEntryClass.LangSys.C("Mailer {0} : Unknown data {1}"), this.Symbol, dataSymbol));
                            AddLogEvent(log);
                        }
                        else
                        {
                            mailBody = mailBody.Replace(varSubString, dt.Value.ToString());
                        }
                        iPosDebutVar = 0;
                    }
                }
                message.Body = mailBody;
                //message.Headers
                SmtpServer.Host = DllEntryClass.SMTP_Param.SMTP_host;
                SmtpServer.Port = DllEntryClass.SMTP_Param.SMTP_port;
                SmtpServer.Credentials = new System.Net.NetworkCredential(DllEntryClass.SMTP_Param.userMail, DllEntryClass.SMTP_Param.userPassword);
                SmtpServer.EnableSsl = DllEntryClass.SMTP_Param.useSSL;
                SmtpServer.SendAsync(message, this);
            }
            else
            {
                LogEvent log = new LogEvent(LOG_EVENT_TYPE.WARNING, DllEntryClass.LangSys.C("There is no SMTP parameters, Sending mail is impossible"));
                AddLogEvent(log);
            }

        }

        public override bool FinalizeRead(BTDoc Doc)
        {
            m_DocGestData = Doc.GestData;
            return base.FinalizeRead(Doc);
        }
    }

    /// <summary>
    /// classe héritant de UserControl
    /// représente l'objet graphique affiché dans la supervision
    /// on peux en faire a peut près ce qu'on veux :
    /// - du dessin
    /// - une aggregation de plusieurs controls standards, 
    /// - les deux, etc.
    /// </summary>
    public class CtrlMailerDispCtrl : UserControl
    {
        // ajouter ici les données membres du control affiché

        public CtrlMailerDispCtrl()
        {
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // mettez ici le code de dessin du control
            base.OnPaint(e);
        }
    }
}

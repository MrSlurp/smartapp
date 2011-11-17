using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using CommonLib;

namespace ImageButton
{
    /// <summary>
    /// Cette classe serta  définir le comportement du control lorsqu'il est executé dans SmartCommand
    /// </summary>
    internal class ImageButtonCmdControl : BTDllImageButtonControl
    {
        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        public ImageButtonCmdControl()
        {

        }

        /// <summary>
        /// Constructeur de l'objet graphique affiché dans les écrans de supervision
        /// </summary>
        public override void CreateControl()
        {
            // on vérifie qu'il n'y a pas déja un control graphique (cette méthode ne doit être appelé qu'une seul fois)
            if (m_Ctrl == null)
            {
                DllImageButtonProp SpecProp = this.SpecificProp as DllImageButtonProp;
                if (!SpecProp.IsBistable)
                {
                    // on crée l'objet graphique qui sera affiché
                    m_Ctrl = new ImageButtonDispCtrl();
                }
                else
                {
                    m_Ctrl = new ImageButtonBstDispCtrl();
                }
                // on définit sa position dans l'écran
                m_Ctrl.Location = m_RectControl.Location;
                // son nom est le symbol de l'objet courant
                m_Ctrl.Name = m_strSymbol;
                // on définit sa taille
                m_Ctrl.Size = m_RectControl.Size;
                // on définit son fond comme étant transparent (peut être changé)
                //m_Ctrl.BackColor = Color.Transparent;

                // faites ici les initialisation spécifiques du control affiché
                // par exemple la liaison du click souris à un handler d'event
                m_Ctrl.Click += new System.EventHandler(this.OnControlEvent);

                string strImageFullPath = PathTranslator.RelativePathToAbsolute(SpecProp.ReleasedImage);
                strImageFullPath = PathTranslator.LinuxVsWindowsPathUse(strImageFullPath);
                try
                {
                    m_Ctrl.BackgroundImage = new Bitmap(strImageFullPath);
                }
                catch (Exception)
                {
                    LogEvent log = new LogEvent(LOG_EVENT_TYPE.WARNING, string.Format(DllEntryClass.LangSys.C("Control {0} Failed to load file {1}"), Symbol, strImageFullPath));
                    AddLogEvent(log);
                }
                
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
            if (m_Ctrl is CheckBox)
            {
                DllImageButtonProp SpecProp = this.SpecificProp as DllImageButtonProp;
                CheckBox chk = m_Ctrl as CheckBox;
                string strImageFullPath;
                if (!chk.Checked)
                {
                    strImageFullPath = PathTranslator.RelativePathToAbsolute(SpecProp.ReleasedImage);
                    strImageFullPath = PathTranslator.LinuxVsWindowsPathUse(strImageFullPath);
                    if (m_AssociateData != null)
                        m_AssociateData.Value = 0;
                }
                else
                {
                    strImageFullPath = PathTranslator.RelativePathToAbsolute(SpecProp.PressedImage);
                    strImageFullPath = PathTranslator.LinuxVsWindowsPathUse(strImageFullPath);
                    if (m_AssociateData != null)
                        m_AssociateData.Value = 1;
                }
                try
                {
                    Bitmap bmp = new Bitmap(strImageFullPath);
                    bmp.MakeTransparent(Color.Magenta);
                    m_Ctrl.BackgroundImage = bmp;
                }
                catch (Exception)
                {
                    LogEvent log = new LogEvent(LOG_EVENT_TYPE.WARNING, string.Format(DllEntryClass.LangSys.C("Control {0} Failed to load file {1}"), Symbol, strImageFullPath));
                    AddLogEvent(log);
                }
            }
            else if (m_Ctrl is Button)
            {
                if (m_AssociateData != null)
                {
                    m_AssociateData.Value = m_AssociateData.Value == 0 ?  1 : 0;
                }
            }
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
                        // traitez ici le passage en mode run du control si nécessaire
                        break;
                    default:
                        break;
                }
            }
        }
    }
}

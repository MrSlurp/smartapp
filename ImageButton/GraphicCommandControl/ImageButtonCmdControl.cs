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
        protected Data m_AssocInputData = null;
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
                m_Ctrl.BackColor = Color.Transparent;

                m_Ctrl.Text = this.m_IControl.Text;

                // faites ici les initialisation spécifiques du control affiché
                // par exemple la liaison du click souris à un handler d'event
                m_Ctrl.Click += new System.EventHandler(this.OnControlEvent);

                if (!string.IsNullOrEmpty(SpecProp.ReleasedImage))
                {
                    string strImageFullPath = PathTranslator.RelativePathToAbsolute(SpecProp.ReleasedImage);
                    strImageFullPath = PathTranslator.LinuxVsWindowsPathUse(strImageFullPath);
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
                else
                {
                    m_Ctrl.BackgroundImage = ImageButtonRes.DefaultImg;
                }

                if (m_Ctrl is Button)
                {
                    ((Button)m_Ctrl).MouseDown += new MouseEventHandler(ImageButtonCmdControl_MouseDown);
                    ((Button)m_Ctrl).MouseUp += new MouseEventHandler(ImageButtonCmdControl_MouseUp);
                    ((Button)m_Ctrl).FlatAppearance.BorderSize = SpecProp.BorderSize;
                    ((Button)m_Ctrl).FlatAppearance.CheckedBackColor = Color.Transparent;
                    ((Button)m_Ctrl).FlatAppearance.MouseDownBackColor = Color.Transparent;
                    ((Button)m_Ctrl).FlatAppearance.MouseOverBackColor = Color.Transparent;
                    ((Button)m_Ctrl).FlatStyle = SpecProp.Style;
                    ((Button)m_Ctrl).TabStop = false;
                }
                else if (m_Ctrl is CheckBox)
                {
                    ((CheckBox)m_Ctrl).FlatStyle = SpecProp.Style;
                    ((CheckBox)m_Ctrl).FlatAppearance.BorderSize = SpecProp.BorderSize;
                    ((CheckBox)m_Ctrl).FlatAppearance.CheckedBackColor = Color.Transparent;
                    ((CheckBox)m_Ctrl).FlatAppearance.MouseDownBackColor = Color.Transparent;
                    ((CheckBox)m_Ctrl).FlatAppearance.MouseOverBackColor = Color.Transparent;
                    ((CheckBox)m_Ctrl).TabStop = false;
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
                if (!SpecProp.ImgFromInput)
                {
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
                        if (!SpecProp.ImgFromInput)
                        {
                            Bitmap bmp = new Bitmap(strImageFullPath);
                            bmp.MakeTransparent(Color.Magenta);
                            m_Ctrl.BackgroundImage = bmp;
                        }
                    }
                    catch (Exception)
                    {
                        LogEvent log = new LogEvent(LOG_EVENT_TYPE.WARNING, string.Format(DllEntryClass.LangSys.C("Control {0} Failed to load file {1}"), Symbol, strImageFullPath));
                        AddLogEvent(log);
                    }
                }
                else
                {
                    if (m_AssocInputData != null && m_AssocInputData.Value == 0)
                    {
                        if (m_AssociateData != null)
                            m_AssociateData.Value = 1;
                    }
                    else if (m_AssocInputData != null && m_AssocInputData.Value != 0)
                    {
                        if (m_AssociateData != null)
                            m_AssociateData.Value = 0;
                    }
                }
            }
            else if (m_Ctrl is Button)
            {
                if (m_AssociateData != null)
                {
                    m_AssociateData.Value = m_AssociateData.Value == 0 ?  1 : 0;
                }
            }

            if (m_bUseScreenEvent)
            {
                m_Parent.ControlEvent();
            }

            return;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ImageButtonCmdControl_MouseUp(object sender, MouseEventArgs e)
        {
            DllImageButtonProp SpecProp = this.SpecificProp as DllImageButtonProp;
            if (m_Ctrl is Button && !string.IsNullOrEmpty(SpecProp.ReleasedImage) && !SpecProp.ImgFromInput)
            {
                string strImageFullPath = PathTranslator.RelativePathToAbsolute(SpecProp.ReleasedImage);
                strImageFullPath = PathTranslator.LinuxVsWindowsPathUse(strImageFullPath);
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
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ImageButtonCmdControl_MouseDown(object sender, MouseEventArgs e)
        {
            DllImageButtonProp SpecProp = this.SpecificProp as DllImageButtonProp;
            if (m_Ctrl is Button && !string.IsNullOrEmpty(SpecProp.PressedImage) && !SpecProp.ImgFromInput)
            {
                string strImageFullPath = PathTranslator.RelativePathToAbsolute(SpecProp.PressedImage);
                strImageFullPath = PathTranslator.LinuxVsWindowsPathUse(strImageFullPath);
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
        }


        /// <summary>
        /// Handler de l'évènement "DataValueChanged" déclenché par la donnée associée par défaut
        /// permet de mettre a jour l'état du control associé en fonction de celle ci.
        /// Pour chaque donnée qui serait utilisé dans les propriété, il est possible de réutiliser cet handler
        /// ou d'en crée d'autres (voir FinalizeRead pour la liaison des évènements)
        /// </summary>
        public override void UpdateFromData()
        {
            DllImageButtonProp prop = (DllImageButtonProp)m_SpecificProp;
            if (m_AssocInputData != null && m_Ctrl != null && prop.ImgFromInput)
            {
                if (prop.ImgFromInput)
                {
                    string strImageFullPath;
                    if (m_AssocInputData.Value == 0)
                    {
                        strImageFullPath = PathTranslator.RelativePathToAbsolute(prop.ReleasedImage);
                        strImageFullPath = PathTranslator.LinuxVsWindowsPathUse(strImageFullPath);
                        if (m_Ctrl is CheckBox)
                        {
                            CheckBox chk = m_Ctrl as CheckBox;
                            chk.Checked = false;
                        }
                    }
                    else
                    {
                        strImageFullPath = PathTranslator.RelativePathToAbsolute(prop.PressedImage);
                        strImageFullPath = PathTranslator.LinuxVsWindowsPathUse(strImageFullPath);
                        if (m_Ctrl is CheckBox)
                        {
                            CheckBox chk = m_Ctrl as CheckBox;
                            chk.Checked = true;
                        }
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
            }
        }

        public override bool FinalizeRead(BTDoc Doc)
        {
            bool bret = base.FinalizeRead(Doc);

            DllImageButtonProp prop = (DllImageButtonProp)m_SpecificProp;
            if (!string.IsNullOrEmpty(prop.InputData))
            {
                m_AssocInputData = (Data)Doc.GestData.GetFromSymbol(prop.InputData);
                if (m_AssocInputData != null)
                    m_AssocInputData.DataValueChanged += new EventDataValueChange(UpdateFromData);

            }
            return bret;
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


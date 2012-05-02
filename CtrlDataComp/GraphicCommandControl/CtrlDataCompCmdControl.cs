using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using CommonLib;

namespace CtrlDataComp
{
    /// <summary>
    /// Cette classe serta  définir le comportement du control lorsqu'il est executé dans SmartCommand
    /// </summary>
    internal class CtrlDataCompCmdControl : BTDllCtrlDataCompControl
    {
        Data m_dataA;
        int m_ValueA;

        Data m_dataB;
        int m_ValueB;

        Data m_dataC;
        int m_ValueC;

        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        public CtrlDataCompCmdControl(BTDoc document)
            :base(document)
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
                // on crée l'objet graphique qui sera affiché
                m_Ctrl = new CtrlDataCompDispCtrl();
                // on définit sa position dans l'écran
                m_Ctrl.Location = m_RectControl.Location;
                // son nom est le symbol de l'objet courant
                m_Ctrl.Name = m_strSymbol;
                // on définit sa taille
                m_Ctrl.Size = m_RectControl.Size;
                // on définit son fond comme étant transparent (peut être changé)
                m_Ctrl.BackColor = Color.Transparent;
                // faites ici les initialisation spécifiques du control affiché
                UpdateFromData();
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
                int iOutputValue = 0;
                DllCtrlDataCompProp SpecProp = m_SpecificProp as DllCtrlDataCompProp;
                switch (SpecProp.CompMode)
                {
                    case eCompareMode.cmp_AInfB:
                        if (m_ValueA < m_ValueB)
                            iOutputValue = 1;
                        else
                            iOutputValue = 0;
                        break;
                    case eCompareMode.cmp_AInfEqB:
                        if (m_ValueA <= m_ValueB)
                            iOutputValue = 1;
                        else
                            iOutputValue = 0;
                        break;
                    case eCompareMode.cmp_ASupB:
                        if (m_ValueA > m_ValueB)
                            iOutputValue = 1;
                        else
                            iOutputValue = 0;
                        break;
                    case eCompareMode.cmp_ASupEqB:
                        if (m_ValueA >= m_ValueB)
                            iOutputValue = 1;
                        else
                            iOutputValue = 0;
                        break;
                    case eCompareMode.cmp_ASupBSupC:
                        if (m_ValueA > m_ValueB && m_ValueB > m_ValueC)
                            iOutputValue = 1;
                        else
                            iOutputValue = 0;
                        break;
                    case eCompareMode.cmp_ASupEqBSupEqC:
                        if (m_ValueA >= m_ValueB && m_ValueB >= m_ValueC)
                            iOutputValue = 1;
                        else
                            iOutputValue = 0;
                        break;
                    default:
                        System.Diagnostics.Debug.Assert(false);
                        break;
                }
                if (iOutputValue != m_AssociateData.Value)
                {
                    m_AssociateData.Value = iOutputValue;
                    m_Executer.ExecuteScript(this.QuickScriptID);
                    if (m_bUseScreenEvent)
                    {
                        m_Parent.ControlEvent();
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Doc"></param>
        /// <returns></returns>
        public override bool FinalizeRead(BTDoc Doc)
        {
            bool bret = base.FinalizeRead(Doc);
            bool ParseRes = true;
            DllCtrlDataCompProp SpecProp = m_SpecificProp as DllCtrlDataCompProp;
            if (!string.IsNullOrEmpty(SpecProp.DataA))
            {
                ParseRes = int.TryParse(SpecProp.DataA, out m_ValueA);
                if (!ParseRes)
                {
                    m_dataA = Doc.GestData.GetFromSymbol(SpecProp.DataA) as Data;
                    if (m_dataA != null)
                        m_dataA.DataValueChanged += new EventDataValueChange(CompData_ValueChanged);
                }
            }

            if (!string.IsNullOrEmpty(SpecProp.DataB))
            {
                ParseRes = int.TryParse(SpecProp.DataB, out m_ValueB);
                if (!ParseRes)
                {
                    m_dataB = Doc.GestData.GetFromSymbol(SpecProp.DataB) as Data;
                    if (m_dataB != null)
                        m_dataB.DataValueChanged += new EventDataValueChange(CompData_ValueChanged);
                }
            }

            if (!string.IsNullOrEmpty(SpecProp.DataC))
            {
                ParseRes = int.TryParse(SpecProp.DataC, out m_ValueC);
                if (!ParseRes)
                {
                    m_dataC = Doc.GestData.GetFromSymbol(SpecProp.DataC) as Data;
                    if (m_dataC != null)
                        m_dataC.DataValueChanged += new EventDataValueChange(CompData_ValueChanged);
                }
            }
            return bret;
        }

        void CompData_ValueChanged()
        {
            if (m_dataA != null)
                m_ValueA = m_dataA.Value;
            if (m_dataB != null)
                m_ValueB = m_dataB.Value;
            if (m_dataC != null)
                m_ValueC = m_dataC.Value;

            this.UpdateFromDataDelegate();
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

    /// <summary>
    /// classe héritant de UserControl
    /// représente l'objet graphique affiché dans la supervision
    /// on peux en faire a peut près ce qu'on veux :
    /// - du dessin
    /// - une aggregation de plusieurs controls standards, 
    /// - les deux, etc.
    /// </summary>
    public class CtrlDataCompDispCtrl : UserControl
    {
        // ajouter ici les données membres du control affiché

        public CtrlDataCompDispCtrl()
        {
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // mettez ici le code de dessin du control
            base.OnPaint(e);
        }
    }
}

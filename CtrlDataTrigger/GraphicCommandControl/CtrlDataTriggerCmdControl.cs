using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using CommonLib;

namespace CtrlDataTrigger
{
    internal class CtrlDataTriggerCmdControl : BTDllCtrlDataTriggerControl
    {
        private enum TriggerState
        {
            STATE_ON,
            STATE_OFF
        };
        Data m_AssocDataOnToOff = null;
        Data m_AssocDataOffToOn = null;
        int m_iValueOnToOff = 0;
        int m_iValueOffToOn = 0;
        TriggerState m_TriggerState = TriggerState.STATE_OFF;
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public CtrlDataTriggerCmdControl(BTDoc document)
            : base(document)
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
                m_Ctrl = new CtrlDataTriggerDispCtrl();
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
            if (m_AssociateData != null && m_Ctrl != null)
            {
                // système normal ou il appel le script à chaque fois
                DllCtrlDataTriggerProp prop = (DllCtrlDataTriggerProp)m_SpecificProp;
                if (prop.BehaveLikeTrigger == false)
                {
                    if (this.m_ScriptContainer["EvtScript"].Length != 0)
                    {
                        m_Executer.ExecuteScript(this.m_iQuickScriptID);
                    }
                    if (m_bUseScreenEvent)
                    {
                        m_Parent.ControlEvent();
                    }
                }
                else
                {
                    UpdateTriggerInputValues();
                    //bFirstValueOK = true;
                    //m_AssociateData.Value;
                    TriggerState NextTriggerState = TriggerState.STATE_OFF;
                    int valeur_testee = m_AssociateData.Value;
                    if (m_iValueOnToOff == m_iValueOffToOn)
                    {
                        if (m_iValueOnToOff == valeur_testee)
                            NextTriggerState = TriggerState.STATE_ON;
                        else
                            NextTriggerState = TriggerState.STATE_OFF;
                    }
                    else
                    {
                        if (m_iValueOnToOff < m_iValueOffToOn)
                        {
                            if (valeur_testee >= m_iValueOffToOn)
                                NextTriggerState = TriggerState.STATE_ON;
                            else if (valeur_testee <= m_iValueOnToOff)
                                NextTriggerState = TriggerState.STATE_OFF;
                        }
                        else
                        {
                            if (valeur_testee <= m_iValueOffToOn)
                                NextTriggerState = TriggerState.STATE_ON;
                            else if (valeur_testee >= m_iValueOnToOff)
                                NextTriggerState = TriggerState.STATE_OFF;
                        }
                    }
                    if (NextTriggerState != m_TriggerState)
                    {
                        if (NextTriggerState == TriggerState.STATE_ON)
                        {
                            m_Executer.ExecuteScript(prop.QuickScriptIDOffToOn);
                        }
                        else
                        {
                            m_Executer.ExecuteScript(prop.QuickScriptIDOnToOff);
                        }

                        m_TriggerState = NextTriggerState;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void UpdateTriggerInputValues()
        {
            //on update que si on a une donnée associée (sinon c'est une constante
            if (m_AssocDataOnToOff != null)
                m_iValueOnToOff = m_AssocDataOnToOff.Value;

            if (m_AssocDataOffToOn != null)
                m_iValueOffToOn = m_AssocDataOffToOn.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Doc"></param>
        /// <returns></returns>
        public override bool FinalizeRead(BTDoc Doc)
        {
            bool bret = base.FinalizeRead(Doc);

            DllCtrlDataTriggerProp prop = (DllCtrlDataTriggerProp)m_SpecificProp;
            if (prop.BehaveLikeTrigger == true)
            {
                bool ParseRes = false;
                if (!string.IsNullOrEmpty(prop.DataOnToOff))
                {
                    ParseRes = int.TryParse(prop.DataOnToOff, out m_iValueOnToOff);
                    if (!ParseRes)
                    {
                        m_AssocDataOnToOff = (Data)Doc.GestData.GetFromSymbol(prop.DataOnToOff);
                        if (m_AssocDataOnToOff != null)
                        {
                            m_iValueOnToOff = m_AssocDataOnToOff.DefaultValue;
                            // ici on a pas besoin de vérifier InvokeRequired, car on ne change pas l'aspect du control
                            m_AssocDataOnToOff.DataValueChanged += new EventDataValueChange(UpdateFromData);
                        }
                    }
                }
                if (!string.IsNullOrEmpty(prop.DataOffToOn))
                {
                    ParseRes = int.TryParse(prop.DataOffToOn, out m_iValueOffToOn);
                    if (!ParseRes)
                    {
                        m_AssocDataOffToOn = (Data)Doc.GestData.GetFromSymbol(prop.DataOffToOn);
                        if (m_AssocDataOffToOn != null)
                        {
                            m_iValueOffToOn = m_AssocDataOffToOn.DefaultValue;
                            // ici on a pas besoin de vérifier InvokeRequired, car on ne change pas l'aspect du control
                            m_AssocDataOffToOn.DataValueChanged += new EventDataValueChange(UpdateFromData);
                        }
                    }
                }
            }
            return bret;
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
                        m_TriggerState = TriggerState.STATE_OFF;
                        UpdateFromData();
                        break;
                    case MESSAGE.MESS_PRE_PARSE:
                        DllCtrlDataTriggerProp prop = (DllCtrlDataTriggerProp)m_SpecificProp;
                        if (prop.ScriptOffToOn.Length != 0)
                            prop.QuickScriptIDOffToOn = m_Executer.PreParseScript(prop.ScriptOffToOn);
                        if (prop.ScriptOnToOff.Length != 0)
                            prop.QuickScriptIDOnToOff = m_Executer.PreParseScript(prop.ScriptOnToOff);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public class CtrlDataTriggerDispCtrl : UserControl
    {
        // ajouter ici les données membres du control affiché

        public CtrlDataTriggerDispCtrl()
        {
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // mettez ici le code de dessin du control
            base.OnPaint(e);
        }
    }
}

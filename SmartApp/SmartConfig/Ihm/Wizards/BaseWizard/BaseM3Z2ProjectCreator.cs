using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using CommonLib;

namespace SmartApp.Wizards
{
    public abstract class BaseM3Z2ProjectCreator
    {
        protected WizardConfigData m_WizConfig;

        protected BTDoc m_Document;

        public WizardConfigData WizConfig
        {
            set { m_WizConfig = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Document"></param>
        /// <param name="WizConfig"></param>
        public BaseM3Z2ProjectCreator(BTDoc Document)
        {
            m_Document = Document;
        }

        public abstract void CreateProjectFromWizConfig();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FuncName"></param>
        /// <param name="FrameNames"></param>
        /// <returns></returns>
        protected string CreateFrameFunction(string FuncName, StringCollection FrameNames)
        {
            Function func = new Function();
            func.Symbol = FuncName;
            string[] functionScripLines = new string[FrameNames.Count];
            for (int i = 0; i < FrameNames.Count; i++)
            {
                string FrameFuncToUse = ((i % 2) != 0) ? FRAME_FUNC.RECEIVE.ToString() : FRAME_FUNC.SEND.ToString();
                functionScripLines[i] = SCR_OBJECT.FRAMES.ToString() + "." +
                                        FrameNames[i] + "." +
                                        FrameFuncToUse + "()";

            }
            func.ScriptLines = functionScripLines;
            BaseObject obj = m_Document.GestFunction.GetFromSymbol(func.Symbol);
            if (obj == null)
            {
                m_Document.GestFunction.AddObj(func);
            }
            else
            {
                Function existingFunc = obj as Function;
                if (existingFunc.ScriptLines != func.ScriptLines)
                {
                    for (int indexFormat = 0; indexFormat < BaseGest.MAX_DEFAULT_ITEM_SYMBOL; indexFormat++)
                    {
                        //      - on crée une chaine temporaire = symbole + suffix de type "_AG{0}"
                        string strTempSymb = func.Symbol + string.Format("_AG{0}", indexFormat);
                        obj = m_Document.GestFunction.GetFromSymbol(strTempSymb);
                        if (obj == null)
                        {
                            func.Symbol = strTempSymb;
                            m_Document.GestFunction.AddObj(func);
                            break;
                        }
                    }
                }
                else
                {
                    // les fonctions sont identiques, rien a faire
                }
            }
            return func.Symbol;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Bloc"></param>
        /// <returns></returns>
        protected WIZ_SL_ADRESS_RANGE GetAddrRangeFromBloc(BlocConfig Bloc)
        {
            WIZ_SL_ADRESS_RANGE addrRange = WIZ_SL_ADRESS_RANGE.ADDR_1_8;
            switch (Bloc.Indice)
            {
                case 0:
                    addrRange = (Bloc.BlocType == BlocsType.IN) ? WIZ_SL_ADRESS_RANGE.ADDR_1_8 : WIZ_SL_ADRESS_RANGE.ADDR_25_32;
                    break;
                case 1:
                    addrRange = (Bloc.BlocType == BlocsType.IN) ? WIZ_SL_ADRESS_RANGE.ADDR_9_16 : WIZ_SL_ADRESS_RANGE.ADDR_33_40;
                    break;
                case 2:
                    addrRange = (Bloc.BlocType == BlocsType.IN) ? WIZ_SL_ADRESS_RANGE.ADDR_17_24 : WIZ_SL_ADRESS_RANGE.ADDR_41_48;
                    break;
                default:
                    System.Diagnostics.Debug.Assert(false);
                    break;
            }
            return addrRange;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TimerName"></param>
        /// <param name="functionName"></param>
        protected void CreateReadSLOutTimer(string TimerName, string functionName)
        {
            BTTimer timer = new BTTimer();
            timer.Symbol = TimerName;
            timer.Period = 1000;
            timer.AutoStart = true;
            string[] TimerScripLines = new string[1];
            TimerScripLines[0] = SCR_OBJECT.FUNCTIONS.ToString() + "." + functionName + "()";

            timer.ScriptLines = TimerScripLines;
            BaseObject obj = m_Document.GestTimer.GetFromSymbol(timer.Symbol);
            if (obj == null)
            {
                m_Document.GestTimer.AddObj(timer);
            }
            else
            {
                BTTimer existingFunc = obj as BTTimer;
                if (existingFunc.ScriptLines != timer.ScriptLines)
                {
                    for (int indexFormat = 0; indexFormat < BaseGest.MAX_DEFAULT_ITEM_SYMBOL; indexFormat++)
                    {
                        //      - on crée une chaine temporaire = symbole + suffix de type "_AG{0}"
                        string strTempSymb = timer.Symbol + string.Format("_AG{0}", indexFormat);
                        obj = m_Document.GestTimer.GetFromSymbol(strTempSymb);
                        if (obj == null)
                        {
                            timer.Symbol = strTempSymb;
                            m_Document.GestTimer.AddObj(timer);
                            break;
                        }
                    }
                }
                else
                {
                    // les timer sont identiques, rien a faire
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="screenName"></param>
        /// <param name="InitFunction"></param>
        protected void CreateDefaultScreen(string screenName, string InitFunction)
        {
            BTScreen screen = new BTScreen();
            screen.Symbol = screenName;
            string[] screenInitScriptLines = new string[1];
            screenInitScriptLines[0] = SCR_OBJECT.FUNCTIONS.ToString() + "." + InitFunction + "()";

            screen.InitScriptLines = screenInitScriptLines;
            BaseObject obj = m_Document.GestScreen.GetFromSymbol(screen.Symbol);
            if (obj == null)
            {
                m_Document.GestScreen.AddObj(screen);
            }
            else
            {
                BTScreen existingScreen = obj as BTScreen;
                if (existingScreen.InitScriptLines != screen.InitScriptLines)
                {
                    for (int indexFormat = 0; indexFormat < BaseGest.MAX_DEFAULT_ITEM_SYMBOL; indexFormat++)
                    {
                        //      - on crée une chaine temporaire = symbole + suffix de type "_AG{0}"
                        string strTempSymb = screen.Symbol + string.Format("_AG{0}", indexFormat);
                        obj = m_Document.GestTimer.GetFromSymbol(strTempSymb);
                        if (obj == null)
                        {
                            screen.Symbol = strTempSymb;
                            m_Document.GestTimer.AddObj(screen);
                            break;
                        }
                    }
                }
                else
                {
                    // les timer sont identiques, rien a faire
                }
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using CommonLib;

namespace SmartApp.Wizards
{

    /// <summary>
    /// classe de base pour la g�n�ration des projets issuent du wizard M3/Z2
    /// </summary>
    public abstract class BaseM3Z2ProjectCreator
    {
        /// <summary>
        /// donn�e de configuration du wizard
        /// </summary>
        protected WizardConfigData m_WizConfig;

        /// <summary>
        /// document de l'application ou cr�er les objets
        /// </summary>
        protected BTDoc m_Document;

        /// <summary>
        /// accesseur en assignation seul pour donner les donn�es de configuration
        /// au cr�ateur de projet
        /// </summary>
        public WizardConfigData WizConfig
        {
            set { m_WizConfig = value; }
        }

        /// <summary>
        /// constructeur par d�faut
        /// </summary>
        /// <param name="Document"></param>
        /// <param name="WizConfig"></param>
        public BaseM3Z2ProjectCreator(BTDoc Document)
        {
            m_Document = Document;
        }

        /// <summary>
        /// fonction charg� de la cr�ation de tout les objets configur� dans le wizard
        /// </summary>
        public abstract void CreateProjectFromWizConfig();

        /// <summary>
        /// cr�e et insert dans le docuement une fonction contenant les appels
        /// d'envoie et de r�c�ption des trames. Les trames doivent aller par deux 
        /// (requ�te et retour) dans la collection des nom de trame
        /// </summary>
        /// <param name="FuncName">Nom de la fonction � cr�er</param>
        /// <param name="FrameNames">Liste des symboles de trame</param>
        /// <returns>le nom de la fonction cr�e</returns>
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
                        //      - on cr�e une chaine temporaire = symbole + suffix de type "_AG{0}"
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
        /// Cr�e le timer de lecture p�riodique des bloc de sortie supervision
        /// </summary>
        /// <param name="TimerName">Nom du timer</param>
        /// <param name="functionName">nom de la fonction contenant les lectures</param>
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
                        //      - on cr�e une chaine temporaire = symbole + suffix de type "_AG{0}"
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
        /// cr�e l'�cran par d�faut avec le script d'init qui relit les blocs
        /// d'entr�e supervision.
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
                        //      - on cr�e une chaine temporaire = symbole + suffix de type "_AG{0}"
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

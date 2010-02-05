using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace CommonLib
{
    public partial class ScriptExecuter
    {
        #region execution des Screen
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void ParseExecuteScreen(string line)
        {
            string[] strTab = line.Split('.');
            if (strTab.Length > 2)
            {
                string strScreen = strTab[1];
                strScreen = strScreen.Trim();
                string strTemp = strTab[2];
                string strTempFull = strTemp;
                int posOpenParenthese = strTemp.LastIndexOf('(');
                if (posOpenParenthese >= 0)
                    strTemp = strTemp.Remove(posOpenParenthese);

                string strScrObject = strTemp;
                SCREEN_FUNC SecondTokenType = SCREEN_FUNC.INVALID;
                try
                {
                    SecondTokenType = (SCREEN_FUNC)Enum.Parse(typeof(SCREEN_FUNC), strScrObject);
                }
                catch (Exception)
                {
                    return;
                }
                switch (SecondTokenType)
                {
                    case SCREEN_FUNC.SHOW_ON_TOP:
                        ExecuteShowScreenToTop(strScreen);
                        break;
                }
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void ExecuteShowScreenToTop(string ScreenSymbol)
        {
            BTScreen ScreenToShow = (BTScreen)m_Document.GestScreen.QuickGetFromSymbol(ScreenSymbol);
            if (ScreenToShow != null)
            {
                ScreenToShow.ShowScreenToTop();
            }
            else
            {
                LogEvent log = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format("Unknown Secreen {0}", ScreenToShow));
                AddLogEvent(log);
            }
        }
        #endregion        
    }
}

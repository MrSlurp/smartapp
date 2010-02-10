using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace CommonLib
{
#if !QUICK_MOTOR
    public partial class ScriptExecuter
    {
        #region execution des fonctions
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void ExecuteFunctionScript(string FunctionSymbol)
        {
            FunctionSymbol = FunctionSymbol.Remove(FunctionSymbol.Length - 2);
            Function fct = (Function)m_Document.GestFunction.QuickGetFromSymbol(FunctionSymbol);
            if (fct != null)
            {
                CommonLib.PerfChrono theChrono = new PerfChrono();
                this.ExecuteScript(fct.ScriptLines);
                theChrono.EndMeasure("InstanceName = " + FunctionSymbol);
            }
            else
            {
                LogEvent log = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format("Unknown function {0}", FunctionSymbol));
                AddLogEvent(log);
            }
        }
        #endregion
        
    }
#endif
}

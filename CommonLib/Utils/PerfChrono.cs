using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace CommonLib
{
    public class PerfChrono
    {
        private Stopwatch m_Watch = null;
        public PerfChrono()
        {
            m_Watch = Stopwatch.StartNew();
        }
        public void EndMeasure()
        {
            EndMeasure(null);
        }

        public void EndMeasure(String strIndication)
        {
            long time = m_Watch.ElapsedMilliseconds;
            TimeSpan spentTime = m_Watch.Elapsed;
            StackTrace stackTrace = new StackTrace();
            StackFrame[] stackFrames = stackTrace.GetFrames();

            for (int i = 0; i < 2 && i < stackFrames.Length; i++)
            {
                Traces.LogAddDebug(TraceCat.PerfChrono, stackFrames[i].GetFileName() + "." + stackFrames[i].GetMethod().Name);
            }

            Traces.LogAddDebug(TraceCat.PerfChrono, "Execution time = " + spentTime.ToString() + "ms");
            if (!string.IsNullOrEmpty(strIndication))
                Traces.LogAddDebug(TraceCat.PerfChrono, strIndication);
        }
    }
}

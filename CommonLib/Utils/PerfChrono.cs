/*
    This file is part of SmartApp.

    SmartApp is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    SmartApp is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with Foobar.  If not, see <http://www.gnu.org/licenses/>.
*/
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

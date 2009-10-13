using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;
using CommonLib;

namespace CtrlSerpentin
{
    [Serializable]
    public class DllEntryClass : IDllControlInterface
    {
        public const uint Serpentin_Control_ID = 100;
        public uint DllID
        {
            get
            {
                return Serpentin_Control_ID;
            }
        }

        public BTControl CreateBTControl()
        {
            return new BTSerpentinControl();
        }

        public BTControl CreateBTControl(InteractiveControl iCtrl)
        {
            return new BTSerpentinControl(iCtrl);
        }
        public BTControl CreateCommandBTControl()
        {
            return new SerpentinControl();
        }

        public InteractiveControl CreateInteractiveControl()
        {
            return new InteractiveSerpentin();
        }

        public Size ToolWindSize
        {
            get
            {
                return new Size(130, 30);
            }
        }
        public string DefaultControlName 
        {
            get
            {
                return "SertpentinControl";
            }
        }

    }
}

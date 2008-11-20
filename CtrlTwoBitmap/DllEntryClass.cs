using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;
using CommonLib;

namespace CtrlTwoBitmap
{
    [Serializable]
    public class DllEntryClass : IDllControlInterface
    {
        public const uint TwoBitmap_Control_ID = 110;
        public DllEntryClass()
        {
            TwoImageRes.InitializeBitmap();
        }

        public uint DllID
        {
            get
            {
                return TwoBitmap_Control_ID;
            }
        }

        public BTControl CreateBTControl()
        {
            return new BTTwoBitmapControl();
        }

        public BTControl CreateBTControl(InteractiveControl iCtrl)
        {
            return new BTTwoBitmapControl(iCtrl);
        }
        public BTControl CreateCommandBTControl()
        {
            return new TwoBitmapControl();
        }

        public InteractiveControl CreateInteractiveControl()
        {
            return new InteractiveTwoBitmap();
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
                return "TwoBitmap";
            }
        }

    }
}

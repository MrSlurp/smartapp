﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;
using CommonLib;

namespace CtrlDataTrigger
{
    public class DllEntryClass : IDllControlInterface
    {
        // changes ici l'identifiant unique de la DLL
        public const uint DLL_Control_ID = 170;
        public DllEntryClass()
        {
            CtrlDataTriggerRes.InitializeBitmap();
        }

        public uint DllID
        {
            get
            {
                return DLL_Control_ID;
            }
        }

        public BTControl CreateBTControl()
        {
            return new BTDllCtrlDataTriggerControl();
        }

        public BTControl CreateBTControl(InteractiveControl iCtrl)
        {
            return new BTDllCtrlDataTriggerControl(iCtrl);
        }
        public BTControl CreateCommandBTControl()
        {
            return new CtrlDataTriggerCmdControl();
        }

        public InteractiveControl CreateInteractiveControl()
        {
            return new InteractiveCtrlDataTriggerDllControl();
        }

        public Size ToolWindSize
        {
            get
            {
                // modifiez ici la taille que le control aura dans la fenêtre d'outil
                return new Size(70, 30);
            }
        }

        public string DefaultControlName
        {
            get
            {
                // modifiez ici le nom par défaut de l'objet lors de sa création
                return "DataTrigger";
            }
        }

    }
}
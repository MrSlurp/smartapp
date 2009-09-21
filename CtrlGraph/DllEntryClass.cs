﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;
using CommonLib;

namespace CtrlGraph
{
    public class DllEntryClass : IDllControlInterface
    {
        // changes ici l'identifiant unique de la DLL
        public const uint DLL_Control_ID = 180;
        public DllEntryClass()
        {
            CtrlGraphRes.InitializeBitmap();
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
            return new BTDllCtrlGraphControl();
        }

        public BTControl CreateBTControl(InteractiveControl iCtrl)
        {
            return new BTDllCtrlGraphControl(iCtrl);
        }
        public BTControl CreateCommandBTControl()
        {
            return new CtrlGraphCmdControl();
        }

        public InteractiveControl CreateInteractiveControl()
        {
            return new InteractiveCtrlGraphDllControl();
        }

        public Size ToolWindSize
        {
            get
            {
                // modifiez ici la taille que le control aura dans la fenêtre d'outil
                return new Size(130, 50);
            }
        }

        public string DefaultControlName
        {
            get
            {
                // modifiez ici le nom par défaut de l'objet lors de sa création
                return "TwoBitmap";
            }
        }

    }
}
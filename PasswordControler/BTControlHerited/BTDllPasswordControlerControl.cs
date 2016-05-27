using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using CommonLib;

namespace PasswordControler
{
    internal class BTDllPasswordControlerControl : BasePluginBTControl
    {
        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        public BTDllPasswordControlerControl(BTDoc document)
            : base(document)
        {
            m_IControl = new InteractivePasswordControlerDllControl();
            if (m_IControl != null)
                m_IControl.SourceBTControl = this;

            m_SpecificProp = new DllPasswordControlerProp(this.ItemScripts);
        }

        /// <summary>
        /// Constructeur de la à partir d'un control interactif
        /// </summary>
        public BTDllPasswordControlerControl(BTDoc document, InteractiveControl Ctrl)
            : base(document, Ctrl)
        {
            m_SpecificProp = new DllPasswordControlerProp(this.ItemScripts);
        }

        /// <summary>
        /// Renvoie l'ID de la DLL
        /// </summary>
        public override uint DllControlID
        {
            get
            {
                return PasswordControler.DllEntryClass.DLL_Control_ID;
            }
        }
    }
}

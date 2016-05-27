using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using CommonLib;

namespace SoundPlayer
{
    internal class BTDllSoundPlayerControl : BasePluginBTControl
    {
        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        public BTDllSoundPlayerControl(BTDoc document)
            : base(document)
        {
            m_IControl = new InteractiveSoundPlayerDllControl();
            if (m_IControl != null)
                m_IControl.SourceBTControl = this;

            m_SpecificProp = new DllSoundPlayerProp(this.ItemScripts);
        }

        /// <summary>
        /// Constructeur de la à partir d'un control interactif
        /// </summary>
        public BTDllSoundPlayerControl(BTDoc document, InteractiveControl Ctrl) 
            : base (document, Ctrl)
        {
            m_SpecificProp = new DllSoundPlayerProp(this.ItemScripts);
        }

        /// <summary>
        /// Renvoie l'ID de la DLL
        /// </summary>
        public override uint DllControlID
        {
            get
            {
                return SoundPlayer.DllEntryClass.DLL_Control_ID;
            }
        }
    }
}

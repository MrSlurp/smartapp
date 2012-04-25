using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using CommonLib;

namespace ScreenItemLocker
{
    internal class BTDllScreenItemLockerControl : BasePluginBTControl
    {
        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        public BTDllScreenItemLockerControl(BTDoc document)
            : base(document)
        {
            m_IControl = new InteractiveScreenItemLockerDllControl();
            if (m_IControl != null)
                m_IControl.SourceBTControl = this;

            m_SpecificProp = new DllScreenItemLockerProp(this.ItemScripts);
        }

        /// <summary>
        /// Constructeur de la à partir d'un control interactif
        /// </summary>
        public BTDllScreenItemLockerControl(BTDoc document, InteractiveControl Ctrl)
            : base(document, Ctrl)
        {
            m_SpecificProp = new DllScreenItemLockerProp(this.ItemScripts);
        }

        /// <summary>
        /// Renvoie l'ID de la DLL
        /// </summary>
        public override uint DllControlID
        {
            get
            {
                return ScreenItemLocker.DllEntryClass.DLL_Control_ID;
            }
        }

        /// <summary>
        /// renvoie true si le control à au moins une donnée associé qui rend son état dynamique
        /// Utilisé pour masquer les controls lors de l'enregistrement de l'image de fond de plan
        /// </summary>
        public override bool HaveDataAssociation
        {
            get
            {
                return true;
            }
        } 
    }
}

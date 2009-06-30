using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using CommonLib;


namespace CtrlDataTrigger
{
    class DllCtrlDataTriggerProp : SpecificControlProp
    {
        // ajouter ici les données membres des propriété

        // ajouter ici les accesseur vers les données membres des propriété

        public override bool ReadIn(System.Xml.XmlNode Node)
        {
            return true;
        }

        public override bool WriteOut(XmlDocument XmlDoc, XmlNode Node)
        {
            return true;
        }

        public override void CopyParametersFrom(SpecificControlProp SrcSpecificProp)
        {
            // ne fait rien, aucun paramètre spéciaux (pour le moment)
        }
    }
}

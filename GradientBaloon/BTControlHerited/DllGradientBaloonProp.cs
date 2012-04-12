using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using CommonLib;


namespace GradientBaloon
{
    class DllGradientBaloonProp : SpecificControlProp
    {
        // ajouter ici les données membres des propriété

        // ajouter ici les accesseur vers les données membres des propriété
        public DllGradientBaloonProp(ItemScriptsConainter scriptContainter) : base(scriptContainter) { }


        public override bool ReadIn(XmlNode Node, BTDoc document)
        {
            return true;
        }

        public override bool WriteOut(XmlDocument XmlDoc, XmlNode Node, BTDoc document)
        {
            return true;
        }

        public override void CopyParametersFrom(SpecificControlProp SrcSpecificProp, bool bFromOtherInstance, BTDoc document)
        {
        }

    }
}

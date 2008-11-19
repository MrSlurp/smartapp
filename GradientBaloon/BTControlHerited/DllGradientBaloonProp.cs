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

        public override bool ReadIn(System.Xml.XmlNode Node)
        {
            return true;
        }

        public override bool WriteOut(XmlDocument XmlDoc, XmlNode Node)
        {
            return true;
        }
    }
}

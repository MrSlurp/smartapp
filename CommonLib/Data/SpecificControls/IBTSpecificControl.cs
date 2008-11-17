﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace CommonLib
{
    interface IBTSpecificControl
    {
        SpecificControlProp SpecBTControlProp { get;}
    }

    public static class SpecificControlParser
    {
        public static BTControl ParseAndCreateSpecificControl(XmlNode Node)
        {
            BTControl newControl = null;
            XmlNode AttrType = Node.Attributes.GetNamedItem(XML_CF_ATTRIB.SpecificType.ToString());
            SPECIFIC_TYPE TypeId = SPECIFIC_TYPE.NULL;
            // on parse le type de control
            try
            {
                TypeId = (SPECIFIC_TYPE)Enum.Parse(typeof(SPECIFIC_TYPE), AttrType.Value, true);
            }
            catch (Exception)
            {
                // en cas de tag non reconne dans l'enum, une exeption est levée, 
                // on la récupère car ca peut arriver
                Console.WriteLine("Impossible de parser le type de controle specifique");
            }

            switch (TypeId)
            {
                case SPECIFIC_TYPE.FILLED_RECT:
                    newControl = new BTFilledRectControl();
                    break;
                case SPECIFIC_TYPE.FILLED_ELLIPSE:
                    newControl = new BTFilledEllipseControl();
                    break;
                case SPECIFIC_TYPE.NULL:
                default:
                    Console.WriteLine("Type de control indéfini");
                    return null;
            }
            return newControl;
        }

        public static SPECIFIC_TYPE ParseSpecificControlType(XmlNode Node)
        {
            XmlNode AttrType = Node.Attributes.GetNamedItem(XML_CF_ATTRIB.SpecificType.ToString());
            SPECIFIC_TYPE TypeId = SPECIFIC_TYPE.NULL;
            // on parse le type de control
            try
            {
                TypeId = (SPECIFIC_TYPE)Enum.Parse(typeof(SPECIFIC_TYPE), AttrType.Value, true);
            }
            catch (Exception)
            {
                // en cas de tag non reconne dans l'enum, une exeption est levée, 
                // on la récupère car ca peut arriver
            }

            return TypeId;
        }

        public static uint ParseDllID(XmlNode Node)
        {
            XmlNode AttrId = Node.Attributes.GetNamedItem(XML_CF_ATTRIB.DllID.ToString());
            return uint.Parse(AttrId.Value);
        }
    }
}
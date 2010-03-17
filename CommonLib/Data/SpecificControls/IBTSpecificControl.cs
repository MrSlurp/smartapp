using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace CommonLib
{
    /// <summary>
    /// classe utilitaire static contenant des fonction utilitaire pour déterminer le type de controle spécifique 
    /// ou l'identifiant d'un DLL contenu dans le fichier XML
    /// </summary>
    public static class SpecificControlParser
    {
        #region méthodes pour les constroles spécifiques
        /// <summary>
        /// détermine et crée un BTControle spécifique en fonction de l'attribut du fichier XML
        /// </summary>
        /// <param name="Node">Noeud du control contenu dans le fichier XML</param>
        /// <returns>le BTcontrol spécifique ou null</returns>
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
                Traces.LogAddDebug(TraceCat.Serialization, "Impossible de parser le type de controle specifique");
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

        /// <summary>
        /// détermine le type de  BTControle spécifique en fonction de l'attribut du fichier XML
        /// </summary>
        /// <param name="Node">Noeud du control contenu dans le fichier XML</param>
        /// <returns>le type spécifique ou SPECIFIC_TYPE.NULL</returns>
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
                Traces.LogAddDebug(TraceCat.CommonLib, "Impossible de parser le type de controle specifique");
            }

            return TypeId;
        }
        #endregion

        #region méthodes pour les controls DLLs
        /// <summary>
        /// détermine l'identifiant DLL de l'objet DLL control en fonction de l'attribut du fichier XML
        /// </summary>
        /// <param name="Node">Noeud du control contenu dans le fichier XML</param>
        /// <returns>l'identifiant unique de la DLL ou 0</returns>
        public static uint ParseDllID(XmlNode Node)
        {
            XmlNode AttrId = Node.Attributes.GetNamedItem(XML_CF_ATTRIB.DllID.ToString());
            if (AttrId != null)
                return uint.Parse(AttrId.Value);
            else
                return 0;
        }
        #endregion
    }
}

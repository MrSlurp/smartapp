/***************************************************************************/
// PROJET : BTCommand : system de commande paramétrable pour équipement
// ayant une mécanisme de commande par liaison série/ethernet/http
/***************************************************************************/
// Fichier : 
/***************************************************************************/
// description :
// fichier contenant les "#define" et enum de l'application
/***************************************************************************/
using System;

namespace BTConfig2.Datas
{
    /// <summary>
    /// classe contenant divers constantes utilisées par l'application
    /// </summary> 
    public class Cste
    {
        public const int MAX_DATA_SIZE = 16;
        public const int MAX_COMBO_ITEMS = 16;
        public const string STR_SUFFIX_CTRLDATA = "_CTRLDATA";

        public const int SIZE_1_BYTE = 8;
        public const int SIZE_2_BYTE = 16;
        public const int SIZE_4_BYTE = 32;
    }

    /// <summary>
    /// enum de la taille des données
    /// </summary>
    enum DATA_SIZE
    {
        DATA_SIZE_NULL = 0,
        DATA_SIZE_1B = 1,
        DATA_SIZE_2B = 2,
        DATA_SIZE_4B = 4,
        DATA_SIZE_8B = 8,
        DATA_SIZE_16B = 16,
        DATA_SIZE_32B = 32,
    }

    /// <summary>
    /// enum des tag du fichier de config (Config File (CF))
    /// ces énums doivent être maintenu en meme temps que ceux de CXmlFileParser de BTConfig
    /// </summary>
    enum XML_CF_TAG
    {
        Root,
        FileHeader,
        FileVersion,
        SoftVersion,
        DataSection,
        Data,
        ScreenSection,
        Screen,
        ControlList,
        InitActionList,
        QuitActionList,
        EventActionList,
        TrameSection,
        Trame,
        DataConvert,
        ControlData,
        DataList,
        ControlSection,
        ActionSection,
        Action,
        ActionParameters,
        Control,
        GroupSection,
        Group,
        Object,
    }
    /// <summary>
    /// enum des attributs du ficher de config (Config File (CF))
    /// ces énums doivent être maintenu en meme temps que ceux de CXmlFileParser de BTConfig
    /// </summary>
    enum XML_CF_ATTRIB
    {
        strNom,
        strSymbol,
        size,
        Type,
        Min,
        Max,
        DefVal,
        Text,
        AssociateData,
        ScreenEvent,
        DataAlign,
        From,
        To,
        Used,
        Param,
        Constant,
        bkColor,
        Indice,
        Release,
        Pos,
    }

    /// <summary>
    /// Types de contols disponibles
    /// les chaines doivent correspondre a ce qui est écrit dans le fichier XML
    /// </summary>
    enum CONTROL_TYPE
    {
        NULL,
        CHECK,
        SLIDER,
        COMBO,
        BUTTON,
        STATIC,
        UP_DOWN,
    }

    /// <summary>
    /// Types de calcule de la donnée de control dans les trames
    /// les chaines doivent correspondre a ce qui est écrit dans le fichier XML
    /// </summary>
    enum CTRLDATA_TYPE
    {
        NONE,
        SUM_COMPL_P1,
        SUM_COMPL_P2,
    }

    /// <summary>
    /// Types de conversion dans les trames
    /// les chaines doivent correspondre a ce qui est écrit dans le fichier XML
    /// </summary>
    enum CONVERT_TYPE
    {
        NONE,
        ASCII,
    }

    /// <summary>
    /// type des action pouvant être effectué par les écran (évolution: par les controls)
    /// les chaines doivent correspondre a ce qui est écrit dans le fichier XML
    /// </summary>
    enum ACTION_TYPE
    {
        ADD,
        SUB,
        MUL,
        DIV,
        NOT,
        SEND,
        RECIEVE,
    }
}
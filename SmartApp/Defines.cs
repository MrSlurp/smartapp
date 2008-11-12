/***************************************************************************/
// PROJET : BTCommand : system de commande param�trable pour �quipement
// ayant une m�canisme de commande par liaison s�rie/ethernet/http
/***************************************************************************/
// Fichier : 
/***************************************************************************/
// description :
// fichier contenant les "#define" et enum de l'application
/***************************************************************************/
using System;
using System.Drawing;

namespace SmartApp
{
    /// <summary>
    /// classe contenant divers constantes utilis�es par l'application
    /// </summary> 
    public class Cste
    {
        public const int MAX_DATA_SIZE = 16;
        public const int MAX_COMBO_ITEMS = 16;
        public const string STR_SUFFIX_CTRLDATA = "_CTRLDATA";

        public const int SIZE_1_BYTE = 8;
        public const int SIZE_2_BYTE = 16;
        public const int SIZE_4_BYTE = 32;

        public const string STRCMD_CFG = "-CFG";
        public const string STRCMD_CMD = "-CMD";
        public const string STRCMD_AUTOCONNECT = "-AC";
        public const string STRCMD_AUTOSTART = "-AS";

        public const int CUR_FILE_VERSION = 3;

        public const int FILE_VERSION_V1000 = 1;
        public const int FILE_VERSION_V1001 = 2;
        public const int FILE_VERSION_V1201 = 3;

        public const int NB_MAX_COMM = 255;
        public const string STR_COMINI_FILENAME = "ConfigComm.ini";
        public const string STR_OPTINI_FILENAME = "Options.ini";
        public const string STR_FORMPOSINI_FILENAME = "Forms.ini";
        public const string STR_FILE_DESC_HEADER_FORMAT = "Comm{0}";
        public const string STR_FILE_DESC_NAME = "Name";
        public const string STR_FILE_DESC_COMM = "Comm";
        public const string STR_FILE_DESC_ADDR = "Addr";

        public const string STR_FILE_DESC_HEADER_OPT = "Options";
        public const string STR_FILE_DESC_LOGDIR = "LogDir";
        public const string STR_FILE_DESC_SAVE_PREF_COMM = "SaveComm";
        public static Color TransparencyColor = Color.FromArgb(255, 0, 255);
    }

    public enum TYPE_APP
    {
        NONE,
        SMART_COMMAND,
        SMART_CONFIG,
    }

    /// <summary>
    /// enum de la taille des donn�es
    /// </summary>
    enum DATA_SIZE
    {
        DATA_SIZE_NULL = 0x0000,
        DATA_SIZE_1B = 0x0001,
        DATA_SIZE_2B = 0x0002,
        DATA_SIZE_4B = 0x0004,
        DATA_SIZE_8B = 0x0008,
        DATA_SIZE_16B = 0x0010,
        DATA_SIZE_32B = 0x0020,
        DATA_SIZE_16BU = 0x0110,
    }

    /// <summary>
    /// enum des tag du fichier de config (Config File (CF))
    /// ces �nums doivent �tre maintenu en meme temps que ceux de CXmlFileParser de BTConfig
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
        InitScript,
        EventScript,
        TrameSection,
        Trame,
        DataConvert,
        ControlData,
        DataList,
        ControlSection,
        Control,
        GroupSection,
        Group,
        Object,
        FunctionSection,
        Function,
        TimerSection,
        Timer,
        LoggerSection,
        Logger,
        Line,
        ImagePath,
        Program,
        SpecificControl,
    }
    /// <summary>
    /// enum des attributs du ficher de config (Config File (CF))
    /// ces �nums doivent �tre maintenu en meme temps que ceux de CXmlFileParser de BTConfig
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
        LoggerType,
        Period,
        FileName,
        ReadOnly,
        AutoStart,
        ActiveColor,
        InactiveColor,
        SpecificType,
    }

    /// <summary>
    /// Types de contols disponibles
    /// les chaines doivent correspondre a ce qui est �crit dans le fichier XML
    /// </summary>
    public enum CONTROL_TYPE
    {
        NULL,
        CHECK,
        SLIDER,
        COMBO,
        BUTTON,
        STATIC,
        UP_DOWN,
        SPECIFIC,
    }

    public enum SPECIFIC_TYPE
    {
        NULL,
        FILLED_RECT,
    }

    /// <summary>
    /// Types de calcule de la donn�e de control dans les trames
    /// les chaines doivent correspondre a ce qui est �crit dans le fichier XML
    /// </summary>
    public enum CTRLDATA_TYPE
    {
        NONE,
        SUM_COMPL_P1,
        SUM_COMPL_P2,
        MODBUS_CRC,
    }

    /// <summary>
    /// Types de conversion dans les trames
    /// les chaines doivent correspondre a ce qui est �crit dans le fichier XML
    /// </summary>
    public enum CONVERT_TYPE
    {
        NONE,
        ASCII,
    }

    public enum LOGGER_TYPE
    {
        NONE,
        STANDARD,
        AUTO,
    }
}
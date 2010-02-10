using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace CommonLib
{

    #region enums
    /// <summary>
    /// Liste de tout les types de jetons (token) 
    /// </summary>
    public enum TOKEN_TYPE
    {
        NULL,
        SCR_OBJECT,
        FRAME,
        TIMER,
        LOGGER,
        FUNCTION,
        LOGGER_FUNC,
        TIMER_FUNC,
        FRAME_FUNC,
        MATHS,
        MATHS_FUNC,
        DATA,
        SCREEN,
        SCREEN_FUNC,
        LOGIC,
        LOGIC_FUNC
    }

    /// <summary>
    /// types "sources" du script (mot clef du langage script)  
    /// </summary>
    public enum SCR_OBJECT
    {
        INVALID,
        FRAMES,
        FUNCTIONS,
        LOGGERS,
        TIMERS,
        MATHS,
        SCREEN,
        LOGIC
    }

    /// <summary>
    /// liste des fonctions sur les trames 
    /// </summary>
    public enum FRAME_FUNC
    {
        INVALID,
        SEND,
        RECEIVE,
    }

    /// <summary>
    /// liste des fonctions sur les trames 
    /// </summary>
    public enum LOGGER_FUNC
    {
        INVALID,
        LOG,
        CLEAR,
        START,
        STOP,
    }

    /// <summary>
    /// liste des fonctions sur les timers
    /// </summary>
    public enum TIMER_FUNC
    {
        INVALID,
        START,
        STOP
    }

    /// <summary>
    /// liste des fonctions mathématiques
    /// </summary>
    public enum MATHS_FUNC
    {
        INVALID,
        ADD,
        SUB,
        MUL,
        DIV,
    }

    /// <summary>
    /// liste des fonctions logiques
    /// </summary>
    public enum LOGIC_FUNC
    {
        INVALID,
        NOT,
        AND,
        OR,
        NAND,
        NOR,
        XOR,
    }

    /// <summary>
    /// liste des fonctions sur les écrans
    /// </summary>
    public enum SCREEN_FUNC
    {
        INVALID,
        SHOW_ON_TOP,
    }

    public enum ALL_FUNC
    {
        INVALID,
        FRAME_SEND,
        FRAME_RECEIVE,
        LOGGER_LOG,
        LOGGER_CLEAR,
        LOGGER_START,
        LOGGER_STOP,
        TIMER_START,
        TIMER_STOP,
        MATHS_ADD,
        MATHS_SUB,
        MATHS_MUL,
        MATHS_DIV,
        LOGIC_NOT,
        LOGIC_AND,
        LOGIC_OR,
        LOGIC_NAND,
        LOGIC_NOR,
        LOGIC_XOR,
        SCREEN_SHOW_ON_TOP,
    }

    /// <summary>
    /// liste des types d'erreurs
    /// </summary>
    public enum ErrorType
    {
        NO_ERROR,
        ERROR,
        WARNING
    }
    #endregion

    class ParseExecGlobals
    {
        public const char TOKEN_SEPARATOR = '.';
        public const char PARAM_SEPARATOR = ',';
    }
}

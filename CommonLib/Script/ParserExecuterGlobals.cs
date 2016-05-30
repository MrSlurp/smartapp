/*
    This file is part of SmartApp.

    SmartApp is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    SmartApp is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with Foobar.  If not, see <http://www.gnu.org/licenses/>.
*/
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
        LOGIC_FUNC,
        SYSTEM,
        SYSTEM_FUNC
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
        LOGIC,
        SYSTEM
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
        NEW_FILE,
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
        COS,
        SIN,
        TAN,
        SQRT,
        POW,
        LN,
        LOG,
        SET,
        MOD,
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
        SCREEN_SHOT,
        HIDE,
        SHOW,
    }

    public enum SYSTEM_FUNC
    {
        INVALID,
        SHELL_EXEC,
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
        LOGGER_NEW_FILE,
        TIMER_START,
        TIMER_STOP,
        MATHS_ADD,
        MATHS_SUB,
        MATHS_MUL,
        MATHS_DIV,
        MATHS_COS,
        MATHS_SIN,
        MATHS_TAN,
        MATHS_SQRT,
        MATHS_POW,
        MATHS_LN,
        MATHS_LOG,
        MATHS_SET,
        MATHS_MOD,
        LOGIC_NOT,
        LOGIC_AND,
        LOGIC_OR,
        LOGIC_NAND,
        LOGIC_NOR,
        LOGIC_XOR,
        SCREEN_SHOW_ON_TOP,
        SCREEN_HIDE,
        SCREEN_SHOW,
        SCREEN_SCREEN_SHOT,
        SYSTEM_SHELL_EXEC
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

using System;

namespace WBSoft.LocalDBApi
{
    /// <summary>
    /// Contains flags to control shutdown behaviour
    /// </summary>
    [Flags]
    public enum ShutdownFlag
    {
        /// <summary>
        /// Apply the default shutdown behaviour when no flag specified (use the SHUTDOWN Transact-SQL command)
        /// </summary>
        Shutdown = 0,

        /// <summary>
        /// Shutdown immediately using the kill process operating system command
        /// </summary>
        KillProcess = 1,

        /// <summary>
        /// Use the SHUTDOWN Transact-SQL command using the WITH NOWAIT option
        /// </summary>
        ShutdownNoWait = 2,
    }
}

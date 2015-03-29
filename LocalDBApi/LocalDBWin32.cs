using System;
using System.Runtime.InteropServices;

namespace WBSoft.LocalDBApi
{
    /// <summary>
    /// Contains the Win32 interop
    /// </summary>
    internal class LocalDBWin32
    {
        /// <summary>
        /// "SqlUserInstance" - The file name of the dll that contains the LocalDB dll we want to interact with
        /// </summary>
        public const string SqlUserInstanceDllFileName = "SqlUserInstance";

        [DllImport("kernel32", SetLastError=true, CharSet=CharSet.Ansi)]
        public static extern IntPtr LoadLibrary(string lpFileName);
        
        [DllImport(SqlUserInstanceDllFileName, CharSet=CharSet.Unicode)]
        public static extern long LocalDBCreateInstance(string wszVersion, string pInstanceName, int dwFlags);
    }
}
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace WBSoft.LocalDBApi
{
    /// <summary>
    /// Contains the Win32 interop
    /// </summary>
    /// <remarks>Methods which return a HRESULT are mapped to uint.  This is because we can't define in C# hex values which overflow.</remarks>
    internal class LocalDBWin32
    {
        /// <summary>
        /// "SqlUserInstance" - The file name of the dll that contains the LocalDB dll we want to interact with
        /// </summary>
        public const string SqlUserInstanceDllFileName = "SqlUserInstance";

        [DllImport("kernel32", SetLastError=true, CharSet=CharSet.Ansi)]
        public static extern IntPtr LoadLibrary(string lpFileName);

        /// <summary>
        /// LocalDB's FormatMessage implementation
        /// </summary>
        [DllImport(SqlUserInstanceDllFileName, CharSet=CharSet.Unicode)]
        public static extern int LocalDBFormatMessage(int hrLocalDB, uint dwFlags, uint dwLanguageId, [Out] StringBuilder messageBuffer, ref int messageBufferLength);

        [DllImport(SqlUserInstanceDllFileName, CharSet=CharSet.Unicode)]
        public static extern int LocalDBCreateInstance(string wszVersion, string pInstanceName, int dwFlags);

        [DllImport(SqlUserInstanceDllFileName, CharSet=CharSet.Unicode)]
        public static extern int LocalDBDeleteInstance(string pInstanceName, int dwFlags);
    }
}
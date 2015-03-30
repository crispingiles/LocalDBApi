namespace WBSoft.LocalDBApi
{
    /// <summary>
    /// Contains the various return codes from calling a LocalDB
    /// </summary>
    /// <remarks>uint so we can use the hex constants from the sqlncli.h without converting</remarks>
    public static class LocalDBReturnCode
    {
        /// <summary>
        /// 0 - The return code indicating success
        /// </summary>
        public const int S_OK = 0;
    }
}
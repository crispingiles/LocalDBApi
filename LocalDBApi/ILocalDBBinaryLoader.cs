namespace WBSoft.LocalDBApi
{
    /// <summary>
    /// Used to load the SqlUserInstance.DLL into the current instance
    /// </summary>
    internal interface ILocalDBBinaryLoader
    {
        /// <summary>
        /// Attempt to load the most recent version of SqlUserInstance.DLL installed.  Can repeatedly call
        /// </summary>
        void LoadMostRecentVersion();

        /// <summary>
        /// Attempt to load the supplied version of SqlUserInstance.DLL.  Can repeatedly call for the same version, but does not support loading multiple versions
        /// </summary>
        void LoadVersion(string versionName);

        /// <summary>
        /// Attempt to load the supplied version of SqlUserInstance.DLL.  Can repeatedly call for the same version, 
        /// </summary>
        void LoadVersion(decimal versionNumber);
        
        /// <summary>
        /// Attempt to load the supplied version of SqlUserInstance.DLL.  Can repeatedly call for the same version, but does not support loading multiple versions
        /// </summary>
        void LoadVersion(LocalDBVersion localDBVersion);

    }
}

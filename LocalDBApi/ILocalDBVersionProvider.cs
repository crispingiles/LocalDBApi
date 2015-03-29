using System.Collections.Generic;

namespace WBSoft.LocalDBApi
{
    /// <summary>
    /// Used to find available versions of LocalDB
    /// </summary>
    public interface ILocalDBVersionProvider
    {
        /// <summary>
        /// Query the registry for the list of installed SQL Server Express Local DB instances
        /// </summary>
        IReadOnlyList<LocalDBVersion> GetInstalledVersions();
    }
}
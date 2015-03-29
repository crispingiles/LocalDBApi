using System;
using System.Collections.Generic;
using Microsoft.Win32;

namespace WBSoft.LocalDBApi
{
    /// <summary>
    /// Used to find available versions of LocalDB
    /// </summary>
    public class LocalDBVersionProvider : ILocalDBVersionProvider
    {
        /// <summary>
        /// Create an instance to find available versions of LocalDB
        /// </summary>
        public LocalDBVersionProvider()
        {
        }

        private readonly string[] subKeyNames = {"SOFTWARE", "Microsoft", "Microsoft SQL Server Local DB", "Installed Versions"};

        public IReadOnlyList<LocalDBVersion> GetInstalledVersions()
        {
            try
            {
                var currentKey = Registry.LocalMachine;
                foreach (var subKeyName in subKeyNames)
                {
                    currentKey = currentKey.OpenSubKey(subKeyName, false);
                    if (currentKey == null)
                    {
                        return new List<LocalDBVersion>();
                    }
                }
                var result = new List<LocalDBVersion>();
                foreach (var versionName in currentKey.GetSubKeyNames())
                {
                    var versionKey = currentKey.OpenSubKey(versionName, false);
                    var instanceAPIPath = (string) versionKey.GetValue("InstanceAPIPath");
                    var parentInstance = (string) versionKey.GetValue("ParentInstance");
                    result.Add(new LocalDBVersion(versionName, instanceAPIPath, parentInstance));
                }
                return result;
            }
            catch (Exception exc)
            {
                throw new Exception("Unable to get installed versions of LocalDB", exc);
            } 
        }
    }
}
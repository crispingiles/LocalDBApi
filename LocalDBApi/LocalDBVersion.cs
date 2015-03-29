namespace WBSoft.LocalDBApi
{
    /// <summary>
    /// Contains information about a installation of LocalDB
    /// </summary>
    public class LocalDBVersion
    {
        public LocalDBVersion(string versionName, string instanceAPIPath, string parentInstance)
        {
            this.versionName = versionName;
            if (!decimal.TryParse(versionName, out versionNumber))
            {
                versionNumber = 0.0m;
            }
            this.instanceAPIPath = instanceAPIPath;
            this.parentInstance = parentInstance;
        }

        private readonly string versionName;

        private readonly decimal versionNumber;

        private readonly string instanceAPIPath;

        private readonly string parentInstance;

        public string VersionName { get { return versionName; } }

        /// <summary>
        /// The value of <see cref="VersionNumber"/> parsed as a decimal.  Returns 0 if <see cref="VersionNumber"/> could not be parsed
        /// </summary>
        public decimal VersionNumber { get { return versionNumber; } }

        public string InstanceAPIPath { get { return instanceAPIPath; } }

        public string ParentInstance { get { return parentInstance; } }
    }
}
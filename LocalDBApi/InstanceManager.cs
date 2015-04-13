using System;
using System.Text;

namespace WBSoft.LocalDBApi
{
    /// <summary>
    /// Used to control instances of SQL Server Express Local DB
    /// </summary>
    public class InstanceManager : IInstanceManager
    {

        public InstanceManager()
            : this(new LocalDBBinaryLoader(), new LocalDBErrorProvider())
        {
        }

        internal InstanceManager(
            ILocalDBBinaryLoader localDBBinaryLoader, 
            ILocalDBErrorProvider localDBErrorProvider)
        {
            this.localDBBinaryLoader = localDBBinaryLoader;
            this.localDBErrorProvider = localDBErrorProvider;
        }

        private readonly ILocalDBBinaryLoader localDBBinaryLoader;

        private readonly ILocalDBErrorProvider localDBErrorProvider;

        public void CreateInstance(string localDBVersion, string instanceName)
        {
            localDBBinaryLoader.LoadVersion(localDBVersion);
            var errorCode = LocalDBWin32.LocalDBCreateInstance(localDBVersion, instanceName, 0);
            if (errorCode != LocalDBReturnCode.S_OK)
            {
                var error = localDBErrorProvider.GetError(errorCode);
                throw new Exception(string.Format("Error creating LocalDB instance {0}: {1}", instanceName, error.ErrorMessage));
            }
        }

        public void DeleteInstance(string instanceName)
        {
            localDBBinaryLoader.LoadMostRecentVersion();
            var errorCode = LocalDBWin32.LocalDBDeleteInstance(instanceName, 0);
            if (errorCode != LocalDBReturnCode.S_OK)
            {
                var error = localDBErrorProvider.GetError(errorCode);
                throw new Exception(string.Format("Error deleting LocalDB instance {0}: {1}", instanceName, error.ErrorMessage));
            }
        }

        public string StartInstance(string instanceName)
        {
            localDBBinaryLoader.LoadMostRecentVersion();
            int connectionStringBufferLength = 1024;
            var connectionString = new StringBuilder(connectionStringBufferLength);
            var errorCode = LocalDBWin32.LocalDBStartInstance(instanceName, 0, connectionString, ref connectionStringBufferLength);
            if (errorCode != LocalDBReturnCode.S_OK)
            {
                var error = localDBErrorProvider.GetError(errorCode);
                throw new Exception(string.Format("Error starting LocalDB instance {0}: {1}", instanceName, error.ErrorMessage));
            }
            return connectionString.ToString();
        }

        public void StopInstance(string instanceName, ShutdownFlag shutdownFlag, TimeSpan timeout)
        {
            localDBBinaryLoader.LoadMostRecentVersion();
            var errorCode = LocalDBWin32.LocalDBStopInstance(instanceName, (int) shutdownFlag, (uint) timeout.TotalSeconds);
            if (errorCode != LocalDBReturnCode.S_OK)
            {
                var error = localDBErrorProvider.GetError(errorCode);
                throw new Exception(string.Format("Error stopping LocalDB instance {0}: {1}", instanceName, error.ErrorMessage));
            }
        }
    }
}

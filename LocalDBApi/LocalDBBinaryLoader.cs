using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace WBSoft.LocalDBApi
{
    /// <summary>
    /// Used to load the SqlUserInstance.DLL into the current instance
    /// </summary>
    /// <remarks>
    /// Uses statics to prevent loading more that one instance.
    /// TODO:We should look to free the IntPtr
    /// </remarks>
    internal class LocalDBBinaryLoader : ILocalDBBinaryLoader
    {

        public LocalDBBinaryLoader() 
            : this(new LocalDBVersionProvider())
        {
        }

        internal LocalDBBinaryLoader(ILocalDBVersionProvider localDBVersionProvider)
        {
            this.localDBVersionProvider = localDBVersionProvider;
        }

        private readonly ILocalDBVersionProvider localDBVersionProvider;

        /// <summary>
        /// Prevents multiple threads from loading at the same time
        /// </summary>
        private static readonly object lockObject = new object();

        private static IntPtr loadLibraryPtr = IntPtr.Zero;

        public void LoadMostRecentVersion()
        {
            var version =
                localDBVersionProvider.GetInstalledVersions().OrderByDescending(x => x.VersionNumber).FirstOrDefault();
            if (version == null)
            {
                throw new Exception("No installed versions of LocalDB found");
            }
            LoadVersion(version);
        }

        /// <summary>
        /// Attempt to load the supplied version of SqlUserInstance.DLL.  Can repeatedly call for the same version, but does not support loading multiple versions
        /// </summary>
        public void LoadVersion(string versionName)
        {
            var availableVersions = localDBVersionProvider.GetInstalledVersions();
            var matchingVersion = availableVersions.FirstOrDefault(x => string.Equals(x.VersionName, versionName, StringComparison.OrdinalIgnoreCase));
            if (matchingVersion == null)
            {
                throw new ArgumentException(
                    string.Format(
                        "No installed version of LocalDB matching version '{0}'.  {1} available version{2}: {3}",
                        versionName, availableVersions.Count, availableVersions.Count != 1 ? "s" : "", string.Join(" ", availableVersions.Select(x => x.VersionName))));
            }

            LoadVersion(matchingVersion);
        }

        public void LoadVersion(decimal versionNumber)
        {
            var availableVersions = localDBVersionProvider.GetInstalledVersions();
            var matchingVersion = availableVersions.FirstOrDefault(x => x.VersionNumber == versionNumber);
            if (matchingVersion == null)
            {
                throw new ArgumentException(
                    string.Format(
                        "No installed version of LocalDB matching version {0}.  {1} available version{2}: {3}",
                        versionNumber, availableVersions.Count, availableVersions.Count != 1 ? "s" : "", string.Join(" ", availableVersions.Select(x => x.VersionNumber))));
            }

            LoadVersion(matchingVersion);
        }

        /// <summary>
        /// Attempt to load the supplied version of SqlUserInstance.DLL.  Can repeatedly call for the same version, but does not support loading multiple versions
        /// </summary>
        public void LoadVersion(LocalDBVersion localDBVersion)
        {
            //We only want one instance to load the DLL
            //Use check, lock, check pattern
            if (loadLibraryPtr != IntPtr.Zero)
            {
                //TODO(cwb):We could throw here if we're trying to load a different one to what has loaded
                return;
            }
            lock (lockObject)
            {
                //Check again
                if (loadLibraryPtr != IntPtr.Zero)
                {
                    //TODO(cwb):We could throw here if we're trying to load a different one to what has loaded
                    return;
                }

                loadLibraryPtr = LocalDBWin32.LoadLibrary(localDBVersion.InstanceAPIPath);
                var loadLibraryExitCode = Marshal.GetLastWin32Error();
                if (loadLibraryExitCode != 0)
                {
                    throw new Exception(string.Format("Unable to load {0} from {1}.  Win32 error code: {2}", 
                        localDBVersion.VersionNumber, localDBVersion.InstanceAPIPath, loadLibraryExitCode));
                }
            }
        }

    }
}
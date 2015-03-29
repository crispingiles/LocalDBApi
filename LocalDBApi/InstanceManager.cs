using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBSoft.LocalDBApi
{
    /// <summary>
    /// Used to control instances of SQL Server Express Local DB
    /// </summary>
    public class InstanceManager : IInstanceManager
    {

        public InstanceManager()
            : this(new LocalDBBinaryLoader())
        {
        }

        internal InstanceManager(ILocalDBBinaryLoader localDBBinaryLoader)
        {
            this.localDBBinaryLoader = localDBBinaryLoader;
        }

        private readonly ILocalDBBinaryLoader localDBBinaryLoader;

        public void CreateInstance(string localDBVersion, string instanceName)
        {
            localDBBinaryLoader.LoadVersion(localDBVersion);
            var exitCode = LocalDBWin32.LocalDBCreateInstance(localDBVersion, instanceName, 0);
            if (exitCode != 0)
            {
                throw new Exception("" + exitCode);
            }
        }


    }
}

using System.Text;

namespace WBSoft.LocalDBApi
{
    /// <summary>
    /// Used to retrieve error information
    /// </summary>
    internal class LocalDBErrorProvider : ILocalDBErrorProvider
    {
        public LocalDBError GetError(int errorCode)
        {
            const int LOCALDB_TRUNCATE_ERR_MESSAGE = 0x0001;
            int messageBufferLength = 1024;
            var  messageBuffer = new StringBuilder(1024);
            LocalDBWin32.LocalDBFormatMessage(errorCode, LOCALDB_TRUNCATE_ERR_MESSAGE, 0, messageBuffer, ref messageBufferLength);
            return new LocalDBError(errorCode, messageBuffer.ToString());
        }
    }
}
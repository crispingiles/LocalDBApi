namespace WBSoft.LocalDBApi
{
    /// <summary>
    /// Contains information about an error code from LocalDB
    /// </summary>
    internal class LocalDBError
    {
        public LocalDBError(int errorCode, string errorMessage)
        {
            this.errorCode = errorCode;
            this.errorMessage = errorMessage;
        }

        private readonly int errorCode;

        private readonly string errorMessage;

        public int ErrorCode
        {
            get { return errorCode; }
        }

        public string ErrorMessage
        {
            get { return errorMessage; }
        }
    }
}
namespace WBSoft.LocalDBApi
{
    /// <summary>
    /// Used to retrieve error information
    /// </summary>
    internal interface ILocalDBErrorProvider
    {

        LocalDBError GetError(int errorCode);

    }
}

namespace Forgery.Errors
{
    /// <summary>
    ///     Exception Type, used in conjuction with the current log level
    ///     The types can be mapped to a set log level.
    /// </summary>
    public enum ForgeryExceptionType
    {
        Warning,
        Critical,
        Info
    }
}
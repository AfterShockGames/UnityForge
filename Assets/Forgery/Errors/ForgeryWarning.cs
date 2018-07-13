using System;
using System.Reflection;

namespace Forgery.Errors
{
    /// <summary>
    ///     Used for throwing critical breaking errors
    /// </summary>
    public class ForgeryWarning : ForgeryExceptionBase
    {
        public override string HelpLink { get; set; }

        public override string GetMessage
        {
            get { return _message; }
        }

        public override string Source { get; set; }

        public override string StackTrace
        {
            get { throw new NotImplementedException(); }
        }

        public override MethodBase TargetSite
        {
            get { throw new NotImplementedException(); }
        }

        public override ForgeryExceptionType ExceptionType
        {
            get { return ForgeryExceptionType.Critical; }
        }

        private readonly string _message;

        /// <summary>
        ///     Warning Constructor
        /// </summary>
        /// <param name="message">The message to throw</param>
        public ForgeryWarning(string message)
        {
            _message = message;
        }
    }
}

using System;
using System.Reflection;

namespace Forgery.Errors
{
    /// <summary>
    ///     Used for throwing critical breaking errors
    /// </summary>
    public class ForgeryCritical : ForgeryExceptionBase
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
        ///     Critical Constructor
        /// </summary>
        /// <param name="message">The message to throw</param>
        public ForgeryCritical(string message)
        {
            _message = message;
        }
    }
}

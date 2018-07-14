using System;
using System.Reflection;

namespace Forge.Errors
{
    /// <summary>
    ///     Used for throwing critical breaking errors
    /// </summary>
    public class ForgeWarning : ForgeExceptionBase
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

        public override ForgeExceptionType ExceptionType
        {
            get { return ForgeExceptionType.Critical; }
        }

        private readonly string _message;

        /// <summary>
        ///     Warning Constructor
        /// </summary>
        /// <param name="message">The message to throw</param>
        public ForgeWarning(string message)
        {
            _message = message;
        }
    }
}

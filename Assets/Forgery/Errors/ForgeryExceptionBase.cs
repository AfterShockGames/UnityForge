using System;
using System.Reflection;

namespace Forgery.Errors
{
    /// <summary>
    ///     Forgery Exception class, used for throwing Forgery related exceptions and logging
    /// </summary>
    public abstract class ForgeryExceptionBase : Exception
    {
        /// <summary>
        ///     Link to help documentation
        /// </summary>
        public new abstract string HelpLink { get; set; }

        /// <summary>
        ///     The thrown message.
        ///     Prefixed with a date
        /// </summary>
        public new string Message
        {
            get { return MessagePre() + GetMessage; }
        }

        /// <summary>
        ///     Gets the forgery error message, this should be implemented in the child classes.
        /// </summary>
        public abstract string GetMessage { get; }
        public new abstract string Source { get; set; }
        public new abstract string StackTrace { get; }
        public new abstract MethodBase TargetSite { get; }

        public abstract ForgeryExceptionType ExceptionType { get; }

        /// <summary>
        ///     Adds a prefix to the log messages
        /// </summary>
        /// <returns>
        ///     The message prefix
        /// </returns>
        private string MessagePre()
        {
            return '[' + DateTime.Now.ToShortTimeString() + "][" + ExceptionType.GetType() + ']';
        }
    }
}

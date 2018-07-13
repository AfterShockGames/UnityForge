using System;
using System.Reflection;

namespace Forgery.Errors
{
    public class ForgeryInfo : ForgeryExceptionBase
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
            get { return ForgeryExceptionType.Info; }
        }

        private readonly string _message;

        public ForgeryInfo(string message) 
        {
            _message = message;
        }
    }
}

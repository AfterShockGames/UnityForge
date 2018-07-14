using System;
using System.Reflection;

namespace Forge.Errors
{
    public class ForgeInfo : ForgeExceptionBase
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
            get { return ForgeExceptionType.Info; }
        }

        private readonly string _message;

        public ForgeInfo(string message) 
        {
            _message = message;
        }
    }
}

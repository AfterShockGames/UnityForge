using System;
using System.Reflection;

namespace Forge.Errors
{
    public class ForgeInfo : ForgeExceptionBase
    {
        public override string HelpLink { get; set; }

        protected override string GetMessage => _message;

        public override string Source { get; set; }

        public override string StackTrace => throw new NotImplementedException();

        public override MethodBase TargetSite { get; }

        protected override ForgeExceptionType ExceptionType => ForgeExceptionType.Info;

        private readonly string _message;

        public ForgeInfo(string message) 
        {
            _message = message;
        }
    }
}

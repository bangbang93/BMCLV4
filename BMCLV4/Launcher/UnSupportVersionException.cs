using System;
using BMCLV4.Language;

namespace BMCLV4.Launcher
{
    class UnSupportVersionException : Exception
    {
        private readonly string _message;

        public override string Message
        {
            get { return _message ?? LangManager.GetLangFromResource("UnSupportVersionExcepton"); }
        }

        public UnSupportVersionException(){}

        public UnSupportVersionException(string message)
        {
            _message = message;
        }
    }
}

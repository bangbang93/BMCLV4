using System;
using BMCLV4.Language;

namespace BMCLV4.Launcher
{
    class NoJavaException : Exception
    {
        private readonly string _message;

        public override string Message
        {
            get { return _message??LangManager.GetLangFromResource("NoJavaException"); }
        }

        public NoJavaException()
        {
        }

        public NoJavaException(string message)
        {
            _message = message;
        }
    }
}

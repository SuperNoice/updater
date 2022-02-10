using System;

namespace Updater.Exeptions
{
    public class LoadConfigError : Exception
    {
        public LoadConfigError() : base() { }

        public LoadConfigError(string message) : base(message) { }
    }
}

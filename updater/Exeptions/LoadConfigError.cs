using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Updater.Exeptions
{
    public class LoadConfigError : Exception
    {
        public LoadConfigError(string message) : base(message) { }
    }
}

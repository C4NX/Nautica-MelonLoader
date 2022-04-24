using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NauticaML.Exceptions
{
    public class NauticaFatalException : Exception
    {
        public NauticaFatalException(): base("A fatal failure has made in Nautica")
        {
        }
    }
}

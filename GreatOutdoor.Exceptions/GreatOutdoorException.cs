using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capgemini.GreatOutdoor.Exceptions
{
    /// <summary>
    /// Represents project-level user-defined exception.
    /// </summary>
    public class GreatOutdoorException : ApplicationException
    {
        ///constructors
        public GreatOutdoorException() : base()
        {
        }

        public GreatOutdoorException(string message) : base(message)
        {
        }

        public GreatOutdoorException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}



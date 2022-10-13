using System;
using System.Collections.Generic;
using System.Text;

namespace Capgemini.GreatOutdoor.Exceptions
{
    /// <summary>
    /// Add comment
    /// </summary>
    public sealed class OfflineReturnException : ApplicationException
    {

        public OfflineReturnException()
            : base()
        {
        }

        public OfflineReturnException(string message)
            : base(message)
        {
        }
        public OfflineReturnException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}



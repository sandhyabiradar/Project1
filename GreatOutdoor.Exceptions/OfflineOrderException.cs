using System;
using System.Collections.Generic;
using System.Text;

namespace Capgemini.GreatOutdoor.Exceptions
{
    /// <summary>
    /// Add comment
    /// </summary>
    public class OfflineOrderException : ApplicationException
    {
        public OfflineOrderException()
            : base()
        {
        }

        public OfflineOrderException(string message)
            : base(message)
        {
        }
        public OfflineOrderException(string message, ApplicationException innerException)
            : base(message, innerException)
        {
        }
    }
}


using System;
using System.Collections.Generic;

namespace Capgemini.GreatOutdoor.Entities
{
    public interface IUser
    {
        string Email { get; set; }
        string Password { get; set; }
    }
}

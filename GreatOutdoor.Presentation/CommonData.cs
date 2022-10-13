using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Capgemini.GreatOutdoor.Entities;
using Capgemini.GreatOutdoor.Helpers;

namespace Capgemini.GreatOutdoor.PresentationLayer
{
    public static class CommonData
    {
        public static IUser CurrentUser { get; set; }
        public static UserType CurrentUserType { get; set; }
    }
}
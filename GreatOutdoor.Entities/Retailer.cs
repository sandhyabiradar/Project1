using System;
using System.Collections.Generic;
using System.Linq;
using Capgemini.GreatOutdoor.Helpers.ValidationAttributes;
using Capgemini.GreatOutdoor.Entities;

namespace Capgemini.GreatOutdoor.Entities
{
    /// <summary>
    /// Interface for Retailer Entity
    /// </summary>
    public interface IRetailer
    {
        Guid RetailerID { get; set; }
        string RetailerName { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        string RetailerMobile { get; set; }
        DateTime CreationDateTime { get; set; }
        DateTime LastModifiedDateTime { get; set; }
    }

    /// <summary>
    /// Represents Retailer
    /// </summary>
    public class Retailer : IRetailer, IUser
    {
        /* Auto-Implemented Properties */
        [Required("Retailer ID can't be blank.")]
        public Guid RetailerID { get; set; }

        [Required("Retailer Name can't be blank.")]
        [RegExp(@"^(\w{2,40})$", "Retailer Name should contain only 2 to 40 characters.")]
        public string RetailerName { get; set; }

        [Required("Email can't be blank.")]
        [RegExp(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", "Email is invalid.")]
        public string Email { get; set; }

        [Required("Password can't be blank.")]
        [RegExp(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,15})", "Password should be 6 to 15 characters with at least one digit, one uppercase letter, one lower case letter.")]
        public string Password { get; set; }

        [Required("Mobile No. Can not be blank")]
        [RegExp(@"^((\+)?(\d{2}[-]))?(\d{10}){1}?$", "10 digit Mobile no.")]
        public string RetailerMobile { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime LastModifiedDateTime { get; set; }

        /* Constructor */
        public Retailer()
        {
            RetailerID = default(Guid);
            RetailerName = null;
            Email = null;
            Password = null;
            RetailerMobile = null;
            CreationDateTime = default(DateTime);
            LastModifiedDateTime = default(DateTime);
        }
    }
}




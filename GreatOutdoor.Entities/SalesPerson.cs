using System;
using System.Collections.Generic;
using System.Linq;
using Capgemini.GreatOutdoor.Helpers.ValidationAttributes;
using Capgemini.GreatOutdoor.Entities;

namespace Capgemini.GreatOutdoor.Entities
{
    /// <summary>
    /// Interface for SalesPerson Entity
    /// </summary>
    public interface ISalesPerson
    {
        Guid SalesPersonID { get; set; }
        string SalesPersonName { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        string SalesPersonMobile { get; set; }
        DateTime CreationDateTime { get; set; }
        DateTime LastModifiedDateTime { get; set; }
    }

    /// <summary>
    /// Represents SalesPerson
    /// </summary>
    public class SalesPerson : ISalesPerson, IUser
    {
        /* Auto-Implemented Properties */
        [Required("SalesPerson ID can't be blank.")]
        public Guid SalesPersonID { get; set; }

        [Required("SalesPerson Name can't be blank.")]
        [RegExp(@"^(\w{2,40})$", "SalesPerson Name should contain only 2 to 40 characters.")]
        public string SalesPersonName { get; set; }

        [Required("Email can't be blank.")]
        [RegExp(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", "Email is invalid.")]
        public string Email { get; set; }

        [Required("Password can't be blank.")]
        [RegExp(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,15})", "Password should be 6 to 15 characters with at least one digit, one uppercase letter, one lower case letter.")]
        public string Password { get; set; }

        [Required("Mobile No. Can not be blank")]
        [RegExp(@"^((\+)?(\d{2}[-]))?(\d{10}){1}?$", "10 digit Mobile no.")]
        public string SalesPersonMobile { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime LastModifiedDateTime { get; set; }

        /* Constructor */
        public SalesPerson()
        {
            SalesPersonID = default(Guid);
            SalesPersonName = null;
            Email = null;
            Password = null;
            SalesPersonMobile = null;
            CreationDateTime = default(DateTime);
            LastModifiedDateTime = default(DateTime);
        }
    }
}




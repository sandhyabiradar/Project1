using System;
using System.Collections.Generic;
using System.Linq;
using Capgemini.GreatOutdoor.Helpers;
using Capgemini.GreatOutdoor.Helpers.ValidationAttributes;

namespace Capgemini.GreatOutdoor.Entities
{
    /// <summary>
    /// Interface for OfflineReturn Entity
    /// </summary>
    public interface IOfflineReturn
    {
        Guid OfflineReturnID { get; set; }
        PurposeOfReturn Purpose { get; set; }
        int QuantityOfReturn { get; set; }
        Guid OfflineOrderID { get; set; }
        Guid ProductID { get; set; }
        double ReturnAmount { get; set; }
        double UnitPrice { get; set; }
        DateTime CreationDateTime { get; set; }
        DateTime LastModifiedDateTime { get; set; }
    }

    /// <summary>
    /// Represents OfflineReturn
    /// </summary>
    public class OfflineReturn : IOfflineReturn
    {
        /* Auto-Implemented Properties */
        [Required("OfflineReturnID can't be blank.")]
        public Guid OfflineReturnID { get; set; }
        [Required("Quantity can't be blank.")]
        // [RegExp(@"^[1-9]+[0-9]*)$", "Quantity cannot be less than 0.")]
        public int QuantityOfReturn { get; set; }
        [Required("ProductID can't be blank.")]
        public Guid ProductID { get; set; }
        [Required("OfflineOrderID can't be blank.")]
        public Guid OfflineOrderID { get; set; }
        [Required("ReturnAmount can't be blank.")]
        public double ReturnAmount { get; set; }
        [Required("UnitPrice can't be blank.")]
        public double UnitPrice { set; get; }
        [Required("PurposeOfReturn can't be blank.")]
        public PurposeOfReturn Purpose { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime LastModifiedDateTime { get; set; }

        /* Constructor */
        public OfflineReturn()
        {
            OfflineReturnID = default(Guid);
            Purpose = default(PurposeOfReturn);
            OfflineOrderID = default(Guid);
            ProductID = default(Guid);
            ReturnAmount = 0.0;
            QuantityOfReturn = 0;
            CreationDateTime = default(DateTime);
            LastModifiedDateTime = default(DateTime);
        }
    }
}
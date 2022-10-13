using System;
using System.Collections.Generic;
using System.Text;
using Capgemini.GreatOutdoor.Helpers.ValidationAttributes;
namespace Capgemini.GreatOutdoor.Entities
{
    public interface IOfflineOrder
    {


        Guid OfflineOrderID { get; set; }
        Guid RetailerID { get; set; }
        Guid SalesPersonID { get; set; }
        double TotalOrderAmount { get; set; }
        DateTime CreationDateTime { get; set; }
        DateTime LastModifiedDateTime { get; set; }
        double TotalQuantity { get; set; }
    }
    public class OfflineOrder : IOfflineOrder
    {
        [Required("OfflineOrderID can't be blank.")]
        public Guid OfflineOrderID { get; set; }
        [Required("RetailerID can't be blank.")]
        public Guid RetailerID { get; set; }
        [Required("SalesPersonID can't be blank.")]
        public Guid SalesPersonID { get; set; }
        [Required("Total Quantity can't be blank.")]
        public double TotalQuantity { get; set; }
        [Required("TotalOrderAmount can't be blank.")]
        public double TotalOrderAmount { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime LastModifiedDateTime { get; set; }

    }
}

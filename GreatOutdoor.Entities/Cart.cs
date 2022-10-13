using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capgemini.GreatOutdoor.Entities
{
    public class Cart
    {
        public Guid CartId { get; set; } //ID of the cart
        public Guid AddressID { get; set; } //address ID of the retailer 
        public Guid RetailerID { get; set; } //ID of the retailer
        
        public int TotalQuantity { get; set; } //Total quantity of the products ordererd
        public int TotalAmount { get; set; } //Cart value
    }
}

﻿using Capgemini.GreatOutdoor.Helpers.ValidationAttributes;
using Capgemini.GreatOutdoor.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capgemini.GreatOutdoor.Entities
{
    /// <summary>
    /// Interface for Order entity
    /// </summary>
    public interface IOrder
    {
        Guid OrderId { get; set; }
        Guid RetailerID { get; set; }
        DateTime DateOfOrder { get; set; }
        int TotalQuantity { get; set; }
        DateTime LastModifiedDateTime { get; set; }
        double OrderAmount { get; set; }
    }

    /// <summary>
    /// Contains summary of current order 
    /// </summary>
    public class Order : IOrder
    {
        /*Auto-Implemented Properties*/
        [Required("OrderId can't be blank.")]
        public Guid OrderId { get; set; } //ID of the order
        [Required("RetailerID can't be blank.")]
        public Guid RetailerID { get; set; } //ID of the retailer
        public DateTime DateOfOrder { get; set; } //Date of placing order
        public int TotalQuantity { get; set; } //Total quantity of the products ordererd
        public DateTime LastModifiedDateTime { get; set; }//date of order modification
        public double OrderAmount { get; set; }//total cost of the order
        public OrderStatus status; //Gives the status of the order
        public double OrderNumber { get; set; } = 0;//order no of the retailer
    }
}

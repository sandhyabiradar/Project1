using System;
using System.Collections.Generic;
using System.Text;

namespace Capgemini.GreatOutdoor.Entities
{
    public interface IOfflineOrderDetail
    {
        Guid OfflineOrderDetailID { get; set; }
        Guid ProductID { get; set; }
        string ProductName { get; set; }
        int Quantity { get; set; }
        double UnitPrice { get; set; }
        double TotalPrice { get; set; }
        Guid OfflineOrderID { get; set; }
    }
    public class OfflineOrderDetail
    {
        public Guid OfflineOrderDetailID { get; set; }
        public Guid ProductID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }
        public Guid OfflineOrderID { get; set; }
    }
}
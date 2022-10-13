using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capgemini.GreatOutdoor.Entities;

namespace Capgemini.GreatOutdoor.Contracts
{
    public interface IOrderBL : IDisposable
    {
        Task<Order> GetOrderByOrderNumberBL(double orderNumber);
        Task<Order> GetOrderByOrderIDBL(Guid searchOrderID);
        Task<List<Order>> GetOrdersByRetailerIDBL(Guid retailerID);
        Task<(bool,Guid)> AddOrderBL(Order newOrder);
        Task<bool> UpdateOrderBL(Order updateOrder);
        Task<bool> DeleteOrderBL(Guid deleteOrderID);
    }
}

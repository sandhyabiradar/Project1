using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capgemini.GreatOutdoor.Entities;

namespace Capgemini.GreatOutdoor.Contracts.BLContracts
{
    public interface IOfflineOrderBL : IDisposable
    {
        Task<(bool, Guid)> AddOfflineOrderBL(OfflineOrder newOfflineOrder);
        Task<OfflineOrder> GetOfflineOrderByOfflineOrderIDBL(Guid searchOfflineOrderID);
        Task<List<OfflineOrder>> GetOfflineOrderBySalesPersonIDBL(Guid salesPersonID);
        Task<List<OfflineOrder>> GetOfflineOrderByRetailerIDBL(Guid retailerID);
        Task<bool> UpdateOfflineOrderBL(OfflineOrder updateOfflineOrder);
        Task<bool> DeleteOfflineOrderBL(Guid deleteOfflineOrderID);



    }
}


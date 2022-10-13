using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capgemini.GreatOutdoor.Entities;

namespace Capgemini.GreatOutdoor.Contracts.BLContracts
{
    public interface IOfflineOrderDetailBL : IDisposable
    {
        Task<bool> AddOfflineOrderDetailBL(OfflineOrderDetail newOfflineOrderDetail);
        Task<List<OfflineOrderDetail>> GetAllOfflineOrderDetailBL();
        Task<OfflineOrderDetail> GetOfflineOrderDetailByOfflineOrderDetailIDBL(Guid searchOfflineOrderDetailID);
        Task<List<OfflineOrderDetail>> GetOfflineOrderDetailByOfflineOrderIDBL(Guid searchOfflineOrderID);
        Task<bool> UpdateOfflineOrderDetailBL(OfflineOrderDetail updateOfflineOrderDetail);
        Task<bool> DeleteOfflineOrderDetailBL(Guid deleteOfflineOrderDetailID);



    }
}
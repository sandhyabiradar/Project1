using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Capgemini.GreatOutdoor.Entities;
using Capgemini.GreatOutdoor.Helpers;

namespace Capgemini.GreatOutdoor.Contracts.BLContracts
{
    public interface IOfflineReturnBL : IDisposable
    {
        Task<bool> AddOfflineReturnBL(OfflineReturn newOfflineReturn);
        Task<List<OfflineReturn>> GetAllOfflineReturnsBL();
        Task<OfflineReturn> GetOfflineReturnByOfflineReturnIDBL(Guid searchOfflineReturnID);
        Task<List<OfflineReturn>> GetOfflineReturnsByPurposeBL(PurposeOfReturn purpose);
        Task<bool> UpdateOfflineReturnBL(OfflineReturn updateOfflineReturn);
        Task<bool> DeleteOfflineReturnBL(Guid deleteOfflinereturnID);
    }
}


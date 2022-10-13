using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Capgemini.GreatOutdoor.Entities;
using Capgemini.GreatOutdoor.Helpers;

namespace Capgemini.GreatOutdoor.Contracts.BLContracts
{
    public interface IOnlineReturnBL : IDisposable
    {
        Task<bool> AddOnlineReturnBL(OnlineReturn newOnlineReturn);
        Task<List<OnlineReturn>> GetAllOnlineReturnsBL();
        Task<OnlineReturn> GetOnlineReturnByOnlineReturnIDBL(Guid searchOnlineReturnID);
        Task<List<OnlineReturn>> GetOnlineReturnsByPurposeBL(PurposeOfReturn purpose);
        Task<bool> UpdateOnlineReturnBL(OnlineReturn updateOnlineReturn);
        Task<bool> DeleteOnlineReturnBL(Guid deleteOnlinereturnID);
    }
}


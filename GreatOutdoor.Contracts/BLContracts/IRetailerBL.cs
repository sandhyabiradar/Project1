using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Capgemini.GreatOutdoor.Entities;

namespace Capgemini.GreatOutdoor.Contracts.BLContracts
{
    public interface IRetailerBL : IDisposable
    {
        Task<(bool,Guid)> AddRetailerBL(Retailer newRetailer);
        Task<List<Retailer>> GetAllRetailersBL();
        Task<Retailer> GetRetailerByRetailerIDBL(Guid searchRetailerID);
        Task<List<Retailer>> GetRetailersByNameBL(string supplierName);
        Task<Retailer> GetRetailerByEmailBL(string email);
        Task<Retailer> GetRetailerByEmailAndPasswordBL(string email, string password);
        Task<bool> UpdateRetailerBL(Retailer updateRetailer);
        Task<bool> UpdateRetailerPasswordBL(Retailer updateRetailer);
        Task<bool> DeleteRetailerBL(Guid deleteRetailerID);
    }
}



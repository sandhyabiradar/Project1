using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Capgemini.GreatOutdoor.Entities;

namespace Capgemini.GreatOutdoor.Contracts.BLContracts
{
    public interface ISalesPersonBL : IDisposable
    {
        Task<(bool,Guid)> AddSalesPersonBL(SalesPerson newSalesPerson);
        Task<List<SalesPerson>> GetAllSalesPersonsBL();
        Task<SalesPerson> GetSalesPersonBySalesPersonIDBL(Guid searchSalesPersonID);
        Task<List<SalesPerson>> GetSalesPersonsByNameBL(string supplierName);
        Task<SalesPerson> GetSalesPersonByEmailBL(string email);
        Task<SalesPerson> GetSalesPersonByEmailAndPasswordBL(string email, string password);
        Task<bool> UpdateSalesPersonBL(SalesPerson updateSalesPerson);
        Task<bool> UpdateSalesPersonPasswordBL(SalesPerson updateSalesPerson);
        Task<bool> DeleteSalesPersonBL(Guid deleteSalesPersonID);
    }
}
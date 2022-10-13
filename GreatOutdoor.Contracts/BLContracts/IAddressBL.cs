using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Capgemini.GreatOutdoor.Entities;

namespace Capgemini.GreatOutdoor.Contracts.BLContracts
{
    public interface IAddressBL : IDisposable
    {
        Task<bool> AddAddressBL(Address newAddress);
        Task<List<Address>> GetAllAddresssBL();
        Task<Address> GetAddressByAddressIDBL(Guid searchAddressID);
        Task<List<Address>> GetAddressByRetailerIDBL( Guid RetailerID);
        Task<bool> UpdateAddressBL(Address updateAddress);
        Task<bool> DeleteAddressBL(Guid deleteAddressID);
    }
}



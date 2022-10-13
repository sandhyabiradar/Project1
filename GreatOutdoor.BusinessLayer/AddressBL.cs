using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Capgemini.GreatOutdoor.Contracts.BLContracts;
using Capgemini.GreatOutdoor.Contracts.DALContracts;
using Capgemini.GreatOutdoor.DataAccessLayer;
using Capgemini.GreatOutdoor.Entities;
using Capgemini.GreatOutdoor.Exceptions;
using Capgemini.GreatOutdoor.Helpers.ValidationAttributes;

namespace Capgemini.GreatOutdoor.BusinessLayer
{
    /// <summary>
    /// Contains data access layer methods for inserting, updating, deleting addresss from Addresss collection.
    /// </summary>
    public class AddressBL : BLBase<Address>, IAddressBL, IDisposable
    {
        //fields
        AddressDALBase addressDAL;

        /// <summary>
        /// Constructor.
        /// </summary>
        public AddressBL()
        {
            this.addressDAL = new AddressDAL();
        }

        /// <summary>
        /// Validations on data before adding or updating.
        /// </summary>
        /// <param name="entityObject">Represents object to be validated.</param>
        /// <returns>Returns a boolean value, that indicates whether the data is valid or not.</returns>
        protected async override Task<bool> Validate(Address entityObject)
        {
            //Create string builder
            StringBuilder sb = new StringBuilder();
            bool valid = await base.Validate(entityObject);

            return valid;
        }

        /// <summary>
        /// Adds new address to Addresss collection.
        /// </summary>
        /// <param name="newAddress">Contains the address details to be added.</param>
        /// <returns>Determinates whether the new address is added.</returns>
        public async Task<bool> AddAddressBL(Address newAddress)
        {
            bool addressAdded = false;
            try
            {
                if (await Validate(newAddress))
                {
                    await Task.Run(() =>
                    {
                        this.addressDAL.AddAddressDAL(newAddress);
                        addressAdded = true;
                        //Serialize();
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
            return addressAdded;
        }

        /// <summary>
        /// Gets all addresss from the collection.
        /// </summary>
        /// <returns>Returns list of all addresss.</returns>
        public async Task<List<Address>> GetAllAddresssBL()
        {
            List<Address> addresssList = null;
            try
            {
                await Task.Run(() =>
                {
                    addresssList = addressDAL.GetAllAddresssDAL();
                });
            }
            catch (Exception)
            {
                throw;
            }
            return addresssList;
        }

        /// <summary>
        /// Gets address based on AddressID.
        /// </summary>
        /// <param name="searchAddressID">Represents AddressID to search.</param>
        /// <returns>Returns Address object.</returns>
        public async Task<Address> GetAddressByAddressIDBL(Guid searchAddressID)
        {
            Address matchingAddress = null;
            try
            {
                await Task.Run(() =>
                {
                    matchingAddress = addressDAL.GetAddressByAddressIDDAL(searchAddressID);
                });
            }
            catch (Exception)
            {
                throw;
            }
            return matchingAddress;
        }



        /// <summary>
        /// Gets address based on Email and Password.
        /// </summary>
        /// <param name="email">Represents Address's Email Address.</param>
        /// <returns>Returns Address object.</returns>
        public async Task<List<Address>> GetAddressByRetailerIDBL(Guid RetailerID)
        {
           List< Address> matchingAddress = null;
            try
            {
                await Task.Run(() =>
                {
                    matchingAddress = addressDAL.GetAddressByRetailerIDDAL(RetailerID);
                });
            }
            catch (Exception)
            {
                throw;
            }
            return matchingAddress;
        }



        /// <summary>
        /// Updates address based on AddressID.
        /// </summary>
        /// <param name="updateAddress">Represents Address details including AddressID, AddressName etc.</param>
        /// <returns>Determinates whether the existing address is updated.</returns>
        public async Task<bool> UpdateAddressBL(Address updateAddress)
        {
            bool addressUpdated = false;
            try
            {
                if ((await Validate(updateAddress)) && (await GetAddressByAddressIDBL(updateAddress.AddressID)) != null)
                {
                    this.addressDAL.UpdateAddressDAL(updateAddress);
                    addressUpdated = true;
                   // Serialize();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return addressUpdated;
        }

        /// <summary>
        /// Deletes address based on AddressID.
        /// </summary>
        /// <param name="deleteAddressID">Represents AddressID to delete.</param>
        /// <returns>Determinates whether the existing address is updated.</returns>
        public async Task<bool> DeleteAddressBL(Guid deleteAddressID)
        {
            bool addressDeleted = false;
            try
            {
                await Task.Run(() =>
                {
                    addressDeleted = addressDAL.DeleteAddressDAL(deleteAddressID);
                    Serialize();
                });
            }
            catch (Exception)
            {
                throw;
            }
            return addressDeleted;
        }

        /// <summary>
        /// Disposes DAL object(s).
        /// </summary>
        public void Dispose()
        {
            ((AddressDAL)addressDAL).Dispose();
        }

        /// <summary>
        /// Invokes Serialize method of DAL.
        /// </summary>
        public void Serialize()
        {
            try
            {
                AddressDAL.Serialize();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///Invokes Deserialize method of DAL.
        /// </summary>
        public void Deserialize()
        {
            try
            {
                AddressDAL.Deserialize();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}




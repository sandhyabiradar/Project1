using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Capgemini.GreatOutdoor.Contracts.BLContracts;
using Capgemini.GreatOutdoor.Contracts.DALContracts;
using Capgemini.GreatOutdoor.DataAccessLayer;
using Capgemini.GreatOutdoor.Entities;
using Capgemini.GreatOutdoor.Exceptions;

namespace Capgemini.GreatOutdoor.BusinessLayer
{
    /// <summary>
    /// Contains data access layer methods for inserting, updating, deleting salesPersons from SalesPersons collection.
    /// </summary>
    public class SalesPersonBL : BLBase<SalesPerson>, ISalesPersonBL, IDisposable
    {
        //fields
        SalesPersonDALBase salesPersonDAL;

        /// <summary>
        /// Constructor.
        /// </summary>
        public SalesPersonBL()
        {
            this.salesPersonDAL = new SalesPersonDAL();
        }

        /// <summary>
        /// Validations on data before adding or updating.
        /// </summary>
        /// <param name="entityObject">Represents object to be validated.</param>
        /// <returns>Returns a boolean value, that indicates whether the data is valid or not.</returns>
        protected async override Task<bool> Validate(SalesPerson entityObject)
        {
            //Create string builder
            StringBuilder sb = new StringBuilder();
            bool valid = await base.Validate(entityObject);

            ////Email is Unique
            //var existingObject = await GetSalesPersonByEmailBL(entityObject.Email);
            //if (existingObject != null && existingObject?.SalesPersonID != entityObject.SalesPersonID)
            //{
            //    valid = false;
            //    sb.Append(Environment.NewLine + $"Email {entityObject.Email} already exists");
            //}

            if (valid == false)
                throw new GreatOutdoorException(sb.ToString());
            return valid;
        }

        /// <summary>
        /// Adds new salesPerson to SalesPersons collection.
        /// </summary>
        /// <param name="newSalesPerson">Contains the salesPerson details to be added.</param>
        /// <returns>Determinates whether the new salesPerson is added.</returns>
        public async Task<(bool, Guid)> AddSalesPersonBL(SalesPerson newSalesPerson)
        {
            bool salesPersonAdded = false;
            Guid newGuid =default(Guid);
            try
            {
                if (await Validate(newSalesPerson))
                {
                    await Task.Run(() =>
                    {
                        (salesPersonAdded, newGuid) = this.salesPersonDAL.AddSalesPersonDAL(newSalesPerson);
                        salesPersonAdded = true;
                        //Serialize();
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
            return (salesPersonAdded, newGuid);
        }

        /// <summary>
        /// Gets all salesPersons from the collection.
        /// </summary>
        /// <returns>Returns list of all salesPersons.</returns>
        public async Task<List<SalesPerson>> GetAllSalesPersonsBL()
        {
            List<SalesPerson> salesPersonsList = null;
            try
            {
                await Task.Run(() =>
                {
                    salesPersonsList = salesPersonDAL.GetAllSalesPersonsDAL();
                });
            }
            catch (Exception)
            {
                throw;
            }
            return salesPersonsList;
        }

        /// <summary>
        /// Gets salesPerson based on SalesPersonID.
        /// </summary>
        /// <param name="searchSalesPersonID">Represents SalesPersonID to search.</param>
        /// <returns>Returns SalesPerson object.</returns>
        public async Task<SalesPerson> GetSalesPersonBySalesPersonIDBL(Guid searchSalesPersonID)
        {
            SalesPerson matchingSalesPerson = null;
            try
            {
                await Task.Run(() =>
                {
                    matchingSalesPerson = salesPersonDAL.GetSalesPersonBySalesPersonIDDAL(searchSalesPersonID);
                });
            }
            catch (Exception)
            {
                throw;
            }
            return matchingSalesPerson;
        }

        /// <summary>
        /// Gets salesPerson based on SalesPersonName.
        /// </summary>
        /// <param name="salesPersonName">Represents SalesPersonName to search.</param>
        /// <returns>Returns SalesPerson object.</returns>
        public async Task<List<SalesPerson>> GetSalesPersonsByNameBL(string salesPersonName)
        {
            List<SalesPerson> matchingSalesPersons = new List<SalesPerson>();
            try
            {
                await Task.Run(() =>
                {
                    matchingSalesPersons = salesPersonDAL.GetSalesPersonsByNameDAL(salesPersonName);
                });
            }
            catch (Exception)
            {
                throw;
            }
            return matchingSalesPersons;
        }

        /// <summary>
        /// Gets salesPerson based on Email and Password.
        /// </summary>
        /// <param name="email">Represents SalesPerson's Email Address.</param>
        /// <returns>Returns SalesPerson object.</returns>
        public async Task<SalesPerson> GetSalesPersonByEmailBL(string email)
        {
            SalesPerson matchingSalesPerson = null;
            try
            {
                await Task.Run(() =>
                {
                    matchingSalesPerson = salesPersonDAL.GetSalesPersonByEmailDAL(email);
                });
            }
            catch (Exception)
            {
                throw;
            }
            return matchingSalesPerson;
        }

        /// <summary>
        /// Gets salesPerson based on Password.
        /// </summary>
        /// <param name="email">Represents SalesPerson's Email Address.</param>
        /// <param name="password">Represents SalesPerson's Password.</param>
        /// <returns>Returns SalesPerson object.</returns>
        public async Task<SalesPerson> GetSalesPersonByEmailAndPasswordBL(string email, string password)
        {
            SalesPerson matchingSalesPerson = null;
            try
            {
                await Task.Run(() =>
                {
                    matchingSalesPerson = salesPersonDAL.GetSalesPersonByEmailAndPasswordDAL(email, password);
                });
            }
            catch (Exception)
            {
                throw;
            }
            return matchingSalesPerson;
        }

        /// <summary>
        /// Updates salesPerson based on SalesPersonID.
        /// </summary>
        /// <param name="updateSalesPerson">Represents SalesPerson details including SalesPersonID, SalesPersonName etc.</param>
        /// <returns>Determinates whether the existing salesPerson is updated.</returns>
        public async Task<bool> UpdateSalesPersonBL(SalesPerson updateSalesPerson)
        {
            bool salesPersonUpdated = false;
            try
            {
                if ((await Validate(updateSalesPerson)) && (await GetSalesPersonBySalesPersonIDBL(updateSalesPerson.SalesPersonID)) != null)
                {
                    this.salesPersonDAL.UpdateSalesPersonDAL(updateSalesPerson);
                    salesPersonUpdated = true;
                    //Serialize();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return salesPersonUpdated;
        }

        /// <summary>
        /// Deletes salesPerson based on SalesPersonID.
        /// </summary>
        /// <param name="deleteSalesPersonID">Represents SalesPersonID to delete.</param>
        /// <returns>Determinates whether the existing salesPerson is updated.</returns>
        public async Task<bool> DeleteSalesPersonBL(Guid deleteSalesPersonID)
        {
            bool salesPersonDeleted = false;
            try
            {
                await Task.Run(() =>
                {
                    salesPersonDeleted = salesPersonDAL.DeleteSalesPersonDAL(deleteSalesPersonID);
                    Serialize();
                });
            }
            catch (Exception)
            {
                throw;
            }
            return salesPersonDeleted;
        }

        /// <summary>
        /// Updates salesPerson's password based on SalesPersonID.
        /// </summary>
        /// <param name="updateSalesPerson">Represents SalesPerson details including SalesPersonID, Password.</param>
        /// <returns>Determinates whether the existing salesPerson's password is updated.</returns>
        public async Task<bool> UpdateSalesPersonPasswordBL(SalesPerson updateSalesPerson)
        {
            bool passwordUpdated = false;
            try
            {
                if ((await Validate(updateSalesPerson)) && (await GetSalesPersonBySalesPersonIDBL(updateSalesPerson.SalesPersonID)) != null)
                {
                    this.salesPersonDAL.UpdateSalesPersonPasswordDAL(updateSalesPerson);
                    passwordUpdated = true;
                   // Serialize();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return passwordUpdated;
        }

        /// <summary>
        /// Disposes DAL object(s).
        /// </summary>
        public void Dispose()
        {
            ((SalesPersonDAL)salesPersonDAL).Dispose();
        }

        /// <summary>
        /// Invokes Serialize method of DAL.
        /// </summary>
        public void Serialize()
        {
            try
            {
                SalesPersonDAL.Serialize();
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
                SalesPersonDAL.Deserialize();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}





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
    /// Contains data access layer methods for inserting, updating, deleting retailers from retailers collection.
    /// </summary>
    public class RetailerBL : BLBase<Retailer>, IRetailerBL, IDisposable
    {
        //fields
        RetailerDALBase retailerDAL;

        /// <summary>
        /// Constructor.
        /// </summary>
        public RetailerBL()
        {
            this.retailerDAL = new RetailerDAL();
        }

        /// <summary>
        /// Validations on data before adding or updating.
        /// </summary>
        /// <param name="entityObject">Represents object to be validated.</param>
        /// <returns>Returns a boolean value, that indicates whether the data is valid or not.</returns>
        protected async override Task<bool> Validate(Retailer entityObject)
        {
            //Create string builder
            StringBuilder sb = new StringBuilder();
            bool valid = await base.Validate(entityObject);

            //Email is Unique
            var existingObject = await GetRetailerByEmailBL(entityObject.Email);
            if (existingObject != null && existingObject?.RetailerID != entityObject.RetailerID)
            {
                valid = false;
                sb.Append(Environment.NewLine + $"Email {entityObject.Email} already exists");
            }

            if (valid == false)
                throw new GreatOutdoorException(sb.ToString());
            return valid;
        }

        /// <summary>
        /// Adds new retailer to Retailers collection.
        /// </summary>
        /// <param name="newRetailer">Contains the retailer details to be added.</param>
        /// <returns>Determinates whether the new retailer is added.</returns>
        public async Task<(bool, Guid)> AddRetailerBL(Retailer newRetailer)
        {
            bool retailerAdded = false;
            Guid RetailerGuid = default(Guid);

            try
            {
                if (await Validate(newRetailer))
                {
                    await Task.Run(() =>
                    {
                        (retailerAdded, RetailerGuid) = this.retailerDAL.AddRetailerDAL(newRetailer);
                        retailerAdded = true;
                        //Serialize();
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
            return (retailerAdded, RetailerGuid);
        }

        /// <summary>
        /// Gets all retailers from the collection.
        /// </summary>
        /// <returns>Returns list of all retailers.</returns>
        public async Task<List<Retailer>> GetAllRetailersBL()
        {
            List<Retailer> retailersList = null;
            try
            {
                await Task.Run(() =>
                {
                    retailersList = retailerDAL.GetAllRetailersDAL();
                });
            }
            catch (Exception)
            {
                throw;
            }
            return retailersList;
        }

        /// <summary>
        /// Gets retailer based on RetailerID.
        /// </summary>
        /// <param name="searchRetailerID">Represents RetailerID to search.</param>
        /// <returns>Returns Retailer object.</returns>
        public async Task<Retailer> GetRetailerByRetailerIDBL(Guid searchRetailerID)
        {
            Retailer matchingRetailer = null;
            try
            {
                await Task.Run(() =>
                {
                    matchingRetailer = retailerDAL.GetRetailerByRetailerIDDAL(searchRetailerID);
                });
            }
            catch (Exception)
            {
                throw;
            }
            return matchingRetailer;
        }

        /// <summary>
        /// Gets retailer based on RetailerName.
        /// </summary>
        /// <param name="retailerName">Represents RetailerName to search.</param>
        /// <returns>Returns Retailer object.</returns>
        public async Task<List<Retailer>> GetRetailersByNameBL(string retailerName)
        {
            List<Retailer> matchingRetailers = new List<Retailer>();
            try
            {
                await Task.Run(() =>
                {
                    matchingRetailers = retailerDAL.GetRetailersByNameDAL(retailerName);
                });
            }
            catch (Exception)
            {
                throw;
            }
            return matchingRetailers;
        }

        /// <summary>
        /// Gets retailer based on Email and Password.
        /// </summary>
        /// <param name="email">Represents Retailer's Email retailer.</param>
        /// <returns>Returns Retailer object.</returns>
        public async Task<Retailer> GetRetailerByEmailBL(string email)
        {
            Retailer matchingRetailer = null;
            try
            {
                await Task.Run(() =>
                {
                    matchingRetailer = retailerDAL.GetRetailerByEmailDAL(email);
                });
            }
            catch (Exception)
            {
                throw;
            }
            return matchingRetailer;
        }

        /// <summary>
        /// Gets retailer based on Password.
        /// </summary>
        /// <param name="email">Represents Retailer's Email retailer.</param>
        /// <param name="password">Represents Retailer's Password.</param>
        /// <returns>Returns Retailer object.</returns>
        public async Task<Retailer> GetRetailerByEmailAndPasswordBL(string email, string password)
        {
            Retailer matchingRetailer = null;
            try
            {
                await Task.Run(() =>
                {
                    matchingRetailer = retailerDAL.GetRetailerByEmailAndPasswordDAL(email, password);
                });
            }
            catch (Exception)
            {
                throw;
            }
            return matchingRetailer;
        }

        /// <summary>
        /// Updates retailer based on RetailerID.
        /// </summary>
        /// <param name="updateRetailer">Represents Retailer details including RetailerID, RetailerName etc.</param>
        /// <returns>Determinates whether the existing retailer is updated.</returns>
        public async Task<bool> UpdateRetailerBL(Retailer updateRetailer)
        {
            bool retailerUpdated = false;
            try
            {
                if ((await Validate(updateRetailer)) && (await GetRetailerByRetailerIDBL(updateRetailer.RetailerID)) != null)
                {
                    this.retailerDAL.UpdateRetailerDAL(updateRetailer);
                    retailerUpdated = true;
                    //Serialize();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return retailerUpdated;
        }

        /// <summary>
        /// Deletes retailer based on RetailerID.
        /// </summary>
        /// <param name="deleteRetailerID">Represents RetailerID to delete.</param>
        /// <returns>Determinates whether the existing retailer is updated.</returns>
        public async Task<bool> DeleteRetailerBL(Guid deleteRetailerID)
        {
            bool retailerDeleted = false;
            try
            {
                await Task.Run(() =>
                {
                    retailerDeleted = retailerDAL.DeleteRetailerDAL(deleteRetailerID);
                    Serialize();
                });
            }
            catch (Exception)
            {
                throw;
            }
            return retailerDeleted;
        }

        /// <summary>
        /// Updates retailer's password based on RetailerID.
        /// </summary>
        /// <param name="updateRetailer">Represents Retailer details including RetailerID, Password.</param>
        /// <returns>Determinates whether the existing retailer's password is updated.</returns>
        public async Task<bool> UpdateRetailerPasswordBL(Retailer updateRetailer)
        {
            bool passwordUpdated = false;
            try
            {
                if ((await Validate(updateRetailer)) && (await GetRetailerByRetailerIDBL(updateRetailer.RetailerID)) != null)
                {
                    this.retailerDAL.UpdateRetailerPasswordDAL(updateRetailer);
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

        public async Task<RetailerReport> GetRetailerReportByRetailIDBL(Guid RetailerID)
        {
           RetailerReport retailerReport = new RetailerReport();
            retailerReport.RetailerID = RetailerID;
            Retailer retailer = new Retailer();
            RetailerBL retailerBL = new RetailerBL();
            retailer = await retailerBL.GetRetailerByRetailerIDBL(retailerReport.RetailerID);
            retailerReport.RetailerName = retailer.RetailerName;
            List<Order> orderList = new List<Order>();
            OrderBL order = new OrderBL();
            orderList = await order.GetOrdersByRetailerIDBL(RetailerID);
            foreach (Order item in orderList)
            {
                retailerReport.RetailerSalesCount++;
                retailerReport.RetailerSalesAmount += item.OrderAmount;
            }

           return retailerReport;

        }


        /// <summary>
        /// Disposes DAL object(s).
        /// </summary>
        public void Dispose()
        {
            ((RetailerDAL)retailerDAL).Dispose();
        }

        /// <summary>
        /// Invokes Serialize method of DAL.
        /// </summary>
        public void Serialize()
        {
            try
            {
                RetailerDAL.Serialize();
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
                RetailerDAL.Deserialize();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}




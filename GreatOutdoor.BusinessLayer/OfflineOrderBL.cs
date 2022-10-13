
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capgemini.GreatOutdoor.Entities;
using Capgemini.GreatOutdoor.DataAccessLayer;
using Capgemini.GreatOutdoor.Contracts.BLContracts;
using Capgemini.GreatOutdoor.Exceptions;

namespace Capgemini.GreatOutdoor.BusinessLayer
{
    public class OfflineOrderBL : BLBase<OfflineOrder>, IOfflineOrderBL, IDisposable
    {
        //fields
        OfflineOrderDAL offlineOrderDAL;

        /// <summary>
        /// Constructor.
        /// </summary>
        public OfflineOrderBL()
        {
            this.offlineOrderDAL = new OfflineOrderDAL();
        }
        /// Validations on data before adding or updating.
        /// </summary>
        /// <param name="entityObject">Represents object to be validated.</param>
        /// <returns>Returns a boolean value, that indicates whether the data is valid or not.</returns>
        protected async override Task<bool> Validate(OfflineOrder entityObject)
        {
            //Create string builder
            StringBuilder sb = new StringBuilder();
            bool valid = await base.Validate(entityObject);
            if (entityObject.TotalOrderAmount <= 0)
            {
                valid = false;
                sb.Append(Environment.NewLine + "Total Amount cannot be negative");
            }
            if (entityObject.TotalQuantity <= 0)
            {
                valid = false;
                sb.Append(Environment.NewLine + "Total Quantity cannot be negative");
            }
            //RetailerID is Unique
            RetailerBL iRetailerBL = new RetailerBL();

            var existingObject = await iRetailerBL.GetRetailerByRetailerIDBL(entityObject.RetailerID);
            if (existingObject == null)
            {
                valid = false;
                sb.Append(Environment.NewLine + $"RetailerID {entityObject.RetailerID} does not exists");
            }
            ////Sales Person is Unique
            //SalesPersonBL iSalesPersonBL = new SalesPersonBL();

            //var existingObject1 = await iSalesPersonBL.GetSalesPersonBySalesPersonIDBL(entityObject.SalesPersonID);
            //if (existingObject1 == null)
            //{
            //    valid = false;
            //    sb.Append(Environment.NewLine + $"SalesPersonID {entityObject.SalesPersonID} does not exists");
            //}



            if (valid == false)
                throw new OfflineOrderException(sb.ToString());
            return valid;
        }


        /// <summary>
        /// Adds new OfflineOrder to OfflineOrders collection.
        /// </summary>
        /// <param name="newOfflineOrder">Contains the OfflineOrder details to be added.</param>
        /// <returns>Determinates whether the new OfflineOrder is added.</returns>
        public async Task<(bool, Guid)> AddOfflineOrderBL(OfflineOrder newOfflineOrder)
        {
            bool OfflineOrderAdded = false;
            Guid newGuid = default(Guid);
            try
            {
                if (await Validate(newOfflineOrder))
                {
                    await Task.Run(() =>
                    {
                       
                        (OfflineOrderAdded, newGuid) = this.offlineOrderDAL.AddOfflineOrderDAL(newOfflineOrder);
                        OfflineOrderAdded = true;
                        //Serialize();

                    });
                }

                return (OfflineOrderAdded, newGuid);
            }
            
            catch (Exception)
            {
                throw;
            }
            
        }
        /// <summary>
        /// Gets OfflineOrder based on OfflineOrderID.
        /// </summary>
        /// <param name="searchOrderID">Represents OfflineOrderID to search.</param>
        /// <returns>Returns OfflineOrder object.</returns>
        public async Task<OfflineOrder> GetOfflineOrderByOfflineOrderIDBL(Guid searchOfflineOrderID)
        {
            OfflineOrder matchingOfflineOrder = null;
            try
            {
                await Task.Run(() =>
                {
                    matchingOfflineOrder = offlineOrderDAL.GetOfflineOrderByOfflineOrderIDDAL(searchOfflineOrderID);
                });
            }
            catch (Exception)
            {
                throw;
            }
            return matchingOfflineOrder;
        }

        /// <summary>
        /// Gets OfflineOrder based on SalesPersonID.
        /// </summary>
        /// <param name="searchOfflineOrderID">Represents SalesPersonID to search.</param>
        /// <returns>Returns Offline Order list.</returns>
        public async Task<List<OfflineOrder>> GetOfflineOrderBySalesPersonIDBL(Guid salesPersonID)
        {
            List<OfflineOrder> matchingOfflineOrder = null;
            try
            {
                await Task.Run(() =>
                {
                    matchingOfflineOrder = offlineOrderDAL.GetOfflineOrderBySalespersonIDDAL(salesPersonID);
                });
            }
            catch (Exception)
            {
                throw;
            }
            return matchingOfflineOrder;
        }
        /// <summary>
        /// Gets Offline order list based on RetailerID.
        /// </summary>
        /// <param name="retailerID">Represents RetailerId to search.</param>
        /// <returns>Returns Offline Order list.</returns>
        public async Task<List<OfflineOrder>> GetOfflineOrderByRetailerIDBL(Guid retailerID)
        {
            List<OfflineOrder> matchingOfflineOrder = null;
            try
            {
                await Task.Run(() =>
                {
                    matchingOfflineOrder = offlineOrderDAL.GetOfflineOrderByRetailerIDDAL(retailerID);
                });
            }
            catch (Exception)
            {
                throw;
            }
            return matchingOfflineOrder;
        }
        /// <summary>
        /// Updates OfflineOrder based on OfflineOrderID.
        /// </summary>
        /// <param name="updateOfflineOrder">Represents OfflineOrder details including OfflineOrderID, OfflineOrderName etc.</param>
        /// <returns>Determinates whether the existing OfflineOrder is updated.</returns>
        public async Task<bool> UpdateOfflineOrderBL(OfflineOrder updateOfflineOrder)
        {
            bool OfflineOrderUpdated = false;
            try
            {
                if ((await Validate(updateOfflineOrder)) && (await GetOfflineOrderByOfflineOrderIDBL(updateOfflineOrder.OfflineOrderID)) != null)
                {
                    this.offlineOrderDAL.UpdateOfflineorderDAL(updateOfflineOrder);
                    OfflineOrderUpdated = true;
                    //Serialize();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return OfflineOrderUpdated;
        }

        /// <summary>
        /// Deletes OfflineOrder based on OfflineOrderID.
        /// </summary>
        /// <param name="deleteOfflineOrderID">Represents OfflineOrderID to delete.</param>
        /// <returns>Determinates whether the existing OfflineOrder is updated.</returns>
        public async Task<bool> DeleteOfflineOrderBL(Guid deleteOfflineOrderID)
        {
            bool offlineOrderDeleted = false;
            try
            {
                await Task.Run(() =>
                {
                    offlineOrderDeleted = offlineOrderDAL.DeleteOfflineOrderDAL(deleteOfflineOrderID);
                    //Serialize();
                });
            }
            catch (Exception)
            {
                throw;
            }
            return offlineOrderDeleted;
        }



        /// <summary>
        /// Disposes DAL object(s).
        /// </summary>
        public void Dispose()
        {
            ((OfflineOrderDAL)offlineOrderDAL).Dispose();
        }


        /// <summary>
        /// Invokes Serialize method of DAL.
        /// </summary>
        //public void Serialize()
        //{
        //    try
        //    {
        //        OfflineOrderDAL.Serialize();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        ///// <summary>
        /////Invokes Deserialize method of DAL.
        ///// </summary>
        //public void Deserialize()
        //{
        //    try
        //    {
        //        OfflineOrderDAL.Deserialize();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

    }
}








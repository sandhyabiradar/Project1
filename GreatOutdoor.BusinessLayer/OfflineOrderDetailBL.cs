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
    public class OfflineOrderDetailBL : BLBase<OfflineOrderDetail>, IOfflineOrderDetailBL, IDisposable
    {
        //fields
        OfflineOrderDetailDAL offlineOrderDetailDAL;

        /// <summary>
        /// Constructor.
        /// </summary>
        public OfflineOrderDetailBL()
        {
            this.offlineOrderDetailDAL = new OfflineOrderDetailDAL();
        }
        /// Validations on data before adding or updating.
        /// </summary>
        /// <param name="entityObject">Represents object to be validated.</param>
        /// <returns>Returns a boolean value, that indicates whether the data is valid or not.</returns>
        protected async override Task<bool> Validate(OfflineOrderDetail entityObject)
        {
            //Create string builder
            StringBuilder sb = new StringBuilder();
            bool valid = await base.Validate(entityObject);
            if (entityObject.TotalPrice <= 0)
            {
                valid = false;
                sb.Append(Environment.NewLine + "Total Price Cannot be negative");
            }
            if (entityObject.Quantity <= 0)
            {
                valid = false;
                sb.Append(Environment.NewLine + "Quantity Cannot be negative");
            }



            if (valid == false)
                throw new OfflineOrderException(sb.ToString());
            return valid;
        }


        /// <summary>
        /// Adds new OfflineOrderDetail to OfflineOrderDetails collection.
        /// </summary>
        /// <param name="newOfflineOrderDetail">Contains the OfflineOrderDetail details to be added.</param>
        /// <returns>Determinates whether the new OfflineOrderDetail is added.</returns>
        public async Task<bool> AddOfflineOrderDetailBL(OfflineOrderDetail newOfflineOrderDetail)
        {
            bool OfflineOrderDetailAdded = false;
            try
            {
                if (await Validate(newOfflineOrderDetail))
                {
                    await Task.Run(() =>
                    {
                        this.offlineOrderDetailDAL.AddOfflineOrderDetailDAL(newOfflineOrderDetail);
                        OfflineOrderDetailAdded = true;
                        Serialize();
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
            return OfflineOrderDetailAdded;
        }
        /// <summary>
        /// Gets all offline order detail from the collection.
        /// </summary>
        /// <returns>Returns list of all orderDetails.</returns>
        public async Task<List<OfflineOrderDetail>> GetAllOfflineOrderDetailBL()
        {
            List<OfflineOrderDetail> offlineOrderDetailList = null;
            try
            {
                await Task.Run(() =>
                {
                    offlineOrderDetailList = offlineOrderDetailDAL.GetAllOfflineOrderDetailDAL();
                });
            }
            catch (Exception)
            {
                throw;
            }
            return offlineOrderDetailList;
        }


        public async Task<List<OfflineOrderDetail>> GetOfflineOrderDetailByOfflineOrderIDBL(Guid OfflineOrderID)
        {
            List<OfflineOrderDetail> matchingOfflineOrderDetail = null;
            try
            {

                await Task.Run(() =>
                {
                    matchingOfflineOrderDetail = offlineOrderDetailDAL.GetOfflineOrderDetailByOfflineOrderIDDAL(OfflineOrderID);
                });
            }
            catch (Exception)
            {
                throw;
            }
            return matchingOfflineOrderDetail;
        }
        /// <summary>
        /// Gets OfflineOrderDetail based on OfflineOrderDetailID.
        /// </summary>
        /// <param name="searchOrderID">Represents OfflineOrderDetailID to search.</param>
        /// <returns>Returns OfflineOrderDetail object.</returns>
        public async Task<OfflineOrderDetail> GetOfflineOrderDetailByOfflineOrderDetailIDBL(Guid searchOfflineOrderDetailID)
        {
            OfflineOrderDetail matchingOfflineOrderDetail = null;
            try
            {
                await Task.Run(() =>
                {
                    matchingOfflineOrderDetail = offlineOrderDetailDAL.GetOfflineOrderDetailByOfflineOrderDetailIDDAL(searchOfflineOrderDetailID);
                });
            }
            catch (Exception)
            {
                throw;
            }
            return matchingOfflineOrderDetail;
        }



        /// <summary>
        /// Updates OfflineOrderDetail based on OfflineOrderDetailID.
        /// </summary>
        /// <param name="updateOfflineOrderDetail">Represents OfflineOrderDetail details including OfflineOrderDetailID, OfflineOrderDetailName etc.</param>
        /// <returns>Determinates whether the existing OfflineOrderDetail is updated.</returns>
        public async Task<bool> UpdateOfflineOrderDetailBL(OfflineOrderDetail updateOfflineOrderDetail)
        {
            bool OfflineOrderDetailUpdated = false;
            try
            {
                if ((await Validate(updateOfflineOrderDetail)) && (await GetOfflineOrderDetailByOfflineOrderDetailIDBL(updateOfflineOrderDetail.OfflineOrderDetailID)) != null)
                {
                    this.offlineOrderDetailDAL.UpdateOfflineOrderDetailDAL(updateOfflineOrderDetail);
                    OfflineOrderDetailUpdated = true;
                    Serialize();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return OfflineOrderDetailUpdated;
        }

        /// <summary>
        /// Deletes OfflineOrderDetail based on OfflineOrderDetailID.
        /// </summary>
        /// <param name="deleteOfflineOrderDetailID">Represents OfflineOrderDetailID to delete.</param>
        /// <returns>Determinates whether the existing OfflineOrderDetail is updated.</returns>
        public async Task<bool> DeleteOfflineOrderDetailBL(Guid deleteOfflineOrderDetailID)
        {
            bool OfflineOrderDetailDeleted = false;
            try
            {
                await Task.Run(() =>
                {
                    OfflineOrderDetailDeleted = offlineOrderDetailDAL.DeleteOfflineOrderDetailDAL(deleteOfflineOrderDetailID);
                    Serialize();
                });
            }
            catch (Exception)
            {
                throw;
            }
            return OfflineOrderDetailDeleted;
        }



        /// <summary>
        /// Disposes DAL object(s).
        /// </summary>
        public void Dispose()
        {
            ((OfflineOrderDetailDAL)offlineOrderDetailDAL).Dispose();
        }


        /// <summary>
        /// Invokes Serialize method of DAL.
        /// </summary>
        public void Serialize()
        {
            try
            {
                OfflineOrderDetailDAL.Serialize();
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
                OfflineOrderDetailDAL.Deserialize();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
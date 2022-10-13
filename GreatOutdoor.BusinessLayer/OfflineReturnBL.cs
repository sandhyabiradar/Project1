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
using Capgemini.GreatOutdoor.Helpers;

namespace Capgemini.GreatOutdoor.BusinessLayer
{
    /// <summary>
    /// Contains data access layer methods for inserting, updating, deleting OfflineReturns from OfflineReturns collection.
    /// </summary>
    public class OfflineReturnBL : BLBase<OfflineReturn>, IOfflineReturnBL, IDisposable
    {
        //fields
        OfflineReturnDALBase offlineReturnDAL;

        /// <summary>
        /// Constructor.
        /// </summary>
        public OfflineReturnBL()
        {
            this.offlineReturnDAL = new OfflineReturnDAL();
        }

        /// <summary>
        /// Validations on data before adding or updating.
        /// </summary>
        /// <param name="entityObject">Represents object to be validated.</param>
        /// <returns>Returns a boolean value, that indicates whether the data is valid or not.</returns>
        protected async override Task<bool> Validate(OfflineReturn entityObject)
        {
            //Create string builder
            StringBuilder sb = new StringBuilder();
            bool valid = await base.Validate(entityObject);

            //OfflineOrderID is Unique
            OfflineOrderBL iOfflineOrderBL = new OfflineOrderBL();

            var existingObject = await iOfflineOrderBL.GetOfflineOrderByOfflineOrderIDBL(entityObject.OfflineOrderID);
            if (existingObject == null)
            {
                valid = false;
                sb.Append(Environment.NewLine + $"OfflineOrderID {entityObject.OfflineOrderID} does not exists");
            }



            ////productID is unique
            //ProductBL iproductBL = new ProductBL();

            //var existingObject2 = await iproductBL.GetProductByProductIDBL(entityObject.ProductID);
            //if (existingObject2 == null)
            //{
            //    valid = false;
            //    sb.Append(Environment.NewLine + $"ProductID {entityObject.ProductID} already exists");
            //}

            if (valid == false)
                throw new GreatOutdoorException(sb.ToString());
            return valid;



        }

        /// <summary>
        /// Adds new OfflineReturn to OfflineReturns collection.
        /// </summary>
        /// <param name="newOfflineReturn">Contains the OfflineReturn details to be added.</param>
        /// <returns>Determinates whether the new OfflineReturn is added.</returns>
        public async Task<bool> AddOfflineReturnBL(OfflineReturn newOfflineReturn)
        {
            bool OfflineReturnAdded = false;
            try
            {
                if (await Validate(newOfflineReturn))
                {
                    await Task.Run(() =>
                    {
                        this.offlineReturnDAL.AddOfflineReturnDAL(newOfflineReturn);
                        OfflineReturnAdded = true;
                        Serialize();
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
            return OfflineReturnAdded;
        }

        /// <summary>
        /// Gets all OfflineReturns from the collection.
        /// </summary>
        /// <returns>Returns list of all OfflineReturns.</returns>
        public async Task<List<OfflineReturn>> GetAllOfflineReturnsBL()
        {
            List<OfflineReturn> OfflineReturnsList = null;
            try
            {
                await Task.Run(() =>
                {
                    OfflineReturnsList = offlineReturnDAL.GetAllOfflineReturnsDAL();
                });
            }
            catch (Exception)
            {
                throw;
            }
            return OfflineReturnsList;
        }

        /// <summary>
        /// Gets OfflineReturn based on OfflineReturnID.
        /// </summary>
        /// <param name="searchOfflineReturnID">Represents OfflineReturnID to search.</param>
        /// <returns>Returns OfflineReturn object.</returns>
        public async Task<OfflineReturn> GetOfflineReturnByOfflineReturnIDBL(Guid searchOfflineReturnID)
        {
            OfflineReturn matchingOfflineReturn = null;
            try
            {
                await Task.Run(() =>
                {
                    matchingOfflineReturn = offlineReturnDAL.GetOfflineReturnByOfflineReturnIDDAL(searchOfflineReturnID);
                });
            }
            catch (Exception)
            {
                throw;
            }
            return matchingOfflineReturn;
        }

        /// <summary>
        /// Gets Offlinereturn based on PurposeOfReturn.
        /// </summary>
        /// <param name="purposeOfReturn">Represents purposeOfReturn to search.</param>
        /// <returns>Returns OfflineReturn object.</returns>
        public async Task<List<OfflineReturn>> GetOfflineReturnsByPurposeBL(PurposeOfReturn purpose)
        {
            List<OfflineReturn> matchingOfflineReturns = new List<OfflineReturn>();
            try
            {
                await Task.Run(() =>
                {
                    matchingOfflineReturns = offlineReturnDAL.GetOfflineReturnsByPurposeDAL(purpose);
                });
            }
            catch (Exception)
            {
                throw;
            }
            return matchingOfflineReturns;
        }



        /// <summary>
        /// Updates OfflineReturn based on OfflineReturnID.
        /// </summary>
        /// <param name="updateOfflineReturn">Represents OfflineReturn details including OfflineReturnID, PurposeOfReturn etc.</param>
        /// <returns>Determinates whether the existing OfflineReturn is updated.</returns>
        public async Task<bool> UpdateOfflineReturnBL(OfflineReturn updateOfflineReturn)
        {
            bool OfflineReturnUpdated = false;
            try
            {
                if ((await Validate(updateOfflineReturn)) && (await GetOfflineReturnByOfflineReturnIDBL(updateOfflineReturn.OfflineReturnID)) != null)
                {
                    this.offlineReturnDAL.UpdateOfflineReturnDAL(updateOfflineReturn);
                    OfflineReturnUpdated = true;
                    Serialize();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return OfflineReturnUpdated;
        }

        /// <summary>
        /// Deletes OfflineReturn based on OfflineReturnID.
        /// </summary>
        /// <param name="deleteOfflineReturnID">Represents OfflineReturnID to delete.</param>
        /// <returns>Determinates whether the existing OfflineReturn is updated.</returns>
        public async Task<bool> DeleteOfflineReturnBL(Guid deleteOfflineReturnID)
        {
            bool OfflineReturnDeleted = false;
            try
            {
                await Task.Run(() =>
                {
                    OfflineReturnDeleted = offlineReturnDAL.DeleteOfflineReturnDAL(deleteOfflineReturnID);
                    Serialize();
                });
            }
            catch (Exception)
            {
                throw;
            }
            return OfflineReturnDeleted;
        }

        /// <summary>
        /// Disposes DAL object(s).
        /// </summary>
        public void Dispose()
        {
            ((OfflineReturnDAL)offlineReturnDAL).Dispose();
        }

        /// <summary>
        /// Invokes Serialize method of DAL.
        /// </summary>
        public static void Serialize()
        {
            try
            {
                OfflineReturnDAL.Serialize();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///Invokes Deserialize method of DAL.
        /// </summary>
        public static void Deserialize()
        {
            try
            {
                OfflineReturnDAL.Deserialize();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
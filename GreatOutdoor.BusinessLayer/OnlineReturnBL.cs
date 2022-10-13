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
    /// Contains data access layer methods for inserting, updating, deleting onlineReturns from OnlineReturns collection.
    /// </summary>
    public class OnlineReturnBL : BLBase<OnlineReturn>, IOnlineReturnBL, IDisposable
    {
        //fields
        OnlineReturnDALBase onlineReturnDAL;

        /// <summary>
        /// Constructor.
        /// </summary>
        public OnlineReturnBL()
        {
            this.onlineReturnDAL = new OnlineReturnDAL();
        }

        /// <summary>
        /// Validations on data before adding or updating.
        /// </summary>
        /// <param name="entityObject">Represents object to be validated.</param>
        /// <returns>Returns a boolean value, that indicates whether the data is valid or not.</returns>
        protected async override Task<bool> Validate(OnlineReturn entityObject)
        {
            //Create string builder
            StringBuilder sb = new StringBuilder();
            bool valid = await base.Validate(entityObject);

            //OrderID is Unique
            OrderBL iorderBL = new OrderBL();

            var existingObject = await iorderBL.GetOrderByOrderIDBL(entityObject.OrderID);
            if (existingObject == null)
            {
                valid = false;
                sb.Append(Environment.NewLine + $"OrderID {entityObject.OrderID} does not exists");
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
        /// Adds new onlineReturn to OnlineReturns collection.
        /// </summary>
        /// <param name="newOnlineReturn">Contains the onlineReturn details to be added.</param>
        /// <returns>Determinates whether the new onlineReturn is added.</returns>
        public async Task<bool> AddOnlineReturnBL(OnlineReturn newOnlineReturn)
        {
            bool onlineReturnAdded = false;
            try
            {
                if (await Validate(newOnlineReturn))
                {
                    await Task.Run(() =>
                    {
                        this.onlineReturnDAL.AddOnlineReturnDAL(newOnlineReturn);
                        onlineReturnAdded = true;
                        //Serialize();
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
            return onlineReturnAdded;
        }

        /// <summary>
        /// Gets all onlineReturns from the collection.
        /// </summary>
        /// <returns>Returns list of all onlineReturns.</returns>
        public async Task<List<OnlineReturn>> GetAllOnlineReturnsBL()
        {
            List<OnlineReturn> onlineReturnsList = null;
            try
            {
                await Task.Run(() =>
                {
                    onlineReturnsList = onlineReturnDAL.GetAllOnlineReturnsDAL();
                });
            }
            catch (Exception)
            {
                throw;
            }
            return onlineReturnsList;
        }

        /// <summary>
        /// Gets onlineReturn based on OnlineReturnID.
        /// </summary>
        /// <param name="searchOnlineReturnID">Represents OnlineReturnID to search.</param>
        /// <returns>Returns OnlineReturn object.</returns>
        public async Task<OnlineReturn> GetOnlineReturnByOnlineReturnIDBL(Guid searchOnlineReturnID)
        {
            OnlineReturn matchingOnlineReturn = null;
            try
            {
                await Task.Run(() =>
                {
                    matchingOnlineReturn = onlineReturnDAL.GetOnlineReturnByOnlineReturnIDDAL(searchOnlineReturnID);
                });
            }
            catch (Exception)
            {
                throw;
            }
            return matchingOnlineReturn;
        }

        /// <summary>
        /// Gets onlinereturn based on PurposeOfReturn.
        /// </summary>
        /// <param name="purposeOfReturn">Represents purposeOfReturn to search.</param>
        /// <returns>Returns OnlineReturn object.</returns>
        public async Task<List<OnlineReturn>> GetOnlineReturnsByPurposeBL(PurposeOfReturn purpose)
        {
            List<OnlineReturn> matchingOnlineReturns = new List<OnlineReturn>();
            try
            {
                await Task.Run(() =>
                {
                    matchingOnlineReturns = onlineReturnDAL.GetOnlineReturnsByPurposeDAL(purpose);
                });
            }
            catch (Exception)
            {
                throw;
            }
            return matchingOnlineReturns;
        }

        public async Task<List<OnlineReturn>> GetOnlineReturnByRetailerIDBL(Guid retailerID)
        {
            List<OnlineReturn> matchingOnlineReturn = null;
            try
            {
                await Task.Run(() =>
                {
                    matchingOnlineReturn = onlineReturnDAL.GetOnlineReturnByRetailerIDDAL(retailerID);
                });

            }
            catch (Exception)
            {

                throw;
            }
            return matchingOnlineReturn;

        }



        /// <summary>
        /// Updates onlineReturn based on OnlineReturnID.
        /// </summary>
        /// <param name="updateOnlineReturn">Represents OnlineReturn details including OnlineReturnID, PurposeOfReturn etc.</param>
        /// <returns>Determinates whether the existing onlineReturn is updated.</returns>
        public async Task<bool> UpdateOnlineReturnBL(OnlineReturn updateOnlineReturn)
        {
            bool onlineReturnUpdated = false;
            try
            {
                if ((await Validate(updateOnlineReturn)) && (await GetOnlineReturnByOnlineReturnIDBL(updateOnlineReturn.OnlineReturnID)) != null)
                {
                    this.onlineReturnDAL.UpdateOnlineReturnDAL(updateOnlineReturn);
                    onlineReturnUpdated = true;
                  //  Serialize();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return onlineReturnUpdated;
        }

        /// <summary>
        /// Deletes onlineReturn based on OnlineReturnID.
        /// </summary>
        /// <param name="deleteOnlineReturnID">Represents OnlineReturnID to delete.</param>
        /// <returns>Determinates whether the existing onlineReturn is updated.</returns>
        public async Task<bool> DeleteOnlineReturnBL(Guid deleteOnlineReturnID)
        {
            bool onlineReturnDeleted = false;
            try
            {
                await Task.Run(() =>
                {
                    onlineReturnDeleted = onlineReturnDAL.DeleteOnlineReturnDAL(deleteOnlineReturnID);
                    Serialize();
                });
            }
            catch (Exception)
            {
                throw;
            }
            return onlineReturnDeleted;
        }
        
        /// <summary>
        /// Disposes DAL object(s).
        /// </summary>
        public void Dispose()
        {
            ((OnlineReturnDAL)onlineReturnDAL).Dispose();
        }

        /// <summary>
        /// Invokes Serialize method of DAL.
        /// </summary>
        public static void Serialize()
        {
            try
            {
                OnlineReturnDAL.Serialize();
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
                OnlineReturnDAL.Deserialize();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
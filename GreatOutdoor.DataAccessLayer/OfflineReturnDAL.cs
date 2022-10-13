using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Capgemini.GreatOutdoor.Contracts.DALContracts;
using Capgemini.GreatOutdoor.Entities;
using Capgemini.GreatOutdoor.Exceptions;
using Capgemini.GreatOutdoor.Helpers;

namespace Capgemini.GreatOutdoor.DataAccessLayer
{
    /// <summary>
    /// Contains data access layer methods for inserting, updating, deleting OfflineReturn from OfflineReturn collection.
    /// </summary>
    public class OfflineReturnDAL : OfflineReturnDALBase, IDisposable
    {
        /// <summary>
        /// Adds new OfflineReturn to OfflineReturn collection.
        /// </summary>
        /// <param name="newOfflineReturn">Contains the OfflineReturn details to be added.</param>
        /// <returns>Determinates whether the new OfflineReturn is added.</returns>
        public override bool AddOfflineReturnDAL(OfflineReturn newOfflineReturn)
        {
            bool OfflineReturnAdded = false;
            try
            {
                newOfflineReturn.OfflineReturnID = Guid.NewGuid();
                newOfflineReturn.CreationDateTime = DateTime.Now;
                newOfflineReturn.LastModifiedDateTime = DateTime.Now;
                OfflineReturnList.Add(newOfflineReturn);
                OfflineReturnAdded = true;
            }
            catch (Exception)
            {
                throw;
            }
            return OfflineReturnAdded;
        }

        /// <summary>
        /// Gets all OfflineReturn from the collection.
        /// </summary>
        /// <returns>Returns list of all OfflineReturn.</returns>
        public override List<OfflineReturn> GetAllOfflineReturnsDAL()
        {
            return OfflineReturnList;
        }

        /// <summary>
        /// Gets OfflineReturn based on OfflineReturnID.
        /// </summary>
        /// <param name="searchOfflineReturnID">Represents OfflineReturnID to search.</param>
        /// <returns>Returns OfflineReturn object.</returns>
        public override OfflineReturn GetOfflineReturnByOfflineReturnIDDAL(Guid searchOfflineReturnID)
        {
            OfflineReturn matchingOfflineReturn = null;
            try
            {
                //Find OfflineReturn based on searchOfflineReturnID
                matchingOfflineReturn = OfflineReturnList.Find
                (
                    (item) => { return item.OfflineReturnID == searchOfflineReturnID; }
                );
            }
            catch (Exception)
            {
                throw;
            }
            return matchingOfflineReturn;
        }

        /// <summary>
        /// Gets OfflineReturn based on Purpose.
        /// </summary>
        /// <param name="Purpose">Represents Purpose to search.</param>
        /// <returns>Returns OfflineReturn object.</returns>
        public override List<OfflineReturn> GetOfflineReturnsByPurposeDAL(PurposeOfReturn purpose)
        {
            List<OfflineReturn> matchingOfflineReturns = new List<OfflineReturn>();
            try
            {
                //Find All OfflineReturns based on purpose
                matchingOfflineReturns = OfflineReturnList.FindAll(
                    (item) => { return item.Purpose == purpose; }
                );
            }
            catch (Exception)
            {
                throw;
            }
            return matchingOfflineReturns;
        }





        /// <summary>
        /// Updates onllineReturn based on OfflineReturnID.
        /// </summary>
        /// <param name="updateOfflineReturn">Represents OfflineReturn details including OfflineReturnID, PurposeOfReturn etc.</param>
        /// <returns>Determinates whether the existing OfflineReturn is updated.</returns>
        public override bool UpdateOfflineReturnDAL(OfflineReturn updateOfflineReturn)
        {
            bool OfflineReturnUpdated = false;
            try
            {
                //Find OfflineReturn based on OfflineReturnID
                OfflineReturn matchingOfflineReturn = GetOfflineReturnByOfflineReturnIDDAL(updateOfflineReturn.OfflineReturnID);

                if (matchingOfflineReturn != null)
                {
                    //Update OfflineReturn details
                    ReflectionHelpers.CopyProperties(updateOfflineReturn, matchingOfflineReturn, new List<string>() { "PurposeOfReturn", "OrderID", "ProductID", "NoOfReturn", "Email" });
                    matchingOfflineReturn.LastModifiedDateTime = DateTime.Now;

                    OfflineReturnUpdated = true;
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
        public override bool DeleteOfflineReturnDAL(Guid deleteOfflineReturnID)
        {
            bool OfflineReturnDeleted = false;
            try
            {
                //Find OfflineReturn based on searchOfflineReturnID
                OfflineReturn matchingOfflineReturn = OfflineReturnList.Find(
                    (item) => { return item.OfflineReturnID == deleteOfflineReturnID; }
                );

                if (matchingOfflineReturn != null)
                {
                    //Delete OfflineReturn from the collection
                    OfflineReturnList.Remove(matchingOfflineReturn);
                    OfflineReturnDeleted = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return OfflineReturnDeleted;
        }


        /// <summary>
        /// Clears unmanaged resources such as db connections or file streams.
        /// </summary>
        public void Dispose()
        {
            //No unmanaged resources currently
        }
    }
}

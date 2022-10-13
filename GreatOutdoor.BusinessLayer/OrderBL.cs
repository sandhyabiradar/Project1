using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Capgemini.GreatOutdoor.Entities;
using Capgemini.GreatOutdoor.DataAccessLayer;
using Capgemini.GreatOutdoor.Contracts.DALContracts;
using Capgemini.GreatOutdoor.Contracts;
using System.Text;

namespace Capgemini.GreatOutdoor.BusinessLayer
{
    public class OrderBL : BLBase<Order>, IOrderBL, IDisposable
    {
        //fields
        OrderDALBase orderDAL;

        /// <summary>
        /// Constructor.
        /// </summary>
        public OrderBL()
        {
            this.orderDAL = new OrderDAL();
        }
        protected async Task<bool> Validate(Order entityObject)
        {
            //Create string builder
            StringBuilder sb = new StringBuilder();
            bool valid = await base.Validate(entityObject);
            if (entityObject.OrderAmount <= 0)
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
            if (valid == false)
            { throw new Exception(sb.ToString()); }
            return valid;
        }
            /// <summary>
            /// Adds new systemUser to SystemUsers collection.
            /// </summary>
            /// <param name="newOrder">Contains the systemUser details to be added.</param>
            /// <returns>Determinates whether the new systemUser is added.</returns>
            public async Task<(bool, Guid)> AddOrderBL(Order newOrder)
        {
            Guid OrderId;
            bool orderAdded = false;
            try
            {

                if (await Validate(newOrder))
                {
                    await Task.Run(() =>
                    {
                        (orderAdded, newOrder.OrderId) = this.orderDAL.AddOrderDAL(newOrder);

                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
            return (orderAdded, newOrder.OrderId);
        }

        /// <summary>
        /// Gets order based on orderID.
        /// </summary>
        /// <param name="searchOrderID">Represents OrderID to search.</param>
        /// <returns>Returns Order object.</returns>
        public async Task<Order> GetOrderByOrderIDBL(Guid searchOrderID)
        {
            Order matchingOrder = null;
            try
            {
                await Task.Run(() =>
                {
                    matchingOrder = orderDAL.GetOrderByOrderIDDAL(searchOrderID);
                });
            }
            catch (Exception)
            {
                throw;
            }
            return matchingOrder;
        }

        public async Task<Order> GetOrderByOrderNumberBL(double orderNumber)
        {
            Order matchingOrder = null;
            try
            {
                await Task.Run(() =>
                {
                    matchingOrder = orderDAL.GetOrderByOrderNumberDAL(orderNumber);
                });
            }
            catch (Exception)
            {
                throw;
            }
            return matchingOrder;
        }

        public async Task<List<Order>> GetOrdersByRetailerIDBL(Guid searchRetailerID)
        {
            List<Order> matchingOrder = null;
            try
            {
                await Task.Run(() =>
                {
                    matchingOrder = orderDAL.GetOrdersByRetailerIDDAL(searchRetailerID);
                });
            }
            catch (Exception)
            {
                throw;
            }
            return matchingOrder;
        }



        /// <summary>
        /// Updates order based on OrderID.
        /// </summary>
        /// <param name="updateOrder">Represents Order details like OrderId.</param>
        /// <returns>Determinates whether the existing Order is updated.</returns>
        public async Task<bool> UpdateOrderBL(Order updateOrder)
        {
            bool orderUpdated = false;
            try
            {
                if ((await Validate(updateOrder)) && (await GetOrderByOrderIDBL(updateOrder.OrderId)) != null)
                {
                    this.orderDAL.UpdateOrderDAL(updateOrder);
                    orderUpdated = true;
                    Serialize();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return orderUpdated;
        }




        /// <summary>
        /// Deletes order based on orderID.
        /// </summary>
        /// <param name="deleteOrderID">Represents orderID to delete.</param>
        /// <returns>Determinates whether the existing orderlist is updated.</returns>
        public async Task<bool> DeleteOrderBL(Guid deleteOrderID)
        {
            bool orderDeleted = false;
            try
            {
                await Task.Run(() =>
                {
                    orderDeleted = orderDAL.DeleteOrderDAL(deleteOrderID);
                    //Serialize();
                });
            }
            catch (Exception)
            {
                throw;
            }
            return orderDeleted;
        }





        /// <summary>
        /// Disposes DAL object(s).
        /// </summary>
        public void Dispose()
        {
            ((OrderDAL)orderDAL).Dispose();
        }

        /// <summary>
        /// Invokes Serialize method of DAL.
        /// </summary>
        public void Serialize()
        {
            try
            {
                OrderDetailDAL.Serialize();
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
                OrderDetailDAL.Deserialize();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}

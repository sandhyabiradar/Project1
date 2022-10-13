﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capgemini.GreatOutdoor.Entities;
using Capgemini.GreatOutdoor.DataAccessLayer;
using Capgemini.GreatOutdoor.Contracts;
using Capgemini.GreatOutdoor.Helpers;
using Capgemini.GreatOutdoor.Exceptions;

namespace Capgemini.GreatOutdoor.BusinessLayer
{
    public class OrderDetailBL : BLBase<OrderDetail>, IOrderDetailBL, IDisposable
    {
        //fields
        OrderDetailDALBase orderDetailDAL;

        /// <summary>
        /// Constructor.
        /// </summary>
        public OrderDetailBL()
        {
            this.orderDetailDAL = new OrderDetailDAL();
        }

        protected async Task<bool> Validate(OrderDetail entityObject)
        {
            //Create string builder
            StringBuilder sb = new StringBuilder();
            bool valid = await base.Validate(entityObject);
            if (entityObject.ProductPrice <= 0)
            {
                valid = false;
                sb.Append(Environment.NewLine + "Total Price cannot be negative");
            }
            if (entityObject.ProductQuantityOrdered <= 0)
            {
                valid = false;
                sb.Append(Environment.NewLine + "Total Quantity cannot be negative");
            }
            //RetailerID is Unique
            ProductBL iProductBL = new ProductBL();

            var existingObject = await iProductBL.GetProductByProductIDBL(entityObject.ProductID);
            if (existingObject == null)
            {
                valid = false;
                sb.Append(Environment.NewLine + $"RetailerID {entityObject.ProductID} does not exists");
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
        public async Task<(bool, Guid)> AddOrderDetailsBL(OrderDetail newOrder)
        {
            bool orderAdded = false;
            Guid OrderGuid=Guid.NewGuid();
            try
            {
                if (await Validate(newOrder))
                {
                    await Task.Run(() =>
                    {
                        (orderAdded, OrderGuid) = this.orderDetailDAL.AddOrderDetailsDAL(newOrder);

                        Serialize();
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
            return (orderAdded, OrderGuid);
        }

        /// <summary>
        /// Gets order based on orderID.
        /// </summary>
        /// <param name="searchOrderID">Represents OrderID to search.</param>
        /// <returns>Returns Order object.</returns>
        public async Task<List<OrderDetail>> GetOrderDetailsByOrderIDBL(Guid searchOrderID)
        {
            List<OrderDetail> matchingOrder = null;
            try
            {
                await Task.Run(() =>
                {
                    matchingOrder = orderDetailDAL.GetOrderDetailsByOrderIDDAL(searchOrderID);
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
        public async Task<bool> UpdateOrderDetailsBL(OrderDetail updateOrder)
        {
            bool orderUpdated = false;
            try
            {
                if ((await Validate(updateOrder)) && (await GetOrderDetailsByOrderIDBL(updateOrder.OrderId)) != null)
                {
                    this.orderDetailDAL.UpdateOrderDetailsDAL(updateOrder);
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
        public async Task<bool> DeleteOrderDetailsBL(Guid deleteOrderID)
        {
            bool orderDeleted = false;
            try
            {
                await Task.Run(() =>
                {
                    orderDeleted = orderDetailDAL.DeleteOrderDetailsDAL(deleteOrderID);
                    //Serialize();
                });
            }
            catch (Exception)
            {
                throw;
            }
            return orderDeleted;
        }



        public async Task<bool> UpdateOrderDispatchedStatusBL(Guid orderID)
        {
            bool orderDispatched = false;
            try
            {
                await Task.Run(() =>
                {
                    orderDispatched = orderDetailDAL.UpdateOrderDispatchedStatusDAL(orderID);
                    //Serialize();
                });
            }
            catch (Exception)
            {
                throw;
            }
            return orderDispatched;
        }

        public async Task<bool> UpdateOrderShippedStatusBL(Guid orderID)
        {
            bool orderShipped = false;
            try
            {
                await Task.Run(() =>
                {
                    orderShipped = orderDetailDAL.UpdateOrderShippedStatusDAL(orderID);
                    Serialize();
                });
            }
            catch (Exception)
            {
                throw;
            }
            return orderShipped;
        }

        public async Task<bool> UpdateOrderDeliveredStatusBL(Guid orderID)
        {
            bool orderDelivered = false;
            try
            {
                await Task.Run(() =>
                {
                    orderDelivered = orderDetailDAL.UpdateOrderDeliveredStatusDAL(orderID);
                    Serialize();
                });
            }
            catch (Exception)
            {
                throw;
            }
            return orderDelivered;
        }

        /// <summary>
        /// Disposes DAL object(s).
        /// </summary>
        public void Dispose()
        {
            ((OrderDetailDAL)orderDetailDAL).Dispose();
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

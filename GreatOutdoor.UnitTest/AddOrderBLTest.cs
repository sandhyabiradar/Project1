using Capgemini.GreatOutdoor.BusinessLayer;
using Capgemini.GreatOutdoor.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Capgemini.GreatOutdoor.UnitTests
{
    [TestClass]
    public class AddOrderBLTest
    {
        /// <summary>
        /// AddOrder to the Collection if it is valid.
        /// </summary>
        [TestMethod]
        public async Task AddValidOrder()
        {
            //Arrange
            OrderBL orderBL = new OrderBL();
            Order order = new Order() { OrderAmount = 10, TotalQuantity = 10, };
            bool isAdded = false;
            Guid newGuid;
            string errorMessage = null;

            //Act
            try
            {
                Retailer retailer = new Retailer() { RetailerID = Guid.NewGuid(), RetailerName = "Abhishek", RetailerMobile = "9039607074", Password = "Abhishek@2", Email = "abhi.rajawat11@gmail.com" };

                RetailerBL retailerBL = new RetailerBL();
                bool isAdded1 = false;
                Guid newGuid1;
                (isAdded1, newGuid1) = await retailerBL.AddRetailerBL(retailer);
                retailer.RetailerID = newGuid1;
                order.RetailerID = newGuid1;
                (isAdded, newGuid) = await orderBL.AddOrderBL(order);
                
            }
            catch (Exception ex)
            {
                isAdded = false;
                errorMessage = ex.Message;
            }
            finally
            {
                //Assert
                Assert.IsTrue(isAdded, errorMessage);
            }
        }

        /// <summary>
        /// Retailer ID should be unique and present in database
        /// </summary>
        [TestMethod]
        public async Task RetailerIDNotMatch()
        {
            //Arrange
            OrderBL orderBL = new OrderBL();
            Order order = new Order() { RetailerID = Guid.NewGuid(), OrderAmount = 10, TotalQuantity = 10 };
            bool isAdded = false;
            Guid newGuid;
            string errorMessage = null;

            //Act
            try
            {
                (isAdded, newGuid) = await orderBL.AddOrderBL(order);
            }
            catch (Exception ex)
            {
                isAdded = false;
                errorMessage = ex.Message;
            }
            finally
            {
                //Assert
                Assert.IsFalse(isAdded, errorMessage);
            }
        }


        /// <summary>
        /// Total Amount of Order can't be negative
        /// </summary>
        [TestMethod]
        public async Task TotalAmountCannotBeNegative()
        {
            //Arrange
            OrderBL orderBL = new OrderBL();
            Order order = new Order() { RetailerID = Guid.NewGuid(), OrderAmount = -10, TotalQuantity = 10 };
            bool isAdded = false;
            Guid newGuid;
            string errorMessage = null;

            //Act
            try
            {
                (isAdded, newGuid) = await orderBL.AddOrderBL(order);
            }
            catch (Exception ex)
            {
                isAdded = false;
                errorMessage = ex.Message;
            }
            finally
            {
                //Assert
                Assert.IsFalse(isAdded, errorMessage);
            }
        }

        /// <summary>
        /// Total Quantity of Order can't be negative
        /// </summary>
        [TestMethod]
        public async Task TotalQuantityCannotBeNegative()
        {
            //Arrange
            OrderBL orderBL = new OrderBL();
            Order order = new Order() { RetailerID = Guid.NewGuid(), OrderAmount = 10, TotalQuantity = -10 };
            bool isAdded = false;
            Guid newGuid;
            string errorMessage = null;

            //Act
            try
            {
                (isAdded, newGuid) = await orderBL.AddOrderBL(order);
            }
            catch (Exception ex)
            {
                isAdded = false;
                errorMessage = ex.Message;
            }
            finally
            {
                //Assert
                Assert.IsFalse(isAdded, errorMessage);
            }
        }


    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Capgemini.GreatOutdoor.BusinessLayer;
using Capgemini.GreatOutdoor.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.GreatOutdoor.UnitTests
{
    [TestClass]
    public class AddOfflineOrderBLTest
    {
        /// <summary>
        /// Add OfflineOrder to the Collection if it is valid.
        /// </summary>
        [TestMethod]
        public async Task AddValidOfflineOrder()
        {
            //Arrange
            OfflineOrderBL offlineOrderBL = new OfflineOrderBL();
            OfflineOrder offlineOrder = new OfflineOrder() { TotalOrderAmount = 10, TotalQuantity = 10 };
            bool isAdded = false;

            string errorMessage = null;

            //Act
            try
            {
                Retailer retailer = new Retailer() { RetailerID = Guid.NewGuid(), RetailerName = "Abhishek", RetailerMobile = "9039607074", Password = "Abhishek@2", Email = "abhi.rajawat@gmail.com" };

                RetailerBL retailerBL = new RetailerBL();
                bool isAdded1 = false;
                Guid newGuid1;
                (isAdded1, newGuid1) = await retailerBL.AddRetailerBL(retailer);
                retailer.RetailerID = newGuid1;
                SalesPerson salesPerson = new SalesPerson() { SalesPersonID = Guid.NewGuid(), SalesPersonName = "Abhishek", SalesPersonMobile = "7987874863", Password = "Abhishek@2", Email = "abhi.rajawat@gmail.com" };
                SalesPersonBL salesPersonBL = new SalesPersonBL();

                bool isAdded2 = false;
                Guid newGuid2;
                (isAdded2, newGuid2) = await salesPersonBL.AddSalesPersonBL(salesPerson);
                salesPerson.SalesPersonID = newGuid2;
                Guid newGuid;


                offlineOrder.RetailerID = newGuid1;

                offlineOrder.SalesPersonID = newGuid2;
                (isAdded, newGuid) = await offlineOrderBL.AddOfflineOrderBL(offlineOrder);
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
            OfflineOrderBL OfflineOrderBL = new OfflineOrderBL();
            OfflineOrder OfflineOrder = new OfflineOrder() { RetailerID = Guid.NewGuid(), SalesPersonID = default(Guid), TotalOrderAmount = 10, TotalQuantity = 10 };
            bool isAdded = false;
            Guid newGuid;
            string errorMessage = null;

            //Act
            try
            {
                (isAdded, newGuid) = await OfflineOrderBL.AddOfflineOrderBL(OfflineOrder);
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
        /// SalesPerson ID should be unique and present in database
        /// </summary>
        [TestMethod]
        public async Task SalesPersonIDNotMatch()
        {
            //Arrange
            OfflineOrderBL OfflineOrderBL = new OfflineOrderBL();
            OfflineOrder OfflineOrder = new OfflineOrder() { };
            bool isAdded = false;
            Guid newGuid;

            string errorMessage = null;

            //Act
            try
            {
                (isAdded, newGuid) = await OfflineOrderBL.AddOfflineOrderBL(OfflineOrder);
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
        /// Total Amount of Offline Order can't be negative
        /// </summary>
        [TestMethod]
        public async Task TotalAmountCannotBeNegative()
        {
            //Arrange
            OfflineOrderBL OfflineOrderBL = new OfflineOrderBL();
            OfflineOrder OfflineOrder = new OfflineOrder() { RetailerID = Guid.NewGuid(), SalesPersonID = default(Guid), TotalOrderAmount = -10, TotalQuantity = 10 };
            bool isAdded = false;
            Guid newGuid;
            string errorMessage = null;

            //Act
            try
            {
                (isAdded, newGuid) = await OfflineOrderBL.AddOfflineOrderBL(OfflineOrder);
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
        /// Total Quantity of Offline Order can't be negative
        /// </summary>
        [TestMethod]
        public async Task TotalQuantityCannotBeNegative()
        {
            //Arrange
            OfflineOrderBL OfflineOrderBL = new OfflineOrderBL();
            OfflineOrder OfflineOrder = new OfflineOrder() { RetailerID = Guid.NewGuid(), SalesPersonID = default(Guid), TotalOrderAmount = 11, TotalQuantity = -10 };
            bool isAdded = false;
            Guid newGuid;
            string errorMessage = null;

            //Act
            try
            {
                (isAdded, newGuid) = await OfflineOrderBL.AddOfflineOrderBL(OfflineOrder);
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
        /// OfflineOrderName should contain at least two characters
        /// </summary>

    }
    //[TestClass]
    //public class UpdateOfflineOrderBLTest
    //{
    //    /// <summary>
    //    /// Update OfflineOrder to the Collection if it is valid.
    //    /// </summary>
    //    [TestMethod]
    //    public async Task UpdateValidOfflineOrder()
    //    {
    //        //Arrange
    //        OfflineOrderBL offlineOrderBL = new OfflineOrderBL();
    //        OfflineOrder offlineOrder = new OfflineOrder() { TotalOrderAmount = 10, TotalQuantity = 10 };
    //        bool isAdded = false;
    //        Guid newGuid;
    //        string errorMessage = null;

    //        //Act
    //        try
    //        {
    //            Retailer retailer = new Retailer();
    //            RetailerBL retailerBL = new RetailerBL();
    //            bool isAdded1 = false;
    //            Guid newGuid1;
    //            (isAdded1, newGuid1) = await retailerBL.AddRetailerBL(retailer);
    //            SalesPerson salesPerson = new SalesPerson();
    //            SalesPersonBL salesPersonBL = new SalesPersonBL();
    //            bool isAdded2 = false;
    //            Guid newGuid2;
    //            (isAdded2, newGuid2) = await salesPersonBL.AddSalesPersonBL(salesPerson);

    //            (isAdded, newGuid) = await offlineOrderBL.AddOfflineOrderBL(offlineOrder);
    //            offlineOrder.RetailerID = newGuid1;
    //            offlineOrder.SalesPersonID = newGuid2;
    //        }
    //        catch (Exception ex)
    //        {
    //            isAdded = false;
    //            errorMessage = ex.Message;
    //        }
    //        finally
    //        {
    //            //Assert
    //            Assert.IsTrue(isAdded, errorMessage);
    //        }
    //    }

    //}

}
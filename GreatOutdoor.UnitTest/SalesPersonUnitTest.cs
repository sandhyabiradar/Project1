
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Capgemini.GreatOutdoor.BusinessLayer;
using Capgemini.GreatOutdoor.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.GreatOutdoor.UnitTests

{
    //Developed By Ayush Agrawal
    // Creation Date: 01-10-2019
    //Last Modified Date: 
    //Module Name: AddSalesPersonUnitTest
    //Project: GreatOutdoor


    [TestClass]
    public class AddSalesPersonBLTest
    {
        /// <summary>
        /// Add SalesPerson to the Collection if it is valid.
        /// </summary>
        [TestMethod]
        public async Task AddValidSalesPerson()
        {
            //Arrange
            SalesPersonBL salesPersonBL = new SalesPersonBL();
            SalesPerson salesPerson = new SalesPerson() { SalesPersonName = "Scott", SalesPersonMobile = "9876543210", Password = "Scott123#", Email = "scott@gmail.com" };
            bool isAdded = false;
            string errorMessage = null;
            Guid newGuid;

            //Act
            try
            {
                (isAdded,newGuid) = await salesPersonBL.AddSalesPersonBL(salesPerson);
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
        /// SalesPerson Name can't be null
        /// </summary>
        [TestMethod]
        public async Task SalesPersonNameCanNotBeNull()
        {
            //Arrange
            SalesPersonBL salesPersonBL = new SalesPersonBL();
            SalesPerson salesPerson = new SalesPerson() { SalesPersonName = null, SalesPersonMobile = "9988776655", Password = "Smith123#", Email = "smith@gmail.com" };
            bool isAdded = false;
            string errorMessage = null;
            Guid newGuid;

            //Act
            try
            {
                (isAdded, newGuid) = await salesPersonBL.AddSalesPersonBL(salesPerson);
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
        /// SalesPerson Mobile can't be null
        /// </summary>
        [TestMethod]
        public async Task SalesPersonMobileCanNotBeNull()
        {
            //Arrange
            SalesPersonBL salesPersonBL = new SalesPersonBL();
            SalesPerson salesPerson = new SalesPerson() { SalesPersonName = "Smith", SalesPersonMobile = null, Password = "Smith123#", Email = "smith@gmail.com" };
            bool isAdded = false;
            string errorMessage = null;
            Guid newGuid;

            //Act
            try
            {
                (isAdded, newGuid) = await salesPersonBL.AddSalesPersonBL(salesPerson);
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
        /// SalesPerson Password can't be null
        /// </summary>
        [TestMethod]
        public async Task SalesPersonPasswordCanNotBeNull()
        {
            //Arrange
            SalesPersonBL salesPersonBL = new SalesPersonBL();
            SalesPerson salesPerson = new SalesPerson() { SalesPersonName = "Allen", SalesPersonMobile = "9877766554", Password = null, Email = "allen@gmail.com" };
            bool isAdded = false;
            string errorMessage = null;
            Guid newGuid;

            //Act
            try
            {
                (isAdded,newGuid) = await salesPersonBL.AddSalesPersonBL(salesPerson);
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
        /// SalesPerson Email can't be null
        /// </summary>
        [TestMethod]
        public async Task SalesPersonEmailCanNotBeNull()
        {
            //Arrange
            SalesPersonBL salesPersonBL = new SalesPersonBL();
            SalesPerson salesPerson = new SalesPerson() { SalesPersonName = "John", SalesPersonMobile = "9876543210", Password = "John123#", Email = null };
            bool isAdded = false;
            string errorMessage = null;
            Guid newGuid;

            //Act
            try
            {
                (isAdded,newGuid) = await salesPersonBL.AddSalesPersonBL(salesPerson);
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
        /// SalesPersonName should contain at least two characters
        /// </summary>
        [TestMethod]
        public async Task SalesPersonNameShouldContainAtLeastTwoCharacters()
        {
            //Arrange
            SalesPersonBL salesPersonBL = new SalesPersonBL();
            SalesPerson salesPerson = new SalesPerson() { SalesPersonName = "J", SalesPersonMobile = "9877897890", Password = "John123#", Email = "john@gmail.com" };
            bool isAdded = false;
            string errorMessage = null;
            Guid newGuid;

            //Act
            try
            {
                (isAdded, newGuid) = await salesPersonBL.AddSalesPersonBL(salesPerson);
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
        /// SalesPersonMobile should be a valid mobile number
        /// </summary>
        [TestMethod]
        public async Task SalesPersonMobileRegExp()
        {
            //Arrange
            SalesPersonBL salesPersonBL = new SalesPersonBL();
            SalesPerson salesPerson = new SalesPerson() { SalesPersonName = "John", SalesPersonMobile = "9877", Password = "John123#", Email = "john@gmail.com" };
            bool isAdded = false;
            string errorMessage = null;
            Guid newGuid;

            //Act
            try
            {
                (isAdded , newGuid)= await salesPersonBL.AddSalesPersonBL(salesPerson);
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
        /// Password should be a valid password as per regular expression
        /// </summary>
        [TestMethod]
        public async Task SalesPersonPasswordRegExp()
        {
            //Arrange
            SalesPersonBL salesPersonBL = new SalesPersonBL();
            SalesPerson salesPerson = new SalesPerson() { SalesPersonName = "John", SalesPersonMobile = "9877897890", Password = "John", Email = "john@gmail.com" };
            bool isAdded = false;
            string errorMessage = null;
            Guid newGuid;

            //Act
            try
            {
               ( isAdded, newGuid) = await salesPersonBL.AddSalesPersonBL(salesPerson);
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
        /// Email should be a valid email as per regular expression
        /// </summary>
        [TestMethod]
        public async Task SalesPersonEmailRegExp()
        {
            //Arrange
            SalesPersonBL salesPersonBL = new SalesPersonBL();
            SalesPerson salesPerson = new SalesPerson() { SalesPersonName = "John", SalesPersonMobile = "9877897890", Password = "John123#", Email = "john" };
            bool isAdded = false;
            string errorMessage = null;
            Guid newGuid;

            //Act
            try
            {
               (isAdded,newGuid) = await salesPersonBL.AddSalesPersonBL(salesPerson);
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


    [TestClass]
    public class UpdateSalesPersonBLTest
    {
        /// <summary>
        /// Update SalesPerson to the Collection if it is valid.
        /// </summary>

        [TestMethod]
        public async Task UpdateValidSalesPerson()
        {
            // Arrange
            SalesPersonBL salesPersonBL = new SalesPersonBL();
            SalesPerson salesPerson1 = new SalesPerson() { SalesPersonName = "Scott", SalesPersonMobile = "9876543210", Password = "Scott123#", Email = "scott@gmail.com" };
            await salesPersonBL.AddSalesPersonBL(salesPerson1);
            SalesPerson salesPerson2 = new SalesPerson() { SalesPersonID = salesPerson1.SalesPersonID, SalesPersonName = "Ayush", SalesPersonMobile = "9098850371", Password = "Ayush123#", Email = "ayush@gmail.com" };

            bool isUpdated = false;
            string errorMessage = null;

            //Act
            try
            {
                isUpdated = await salesPersonBL.UpdateSalesPersonBL(salesPerson2);
            }
            catch (Exception ex)
            {
                isUpdated = false;
                errorMessage = ex.Message;
            }
            finally
            {
                //Assert
                Assert.IsTrue(isUpdated, errorMessage);
            }
        }

        /// <summary>
        /// SalesPerson Name can't be null
        /// </summary>
        [TestMethod]
        public async Task SalesPersonNameCanNotBeNull()
        {
            //Arrange
            SalesPersonBL salesPersonBL = new SalesPersonBL();
            SalesPerson salesPerson1 = new SalesPerson() { SalesPersonName = "Smith", SalesPersonMobile = "9988776655", Password = "Smith123#", Email = "smith@gmail.com" };
            await salesPersonBL.AddSalesPersonBL(salesPerson1);
            SalesPerson salesPerson2 = new SalesPerson() { SalesPersonID = salesPerson1.SalesPersonID, SalesPersonName = null, SalesPersonMobile = "9098850371", Password = "Ayush123#", Email = "ayush@gmail.com" };
            bool isUpdated = false;
            string errorMessage = null;

            //Act
            try
            {
                isUpdated = await salesPersonBL.UpdateSalesPersonBL(salesPerson2);
            }
            catch (Exception ex)
            {
                isUpdated = false;
                errorMessage = ex.Message;
            }
            finally
            {
                //Assert
                Assert.IsFalse(isUpdated, errorMessage);
            }
        }

        /// <summary>
        /// SalesPerson Mobile can't be null
        /// </summary>
        [TestMethod]
        public async Task SalesPersonMobileCanNotBeNull()
        {
            //Arrange
            SalesPersonBL salesPersonBL = new SalesPersonBL();
            SalesPerson salesPerson1 = new SalesPerson() { SalesPersonName = "Smith", SalesPersonMobile = "9098850371", Password = "Smith123#", Email = "shyam@gmail.com" };
            await salesPersonBL.AddSalesPersonBL(salesPerson1);
            SalesPerson salesPerson2 = new SalesPerson() { SalesPersonID = salesPerson1.SalesPersonID, SalesPersonName = "Ayush", SalesPersonMobile = null, Password = "Ayush123#", Email = "ayush@gmail.com" };


            bool isUpdated = false;
            string errorMessage = null;

            //Act
            try
            {
                isUpdated = await salesPersonBL.UpdateSalesPersonBL(salesPerson2);
            }
            catch (Exception ex)
            {
                isUpdated = false;
                errorMessage = ex.Message;
            }
            finally
            {
                //Assert
                Assert.IsFalse(isUpdated, errorMessage);
            }
        }

        /// <summary>
        /// SalesPerson Password can't be null
        /// </summary>
        [TestMethod]
        public async Task SalesPersonPasswordCanNotBeNull()
        {
            //Arrange

            SalesPersonBL salesPersonBL = new SalesPersonBL();
            SalesPerson salesPerson1 = new SalesPerson() { SalesPersonName = "Allen", SalesPersonMobile = "9877766554", Password = "Ayush123@", Email = "allen@gmail.com" };
            await salesPersonBL.AddSalesPersonBL(salesPerson1);
            SalesPerson salesPerson2 = new SalesPerson() { SalesPersonID = salesPerson1.SalesPersonID, SalesPersonName = "Ayush", SalesPersonMobile = "9098850371", Password = null, Email = "ayush@gmail.com" };

            bool isUpdated = false;
            string errorMessage = null;

            //Act
            try
            {
                isUpdated = await salesPersonBL.UpdateSalesPersonBL(salesPerson2);
            }
            catch (Exception ex)
            {
                isUpdated = false;
                errorMessage = ex.Message;
            }
            finally
            {
                //Assert
                Assert.IsFalse(isUpdated, errorMessage);
            }
        }

        /// <summary>
        /// SalesPerson Email can't be null
        /// </summary>

        [TestMethod]
        public async Task SalesPersonEmailCanNotBeNull()
        {
            //Arrange
            SalesPersonBL salesPersonBL = new SalesPersonBL();
            SalesPerson salesPerson1 = new SalesPerson() { SalesPersonName = "John", SalesPersonMobile = "9876543210", Password = "John123#", Email = "ayush087@gmail.com" };
            await salesPersonBL.AddSalesPersonBL(salesPerson1);
            SalesPerson salesPerson2 = new SalesPerson() { SalesPersonID = salesPerson1.SalesPersonID, SalesPersonName = "Ayush", SalesPersonMobile = "9098850371", Password = "Ayush123#", Email = null };

            bool isUpdated = false;
            string errorMessage = null;

            //Act
            try
            {
                isUpdated = await salesPersonBL.UpdateSalesPersonBL(salesPerson2);
            }
            catch (Exception ex)
            {
                isUpdated = false;
                errorMessage = ex.Message;
            }
            finally
            {
                //Assert
                Assert.IsFalse(isUpdated, errorMessage);
            }
        }

        /// <summary>
        /// SalesPersonName should contain at least two characters
        /// </summary>
        [TestMethod]
        public async Task SalesPersonNameShouldContainAtLeastTwoCharacters()
        {
            //Arrange
            SalesPersonBL salesPersonBL = new SalesPersonBL();
            SalesPerson salesPerson1 = new SalesPerson() { SalesPersonName = "John", SalesPersonMobile = "9877897890", Password = "John123#", Email = "john23@gmail.com" };
            await salesPersonBL.AddSalesPersonBL(salesPerson1);
            SalesPerson salesPerson2 = new SalesPerson() { SalesPersonID = salesPerson1.SalesPersonID, SalesPersonName = "J", SalesPersonMobile = "9877897890", Password = "John123#", Email = "john@gmail.com" };

            bool isUpdated = false;
            string errorMessage = null;

            //Act
            try
            {
                isUpdated = await salesPersonBL.UpdateSalesPersonBL(salesPerson2);
            }
            catch (Exception ex)
            {
                isUpdated = false;
                errorMessage = ex.Message;
            }
            finally
            {
                //Assert
                Assert.IsFalse(isUpdated, errorMessage);
            }
        }

        /// <summary>
        /// SalesPersonMobile should be a valid mobile number
        /// </summary>
        [TestMethod]
        public async Task SalesPersonMobileRegExp()
        {
            //Arrange
            SalesPersonBL salesPersonBL = new SalesPersonBL();
            SalesPerson salesPerson1 = new SalesPerson() { SalesPersonName = "John", SalesPersonMobile = "9893103654", Password = "John123#", Email = "john@gmail.com" };
            await salesPersonBL.AddSalesPersonBL(salesPerson1);
            SalesPerson salesPerson2 = new SalesPerson() { SalesPersonID=salesPerson1.SalesPersonID,SalesPersonName = "John", SalesPersonMobile = "9877", Password = "John123#", Email = "john@gmail.com" };

            bool isUpdated = false;
            string errorMessage = null;

            //Act
            try
            {
                isUpdated = await salesPersonBL.UpdateSalesPersonBL(salesPerson2);
            }
            catch (Exception ex)
            {
                isUpdated = false;
                errorMessage = ex.Message;
            }
            finally
            {
                //Assert
                Assert.IsFalse(isUpdated, errorMessage);
            }
        }

        /// <summary>
        /// Password should be a valid password as per regular expression
        /// </summary>
        [TestMethod]
        public async Task SalesPersonPasswordRegExp()
        {
            //Arrange
            SalesPersonBL salesPersonBL = new SalesPersonBL();
            SalesPerson salesPerson1 = new SalesPerson() { SalesPersonName = "John", SalesPersonMobile = "9877897890", Password = "John123#", Email = "Pushpraj@gmail.com" };
            await salesPersonBL.AddSalesPersonBL(salesPerson1);
            SalesPerson salesPerson2 = new SalesPerson() { SalesPersonID = salesPerson1.SalesPersonID, SalesPersonName = "John", SalesPersonMobile = "9877897890", Password = "a", Email = "john@gmail.com" };

            bool isUpdated = false;
            string errorMessage = null;

            //Act
            try
            {
                isUpdated = await salesPersonBL.UpdateSalesPersonBL(salesPerson2);
            }
            catch (Exception ex)
            {
                isUpdated = false;
                errorMessage = ex.Message;
            }
            finally
            {
                //Assert
                Assert.IsFalse(isUpdated, errorMessage);
            }
        }

        /// <summary>
        /// Email should be a valid email as per regular expression
        /// </summary>
        [TestMethod]
        public async Task SalesPersonEmailRegExp()
        {
            //Arrange
            SalesPersonBL salesPersonBL = new SalesPersonBL();
            SalesPerson salesPerson1 = new SalesPerson() { SalesPersonName = "John", SalesPersonMobile = "9877897890", Password = "John123#", Email = "Kaushik@gmail.com" };
            await salesPersonBL.AddSalesPersonBL(salesPerson1);
            SalesPerson salesPerson2 = new SalesPerson(){SalesPersonID = salesPerson1.SalesPersonID, SalesPersonName = "John", SalesPersonMobile = "9877897890", Password = "John123#", Email = "kaushik" };

            bool isUpdated = false;
            string errorMessage = null;

            //Act
            try
            {
                isUpdated = await salesPersonBL.UpdateSalesPersonBL(salesPerson2);
            }
            catch (Exception ex)
            {
                isUpdated = false;
                errorMessage = ex.Message;
            }
            finally
            {
                //Assert
                Assert.IsFalse(isUpdated, errorMessage);
            }
        }

        /// <summary>
        /// SalesPersonID Does Not Exist
        /// </summary>
        [TestMethod]
        public async Task SalesPersonIDDoesNotExist()
        {
            //Arrange
            SalesPersonBL salesPersonBL = new SalesPersonBL();
            SalesPerson salesPerson1 = new SalesPerson() { SalesPersonName = "John", SalesPersonMobile = "9877897890", Password = "John123#", Email = "Ram@gmail.com" };
            await salesPersonBL.AddSalesPersonBL(salesPerson1);
            SalesPerson salesPerson2 = new SalesPerson() { SalesPersonID = default(Guid), SalesPersonName = "John", SalesPersonMobile = "9877897890", Password = "John123#", Email = "john@gmail.com" };

            bool isUpdated = false;
            string errorMessage = null;

            //Act
            try
            {
                isUpdated = await salesPersonBL.UpdateSalesPersonBL(salesPerson2);
            }
            catch (Exception ex)
            {
                isUpdated = false;
                errorMessage = ex.Message;
            }
            finally
            {
                //Assert
                Assert.IsFalse(isUpdated, errorMessage);
            }
        }

    }


    [TestClass]
    public class DeleteSalesPersonBLTest
    {

        /// <summary>
        /// delete SalesPerson to the Collection if it is valid.
        /// </summary>
        [TestMethod]
        public async Task DeleteValidSalesPersonBL()
        {
            //Arrange
            SalesPersonBL salesPersonBL = new SalesPersonBL();
            SalesPerson salesPerson1 = new SalesPerson() { SalesPersonName = "John", SalesPersonMobile = "9877897890", Password = "John123#", Email = "joh@gmail.com" };
            await salesPersonBL.AddSalesPersonBL(salesPerson1);
            bool isDeleted = false;
            string errorMessage = null;

            //Act
            try
            {
                isDeleted = await salesPersonBL.DeleteSalesPersonBL(salesPerson1.SalesPersonID);
            }
            catch (Exception ex)
            {
                isDeleted = false;
                errorMessage = ex.Message;
            }
            finally
            {
                //Assert
                Assert.IsTrue(isDeleted, errorMessage);
            }
        }

        /// <summary>
        /// SalesPersonID Does Not Exist.
        /// </summary>
        [TestMethod]
        public async Task SalesPersonIDDoesNotExist()
        {
            //Arrange
            SalesPersonBL salesPersonBL = new SalesPersonBL();
            SalesPerson salesPerson1 = new SalesPerson() { SalesPersonName = "John", SalesPersonMobile = "9877897890", Password = "John123#", Email = "jn@gmail.com" }; await salesPersonBL.AddSalesPersonBL(salesPerson1);
            bool isDeleted = false;
            string errorMessage = null;

            //Act
            try
            {
                isDeleted = await salesPersonBL.DeleteSalesPersonBL(default(Guid));
            }
            catch (Exception ex)
            {
                isDeleted = false;
                errorMessage = ex.Message;
            }
            finally
            {
                //Assert
                Assert.IsFalse(isDeleted, errorMessage);
            }
        }
    }

    [TestClass]
    public class GetSalesPersonBLTest
    {

        /// <summary>
        /// Get all SalesPersonBL to the Collection if it is isvalid.
        /// </summary>


        [TestMethod]
        public async Task GetAllValidSalesPersons()
        {
            //Arrange
            SalesPersonBL salesPersonBL = new SalesPersonBL();
            SalesPerson salesPerson = new SalesPerson() { SalesPersonName = "John", SalesPersonMobile = "9877897890", Password = "John123#", Email = "Utkarsh@gmail.com" };
            await salesPersonBL.AddSalesPersonBL(salesPerson);
            bool isDisplayed = false;
            string errorMessage = null;

            //Act
            try
            {
                List<SalesPerson> salesPersonList = await salesPersonBL.GetAllSalesPersonsBL();
                if (salesPersonList.Count > 0)
                {
                    isDisplayed = true;
                }
            }
            catch (Exception ex)
            {
                isDisplayed = false;
                errorMessage = ex.Message;
            }
            finally
            {
                //Assert
                Assert.IsTrue(isDisplayed, errorMessage);
            }
        }

        /// <summary>
        /// SalesPerson List Cannot be null
        /// </summary>


        [TestMethod]
        public async Task SalesPersonListCanNotBeEmpty()
        {
            //Arrange

            bool isDisplayed = false;
            string errorMessage = null;

            //Act
            try
            {
                SalesPersonBL salesPersonBL = new SalesPersonBL();
                List<SalesPerson> salesPersonList = await salesPersonBL.GetAllSalesPersonsBL();
                if (salesPersonList.Count < 1)
                {
                    isDisplayed = true;
                }
            }
            catch (Exception ex)
            {
                isDisplayed = false;
                errorMessage = ex.Message;
            }
            finally
            {
                //Assert
                Assert.IsFalse(isDisplayed, errorMessage);
            }
        }


        /// <summary>
        /// Get SalesPerson in the List if Id is Valid
        /// </summary>



        [TestMethod]
        public async Task GetvalidSalesPersonBySalesPersonIDBL()
        {
            //Arrange
            SalesPersonBL salesPersonBL = new SalesPersonBL();
            SalesPerson salesPerson = new SalesPerson() { SalesPersonName = "John", SalesPersonMobile = "9877897890", Password = "John123#", Email = "johncena@gmail.com" };
            await salesPersonBL.AddSalesPersonBL(salesPerson);
            bool isDisplayed = false;
            string errorMessage = null;

            //Act
            try
            {
                SalesPerson salesPerson1 = await salesPersonBL.GetSalesPersonBySalesPersonIDBL(salesPerson.SalesPersonID);
                if (salesPerson.SalesPersonID == salesPerson1.SalesPersonID)
                {
                    isDisplayed = true;
                }
            }
            catch (Exception ex)
            {
                isDisplayed = false;
                errorMessage = ex.Message;
            }
            finally
            {
                //Assert
                Assert.IsTrue(isDisplayed, errorMessage);
            }
        }

        /// <summary>
        /// SalesPersonID should Exist
        /// </summary>
        [TestMethod]
        public async Task SalesPersonIDDoesNotExist()
        {
            //Arrange
            SalesPersonBL salesPersonBL = new SalesPersonBL();
            SalesPerson salesPerson = new SalesPerson() { SalesPersonName = "John", SalesPersonMobile = "9877897890", Password = "John123#", Email = "Shivam@gmail.com" };
            await salesPersonBL.AddSalesPersonBL(salesPerson);
            bool isDisplayed = false;
            string errorMessage = null;

            //Act
            try
            {
                SalesPerson salesPerson1 = await salesPersonBL.GetSalesPersonBySalesPersonIDBL(default(Guid));
            }
            catch (Exception ex)
            {
                isDisplayed = false;
                errorMessage = ex.Message;
            }
            finally
            {
                //Assert
                Assert.IsFalse(isDisplayed, errorMessage);
            }
        }

        /// <summary>
        /// GetSalesPersonByEmailBL to the Collection if it is isvalid.
        /// </summary>
        [TestMethod]
        public async Task GetSalesPersonByEmailBL()
        {
            //Arrange
            SalesPersonBL salesPersonBL = new SalesPersonBL();
            SalesPerson salesPerson = new SalesPerson() { SalesPersonName = "John", SalesPersonMobile = "9877897890", Password = "John123#", Email = "utkarsh@gmail.com" };
            await salesPersonBL.AddSalesPersonBL(salesPerson);
            bool isDisplayed = false;
            string errorMessage = null;

            //Act
            try
            {
                SalesPerson salesPerson1 = await salesPersonBL.GetSalesPersonByEmailBL(salesPerson.Email);
                if (salesPerson.Email == salesPerson1.Email)
                {
                    isDisplayed = true;
                }
            }
            catch (Exception ex)
            {
                isDisplayed = false;
                errorMessage = ex.Message;
            }
            finally
            {
                //Assert
                Assert.IsTrue(isDisplayed, errorMessage);
            }
        }

        /// <summary>
        /// SalesPerson Email Should Exist
        /// </summary>
        [TestMethod]
        public async Task SalesPersonEmailShouldExist()
        {
            //Arrange
            SalesPersonBL salesPersonBL = new SalesPersonBL();
            SalesPerson salesPerson = new SalesPerson() { SalesPersonName = "John", SalesPersonMobile = "9877897890", Password = "John123#", Email = "Daddu@gmail.com" };
            await salesPersonBL.AddSalesPersonBL(salesPerson);
            bool isDisplayed = false;
            string errorMessage = null;

            //Act
            try
            {
                SalesPerson salesPerson1 = await salesPersonBL.GetSalesPersonByEmailBL("email");
            }
            catch (Exception ex)
            {
                isDisplayed = false;
                errorMessage = ex.Message;
            }
            finally
            {
                //Assert
                Assert.IsFalse(isDisplayed, errorMessage);
            }
        }


        /// <summary>
        /// GetSalesPersonsByNameBL to the Collection if it is isvalid.
        /// </summary>
        [TestMethod]
        public async Task GetSalesPersonsByNameBL()
        {
            //Arrange
            SalesPersonBL salesPersonBL = new SalesPersonBL();
            SalesPerson salesPerson = new SalesPerson() { SalesPersonName = "JohnCena", SalesPersonMobile = "9877897890", Password = "John123#", Email = "Kaushik@gmail.com" };
            await salesPersonBL.AddSalesPersonBL(salesPerson);
            bool isDisplayed = false;
            string errorMessage = null;

            //Act
            try
            {
                List<SalesPerson> salesPersonList1 = await salesPersonBL.GetSalesPersonsByNameBL(salesPerson.SalesPersonName);
                if (salesPersonList1.Count > 0)
                {
                    isDisplayed = true;
                }
            }
            catch (Exception ex)
            {
                isDisplayed = false;
                errorMessage = ex.Message;
            }
            finally
            {
                //Assert
                Assert.IsTrue(isDisplayed, errorMessage);
            }
        }

        /// <summary>
        /// SalesPersonName must Exist.
        /// </summary>
        [TestMethod]
        public async Task SalesPersonsNameDoesNotExist()
        {
            //Arrange
            SalesPersonBL salesPersonBL = new SalesPersonBL();
            SalesPerson salesPerson = new SalesPerson() { SalesPersonName = "John", SalesPersonMobile = "9877897890", Password = "John123#", Email = "john121@gmail.com" };
            await salesPersonBL.AddSalesPersonBL(salesPerson);
            bool isDisplayed = false;
            string errorMessage = null;

            //Act
            try
            {
                List<SalesPerson> salesPerson1 = await salesPersonBL.GetSalesPersonsByNameBL("SalesPersonName");
            }
            catch (Exception ex)
            {
                isDisplayed = false;
                errorMessage = ex.Message;
            }
            finally
            {
                //Assert
                Assert.IsFalse(isDisplayed, errorMessage);
            }
        }

        /// <summary>
        ///  GetSalesPersonByEmailAndPasswordBL to the Collection if it is isvalid.
        /// </summary>
        [TestMethod]
        public async Task GetSalesPersonByEmailAndPasswordBL()
        {
            //Arrange
            SalesPersonBL salesPersonBL = new SalesPersonBL();
            SalesPerson salesPerson = new SalesPerson() { SalesPersonName = "John", SalesPersonMobile = "9877897890", Password = "John123#", Email = "Ayush@gmail.com" };
            await salesPersonBL.AddSalesPersonBL(salesPerson);
            bool isDisplayed = false;
            string errorMessage = null;

            //Act
            try
            {
                SalesPerson salesPerson1 = await salesPersonBL.GetSalesPersonByEmailAndPasswordBL(salesPerson.Email, salesPerson.Password);
                if ((salesPerson.SalesPersonName == salesPerson1.SalesPersonName) && (salesPerson.Password == salesPerson1.Password))
                {
                    isDisplayed = true;
                }
            }
            catch (Exception ex)
            {
                isDisplayed = false;
                errorMessage = ex.Message;
            }
            finally
            {
                //Assert
                Assert.IsTrue(isDisplayed, errorMessage);
            }
        }


        /// <summary>
        ///  GetSalesPersonByEmailAndPasswordBL to the Collection if it is invalid.
        /// </summary>
        [TestMethod]
        public async Task InvalidEmailAndPasswordBL3()
        {
            //Arrange
            SalesPersonBL salesPersonBL = new SalesPersonBL();
            SalesPerson salesPerson = new SalesPerson() { SalesPersonName = "John", SalesPersonMobile = "9877897890", Password = "John123#", Email = "Sourabh@gmail.com" };
            await salesPersonBL.AddSalesPersonBL(salesPerson);
            bool isDisplayed = false;
            string errorMessage = null;

            //Act
            try
            {
                SalesPerson salesPerson1 = await salesPersonBL.GetSalesPersonByEmailAndPasswordBL("Email", "Password");
            }
            catch (Exception ex)
            {
                isDisplayed = false;
                errorMessage = ex.Message;
            }
            finally
            {
                //Assert
                Assert.IsFalse(isDisplayed, errorMessage);
            }
        }



    }






}
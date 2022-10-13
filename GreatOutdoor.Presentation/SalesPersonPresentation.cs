using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Console;
using Capgemini.GreatOutdoor.BusinessLayer;
using Capgemini.GreatOutdoor.Entities;
using Capgemini.GreatOutdoor.Exceptions;
using Capgemini.GreatOutdoor.Contracts.BLContracts;

namespace Capgemini.GreatOutdoor.PresentationLayer
{
    public static class SalesPersonPresentation
    {
        /// <summary>
        /// Menu for Sales Person Menu
        /// </summary>
        /// <returns></returns>
        public static async Task<int> SalesPersonMenu()
        {
            int choice = -2;

            do
            {
                //Menu
                WriteLine("\n***************Sales Person MENU***********");
                WriteLine("---" + ((SalesPerson)CommonData.CurrentUser).SalesPersonName + "---");
                WriteLine("1. View Your Profile");
                WriteLine("2. Update Your Profile");
                WriteLine("3. Upload Offline Order");
                WriteLine("4. Change Password");
                WriteLine("5. Logout");
                WriteLine("6. Exit");
                Write("Choice: ");

                //Accept and check choice
                bool isValidChoice = int.TryParse(ReadLine(), out choice);
                if (isValidChoice)
                {
                    switch (choice)
                    {
                        case 1: await ViewCurrentSalesPerson(); break;

                        case 2: await UpdateSalesPerson(); break;

                        case 3: await UploadOfflineOrder(); break;

                        case 4: await ChangeSalesPersonPassword(); break;

                        case 5: break;

                        case 6: break;

                        default: WriteLine("Invalid Choice"); break;
                    }
                }
                else
                {
                    choice = -2;
                }
            } while (choice != 5 && choice != 6);
            return choice;
        }




        /// <summary>
        /// Displays list of SalesPerson.
        /// </summary>
        /// <returns></returns>
        public static async Task ViewCurrentSalesPerson()
        {
            try
            {
                using (ISalesPersonBL salesPersonBL = new SalesPersonBL())
                {
                    //Get and display list of system users.
                    SalesPerson salesPerson = await salesPersonBL.GetSalesPersonBySalesPersonIDBL(((SalesPerson)CommonData.CurrentUser).SalesPersonID);
                    WriteLine("Your Profile:");
                    if (salesPerson != null)
                    {
                        WriteLine("Your ID " + "-" + salesPerson.SalesPersonID);
                        WriteLine("Name " + "-" + salesPerson.SalesPersonName);
                        WriteLine("Mobile " + "-" + salesPerson.SalesPersonMobile);
                        WriteLine("Email " + "-" + salesPerson.Email);
                        WriteLine("Creation DateTime " + "=" + salesPerson.CreationDateTime);
                        WriteLine("Last Modified DateTime " + "-" + salesPerson.LastModifiedDateTime);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex);
                WriteLine(ex.Message);
            }
        }


        /// <summary>
        /// Updates SalesPerson.
        /// </summary>
        /// <returns></returns>
        public static async Task UpdateSalesPerson()
        {
            try
            {
                using (ISalesPersonBL salesPersonBL = new SalesPersonBL())
                {
                    //update SalesPerson Details
                    SalesPerson salesPerson = await salesPersonBL.GetSalesPersonBySalesPersonIDBL(((SalesPerson)CommonData.CurrentUser).SalesPersonID);
                    if (salesPerson != null)
                    {
                        //Read inputs

                        Write("Name: ");
                        salesPerson.SalesPersonName = ReadLine();
                        Write("Mobile: ");
                        salesPerson.SalesPersonMobile = ReadLine();
                        Write("Email: ");
                        salesPerson.Email = ReadLine();

                        //Invoke UpdateSalesPersonBL method to update
                        bool isUpdated = await salesPersonBL.UpdateSalesPersonBL(salesPerson);
                        if (isUpdated)
                        {
                            WriteLine("SalesPerson Updated");
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex);
                WriteLine(ex.Message);
            }
        }

        public static async Task AddSalesPerson()
        {
            try
            {
                //Read inputs
                SalesPerson salesPerson = new SalesPerson();
                Write("Name: ");
                salesPerson.SalesPersonName = ReadLine();
                Write("Mobile: ");
                salesPerson.SalesPersonMobile = ReadLine();
                Write("Email: ");
                salesPerson.Email = ReadLine();

                Write("Password: ");
                salesPerson.Password = ReadLine();

                //Invoke AddSalesPersonBL method to add
                using (ISalesPersonBL salesPersonBL = new SalesPersonBL())
                {
                    bool isAdded;
                    Guid salesPersonGuid;
                     (isAdded,salesPersonGuid) = await salesPersonBL.AddSalesPersonBL(salesPerson);
                    if (isAdded)
                    {
                        WriteLine("Salesperson Added");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex);
                WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Delete SalesPerson.
        /// </summary>
        /// <returns></returns>
        public static async Task DeleteSalesPerson()
        {
            try
            {
                using (ISalesPersonBL salesPersonBL = new SalesPersonBL())
                {
                    //Read Sl.No
                    Write("SalesPerson #: ");
                    bool isNumberValid = int.TryParse(ReadLine(), out int serial);
                    if (isNumberValid)
                    {
                        serial--;
                        List<SalesPerson> salesPersons = await salesPersonBL.GetAllSalesPersonsBL();
                        if (serial <= salesPersons.Count - 1)
                        {
                            //Confirmation
                            SalesPerson salesPerson = salesPersons[serial];
                            Write("Are you sure? (Y/N): ");
                            string confirmation = ReadLine();

                            if (confirmation.Equals("Y", StringComparison.OrdinalIgnoreCase))
                            {
                                //Invoke DeleteSalesPersonBL method to delete
                                bool isDeleted = await salesPersonBL.DeleteSalesPersonBL(salesPerson.SalesPersonID);
                                if (isDeleted)
                                {
                                    WriteLine("SalesPerson Deleted");
                                }
                            }
                        }
                        else
                        {
                            WriteLine($"Invalid SalesPerson #.\nPlease enter a number between 1 to {salesPersons.Count}");
                        }
                    }
                    else
                    {
                        WriteLine($"Invalid number.");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex);
                WriteLine(ex.Message);
            }
        }




        /// <summary>
        /// Updates SalesPerson's Password.
        /// </summary>
        /// <returns></returns>
        public static async Task ChangeSalesPersonPassword()
        {
            try
            {
                using (ISalesPersonBL salesPersonBL = new SalesPersonBL())
                {
                    //Read Current Password
                    Write("Current Password: ");
                    string currentPassword = ReadLine();

                    SalesPerson existingSalesPerson = await salesPersonBL.GetSalesPersonByEmailAndPasswordBL(CommonData.CurrentUser.Email, currentPassword);

                    if (existingSalesPerson != null)
                    {
                        //Read inputs
                        Write("New Password: ");
                        string newPassword = ReadLine();
                        Write("Confirm Password: ");
                        string confirmPassword = ReadLine();

                        if (newPassword.Equals(confirmPassword))
                        {
                            existingSalesPerson.Password = newPassword;

                            //Invoke UpdateSalesPersonBL method to update
                            bool isUpdated = await salesPersonBL.UpdateSalesPersonPasswordBL(existingSalesPerson);
                            if (isUpdated)
                            {
                                WriteLine("SalesPerson Password Updated");
                            }
                        }
                        else
                        {
                            WriteLine($"New Password and Confirm Password doesn't match");
                        }
                    }
                    else
                    {
                        WriteLine($"Current Password doesn't match.");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex);
                WriteLine(ex.Message);
            }
        }
        public static async Task UploadOfflineOrder()
        {
            try
            {
                //Read inputs
                OfflineOrder offlineOrder = new OfflineOrder();
                OfflineOrderBL offlineOrderBL1 = new OfflineOrderBL();
                offlineOrder.TotalOrderAmount = 10;
                offlineOrder.TotalQuantity = 10;

                using (IRetailerBL retailerBL = new RetailerBL())
                {
                    int serial = 0;


                    //Get and display list of Retailer.
                    List<Retailer> retailers = await retailerBL.GetAllRetailersBL();
                    WriteLine("Retailers:");
                    if (retailers != null && retailers?.Count > 0)
                    {
                        WriteLine("#\tName\tEmail\tRetailerID\t");

                        foreach (var retailer in retailers)
                        {
                            serial++;
                            WriteLine($"{serial}\t{retailer.RetailerName}\t{retailer.Email}\t{retailer.RetailerID}");

                        }
                    }

                    Write("Select Retailer #: ");
                    bool isNumberValid = int.TryParse(ReadLine(), out serial);
                    if (isNumberValid)
                    {
                        serial--;
                        if (serial <= retailers.Count - 1)
                        {

                            Retailer retailer1 = retailers[serial];

                            offlineOrder.RetailerID = retailer1.RetailerID;
                        }
                        else
                        {
                            WriteLine("INVALID ENTRY");
                        }

                    }
                    SalesPersonBL salespersonBL = new SalesPersonBL();
                    SalesPerson salesPerson = await salespersonBL.GetSalesPersonByEmailBL(CommonData.CurrentUser.Email);
                    offlineOrder.SalesPersonID = salesPerson.SalesPersonID;



                }


                using (IProductBL productBL = new ProductBL())
                {
                    int serial1 = 0;

                    //Get and display list of Product.
                    List<Product> products = await productBL.GetAllProductsBL();
                    WriteLine("Select Products from following List");
                    WriteLine("Products:");
                    if (products != null && products?.Count > 0)
                    {
                        WriteLine("#\tProductName\tPrice\t\tDescription\t");

                        foreach (var product in products)
                        {
                            serial1++;
                            WriteLine($"{serial1}\t{product.ProductName}\t{product.ProductPrice}\t{product.ProductColor}\t{product.ProductSize}\t{product.ProductMaterial}");

                        }
                    }
                    //Add Product Details
                    char c;
                    WriteLine("Add product(Y/N):");
                    c = Char.Parse(ReadLine());

                    while ((c == 'Y') || (c == 'y'))
                    {

                        OfflineOrderDetail offlineOrderDetail = new OfflineOrderDetail();
                        offlineOrderDetail.OfflineOrderID = offlineOrder.OfflineOrderID;

                        Write("Select Product #: ");
                        bool isNumberValid1 = int.TryParse(ReadLine(), out serial1);
                        if (isNumberValid1)
                        {
                            serial1--;
                            if (serial1 <= products.Count - 1)
                            {

                                Product product1 = products[serial1];

                                offlineOrderDetail.ProductID = product1.ProductID;
                                offlineOrderDetail.UnitPrice = product1.ProductPrice;
                                offlineOrderDetail.ProductName = product1.ProductName;
                                WriteLine("Enter Quantity:");

                                int quantity = Int32.Parse(ReadLine());
                                offlineOrderDetail.Quantity = quantity;
                                offlineOrderDetail.TotalPrice = offlineOrderDetail.UnitPrice * quantity;
                                using (IOfflineOrderDetailBL offlineOrderDetailBL = new OfflineOrderDetailBL())
                                {
                                    bool isAdded = await offlineOrderDetailBL.AddOfflineOrderDetailBL(offlineOrderDetail);
                                    if (isAdded)
                                    {
                                        WriteLine("Offline Order Detail Added");
                                    }
                                }

                            }
                            else { WriteLine("Invalid Serial Number"); }

                        }
                        else { WriteLine("Invalid choice"); }

                        WriteLine("Add product(Y/N)");
                        c = Char.Parse(ReadLine());


                    }
                    if (!(c == 'y') || (c == 'Y') || (c == 'n') || (c == 'N')) { WriteLine("Invalid Choice"); }
                }

                using (IOfflineOrderBL offlineOrderBL = new OfflineOrderBL())
                {
                    bool isAdded;
                    Guid OfflineOrderGuid;

                    (isAdded,OfflineOrderGuid) = await offlineOrderBL.AddOfflineOrderBL(offlineOrder);
                    if (isAdded)
                    {
                        // WriteLine("Offline Order Added");
                    }

                    using (IOfflineOrderDetailBL offlineOrderDetailBL = new OfflineOrderDetailBL())
                    {
                        //Get and display list of offline order details using order ID .
                        List<OfflineOrderDetail> offlineOrderDetails = await offlineOrderDetailBL.GetAllOfflineOrderDetailBL();
                        WriteLine("OfflineOrderDetails:");
                        if (offlineOrderDetails != null && offlineOrderDetails?.Count > 0)
                        {
                            WriteLine("#\tProductName\tUnitPrice\tQuantity\tTotal Price");
                            int serial2 = 0;
                            foreach (var offlineOrderDetail in offlineOrderDetails)
                            {
                                serial2++;
                                WriteLine($"{serial2}\t{offlineOrderDetail.ProductName}\t\t {offlineOrderDetail.UnitPrice}\t\t {offlineOrderDetail.Quantity}\t\t {offlineOrderDetail.TotalPrice}");
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
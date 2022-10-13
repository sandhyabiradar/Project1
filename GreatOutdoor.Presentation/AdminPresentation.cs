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
    public static class AdminPresentation
    {
        /// <summary>
        /// Menu for Admin User
        /// </summary>
        /// <returns></returns>
        public static async Task<int> AdminUserMenu()
        {
            int choice = -2;
            using (IAdminBL AdminBL = new AdminBL())
            {
                do
                {//Menu

                    WriteLine("Welcome Admin " + ((Admin)CommonData.CurrentUser).AdminName + "!");
                    WriteLine("");

                    WriteLine("------------Retailer Related Options------------");
                    WriteLine("");
                    WriteLine("1. View Reatiler Profile");
                    WriteLine("2. View Retailer Reports");

                    WriteLine("");

                    WriteLine("-----------------Sales Related Options-----------------");
                    WriteLine("");
                    WriteLine("3. View Sales Reports");
                    WriteLine("4. View Sales Profile");
                    WriteLine("5. Add Sales Person");
                    WriteLine("6. Update Sales Person");
                    WriteLine("7. Delete Sales Person");
                    WriteLine("");

                    WriteLine("-------------------Edit Personal Profile---------------");
                    WriteLine("");
                    WriteLine("8.Update Admin Profile ");
                    WriteLine("9. Change Password");
                    WriteLine("10. Logout");
                    WriteLine("");
                    WriteLine("..................Product Related Options..........");
                    WriteLine("");
                    WriteLine("11. Add New Product");
                    WriteLine("-1. Exit");
                    WriteLine("");
                    Write("Choice: ");

                    //Accept and check choice
                    bool isValidChoice = int.TryParse(ReadLine(), out choice);
                    if (isValidChoice)
                    {
                        switch (choice)
                        {
                            case 1: await ViewRetailersProfile(); break;
                            case 2: await ViewRetailerReports(); break;
                            case 3: await ViewAllSalesReports(); break;
                            case 4: await ViewSalesPersonsProfile(); break;
                            case 5: await AddSalesPerson(); break;
                            case 6: await UpdateSalesPerson(); break;
                            case 7: await DeleteSalesPerson(); break;

                            case 8: await UpdateAdmin(); break;
                            case 9: await ChangeAdminPassword(); break;
                            case 10: break;
                            case 11: await AddProduct(); break;
                            case -1: break;
                            default: WriteLine("Invalid Choice"); break;
                        }
                    }
                    else
                    {
                        choice = -2;
                    }
                } while (choice != 0 && choice != -1);
            }
            return choice;
        }

        /// <summary>
        /// Add Sales Person
        /// </summary>
        /// <returns></returns>
        public static async Task AddSalesPerson()
        {
            try
            {
                //Read inputs
                SalesPerson salesPerson = new SalesPerson();
                Write("Name: ");
                salesPerson.SalesPersonName = ReadLine();
                Write("Email: ");
                salesPerson.Email = ReadLine();
                Write("Password: ");
                salesPerson.Password = ReadLine();

                //Invoke AddsalesPersonBL method to add
                using (ISalesPersonBL salesPersonBL = new SalesPersonBL())
                {
                    bool isAdded;
                    Guid salesPersonGuid;
                    (isAdded, salesPersonGuid) = await salesPersonBL.AddSalesPersonBL(salesPerson);
                    if (isAdded)
                    {
                        WriteLine("Sales Person Added");
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
        /// Updates Sales Person.
        /// </summary>
        /// <returns></returns>
        public static async Task UpdateSalesPerson()
        {
            try
            {
                using (ISalesPersonBL salesPersonBL = new SalesPersonBL())
                {
                    //Read Sl.No
                    Write("Sales Person #: ");
                    bool isNumberValid = int.TryParse(ReadLine(), out int serial);
                    if (isNumberValid)
                    {
                        serial--;
                        List<SalesPerson> salesPersons = await salesPersonBL.GetAllSalesPersonsBL();
                        if (serial <= salesPersons.Count - 1)
                        {
                            //Read inputs
                            SalesPerson salesPerson = salesPersons[serial];
                            Write("Name: ");
                            salesPerson.SalesPersonName = ReadLine();
                            Write("Email: ");
                            salesPerson.Email = ReadLine();

                            //Invoke UpdateSystemUserBL method to update
                            bool isUpdated = await salesPersonBL.UpdateSalesPersonBL(salesPerson);
                            if (isUpdated)
                            {
                                WriteLine("Sales Person Updated");
                            }
                        }
                        else
                        {
                            WriteLine($"Invalid Sale sPerson #.\nPlease enter a number between 1 to {salesPersons.Count}");
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
        /// Delete Sales person.
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
                                //Invoke DeleteSystemUserBL method to delete
                                bool isDeleted = await salesPersonBL.DeleteSalesPersonBL(salesPerson.SalesPersonID);
                                if (isDeleted)
                                {
                                    WriteLine("Sales Person Deleted");
                                }
                            }
                        }
                        else
                        {
                            WriteLine($"Invalid Sales Person #.\nPlease enter a number between 1 to {salesPersons.Count}");
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
        /// Updates Admin's Password.
        /// </summary>
        /// <returns></returns>
        public static async Task ChangeAdminPassword()
        {
            try
            {
                using (IAdminBL adminBL = new AdminBL())
                {
                    //Read Current Password
                    Write("Current Password: ");
                    string currentPassword = ReadLine();

                    Admin existingAdmin = await adminBL.GetAdminByEmailAndPasswordBL(CommonData.CurrentUser.Email, currentPassword);

                    if (existingAdmin != null)
                    {
                        //Read inputs
                        Write("New Password: ");
                        string newPassword = ReadLine();
                        Write("Confirm Password: ");
                        string confirmPassword = ReadLine();

                        if (newPassword.Equals(confirmPassword))
                        {
                            existingAdmin.Password = newPassword;

                            //Invoke UpdateSystemUserBL method to update
                            bool isUpdated = await adminBL.UpdateAdminPasswordBL(existingAdmin);
                            if (isUpdated)
                            {
                                WriteLine("Admin Password Updated");
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



        /// <summary>
        /// updates admin profile (i.e, name, email,mobile number).
        /// </summary>
        /// <returns></returns>
        public static async Task UpdateAdmin()
        {
            try
            {
                using (IAdminBL adminBL = new AdminBL())
                {
                    //Read Sl.No
                    Admin admin = await adminBL.GetAdminByAdminEmailBL(CommonData.CurrentUser.Email);
                    Write("Name: ");
                    admin.AdminName = ReadLine();
                    Write("Email: ");
                    admin.Email = ReadLine();

                    //Invoke UpdateRetailerBL method to update
                    bool isUpdated = await adminBL.UpdateAdminBL(admin);
                    if (isUpdated)
                    {
                        WriteLine(" Account Updated");
                    }

                }

            }
            catch (GreatOutdoorException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// views all sales persons report
        /// </summary>
        /// <returns></returns>
        public static async Task ViewAllSalesReports()
        {
            ViewSalesReportsBL vr = new ViewSalesReportsBL();
            List<ViewSalesReports> salesreports = await vr.ViewAllSalesReportsBL();
            WriteLine("Sales Person ID \t\t Sales Person Name \t  Offline Sales Count \t All orders Cost\t  Target");
            foreach (ViewSalesReports item in salesreports)
            {
                WriteLine(item.SalespersonID + "\t" + item.SalespersonName + "\t" + item.OfflinesalesCount + "\t" + item.TotalAmount + "\t" + item.Target);
            }
        }

        /// <summary>
        /// views sales person profile based on name/email/id.
        /// </summary>
        /// <returns></returns>
        public static async Task ViewSalesPersonsProfile()
        {
            try
            {
                using (ISalesPersonBL SalesPersonBL = new SalesPersonBL())
                {
                    //Get and display list of system users.
                    List<SalesPerson> SalesPersons = await SalesPersonBL.GetAllSalesPersonsBL();
                    WriteLine("Sales Persons:");
                    if (SalesPersons != null && SalesPersons?.Count > 0)
                    {
                        WriteLine("#\tName\tMobile\tEmail\tCreated\tModified");
                        int serial = 0;
                        foreach (var SalesPerson in SalesPersons)
                        {
                            serial++;
                            WriteLine($"{serial}\t{SalesPerson.SalesPersonName}\t{SalesPerson.SalesPersonMobile}\t{SalesPerson.Email}\t{SalesPerson.CreationDateTime}\t{SalesPerson.LastModifiedDateTime}");
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

        /// <summary>
        /// views retailer reports.
        /// </summary>
        /// <returns></returns>
        public static async Task ViewRetailerReports()
        {
            RetailerBL retailerBL = new RetailerBL();
            List<Retailer> retailers = await retailerBL.GetAllRetailersBL();
            List<RetailerReport> retailerreports = new List<RetailerReport>();
            RetailerReport item;
            foreach (var retailer in retailers)
            {
                item = await retailerBL.GetRetailerReportByRetailIDBL(retailer.RetailerID);
                retailerreports.Add(item);

            }
            WriteLine("#\tRetailer Name\t no.of orders placed\t Total amount spent on orders");
            int serial = 0;
            foreach (var report in retailerreports)
            {
                serial++;
                WriteLine($"{serial}\t{report.RetailerName}\t{report.RetailerSalesCount}\t{report.RetailerSalesAmount}");
            }



        }
        /// <summary>
        /// Adds Product.
        /// </summary>
        /// <returns></returns>
        public static async Task AddProduct()
        {
            try
            {
                //Read inputs
                Product product = new Product();
                Write("Product Name: ");
                product.ProductName = ReadLine();
                Write("Product Category: ");

                Write("Product Price: ");
                product.ProductPrice = double.Parse(ReadLine());
                Write("Product Color: ");
                product.ProductColor = ReadLine();
                Write("Product Size: ");
                product.ProductSize = ReadLine();
                Write("Product Material: ");
                product.ProductMaterial = ReadLine();

                //Invoke AddSystemUserBL method to add
                using (IProductBL productBL = new ProductBL())
                {
                    bool isAdded = await productBL.AddProductBL(product);
                    if (isAdded)
                    {
                        WriteLine("Product Added!");
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
        /// views retailers profile
        /// </summary>
        /// <returns></returns>
        public static async Task ViewRetailersProfile()
        {
            RetailerBL retailerBL = new RetailerBL();
            List<Retailer> retailers = await retailerBL.GetAllRetailersBL();
            WriteLine("#\tRetailer Name\t Retialer Email\tRetailer Phone no.");
            int serial = 0;
            foreach (var retailer in retailers)
            {
                serial++;
                WriteLine($"{serial}\t{retailer.RetailerName}\t{retailer.Email}\t{retailer.RetailerMobile}");
            }

        }

    }
}

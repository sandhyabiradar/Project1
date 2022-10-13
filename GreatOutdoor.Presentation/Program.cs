using System;
using System.Threading.Tasks;
using static System.Console;
using Capgemini.GreatOutdoor.BusinessLayer;
using Capgemini.GreatOutdoor.Exceptions;
using Capgemini.GreatOutdoor.Helpers;
using Capgemini.GreatOutdoor.Entities;
using Capgemini.GreatOutdoor.Contracts.BLContracts;

namespace Capgemini.GreatOutdoor.PresentationLayer
{
    class Program
    {
        /// <summary>
        /// GreatOutdoors page
        /// </summary>
        /// <param name="args">Command line arguments</param>
        /// <returns></returns>
        static async Task Main(string[] args)
        {
            try
            {
                int option = 1;
                do
                {
                    int internalChoice = -2;
                    WriteLine("===============GREAT OUTDOORS MANAGEMENT SYSTEM=========================");
                    WriteLine("1.Existing User\n2.Retailer Registration");
                    WriteLine("Enter your choice");
                    int choice;
                    bool isValidChoice = int.TryParse(ReadLine(), out choice);
                    if (isValidChoice)
                    {
                        switch (choice)
                        {
                            case 1:
                                do
                                {
                                    //Invoke Login Screen
                                    (UserType userType, IUser currentUser) = await ShowLoginScreen();

                                    //Set current user details into CommonData (global data)
                                    CommonData.CurrentUser = currentUser;
                                    CommonData.CurrentUserType = userType;

                                    //Invoke User's Menu
                                    if (userType == UserType.Admin)
                                    {
                                        internalChoice = await AdminPresentation.AdminUserMenu();
                                    }
                                    else if (userType == UserType.SalesPerson)
                                    {
                                        internalChoice = await SalesPersonPresentation.SalesPersonMenu();
                                    }
                                    else if (userType == UserType.Retailer)
                                    {
                                        internalChoice = await RetailerPresentation.RetailerUserMenu();
                                    }
                                    else if (userType == UserType.Anonymous)
                                    {
                                    }
                                } while (internalChoice != -1);
                                break;

                            case 2:
                                {
                                    await AddRetailer();

                                }
                                break;
                            default:
                                {
                                    WriteLine("Invalid choice");
                                }
                                break;
                        }

                    }
                } while (option == 1);
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex);
                WriteLine(ex.Message);
            }

            WriteLine("Thank you!");
            ReadKey();
        }

        /// <summary>
        /// Login (based on Email and Password)
        /// </summary>
        /// <returns></returns>
        static async Task<(UserType, IUser)> ShowLoginScreen()
        {
            //Read inputs
            string email, password;
            WriteLine("=====LOGIN=========");
            Write("Email: ");
            email = ReadLine();
            Write("Password: ");
            password = null;
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if ((key.Key != ConsoleKey.Backspace) && (key.Key != ConsoleKey.Enter))
                {
                    password += key.KeyChar;
                    Write("*");
                }
                else
                {
                    Write("\b");
                }
            }
            while (key.Key != ConsoleKey.Enter);
            WriteLine("");

            using (IAdminBL adminBL = new AdminBL())
            {
                //Invoke GetAdminByEmailAndPasswordBL for checking email and password of Admin
                Admin admin = await adminBL.GetAdminByEmailAndPasswordBL(email, password);
                if (admin != null)
                {
                    return (UserType.Admin, admin);
                }
            }

            using (ISalesPersonBL SalesPersonBL = new SalesPersonBL())
            {
                //Invoke GetAdminByEmailAndPasswordBL for checking email and password of Admin
                SalesPerson SalesPerson = await SalesPersonBL.GetSalesPersonByEmailAndPasswordBL(email, password);
                if (SalesPerson != null)
                {
                    return (UserType.SalesPerson, SalesPerson);
                }
            }
            using (IRetailerBL RetailerBL = new RetailerBL())
            {
                //Invoke GetAdminByEmailAndPasswordBL for checking email and password of Admin
                Retailer retailer = await RetailerBL.GetRetailerByEmailAndPasswordBL(email, password);
                if (retailer != null)
                {
                    return (UserType.Retailer, retailer);
                }
            }

            WriteLine("Invalid Email or Password. Please try again...");
            return (UserType.Anonymous, null);
        }
        private static async Task AddRetailer()
        {
            try
            {

                Retailer newRetailer = new Retailer();
                Console.WriteLine("Enter Retailer Name :");
                newRetailer.RetailerName = Console.ReadLine();
                Console.WriteLine("Enter Phone Number :");
                newRetailer.RetailerMobile = Console.ReadLine();
                Console.WriteLine("Enter Retailer's Email");
                newRetailer.Email = Console.ReadLine();
                Console.WriteLine("Enter Retailer's Password");
                newRetailer.Password = Console.ReadLine();
                newRetailer.RetailerID = default(Guid);
                RetailerBL rb = new RetailerBL();

                bool retailerAdded;
                Guid newRetailerGuid;
                  (retailerAdded,newRetailerGuid)  = await rb.AddRetailerBL(newRetailer);
                if (retailerAdded)
                {
                    Console.WriteLine("Retailer Added");
                    Console.WriteLine("Your User id is " + newRetailer.Email);

                }
                else
                    Console.WriteLine("Retailer Not Added");

            }
            catch (GreatOutdoorException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


    }
}
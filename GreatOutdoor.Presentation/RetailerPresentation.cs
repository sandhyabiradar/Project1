using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Console;
using Capgemini.GreatOutdoor.BusinessLayer;
using Capgemini.GreatOutdoor.Entities;
using Capgemini.GreatOutdoor.Exceptions;
using Capgemini.GreatOutdoor.Contracts.BLContracts;
using GreatOutdoor.Presentation;
using Capgemini.GreatOutdoor.Contracts;
using System.Linq;

namespace Capgemini.GreatOutdoor.PresentationLayer
{
    public static class RetailerPresentation
    {
        /// <summary>
        /// Menu for Retailer
        /// </summary>
        /// <returns></returns>
        public static async Task<int> RetailerUserMenu()
        {
            int choice = -2;
            using (IRetailerBL retailerBL = new RetailerBL())
            {
                do
                {

                    //Menu
                    WriteLine("\n1. Initiate order");
                    WriteLine("2. Cancel retailer  Order");
                    WriteLine("3. Return OnlineOrder");
                    WriteLine("4. Update Account");
                    WriteLine("5. Change Password");
                    WriteLine("6. Manage Address");
                    WriteLine("7. Delete Account");
                    WriteLine("0. Logout");
                    WriteLine("-1. Exit");
                    Write("Choice: ");

                    //Accept and check choice
                    bool isValidChoice = int.TryParse(ReadLine(), out choice);
                    if (isValidChoice)
                    {
                        switch (choice)
                        {
                            case 1: await InitiateOrder(); break;
                            case 2: await CancelRetailerOrder(); break;
                            case 3: await ReturnOnlineOrder(); break;
                            case 4: await UpdateRetailerAccount(); break;
                            case 5: await ChangeRetailerPassword(); break;
                            case 6: await ManageAddress(); break;
                            case 7: await DeleteRetailerAccount(); break;
                            case 0: break;
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
        /// Initiate Ordeer
        /// </summary>
        /// <returns></returns>
        public static async Task InitiateOrder()
        {
            try
            {
                using (IProductBL product = new ProductBL())
                {
                    await RetailerProductPresentation.RetailerProductMenu();
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex);
                WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Return  Order
        /// </summary>
        /// <returns></returns>
        public static async Task ReturnOnlineOrder()
        {
            try
            {
                using (IProductBL product = new ProductBL())
                {
                    await OnlineReturnPresentation.OnlineReturnMenu();
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex);
                WriteLine(ex.Message);
            }
        }

        /// <summary>

        /// <summary>
        /// Manage Address
        /// </summary>
        /// <returns></returns>
        public static async Task ManageAddress()
        {
            try
            {
                int internalvalue = await AddressPresentation.AddressPresentationMenu();
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex);
                WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Updates Retailer
        /// </summary>
        /// <returns></returns>
        public static async Task UpdateRetailerAccount()
        {
            try
            {
                using (IRetailerBL retailerBL = new RetailerBL())
                {
                    //Read Sl.No
                    Retailer retailer = await retailerBL.GetRetailerByEmailBL(CommonData.CurrentUser.Email);
                    Write("Name: ");
                    retailer.RetailerName = ReadLine();
                    Write("Email: ");
                    retailer.Email = ReadLine();

                    //Invoke UpdateRetailerBL method to update
                    bool isUpdated = await retailerBL.UpdateRetailerBL(retailer);
                    if (isUpdated)
                    {
                        WriteLine(" Account Updated");
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
        /// Delete Retailer Account.
        /// </summary>
        /// <returns></returns>
        public static async Task DeleteRetailerAccount()
        {
            try
            {
                using (IRetailerBL retailerBL = new RetailerBL())
                {

                    Write("Are you sure? (Y/N): ");
                    Write("Current Password: ");
                    string currentPassword = ReadLine();
                    string confirmation = ReadLine();
                    Retailer retailer = await retailerBL.GetRetailerByEmailAndPasswordBL(CommonData.CurrentUser.Email, currentPassword);
                    if (confirmation.Equals("Y", StringComparison.OrdinalIgnoreCase))
                    {
                        //Invoke DeleteRetailerBL method to delete
                        bool isDeleted = await retailerBL.DeleteRetailerBL(retailer.RetailerID);
                        if (isDeleted)
                        {
                            WriteLine("Retailer Account Deleted");
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
        /// Updates Retailer's Password.
        /// </summary>
        /// <returns></returns>
        public static async Task ChangeRetailerPassword()
        {
            try
            {
                using (IRetailerBL retailerBL = new RetailerBL())
                {
                    //Read Current Password
                    Write("Current Password: ");
                    string currentPassword = ReadLine();

                    Retailer existingRetailer = await retailerBL.GetRetailerByEmailAndPasswordBL(CommonData.CurrentUser.Email, currentPassword);

                    if (existingRetailer != null)
                    {
                        //Read inputs
                        Write("New Password: ");
                        string newPassword = ReadLine();
                        Write("Confirm Password: ");
                        string confirmPassword = ReadLine();

                        if (newPassword.Equals(confirmPassword))
                        {
                            existingRetailer.Password = newPassword;

                            //Invoke UpdateRetailerBL method to update
                            bool isUpdated = await retailerBL.UpdateRetailerPasswordBL(existingRetailer);
                            if (isUpdated)
                            {
                                WriteLine("Retailer Password Updated");
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

        //public static async Task CancelRetailerOrder()
        //{
        //    List<OrderDetail> matchingOrder = new List<OrderDetail>();//list maintains the order details of the order which user wishes to cancel
        //    try
        //    {
        //        using (IRetailerBL retailerBL = new RetailerBL())
        //        {
        //            //gives the current retailer
        //            Retailer retailer = await retailerBL.GetRetailerByEmailBL(CommonData.CurrentUser.Email);
        //            using (IOrderBL orderDAL = new OrderBL())
        //            {
        //                //list of orders ordered by the retailer
        //                List<Order> RetailerOrderList = await orderDAL.GetOrdersByRetailerIDBL(retailer.RetailerID);
        //                Console.WriteLine("Enter the order number which you want to cancel");
        //                int orderToBeCancelled = int.Parse(Console.ReadLine());//user input of order which he has to cancel 
        //                foreach (Order order in RetailerOrderList)
        //                {
        //                    using (IOrderDetailBL orderDetailBL = new OrderDetailBL())
        //                    {
        //                        //getting the order details of required order to be cancelled
        //                        List<OrderDetail> RetailerOrderDetails = await orderDetailBL.GetOrderDetailsByOrderIDBL(order.OrderId);
        //                        matchingOrder = RetailerOrderDetails.FindAll(
        //                                   (item) => { return item.OrderSerial == orderToBeCancelled; }
        //                               );
        //                        break;
        //                    }
        //                }

        //                if (matchingOrder.Count != 0)
        //                {
        //                    OrderDetailBL orderDetailBL = new OrderDetailBL();
        //                    //cancel order if order not delivered
        //                    if (!( await orderDetailBL.UpdateOrderDeliveredStatusBL(matchingOrder[0].OrderId)))
        //                    {
        //                        int serial = 0;
        //                        Console.WriteLine("Products in the order are ");
        //                        foreach (OrderDetail orderDetail in matchingOrder)
        //                        {
        //                            //displaying order details with the products ordered
        //                            serial++;
        //                            Console.WriteLine("#\tProductID \t ProductQuantityOrdered");
        //                            Console.WriteLine($"{ serial}\t{ orderDetail.ProductID}\t{ orderDetail.ProductQuantityOrdered}");
        //                        }
        //                        Console.WriteLine("Enter The Product to be Cancelled");
        //                        int ProductToBeCancelled = int.Parse(Console.ReadLine());
        //                        Console.WriteLine("Enter The Product Quantity to be Cancelled");
        //                        int quantityToBeCancelled = int.Parse(Console.ReadLine());
        //                        if (matchingOrder[ProductToBeCancelled - 1].ProductQuantityOrdered >= quantityToBeCancelled)
        //                        {
        //                            //updating order quantity and revenue
        //                            matchingOrder[ProductToBeCancelled - 1].ProductQuantityOrdered -= quantityToBeCancelled;
        //                            matchingOrder[ProductToBeCancelled - 1].TotalAmount -= matchingOrder[ProductToBeCancelled - 1].ProductPrice * quantityToBeCancelled;
        //                            OrderDetailBL orderDetailBL1 = new OrderDetailBL();
        //                            await orderDetailBL1.UpdateOrderDetailsBL(matchingOrder[ProductToBeCancelled - 1]);

        //                            Console.WriteLine("Product Cancelled Succesfully");

        //                        }
        //                        else
        //                        {
        //                            Console.WriteLine("PRODUCT QUANTITY EXCEEDED");
        //                        }
        //                    }
        //                    else
        //                    {
        //                        Console.WriteLine("Order Can't be cancelled as it is delivered");
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}



        public static async Task CancelRetailerOrder()
        {
            IProductBL productBL = new ProductBL();
            List<OrderDetail> matchingOrder = new List<OrderDetail>();//list maintains the order details of the order which user wishes to cancel
            Console.WriteLine("Enter the order number which you want to cancel");
            double orderToBeCancelled = double.Parse(Console.ReadLine());//user input of order which he has to cancel 
            Console.WriteLine("Entered Number is: " + orderToBeCancelled);
            try
            {
                using (IOrderBL orderBL = new OrderBL())
                {
                    try
                    {

                        Order order = await orderBL.GetOrderByOrderNumberBL(orderToBeCancelled);
                        using (IOrderDetailBL orderDetailBL = new OrderDetailBL())
                        {
                            matchingOrder = await orderDetailBL.GetOrderDetailsByOrderIDBL(order.OrderId);
                            int a = matchingOrder.Count();
                            Console.WriteLine("No. of Products ordered: " + a);
                        }
                    }
                    catch (Exception)
                    {
                        WriteLine("Invalid OrderId");
                    }

                }


                if (matchingOrder.Count != 0)
                {
                    OrderDetailBL orderDetailBL = new OrderDetailBL();
                    //cancel order if order not delivered
                    if ((await orderDetailBL.UpdateOrderDeliveredStatusBL(matchingOrder[0].OrderId)))
                    {
                        int serial = 0;

                        Console.WriteLine("Products in the order are ");
                        Console.WriteLine("# \t\t\tProductID \t\t\t ProductQuantityOrdered \t\t UnitProductPrice");
                        foreach (OrderDetail orderDetail in matchingOrder)
                        {
                            //displaying order details with the products ordered
                            Product product = await productBL.GetProductByProductIDBL(orderDetail.ProductID);
                            serial++;
                            //Console.WriteLine(product.ProductName);
                            Console.WriteLine($"{ serial}\t{ orderDetail.ProductID}\t\t{ orderDetail.ProductQuantityOrdered}\t\t{orderDetail.ProductPrice}");
                        }
                        Console.WriteLine("Enter The Product to be Cancelled");
                        int ProductToBeCancelled = int.Parse(Console.ReadLine());
                        if (ProductToBeCancelled <= matchingOrder.Count && ProductToBeCancelled > 0)
                        {
                            Console.WriteLine("Enter The Product Quantity to be Cancelled");
                            int quantityToBeCancelled = int.Parse(Console.ReadLine());
                            if (matchingOrder[ProductToBeCancelled - 1].ProductQuantityOrdered >= quantityToBeCancelled)
                            {
                                //updating order quantity and revenue
                                matchingOrder[ProductToBeCancelled - 1].ProductQuantityOrdered -= quantityToBeCancelled;
                                matchingOrder[ProductToBeCancelled - 1].TotalAmount -= matchingOrder[ProductToBeCancelled - 1].ProductPrice * quantityToBeCancelled;
                                Console.WriteLine("Total Refund Amount: " + (matchingOrder[ProductToBeCancelled - 1].ProductPrice * quantityToBeCancelled));
                                OrderDetailBL orderDetailBL1 = new OrderDetailBL();
                                await orderDetailBL1.UpdateOrderDetailsBL(matchingOrder[ProductToBeCancelled - 1]);
                                Console.WriteLine("Product Cancelled Succesfully");
                            }
                            else
                            {
                                Console.WriteLine("PRODUCT QUANTITY EXCEEDED");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Wrong Input");
                        }

                    }
                    else
                    {
                        Console.WriteLine("Order Can't be cancelled as it is delivered");
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



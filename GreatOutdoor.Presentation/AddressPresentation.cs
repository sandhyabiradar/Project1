﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Console;
using Capgemini.GreatOutdoor.BusinessLayer;
using Capgemini.GreatOutdoor.Entities;
using Capgemini.GreatOutdoor.Exceptions;
using Capgemini.GreatOutdoor.Contracts.BLContracts;

namespace Capgemini.GreatOutdoor.PresentationLayer
{
    public static class AddressPresentation
    {
        /// <summary>
        /// Menu for Retailer Address User
        /// </summary>
        /// <returns></returns>
        public static async Task<int> AddressPresentationMenu()
        {
            int choice = -2;
            using (IAddressBL addressBL = new AddressBL())
            {
                do
                {
                    //Get and display list of system users.
                    RetailerBL retailerBL = new RetailerBL();
                    Retailer retailer = await retailerBL.GetRetailerByEmailBL(CommonData.CurrentUser.Email);
                    List<Address> addresses = await addressBL.GetAddressByRetailerIDBL(retailer.RetailerID);
                    WriteLine("\n***************ADDRESS***********\n");
                    WriteLine("Addresses:");
                    if (addresses != null && addresses?.Count > 0)
                    {
                        WriteLine("#\tAddressLine 1\tLandMark\tCity\tState");
                        int serial = 0;
                        foreach (var address in addresses)
                        {
                            serial++;
                            WriteLine($"{serial}\t{address.AddressLine1}\t{address.Landmark}\t{address.City}\t{address.State}");
                        }
                    }

                    //Menu
                    WriteLine("\n1.Add Address");
                    WriteLine("2. Update Existing Address");
                    WriteLine("3. Delete Existing Address");
                    WriteLine("0. Back");
                    WriteLine("-1. Exit");
                    Write("Choice: ");

                    //Accept and check choice
                    bool isValidChoice = int.TryParse(ReadLine(), out choice);
                    if (!isValidChoice)
                    {
                        switch (choice)
                        {
                            case 1: await AddAddress(); break;
                            case 2: await UpdateAddress(); break;
                            case 3: await DeleteAddress(); break;
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
            return ;
        }

        /// <summary>
        /// Adds Address
        /// </summary>
        /// <returns></returns>
        public static async Task AddAddress()
        {
            try
            {
                //Read inputs
                Address address = new Address();
                RetailerBL retailerBL = new RetailerBL();
                Retailer retailer = await retailerBL.GetRetailerByEmailBL(CommonData.CurrentUser.Email);
                address.RetailerID = retailer.RetailerID;
                Write("Address Line 1: ");
                address.AddressLine1 = ReadLine();
                Write("Address Line 2: ");
                address.AddressLine2 = ReadLine();
                Write("LandMark: ");
                address.Landmark = ReadLine();
                Write("City: ");
                address.City = ReadLine();
                Write("State: ");
                address.State = ReadLine();
                Write("PinCode: ");
                address.PinCode = ReadLine();
                //Invoke AddAddressBL method to add
                using (AddressBL addressBL = new AddressBL())
                {
                    bool isAdded = await addressBL.AddAddressBL(address);
                    if (isAdded)
                    {
                        WriteLine("Address Added");
                    }
                    WriteLine("Do you want to serailize:Y/N");

                    string confirmation = ReadLine();

                    if (confirmation.Equals("Y", StringComparison.OrdinalIgnoreCase))
                    {
                        addressBL.Serialize();
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
        /// Updates address
        /// </summary>
        /// <returns></returns>
        public static async Task UpdateAddress()
        {
            try
            {
                using (IAddressBL addressBL = new AddressBL())
                {
                    //Read Sl.No
                    Write("Address #: ");
                    bool isNumberValid = int.TryParse(ReadLine(), out int serial);
                    if (isNumberValid)
                    {
                        serial--;
                        RetailerBL retailerBL = new RetailerBL();
                        Retailer retailer = await retailerBL.GetRetailerByEmailBL(CommonData.CurrentUser.Email);
                        List<Address> addresses = await addressBL.GetAddressByRetailerIDBL(retailer.RetailerID);
                        if (serial <= addresses.Count - 1)
                        {
                            //Read inputs
                            Address address = addresses[serial];
                            Write("Address Line 1: ");
                            address.AddressLine1 = ReadLine();
                            Write("Address Line 2: ");
                            address.AddressLine2 = ReadLine();
                            Write("LandMark: ");
                            address.Landmark = ReadLine();
                            Write("City: ");
                            address.City = ReadLine();
                            Write("State: ");
                            address.State = ReadLine();
                            Write("PinCode: ");
                            address.PinCode = ReadLine();

                            //Invoke UpdateAddressBL method to update
                            bool isUpdated = await addressBL.UpdateAddressBL(address);
                            if (isUpdated)
                            {
                                WriteLine("Address Updated");
                            }
                        }
                        else
                        {
                            WriteLine($"Invalid Address #.\nPlease enter a number between 1 to {addresses.Count}");
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
        /// Delete Address.
        /// </summary>
        /// <returns></returns>
        public static async Task DeleteAddress()
        {
            try
            {
                using (IAddressBL addressBL = new AddressBL())
                {
                    //Read Sl.No
                    Write("Address #: ");
                    bool isNumberValid = int.TryParse(ReadLine(), out int serial);
                    if (isNumberValid)
                    {
                        serial--;
                        RetailerBL retailerBL = new RetailerBL();
                        Retailer retailer = await retailerBL.GetRetailerByEmailBL(CommonData.CurrentUser.Email);
                        List<Address> addresses = await addressBL.GetAddressByRetailerIDBL(retailer.RetailerID);

                        Write("Are you sure? (Y/N): ");


                        string confirmation = ReadLine();

                        if (confirmation.Equals("Y", StringComparison.OrdinalIgnoreCase))
                        {
                            if (serial <= addresses.Count - 1)
                            { //Invoke DeleteSystemUserBL method to delete
                                Address address = addresses[serial];
                                bool isDeleted = await addressBL.DeleteAddressBL(address.AddressID);
                                if (isDeleted)
                                {
                                    WriteLine("Retailer Address Deleted");
                                }
                            }
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
    }
}







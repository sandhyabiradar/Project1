﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Capgemini.GreatOutdoor.Entities;
using Newtonsoft.Json;
namespace Capgemini.GreatOutdoor.Contracts.DALContracts
{ /// <summary>
  /// This abstract class acts as a base for OrderDAL class
  /// </summary>
    public abstract class OrderDALBase
    {
        //Collection of Orders
        protected static List<Order> ordersList = new List<Order>();

        private static string fileName = "orders.json";

        //Methods
        public abstract Order GetOrderByOrderNumberDAL(double orderNumber);
        public abstract (bool,Guid) AddOrderDAL(Order newOrder);
        public abstract Order GetOrderByOrderIDDAL(Guid searchOrderID);
        public abstract bool UpdateOrderDAL(Order updateOrder);
        public abstract bool DeleteOrderDAL(Guid deleteOrderID);
        public abstract List<Order> GetOrdersByRetailerIDDAL(Guid retailerID);

        /// <summary>
        /// Writes collection to the file in JSON format.
        /// </summary>
        public static void Serialize()
        {
            string serializedJson = JsonConvert.SerializeObject(ordersList);
            using (StreamWriter streamWriter = new StreamWriter(fileName))
            {
                streamWriter.Write(serializedJson);
                streamWriter.Close();
            }
        }

        /// <summary>
        /// Reads collection from the file in JSON format.
        /// </summary>
        public static void Deserialize()
        {
            string fileContent = string.Empty;
            if (!File.Exists(fileName))
                File.Create(fileName).Close();

            using (StreamReader streamReader = new StreamReader(fileName))
            {
                fileContent = streamReader.ReadToEnd();
                streamReader.Close();
                var orderListFromFile = JsonConvert.DeserializeObject<List<Order>>(fileContent);
                if (orderListFromFile != null)
                {
                    ordersList = orderListFromFile;
                }
            }
        }

        /// <summary>
        /// Static Constructor.
        /// </summary>
        static OrderDALBase()
        {
            Deserialize();
        }
    }
}

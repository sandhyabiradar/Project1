using System;
using System.Collections.Generic;
using System.IO;
using Capgemini.GreatOutdoor.Entities;
using Newtonsoft.Json;

namespace Capgemini.GreatOutdoor.Contracts.DALContracts
{
    /// <summary>
    /// This abstract class acts as a base for RetailerDAL class
    /// </summary>
    public abstract class RetailerDALBase
    {
        //Collection of Retailers
        protected static List<Retailer> retailerList = new List<Retailer>() { new Retailer() { RetailerID = Guid.NewGuid(), Email = "prafull@capgemini.com", RetailerName = "prafull", Password = "prafull", CreationDateTime = DateTime.Now, LastModifiedDateTime = DateTime.Now } };
        private static string fileName = "retailers.json";

        //Methods for CRUD operations
        public abstract (bool,Guid) AddRetailerDAL(Retailer newRetailer);
        public abstract List<Retailer> GetAllRetailersDAL();
        public abstract Retailer GetRetailerByRetailerIDDAL(Guid searchRetailerID);
        public abstract List<Retailer> GetRetailersByNameDAL(string retailerName);
        public abstract Retailer GetRetailerByEmailDAL(string email);
        public abstract Retailer GetRetailerByEmailAndPasswordDAL(string email, string password);
        public abstract bool UpdateRetailerDAL(Retailer updateRetailer);
        public abstract bool UpdateRetailerPasswordDAL(Retailer updateRetailer);
        public abstract bool DeleteRetailerDAL(Guid deleteRetailerID);

        /// <summary>
        /// Writes collection to the file in JSON format.
        /// </summary>
        public static void Serialize()
        {
            string serializedJson = JsonConvert.SerializeObject(retailerList);
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
                var retailerListFromFile = JsonConvert.DeserializeObject<List<Retailer>>(fileContent);
                if (retailerListFromFile != null)
                {
                    retailerList = retailerListFromFile;
                }
            }
        }

        /// <summary>
        /// Static Constructor.
        /// </summary>
        static RetailerDALBase()
        {
            Deserialize();
        }
    }
}



using System;
using System.Collections.Generic;
using System.IO;
using Capgemini.GreatOutdoor.Entities;
using Newtonsoft.Json;

namespace Capgemini.GreatOutdoor.Contracts.DALContracts
{
    /// <summary>
    /// This abstract class acts as a base for SalesPersonDAL class
    /// </summary>
    public abstract class SalesPersonDALBase
    {
        //Collection of SalesPersons
        public static List<SalesPerson> salesPersonList = new List<SalesPerson>()
        {
            new SalesPerson() { SalesPersonID = Guid.NewGuid(), Email = "aayush@capgemini.com", SalesPersonName = "Aayush", Password = "aayush", CreationDateTime = DateTime.Now, LastModifiedDateTime = DateTime.Now },
            new SalesPerson() { SalesPersonID = Guid.NewGuid(), Email = "akhilh@capgemini.com", SalesPersonName = "Akhil", Password = "akhil", CreationDateTime = DateTime.Now, LastModifiedDateTime = DateTime.Now }
        };
        private static string fileName = "salesPersons.json";

        //Methods for CRUD operations
        public abstract (bool,Guid) AddSalesPersonDAL(SalesPerson newSalesPerson);
        public abstract List<SalesPerson> GetAllSalesPersonsDAL();
        public abstract SalesPerson GetSalesPersonBySalesPersonIDDAL(Guid searchSalesPersonID);
        public abstract List<SalesPerson> GetSalesPersonsByNameDAL(string salesPersonName);
        public abstract SalesPerson GetSalesPersonByEmailDAL(string email);
        public abstract SalesPerson GetSalesPersonByEmailAndPasswordDAL(string email, string password);
        public abstract bool UpdateSalesPersonDAL(SalesPerson updateSalesPerson);
        public abstract bool UpdateSalesPersonPasswordDAL(SalesPerson updateSalesPerson);
        public abstract bool DeleteSalesPersonDAL(Guid deleteSalesPersonID);

        /// <summary>
        /// Writes collection to the file in JSON format.
        /// </summary>
        public static void Serialize()
        {
            string serializedJson = JsonConvert.SerializeObject(salesPersonList);
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
                var salesPersonListFromFile = JsonConvert.DeserializeObject<List<SalesPerson>>(fileContent);
                if (salesPersonListFromFile != null)
                {
                    salesPersonList = salesPersonListFromFile;
                }
            }
        }

        /// <summary>
        /// Static Constructor.
        /// </summary>
        static SalesPersonDALBase()
        {
            Deserialize();
        }
    }
}




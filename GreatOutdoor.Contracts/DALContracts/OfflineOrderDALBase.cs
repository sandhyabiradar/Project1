using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using Capgemini.GreatOutdoor.Entities;



namespace Capgemini.GreatOutdoor.Contracts.DALContracts
{
    public abstract class OfflineOrderDALBase
    {

        //Collection of OfflineOrders
        protected static List<OfflineOrder> OfflineOrderList = new List<OfflineOrder>();
        //private static string fileName = "OfflineOrders.json";
        /// <summary>
        /// Writes collection to the file in JSON format.
        /// </summary>
        //public static void Serialize()
        //{
        //    string serializedJson = JsonConvert.SerializeObject(OfflineOrderList);
        //    using (StreamWriter streamWriter = new StreamWriter(fileName))
        //    {
        //        streamWriter.Write(serializedJson);
        //        streamWriter.Close();
        //    }
        //}

        /// <summary>
        /// Reads collection from the file in JSON format.
        /// </summary>
        //public static void Deserialize()
        //{
        //    string fileContent = string.Empty;
        //    if (!File.Exists(fileName))
        //        File.Create(fileName).Close();

        //    using (StreamReader streamReader = new StreamReader(fileName))
        //    {
        //        fileContent = streamReader.ReadToEnd();
        //        streamReader.Close();
        //        var OfflineOrderListFromFile = JsonConvert.DeserializeObject<List<OfflineOrder>>(fileContent);
        //        if (OfflineOrderListFromFile != null)
        //        {
        //            OfflineOrderList = OfflineOrderListFromFile;
        //        }
        //    }
        //}
        ///// <summary>
        /// Static Constructor.
        /// </summary>
        //static OfflineOrderDALBase()
        //{
        //    Deserialize();
        //}
    }
}

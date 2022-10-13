using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Capgemini.GreatOutdoor.Entities;
using Newtonsoft.Json;


namespace Capgemini.GreatOutdoor.Contracts.DALContracts
{
    public class OfflineOrderDetailDALBase
    {
        //Collection of OfflineOrderDetail
        protected static List<OfflineOrderDetail> OfflineOrderDetailList = new List<OfflineOrderDetail>();
        private static string fileName = "OfflineOrderDetail.json";
        /// <summary>
        /// Writes collection to the file in JSON format.
        /// </summary>
        public static void Serialize()
        {
            string serializedJson = JsonConvert.SerializeObject(OfflineOrderDetailList);
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
                var OfflineOrderDetailListFromFile = JsonConvert.DeserializeObject<List<OfflineOrderDetail>>(fileContent);
                if (OfflineOrderDetailListFromFile != null)
                {
                    OfflineOrderDetailList = OfflineOrderDetailListFromFile;
                }
            }
        }
        /// <summary>
        /// Static Constructor.
        /// </summary>
        static OfflineOrderDetailDALBase()
        {
            Deserialize();
        }
    }
}

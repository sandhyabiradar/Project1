using System;
using System.Collections.Generic;
using System.IO;
using Capgemini.GreatOutdoor.Entities;
using Newtonsoft.Json;
using Capgemini.GreatOutdoor.Helpers;

namespace Capgemini.GreatOutdoor.Contracts.DALContracts
{
    /// <summary>
    /// This abstract class acts as a base for OfflineReturnDAL class
    /// </summary>
    public abstract class OfflineReturnDALBase
    {
        //Collection of OfflineReturnList
        protected static List<OfflineReturn> OfflineReturnList = new List<OfflineReturn>();
        private static string fileName = "OfflineReturns.json";

        //Methods for CRUD operations
        public abstract bool AddOfflineReturnDAL(OfflineReturn newOfflineReturn);
        public abstract List<OfflineReturn> GetAllOfflineReturnsDAL();
        public abstract OfflineReturn GetOfflineReturnByOfflineReturnIDDAL(Guid searchOfflineReturnID);
        public abstract List<OfflineReturn> GetOfflineReturnsByPurposeDAL(PurposeOfReturn purpose);
        public abstract bool UpdateOfflineReturnDAL(OfflineReturn updateOfflineReturn);
        public abstract bool DeleteOfflineReturnDAL(Guid deleteOfflineReturnID);

        /// <summary>
        /// Writes collection to the file in JSON format.
        /// </summary>
        public static void Serialize()
        {
            string serializedJson = JsonConvert.SerializeObject(OfflineReturnList);
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
                var systemUserListFromFile = JsonConvert.DeserializeObject<List<OfflineReturn>>(fileContent);
                if (systemUserListFromFile != null)
                {
                    OfflineReturnList = systemUserListFromFile;
                }
            }
        }

        /// <summary>
        /// Static Constructor.
        /// </summary>
        static OfflineReturnDALBase()
        {
            Deserialize();
        }
    }
}



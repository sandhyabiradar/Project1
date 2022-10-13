using System;
using System.Collections.Generic;
using System.Text;

namespace Capgemini.GreatOutdoor.Entities
{
    public class RetailerReport
    {
        public Guid RetailerID { get; set; }
        public string RetailerName { get; set; }
        public int RetailerSalesCount { get; set; }
        public double RetailerSalesAmount { get; set; }


        public RetailerReport()
        {
            RetailerID = default(Guid);
            RetailerName = null;
            RetailerSalesCount = 0;
            RetailerSalesAmount = 0;



        }
    }


}


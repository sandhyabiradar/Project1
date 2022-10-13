using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Capgemini.GreatOutdoor.DataAccessLayer;
using Capgemini.GreatOutdoor.Entities;
/// <summary>
/// developed by sravani
/// </summary>
namespace Capgemini.GreatOutdoor.BusinessLayer
{/// <summary>
/// view sales reports validations class
/// </summary>
    public class ViewSalesReportsBL
    {
       /// <summary>
       /// lists all the sales persons reports
       /// </summary>
       /// <returns></returns>
        public async Task<List<ViewSalesReports>> ViewAllSalesReportsBL()
        {
            List<ViewSalesReports> viewSales = new List<ViewSalesReports>();
            try
            {
                
                ViewSalesReports item=new ViewSalesReports();
                SalesPersonDAL splist = new SalesPersonDAL();
                List<SalesPerson> salesPersonList = splist.GetAllSalesPersonsDAL();
                

                foreach (SalesPerson sp in  salesPersonList)
                {
                    item.SalespersonID = sp.SalesPersonID;
                    item.SalespersonName = sp.SalesPersonName;
                    item.LastUpdatedsalestime = default(DateTime);
                    OfflineOrderBL offorder = new OfflineOrderBL();
                    List<OfflineOrder> newlist = await offorder.GetOfflineOrderBySalesPersonIDBL(item.SalespersonID);
                    foreach(var Offlineorder in newlist)
                    {
                        item.TotalAmount += Offlineorder.TotalOrderAmount;

                    }
                    item.OfflinesalesCount = newlist.Count;
                    
                    if (item.OfflinesalesCount > 100)
                    {
                        item.Target = "exceeded";
                    }
                    else if (item.OfflinesalesCount == 100)
                    {
                        item.Target = "Met";
                    }
                    else
                    {
                        item.Target = "Not Met";
                    }
                    viewSales.Add(item);

                }
                
            }
            catch (Exception)
            {
                throw;
            }
           return viewSales;
            
        }

    }
    }


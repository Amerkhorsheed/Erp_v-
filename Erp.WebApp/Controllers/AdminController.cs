using Erp_V1.DAL.DAL;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;

namespace Erp.WebApp.Controllers
{
    [CustomAuthorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly erp_v2Entities _dbContext = new erp_v2Entities();

        public ActionResult Index()
        {
            // --- 1. KPIs (No change needed) ---
            ViewBag.TotalProfit = CalculateTotalProfit();
            ViewBag.TotalRevenue = (_dbContext.SALES.Sum(s => (decimal?)s.ProductSalesPrice * s.ProductSalesAmout) ?? 0).ToString("C");
            ViewBag.TotalCustomers = _dbContext.CUSTOMER.Count(c => c.isDeleted == false);
            ViewBag.TotalSuppliers = _dbContext.SUPPLIER.Count(s => s.isDeleted == false);

            // --- 2. Profit & Expenses Chart (No change needed) ---
            var monthlyData = _dbContext.SALES
                .Join(_dbContext.PRODUCT, s => s.ProductID, p => p.ID, (s, p) => new { s, p })
                .GroupBy(x => new { x.s.SalesDate.Year, x.s.SalesDate.Month })
                .ToList()
                .Select(g => new
                {
                    MonthSort = g.Key.Year * 100 + g.Key.Month,
                    MonthLabel = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMM yyyy"),
                    Profit = g.Sum(x => ((decimal)x.s.ProductSalesPrice - (decimal)x.p.Price) * (decimal)x.s.ProductSalesAmout)
                })
                .OrderBy(x => x.MonthSort)
                .ToList();

            ViewBag.ChartLabels = JsonConvert.SerializeObject(monthlyData.Select(d => d.MonthLabel));
            ViewBag.ProfitData = JsonConvert.SerializeObject(monthlyData.Select(d => d.Profit));
            ViewBag.ExpenseData = JsonConvert.SerializeObject(monthlyData.Select(d => 0));

            // --- 3. Top 10 Products Chart (No change needed) ---
            var topProducts = _dbContext.SALES
                .Join(_dbContext.PRODUCT, s => s.ProductID, p => p.ID, (s, p) => new { s, p })
                .GroupBy(x => x.p.ProductName)
                .ToList()
                .Select(g => new
                {
                    ProductName = g.Key,
                    TotalSales = g.Sum(x => (decimal?)((decimal)x.s.ProductSalesPrice * (decimal)x.s.ProductSalesAmout)) ?? 0
                })
                .OrderByDescending(x => x.TotalSales)
                .Take(10)
                .OrderBy(x => x.TotalSales)
                .ToList();

            ViewBag.TopProductsLabels = JsonConvert.SerializeObject(topProducts.Select(p => p.ProductName));
            ViewBag.TopProductsData = JsonConvert.SerializeObject(topProducts.Select(p => p.TotalSales));

            // --- 4. FIX for Busiest Sales Day (Radar Chart) ---
            var salesByDay = _dbContext.SALES
                // Step 1: Select only the columns you need from the database.
                .Select(s => new
                {
                    s.SalesDate,
                    s.ProductSalesPrice,
                    s.ProductSalesAmout
                })
                // Step 2: Bring this minimal data into memory.
                .ToList()
                // Step 3: Now perform the GroupBy in C#, where .DayOfWeek is supported.
                .GroupBy(s => s.SalesDate.DayOfWeek)
                .ToDictionary(
                    g => g.Key,
                    g => g.Sum(s => (decimal?)s.ProductSalesPrice * s.ProductSalesAmout) ?? 0
                );

            var dayLabels = new string[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
            var dayData = new decimal[7];
            for (int i = 0; i < 7; i++)
            {
                if (salesByDay.ContainsKey((DayOfWeek)i))
                {
                    dayData[i] = salesByDay[(DayOfWeek)i];
                }
            }

            ViewBag.SalesByDayLabels = JsonConvert.SerializeObject(dayLabels);
            ViewBag.SalesByDayData = JsonConvert.SerializeObject(dayData);

            return View();
        }

        private string CalculateTotalProfit()
        {
            var salesData = _dbContext.SALES
                .Join(_dbContext.PRODUCT,
                      sale => sale.ProductID, product => product.ID,
                      (sale, product) => new {
                          SalePrice = sale.ProductSalesPrice,
                          CostPrice = product.Price,
                          Discount = sale.MaxDiscount,
                          Amount = sale.ProductSalesAmout
                      }).ToList();
            decimal totalSalesProfit = salesData.Sum(x => ((decimal)x.SalePrice - (decimal)x.CostPrice - (decimal)x.Discount) * (decimal)x.Amount);
            decimal totalExpenses = _dbContext.EXPENSES.Sum(e => (decimal?)e.Amount) ?? 0;
            return (totalSalesProfit - totalExpenses).ToString("C");
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing) { _dbContext.Dispose(); }
            base.Dispose(disposing);
        }
    }
}
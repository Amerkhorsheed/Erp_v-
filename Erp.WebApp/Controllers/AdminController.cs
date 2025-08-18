using Erp_V1.DAL.DAL;
using Erp_V1.DAL.DTO;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Data.Entity;

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

        // Returns last 20 transactions grouped by TransactionId with item count and total per transaction
        [HttpGet]
        public JsonResult GetRecentSales()
        {
            var recent = _dbContext.SALES
                .Where(s => !s.isDeleted)
                .GroupBy(s => s.TransactionId)
                .Select(g => new
                {
                    TransactionId = g.Key,
                    LatestDate = g.Max(s => s.SalesDate),
                    CustomerId = g.OrderByDescending(s => s.SalesDate).Select(s => s.CustomerID).FirstOrDefault(),
                    ItemCount = g.Sum(s => (int?)s.ProductSalesAmout) ?? 0,
                    TotalAmount = g.Sum(s => (long?)(s.ProductSalesPrice * s.ProductSalesAmout)) ?? 0
                })
                .OrderByDescending(x => x.LatestDate)
                .Take(20)
                .ToList();

            var customerNames = _dbContext.CUSTOMER
                .Where(c => !c.isDeleted)
                .ToDictionary(c => c.ID, c => c.CustomerName);

            var result = recent.Select(x => new
            {
                x.TransactionId,
                x.CustomerId,
                CustomerName = customerNames.ContainsKey(x.CustomerId) ? customerNames[x.CustomerId] : "Unknown",
                x.ItemCount,
                x.TotalAmount,
                Date = x.LatestDate
            });

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // Filter sales by date range and return aggregated per transaction
        [HttpGet]
        public JsonResult SearchSalesByDate(DateTime? startDate, DateTime? endDate)
        {
            var query = _dbContext.SALES.Where(s => !s.isDeleted);
            if (startDate.HasValue)
            {
                query = query.Where(s => DbFunctions.TruncateTime(s.SalesDate) >= DbFunctions.TruncateTime(startDate.Value));
            }
            if (endDate.HasValue)
            {
                query = query.Where(s => DbFunctions.TruncateTime(s.SalesDate) <= DbFunctions.TruncateTime(endDate.Value));
            }

            var grouped = query
                .GroupBy(s => s.TransactionId)
                .Select(g => new
                {
                    TransactionId = g.Key,
                    LatestDate = g.Max(s => s.SalesDate),
                    CustomerId = g.OrderByDescending(s => s.SalesDate).Select(s => s.CustomerID).FirstOrDefault(),
                    ItemCount = g.Sum(s => (int?)s.ProductSalesAmout) ?? 0,
                    TotalAmount = g.Sum(s => (long?)(s.ProductSalesPrice * s.ProductSalesAmout)) ?? 0
                })
                .OrderByDescending(x => x.LatestDate)
                .Take(20)
                .ToList();

            var customerNames = _dbContext.CUSTOMER
                .Where(c => !c.isDeleted)
                .ToDictionary(c => c.ID, c => c.CustomerName);

            var result2 = grouped.Select(x => new
            {
                x.TransactionId,
                x.CustomerId,
                CustomerName = customerNames.ContainsKey(x.CustomerId) ? customerNames[x.CustomerId] : "Unknown",
                x.ItemCount,
                x.TotalAmount,
                Date = x.LatestDate
            });

            return Json(result2, JsonRequestBehavior.AllowGet);
        }

        // Show comprehensive customer info and sales list
        [HttpGet]
        public ActionResult CustomerDetail(int id)
        {
            var customer = _dbContext.CUSTOMER.FirstOrDefault(c => c.ID == id && !c.isDeleted);
            if (customer == null)
            {
                return HttpNotFound();
            }

            var sales = _dbContext.SALES
                .Where(s => s.CustomerID == id && !s.isDeleted)
                .OrderByDescending(s => s.SalesDate)
                .Select(s => new SalesDetailDTO
                {
                    SalesID = s.ID,
                    CustomerID = s.CustomerID,
                    ProductID = s.ProductID,
                    CategoryID = s.CategoryID,
                    SalesAmount = s.ProductSalesAmout,
                    Price = s.ProductSalesPrice,
                    SalesDate = s.SalesDate,
                    TransactionId = s.TransactionId,
                    Madfou3 = s.Madfou3.HasValue ? (int)s.Madfou3.Value : 0,
                    Baky = s.Baky.HasValue ? (int)s.Baky.Value : 0
                })
                .ToList();

            var model = new
            {
                Customer = new CustomerDetailDTO
                {
                    ID = customer.ID,
                    CustomerName = customer.CustomerName,
                    Cust_Address = customer.Cust_Address,
                    Cust_Phone = customer.Cust_Phone,
                    Email = customer.Email,
                    Notes = customer.Notes,
                    baky = customer.baky ?? 0,
                    isDeleted = customer.isDeleted
                },
                Sales = sales
            };

            ViewBag.CustomerJson = JsonConvert.SerializeObject(model);
            return View();
        }

        // Get product analytics for dashboard display
        [HttpGet]
        public JsonResult GetProductAnalytics()
        {
            var lowStockThreshold = 10; // You can make this configurable

            var totalProducts = _dbContext.PRODUCT.Count(p => !p.isDeleted);
            var lowStockProducts = _dbContext.PRODUCT.Count(p => !p.isDeleted && p.StockAmount <= lowStockThreshold);
            var outOfStockProducts = _dbContext.PRODUCT.Count(p => !p.isDeleted && p.StockAmount <= 0);
            
            var topSellingProducts = _dbContext.SALES
                .Where(s => !s.isDeleted)
                .GroupBy(s => s.ProductID)
                .Select(g => new { 
                    ProductId = g.Key, 
                    TotalSold = g.Sum(s => s.ProductSalesAmout) 
                })
                .OrderByDescending(x => x.TotalSold)
                .Take(5)
                .ToList();

            var result = new
            {
                totalProducts,
                lowStockProducts,
                outOfStockProducts,
                lowStockThreshold,
                topSellingProducts
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // Get sales trend data for chart visualization
        [HttpGet]
        public JsonResult GetSalesTrend(DateTime? startDate, DateTime? endDate, string period = "daily")
        {
            var query = _dbContext.SALES.Where(s => !s.isDeleted);
            
            // Apply date filters
            if (startDate.HasValue)
            {
                query = query.Where(s => DbFunctions.TruncateTime(s.SalesDate) >= DbFunctions.TruncateTime(startDate.Value));
            }
            if (endDate.HasValue)
            {
                query = query.Where(s => DbFunctions.TruncateTime(s.SalesDate) <= DbFunctions.TruncateTime(endDate.Value));
            }

            // Default to last 30 days if no dates provided
            if (!startDate.HasValue && !endDate.HasValue)
            {
                var thirtyDaysAgo = DateTime.Now.AddDays(-30);
                query = query.Where(s => DbFunctions.TruncateTime(s.SalesDate) >= DbFunctions.TruncateTime(thirtyDaysAgo));
            }

            var salesData = query.ToList();

            List<object> chartData;
            List<string> labels;

            if (period.ToLower() == "weekly")
            {
                // Group by week
                var weeklyData = salesData
                    .GroupBy(s => new { 
                        Year = s.SalesDate.Year, 
                        Week = System.Globalization.CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
                            s.SalesDate, 
                            System.Globalization.CalendarWeekRule.FirstDay, 
                            DayOfWeek.Monday) 
                    })
                    .Select(g => new {
                        Period = g.Key,
                        TotalSales = g.Sum(s => s.ProductSalesPrice * s.ProductSalesAmout),
                        TransactionCount = g.Select(s => s.TransactionId).Distinct().Count(),
                        Date = g.Min(s => s.SalesDate)
                    })
                    .OrderBy(x => x.Date)
                    .ToList();

                labels = weeklyData.Select(x => $"Week {x.Period.Week}, {x.Period.Year}").ToList();
                chartData = weeklyData.Select(x => new { 
                    sales = x.TotalSales, 
                    transactions = x.TransactionCount,
                    date = x.Date.ToString("yyyy-MM-dd")
                }).Cast<object>().ToList();
            }
            else if (period.ToLower() == "monthly")
            {
                // Group by month
                var monthlyData = salesData
                    .GroupBy(s => new { s.SalesDate.Year, s.SalesDate.Month })
                    .Select(g => new {
                        Year = g.Key.Year,
                        Month = g.Key.Month,
                        TotalSales = g.Sum(s => s.ProductSalesPrice * s.ProductSalesAmout),
                        TransactionCount = g.Select(s => s.TransactionId).Distinct().Count(),
                        Date = new DateTime(g.Key.Year, g.Key.Month, 1)
                    })
                    .OrderBy(x => x.Date)
                    .ToList();

                labels = monthlyData.Select(x => new DateTime(x.Year, x.Month, 1).ToString("MMM yyyy")).ToList();
                chartData = monthlyData.Select(x => new {
                    sales = x.TotalSales,
                    transactions = x.TransactionCount,
                    date = x.Date.ToString("yyyy-MM-dd")
                }).Cast<object>().ToList();
            }
            else
            {
                // Group by day (default)
                var dailyData = salesData
                    .GroupBy(s => s.SalesDate.Date)
                    .Select(g => new {
                        Date = g.Key,
                        TotalSales = g.Sum(s => s.ProductSalesPrice * s.ProductSalesAmout),
                        TransactionCount = g.Select(s => s.TransactionId).Distinct().Count()
                    })
                    .OrderBy(x => x.Date)
                    .ToList();

                labels = dailyData.Select(x => x.Date.ToString("MMM dd")).ToList();
                chartData = dailyData.Select(x => new { 
                    sales = x.TotalSales, 
                    transactions = x.TransactionCount,
                    date = x.Date.ToString("yyyy-MM-dd")
                }).Cast<object>().ToList();
            }

            var result = new
            {
                labels = labels,
                data = chartData,
                period = period
            };

            return Json(result, JsonRequestBehavior.AllowGet);
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
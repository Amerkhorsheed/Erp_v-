// IMPORTANT: Make sure all your using statements are correct for your project
using Erp.WebApp.ViewModels;
using Erp_V1.DAL.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Erp.WebApp.Controllers
{
    [CustomAuthorize(Roles = "Admin")] // Assuming you have this security attribute
    public class AdminController : Controller
    {
        private readonly erp_v2Entities _dbContext = new erp_v2Entities();

        public ActionResult Index()
        {
            var model = new DashboardViewModel
            {
                CurrencySymbol = "?", // Or get from user settings
                KPIs = new List<KpiViewModel>
                {
                    new KpiViewModel
                    {
                        Title = "Total Profit",
                        Value = CalculateTotalProfit(),
                        Format = "currency",
                        TrendText = "Healthy Growth",
                        TrendIcon = "fa-arrow-trend-up",
                        IconClass = "fa-chart-line",
                        GradientClass = "bg-gradient-profit"
                    },
                    new KpiViewModel
                    {
                        Title = "Total Revenue",
                        Value = _dbContext.SALES.Where(s => !s.isDeleted).Sum(s => (decimal?)s.ProductSalesPrice * s.ProductSalesAmout) ?? 0,
                        Format = "currency",
                        TrendText = "Steady Momentum",
                        TrendIcon = "fa-signal",
                        IconClass = "fa-coins",
                        GradientClass = "bg-gradient-revenue"
                    },
                    new KpiViewModel
                    {
                        Title = "Active Customers",
                        Value = _dbContext.CUSTOMER.Count(c => !c.isDeleted),
                        Format = "int",
                        TrendText = "Community Rising",
                        TrendIcon = "fa-user-plus",
                        IconClass = "fa-users",
                        GradientClass = "bg-gradient-customers"
                    },
                    new KpiViewModel
                    {
                        Title = "Valued Suppliers",
                        Value = _dbContext.SUPPLIER.Count(s => !s.isDeleted),
                        Format = "int",
                        TrendText = "Strong Network",
                        TrendIcon = "fa-handshake",
                        IconClass = "fa-truck-fast",
                        GradientClass = "bg-gradient-suppliers"
                    }
                }
            };
            return View(model);
        }

        // Combined endpoint for recent sales and date-filtered sales
        [HttpGet]
        public JsonResult GetSalesTransactions(DateTime? startDate, DateTime? endDate)
        {
            var query = _dbContext.SALES.Where(s => !s.isDeleted);

            if (startDate.HasValue) query = query.Where(s => s.SalesDate >= startDate.Value);
            if (endDate.HasValue)
            {
                var endOfDay = endDate.Value.AddDays(1).AddTicks(-1);
                query = query.Where(s => s.SalesDate <= endOfDay);
            }

            // Join SALES with CUSTOMER and group by stable keys to avoid unsupported FirstOrDefault() in GroupBy projections
            var sales = query
                .Join(_dbContext.CUSTOMER,
                      sale => sale.CustomerID,
                      customer => customer.ID,
                      (sale, customer) => new { Sale = sale, Customer = customer })
                .GroupBy(x => new { x.Sale.TransactionId, x.Customer.ID, x.Customer.CustomerName })
                .Select(g => new
                {
                    TransactionId = g.Key.TransactionId,
                    LatestDate = g.Max(x => x.Sale.SalesDate),
                    CustomerId = g.Key.ID,
                    CustomerName = g.Key.CustomerName,
                    ItemCount = g.Sum(x => (int?)x.Sale.ProductSalesAmout) ?? 0,
                    // Ensure EF does decimal math by casting an operand to decimal?
                    TotalAmount = g.Sum(x => ((decimal?)x.Sale.ProductSalesPrice) * x.Sale.ProductSalesAmout) ?? 0m
                })
                .OrderByDescending(x => x.LatestDate)
                .Take(20)
                .ToList();

            var result = sales.Select(s => new {
                s.TransactionId,
                s.CustomerId,
                s.CustomerName,
                s.ItemCount,
                s.TotalAmount,
                Date = s.LatestDate
            });

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetProductAnalytics()
        {
            var lowStockThreshold = 10;

            var productStats = _dbContext.PRODUCT
                .Where(p => !p.isDeleted)
                .Select(p => new { p.StockAmount })
                .ToList();

            // FIX: Join SALES with PRODUCT to get access to ProductName before grouping
            var topSellingProducts = _dbContext.SALES
                .Where(s => !s.isDeleted)
                .Join(_dbContext.PRODUCT, // The table to join with
                      sale => sale.ProductID,     // Foreign key from SALES
                      product => product.ID,      // Primary key from PRODUCT
                      (sale, product) => new { Sale = sale, Product = product }) // Create a new temporary object
                .GroupBy(joined => new { joined.Product.ID, joined.Product.ProductName }) // Group by the product info
                .Select(g => new
                {
                    ProductName = g.Key.ProductName, // Access ProductName from the group key
                    TotalSold = g.Sum(x => (int?)x.Sale.ProductSalesAmout) ?? 0
                })
                .OrderByDescending(x => x.TotalSold)
                .Take(5)
                .ToList();

            var result = new
            {
                totalProducts = productStats.Count,
                lowStockProducts = productStats.Count(p => p.StockAmount > 0 && p.StockAmount <= lowStockThreshold),
                outOfStockProducts = productStats.Count(p => p.StockAmount <= 0),
                topSellingProducts
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetSalesTrend(string period = "daily")
        {
            var thirtyDaysAgo = DateTime.Now.Date.AddDays(-30);
            var query = _dbContext.SALES.Where(s => !s.isDeleted && s.SalesDate >= thirtyDaysAgo);

            var salesData = query.Select(s => new
            {
                s.SalesDate,
                s.TransactionId,
                Amount = s.ProductSalesPrice * s.ProductSalesAmout
            }).ToList();

            if (period.ToLower() == "weekly")
            {
                var weeklyData = salesData
                    .GroupBy(s => CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(s.SalesDate, CalendarWeekRule.FirstDay, DayOfWeek.Monday))
                    .Select(g => new
                    {
                        Date = g.Min(s => s.SalesDate),
                        TotalSales = g.Sum(s => (decimal?)s.Amount) ?? 0,
                        TransactionCount = g.Select(s => s.TransactionId).Distinct().Count()
                    })
                    .OrderBy(x => x.Date)
                    .ToList();

                return Json(new
                {
                    labels = weeklyData.Select(x => x.Date.ToString("MMM d")),
                    data = weeklyData.Select(x => new { sales = x.TotalSales, transactions = x.TransactionCount })
                }, JsonRequestBehavior.AllowGet);
            }
            else if (period.ToLower() == "monthly")
            {
                var monthlyData = salesData
                    .GroupBy(s => new { s.SalesDate.Year, s.SalesDate.Month })
                    .Select(g => new {
                        Date = new DateTime(g.Key.Year, g.Key.Month, 1),
                        TotalSales = g.Sum(s => (decimal?)s.Amount) ?? 0,
                        TransactionCount = g.Select(s => s.TransactionId).Distinct().Count()
                    })
                    .OrderBy(x => x.Date)
                    .ToList();

                return Json(new
                {
                    labels = monthlyData.Select(x => x.Date.ToString("MMM yyyy")),
                    data = monthlyData.Select(x => new { sales = x.TotalSales, transactions = x.TransactionCount })
                }, JsonRequestBehavior.AllowGet);
            }
            else // Daily (Default)
            {
                var dailyData = salesData
                    .GroupBy(s => s.SalesDate.Date)
                    .Select(g => new {
                        Date = g.Key,
                        TotalSales = g.Sum(s => (decimal?)s.Amount) ?? 0,
                        TransactionCount = g.Select(s => s.TransactionId).Distinct().Count()
                    })
                    .OrderBy(x => x.Date)
                    .ToList();

                return Json(new
                {
                    labels = dailyData.Select(x => x.Date.ToString("MMM d")),
                    data = dailyData.Select(x => new { sales = x.TotalSales, transactions = x.TransactionCount })
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult CustomerDetail(int id)
        {
            // Load customer basic info
            var customer = _dbContext.CUSTOMER
                .Where(c => !c.isDeleted && c.ID == id)
                .Select(c => new
                {
                    c.ID,
                    c.CustomerName,
                    c.Cust_Address,
                    c.Cust_Phone,
                    c.Notes,
                    c.Email,
                    c.baky
                })
                .FirstOrDefault();

            if (customer == null)
            {
                return HttpNotFound("Customer not found");
            }

            // Load sales history for the customer
            var sales = _dbContext.SALES
                .Where(s => !s.isDeleted && s.CustomerID == id)
                .OrderByDescending(s => s.SalesDate)
                .Select(s => new
                {
                    s.ID,
                    SalesID = s.ID,
                    s.TransactionId,
                    s.ProductID,
                    SalesAmount = s.ProductSalesAmout,
                    Price = s.ProductSalesPrice,
                    Madfou3 = (long?)(s.Madfou3 ?? 0),
                    Baky = (long?)(s.Baky ?? 0),
                    s.SalesDate
                })
                .ToList();

            var payload = new
            {
                Customer = customer,
                Sales = sales
            };

            ViewBag.CustomerJson = JsonConvert.SerializeObject(payload);
            return View(); // Will render Views/Admin/CustomerDetail.cshtml
        }

        private decimal CalculateTotalProfit()
        {
            // Compute profit in SQL using double to avoid decimal cast issues, then convert to decimal in memory
            var salesProfitDouble = _dbContext.SALES
                .Where(s => !s.isDeleted)
                .Join(_dbContext.PRODUCT,
                      sale => sale.ProductID, product => product.ID,
                      (sale, product) =>
                          ((double)sale.ProductSalesPrice
                           - (double)product.Price
                           - (sale.MaxDiscount.HasValue ? (double)sale.MaxDiscount.Value : 0.0))
                          * (double)sale.ProductSalesAmout)
                .DefaultIfEmpty(0.0)
                .Sum();

            decimal salesProfit = (decimal)salesProfitDouble;

            decimal totalExpenses = _dbContext.EXPENSES.Select(e => (decimal?)e.Amount).DefaultIfEmpty(0m).Sum() ?? 0m;
            return salesProfit - totalExpenses;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) { _dbContext.Dispose(); }
            base.Dispose(disposing);
        }
    }
}
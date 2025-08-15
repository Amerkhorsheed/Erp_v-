using Erp_V1.DAL.DAL;
using Erp.WebApp.Services;
using Erp.WebApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Erp.WebApp.Controllers
{
    // A private helper class to hold strongly-typed query results, avoiding 'dynamic' issues.
    internal class InvoiceDetailData
    {
        public SALES Sale { get; set; }
        public CUSTOMER Customer { get; set; }
        public PRODUCT Product { get; set; }
    }

    [CustomAuthorize] // Ensures only authenticated users can access these actions.
    public class InvoiceController : Controller
    {
        private readonly IInvoicePdfService _invoicePdfService;
        private readonly IEmailService _emailService;
        private readonly erp_v2Entities _dbContext = new erp_v2Entities();

        // Services are injected by the Unity DI container.
        public InvoiceController(IInvoicePdfService invoicePdfService, IEmailService emailService)
        {
            _invoicePdfService = invoicePdfService;
            _emailService = emailService;
        }

        /// <summary>
        /// Displays the main invoice page.
        /// URL: /Invoice/Index?ids=1,2,3
        /// </summary>
        public ActionResult Index(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                // If no IDs are provided, show an error view.
                return View("Error", new HandleErrorInfo(new ArgumentException("No invoice ID was provided."), "Invoice", "Index"));
            }
            ViewBag.SaleIds = ids;
            return View();
        }

        /// <summary>
        /// Provides invoice data as JSON for the view's JavaScript to fetch.
        /// URL: /Invoice/GetInvoiceData?ids=1,2,3
        /// </summary>
        public async Task<JsonResult> GetInvoiceData(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                Response.StatusCode = 400; // Bad Request
                return Json(new { message = "No IDs were provided." }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                var saleIds = ids.Split(',').Select(int.Parse).ToList();

                // Retry logic to handle potential database commit delays.
                List<InvoiceDetailData> salesWithDetails = null;
                for (int i = 0; i < 3; i++) // Retry up to 3 times
                {
                    salesWithDetails = await (from s in _dbContext.SALES
                                              join c in _dbContext.CUSTOMER on s.CustomerID equals c.ID
                                              join p in _dbContext.PRODUCT on s.ProductID equals p.ID
                                              where saleIds.Contains(s.ID)
                                              select new InvoiceDetailData { Sale = s, Customer = c, Product = p })
                                              .ToListAsync();

                    if (salesWithDetails.Any() && salesWithDetails.Count == saleIds.Count)
                    {
                        break; // Success: all records found.
                    }
                    await Task.Delay(200); // Wait 200ms before retrying.
                }

                if (salesWithDetails == null || !salesWithDetails.Any())
                {
                    Response.StatusCode = 404; // Not Found
                    return Json(new { message = $"Error: No sales records found for the provided IDs ({ids}). The record might still be processing." }, JsonRequestBehavior.AllowGet);
                }

                var firstDetail = salesWithDetails.First();
                var items = salesWithDetails.Select(d => new {
                    productName = d.Product.ProductName,
                    quantity = d.Sale.ProductSalesAmout,
                    unitPrice = d.Sale.ProductSalesPrice,
                    discount = d.Sale.MaxDiscount ?? 0,
                    lineTotal = (d.Sale.ProductSalesPrice * d.Sale.ProductSalesAmout) - (d.Sale.MaxDiscount ?? 0)
                }).ToList();

                var grandTotal = items.Sum(i => i.lineTotal);

                var viewModel = new
                {
                    customerName = firstDetail.Customer.CustomerName,
                    customerAddress = firstDetail.Customer.Cust_Address,
                    customerPhone = firstDetail.Customer.Cust_Phone,
                    saleDate = firstDetail.Sale.SalesDate.ToString("o"), // ISO 8601 format for JavaScript
                    items = items,
                    grandTotal = grandTotal
                };
                return Json(viewModel, JsonRequestBehavior.AllowGet);
            }
            catch (FormatException)
            {
                Response.StatusCode = 400; // Bad Request
                return Json(new { message = "The provided IDs are not in a valid format." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500; // Internal Server Error
                return Json(new { message = "An unexpected server error occurred: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Generates the PDF invoice and emails it to the customer.
        /// URL: /Invoice/EmailInvoice?ids=1,2,3
        /// </summary>
        [HttpPost]
        public async Task<JsonResult> EmailInvoice(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return Json(new { success = false, message = "No IDs were provided." });
            }

            try
            {
                var saleIds = ids.Split(',').Select(int.Parse).ToList();
                var customer = await (from s in _dbContext.SALES
                                      join c in _dbContext.CUSTOMER on s.CustomerID equals c.ID
                                      where saleIds.Contains(s.ID)
                                      select c).FirstOrDefaultAsync();

                if (customer == null || string.IsNullOrEmpty(customer.Email))
                {
                    return Json(new { success = false, message = "Customer email could not be found." });
                }

                // This is the call that could crash if data is null.
                byte[] pdfBytes = await _invoicePdfService.GenerateInvoicePdfAsync(saleIds);

                // Send the email with the generated PDF.
                bool emailSent = await _emailService.SendInvoiceEmailAsync(
                    customer.Email, customer.CustomerName, saleIds.First(), pdfBytes);

                if (emailSent)
                {
                    return Json(new { success = true, message = "Invoice has been sent to the customer's email." });
                }
                else
                {
                    return Json(new { success = false, message = "The email service failed to send the invoice." });
                }
            }
            catch (Exception ex)
            {
                // This catch block is crucial. It prevents the app from crashing and returning
                // an HTML error page, which causes the "Unexpected token '<'" error in JavaScript.
                Response.StatusCode = 500; // Internal Server Error
                return Json(new { success = false, message = "An error occurred on the server: " + ex.Message });
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
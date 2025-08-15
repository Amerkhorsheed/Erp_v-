using Erp.WebApp.Services;
using Erp.WebApp.Services.Interfaces;
using Erp.WebApp.ViewModels;
using Erp_V1.DAL.DAL;
using Erp_V1.DAL.DTO;
using Erp_V1.BLL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Erp.WebApp.Controllers
{
    [CustomAuthorize(Roles = "Seller,Admin")]
    public class PartialPaymentController : Controller
    {
        private readonly erp_v2Entities _context;
        private readonly IInvoicePdfService _invoicePdfService;
        private readonly IEmailService _emailService;
        private readonly IApiService _apiService;
        private readonly IAuthService _authService;
        private readonly SalesBLL _salesBll;

        public PartialPaymentController(IInvoicePdfService invoicePdfService, IEmailService emailService, IApiService apiService, IAuthService authService)
        {
            _context = new erp_v2Entities();
            _invoicePdfService = invoicePdfService;
            _emailService = emailService;
            _apiService = apiService;
            _authService = authService;
            _salesBll = new SalesBLL();
        }

        [HttpPost]
        public async Task<JsonResult> EmailInvoice(List<int> saleIds, string customerEmail)
        {
            try
            {
                if (saleIds == null || !saleIds.Any())
                {
                    return Json(new { success = false, message = "No sale IDs provided." });
                }

                if (string.IsNullOrEmpty(customerEmail))
                {
                    return Json(new { success = false, message = "Customer email is required." });
                }

                // Get customer information
                var firstSaleId = saleIds.First();
                var sale = await _context.SALES.FirstOrDefaultAsync(s => s.ID == firstSaleId);
                if (sale == null)
                {
                    return Json(new { success = false, message = "Sale not found." });
                }

                var customer = await _context.CUSTOMER.FirstOrDefaultAsync(c => c.ID == sale.CustomerID);
                if (customer == null)
                {
                    return Json(new { success = false, message = "Customer not found." });
                }

                // Generate PDF invoice
                var pdfBytes = await _invoicePdfService.GenerateInvoicePdfAsync(saleIds);

                // Send email with invoice attachment
                var emailSent = await _emailService.SendInvoiceEmailAsync(customerEmail, customer.CustomerName, firstSaleId, pdfBytes);

                if (emailSent)
                {
                    return Json(new { success = true, message = "Invoice emailed successfully!" });
                }
                else
                {
                    return Json(new { success = false, message = "Failed to send email. Please try again." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error sending email: {ex.Message}" });
            }
        }

        // Parameterless constructor for compatibility
        public PartialPaymentController() : this(new Erp.WebApp.Services.InvoicePdfService(new erp_v2Entities()), new Erp.WebApp.Services.EmailService(), null, null)
        {
            _salesBll = new SalesBLL();
        }

        public ActionResult Index()
        {
            var viewModel = new PartialPaymentSaleViewModel();

            viewModel.Customers = _context.CUSTOMER
                .Where(c => c.isDeleted == false)
                .Select(c => new CustomerDetailDTO
                {
                    ID = c.ID,
                    CustomerName = c.CustomerName,
                    Email = c.Email,
                    Cust_Phone = c.Cust_Phone
                }).ToList();

            viewModel.Products = _context.PRODUCT
                .Where(p => p.isDeleted == false && p.StockAmount > 0)
                .Select(p => new ProductDetailDTO
                {
                    ProductID = p.ID,
                    ProductName = p.ProductName,
                    stockAmount = p.StockAmount,
                    Sale_Price = p.Sale_Price ?? 0,
                    price = p.Price,
                    MaxDiscount = p.MaxDiscount ?? 0
                }).ToList();

            ViewBag.CustomersJson = JsonConvert.SerializeObject(viewModel.Customers);
            ViewBag.ProductsJson = JsonConvert.SerializeObject(viewModel.Products);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> CreatePartialPaymentSale(PartialPaymentSaleRequest saleData)
        {
            if (saleData == null || saleData.Items == null || !saleData.Items.Any())
            {
                Response.StatusCode = 400;
                return Json(new { success = false, message = "Invalid sale data provided." }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                // Generate a unique transaction ID for all items in this sale
                var transactionId = Guid.NewGuid().ToString();
                var totalSaleAmount = saleData.Items.Sum(item => (decimal)item.Quantity * item.UnitPrice - item.Discount);
                
                // Calculate payment amounts based on payment type
                decimal paidAmount = 0;
                decimal remainingAmount = 0;
                
                switch (saleData.PaymentType)
                {
                    case "full":
                        paidAmount = totalSaleAmount;
                        remainingAmount = 0;
                        break;
                    case "partial":
                        paidAmount = saleData.PaidAmount ?? 0;
                        remainingAmount = totalSaleAmount - paidAmount;
                        break;
                    case "none":
                        paidAmount = 0;
                        remainingAmount = totalSaleAmount;
                        break;
                }

                // Convert to SalesDetailDTO format for BLL
                var cartDtos = saleData.Items.Select(item => {
                    var itemTotal = (decimal)item.Quantity * item.UnitPrice - item.Discount;
                    var itemPaidAmount = totalSaleAmount > 0 ? Math.Round(paidAmount * itemTotal / totalSaleAmount, 2) : 0;
                    var itemRemainingAmount = totalSaleAmount > 0 ? Math.Round(remainingAmount * itemTotal / totalSaleAmount, 2) : itemTotal;
                    
                    return new SalesDetailDTO
                    {
                        ProductID = item.ProductId,
                        CustomerID = saleData.CustomerId,
                        SalesAmount = item.Quantity,
                        Price = (int)item.UnitPrice,
                        MaxDiscount = (float)item.Discount,
                        SalesDate = DateTime.Now,
                        Madfou3 = (long)itemPaidAmount,
                        Baky = (long)itemRemainingAmount
                    };
                }).ToList();

                // Use BLL to insert transaction with all items atomically
                var saleIds = _salesBll.InsertTransactionWithItems(cartDtos, transactionId);

                // Get customer info for WhatsApp and email
                var customer = _context.CUSTOMER.FirstOrDefault(c => c.ID == saleData.CustomerId);
                var whatsAppUrl = "";
                
                if (customer != null && !string.IsNullOrEmpty(customer.Cust_Phone))
                {
                    var message = $"Hello {customer.CustomerName}, your order has been processed successfully. Transaction ID: {transactionId}. Total Amount: ${totalSaleAmount:F2}";
                    if (saleData.PaymentType == "partial")
                    {
                        message += $", Paid: ${paidAmount:F2}, Remaining: ${remainingAmount:F2}";
                    }
                    else if (saleData.PaymentType == "none")
                    {
                        message += $", Payment Pending: ${remainingAmount:F2}";
                    }
                    
                    whatsAppUrl = $"https://wa.me/{customer.Cust_Phone.Replace("+", "").Replace(" ", "")}?text={Uri.EscapeDataString(message)}";
                }

                // Send email invoice if customer has email
                if (customer != null && !string.IsNullOrEmpty(customer.Email) && _emailService != null)
                {
                    try
                    {
                        var invoiceData = new
                        {
                            TransactionId = transactionId,
                            CustomerName = customer.CustomerName,
                            Items = saleData.Items,
                            TotalAmount = totalSaleAmount,
                            PaidAmount = paidAmount,
                            RemainingAmount = remainingAmount,
                            PaymentType = saleData.PaymentType,
                            SaleDate = DateTime.Today
                        };
                        
                        // Generate PDF for partial payment invoice
                         var pdfBytes = await _invoicePdfService.GenerateInvoicePdfAsync(saleIds);
                         
                         // Send invoice email with PDF attachment
                         await _emailService.SendInvoiceEmailAsync(customer.Email, customer.CustomerName, saleIds.First(), pdfBytes);
                    }
                    catch (Exception emailEx)
                    {
                        // Log email error but don't fail the sale
                        System.Diagnostics.Debug.WriteLine($"Email sending failed: {emailEx.Message}");
                    }
                }

                var responseMessage = "Sale completed successfully!";
                if (saleData.PaymentType == "partial")
                {
                    responseMessage = $"Partial payment sale completed! Paid: ${paidAmount:F2}, Remaining: ${remainingAmount:F2}";
                }
                else if (saleData.PaymentType == "none")
                {
                    responseMessage = $"Sale recorded with no payment. Total pending: ${remainingAmount:F2}";
                }

                return Json(new
                {
                    success = true,
                    message = responseMessage,
                    saleIds = saleIds,
                    whatsAppUrl = whatsAppUrl,
                    transactionId = transactionId,
                    totalAmount = totalSaleAmount,
                    paidAmount = paidAmount,
                    remainingAmount = remainingAmount,
                    customerEmail = customer?.Email
                });
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(new
                {
                    success = false,
                    message = $"An error occurred while processing the sale: {ex.Message}"
                }, JsonRequestBehavior.AllowGet);
            }
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    public class PartialPaymentSaleRequest
    {
        public int CustomerId { get; set; }
        public List<PartialPaymentSaleItem> Items { get; set; }
        public string PaymentType { get; set; } // "full", "partial", "none"
        public decimal? PaidAmount { get; set; }
    }

    public class PartialPaymentSaleItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
    }
}
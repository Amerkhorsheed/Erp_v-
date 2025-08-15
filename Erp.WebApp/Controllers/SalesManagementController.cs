using Erp.WebApp.Services;
using Erp.WebApp.Services.Interfaces;
using Erp_V1.DAL.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Erp.WebApp.Controllers
{
    [CustomAuthorize(Roles = "Seller,Admin")]
    public class SalesManagementController : Controller
    {
        private readonly erp_v2Entities _context = new erp_v2Entities();
        private readonly IInvoicePdfService _invoicePdfService;
        private readonly IEmailService _emailService;
        private readonly ISalesManagementEmailService _salesEmailService;

        // Construtor para inje��o de depend�ncia
        public SalesManagementController(IInvoicePdfService invoicePdfService, IEmailService emailService, ISalesManagementEmailService salesEmailService)
        {
            _invoicePdfService = invoicePdfService;
            _emailService = emailService;
            _salesEmailService = salesEmailService;
        }

        // Action principal que retorna a view do dashboard
        public ActionResult Index()
        {
            return View();
        }

        // Endpoint para buscar os dados dos KPIs
        [HttpGet]
        public async Task<JsonResult> GetKPIs()
        {
            try
            {
                var sales = await _context.SALES.Where(s => !s.isDeleted).ToListAsync();

                if (!sales.Any())
                {
                    return Json(new { success = true, data = new { totalSales = 0, totalRevenue = 0, paidSales = 0, paymentRate = 0, partialPaymentSales = 0, partialPaymentRate = 0, unpaidSales = 0, unpaidRate = 0 } }, JsonRequestBehavior.AllowGet);
                }

                var totalSales = sales.Count;
                var totalRevenue = sales.Sum(s => s.Total ?? 0);
                var paidSales = sales.Count(s => s.Baky == 0);
                var partialPaymentSales = sales.Count(s => s.Madfou3 > 0 && s.Baky > 0);
                var unpaidSales = sales.Count(s => s.Madfou3 == 0);

                var kpiData = new
                {
                    totalSales = totalSales,
                    totalRevenue = totalRevenue,
                    paidSales = paidSales,
                    paymentRate = totalSales > 0 ? (int)Math.Round((double)paidSales / totalSales * 100) : 0,
                    partialPaymentSales = partialPaymentSales,
                    partialPaymentRate = totalSales > 0 ? (int)Math.Round((double)partialPaymentSales / totalSales * 100) : 0,
                    unpaidSales = unpaidSales,
                    unpaidRate = totalSales > 0 ? (int)Math.Round((double)unpaidSales / totalSales * 100) : 0
                };

                return Json(new { success = true, data = kpiData }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error loading KPIs: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // Endpoint para buscar dados de vendas com filtro e pagina��o
        [HttpGet]
        public async Task<JsonResult> GetSalesData(string searchTerm, string paymentStatus, string startDate, string endDate, int page = 1, int pageSize = 10)
        {
            try
            {
                var query = _context.SALES
                    .Where(s => !s.isDeleted)
                    .Join(_context.CUSTOMER, s => s.CustomerID, c => c.ID, (s, c) => new { Sale = s, Customer = c })
                    .Join(_context.PRODUCT, sc => sc.Sale.ProductID, p => p.ID, (sc, p) => new { sc.Sale, sc.Customer, Product = p });

                // Filtro de pesquisa
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    query = query.Where(x => x.Customer.CustomerName.Contains(searchTerm) ||
                                             x.Product.ProductName.Contains(searchTerm) ||
                                             (x.Sale.TransactionId ?? x.Sale.ID.ToString()).Contains(searchTerm));
                }

                // Filtro de status de pagamento
                if (!string.IsNullOrEmpty(paymentStatus))
                {
                    if (paymentStatus == "paid") query = query.Where(x => x.Sale.Baky == 0);
                    else if (paymentStatus == "partial") query = query.Where(x => x.Sale.Madfou3 > 0 && x.Sale.Baky > 0);
                    else if (paymentStatus == "unpaid") query = query.Where(x => x.Sale.Madfou3 == 0);
                }

                // Filtro de data
                if (DateTime.TryParse(startDate, out var start))
                {
                    query = query.Where(x => x.Sale.SalesDate >= start);
                }
                if (DateTime.TryParse(endDate, out var end))
                {
                    end = end.AddDays(1); // Inclui o dia todo
                    query = query.Where(x => x.Sale.SalesDate < end);
                }

                var totalRecords = await query.CountAsync();
                var rawSalesData = await query
                    .OrderByDescending(x => x.Sale.SalesDate)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(x => new
                    {
                        TransactionId = x.Sale.TransactionId,
                        SaleId = x.Sale.ID,
                        CustomerName = x.Customer.CustomerName,
                        ProductName = x.Product.ProductName,
                        SalesAmount = x.Sale.ProductSalesAmout,
                        Price = x.Sale.ProductSalesPrice,
                        TotalAmount = x.Sale.Total ?? 0,
                        Madfou3 = x.Sale.Madfou3 ?? 0,
                        Baky = x.Sale.Baky ?? 0,
                        SalesDate = x.Sale.SalesDate
                    })
                    .ToListAsync();

                // Format data after database query execution
                var salesData = rawSalesData.Select(x => new
                {
                    TransactionId = x.TransactionId ?? x.SaleId.ToString(),
                    CustomerName = x.CustomerName,
                    ProductName = x.ProductName,
                    SalesAmount = x.SalesAmount,
                    Price = x.Price,
                    TotalAmount = x.TotalAmount,
                    Madfou3 = x.Madfou3,
                    Baky = x.Baky,
                    PaymentStatus = (x.Baky == 0) ? "Fully Paid" : ((x.Madfou3 > 0) ? "Partially Paid" : "Unpaid"),
                    PaymentPercentage = (x.TotalAmount > 0) ? (int)(x.Madfou3 * 100 / x.TotalAmount) : 0,
                    SalesDate = x.SalesDate.ToString("yyyy-MM-ddTHH:mm:ss")
                }).ToList();

                return Json(new
                {
                    success = true,
                    data = salesData,
                    currentPage = page,
                    totalPages = (int)Math.Ceiling((double)totalRecords / pageSize),
                    totalRecords = totalRecords
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error loading sales data: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // Endpoint para processar um pagamento
        [HttpPost]
        public async Task<JsonResult> ProcessPayment(string[] saleIds, decimal paymentAmount)
        {
            if (saleIds == null || !saleIds.Any() || paymentAmount <= 0)
            {
                return Json(new { success = false, message = "Invalid data provided." });
            }

            try
            {
                var actualSaleIds = new List<int>();
                
                foreach (var saleId in saleIds)
                {
                    if (int.TryParse(saleId, out int id))
                    {
                        actualSaleIds.Add(id);
                    }
                    else
                    {
                        // Handle TransactionId - find all sales with this TransactionId
                        var salesWithTransactionId = await _context.SALES
                            .Where(s => s.TransactionId == saleId && !s.isDeleted)
                            .Select(s => s.ID)
                            .ToListAsync();
                        actualSaleIds.AddRange(salesWithTransactionId);
                    }
                }
                
                var salesToUpdate = await _context.SALES.Where(s => actualSaleIds.Contains(s.ID) && s.Baky > 0).ToListAsync();
                if (!salesToUpdate.Any())
                {
                    return Json(new { success = false, message = "No unpaid or partially paid sales found for the given IDs." });
                }

                var remainingToPay = paymentAmount;
                foreach (var sale in salesToUpdate.OrderBy(s => s.SalesDate))
                {
                    if (remainingToPay <= 0) break;

                    var paymentForThisSale = Math.Min(remainingToPay, (decimal)sale.Baky);
                    sale.Madfou3 += (long)paymentForThisSale;
                    sale.Baky -= (long)paymentForThisSale;
                    remainingToPay -= paymentForThisSale;
                }

                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Payment processed successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error processing payment: " + ex.Message });
            }
        }

        // Endpoint para enviar confirma��o de pagamento por e-mail

        [HttpPost]
        public async Task<JsonResult> SendPaymentConfirmation(string[] saleIds, string customerEmail)
        {
            if (saleIds == null || !saleIds.Any() || string.IsNullOrEmpty(customerEmail))
            {
                return Json(new { success = false, message = "Invalid data provided. Please select sales and provide an email." });
            }

            try
            {
                var actualSaleIds = new List<int>();
                
                foreach (var saleId in saleIds)
                {
                    if (int.TryParse(saleId, out int id))
                    {
                        actualSaleIds.Add(id);
                    }
                    else
                    {
                        // Handle TransactionId - find all sales with this TransactionId
                        var salesWithTransactionId = await _context.SALES
                            .Where(s => s.TransactionId == saleId && !s.isDeleted)
                            .Select(s => s.ID)
                            .ToListAsync();
                        actualSaleIds.AddRange(salesWithTransactionId);
                    }
                }
                
                var firstSaleId = actualSaleIds.First();

                // --- FIX: Step 1 ---
                // Fetch the sale first without including the non-existent navigation property.
                var firstSale = await _context.SALES.FirstOrDefaultAsync(s => s.ID == firstSaleId);
                if (firstSale == null)
                {
                    return Json(new { success = false, message = "Sale not found." });
                }

                // --- FIX: Step 2 ---
                // Now, fetch the customer separately using the CustomerID from the sale object.
                var customer = await _context.CUSTOMER.FirstOrDefaultAsync(c => c.ID == firstSale.CustomerID);
                if (customer == null)
                {
                    return Json(new { success = false, message = "Customer associated with the sale could not be found." });
                }

                // Generate the PDF invoice
                var pdfBytes = await _invoicePdfService.GenerateInvoicePdfAsync(actualSaleIds);

                // --- FIX: Step 3 ---
                // Use the customer object we fetched to get the customer's name.
                var emailSent = await _emailService.SendInvoiceEmailAsync(customerEmail, customer.CustomerName, firstSale.ID, pdfBytes);

                if (emailSent)
                {
                    return Json(new { success = true, message = "Payment confirmation email sent successfully!" });
                }
                else
                {
                    return Json(new { success = false, message = "Failed to send email." });
                }
            }
            catch (Exception ex)
            {
                // Return a generic server error message
                return Json(new { success = false, message = $"An error occurred while sending the email: {ex.Message}" });
            }
        }

        // POST: SalesManagement/GenerateInvoicePreview
        [HttpPost]
        public async Task<JsonResult> GenerateInvoicePreview(string[] saleIds)
        {
            try
            {
                if (saleIds == null || saleIds.Length == 0)
                {
                    return Json(new { success = false, message = "No sales selected." });
                }

                var actualSaleIds = new List<int>();
                
                foreach (var saleId in saleIds)
                {
                    if (int.TryParse(saleId, out int id))
                    {
                        actualSaleIds.Add(id);
                    }
                    else
                    {
                        // Handle TransactionId - find all sales with this TransactionId
                        var salesWithTransactionId = await _context.SALES
                            .Where(s => s.TransactionId == saleId && !s.isDeleted)
                            .Select(s => s.ID)
                            .ToListAsync();
                        actualSaleIds.AddRange(salesWithTransactionId);
                    }
                }

                var sales = await _context.SALES
                    .Where(s => actualSaleIds.Contains(s.ID) && !s.isDeleted)
                    .ToListAsync();

                if (!sales.Any())
                {
                    return Json(new { success = false, message = "Selected sales not found." });
                }

                // Generate invoice HTML preview
                var invoiceHtml = await GenerateInvoiceHtmlAsync(sales);

                return Json(new { success = true, invoiceHtml = invoiceHtml });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error generating invoice preview: {ex.Message}" });
            }
        }

        // POST: SalesManagement/SendInvoiceEmail
        [HttpPost]
        public async Task<JsonResult> SendInvoiceEmail(string[] saleIds, string customerEmail, string emailSubject, string additionalMessage = "", bool includePaymentLink = true, bool attachPDF = true)
        {
            try
            {
                if (saleIds == null || saleIds.Length == 0)
                {
                    return Json(new { success = false, message = "No sales selected." });
                }
                
                var actualSaleIds = new List<int>();
                
                foreach (var saleId in saleIds)
                {
                    if (int.TryParse(saleId, out int id))
                    {
                        actualSaleIds.Add(id);
                    }
                    else
                    {
                        // Handle TransactionId - find all sales with this TransactionId
                        var salesWithTransactionId = await _context.SALES
                            .Where(s => s.TransactionId == saleId && !s.isDeleted)
                            .Select(s => s.ID)
                            .ToListAsync();
                        actualSaleIds.AddRange(salesWithTransactionId);
                    }
                }

                if (string.IsNullOrEmpty(customerEmail))
                {
                    return Json(new { success = false, message = "Customer email is required." });
                }

                if (string.IsNullOrEmpty(emailSubject))
                {
                    return Json(new { success = false, message = "Email subject is required." });
                }

                var sales = await _context.SALES
                    .Where(s => actualSaleIds.Contains(s.ID) && !s.isDeleted)
                    .ToListAsync();

                // Get related customer and product data
                var customerIds = sales.Select(s => s.CustomerID).Distinct().ToList();
                var productIds = sales.Select(s => s.ProductID).Distinct().ToList();
                
                var customers = await _context.CUSTOMER
                    .Where(c => customerIds.Contains(c.ID) && !c.isDeleted)
                    .ToListAsync();
                    
                var products = await _context.PRODUCT
                    .Where(p => productIds.Contains(p.ID) && !p.isDeleted)
                    .ToListAsync();

                if (!sales.Any())
                {
                    return Json(new { success = false, message = "Selected sales not found." });
                }

                // Generate email body
                var emailBody = await GenerateInvoiceEmailBodyAsync(sales, emailSubject, additionalMessage, includePaymentLink);
                
                byte[] pdfAttachment = null;
                if (attachPDF && _invoicePdfService != null)
                {
                    try
                    {
                        pdfAttachment = await _invoicePdfService.GenerateInvoicePdfAsync(actualSaleIds);
                    }
                    catch (Exception pdfEx)
                    {
                        // Log PDF generation error but continue with email
                        System.Diagnostics.Debug.WriteLine($"PDF generation failed: {pdfEx.Message}");
                    }
                }

                // Send email using SalesManagementEmailService
                if (_salesEmailService != null)
                {
                    var firstSale = sales.FirstOrDefault();
                    var customer = firstSale != null ? 
                        await _context.CUSTOMER.FirstOrDefaultAsync(c => c.ID == firstSale.CustomerID && !c.isDeleted) : null;
                    var customerName = customer?.CustomerName ?? "Valued Customer";
                    
                    var emailSent = await _salesEmailService.SendInvoiceEmailAsync(
                        customerEmail, 
                        customerName, 
                        sales.FirstOrDefault()?.ID ?? 0, 
                        pdfAttachment,
                        additionalMessage,
                        includePaymentLink
                    );

                    if (emailSent)
                    {
                        return Json(new { success = true, message = "Invoice email sent successfully with Sales Management portal link!" });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Failed to send email. Please check email configuration." });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Sales Management email service is not available." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error sending invoice email: {ex.Message}" });
            }
        }

        private async Task<string> GenerateInvoiceHtmlAsync(System.Collections.Generic.List<SALES> sales)
        {
            if (!sales.Any()) return "<p>No sales data available.</p>";

            // Get customer info for the first sale
            var firstSale = sales.FirstOrDefault();
            var customer = firstSale != null ? 
                await _context.CUSTOMER.FirstOrDefaultAsync(c => c.ID == firstSale.CustomerID && !c.isDeleted) : null;
            
            // Get all products for the sales
            var productIds = sales.Select(s => s.ProductID).Distinct().ToList();
            var products = await _context.PRODUCT
                .Where(p => productIds.Contains(p.ID) && !p.isDeleted)
                .ToListAsync();
            
            var totalAmount = sales.Sum(s => s.Total ?? 0);
            var totalPaid = sales.Sum(s => s.Madfou3 ?? 0);
            var totalOutstanding = sales.Sum(s => s.Baky ?? 0);

            var html = $@"
                <div class='invoice-preview'>
                    <div class='invoice-header text-center mb-4'>
                        <h3 class='text-primary'><i class='fas fa-file-invoice me-2'></i>Invoice</h3>
                        <p class='text-muted'>Generated on {DateTime.Now:MMMM dd, yyyy}</p>
                    </div>
                    
                    <div class='row mb-4'>
                        <div class='col-md-6'>
                            <h5>Bill To:</h5>
                            <p><strong>{customer?.CustomerName ?? "N/A"}</strong><br>
                            Email: {customer?.Email ?? "N/A"}<br>
                            Phone: {customer?.Cust_Phone ?? "N/A"}</p>
                        </div>
                        <div class='col-md-6 text-end'>
                            <h5>Invoice Details:</h5>
                            <p>Transaction IDs: {string.Join(", ", sales.Select(s => s.TransactionId ?? s.ID.ToString()))}<br>
                            Date Range: {sales.Min(s => s.SalesDate):MM/dd/yyyy} - {sales.Max(s => s.SalesDate):MM/dd/yyyy}</p>
                        </div>
                    </div>
                    
                    <table class='table table-striped'>
                        <thead class='table-dark'>
                            <tr>
                                <th>Transaction ID</th>
                                <th>Product</th>
                                <th>Quantity</th>
                                <th>Unit Price</th>
                                <th>Total</th>
                                <th>Paid</th>
                                <th>Outstanding</th>
                            </tr>
                        </thead>
                        <tbody>";

            foreach (var sale in sales)
            {
                html += $@"
                    <tr>
                        <td>#{sale.TransactionId ?? sale.ID.ToString()}</td>
                        <td>{products.FirstOrDefault(p => p.ID == sale.ProductID)?.ProductName ?? "N/A"}</td>
                        <td>{sale.ProductSalesAmout}</td>
                        <td>${sale.ProductSalesPrice:F2}</td>
                        <td><strong>${sale.Total ?? 0:F2}</strong></td>
                        <td>${sale.Madfou3 ?? 0:F2}</td>
                        <td class='{(sale.Baky > 0 ? "text-danger" : "text-success")}'>${sale.Baky ?? 0:F2}</td>
                    </tr>";
            }

            html += $@"
                        </tbody>
                        <tfoot class='table-dark'>
                            <tr>
                                <th colspan='4'>Totals:</th>
                                <th>${totalAmount:F2}</th>
                                <th>${totalPaid:F2}</th>
                                <th class='{(totalOutstanding > 0 ? "text-danger" : "text-success")}'>${totalOutstanding:F2}</th>
                            </tr>
                        </tfoot>
                    </table>
                    
                    <div class='row mt-4'>
                        <div class='col-md-6'>
                            <div class='alert alert-info'>
                                <h6><i class='fas fa-info-circle me-2'></i>Payment Summary</h6>
                                <p class='mb-1'>Total Amount: <strong>${totalAmount:F2}</strong></p>
                                <p class='mb-1'>Amount Paid: <strong>${totalPaid:F2}</strong></p>
                                <p class='mb-0'>Outstanding Balance: <strong class='{(totalOutstanding > 0 ? "text-danger" : "text-success")}'>${totalOutstanding:F2}</strong></p>
                            </div>
                        </div>
                        <div class='col-md-6'>
                            <div class='alert {(totalOutstanding > 0 ? "alert-warning" : "alert-success")}'>
                                <h6><i class='fas fa-{(totalOutstanding > 0 ? "exclamation-triangle" : "check-circle")} me-2'></i>Status</h6>
                                <p class='mb-0'>{(totalOutstanding > 0 ? $"Payment Required: ${totalOutstanding:F2}" : "Fully Paid")}</p>
                            </div>
                        </div>
                    </div>
                </div>";

            return html;
        }

        private async Task<string> GenerateInvoiceEmailBodyAsync(System.Collections.Generic.List<SALES> sales, string emailSubject, string additionalMessage, bool includePaymentLink)
        {
            if (!sales.Any()) return "No sales data available.";

            // Get customer info for the first sale
            var firstSale = sales.FirstOrDefault();
            var customer = firstSale != null ? 
                await _context.CUSTOMER.FirstOrDefaultAsync(c => c.ID == firstSale.CustomerID && !c.isDeleted) : null;
            
            // Get all products for the sales
            var productIds = sales.Select(s => s.ProductID).Distinct().ToList();
            var products = await _context.PRODUCT
                .Where(p => productIds.Contains(p.ID) && !p.isDeleted)
                .ToListAsync();
            
            var totalAmount = sales.Sum(s => s.Total ?? 0);
            var totalPaid = sales.Sum(s => s.Madfou3 ?? 0);
            var totalOutstanding = sales.Sum(s => s.Baky ?? 0);

            var emailBody = $@"
                <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto;'>
                    <div style='background: linear-gradient(135deg, #667eea 0%, #764ba2 100%); color: white; padding: 20px; text-align: center;'>
                        <h2>Invoice from Your ERP System</h2>
                        <p>Thank you for your business!</p>
                    </div>
                    
                    <div style='padding: 20px; background: #f8f9fa;'>
                        <h3>Dear {customer?.CustomerName ?? "Valued Customer"},</h3>
                        <p>Please find below the details of your recent purchase(s):</p>
                        
                        {(!string.IsNullOrEmpty(additionalMessage) ? $"<div style='background: #e3f2fd; padding: 15px; border-left: 4px solid #2196f3; margin: 15px 0;'><p><strong>Message:</strong> {additionalMessage}</p></div>" : "")}
                        
                        <table style='width: 100%; border-collapse: collapse; margin: 20px 0;'>
                            <thead>
                                <tr style='background: #343a40; color: white;'>
                                    <th style='padding: 10px; border: 1px solid #ddd;'>Sale ID</th>
                                    <th style='padding: 10px; border: 1px solid #ddd;'>Product</th>
                                    <th style='padding: 10px; border: 1px solid #ddd;'>Quantity</th>
                                    <th style='padding: 10px; border: 1px solid #ddd;'>Total</th>
                                    <th style='padding: 10px; border: 1px solid #ddd;'>Outstanding</th>
                                </tr>
                            </thead>
                            <tbody>";

            foreach (var sale in sales)
            {
                emailBody += $@"
                    <tr>
                        <td style='padding: 10px; border: 1px solid #ddd;'>#{sale.ID}</td>
                        <td style='padding: 10px; border: 1px solid #ddd;'>{products.FirstOrDefault(p => p.ID == sale.ProductID)?.ProductName ?? "N/A"}</td>
                         <td style='padding: 10px; border: 1px solid #ddd;'>{sale.ProductSalesAmout}</td>
                        <td style='padding: 10px; border: 1px solid #ddd;'>${sale.Total ?? 0:F2}</td>
                        <td style='padding: 10px; border: 1px solid #ddd; color: {(sale.Baky > 0 ? "#dc3545" : "#28a745")};'>${sale.Baky ?? 0:F2}</td>
                    </tr>";
            }

            emailBody += $@"
                            </tbody>
                            <tfoot>
                                <tr style='background: #f8f9fa; font-weight: bold;'>
                                    <td colspan='3' style='padding: 10px; border: 1px solid #ddd;'>Total:</td>
                                    <td style='padding: 10px; border: 1px solid #ddd;'>${totalAmount:F2}</td>
                                    <td style='padding: 10px; border: 1px solid #ddd; color: {(totalOutstanding > 0 ? "#dc3545" : "#28a745")};'>${totalOutstanding:F2}</td>
                                </tr>
                            </tfoot>
                        </table>
                        
                        <div style='background: {(totalOutstanding > 0 ? "#fff3cd" : "#d4edda")}; padding: 15px; border-radius: 5px; margin: 20px 0;'>
                            <h4 style='color: {(totalOutstanding > 0 ? "#856404" : "#155724")};'>Payment Status</h4>
                            <p>Total Amount: <strong>${totalAmount:F2}</strong></p>
                            <p>Amount Paid: <strong>${totalPaid:F2}</strong></p>
                            <p>Outstanding Balance: <strong style='color: {(totalOutstanding > 0 ? "#dc3545" : "#28a745")};'>${totalOutstanding:F2}</strong></p>
                        </div>
                        
                        {(includePaymentLink && totalOutstanding > 0 ? $@"
                        <div style='text-align: center; margin: 20px 0;'>
                            <a href='#' style='background: #28a745; color: white; padding: 12px 24px; text-decoration: none; border-radius: 5px; display: inline-block;'>Pay Outstanding Balance (${totalOutstanding:F2})</a>
                        </div>" : "")}
                        
                        <div style='margin-top: 30px; padding-top: 20px; border-top: 1px solid #ddd; color: #6c757d; font-size: 12px;'>
                            <p>This is an automated email. Please do not reply to this email.</p>
                            <p>If you have any questions, please contact our support team.</p>
                            <p>Generated on {DateTime.Now:MMMM dd, yyyy} at {DateTime.Now:HH:mm}</p>
                        </div>
                    </div>
                </div>";

            return emailBody;
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
}
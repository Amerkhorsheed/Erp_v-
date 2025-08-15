using Erp_V1.BLL;
using Erp_V1.DAL.DAL;
using Erp_V1.DAL.DTO;
using Seller.API.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using SalesBLL = Erp_V1.BLL.SalesBLL;
using IInvoicePdfService = Seller.API.Services.IInvoicePdfService;
using IEmailService = Seller.API.Services.IEmailService;
using IWhatsAppService = Seller.API.Services.IWhatsAppService;
using erp_v2Entities = Erp_V1.DAL.DAL.erp_v2Entities;
using SalesDetailDTO = Erp_V1.DAL.DTO.SalesDetailDTO;

namespace Seller.API.Controllers
{
    [Authorize(Roles = "Seller,Admin")]
    [RoutePrefix("api/transaction")]
    public class TransactionController : ApiController
    {
        private readonly SalesBLL _salesBll;
        private readonly IInvoicePdfService _invoicePdfService;
        private readonly IEmailService _emailService;
        private readonly IWhatsAppService _whatsAppService;
        private readonly erp_v2Entities _dbContext;

        public TransactionController(
            SalesBLL salesBll,
            IInvoicePdfService invoicePdfService,
            IEmailService emailService,
            IWhatsAppService whatsAppService,
            erp_v2Entities dbContext)
        {
            _salesBll = salesBll;
            _invoicePdfService = invoicePdfService;
            _emailService = emailService;
            _whatsAppService = whatsAppService;
            _dbContext = dbContext;
        }

        [HttpPost]
        [Route("sale")]
        public async Task<IHttpActionResult> CreateSaleTransaction([FromBody] SaleCreationRequestApiModel request)
        {
            // The global exception filter will catch any catastrophic failures (like DB connection).
            // This try-catch handles specific business process failures.
            try
            {
                if (request == null || request.Items == null || !request.Items.Any())
                {
                    return BadRequest("Invalid sale data. The request body is empty or missing items.");
                }

                // Generate a single TransactionId for all items in this sale
                var transactionId = Guid.NewGuid().ToString();
                
                // Convert request items to SalesDetailDTO list
                var saleDetailDtos = request.Items.Select(item => new SalesDetailDTO
                {
                    CustomerID = request.CustomerId,
                    ProductID = item.ProductId,
                    SalesAmount = item.Quantity,
                    Price = (int)item.UnitPrice,
                    MaxDiscount = (float)item.Discount,
                    SalesDate = DateTime.Now
                }).ToList();
                
                // Create all sales atomically with the same TransactionId
                var savedSaleIds = _salesBll.InsertTransactionWithItems(saleDetailDtos, transactionId);

                if (!savedSaleIds.Any())
                {
                    return InternalServerError(new Exception("Transaction failed. No sales were saved to the database."));
                }

                var customer = await _dbContext.CUSTOMER.FindAsync(request.CustomerId);
                
                // Generate WhatsApp URL if customer has a valid phone number
                string whatsAppUrl = null;
                if (customer != null)
                {
                    // Get sales data for WhatsApp message
                    var salesForWhatsApp = _dbContext.SALES
                         .Where(s => savedSaleIds.Contains(s.ID))
                         .ToList();
                    
                    // Generate invoice URL (you may need to adjust this based on your actual invoice URL structure)
                    string invoiceUrl = $"https://yourapp.com/invoice/{string.Join(",", savedSaleIds)}";
                    
                    whatsAppUrl = _whatsAppService.GenerateInvoiceClickToChatUrl(customer, salesForWhatsApp, invoiceUrl);
                }
                
                if (customer != null && !string.IsNullOrEmpty(customer.Email))
                {
                    // Validate email format
                    if (!IsValidEmail(customer.Email))
                    {
                        System.Diagnostics.Debug.WriteLine($"[TRANSACTION] Invalid email format for customer {request.CustomerId}: {customer.Email}");
                        return Ok(new { 
                            success = true, 
                            message = "Sale created successfully, but customer email format is invalid.", 
                            saleIds = savedSaleIds,
                            transactionId = transactionId,
                            whatsAppUrl = whatsAppUrl
                        });
                    }
                    try
                    {
                        System.Diagnostics.Debug.WriteLine($"[TRANSACTION] Generating PDF for sale IDs: {string.Join(", ", savedSaleIds)}");
                        byte[] pdfBytes = await _invoicePdfService.GenerateInvoicePdfAsync(savedSaleIds);
                        
                        System.Diagnostics.Debug.WriteLine($"[TRANSACTION] PDF generated successfully, size: {pdfBytes.Length} bytes");
                        System.Diagnostics.Debug.WriteLine($"[TRANSACTION] Sending email to: {customer.Email}");
                        
                        await _emailService.SendInvoiceEmailAsync(
                            customer.Email,
                            customer.CustomerName,
                            pdfBytes
                        );
                        
                        System.Diagnostics.Debug.WriteLine($"[TRANSACTION] Email sent successfully to: {customer.Email}");
                    }
                    catch (System.Exception emailEx)
                    {
                        System.Diagnostics.Debug.WriteLine($"[TRANSACTION_ERROR] Email/PDF error: {emailEx.Message}");
                        System.Diagnostics.Debug.WriteLine($"[TRANSACTION_ERROR] Stack trace: {emailEx.StackTrace}");
                        
                        // Log the specific error but don't fail the entire transaction
                        // The sale was already saved successfully
                        System.Diagnostics.Debug.WriteLine($"[TRANSACTION] Sale completed successfully, but email sending failed for customer: {customer.Email}");
                        
                        // Return success with a warning message instead of throwing an exception
                        return Ok(new { 
                            success = true, 
                            message = "Sale created successfully, but failed to send invoice email. Please check email configuration.", 
                            saleIds = savedSaleIds,
                            transactionId = transactionId,
                            emailError = emailEx.Message,
                            whatsAppUrl = whatsAppUrl
                        });
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"[TRANSACTION] Customer {request.CustomerId} has no email address, skipping email sending");
                    return Ok(new { success = true, message = "Sale created successfully.", saleIds = savedSaleIds, transactionId = transactionId, whatsAppUrl = whatsAppUrl });
                }

                return Ok(new { success = true, message = "Sale created and invoice emailed successfully.", saleIds = savedSaleIds, transactionId = transactionId, whatsAppUrl = whatsAppUrl });
            }
            catch (Exception ex)
            {
                // This will now catch the "loud" failure from EmailService
                // and return a proper error message to the frontend.
                return InternalServerError(ex);
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }

    // --- ViewModels for this specific controller's API endpoint ---
    // These models define the expected structure of the JSON request body.
    public class SaleCreationRequestApiModel
    {
        public int CustomerId { get; set; }
        public List<SaleItemRequestApiModel> Items { get; set; }
    }

    public class SaleItemRequestApiModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
    }
}
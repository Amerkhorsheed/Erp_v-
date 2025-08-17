using System;
using System.Threading.Tasks;
using System.Web.Http;
using Erp.WebApp.Services;
using System.Web.Http.Description;
using Erp.WebApp.Services.Interfaces;

namespace Erp.WebApp.Controllers
{
    [RoutePrefix("api/email")]
    public class EmailController : ApiController
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        /// <summary>
        /// Send a generic email
        /// </summary>
        [HttpPost]
        [Route("send")]
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> SendEmail([FromBody] SendEmailRequest request)
        {
            try
            {
                if (request == null || string.IsNullOrEmpty(request.ToEmail) || string.IsNullOrEmpty(request.Subject))
                {
                    return BadRequest("Invalid email request. ToEmail and Subject are required.");
                }

                var result = await _emailService.SendEmailAsync(
                    request.ToEmail,
                    request.Subject,
                    request.Body ?? string.Empty,
                    request.AttachmentData,
                    request.AttachmentName
                );

                if (result)
                {
                    return Ok(new { success = true, message = "Email sent successfully" });
                }
                else
                {
                    return InternalServerError(new Exception("Failed to send email"));
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Send a welcome email to a new customer
        /// </summary>
        [HttpPost]
        [Route("welcome")]
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> SendWelcomeEmail([FromBody] WelcomeEmailRequest request)
        {
            try
            {
                if (request == null || string.IsNullOrEmpty(request.CustomerEmail) || string.IsNullOrEmpty(request.CustomerName))
                {
                    return BadRequest("Invalid request. CustomerEmail and CustomerName are required.");
                }

                var result = await _emailService.SendWelcomeEmailAsync(request.CustomerEmail, request.CustomerName);

                if (result)
                {
                    return Ok(new { success = true, message = "Welcome email sent successfully" });
                }
                else
                {
                    return InternalServerError(new Exception("Failed to send welcome email"));
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Send a sale confirmation email
        /// </summary>
        [HttpPost]
        [Route("sale-confirmation")]
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> SendSaleConfirmationEmail([FromBody] SaleConfirmationEmailRequest request)
        {
            try
            {
                if (request == null || string.IsNullOrEmpty(request.CustomerEmail) || string.IsNullOrEmpty(request.CustomerName))
                {
                    return BadRequest("Invalid request. CustomerEmail and CustomerName are required.");
                }

                var result = await _emailService.SendSaleConfirmationEmailAsync(
                    request.CustomerEmail,
                    request.CustomerName,
                    request.SaleId,
                    request.TotalAmount
                );

                if (result)
                {
                    return Ok(new { success = true, message = "Sale confirmation email sent successfully" });
                }
                else
                {
                    return InternalServerError(new Exception("Failed to send sale confirmation email"));
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Send an invoice email with PDF attachment
        /// </summary>
        [HttpPost]
        [Route("invoice")]
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> SendInvoiceEmail([FromBody] InvoiceEmailRequest request)
        {
            try
            {
                if (request == null || string.IsNullOrEmpty(request.CustomerEmail) || string.IsNullOrEmpty(request.CustomerName) || request.PdfAttachment == null)
                {
                    return BadRequest("Invalid request. CustomerEmail, CustomerName, and PdfAttachment are required.");
                }

                var result = await _emailService.SendInvoiceEmailAsync(
                    request.CustomerEmail,
                    request.CustomerName,
                    request.SaleId,
                    request.PdfAttachment
                );

                if (result)
                {
                    return Ok(new { success = true, message = "Invoice email sent successfully" });
                }
                else
                {
                    return InternalServerError(new Exception("Failed to send invoice email"));
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }

    // Request models
    public class SendEmailRequest
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public byte[] AttachmentData { get; set; }
        public string AttachmentName { get; set; }
    }

    public class WelcomeEmailRequest
    {
        public string CustomerEmail { get; set; }
        public string CustomerName { get; set; }
    }

    public class SaleConfirmationEmailRequest
    {
        public string CustomerEmail { get; set; }
        public string CustomerName { get; set; }
        public int SaleId { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class InvoiceEmailRequest
    {
        public string CustomerEmail { get; set; }
        public string CustomerName { get; set; }
        public int SaleId { get; set; }
        public byte[] PdfAttachment { get; set; }
    }
}
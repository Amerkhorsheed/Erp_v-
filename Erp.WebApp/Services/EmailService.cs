using System;
using System.Configuration; // 1. ADD this standard .NET Framework using statement
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Erp.WebApp.Services.Interfaces;

namespace Erp.WebApp.Services
{
    public class EmailService : IEmailService
    {
        // 2. REMOVE the constructor that required IConfiguration and ILogger.
        // The service is now self-contained.
        public EmailService()
        {
        }

        public async Task<bool> SendEmailAsync(string toEmail, string subject, string body, byte[] attachmentData = null, string attachmentName = null)
        {
            try
            {
                var smtpClient = new SmtpClient(ConfigurationManager.AppSettings["EmailSettings:SmtpHost"])
                {
                    Port = int.Parse(ConfigurationManager.AppSettings["EmailSettings:SmtpPort"]),
                    Credentials = new NetworkCredential(
                        ConfigurationManager.AppSettings["EmailSettings:SmtpUser"],
                        ConfigurationManager.AppSettings["EmailSettings:SmtpPass"]
                    ),
                    EnableSsl = true
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(ConfigurationManager.AppSettings["EmailSettings:FromEmail"], ConfigurationManager.AppSettings["EmailSettings:FromName"]),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                mailMessage.To.Add(toEmail);

                if (attachmentData != null && !string.IsNullOrEmpty(attachmentName))
                {
                    var stream = new MemoryStream(attachmentData);
                    mailMessage.Attachments.Add(new Attachment(stream, attachmentName));
                }

                await smtpClient.SendMailAsync(mailMessage);
                System.Diagnostics.Debug.WriteLine($"[EMAIL_SENT] Email sent successfully to: {toEmail}");
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[EMAIL_ERROR] Failed to send email to {toEmail}: {ex.Message}");
                return false;
            }
        }

        // --- NO CHANGES ARE NEEDED FOR THE METHODS BELOW ---
        // They correctly call the refactored SendEmailAsync method above.

        public async Task<bool> SendWelcomeEmailAsync(string customerEmail, string customerName)
        {
            var subject = "Welcome to Our Store!";
            var body = GenerateWelcomeEmailTemplate(customerName);
            return await SendEmailAsync(customerEmail, subject, body);
        }

        public async Task<bool> SendSaleConfirmationEmailAsync(string customerEmail, string customerName, int saleId, decimal totalAmount)
        {
            var subject = "Sale Confirmation - Thank You for Your Purchase!";
            var body = GenerateSaleConfirmationTemplate(customerName, saleId, totalAmount);
            return await SendEmailAsync(customerEmail, subject, body);
        }

        public async Task<bool> SendInvoiceEmailAsync(string customerEmail, string customerName, int saleId, byte[] pdfAttachment)
        {
            var subject = "Your Invoice - Thank You for Your Purchase!";
            var body = GenerateInvoiceEmailTemplate(customerName, saleId);
            return await SendEmailAsync(customerEmail, subject, body, pdfAttachment, "invoice.pdf");
        }

        private string GenerateInvoiceEmailTemplate(string customerName, int saleId)
        {
            return $@"
                <div style='font-family: Arial, sans-serif; line-height: 1.6;'>
                    <h2>Invoice Attached</h2>
                    <p>Dear {customerName},</p>
                    <p>Thank you for your business! Your invoice (<b>#{saleId}</b>) is attached to this email as a PDF document.</p>
                    <p>If you have any questions, please feel free to contact us.</p>
                    <p>Best regards,<br><b>The ERP Team</b></p>
                </div>";
        }

        private string GenerateWelcomeEmailTemplate(string customerName)
        {
            return $@"
                <div style='font-family: Arial, sans-serif; line-height: 1.6;'>
                    <h2>Welcome Aboard!</h2>
                    <p>Dear {customerName},</p>
                    <p>We are thrilled to welcome you as a new customer. We look forward to working with you.</p>
                    <p>Best regards,<br><b>The ERP Team</b></p>
                </div>";
        }

        private string GenerateSaleConfirmationTemplate(string customerName, int saleId, decimal totalAmount)
        {
            return $@"
                <div style='font-family: Arial, sans-serif; line-height: 1.6;'>
                    <h2>Your Order is Confirmed!</h2>
                    <p>Dear {customerName},</p>
                    <p>This email confirms that we have successfully processed your order <b>#{saleId}</b> for a total of <b>${totalAmount:F2}</b>.</p>
                    <p>Your invoice will be sent in a separate email shortly.</p>
                    <p>Best regards,<br><b>The ERP Team</b></p>
                </div>";
        }
    }
}
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Seller.API.Services
{
    public class EmailService : IEmailService
    {
        public async Task SendEmailAsync(string to, string subject, string body, byte[] attachment = null, string attachmentName = null)
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
                mailMessage.To.Add(to);

                if (attachment != null && !string.IsNullOrEmpty(attachmentName))
                {
                    var stream = new MemoryStream(attachment);
                    mailMessage.Attachments.Add(new Attachment(stream, attachmentName));
                }

                await smtpClient.SendMailAsync(mailMessage);
                System.Diagnostics.Debug.WriteLine($"[EMAIL_SENT] Email sent successfully to: {to}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[EMAIL_ERROR] Failed to send email to {to}: {ex.Message}");
                throw;
            }
        }

        public async Task SendWelcomeEmailAsync(string to, string customerName)
        {
            var subject = "Welcome to Our Company!";
            var body = GenerateWelcomeEmailTemplate(customerName);
            await SendEmailAsync(to, subject, body);
        }

        public async Task SendSaleConfirmationEmailAsync(string to, string customerName, decimal totalAmount)
        {
            var subject = "Sale Confirmation - Thank You for Your Purchase!";
            var body = GenerateSaleConfirmationTemplate(customerName, totalAmount);
            await SendEmailAsync(to, subject, body);
        }

        public async Task SendInvoiceEmailAsync(string to, string customerName, byte[] invoicePdf)
        {
            var subject = "Your Invoice - Thank You for Your Purchase!";
            var body = GenerateInvoiceEmailTemplate(customerName);
            await SendEmailAsync(to, subject, body, invoicePdf, "invoice.pdf");
        }

        private string GenerateInvoiceEmailTemplate(string customerName)
        {
            return $@"
                <div style='font-family: Arial, sans-serif; line-height: 1.6;'>
                    <h2>Invoice Attached</h2>
                    <p>Dear {customerName},</p>
                    <p>Thank you for your business! Your invoice is attached to this email as a PDF document.</p>
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

        private string GenerateSaleConfirmationTemplate(string customerName, decimal totalAmount)
        {
            return $@"
                <div style='font-family: Arial, sans-serif; line-height: 1.6;'>
                    <h2>Your Order is Confirmed!</h2>
                    <p>Dear {customerName},</p>
                    <p>This email confirms that we have successfully processed your order for a total of <b>${totalAmount:F2}</b>.</p>
                    <p>Your invoice will be sent in a separate email shortly.</p>
                    <p>Best regards,<br><b>The ERP Team</b></p>
                </div>";
        }
    }
}
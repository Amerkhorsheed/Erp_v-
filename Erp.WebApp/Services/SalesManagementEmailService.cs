using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Erp.WebApp.Services
{
    public class SalesManagementEmailService : ISalesManagementEmailService
    {
        private const string SALES_MANAGEMENT_URL = "https://localhost:44327/SalesManagement/Index";
        
        public SalesManagementEmailService()
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
                    using (var stream = new MemoryStream(attachmentData))
                    {
                        var attachment = new Attachment(stream, attachmentName, "application/pdf");
                        mailMessage.Attachments.Add(attachment);
                        
                        // Send the email while the stream is still open
                        await smtpClient.SendMailAsync(mailMessage);
                        System.Diagnostics.Debug.WriteLine($"[SALES_EMAIL_SENT] Email with PDF attachment sent successfully to: {toEmail}");
                        return true;
                    }
                }
                else
                {
                    // Send email without attachment
                    await smtpClient.SendMailAsync(mailMessage);
                    System.Diagnostics.Debug.WriteLine($"[SALES_EMAIL_SENT] Email sent successfully to: {toEmail}");
                    return true;
                }

                // This code should not be reached due to the changes above
                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[SALES_EMAIL_ERROR] Failed to send email to {toEmail}: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> SendInvoiceEmailAsync(string customerEmail, string customerName, int saleId, byte[] pdfAttachment, string additionalMessage = "", bool includePaymentLink = true)
        {
            var subject = "Your Invoice - Thank You for Your Purchase!";
            var body = GenerateInvoiceEmailTemplate(customerName, saleId, additionalMessage, includePaymentLink);
            return await SendEmailAsync(customerEmail, subject, body, pdfAttachment, "invoice.pdf");
        }

        public async Task<bool> SendSalesNotificationAsync(string customerEmail, string customerName, string[] saleIds, string additionalMessage = "")
        {
            var subject = "Sales Management Notification";
            var body = GenerateSalesNotificationTemplate(customerName, saleIds, additionalMessage);
            return await SendEmailAsync(customerEmail, subject, body);
        }

        public async Task<bool> SendPaymentConfirmationAsync(string customerEmail, string customerName, decimal paymentAmount, string[] saleIds)
        {
            var subject = "Payment Confirmation - Thank You!";
            var body = GeneratePaymentConfirmationTemplate(customerName, paymentAmount, saleIds);
            return await SendEmailAsync(customerEmail, subject, body);
        }

        private string GenerateInvoiceEmailTemplate(string customerName, int saleId, string additionalMessage = "", bool includePaymentLink = true)
        {
            var additionalMessageSection = !string.IsNullOrEmpty(additionalMessage) ? $@"
                <div style='background-color: #e9ecef; padding: 15px; border-radius: 5px; margin: 20px 0;'>
                    <h4 style='color: #495057;'>📝 Additional Message</h4>
                    <p>{additionalMessage}</p>
                </div>" : "";

            return $@"
                <div style='font-family: Arial, sans-serif; line-height: 1.6; max-width: 600px; margin: 0 auto;'>
                    <div style='background-color: #007bff; color: white; padding: 20px; text-align: center; border-radius: 10px 10px 0 0;'>
                        <h2 style='margin: 0;'>📄 Invoice Attached</h2>
                    </div>
                    <div style='background-color: white; padding: 30px; border: 1px solid #dee2e6; border-radius: 0 0 10px 10px;'>
                        <p>Dear <strong>{customerName}</strong>,</p>
                        <p>Thank you for your business! Your invoice (<b>#{saleId}</b>) is attached to this email as a PDF document.</p>
                        {additionalMessageSection}
                        <p>If you have any questions, please feel free to contact us.</p>
                        <hr style='border: none; border-top: 1px solid #dee2e6; margin: 30px 0;'>
                        <p style='color: #6c757d; font-size: 14px;'>Best regards,<br><b>The ERP Sales Team</b></p>
                    </div>
                </div>";
        }

        private string GenerateSalesNotificationTemplate(string customerName, string[] saleIds, string additionalMessage = "")
        {
            var salesList = string.Join(", ", saleIds);
            var additionalMessageSection = !string.IsNullOrEmpty(additionalMessage) ? $@"
                <div style='background-color: #e9ecef; padding: 15px; border-radius: 5px; margin: 20px 0;'>
                    <h4 style='color: #495057;'>📝 Message</h4>
                    <p>{additionalMessage}</p>
                </div>" : "";

            return $@"
                <div style='font-family: Arial, sans-serif; line-height: 1.6; max-width: 600px; margin: 0 auto;'>
                    <div style='background-color: #28a745; color: white; padding: 20px; text-align: center; border-radius: 10px 10px 0 0;'>
                        <h2 style='margin: 0;'>🛒 Sales Notification</h2>
                    </div>
                    <div style='background-color: white; padding: 30px; border: 1px solid #dee2e6; border-radius: 0 0 10px 10px;'>
                        <p>Dear <strong>{customerName}</strong>,</p>
                        <p>This is a notification regarding your sales: <strong>{salesList}</strong></p>
                        {additionalMessageSection}

                        <p>If you have any questions, please feel free to contact us.</p>
                        <hr style='border: none; border-top: 1px solid #dee2e6; margin: 30px 0;'>
                        <p style='color: #6c757d; font-size: 14px;'>Best regards,<br><b>The ERP Sales Team</b></p>
                    </div>
                </div>";
        }

        private string GeneratePaymentConfirmationTemplate(string customerName, decimal paymentAmount, string[] saleIds)
        {
            var salesList = string.Join(", ", saleIds);

            return $@"
                <div style='font-family: Arial, sans-serif; line-height: 1.6; max-width: 600px; margin: 0 auto;'>
                    <div style='background-color: #17a2b8; color: white; padding: 20px; text-align: center; border-radius: 10px 10px 0 0;'>
                        <h2 style='margin: 0;'>💰 Payment Confirmation</h2>
                    </div>
                    <div style='background-color: white; padding: 30px; border: 1px solid #dee2e6; border-radius: 0 0 10px 10px;'>
                        <p>Dear <strong>{customerName}</strong>,</p>
                        <p>We have successfully received your payment of <strong>${paymentAmount:F2}</strong> for sales: <strong>{salesList}</strong></p>
                        <div style='background-color: #d4edda; padding: 15px; border-radius: 5px; margin: 20px 0; border-left: 4px solid #28a745;'>
                            <h4 style='color: #155724; margin-bottom: 10px;'>✅ Payment Processed</h4>
                            <p style='color: #155724; margin: 0;'>Your payment has been successfully processed and applied to your account.</p>
                        </div>

                        <p>Thank you for your payment!</p>
                        <hr style='border: none; border-top: 1px solid #dee2e6; margin: 30px 0;'>
                        <p style='color: #6c757d; font-size: 14px;'>Best regards,<br><b>The ERP Sales Team</b></p>
                    </div>
                </div>";
        }
    }
}
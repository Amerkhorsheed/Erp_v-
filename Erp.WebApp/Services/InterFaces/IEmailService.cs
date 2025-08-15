// File: Services/Interfaces/IEmailService.cs

using System.Threading.Tasks;

namespace Erp.WebApp.Services
{
    public interface IEmailService
{
    // A generic method that can handle attachments
    Task<bool> SendEmailAsync(string toEmail, string subject, string body, byte[]? attachmentData = null, string? attachmentName = null);

    // A specific, high-level method for sending welcome emails
    Task<bool> SendWelcomeEmailAsync(string customerEmail, string customerName);

    // A specific, high-level method for sending sale confirmations
    Task<bool> SendSaleConfirmationEmailAsync(string customerEmail, string customerName, int saleId, decimal totalAmount);

    // --- NEW METHOD ---
    // A specific, high-level method for sending the invoice PDF
    Task<bool> SendInvoiceEmailAsync(string customerEmail, string customerName, int saleId, byte[] pdfAttachment);
    }
}
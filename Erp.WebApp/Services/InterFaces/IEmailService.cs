using System.Threading.Tasks;

namespace Erp.WebApp.Services.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string toEmail, string subject, string body, byte[] attachmentData = null, string attachmentName = null);
        Task<bool> SendWelcomeEmailAsync(string customerEmail, string customerName);
        Task<bool> SendSaleConfirmationEmailAsync(string customerEmail, string customerName, int saleId, decimal totalAmount);
        Task<bool> SendInvoiceEmailAsync(string customerEmail, string customerName, int saleId, byte[] pdfAttachment);
    }
}
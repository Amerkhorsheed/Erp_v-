using System.Threading.Tasks;

namespace Erp.WebApp.Services.Interfaces
{
    public interface ISalesManagementEmailService
    {
        Task<bool> SendEmailAsync(string toEmail, string subject, string body, byte[] attachmentData = null, string attachmentName = null);
        Task<bool> SendInvoiceEmailAsync(string customerEmail, string customerName, int saleId, byte[] pdfAttachment, string additionalMessage = "", bool includePaymentLink = true);
        Task<bool> SendSalesNotificationAsync(string customerEmail, string customerName, string[] saleIds, string additionalMessage = "");
        Task<bool> SendPaymentConfirmationAsync(string customerEmail, string customerName, decimal paymentAmount, string[] saleIds);
    }
}
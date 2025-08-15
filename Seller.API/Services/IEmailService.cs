using System.Threading.Tasks;

namespace Seller.API.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body, byte[] attachment = null, string attachmentName = null);
        Task SendWelcomeEmailAsync(string to, string customerName);
        Task SendSaleConfirmationEmailAsync(string to, string customerName, decimal totalAmount);
        Task SendInvoiceEmailAsync(string to, string customerName, byte[] invoicePdf);
    }
}
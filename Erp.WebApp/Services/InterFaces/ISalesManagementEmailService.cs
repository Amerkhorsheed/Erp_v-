using System.Threading.Tasks;

namespace Erp.WebApp.Services
{
    public interface ISalesManagementEmailService
    {
        /// <summary>
        /// Send a generic email with optional attachment
        /// </summary>
        /// <param name="toEmail">Recipient email address</param>
        /// <param name="subject">Email subject</param>
        /// <param name="body">Email body (HTML)</param>
        /// <param name="attachmentData">Optional attachment data</param>
        /// <param name="attachmentName">Optional attachment filename</param>
        /// <returns>True if email sent successfully</returns>
        Task<bool> SendEmailAsync(string toEmail, string subject, string body, byte[] attachmentData = null, string attachmentName = null);

        /// <summary>
        /// Send an invoice email with PDF attachment and Sales Management portal link
        /// </summary>
        /// <param name="customerEmail">Customer email address</param>
        /// <param name="customerName">Customer name</param>
        /// <param name="saleId">Sale ID for the invoice</param>
        /// <param name="pdfAttachment">PDF invoice attachment</param>
        /// <param name="additionalMessage">Optional additional message</param>
        /// <param name="includePaymentLink">Whether to include payment link</param>
        /// <returns>True if email sent successfully</returns>
        Task<bool> SendInvoiceEmailAsync(string customerEmail, string customerName, int saleId, byte[] pdfAttachment, string additionalMessage = "", bool includePaymentLink = true);

        /// <summary>
        /// Send a sales notification email with Sales Management portal link
        /// </summary>
        /// <param name="customerEmail">Customer email address</param>
        /// <param name="customerName">Customer name</param>
        /// <param name="saleIds">Array of sale IDs</param>
        /// <param name="additionalMessage">Optional additional message</param>
        /// <returns>True if email sent successfully</returns>
        Task<bool> SendSalesNotificationAsync(string customerEmail, string customerName, string[] saleIds, string additionalMessage = "");

        /// <summary>
        /// Send a payment confirmation email with Sales Management portal link
        /// </summary>
        /// <param name="customerEmail">Customer email address</param>
        /// <param name="customerName">Customer name</param>
        /// <param name="paymentAmount">Payment amount</param>
        /// <param name="saleIds">Array of sale IDs the payment applies to</param>
        /// <returns>True if email sent successfully</returns>
        Task<bool> SendPaymentConfirmationAsync(string customerEmail, string customerName, decimal paymentAmount, string[] saleIds);
    }
}
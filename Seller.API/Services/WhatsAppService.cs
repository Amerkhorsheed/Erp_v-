using Erp_V1.DAL.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Seller.API.Services
{
    public class WhatsAppService : IWhatsAppService
    {
        public string GenerateInvoiceClickToChatUrl(CUSTOMER customer, List<SALES> sales, string invoiceUrl)
        {
            // 1. Validate and format the phone number.
            var formattedPhone = FormatPhoneNumber(customer.Cust_Phone);
            if (string.IsNullOrWhiteSpace(formattedPhone))
            {
                return null;
            }

            // 2. Create the message body.
            var messageBuilder = new StringBuilder();
            decimal grandTotal = 0;

            messageBuilder.AppendLine($"Hello {customer.CustomerName},");
            messageBuilder.AppendLine("Thank you for your purchase from ERP System!");
            messageBuilder.AppendLine("\n*Your Order Summary:*");

            foreach (var sale in sales)
            {
                var lineTotal = (decimal)((sale.ProductSalesPrice * sale.ProductSalesAmout) - (sale.MaxDiscount ?? 0));
                grandTotal += lineTotal;
                messageBuilder.AppendLine($"- Product ID: {sale.ProductID} (Qty: {sale.ProductSalesAmout})");
            }

            messageBuilder.AppendLine($"\n*Total: ${grandTotal:F2}*");
            messageBuilder.AppendLine("\nYou can view your full detailed invoice here:");
            messageBuilder.AppendLine(invoiceUrl);

            // 3. URL-encode the message for the link.
            var encodedMessage = HttpUtility.UrlEncode(messageBuilder.ToString());

            // 4. Construct the final "Click-to-Chat" URL.
            return $"https://wa.me/{formattedPhone}?text={encodedMessage}";
        }

        /// <summary>
        /// Cleans and formats a phone number for use in a wa.me link.
        /// Assumes numbers are Syrian or need a country code.
        /// </summary>
        /// <param name="phoneNumber">The raw phone number string.</param>
        /// <returns>A formatted phone number with country code, or null if invalid.</returns>
        private string FormatPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                return null;
            }

            // Remove all non-digit characters
            var digitsOnly = Regex.Replace(phoneNumber, @"\D", "");

            if (string.IsNullOrEmpty(digitsOnly))
            {
                return null;
            }

            // Handle Syrian numbers (e.g., 09xxxxxxxx -> 9639xxxxxxxx)
            if (digitsOnly.Length == 10 && digitsOnly.StartsWith("09"))
            {
                return "963" + digitsOnly.Substring(1);
            }

            // Handle numbers that already have the country code but might have a leading 00 or +
            if (digitsOnly.StartsWith("963") && digitsOnly.Length > 9)
            {
                return digitsOnly;
            }

            // Add other country code logic here if needed.
            // For now, we assume if it's not a recognizable Syrian number, it might be invalid.
            return null;
        }
    }
}
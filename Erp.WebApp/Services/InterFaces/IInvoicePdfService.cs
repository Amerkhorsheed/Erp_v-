// File: Services/Interfaces/IInvoicePdfService.cs

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Erp.WebApp.Services.Interfaces
{
    /// <summary>
    /// Defines a service for generating PDF documents, specifically invoices.
    /// </summary>
    public interface IInvoicePdfService
    {
        /// <summary>
        /// Generates a single PDF invoice document for a collection of sale records that make up one transaction.
        /// </summary>
        /// <param name="saleIds">A list of Sale record IDs that belong to the same customer transaction.</param>
        /// <returns>A byte array representing the generated PDF file.</returns>
        Task<byte[]> GenerateInvoicePdfAsync(List<int> saleIds);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Erp.WebApp.Services.Interfaces
{
    public interface IInvoicePdfService
    {
        Task<byte[]> GenerateInvoicePdfAsync(List<int> saleIds);
    }
}
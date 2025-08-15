using Erp_V1.DAL.DTO;
using System.Collections.Generic;

namespace Erp.WebApp.ViewModels
{
    public class InvoiceManagementViewModel
    {
        public List<InvoiceDetailDTO> Invoices { get; set; }
        public List<CustomerDetailDTO> Customers { get; set; }
        public List<ProductDetailDTO> Products { get; set; }
        public List<CategoryDetailDTO> Categories { get; set; }

        public InvoiceManagementViewModel()
        {
            Invoices = new List<InvoiceDetailDTO>();
            Customers = new List<CustomerDetailDTO>();
            Products = new List<ProductDetailDTO>();
            Categories = new List<CategoryDetailDTO>();
        }
    }

    public class CreateInvoiceViewModel
    {
        public InvoiceDetailDTO Invoice { get; set; }
        public List<CustomerDetailDTO> Customers { get; set; }
        public List<ProductDetailDTO> Products { get; set; }
        public List<CategoryDetailDTO> Categories { get; set; }

        public CreateInvoiceViewModel()
        {
            Invoice = new InvoiceDetailDTO();
            Customers = new List<CustomerDetailDTO>();
            Products = new List<ProductDetailDTO>();
            Categories = new List<CategoryDetailDTO>();
        }
    }

    public class EditInvoiceViewModel
    {
        public InvoiceDetailDTO Invoice { get; set; }
        public List<CustomerDetailDTO> Customers { get; set; }
        public List<ProductDetailDTO> Products { get; set; }
        public List<CategoryDetailDTO> Categories { get; set; }

        public EditInvoiceViewModel()
        {
            Invoice = new InvoiceDetailDTO();
            Customers = new List<CustomerDetailDTO>();
            Products = new List<ProductDetailDTO>();
            Categories = new List<CategoryDetailDTO>();
        }
    }
}
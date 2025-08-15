// File: Erp.WebApp/ViewModels/CreateSaleViewModel.cs
using Erp_V1.DAL.DTO;
using System.Collections.Generic;

namespace Erp.WebApp.ViewModels
{
    public class CreateSaleViewModel
    {
        // This will hold the list of all customers for the dropdown
        public List<CustomerDetailDTO> Customers { get; set; }

        // This will hold the list of all products for the search functionality
        public List<ProductDetailDTO> Products { get; set; }

        // We can initialize them in the constructor to avoid null reference errors
        public CreateSaleViewModel()
        {
            Customers = new List<CustomerDetailDTO>();
            Products = new List<ProductDetailDTO>();
        }
    }
}
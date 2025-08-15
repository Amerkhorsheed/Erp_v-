using Erp.WebApp.Services;
using Erp.WebApp.Services.Interfaces;
using Erp.WebApp.ViewModels;
using Erp_V1.DAL.DAL;
using Erp_V1.DAL.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Erp.WebApp.Controllers
{
    [CustomAuthorize(Roles = "Seller,Admin")]
    public class SellerController : Controller
    {
        private readonly erp_v2Entities _dbContext = new erp_v2Entities();
        private readonly IApiService _apiService;
        private readonly IAuthService _authService;

        public SellerController(IApiService apiService, IAuthService authService)
        {
            _apiService = apiService;
            _authService = authService;
        }

        public ActionResult Index()
        {
            var viewModel = new CreateSaleViewModel();

            viewModel.Customers = _dbContext.CUSTOMER
                .Where(c => c.isDeleted == false)
                .Select(c => new CustomerDetailDTO
                {
                    ID = c.ID,
                    CustomerName = c.CustomerName,
                    Email = c.Email
                }).ToList();

            viewModel.Products = _dbContext.PRODUCT
                .Where(p => p.isDeleted == false && p.StockAmount > 0)
                .Select(p => new ProductDetailDTO
                {
                    ProductID = p.ID,
                    ProductName = p.ProductName,
                    stockAmount = p.StockAmount,
                    Sale_Price = (float)p.Sale_Price,
                }).ToList();

            ViewBag.CustomersJson = JsonConvert.SerializeObject(viewModel.Customers);
            ViewBag.ProductsJson = JsonConvert.SerializeObject(viewModel.Products);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> CreateSale(SaleCreationRequest saleData)
        {
            if (saleData == null || saleData.Items == null || !saleData.Items.Any())
            {
                Response.StatusCode = 400;
                return Json(new { success = false, message = "Invalid sale data provided." }, JsonRequestBehavior.AllowGet);
            }

            var token = _authService.Token;
            if (string.IsNullOrEmpty(token))
            {
                Response.StatusCode = 401;
                return Json(new { success = false, message = "Authentication token not found." }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                var response = await _apiService.PostAsync<SaleCreationRequest, dynamic>(
                    "api/transaction/sale",
                    saleData,
                    token
                );

                // Ensure the response is properly structured for the frontend
                if (response != null)
                {
                    // Extract and properly format the response data
                    var success = response.success ?? true;
                    var message = response.message ?? "Sale completed successfully.";
                    
                    // Handle saleIds - ensure it's a proper array of integers
                    var saleIds = new List<int>();
                    if (response.saleIds != null)
                    {
                        try
                        {
                            // Convert dynamic saleIds to List<int>
                            foreach (var id in response.saleIds)
                            {
                                if (int.TryParse(id.ToString(), out int saleId))
                                {
                                    saleIds.Add(saleId);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine($"[CONTROLLER] Error parsing saleIds: {ex.Message}");
                        }
                    }
                    
                    // Handle whatsAppUrl
                    var whatsAppUrl = response.whatsAppUrl?.ToString() ?? "";
                    
                    // Handle transactionId
                    var transactionId = response.transactionId?.ToString() ?? "";
                    
                    return Json(new
                    {
                        success = success,
                        message = message,
                        saleIds = saleIds,
                        transactionId = transactionId,
                        whatsAppUrl = whatsAppUrl
                    });
                }
                else
                {
                    return Json(new { success = false, message = "No response received from API." });
                }
            }
            catch (Exception ex)
            {
                // FIX: This now returns a clean JSON error response
                Response.StatusCode = 500;
                return Json(new
                {
                    success = false,
                    message = $"An error occurred while communicating with the API: {ex.Message}"
                }, JsonRequestBehavior.AllowGet);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }

// These request models remain the same, as they match the JavaScript payload.
public class SaleCreationRequest
    {
        public int CustomerId { get; set; }
        public List<SaleItemRequest> Items { get; set; }
    }

    public class SaleItemRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;
using Erp.WebApp.ViewModels;
using Newtonsoft.Json;

namespace Erp.WebApp.Controllers
{
    [CustomAuthorize(Roles = "Admin,Seller")]
    public class InvoiceManagementController : Controller
    {
        private readonly InvoiceBLL _invoiceBll;
        private readonly CustomerBLL _customerBll;
        private readonly ProductBLL _productBll;
        private readonly CategoryBLL _categoryBll;

        public InvoiceManagementController(InvoiceBLL invoiceBll, CustomerBLL customerBll, ProductBLL productBll, CategoryBLL categoryBll)
        {
            _invoiceBll = invoiceBll ?? throw new ArgumentNullException(nameof(invoiceBll));
            _customerBll = customerBll ?? throw new ArgumentNullException(nameof(customerBll));
            _productBll = productBll ?? throw new ArgumentNullException(nameof(productBll));
            _categoryBll = categoryBll ?? throw new ArgumentNullException(nameof(categoryBll));
        }

        // GET: InvoiceManagement
        public ActionResult Index()
        {
            try
            {
                var invoiceData = _invoiceBll.Select();
                var viewModel = new InvoiceManagementViewModel
                {
                    Invoices = invoiceData.Invoices,
                    Customers = invoiceData.Customers,
                    Products = invoiceData.Products,
                    Categories = invoiceData.Categories
                };
                return View(viewModel);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error loading invoices: " + ex.Message;
                return View(new InvoiceManagementViewModel());
            }
        }

        // GET: InvoiceManagement/Create
        public ActionResult Create()
        {
            try
            {
                var invoiceData = _invoiceBll.Select();
                var viewModel = new CreateInvoiceViewModel
                {
                    Invoice = new InvoiceDetailDTO
                    {
                        InvoiceDate = DateTime.Today,
                        DueDate = DateTime.Today.AddDays(30),
                        InvoiceState = "Draft",
                        Items = new List<InvoiceItemDetailDTO>()
                    },
                    Customers = invoiceData.Customers,
                    Products = invoiceData.Products,
                    Categories = invoiceData.Categories
                };
                return View(viewModel);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error loading create invoice page: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        // POST: InvoiceManagement/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateInvoiceViewModel model)
        {
            try
            {
                // Log the incoming request for debugging
                System.Diagnostics.Debug.WriteLine($"Create Invoice Request - CustomerID: {model?.Invoice?.CustomerID}");
                
                if (model?.Invoice == null)
                {
                    System.Diagnostics.Debug.WriteLine("Create Invoice Error: Model or Invoice is null");
                    return Json(new { success = false, message = "No data received" });
                }

                // Get items from form data (they are submitted as form fields)
                var items = new List<InvoiceItemDetailDTO>();
                var formKeys = Request.Form.AllKeys;
                
                // Parse items from form data
                var itemIndices = formKeys
                    .Where(k => k.StartsWith("Items[") && k.Contains("].ProductID"))
                    .Select(k => k.Substring(6, k.IndexOf(']') - 6))
                    .Distinct()
                    .ToList();
                
                System.Diagnostics.Debug.WriteLine($"Found {itemIndices.Count} items in form data");
                
                foreach (var index in itemIndices)
                {
                    var productId = Request.Form[$"Items[{index}].ProductID"];
                    var categoryId = Request.Form[$"Items[{index}].CategoryID"];
                    var quantity = Request.Form[$"Items[{index}].Quantity"];
                    var unitPrice = Request.Form[$"Items[{index}].UnitPrice"];
                    var discountPercentage = Request.Form[$"Items[{index}].DiscountPercentage"];
                    
                    if (!string.IsNullOrEmpty(productId) && !string.IsNullOrEmpty(quantity))
                    {
                        // Get product name from the products list
                        var product = _productBll.Select().Products.FirstOrDefault(p => p.ProductID == int.Parse(productId));
                        
                        items.Add(new InvoiceItemDetailDTO
                        {
                            ProductID = int.Parse(productId),
                            CategoryID = int.Parse(categoryId ?? "0"),
                            ProductName = product?.ProductName ?? "Unknown Product",
                            Description = "",
                            Quantity = int.Parse(quantity),
                            UnitPrice = decimal.Parse(unitPrice ?? "0"),
                            DiscountPercentage = decimal.Parse(discountPercentage ?? "0"),
                            DiscountAmount = 0
                        });
                    }
                }

                if (!items.Any())
                {
                    System.Diagnostics.Debug.WriteLine("Create Invoice Error: No items provided");
                    return Json(new { success = false, message = "At least one item is required" });
                }

                var invoice = new InvoiceDetailDTO
                {
                    CustomerID = model.Invoice.CustomerID,
                    InvoiceDate = model.Invoice.InvoiceDate,
                    DueDate = model.Invoice.DueDate,
                    InvoiceState = model.Invoice.InvoiceState ?? "Draft",
                    Notes = model.Invoice.Notes,
                    TaxAmount = model.Invoice.TaxAmount,
                    DiscountAmount = model.Invoice.DiscountAmount,
                    CreatedBy = "WebUser", // You can get this from session/authentication
                    Items = items
                };

                System.Diagnostics.Debug.WriteLine($"Calling InvoiceBLL.Insert with {invoice.Items.Count} items");
                bool result = _invoiceBll.Insert(invoice);
                
                if (result)
                {
                    System.Diagnostics.Debug.WriteLine($"Invoice created successfully with ID: {invoice.InvoiceID}");
                    return Json(new { success = true, message = "Invoice created successfully", invoiceId = invoice.InvoiceID });
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Invoice creation failed - BLL returned false");
                    return Json(new { success = false, message = "Failed to create invoice" });
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Create Invoice Exception: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack Trace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    System.Diagnostics.Debug.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                return Json(new { success = false, message = $"Error creating invoice: {ex.Message}" });
            }
        }

        // GET: InvoiceManagement/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var invoiceData = _invoiceBll.Select();
                var invoice = invoiceData.Invoices.FirstOrDefault(i => i.InvoiceID == id);
                
                if (invoice == null)
                {
                    ViewBag.Error = "Invoice not found";
                    return RedirectToAction("Index");
                }

                var viewModel = new EditInvoiceViewModel
                {
                    Invoice = invoice,
                    Customers = invoiceData.Customers,
                    Products = invoiceData.Products,
                    Categories = invoiceData.Categories
                };
                
                return View(viewModel);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error loading invoice: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        // POST: InvoiceManagement/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditInvoiceRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { success = false, message = "Invalid data provided" });
                }

                var invoice = new InvoiceDetailDTO
                {
                    InvoiceID = id,
                    CustomerID = request.CustomerId,
                    InvoiceDate = request.InvoiceDate,
                    DueDate = request.DueDate,
                    InvoiceState = request.InvoiceState,
                    Notes = request.Notes,
                    TaxAmount = request.TaxAmount,
                    DiscountAmount = request.DiscountAmount,
                    PaidAmount = request.PaidAmount,
                    ModifiedBy = "WebUser"
                };

                bool result = _invoiceBll.Update(invoice);
                
                if (result)
                {
                    return Json(new { success = true, message = "Invoice updated successfully" });
                }
                else
                {
                    return Json(new { success = false, message = "Failed to update invoice" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error updating invoice: " + ex.Message });
            }
        }

        // POST: InvoiceManagement/UpdateState
        [HttpPost]
        public ActionResult UpdateState(int invoiceId, string newState)
        {
            try
            {
                bool result = _invoiceBll.UpdateInvoiceState(invoiceId, newState);
                
                if (result)
                {
                    return Json(new { success = true, message = $"Invoice state updated to {newState}" });
                }
                else
                {
                    return Json(new { success = false, message = "Failed to update invoice state" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error updating invoice state: " + ex.Message });
            }
        }

        // POST: InvoiceManagement/MarkAsPaid
        [HttpPost]
        public ActionResult MarkAsPaid(int invoiceId, decimal paidAmount)
        {
            try
            {
                bool result = _invoiceBll.MarkAsPaid(invoiceId, paidAmount);
                
                if (result)
                {
                    return Json(new { success = true, message = "Payment recorded successfully" });
                }
                else
                {
                    return Json(new { success = false, message = "Failed to record payment" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error recording payment: " + ex.Message });
            }
        }

        // GET: InvoiceManagement/GetInvoicesByState
        public ActionResult GetInvoicesByState(string state)
        {
            try
            {
                var invoices = _invoiceBll.GetInvoicesByState(state);
                return Json(new { success = true, data = invoices }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error retrieving invoices: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: InvoiceManagement/GetOverdueInvoices
        public ActionResult GetOverdueInvoices()
        {
            try
            {
                var invoices = _invoiceBll.GetOverdueInvoices();
                return Json(new { success = true, data = invoices }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error retrieving overdue invoices: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: InvoiceManagement/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var invoice = new InvoiceDetailDTO { InvoiceID = id };
                bool result = _invoiceBll.Delete(invoice);
                
                if (result)
                {
                    return Json(new { success = true, message = "Invoice deleted successfully" });
                }
                else
                {
                    return Json(new { success = false, message = "Failed to delete invoice" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error deleting invoice: " + ex.Message });
            }
        }
    }

    // Request Models
    public class CreateInvoiceRequest
    {
        public int CustomerId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public string InvoiceState { get; set; }
        public string Notes { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public List<InvoiceItemRequest> Items { get; set; }
    }

    public class EditInvoiceRequest
    {
        public int CustomerId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public string InvoiceState { get; set; }
        public string Notes { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal PaidAmount { get; set; }
    }

    public class InvoiceItemRequest
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountAmount { get; set; }
    }
}
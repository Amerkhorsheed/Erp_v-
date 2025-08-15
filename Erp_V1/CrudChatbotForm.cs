using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;

namespace Erp_V1
{
    public partial class CrudChatbotForm : Form
    {
        private readonly ProductBLL _productBLL = new ProductBLL();
        private readonly CategoryBLL _categoryBLL = new CategoryBLL();
        private readonly CustomerBLL _customerBLL = new CustomerBLL(); // Add Customer BLL
        private readonly SalesBLL _salesBLL = new SalesBLL();     // Add Sales BLL
        // You might need to add SupplierBLL here if you have it

        public CrudChatbotForm()
        {
            InitializeComponent();
            //InitializeDesignerCode();
        }

        // ... (Designer Code and UI Enhancements - InitializeDesignerCode, TxtInput_GotFocus, TxtInput_LostFocus, btnSend_Click, txtInput_KeyPress, SendCommand, AppendChatMessage) ...

        #region Chatbot Logic - Command Processing

        private string ProcessCommand(string command)
        {
            command = command.ToLowerInvariant();
            string[] parts = command.Split(' ');
            if (parts.Length < 2)
            {
                return GetHelpMessage(); // Get help message if command is too short
            }

            string operation = parts[0]; // e.g., "create", "get", "update", "delete", "get"
            string entityType = parts[1]; // e.g., "product", "category", "customer", "sales"
            string parameters = string.Join(" ", parts.Skip(2)).Trim(); // Parameters part

            return ExecuteCrudCommand(operation, entityType, parameters);
        }

        private string ExecuteCrudCommand(string operation, string entityType, string parameters)
        {
            try
            {
                switch (entityType)
                {
                    case "product":
                        return ProcessProductCommand(operation, parameters);
                    case "category":
                        return ProcessCategoryCommand(operation, parameters);
                    case "customer":
                        return ProcessCustomerCommand(operation, parameters);
                    case "sales":
                        return ProcessSalesCommand(operation, parameters);
                    //case "supplier": // Add case for supplier when you implement SupplierBLL
                    //    return ProcessSupplierCommand(operation, parameters);
                    default:
                        return $"Invalid entity type: '{entityType}'.\n" + GetHelpMessage();
                }
            }
            catch (FormatException fe)
            {
                return fe.Message; // Return specific format exception message
            }
            catch (Exception ex)
            {
                return $"Error processing command: {ex.Message}"; // Generic error message
            }
        }

        #region Entity Command Handlers - Product, Category, Customer, Sales

        private string ProcessProductCommand(string operation, string parameters)
        {
            switch (operation)
            {
                case "create":
                    return CreateProduct(parameters);
                case "get":
                    return GetProducts();
                case "update":
                    return UpdateProduct(parameters);
                case "delete":
                    return DeleteProduct(parameters);
                case "back": // For "get back product"
                    return GetBackProduct(parameters);
                default:
                    return $"Invalid product operation: '{operation}'.\n" + GetHelpMessage();
            }
        }

        private string ProcessCategoryCommand(string operation, string parameters)
        {
            switch (operation)
            {
                case "create":
                    return CreateCategory(parameters);
                case "get":
                    return GetCategories();
                case "update":
                    return UpdateCategory(parameters);
                case "delete":
                    return DeleteCategory(parameters);
                case "back": // For "get back category"
                    return GetBackCategory(parameters);
                default:
                    return $"Invalid category operation: '{operation}'.\n" + GetHelpMessage();
            }
        }

        private string ProcessCustomerCommand(string operation, string parameters)
        {
            switch (operation)
            {
                case "create":
                    return CreateCustomer(parameters);
                case "get":
                    return GetCustomers();
                case "update":
                    return UpdateCustomer(parameters);
                case "delete":
                    return DeleteCustomer(parameters);
                case "back": // For "get back customer"
                    return GetBackCustomer(parameters);
                default:
                    return $"Invalid customer operation: '{operation}'.\n" + GetHelpMessage();
            }
        }

        private string ProcessSalesCommand(string operation, string parameters)
        {
            switch (operation)
            {
                case "create":
                    return CreateSales(parameters);
                case "get":
                    return GetSales();
                case "update":
                    return UpdateSales(parameters);
                case "delete":
                    return DeleteSales(parameters);
                case "back": // For "get back sales"
                    return GetBackSales(parameters);
                default:
                    return $"Invalid sales operation: '{operation}'.\n" + GetHelpMessage();
            }
        }

        // ... (You would add ProcessSupplierCommand similarly when you implement SupplierBLL) ...

        #endregion Entity Command Handlers


        #region Product Command Implementations (CreateProduct, GetProducts, UpdateProduct, DeleteProduct, GetBackProduct)

        private string CreateProduct(string parameters)
        {
            try
            {
                Dictionary<string, string> paramDict = ParseParameters(parameters);
                if (!paramDict.ContainsKey("name") || !paramDict.ContainsKey("category") || !paramDict.ContainsKey("price"))
                {
                    return "To create a product, please provide: name, category, and price.\n" +
                           "Example: create product name=Laptop, category=Electronics, price=1200";
                }

                ProductDetailDTO productDTO = new ProductDetailDTO()
                {
                    ProductName = paramDict["name"],
                    CategoryName = paramDict["category"],
                    price = ParseIntParameter(paramDict["price"], "price"),
                    // ... (other product properties can be added from parameters)
                };

                CategoryDetailDTO category = GetCategoryByName(productDTO.CategoryName);
                if (category == null)
                {
                    return $"Category '{productDTO.CategoryName}' not found. Please create the category first.";
                }
                productDTO.CategoryID = category.ID;

                bool result = _productBLL.Insert(productDTO);
                if (result)
                {
                    return $"Product '{productDTO.ProductName}' created successfully.";
                }
                else
                {
                    return $"Failed to create product '{productDTO.ProductName}'. Please check details and try again.";
                }
            }
            catch (FormatException fe)
            {
                return fe.Message;
            }
            catch (Exception ex)
            {
                return $"Error creating product: {ex.Message}";
            }
        }

        private string GetProducts()
        {
            try
            {
                ProductDTO productDTO = _productBLL.Select();
                if (productDTO.Products == null || productDTO.Products.Count == 0)
                {
                    return "No products found in the system.";
                }

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("--- Products List ---");
                foreach (var product in productDTO.Products)
                {
                    sb.AppendLine($"- {product.ProductName} (Category: {product.CategoryName}, Price: {product.price:C})");
                }
                sb.AppendLine("--- End of List ---");
                return sb.ToString();
            }
            catch (Exception ex)
            {
                return $"Error retrieving products: {ex.Message}";
            }
        }

        private string UpdateProduct(string parameters)
        {
            try
            {
                Dictionary<string, string> paramDict = ParseParameters(parameters);
                if (!paramDict.ContainsKey("id"))
                {
                    return "To update a product, please provide the product ID (id=...). \nExample: update product id=123, price=1500";
                }

                int productId = ParseIntParameter(paramDict["id"], "product ID");
                ProductDetailDTO productDTO = new ProductDetailDTO() { ProductID = productId };

                if (paramDict.ContainsKey("name")) productDTO.ProductName = paramDict["name"];
                if (paramDict.ContainsKey("category")) productDTO.CategoryName = paramDict["category"];
                if (paramDict.ContainsKey("price")) productDTO.price = ParseIntParameter(paramDict["price"], "price");
                if (paramDict.ContainsKey("stock")) productDTO.stockAmount = ParseIntParameter(paramDict["stock"], "stock");
                if (paramDict.ContainsKey("saleprice")) productDTO.Sale_Price = ParseFloatParameter(paramDict["saleprice"], "sale price");
                if (paramDict.ContainsKey("minqty")) productDTO.MinQty = ParseFloatParameter(paramDict["minqty"], "minimum quantity");
                if (paramDict.ContainsKey("maxdiscount")) productDTO.MaxDiscount = ParseFloatParameter(paramDict["maxdiscount"], "maximum discount");

                if (paramDict.ContainsKey("category"))
                {
                    CategoryDetailDTO category = GetCategoryByName(productDTO.CategoryName);
                    if (category == null)
                    {
                        return $"Category '{productDTO.CategoryName}' not found. Update failed.";
                    }
                    productDTO.CategoryID = category.ID;
                }

                bool result = _productBLL.Update(productDTO);
                if (result)
                {
                    return $"Product ID '{productId}' updated successfully.";
                }
                else
                {
                    return $"Failed to update product ID '{productId}'. Please check details and try again.";
                }
            }
            catch (FormatException fe)
            {
                return fe.Message;
            }
            catch (Exception ex)
            {
                return $"Error updating product: {ex.Message}";
            }
        }

        private string DeleteProduct(string parameters)
        {
            return ExecuteDeleteEntity("product", parameters); // Reusing generic delete
        }

        private string GetBackProduct(string parameters)
        {
            return ExecuteGetBackEntity("product", parameters); // Reusing generic get back
        }

        #endregion Product Command Implementations

        #region Category Command Implementations (CreateCategory, GetCategories, UpdateCategory, DeleteCategory, GetBackCategory)

        private string CreateCategory(string parameters)
        {
            try
            {
                Dictionary<string, string> paramDict = ParseParameters(parameters);
                if (!paramDict.ContainsKey("name"))
                {
                    return "To create a category, please provide: name.\n" +
                           "Example: create category name=Electronics";
                }

                CategoryDetailDTO categoryDTO = new CategoryDetailDTO()
                {
                    CategoryName = paramDict["name"],
                };

                bool result = _categoryBLL.Insert(categoryDTO);
                if (result)
                {
                    return $"Category '{categoryDTO.CategoryName}' created successfully.";
                }
                else
                {
                    return $"Failed to create category '{categoryDTO.CategoryName}'. Please check details and try again.";
                }
            }
            catch (FormatException fe)
            {
                return fe.Message;
            }
            catch (Exception ex)
            {
                return $"Error creating category: {ex.Message}";
            }
        }

        private string GetCategories()
        {
            try
            {
                CategoryDTO categoryDTO = _categoryBLL.Select();
                if (categoryDTO.Categories == null || categoryDTO.Categories.Count == 0)
                {
                    return "No categories found in the system.";
                }

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("--- Categories List ---");
                foreach (var category in categoryDTO.Categories)
                {
                    sb.AppendLine($"- {category.CategoryName}");
                }
                sb.AppendLine("--- End of List ---");
                return sb.ToString();
            }
            catch (Exception ex)
            {
                return $"Error retrieving categories: {ex.Message}";
            }
        }

        private string UpdateCategory(string parameters)
        {
            try
            {
                Dictionary<string, string> paramDict = ParseParameters(parameters);
                if (!paramDict.ContainsKey("id") || !paramDict.ContainsKey("name"))
                {
                    return "To update a category, please provide both ID and name.\n" +
                           "Example: update category id=1, name=NewCategoryName";
                }

                int categoryId = ParseIntParameter(paramDict["id"], "category ID");
                CategoryDetailDTO categoryDTO = new CategoryDetailDTO() { ID = categoryId, CategoryName = paramDict["name"] };

                bool result = _categoryBLL.Update(categoryDTO);
                if (result)
                {
                    return $"Category ID '{categoryId}' updated successfully.";
                }
                else
                {
                    return $"Failed to update category ID '{categoryId}'. Please check details and try again.";
                }
            }
            catch (FormatException fe)
            {
                return fe.Message;
            }
            catch (Exception ex)
            {
                return $"Error updating category: {ex.Message}";
            }
        }

        private string DeleteCategory(string parameters)
        {
            return ExecuteDeleteEntity("category", parameters); // Reusing generic delete
        }

        private string GetBackCategory(string parameters)
        {
            return ExecuteGetBackEntity("category", parameters); // Reusing generic get back
        }

        #endregion Category Command Implementations

        #region Customer Command Implementations (CreateCustomer, GetCustomers, UpdateCustomer, DeleteCustomer, GetBackCustomer)

        private string CreateCustomer(string parameters)
        {
            try
            {
                Dictionary<string, string> paramDict = ParseParameters(parameters);
                if (!paramDict.ContainsKey("name") || !paramDict.ContainsKey("phone") || !paramDict.ContainsKey("address"))
                {
                    return "To create a customer, please provide: name, phone, and address.\n" +
                           "Example: create customer name=John Doe, phone=1234567890, address=123 Main St";
                }

                CustomerDetailDTO customerDTO = new CustomerDetailDTO()
                {
                    CustomerName = paramDict["name"],
                    Cust_Phone = paramDict["phone"],
                    Cust_Address = paramDict["address"],
                    Notes = paramDict.ContainsKey("notes") ? paramDict["notes"] : null // Optional notes
                };

                bool result = _customerBLL.Insert(customerDTO);
                if (result)
                {
                    return $"Customer '{customerDTO.CustomerName}' created successfully.";
                }
                else
                {
                    return $"Failed to create customer '{customerDTO.CustomerName}'. Please check details and try again.";
                }
            }
            catch (FormatException fe)
            {
                return fe.Message;
            }
            catch (Exception ex)
            {
                return $"Error creating customer: {ex.Message}";
            }
        }

        private string GetCustomers()
        {
            try
            {
                CustomerDTO customerDTO = _customerBLL.Select();
                if (customerDTO.Customers == null || customerDTO.Customers.Count == 0)
                {
                    return "No customers found in the system.";
                }

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("--- Customers List ---");
                foreach (var customer in customerDTO.Customers)
                {
                    sb.AppendLine($"- {customer.CustomerName} (Phone: {customer.Cust_Phone}, Address: {customer.Cust_Address})");
                }
                sb.AppendLine("--- End of List ---");
                return sb.ToString();
            }
            catch (Exception ex)
            {
                return $"Error retrieving customers: {ex.Message}";
            }
        }

        private string UpdateCustomer(string parameters)
        {
            try
            {
                Dictionary<string, string> paramDict = ParseParameters(parameters);
                if (!paramDict.ContainsKey("id"))
                {
                    return "To update a customer, please provide the customer ID (id=...). \n" +
                           "Example: update customer id=1, name=Jane Doe, phone=9876543210";
                }

                int customerId = ParseIntParameter(paramDict["id"], "customer ID");
                CustomerDetailDTO customerDTO = new CustomerDetailDTO() { ID = customerId };

                if (paramDict.ContainsKey("name")) customerDTO.CustomerName = paramDict["name"];
                if (paramDict.ContainsKey("phone")) customerDTO.Cust_Phone = paramDict["phone"];
                if (paramDict.ContainsKey("address")) customerDTO.Cust_Address = paramDict["address"];
                if (paramDict.ContainsKey("notes")) customerDTO.Notes = paramDict["notes"];


                bool result = _customerBLL.Update(customerDTO);
                if (result)
                {
                    return $"Customer ID '{customerId}' updated successfully.";
                }
                else
                {
                    return $"Failed to update customer ID '{customerId}'. Please check details and try again.";
                }
            }
            catch (FormatException fe)
            {
                return fe.Message;
            }
            catch (Exception ex)
            {
                return $"Error updating customer: {ex.Message}";
            }
        }

        private string DeleteCustomer(string parameters)
        {
            return ExecuteDeleteEntity("customer", parameters); // Reusing generic delete
        }

        private string GetBackCustomer(string parameters)
        {
            return ExecuteGetBackEntity("customer", parameters); // Reusing generic get back
        }

        #endregion Customer Command Implementations

        #region Sales Command Implementations (CreateSales, GetSales, UpdateSales, DeleteSales, GetBackSales)

        private string CreateSales(string parameters)
        {
            try
            {
                Dictionary<string, string> paramDict = ParseParameters(parameters);
                if (!paramDict.ContainsKey("productid") || !paramDict.ContainsKey("customerid") || !paramDict.ContainsKey("amount") || !paramDict.ContainsKey("price"))
                {
                    return "To create a sale, please provide: productid, customerid, amount, and price.\n" +
                           "Example: create sales productid=1, customerid=2, amount=5, price=100";
                }

                SalesDetailDTO salesDTO = new SalesDetailDTO()
                {
                    ProductID = ParseIntParameter(paramDict["productid"], "product ID"),
                    CustomerID = ParseIntParameter(paramDict["customerid"], "customer ID"),
                    SalesAmount = ParseIntParameter(paramDict["amount"], "sales amount"),
                    Price = ParseIntParameter(paramDict["price"], "price"),
                    SalesDate = DateTime.Now, // Default to current date/time
                    // ... (you might add other sales properties from parameters if needed)
                };

                bool result = _salesBLL.Insert(salesDTO);
                if (result)
                {
                    return $"Sale recorded successfully for Product ID '{salesDTO.ProductID}', Customer ID '{salesDTO.CustomerID}'.";
                }
                else
                {
                    return $"Failed to record sale. Please check details and try again.";
                }
            }
            catch (FormatException fe)
            {
                return fe.Message;
            }
            catch (Exception ex)
            {
                return $"Error creating sale: {ex.Message}";
            }
        }

        private string GetSales()
        {
            try
            {
                SalesDTO salesDTO = _salesBLL.Select();
                if (salesDTO.Sales == null || salesDTO.Sales.Count == 0)
                {
                    return "No sales records found in the system.";
                }

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("--- Sales List ---");
                foreach (var sale in salesDTO.Sales)
                {
                    sb.AppendLine($"- Sale ID: {sale.SalesID}, Product: {sale.ProductName}, Customer: {sale.CustomerName}, Amount: {sale.SalesAmount}, Price: {sale.Price:C}, Date: {sale.SalesDate.ToShortDateString()}");
                }
                sb.AppendLine("--- End of List ---");
                return sb.ToString();
            }
            catch (Exception ex)
            {
                return $"Error retrieving sales records: {ex.Message}";
            }
        }

        private string UpdateSales(string parameters)
        {
            try
            {
                Dictionary<string, string> paramDict = ParseParameters(parameters);
                if (!paramDict.ContainsKey("id"))
                {
                    return "To update a sale, please provide the sale ID (id=...). \n" +
                           "Example: update sales id=1, amount=10, price=120";
                }

                int salesId = ParseIntParameter(paramDict["id"], "sale ID");
                SalesDetailDTO salesDTO = new SalesDetailDTO() { SalesID = salesId };

                if (paramDict.ContainsKey("amount")) salesDTO.SalesAmount = ParseIntParameter(paramDict["amount"], "sales amount");
                if (paramDict.ContainsKey("price")) salesDTO.Price = ParseIntParameter(paramDict["price"], "price");
                if (paramDict.ContainsKey("date")) salesDTO.SalesDate = DateTime.Parse(paramDict["date"]); // Consider date parsing carefully

                bool result = _salesBLL.Update(salesDTO);
                if (result)
                {
                    return $"Sale ID '{salesId}' updated successfully.";
                }
                else
                {
                    return $"Failed to update sale ID '{salesId}'. Please check details and try again.";
                }
            }
            catch (FormatException fe)
            {
                return fe.Message;
            }
            catch (Exception ex)
            {
                return $"Error updating sale: {ex.Message}";
            }
        }

        private string DeleteSales(string parameters)
        {
            return ExecuteDeleteEntity("sales", parameters); // Reusing generic delete
        }

        private string GetBackSales(string parameters)
        {
            return ExecuteGetBackEntity("sales", parameters); // Reusing generic get back
        }

        #endregion Sales Command Implementations

        #region Generic Helper Functions (ExecuteDeleteEntity, ExecuteGetBackEntity, GetCategoryByName, ParseParameters, ParseIntParameter, ParseFloatParameter, GetHelpMessage)

        private string ExecuteDeleteEntity(string entityType, string parameters)
        {
            try
            {
                Dictionary<string, string> paramDict = ParseParameters(parameters);
                if (!paramDict.ContainsKey("id"))
                {
                    return $"To delete a {entityType}, please provide the {entityType} ID (id=...). \nExample: delete {entityType} id=123";
                }

                int entityId = ParseIntParameter(paramDict["id"], $"{entityType} ID");

                bool result = false;
                switch (entityType)
                {
                    case "product":
                        result = _productBLL.Delete(new ProductDetailDTO() { ProductID = entityId });
                        break;
                    case "category":
                        result = _categoryBLL.Delete(new CategoryDetailDTO() { ID = entityId });
                        break;
                    case "customer":
                        result = _customerBLL.Delete(new CustomerDetailDTO() { ID = entityId });
                        break;
                    case "sales":
                        result = _salesBLL.Delete(new SalesDetailDTO() { SalesID = entityId });
                        break;
                    //case "supplier": // Add case for supplier when you implement SupplierBLL
                    //    result = _supplierBLL.Delete(new SupplierDetailDTO() { ID = entityId });
                    //    break;
                    default:
                        return $"Deletion not supported for entity type: '{entityType}'.";
                }


                if (result)
                {
                    return $"{entityType} ID '{entityId}' deleted successfully.";
                }
                else
                {
                    return $"Failed to delete {entityType} ID '{entityId}'. {entityType} may not exist or an error occurred.";
                }
            }
            catch (FormatException fe)
            {
                return fe.Message;
            }
            catch (Exception ex)
            {
                return $"Error deleting {entityType}: {ex.Message}";
            }
        }

        private string ExecuteGetBackEntity(string entityType, string parameters)
        {
            try
            {
                Dictionary<string, string> paramDict = ParseParameters(parameters);
                if (!paramDict.ContainsKey("id"))
                {
                    return $"To restore a deleted {entityType}, provide the {entityType} ID (id=...). \nExample: get back {entityType} id=123";
                }

                int entityId = ParseIntParameter(paramDict["id"], $"{entityType} ID");
                bool result = false;

                switch (entityType)
                {
                    case "product":
                        result = _productBLL.GetBack(new ProductDetailDTO() { ProductID = entityId });
                        break;
                    case "category":
                        result = _categoryBLL.GetBack(new CategoryDetailDTO() { ID = entityId });
                        break;
                    case "customer":
                        result = _customerBLL.GetBack(new CustomerDetailDTO() { ID = entityId });
                        break;
                    case "sales":
                        result = _salesBLL.GetBack(new SalesDetailDTO() { SalesID = entityId });
                        break;
                    //case "supplier": // Add case for supplier when you implement SupplierBLL
                    //    result = _supplierBLL.GetBack(new SupplierDetailDTO() { ID = entityId });
                    //    break;
                    default:
                        return $"Restore operation not supported for entity type: '{entityType}'.";
                }


                if (result)
                {
                    return $"{entityType} ID '{entityId}' restored successfully.";
                }
                else
                {
                    return $"Failed to restore {entityType} ID '{entityId}'. {entityType} may not exist or was not deleted.";
                }
            }
            catch (FormatException fe)
            {
                return fe.Message;
            }
            catch (Exception ex)
            {
                return $"Error restoring {entityType}: {ex.Message}";
            }
        }


        private CategoryDetailDTO GetCategoryByName(string categoryName)
        {
            CategoryDTO categoryDTO = _categoryBLL.Select();
            if (categoryDTO.Categories != null)
            {
                return categoryDTO.Categories.FirstOrDefault(c => c.CategoryName.ToLower() == categoryName.ToLower());
            }
            return null;
        }

        private Dictionary<string, string> ParseParameters(string parameters)
        {
            Dictionary<string, string> paramDict = ParseParametersBase(parameters);
            return paramDict;
        }

        private Dictionary<string, string> ParseParametersBase(string parameters)
        {
            Dictionary<string, string> paramDict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            string[] pairs = parameters.Split(',');
            foreach (var pair in pairs)
            {
                string[] parts = pair.Trim().Split('=');
                if (parts.Length == 2)
                {
                    paramDict[parts[0].Trim()] = parts[1].Trim();
                }
            }
            return paramDict;
        }

        private int ParseIntParameter(string value, string paramName)
        {
            if (int.TryParse(value, out int result))
            {
                return result;
            }
            throw new FormatException($"Invalid format for '{paramName}'. Please provide a valid integer value.");
        }

        private float ParseFloatParameter(string value, string paramName)
        {
            if (float.TryParse(value, out float result))
            {
                return result;
            }
            throw new FormatException($"Invalid format for '{paramName}'. Please provide a valid numeric value.");
        }

        private string GetHelpMessage()
        {
            return "Sorry, I did not understand that command.\n\n" +
                   "Available commands are:\n" +
                   "- create product name=ProductName, category=CategoryName, price=Price, ...\n" +
                   "- get products\n" +
                   "- update product id=ProductID, name=NewProductName, price=NewPrice, ...\n" +
                   "- delete product id=ProductID\n" +
                   "- get back product id=ProductID\n" +
                   "- create category name=CategoryName\n" +
                   "- get categories\n" +
                   "- update category id=CategoryID, name=NewCategoryName\n" +
                   "- delete category id=CategoryID\n" +
                   "- get back category id=CategoryID\n" +
                   "- create customer name=CustomerName, phone=PhoneNumber, address=Address, [notes=Notes]\n" +
                   "- get customers\n" +
                   "- update customer id=CustomerID, name=NewCustomerName, phone=NewPhoneNumber, address=NewAddress, [notes=NewNotes]\n" +
                   "- delete customer id=CustomerID\n" +
                   "- get back customer id=CustomerID\n" +
                   "- create sales productid=ProductID, customerid=CustomerID, amount=SalesAmount, price=SalesPrice\n" +
                   "- get sales\n" +
                   "- update sales id=SalesID, amount=NewSalesAmount, price=NewSalesPrice, [date=SalesDate]\n" +
                   "- delete sales id=SalesID\n" +
                   "- get back sales id=SalesID";
        }

        #endregion Generic Helper Functions

        #endregion Chatbot Logic


        private void CrudChatbotForm_Load(object sender, EventArgs e)
        {
            txtInput.KeyPress += txtInput_KeyPress;
        }

        private void txtInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true; // Prevent beep sound
                SendCommand();
            }
        }
        private void SendCommand()
        {
            string userInput = txtInput.Text.Trim();
            if (string.IsNullOrEmpty(userInput) || (txtInput.ForeColor == Color.Gray && userInput == "Type your command here (e.g., create product)"))
            {
                return; // Do nothing if input is empty or is placeholder text
            }

            AppendChatMessage("User", userInput);
            txtInput.Clear();
            txtInput.ForeColor = Color.Black; // Reset text color

            string response = ProcessCommand(userInput);
            AppendChatMessage("System", response);
        }
        private void AppendChatMessage(string sender, string message)
        {
            txtChatOutput.AppendText($"{sender}: {message}\r\n");
            txtChatOutput.ScrollToCaret(); // Auto-scroll to the latest message
        }
    }
}
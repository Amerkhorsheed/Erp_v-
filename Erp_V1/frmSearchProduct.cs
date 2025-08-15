using DevExpress.XtraEditors;
using DevExpress.XtraBars.Docking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;

namespace Erp_V1
{
    public partial class frmSearchProduct : XtraForm
    {
        private const string DefaultSkin = "Office 2019 Colorful";
        private readonly ProductBLL _productBLL;
        private List<ProductDetailDTO> _allProducts;

        public frmSearchProduct()
        {
            InitializeComponent();

            // Set a consistent DevExpress skin
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(DefaultSkin);

            _productBLL = new ProductBLL();
            LoadProducts();
        }

        /// <summary>
        /// Load and cache the products at startup.
        /// </summary>
        private void LoadProducts()
        {
            try
            {
                var productDTO = _productBLL.Select();
                _allProducts = productDTO?.Products ?? new List<ProductDetailDTO>();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error loading products:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _allProducts = new List<ProductDetailDTO>();
            }
        }

        /// <summary>
        /// Searches the cached products when the search button is clicked.
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(keyword))
            {
                XtraMessageBox.Show("Enter a search keyword.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var filtered = _allProducts
                .Where(p => p.ProductName.ToLower().Contains(keyword) ||
                            p.CategoryName.ToLower().Contains(keyword))
                .ToList();

            lstSearchResults.Items.Clear();

            if (filtered.Any())
            {
                foreach (var product in filtered)
                {
                    lstSearchResults.Items.Add(product.ProductName);
                }
            }
            else
            {
                XtraMessageBox.Show("No products match the search criteria.",
                    "Search Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// When a search result is selected, show its details.
        /// </summary>
        private void lstSearchResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstSearchResults.SelectedItem == null)
                return;

            string selectedName = lstSearchResults.SelectedItem.ToString();
            var selectedProduct = _allProducts.FirstOrDefault(
                p => p.ProductName.Equals(selectedName, StringComparison.CurrentCultureIgnoreCase));

            if (selectedProduct != null)
            {
                lblProductNameValue.Text = selectedProduct.ProductName;
                lblCategoryNameValue.Text = selectedProduct.CategoryName;
                lblPriceValue.Text = selectedProduct.price.ToString("C");
                lblStockAmountValue.Text = selectedProduct.stockAmount.ToString();
            }
            else
            {
                XtraMessageBox.Show("Product details not found.",
                    "Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

using Erp_V1.BLL;
using Erp_V1.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Erp_V1
{
    public partial class frmProductShortagePredictor : Form
    {
        private readonly ProductBLL _productBLL;
        private readonly LinearRegressionModel _linearRegressionModel;

        public frmProductShortagePredictor()
        {
            InitializeComponent();
            _productBLL = new ProductBLL();
            _linearRegressionModel = new LinearRegressionModel();
            LoadProductList();
        }

        private void LoadProductList()
        {
            try
            {
                // Fetch products from BLL
                var result = _productBLL.Select();
                if (result == null || result.Products == null || result.Products.Count == 0)
                {
                    MessageBox.Show("No products found.");
                    return;
                }

                List<ProductDetailDTO> products = result.Products;

                // Train the Linear Regression Model
                if (products.Count > 0)
                {
                    _linearRegressionModel.Train(products);
                }
                else
                {
                    MessageBox.Show("No data available for training.");
                    return;
                }

                // Predict stock amount for each product
                foreach (var product in products)
                {
                    if (product != null)
                    {
                        product.stockAmount = _linearRegressionModel.Predict(product);
                    }
                }

                // Bind the product list to the DataGridView
                dgvProductList.DataSource = products;
                MessageBox.Show($"Loaded {products.Count} products successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading product list: {ex.Message}");
            }
        }
    }
}

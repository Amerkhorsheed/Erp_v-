
using System;
using System.Windows.Forms;
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;

namespace Erp_V1
{
    public partial class frmRecommendation : Form
    {
        private RecommendationEngineAI _recommendationEngine;

        public frmRecommendation()
        {
            InitializeComponent();
            InitializeRecommendationEngine();
        }

        private void InitializeRecommendationEngine()
        {
            SalesBLL salesBLL = new SalesBLL();
            ProductBLL productBLL = new ProductBLL();
            _recommendationEngine = new RecommendationEngineAI(salesBLL, productBLL);
        }

        private void frmRecommendation_Load(object sender, EventArgs e)
        {
            LoadCustomerNames();
        }

        private void LoadCustomerNames()
        {
            try
            {
                var customerBLL = new CustomerBLL();
                var customers = customerBLL.Select().Customers;
                cboCustomerNames.DataSource = customers;
                cboCustomerNames.DisplayMember = "CustomerName";
                cboCustomerNames.ValueMember = "ID";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customer names: {ex.Message}");
            }
        }

        private void btnGetRecommendations_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedCustomerName = cboCustomerNames.Text;
                var recommendations = _recommendationEngine.GetRecommendationsForCustomer(selectedCustomerName);

                dgvRecommendations.DataSource = recommendations;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}

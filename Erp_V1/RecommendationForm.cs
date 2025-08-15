using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;

namespace Erp_V1
{
    public partial class RecommendationForm : Form
    {
        private CustomerBLL customerBLL = new CustomerBLL();
        private RecommendationEngineALS recommendationEngine = new RecommendationEngineALS();

        public RecommendationForm()
        {
            InitializeComponent();
            LoadCustomers();
        }

        private void LoadCustomers()
        {
            var customers = customerBLL.Select().Customers;
            cmbCustomer.DataSource = customers;
            cmbCustomer.DisplayMember = "CustomerName";
            cmbCustomer.ValueMember = "CustomerName";
        }

        private void btnGetRecommendations_Click(object sender, EventArgs e)
        {
            try
            {
                var customerName = cmbCustomer.SelectedValue.ToString();
                var recommendations = recommendationEngine.GetRecommendationsForCustomer(customerName);

                if (recommendations.Any())
                {
                    lblNoRecommendations.Visible = false;
                    dgvRecommendations.DataSource = recommendations;
                }
                else
                {
                    lblNoRecommendations.Visible = true;
                    dgvRecommendations.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RecommendationForm_Load(object sender, EventArgs e)
        {

        }
    }
}

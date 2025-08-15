// File: FrmRecommendations.cs
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Erp_V1.BLL;
using Erp_V1.DAL.DAO;
using Erp_V1.DAL.DTO;
using Erp_V1.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging.Abstractions;

namespace Erp_V1
{
    /// <summary>
    /// WinForms UI for selecting a customer and viewing product recommendations.
    /// </summary>
    public partial class FrmRecommendations : Form
    {
        private readonly RecommendationService _recommendationService;
        private readonly CustomerBLL _customerBLL;
        private readonly BindingSource _customerBindingSource = new BindingSource();
        private readonly BindingSource _recommendationBindingSource = new BindingSource();

        /// <summary>
        /// Default constructor. Instantiates dependencies with default DAOs, an in-memory cache, and a null logger.
        /// </summary>
        public FrmRecommendations()
            : this(
                new RecommendationService(
                    new RecommendationBLL(new SalesDAO(), new ProductDAO(), NullLogger<RecommendationBLL>.Instance),
                    new MemoryCache(new MemoryCacheOptions()),
                    NullLogger<RecommendationService>.Instance),
                new CustomerBLL())
        {
        }

        /// <summary>
        /// Constructor that accepts injected services for unit testing or DI integration.
        /// </summary>
        /// <param name="recommendationService">Service for fetching recommendations.</param>
        /// <param name="customerBLL">Business logic for loading customers.</param>
        public FrmRecommendations(
            RecommendationService recommendationService,
            CustomerBLL customerBLL)
        {
            _recommendationService = recommendationService
                                     ?? throw new ArgumentNullException(nameof(recommendationService));
            _customerBLL = customerBLL
                           ?? throw new ArgumentNullException(nameof(customerBLL));

            InitializeComponent();
            InitializeDataGridView();
            AttachEventHandlers();
        }

        #region ───── Initialization ─────

        /// <summary>
        /// Sets up DataGridView columns, styles, and binding source.
        /// </summary>
        private void InitializeDataGridView()
        {
            dgvRecommendations.AutoGenerateColumns = false;
            dgvRecommendations.DataSource = _recommendationBindingSource;

            dgvRecommendations.Columns.Clear();

            dgvRecommendations.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(ProductDetailDTO.ProductID),
                HeaderText = "Product ID",
                Width = 60,
                ReadOnly = true
            });
            dgvRecommendations.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(ProductDetailDTO.ProductName),
                HeaderText = "Product Name",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true
            });
            dgvRecommendations.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(ProductDetailDTO.CategoryName),
                HeaderText = "Category",
                Width = 140,
                ReadOnly = true
            });
            dgvRecommendations.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(ProductDetailDTO.price),
                HeaderText = "Price",
                Width = 80,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            });
            dgvRecommendations.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(ProductDetailDTO.stockAmount),
                HeaderText = "In Stock",
                Width = 70,
                ReadOnly = true
            });

            dgvRecommendations.AllowUserToAddRows = false;
            dgvRecommendations.AllowUserToDeleteRows = false;
            dgvRecommendations.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRecommendations.MultiSelect = false;
            dgvRecommendations.ReadOnly = true;
            dgvRecommendations.BackgroundColor = Color.White;
            dgvRecommendations.GridColor = Color.LightGray;
        }

        /// <summary>
        /// Wires up form-level event handlers.
        /// </summary>
        private void AttachEventHandlers()
        {
            Load += FrmRecommendations_Load;
            btnFetchRecommendations.Click += BtnFetchRecommendations_Click;
        }

        #endregion

        #region ───── Form Events ─────

        /// <summary>
        /// Asynchronously loads all customers into the ComboBox when the form is shown.
        /// </summary>
        private async void FrmRecommendations_Load(object sender, EventArgs e)
        {
            SetUiBusy("Loading customers...");
            btnFetchRecommendations.Enabled = false;
            cmbCustomers.DataSource = null;

            try
            {
                List<CustomerDetailDTO> customers =
                    await Task.Run(() => _customerBLL.Select()?.Customers);

                if (customers == null || customers.Count == 0)
                {
                    ShowInformation("No customers found in the system.");
                    lblStatus.Text = "No customers available";
                    return;
                }

                _customerBindingSource.DataSource = customers;
                cmbCustomers.DataSource = _customerBindingSource;
                cmbCustomers.DisplayMember = nameof(CustomerDetailDTO.CustomerName);
                cmbCustomers.ValueMember = nameof(CustomerDetailDTO.CustomerName);

                lblStatus.Text = "Ready";
                btnFetchRecommendations.Enabled = true;
            }
            catch (Exception ex)
            {
                ShowError("Failed to load customers", ex);
                lblStatus.Text = "Error loading customers";
            }
            finally
            {
                ResetUi();
            }
        }

        /// <summary>
        /// Click handler for the "Get Recommendations" button. Fetches recommendations asynchronously.
        /// </summary>
        private async void BtnFetchRecommendations_Click(object sender, EventArgs e)
        {
            string selectedCustomer = cmbCustomers.SelectedValue as string;
            if (string.IsNullOrWhiteSpace(selectedCustomer))
            {
                ShowWarning("Please select a customer from the list.");
                return;
            }

            SetUiBusy("Fetching recommendations...");
            btnFetchRecommendations.Enabled = false;
            _recommendationBindingSource.Clear();
            dgvRecommendations.Refresh();

            try
            {
                var recs = await Task.Run(
                    () => _recommendationService.GetRecommendationsAsync(selectedCustomer));

                if (recs == null || recs.Count == 0)
                {
                    ShowInformation($"No recommendations available for \"{selectedCustomer}\".");
                    lblStatus.Text = "No recommendations";
                    return;
                }

                _recommendationBindingSource.DataSource = recs;
                dgvRecommendations.AutoResizeColumns();
                lblStatus.Text = "Recommendations loaded";
            }
            catch (Exception ex)
            {
                ShowError("Error retrieving recommendations", ex);
                lblStatus.Text = "Error loading recommendations";
            }
            finally
            {
                ResetUi();
                btnFetchRecommendations.Enabled = true;
            }
        }

        #endregion

        #region ───── UI Helpers ─────

        /// <summary>
        /// Disables input controls and shows a wait cursor with a status message.
        /// </summary>
        private void SetUiBusy(string statusMessage)
        {
            Cursor = Cursors.WaitCursor;
            lblStatus.Text = statusMessage;
            cmbCustomers.Enabled = false;
            btnFetchRecommendations.Enabled = false;
        }

        /// <summary>
        /// Resets the cursor and re-enables input controls.
        /// </summary>
        private void ResetUi()
        {
            Cursor = Cursors.Default;
            cmbCustomers.Enabled = true;
        }

        /// <summary>
        /// Displays an error dialog with detailed exception information.
        /// </summary>
        private void ShowError(string title, Exception ex)
        {
            MessageBox.Show(
                $"{title}:\n{ex.Message}",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        /// <summary>
        /// Displays an informational message dialog.
        /// </summary>
        private void ShowInformation(string message)
        {
            MessageBox.Show(
                message,
                "Information",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        /// <summary>
        /// Displays a warning message dialog.
        /// </summary>
        private void ShowWarning(string message)
        {
            MessageBox.Show(
                message,
                "Warning",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }

        #endregion
    }
}

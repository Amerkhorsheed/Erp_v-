// File: DeliveryListForm.cs
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Erp_V1
{
    public partial class DeliveryListForm : MaterialForm
    {
        private readonly DeliveryBLL _bll = new DeliveryBLL();
        private List<DeliveryDetailDTO> _allDeliveries;

        public DeliveryListForm()
        {
            InitializeComponent();
            InitializeMaterialSkin();
        }

        private void InitializeMaterialSkin()
        {
            var mgr = MaterialSkinManager.Instance;
            mgr.AddFormToManage(this);
            mgr.Theme = MaterialSkinManager.Themes.LIGHT;
            mgr.ColorScheme = new ColorScheme(
                Primary.BlueGrey800, Primary.BlueGrey900,
                Primary.BlueGrey500, Accent.LightBlue200,
                TextShade.WHITE
            );
        }

        private void DeliveryListForm_Load(object sender, EventArgs e)
        {
            ConfigureGrid();
            LoadData();
        }

        private void ConfigureGrid()
        {
            dgvDeliveries.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDeliveries.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDeliveries.MultiSelect = false;
            dgvDeliveries.ReadOnly = true;
            dgvDeliveries.AllowUserToAddRows = false;
            dgvDeliveries.RowHeadersVisible = false;
        }

        private void LoadData()
        {
            _allDeliveries = _bll.Select().Deliveries;
            dgvDeliveries.DataSource = _allDeliveries;
            ApplyColumnSettings();
        }

        private void ApplyColumnSettings()
        {
            // Hide ID columns
            if (dgvDeliveries.Columns["DeliveryID"] != null) dgvDeliveries.Columns["DeliveryID"].Visible = false;
            if (dgvDeliveries.Columns["SalesID"] != null) dgvDeliveries.Columns["SalesID"].Visible = false;
            if (dgvDeliveries.Columns["DeliveryPersonID"] != null) dgvDeliveries.Columns["DeliveryPersonID"].Visible = false;

            // Set user-friendly header text
            if (dgvDeliveries.Columns["CustomerName"] != null) dgvDeliveries.Columns["CustomerName"].HeaderText = "Customer Name";
            if (dgvDeliveries.Columns["DeliveryPersonName"] != null) dgvDeliveries.Columns["DeliveryPersonName"].HeaderText = "Assigned To";
            if (dgvDeliveries.Columns["Address"] != null) dgvDeliveries.Columns["Address"].HeaderText = "Delivery Address";
            if (dgvDeliveries.Columns["Status"] != null) dgvDeliveries.Columns["Status"].HeaderText = "Status";
            if (dgvDeliveries.Columns["AssignedDate"] != null) dgvDeliveries.Columns["AssignedDate"].HeaderText = "Assigned Date";
            if (dgvDeliveries.Columns["DeliveredDate"] != null) dgvDeliveries.Columns["DeliveredDate"].HeaderText = "Delivered Date";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvDeliveries.CurrentRow == null)
            {
                MaterialMessageBox.Show("Please select a delivery to update.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var dto = (DeliveryDetailDTO)dgvDeliveries.CurrentRow.DataBoundItem;
            using (var dlg = new DeliveryDetailForm(dto))
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    LoadData(); // Refresh data after an update
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDeliveries.CurrentRow == null)
            {
                MaterialMessageBox.Show("Please select a delivery to delete.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var dto = (DeliveryDetailDTO)dgvDeliveries.CurrentRow.DataBoundItem;
            var confirm = MaterialMessageBox.Show(
                $"Are you sure you want to delete the delivery for '{dto.CustomerName}'?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                if (_bll.Delete(dto))
                {
                    MaterialMessageBox.Show("Delivery deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                else
                {
                    MaterialMessageBox.Show("Failed to delete the delivery.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            searchTextBox.Clear();
            LoadData();
        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            var term = searchTextBox.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(term))
            {
                dgvDeliveries.DataSource = _allDeliveries;
            }
            else
            {
                dgvDeliveries.DataSource = _allDeliveries
                  .Where(d => d.CustomerName.ToLower().Contains(term) ||
                              d.Status.ToLower().Contains(term) ||
                              d.DeliveryPersonName.ToLower().Contains(term))
                  .ToList();
            }
            ApplyColumnSettings();
        }
    }
}
// File: CustomerCampaignForm.cs
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
    public partial class CustomerCampaignForm : MaterialForm
    {
        private readonly CustomerCampaignBLL _bll = new CustomerCampaignBLL();
        private List<CustomerCampaignDTO> _allCampaigns;

        public CustomerCampaignForm()
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
                Primary.DeepPurple500, Primary.DeepPurple700,
                Primary.DeepPurple100, Accent.Pink200,
                TextShade.WHITE
            );
        }

        private void CustomerCampaignForm_Load(object sender, EventArgs e)
        {
            ConfigureGrid();
            LoadData();
        }

        private void ConfigureGrid()
        {
            dgvCampaigns.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCampaigns.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCampaigns.MultiSelect = false;
            dgvCampaigns.ReadOnly = true;
            dgvCampaigns.AllowUserToAddRows = false;
            dgvCampaigns.RowHeadersVisible = false;
        }

        private void LoadData()
        {
            _allCampaigns = _bll.Select().Campaigns;
            dgvCampaigns.DataSource = _allCampaigns;
            ApplyColumnSettings();
        }

        private void ApplyColumnSettings()
        {
            if (dgvCampaigns.Columns["ID"] != null) dgvCampaigns.Columns["ID"].Visible = false;
            if (dgvCampaigns.Columns["CustomerID"] != null) dgvCampaigns.Columns["CustomerID"].Visible = false;
            if (dgvCampaigns.Columns["CustomerName"] != null) dgvCampaigns.Columns["CustomerName"].HeaderText = "Customer Name";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var dlg = new CustomerCampaignAddForm())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                    LoadData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvCampaigns.CurrentRow == null)
            {
                MaterialMessageBox.Show("Select a campaign to edit.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var dto = (CustomerCampaignDTO)dgvCampaigns.CurrentRow.DataBoundItem;
            using (var dlg = new CustomerCampaignAddForm(dto))
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                    LoadData();
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCampaigns.CurrentRow == null) return;
            var dto = (CustomerCampaignDTO)dgvCampaigns.CurrentRow.DataBoundItem;
            var confirm = MaterialMessageBox.Show(
                $"Delete campaign '{dto.CampaignName}' for '{dto.CustomerName}'?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.Yes)
            {
                await _bll.DeleteAsync(dto);
                LoadData();
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
            dgvCampaigns.DataSource = string.IsNullOrEmpty(term)
                ? _allCampaigns
                : _allCampaigns
                  .Where(c => !string.IsNullOrEmpty(c.CampaignName) &&
                              c.CustomerName.ToLower().Contains(term) ||
                              c.CampaignName.ToLower().Contains(term))
                  .ToList();
            ApplyColumnSettings();
        }
    }
}

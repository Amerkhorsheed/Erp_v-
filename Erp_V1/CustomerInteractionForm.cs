// File: CustomerInteractionForm.cs
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace Erp_V1
{
    public partial class CustomerInteractionForm : MaterialForm
    {
        private readonly CustomerInteractionBLL _bll = new CustomerInteractionBLL();
        private List<CustomerInteractionDTO> _allInteractions;

        public CustomerInteractionForm()
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
                Primary.Teal500, Primary.Teal700,
                Primary.Teal100, Accent.LightBlue200,
                TextShade.WHITE
            );
        }

        private void CustomerInteractionForm_Load(object sender, EventArgs e)
        {
            ConfigureGrid();
            LoadData(); // synchronous
        }

        private void ConfigureGrid()
        {
            dgvInteractions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvInteractions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInteractions.MultiSelect = false;
            dgvInteractions.ReadOnly = true;
            dgvInteractions.AllowUserToAddRows = false;
            dgvInteractions.RowHeadersVisible = false;
            dgvInteractions.EnableHeadersVisualStyles = false;
            dgvInteractions.BackgroundColor = Color.White;
            dgvInteractions.BorderStyle = BorderStyle.None;
            dgvInteractions.GridColor = Color.Gainsboro;
        }

        private void LoadData()
        {
            // use synchronous Select()
            _allInteractions = _bll.Select().Interactions;
            dgvInteractions.DataSource = _allInteractions;
            ApplyColumnSettings();
        }

        private void ApplyColumnSettings()
        {
            if (dgvInteractions.Columns["ID"] != null) dgvInteractions.Columns["ID"].Visible = false;
            if (dgvInteractions.Columns["CustomerID"] != null) dgvInteractions.Columns["CustomerID"].Visible = false;
            if (dgvInteractions.Columns["CustomerName"] != null) dgvInteractions.Columns["CustomerName"].HeaderText = "Customer Name";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var dlg = new CustomerInteractionAddForm())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                    LoadData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvInteractions.CurrentRow == null)
            {
                MaterialMessageBox.Show("Select an interaction to edit.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var dto = (CustomerInteractionDTO)dgvInteractions.CurrentRow.DataBoundItem;
            using (var dlg = new CustomerInteractionAddForm(dto))
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                    LoadData();
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvInteractions.CurrentRow == null) return;

            var dto = (CustomerInteractionDTO)dgvInteractions.CurrentRow.DataBoundItem;
            var confirm = MaterialMessageBox.Show(
                $"Delete interaction for '{dto.CustomerName}' ({dto.Type})?",
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
            dgvInteractions.DataSource = string.IsNullOrEmpty(term)
                ? _allInteractions
                : _allInteractions
                  .Where(i => !string.IsNullOrEmpty(i.CustomerName)
                           && i.CustomerName.ToLower().Contains(term))
                  .ToList();
            ApplyColumnSettings();
        }
    }
}

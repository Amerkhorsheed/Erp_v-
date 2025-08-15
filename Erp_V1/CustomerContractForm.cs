// File: CustomerContractForm.cs
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Erp_V1
{
    public partial class CustomerContractForm : MaterialForm
    {
        private readonly CustomerContractBLL _bll = new CustomerContractBLL();
        private List<CustomerContractDTO> _allContracts;

        public CustomerContractForm()
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
                Primary.Blue500, Primary.Blue700,
                Primary.Blue100, Accent.LightBlue200,
                TextShade.WHITE
            );
        }

        private async void CustomerContractForm_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            var result = await _bll.SelectAsync();
            _allContracts = result.Contracts;
            dgvContracts.DataSource = _allContracts;

            // hide internal columns:
            if (dgvContracts.Columns["ID"] != null) dgvContracts.Columns["ID"].Visible = false;
            if (dgvContracts.Columns["IsDeleted"] != null) dgvContracts.Columns["IsDeleted"].Visible = false;
            if (dgvContracts.Columns["DeletedDate"] != null) dgvContracts.Columns["DeletedDate"].Visible = false;

            // other tweaks:
            if (dgvContracts.Columns["CustomerID"] != null) dgvContracts.Columns["CustomerID"].Visible = false;
            if (dgvContracts.Columns["CustomerName"] != null) dgvContracts.Columns["CustomerName"].HeaderText = "Customer";
        }



        private async void btnAdd_Click(object sender, EventArgs e)
        {
            // C# 7.3–compatible:
            using (var dlg = new CustomerContractAddForm())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                    await LoadDataAsync();
            }

        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvContracts.CurrentRow == null) return;
            var dto = (CustomerContractDTO)dgvContracts.CurrentRow.DataBoundItem;
            using (var dlg = new CustomerContractAddForm(dto))
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                    await LoadDataAsync();
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvContracts.CurrentRow == null) return;
            var dto = (CustomerContractDTO)dgvContracts.CurrentRow.DataBoundItem;
            if (MaterialMessageBox.Show(
                    $"Delete contract {dto.ContractNumber}?",
                    "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning
                ) == DialogResult.Yes)
            {
                await _bll.DeleteAsync(dto);
                await LoadDataAsync();
            }
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            searchTextBox.Clear();
            await LoadDataAsync();
        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            var term = searchTextBox.Text.Trim().ToLower();
            dgvContracts.DataSource = string.IsNullOrEmpty(term)
                ? _allContracts
                : _allContracts.Where(c =>
                    !string.IsNullOrEmpty(c.CustomerName) &&
                    c.CustomerName.ToLower().Contains(term))
                  .ToList();
        }
    }
}

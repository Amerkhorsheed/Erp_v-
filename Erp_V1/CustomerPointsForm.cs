// File: CustomerPointsForm.cs
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
    public partial class CustomerPointsForm : MaterialForm
    {
        private readonly CustomerPointsBLL _bll = new CustomerPointsBLL();
        private List<CustomerPointsDTO> _allPoints;

        public CustomerPointsForm()
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
                Primary.Green500, Primary.Green700,
                Primary.Green100, Accent.LightGreen200,
                TextShade.WHITE
            );
        }

        private void CustomerPointsForm_Load(object sender, EventArgs e)
        {
            ConfigureGrid();
            LoadData();
        }

        private void ConfigureGrid()
        {
            dgvPoints.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPoints.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPoints.MultiSelect = false;
            dgvPoints.ReadOnly = true;
            dgvPoints.AllowUserToAddRows = false;
            dgvPoints.RowHeadersVisible = false;
        }

        private void LoadData()
        {
            _allPoints = _bll.Select().PointsEntries;
            dgvPoints.DataSource = _allPoints;
            HideExtraColumns();
        }

        private void HideExtraColumns()
        {
            if (dgvPoints.Columns["ID"] != null) dgvPoints.Columns["ID"].Visible = false;
            if (dgvPoints.Columns["CustomerID"] != null) dgvPoints.Columns["CustomerID"].Visible = false;
            if (dgvPoints.Columns["CustomerName"] != null) dgvPoints.Columns["CustomerName"].HeaderText = "Customer Name";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var dlg = new CustomerPointsAddForm())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                    LoadData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvPoints.CurrentRow == null)
            {
                MaterialMessageBox.Show("Select an entry to edit.", "Info",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var dto = (CustomerPointsDTO)dgvPoints.CurrentRow.DataBoundItem;
            using (var dlg = new CustomerPointsAddForm(dto))
            {
                if (dlg.ShowDialog() == DialogResult.OK)
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
            dgvPoints.DataSource = string.IsNullOrEmpty(term)
                ? _allPoints
                : _allPoints
                    .Where(p => !string.IsNullOrEmpty(p.CustomerName)
                             && p.CustomerName.ToLower().Contains(term))
                    .ToList();
            HideExtraColumns();
        }
    }
}

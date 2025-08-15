// File: SalaryListForm.cs
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Erp_V1
{
    public partial class SalaryListForm : MaterialForm
    {
        private readonly SalaryBLL _bll = new SalaryBLL();
        private SalaryDTO _dto = new SalaryDTO();
        private List<SalaryDetailDTO> _salaries = new List<SalaryDetailDTO>();

        public SalaryListForm()
        {
            InitializeComponent();
            InitializeMaterialSkin();
        }

        private void InitializeMaterialSkin()
        {
            var skinManager = MaterialSkinManager.Instance;
            skinManager.AddFormToManage(this);
            skinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            skinManager.ColorScheme = new ColorScheme(Primary.Indigo500, Primary.Indigo700, Primary.Indigo100, Accent.Pink200, TextShade.WHITE);
        }

        private void SalaryListForm_Load(object sender, EventArgs e)
        {
            LoadDataAndFilters();
        }

        private async void LoadDataAndFilters()
        {
            try
            {
                _dto = await Task.Run(() => _bll.Select());
                _salaries = _dto.Salaries ?? new List<SalaryDetailDTO>();

                dataGridView.DataSource = _salaries;
                ConfigureGridView();

                // Use safe data binding order
                cmbDepartment.ValueMember = "ID";
                cmbDepartment.DisplayMember = "DepartmentName";
                cmbDepartment.DataSource = _dto.Departments;
                cmbDepartment.SelectedIndex = -1;

                cmbPosition.ValueMember = "ID";
                cmbPosition.DisplayMember = "PositionName";
                cmbPosition.DataSource = _dto.Positions;
                cmbPosition.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureGridView()
        {
            if (dataGridView.Columns.Count > 0)
            {
                dataGridView.Columns[nameof(SalaryDetailDTO.SalaryID)].Visible = false;
                dataGridView.Columns[nameof(SalaryDetailDTO.EmployeeID)].Visible = false;
                dataGridView.Columns[nameof(SalaryDetailDTO.DepartmentID)].Visible = false;
                dataGridView.Columns[nameof(SalaryDetailDTO.PositionID)].Visible = false;
                dataGridView.Columns[nameof(SalaryDetailDTO.MonthID)].Visible = false;

                dataGridView.Columns[nameof(SalaryDetailDTO.UserNo)].HeaderText = "User No";
                dataGridView.Columns[nameof(SalaryDetailDTO.Name)].HeaderText = "First Name";
                dataGridView.Columns[nameof(SalaryDetailDTO.Surname)].HeaderText = "Last Name";
                dataGridView.Columns[nameof(SalaryDetailDTO.Amount)].HeaderText = "Salary Amount";
                dataGridView.Columns[nameof(SalaryDetailDTO.Amount)].DefaultCellStyle.Format = "C2";
                dataGridView.Columns[nameof(SalaryDetailDTO.Year)].HeaderText = "Year";
                dataGridView.Columns[nameof(SalaryDetailDTO.MonthName)].HeaderText = "Month";
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var filteredList = _salaries.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(txtUserNo.Text) && int.TryParse(txtUserNo.Text, out int userNo))
            {
                filteredList = filteredList.Where(x => x.UserNo == userNo);
            }
            if (!string.IsNullOrWhiteSpace(txtName.Text))
            {
                filteredList = filteredList.Where(x => x.Name.Contains(txtName.Text.Trim()));
            }
            if (cmbDepartment.SelectedValue is int deptId)
            {
                filteredList = filteredList.Where(x => x.DepartmentID == deptId);
            }
            if (cmbPosition.SelectedValue is int posId)
            {
                filteredList = filteredList.Where(x => x.PositionID == posId);
            }

            dataGridView.DataSource = filteredList.ToList();
            ConfigureGridView();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUserNo.Clear();
            txtName.Clear();
            cmbDepartment.SelectedIndex = -1;
            cmbPosition.SelectedIndex = -1;
            dataGridView.DataSource = _salaries;
            ConfigureGridView();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var form = new SalaryAddForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadDataAndFilters();
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0) return;
            var selected = (SalaryDetailDTO)dataGridView.SelectedRows[0].DataBoundItem;

            using (var form = new SalaryAddForm(selected))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadDataAndFilters();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0) return;
            var selected = (SalaryDetailDTO)dataGridView.SelectedRows[0].DataBoundItem;

            var confirm = MessageBox.Show($"Are you sure you want to permanently delete the salary record for {selected.Name} {selected.Surname}?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                if (_bll.Delete(selected))
                {
                    MessageBox.Show("Salary record deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataAndFilters();
                }
                else
                {
                    MessageBox.Show("Failed to delete the record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
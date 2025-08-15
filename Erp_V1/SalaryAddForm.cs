// File: SalaryAddForm.cs
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Erp_V1
{
    public partial class SalaryAddForm : MaterialForm
    {
        private readonly SalaryBLL _bll = new SalaryBLL();
        private SalaryDTO _lookupData;
        private SalaryDetailDTO _dto;
        private readonly bool _isEditMode;
        private int _selectedEmployeeId = 0;

        public SalaryAddForm()
        {
            InitializeComponent();
            InitializeMaterialSkin();
            _isEditMode = false;
            this.Text = "Add New Salary";
        }

        public SalaryAddForm(SalaryDetailDTO dtoToEdit) : this()
        {
            _isEditMode = true;
            _dto = dtoToEdit;
            this.Text = "Edit Salary";
        }

        private void InitializeMaterialSkin()
        {
            var skinManager = MaterialSkinManager.Instance;
            skinManager.AddFormToManage(this);
            skinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            skinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }

        private async void SalaryAddForm_Load(object sender, EventArgs e)
        {
            try
            {
                _lookupData = await Task.Run(() => _bll.Select());

                // Use safe binding order
                cmbMonth.ValueMember = "ID";
                cmbMonth.DisplayMember = "MonthName";
                cmbMonth.DataSource = _lookupData.Months;

                dgvEmployees.DataSource = _lookupData.Employees;
                ConfigureEmployeeGridView();

                if (_isEditMode)
                {
                    PopulateFieldsForEdit();
                }
                else
                {
                    txtYear.Text = DateTime.Today.Year.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Form could not be loaded. Error: {ex.Message}", "Loading Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void PopulateFieldsForEdit()
        {
            // Store the ID of the employee being edited
            _selectedEmployeeId = _dto.EmployeeID;

            // Populate the fields on the right
            txtUserNo.Text = _dto.UserNo.ToString();
            txtName.Text = _dto.Name;
            txtSurname.Text = _dto.Surname;
            txtAmount.Text = _dto.Amount.ToString();
            txtYear.Text = _dto.Year.ToString();
            cmbMonth.SelectedValue = _dto.MonthID;

            // Disable employee selection when editing a salary
            dgvEmployees.Enabled = false;
        }

        private void ConfigureEmployeeGridView()
        {
            if (dgvEmployees.Columns.Count > 0)
            {
                dgvEmployees.Columns[nameof(EmployeeDetailDTO.EmployeeID)].Visible = false;
                dgvEmployees.Columns[nameof(EmployeeDetailDTO.Password)].Visible = false;
                dgvEmployees.Columns[nameof(EmployeeDetailDTO.Address)].Visible = false;
                dgvEmployees.Columns[nameof(EmployeeDetailDTO.BirthDay)].Visible = false;
                dgvEmployees.Columns[nameof(EmployeeDetailDTO.ImagePath)].Visible = false;
                dgvEmployees.Columns[nameof(EmployeeDetailDTO.RoleID)].Visible = false;
                dgvEmployees.Columns[nameof(EmployeeDetailDTO.Salary)].HeaderText = "Current Salary";
            }
        }

        private void dgvEmployees_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (_isEditMode) return; // Don't allow changing employee in edit mode

            if (e.RowIndex >= 0)
            {
                var selectedEmployee = dgvEmployees.Rows[e.RowIndex].DataBoundItem as EmployeeDetailDTO;
                if (selectedEmployee != null)
                {
                    _selectedEmployeeId = selectedEmployee.EmployeeID;
                    txtUserNo.Text = selectedEmployee.UserNo.ToString();
                    txtName.Text = selectedEmployee.Name;
                    txtSurname.Text = selectedEmployee.Surname;
                    txtAmount.Text = selectedEmployee.Salary.ToString(); // Pre-fill with current salary
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // --- Validation ---
            if (_selectedEmployeeId == 0)
            {
                MessageBox.Show("Please select an employee.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(txtAmount.Text, out int amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid salary amount.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(txtYear.Text, out int year) || year < 2000)
            {
                MessageBox.Show("Please enter a valid year.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- DTO Population ---
            var dto = _isEditMode ? _dto : new SalaryDetailDTO();
            dto.EmployeeID = _selectedEmployeeId;
            dto.Amount = amount;
            dto.Year = year;
            dto.MonthID = (int)cmbMonth.SelectedValue;

            try
            {
                bool success = _isEditMode ? _bll.Update(dto) : _bll.Insert(dto);
                if (success)
                {
                    MessageBox.Show("Salary saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("The operation failed to save.", "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred:\n{ex.Message}", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
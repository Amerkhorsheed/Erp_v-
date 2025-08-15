// File: TaskAddForm.cs
using Erp_V1.BLL;
using Erp_V1.DAL;
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
    public partial class TaskAddForm : MaterialForm
    {
        private readonly TaskBLL _bll = new TaskBLL();
        private TaskDTO _lookupData;
        private TaskDetailDTO _dto;
        private readonly bool _isEditMode;

        public TaskAddForm()
        {
            InitializeComponent();
            InitializeMaterialSkin();
            _isEditMode = false;
            this.Text = "Add New Task";
        }

        public TaskAddForm(TaskDetailDTO dtoToEdit) : this()
        {
            _isEditMode = true;
            _dto = dtoToEdit;
            this.Text = "Edit Task";
        }

        private void InitializeMaterialSkin()
        {
            var skinManager = MaterialSkinManager.Instance;
            skinManager.AddFormToManage(this);
            skinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            skinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }

        private async void TaskAddForm_Load(object sender, EventArgs e)
        {
            try
            {
                _lookupData = await Task.Run(() => new TaskBLL().Select());

                // Populate filter dropdowns
                cmbFilterDepartment.ValueMember = "ID";
                cmbFilterDepartment.DisplayMember = "DepartmentName";
                cmbFilterDepartment.DataSource = _lookupData.Departments;
                cmbFilterDepartment.SelectedIndex = -1;

                cmbFilterPosition.ValueMember = "ID";
                cmbFilterPosition.DisplayMember = "PositionName";
                cmbFilterPosition.DataSource = _lookupData.Positions;
                cmbFilterPosition.SelectedIndex = -1;

                // Populate the Task State dropdown
                cmbTaskState.ValueMember = "ID";
                cmbTaskState.DisplayMember = "StateName";
                cmbTaskState.DataSource = _lookupData.TaskStates;

                ApplyEmployeeFilter();

                if (_isEditMode)
                {
                    PopulateFields();
                    // Allow user to change status when editing
                    cmbTaskState.Enabled = true;
                }
                else
                {
                    // ** THIS IS THE FIX: Default the UI to show 'Delivered' for new tasks **
                    cmbTaskState.SelectedValue = TaskStates.Delivered;
                    cmbTaskState.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Form could not be loaded. Error: {ex.Message}", "Loading Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void PopulateFields()
        {
            txtTitle.Text = _dto.Title;
            txtContent.Text = _dto.Content;

            cmbFilterDepartment.SelectedValue = _dto.DepartmentID;
            cmbFilterPosition.SelectedValue = _dto.PositionID;

            ApplyEmployeeFilter();
            cmbEmployee.SelectedValue = _dto.EmployeeID;

            cmbTaskState.SelectedValue = _dto.taskStateID;
        }

        private void ApplyEmployeeFilter()
        {
            var employees = _lookupData.Employees?.AsEnumerable() ?? new List<EmployeeDetailDTO>();

            if (cmbFilterDepartment.SelectedValue is int deptId)
            {
                employees = employees.Where(x => x.DepartmentID == deptId);
            }
            if (cmbFilterPosition.SelectedValue is int posId)
            {
                employees = employees.Where(x => x.PositionID == posId);
            }

            cmbEmployee.ValueMember = "EmployeeID";
            cmbEmployee.DisplayMember = "FullName";
            cmbEmployee.DataSource = employees.ToList();
            cmbEmployee.SelectedIndex = -1;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                MessageBox.Show("Please fix validation errors.", "Validation Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dto = _isEditMode ? _dto : new TaskDetailDTO();
            dto.Title = txtTitle.Text.Trim();
            dto.Content = txtContent.Text.Trim();
            dto.EmployeeID = (int)cmbEmployee.SelectedValue;

            if (_isEditMode)
            {
                dto.taskStateID = (int)cmbTaskState.SelectedValue;
            }

            try
            {
                bool success = _isEditMode ? _bll.Update(dto) : _bll.Insert(dto);
                if (success)
                {
                    MessageBox.Show("Task saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                string errorMessage = ex.InnerException?.InnerException?.Message ?? ex.InnerException?.Message ?? ex.Message;
                MessageBox.Show($"An error occurred:\n{errorMessage}", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Unchanged UI Events
        private void cmbEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEmployee.SelectedItem is EmployeeDetailDTO selectedEmployee)
            {
                txtDepartmentDisplay.Text = selectedEmployee.DepartmentName;
                txtPositionDisplay.Text = selectedEmployee.PositionName;
            }
            else
            {
                txtDepartmentDisplay.Clear();
                txtPositionDisplay.Clear();
            }
        }

        private void cmbFilterDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyEmployeeFilter();
        }

        private void cmbFilterPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyEmployeeFilter();
        }

        private void btnClearFilters_Click(object sender, EventArgs e)
        {
            cmbFilterDepartment.SelectedIndex = -1;
            cmbFilterPosition.SelectedIndex = -1;
            ApplyEmployeeFilter();
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(txtTitle, "Task title is required.");
            }
            else
            {
                errorProvider.SetError(txtTitle, "");
            }
        }

        private void cmbEmployee_Validating(object sender, CancelEventArgs e)
        {
            if (cmbEmployee.SelectedIndex == -1)
            {
                e.Cancel = true;
                errorProvider.SetError(cmbEmployee, "Please assign the task to an employee.");
            }
            else
            {
                errorProvider.SetError(cmbEmployee, "");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
        #endregion
    }
}
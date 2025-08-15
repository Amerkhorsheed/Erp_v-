// File: TaskListForm.cs
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
    public partial class TaskListForm : MaterialForm
    {
        private readonly TaskBLL _bll = new TaskBLL();
        private TaskDTO _dto = new TaskDTO();
        private List<TaskDetailDTO> _tasks = new List<TaskDetailDTO>();

        public TaskListForm()
        {
            InitializeComponent();
            InitializeMaterialSkin();
        }

        private void InitializeMaterialSkin()
        {
            var skinManager = MaterialSkinManager.Instance;
            skinManager.AddFormToManage(this);
            skinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            skinManager.ColorScheme = new ColorScheme(Primary.Green600, Primary.Green700, Primary.Green200, Accent.Red400, TextShade.WHITE);
        }

        private void TaskListForm_Load(object sender, EventArgs e)
        {
            LoadDataAndFilters();
        }

        private async void LoadDataAndFilters()
        {
            try
            {
                _dto = await Task.Run(() => _bll.Select());
                _tasks = _dto.Tasks ?? new List<TaskDetailDTO>();

                dataGridView.DataSource = _tasks;
                ConfigureGridView();

                // ** THIS IS THE FIX for data binding errors **
                // Populate filter dropdowns using the safe binding order:
                // 1. ValueMember, 2. DisplayMember, 3. DataSource
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
                // Use nameof() for type safety to prevent errors if you rename properties
                dataGridView.Columns[nameof(TaskDetailDTO.TaskID)].Visible = false;
                dataGridView.Columns[nameof(TaskDetailDTO.EmployeeID)].Visible = false;
                dataGridView.Columns[nameof(TaskDetailDTO.DepartmentID)].Visible = false;
                dataGridView.Columns[nameof(TaskDetailDTO.PositionID)].Visible = false;
                dataGridView.Columns[nameof(TaskDetailDTO.taskStateID)].Visible = false;
                dataGridView.Columns[nameof(TaskDetailDTO.Content)].Visible = false;

                dataGridView.Columns[nameof(TaskDetailDTO.UserNo)].HeaderText = "User No";
                dataGridView.Columns[nameof(TaskDetailDTO.Name)].HeaderText = "First Name";
                dataGridView.Columns[nameof(TaskDetailDTO.Surname)].HeaderText = "Last Name";
                dataGridView.Columns[nameof(TaskDetailDTO.Title)].HeaderText = "Task Title";
                dataGridView.Columns[nameof(TaskDetailDTO.TaskStateName)].HeaderText = "Status";
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                var filteredList = _tasks.AsEnumerable();

                //if (!string.IsNullOrWhiteSpace(txtUserNo.Text) && int.TryParse(txtUserNo.Text, out int userNo))
                //{
                //    filteredList = filteredList.Where(x => x.UserNo == userNo);
                //}
                if (!string.IsNullOrWhiteSpace(txtName.Text))
                {
                    filteredList = filteredList.Where(x => x.Name.Contains(txtName.Text.Trim()));
                }
                if (!string.IsNullOrWhiteSpace(txtSurname.Text))
                {
                    filteredList = filteredList.Where(x => x.Surname.Contains(txtSurname.Text.Trim()));
                }

                // ** THIS IS THE FIX for the InvalidCastException **
                // Get the selected value safely.
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
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while searching: {ex.Message}", "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //txtUserNo.Clear();
            txtName.Clear();
            txtSurname.Clear();
            cmbDepartment.SelectedIndex = -1;
            cmbPosition.SelectedIndex = -1;
            dataGridView.DataSource = _tasks;
            ConfigureGridView();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var form = new TaskAddForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadDataAndFilters(); // Refresh list after adding
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a task to update.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedTask = (TaskDetailDTO)dataGridView.SelectedRows[0].DataBoundItem;
            using (var form = new TaskAddForm(selectedTask))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadDataAndFilters(); // Refresh list after updating
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0) return;

            var selectedTask = (TaskDetailDTO)dataGridView.SelectedRows[0].DataBoundItem;
            var confirmResult = MessageBox.Show($"Are you sure you want to delete the task '{selectedTask.Title}'? This is permanent.", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.Yes)
            {
                if (_bll.Delete(selectedTask))
                {
                    MessageBox.Show("Task deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataAndFilters();
                }
                else
                {
                    MessageBox.Show("Failed to delete the task.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0) return;

            var selectedTask = (TaskDetailDTO)dataGridView.SelectedRows[0].DataBoundItem;

            if (selectedTask.taskStateID != TaskStates.Delivered)
            {
                MessageBox.Show("Only tasks that have been delivered can be approved.", "Workflow Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmResult = MessageBox.Show("Are you sure you want to approve this task?", "Confirm Approval", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult == DialogResult.Yes)
            {
                // Assuming the user of this form is an Admin
                if (_bll.Approve(selectedTask, isAdmin: true))
                {
                    MessageBox.Show("Task approved.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataAndFilters();
                }
                else
                {
                    MessageBox.Show("Failed to approve the task.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0 && dataGridView.SelectedRows[0].DataBoundItem is TaskDetailDTO selectedTask)
            {
                // Enable approve button only if the task state is "Delivered"
                btnApprove.Enabled = (selectedTask.taskStateID == TaskStates.Delivered);
            }
            else
            {
                btnApprove.Enabled = false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
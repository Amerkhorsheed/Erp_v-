using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;

namespace Erp_V1
{
    public partial class DepartmentForm : Form
    {
        private readonly DepartmentBLL _departmentBLL = new DepartmentBLL();
        private List<DepartmentDetailDTO> _departments;

        public DepartmentForm()
        {
            InitializeComponent();
            Text = "Department Management";
            BackColor = Color.FromArgb(34, 40, 49);

            dgvDepartments.EnableHeadersVisualStyles = false;
            dgvDepartments.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(57, 62, 70);
            dgvDepartments.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvDepartments.RowTemplate.Height = 28;
            dgvDepartments.RowsDefaultCellStyle.BackColor = Color.FromArgb(50, 54, 61);
            dgvDepartments.RowsDefaultCellStyle.ForeColor = Color.White;
        }

        private void DepartmentForm_Load(object sender, EventArgs e)
        {
            LoadDepartments();
        }

        private void LoadDepartments()
        {
            _departments = _departmentBLL.Select().Departments
                .Where(d => !d.IsDeleted)
                .ToList();

            dgvDepartments.DataSource = _departments;
            FormatGrid();
        }

        private void FormatGrid()
        {
            // Rename your displayed columns
            //dgvDepartments.Columns["DepartmentID"].HeaderText = "ID";
            dgvDepartments.Columns["DepartmentName"].HeaderText = "Name";

            // Hide any other auto‑generated properties (IsDeleted, DeletedDate, etc.)
            foreach (var key in new[] { "DepartmentID", "IsDeleted", "DeletedDate" })
            {
                if (dgvDepartments.Columns.Contains(key))
                    dgvDepartments.Columns[key].Visible = false;
            }

            // Stretch only the visible ones
            foreach (DataGridViewColumn col in dgvDepartments.Columns)
            {
                if (col.Visible)
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var name = txtName.Text.Trim();
            if (string.IsNullOrEmpty(name)) return;

            var dto = new DepartmentDetailDTO { DepartmentName = name };
            if (_departmentBLL.Insert(dto))
            {
                MessageBox.Show("Department added successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtName.Clear();
                LoadDepartments();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvDepartments.CurrentRow == null) return;
            var selected = (DepartmentDetailDTO)dgvDepartments.CurrentRow.DataBoundItem;

            if (MessageBox.Show("Are you sure you want to update this department?",
                "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                != DialogResult.Yes) return;

            selected.DepartmentName = txtName.Text.Trim();
            if (_departmentBLL.Update(selected))
            {
                MessageBox.Show("Department updated successfully.", "Updated",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadDepartments();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDepartments.CurrentRow == null) return;
            var selected = (DepartmentDetailDTO)dgvDepartments.CurrentRow.DataBoundItem;

            if (MessageBox.Show("Are you sure you want to delete this department?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                != DialogResult.Yes) return;

            if (_departmentBLL.Delete(selected))
            {
                MessageBox.Show("Department deleted successfully.", "Deleted",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadDepartments();
            }
        }

        private void dgvDepartments_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDepartments.CurrentRow == null) return;
            var selected = (DepartmentDetailDTO)dgvDepartments.CurrentRow.DataBoundItem;
            txtName.Text = selected.DepartmentName;
        }
    }
}

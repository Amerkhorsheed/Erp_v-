using Erp_V1.BLL;
using Erp_V1.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Erp_V1
{
    public partial class FrmRolesList : Form
    {
        private readonly RoleBLL _bll = new RoleBLL();
        private RoleDetailDTO _selectedRole = new RoleDetailDTO();

        public FrmRolesList()
        {
            InitializeComponent();
        }

        private void FrmRolesList_Load(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            RoleDTO dto = _bll.Select();
            dataGridView1.DataSource = dto.Roles;
            dataGridView1.Columns["RoleID"].Visible = false;
            dataGridView1.Columns["Description"].Width = 300;
            ClearForm();
        }

        private void ClearForm()
        {
            txtSearch.Text = "";
            txtRoleName.Text = "";
            txtDescription.Text = "";
            _selectedRole = new RoleDetailDTO();
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnPermissions.Enabled = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (FrmRoleAdd addForm = new FrmRoleAdd())
            {
                if (addForm.ShowDialog() == DialogResult.OK)
                {
                    RefreshGrid();
                }
            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dataGridView1.Rows[e.RowIndex];
            _selectedRole.RoleID = Convert.ToInt32(row.Cells["RoleID"].Value);
            _selectedRole.RoleName = row.Cells["RoleName"].Value.ToString();
            _selectedRole.Description = row.Cells["Description"].Value.ToString();

            txtRoleName.Text = _selectedRole.RoleName;
            txtDescription.Text = _selectedRole.Description;

            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
            btnPermissions.Enabled = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedRole.RoleID == 0)
            {
                MessageBox.Show("Select a role to update.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _selectedRole.RoleName = txtRoleName.Text.Trim();
            _selectedRole.Description = txtDescription.Text.Trim();

            bool result = _bll.Update(_selectedRole);
            if (result)
            {
                MessageBox.Show("Role updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshGrid();
            }
            else
            {
                MessageBox.Show("Failed to update role.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedRole.RoleID == 0)
            {
                MessageBox.Show("Select a role to delete.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var ask = MessageBox.Show(
                $"Are you sure you want to delete the role “{_selectedRole.RoleName}”?",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );
            if (ask != DialogResult.Yes) return;

            try
            {
                bool deleted = _bll.Delete(_selectedRole);
                if (deleted)
                {
                    MessageBox.Show("Role deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshGrid();
                }
                else
                {
                    MessageBox.Show("Failed to delete role.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Cannot Delete Role", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnPermissions_Click(object sender, EventArgs e)
        {
            if (_selectedRole.RoleID == 0)
            {
                MessageBox.Show("Select a role to manage its permissions.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (FrmRolePermissions2 permForm = new FrmRolePermissions2(_selectedRole.RoleID, _selectedRole.RoleName))
            {
                permForm.ShowDialog();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string key = txtSearch.Text.Trim().ToLower();
            RoleDTO dto = _bll.Select();
            var filtered = dto.Roles.Where(r =>
                r.RoleName.ToLower().Contains(key) ||
                r.Description.ToLower().Contains(key)
            ).ToList();

            dataGridView1.DataSource = filtered;
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            RefreshGrid();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

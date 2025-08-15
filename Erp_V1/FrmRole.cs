using Erp_V1.BLL;
using Erp_V1.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Erp_V1
{
    public partial class FrmRole : Form
    {
        private RoleBLL _bll = new RoleBLL();
        private RoleDTO _dto = new RoleDTO();
        private RoleDetailDTO _detailDto = new RoleDetailDTO();

        public FrmRole()
        {
            InitializeComponent();
        }

        private void FrmRole_Load(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            _dto = _bll.Select();
            dataGridView1.DataSource = _dto.Roles;
            dataGridView1.Columns["RoleID"].Visible = false; // Hide ID column
            dataGridView1.Columns["Description"].Width = 250;
            ClearForm();
        }

        private void ClearForm()
        {
            txtRoleName.Clear();
            txtDescription.Clear();
            _detailDto = new RoleDetailDTO();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRoleName.Text))
            {
                MessageBox.Show("Role Name is required.");
                return;
            }

            _detailDto.RoleName = txtRoleName.Text;
            _detailDto.Description = txtDescription.Text;

            if (_bll.Insert(_detailDto))
            {
                MessageBox.Show("Role was added successfully.");
                RefreshGrid();
            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                _detailDto.RoleID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["RoleID"].Value);
                _detailDto.RoleName = dataGridView1.Rows[e.RowIndex].Cells["RoleName"].Value.ToString();
                _detailDto.Description = dataGridView1.Rows[e.RowIndex].Cells["Description"].Value.ToString();

                txtRoleName.Text = _detailDto.RoleName;
                txtDescription.Text = _detailDto.Description;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_detailDto.RoleID == 0)
            {
                MessageBox.Show("Please select a role from the list to update.");
                return;
            }

            _detailDto.RoleName = txtRoleName.Text;
            _detailDto.Description = txtDescription.Text;

            if (_bll.Update(_detailDto))
            {
                MessageBox.Show("Role was updated successfully.");
                RefreshGrid();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_detailDto.RoleID == 0)
            {
                MessageBox.Show("Please select a role from the list to delete.");
                return;
            }

            var result = MessageBox.Show(
                $"Are you sure you want to delete the “{_detailDto.RoleName}” role?",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );
            if (result != DialogResult.Yes) return;

            try
            {
                if (_bll.Delete(_detailDto))
                {
                    MessageBox.Show("Role deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshGrid();
                }
                else
                {
                    // This shouldn’t normally happen if you always throw on failure, 
                    // but just in case:
                    MessageBox.Show(
                        "Role deletion failed for an unknown reason.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                // ex.Message will be something like:
                // "Cannot delete role “Administrator” because it is still assigned 
                // to one or more employees. Please unassign this role from all 
                // employees before deleting it."
                MessageBox.Show(
                    ex.Message,
                    "Cannot Delete Role",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }


        private void btnPermissions_Click(object sender, EventArgs e)
        {
            if (_detailDto.RoleID == 0)
            {
                MessageBox.Show("Please select a role from the list to manage its permissions.");
                return;
            }

            // Open the new permissions form
            FrmRolePermission frm = new FrmRolePermission(_detailDto.RoleID, _detailDto.RoleName);
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
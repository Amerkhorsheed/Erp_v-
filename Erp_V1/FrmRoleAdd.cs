using Erp_V1.BLL;
using Erp_V1.DAL.DTO;
using System;
using System.Windows.Forms;

namespace Erp_V1
{
    public partial class FrmRoleAdd : Form
    {
        private readonly RoleBLL _bll = new RoleBLL();
        public FrmRoleAdd()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string roleName = txtRoleName.Text.Trim();
            string description = txtDescription.Text.Trim();

            if (string.IsNullOrWhiteSpace(roleName))
            {
                MessageBox.Show("Role Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRoleName.Focus();
                return;
            }

            var dto = new RoleDetailDTO
            {
                RoleName = roleName,
                Description = description
            };

            bool success = _bll.Insert(dto);
            if (success)
            {
                MessageBox.Show("Role added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to add role. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

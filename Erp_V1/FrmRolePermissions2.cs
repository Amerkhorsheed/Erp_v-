using Erp_V1.BLL;
using Erp_V1.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Erp_V1
{
    public partial class FrmRolePermissions2 : Form
    {
        private readonly RolePermissionBLL _permBll = new RolePermissionBLL();
        private int _roleId;
        private string _roleName;

        public FrmRolePermissions2(int roleId, string roleName)
        {
            InitializeComponent();
            _roleId = roleId;
            _roleName = roleName;
        }

        private void FrmRolePermissions2_Load(object sender, EventArgs e)
        {
            lblRoleName.Text = $"Permissions for: {_roleName}";
            LoadPermissions();
        }

        private void LoadPermissions()
        {
            // 1) Get all possible permissions
            List<string> allPermissions = _permBll.GetAllPossiblePermissions();

            // 2) Get the current permissions for this role
            List<string> existing = _permBll.GetPermissionsByRole(_roleId);

            // 3) Populate the CheckedListBox
            clbPermissions.Items.Clear();
            foreach (var perm in allPermissions)
            {
                int idx = clbPermissions.Items.Add(perm);
                if (existing.Contains(perm))
                {
                    clbPermissions.SetItemChecked(idx, true);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Collect checked items
            List<string> selected = clbPermissions.CheckedItems
                                       .Cast<string>()
                                       .ToList();

            bool updated = _permBll.UpdatePermissionsForRole(_roleId, selected);
            if (updated)
            {
                MessageBox.Show("Permissions updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to update permissions.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

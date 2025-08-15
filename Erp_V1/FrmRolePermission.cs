using Erp_V1.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Erp_V1
{
    /// <summary>
    /// Manages the assignment of permissions to a specific role.
    /// </summary>
    public partial class FrmRolePermission : Form
    {
        private readonly int _roleId;
        private readonly string _roleName;
        private readonly RolePermissionBLL _bll;

        /// <summary>
        /// Only constructor used at runtime. Enforces a valid role context.
        /// </summary>
        public FrmRolePermission(int roleId, string roleName)
        {
            if (roleId <= 0)
                throw new ArgumentException("roleId must be > 0", nameof(roleId));

            InitializeComponent();
            _roleId = roleId;
            _roleName = roleName;
            _bll = new RolePermissionBLL();

            // Defer actual loading until after control is fully realized
            this.Shown += (s, e) => this.BeginInvoke(new MethodInvoker(LoadAndDisplayPermissions));
        }

        private void LoadAndDisplayPermissions()
        {
            try
            {
                // Guard against disposal before starting
                if (IsDisposed || checkedListBoxPermissions.IsDisposed) return;

                lblRoleName.Text = $"Permissions for Role: {_roleName}";

                // 1. Get data first (safely)
                List<string> allPerms = _bll.GetAllPossiblePermissions() ?? new List<string>();
                HashSet<string> assigned = new HashSet<string>(_bll.GetPermissionsByRole(_roleId));

                // 2. Verify control state again after potentially slow DB calls
                if (IsDisposed || checkedListBoxPermissions.IsDisposed) return;

                // 3. Use manual population instead of DataSource
                checkedListBoxPermissions.BeginUpdate();
                try
                {
                    checkedListBoxPermissions.Items.Clear();

                    // Add items safely
                    foreach (string perm in allPerms)
                    {
                        // Check disposal status mid-population
                        if (IsDisposed || checkedListBoxPermissions.IsDisposed) return;
                        checkedListBoxPermissions.Items.Add(perm);
                    }

                    // Set check states
                    for (int i = 0; i < checkedListBoxPermissions.Items.Count; i++)
                    {
                        if (IsDisposed || checkedListBoxPermissions.IsDisposed) return;

                        var item = checkedListBoxPermissions.Items[i] as string;
                        if (item != null && assigned.Contains(item))
                        {
                            checkedListBoxPermissions.SetItemChecked(i, true);
                        }
                    }
                }
                finally
                {
                    if (!checkedListBoxPermissions.IsDisposed)
                        checkedListBoxPermissions.EndUpdate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading permissions: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var selected = checkedListBoxPermissions.CheckedItems.OfType<string>().ToList();
            try
            {
                if (_bll.UpdatePermissionsForRole(_roleId, selected))
                {
                    MessageBox.Show("Permissions updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                }
                else MessageBox.Show("Failed to update permissions.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving permissions: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { this.Close(); }
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            bool check = chkSelectAll.Checked;
            for (int i = 0; i < checkedListBoxPermissions.Items.Count; i++)
                checkedListBoxPermissions.SetItemChecked(i, check);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
//using DevExpress.XtraEditors;
//using Erp_V1.BLL;
//using Erp_V1.DAL.DTO;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Windows.Forms;

//namespace Erp_V1.Forms
//{
//    public partial class FrmRolePermissions : XtraForm
//    {
//        private readonly RoleBLL _roleBLL = new RoleBLL();
//        private readonly List<string> _availableForms = new List<string>
//        {
//            "FrmCustomer",
//            "FrmEmployee",
//            "FrmDepartment",
//            "FrmPosition",
//            "FrmRole",
//            "FrmTask",
//            "FrmSalary",
//            // Add more forms as needed
//        };

//        public FrmRolePermissions()
//        {
//            InitializeComponent();
//            LoadRoles();
//        }

//        private void InitializeComponent()
//        {
//            this.cmbRoles = new LookUpEdit();
//            this.chkListForms = new CheckedListBoxControl();
//            this.btnSave = new SimpleButton();
//            this.btnCancel = new SimpleButton();
//            this.labelControl1 = new LabelControl();
//            this.labelControl2 = new LabelControl();

//            // cmbRoles
//            this.cmbRoles.Location = new System.Drawing.Point(12, 29);
//            this.cmbRoles.Name = "cmbRoles";
//            this.cmbRoles.Properties.NullText = "Select a role...";
//            this.cmbRoles.Size = new System.Drawing.Size(200, 20);
//            this.cmbRoles.TabIndex = 0;
//            this.cmbRoles.EditValueChanged += new EventHandler(this.cmbRoles_EditValueChanged);

//            // chkListForms
//            this.chkListForms.Location = new System.Drawing.Point(12, 78);
//            this.chkListForms.Name = "chkListForms";
//            this.chkListForms.Size = new System.Drawing.Size(200, 200);
//            this.chkListForms.TabIndex = 1;

//            // btnSave
//            this.btnSave.Location = new System.Drawing.Point(12, 284);
//            this.btnSave.Name = "btnSave";
//            this.btnSave.Size = new System.Drawing.Size(75, 23);
//            this.btnSave.TabIndex = 2;
//            this.btnSave.Text = "Save";
//            this.btnSave.Click += new EventHandler(this.btnSave_Click);

//            // btnCancel
//            this.btnCancel.Location = new System.Drawing.Point(137, 284);
//            this.btnCancel.Name = "btnCancel";
//            this.btnCancel.Size = new System.Drawing.Size(75, 23);
//            this.btnCancel.TabIndex = 3;
//            this.btnCancel.Text = "Cancel";
//            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);

//            // labelControl1
//            this.labelControl1.Location = new System.Drawing.Point(12, 12);
//            this.labelControl1.Name = "labelControl1";
//            this.labelControl1.Size = new System.Drawing.Size(24, 13);
//            this.labelControl1.TabIndex = 4;
//            this.labelControl1.Text = "Role:";

//            // labelControl2
//            this.labelControl1.Location = new System.Drawing.Point(12, 62);
//            this.labelControl1.Name = "labelControl2";
//            this.labelControl1.Size = new System.Drawing.Size(35, 13);
//            this.labelControl1.TabIndex = 5;
//            this.labelControl1.Text = "Forms:";

//            // FrmRolePermissions
//            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
//            this.AutoScaleMode = AutoScaleMode.Font;
//            this.ClientSize = new System.Drawing.Size(224, 321);
//            this.Controls.Add(this.labelControl2);
//            this.Controls.Add(this.labelControl1);
//            this.Controls.Add(this.btnCancel);
//            this.Controls.Add(this.btnSave);
//            this.Controls.Add(this.chkListForms);
//            this.Controls.Add(this.cmbRoles);
//            this.FormBorderStyle = FormBorderStyle.FixedDialog;
//            this.MaximizeBox = false;
//            this.MinimizeBox = false;
//            this.Name = "FrmRolePermissions";
//            this.StartPosition = FormStartPosition.CenterParent;
//            this.Text = "Role Permissions";
//            this.ResumeLayout(false);
//            this.PerformLayout();
//        }

//        private LookUpEdit cmbRoles;
//        private CheckedListBoxControl chkListForms;
//        private SimpleButton btnSave;
//        private SimpleButton btnCancel;
//        private LabelControl labelControl1;
//        private LabelControl labelControl2;

//        private void LoadRoles()
//        {
//            var roles = _roleBLL.Select().Roles;
//            cmbRoles.Properties.DataSource = roles;
//            cmbRoles.Properties.DisplayMember = "RoleName";
//            cmbRoles.Properties.ValueMember = "RoleID";

//            // Populate form list
//            chkListForms.Items.AddRange(_availableForms);
//        }

//        private void cmbRoles_EditValueChanged(object sender, EventArgs e)
//        {
//            if (cmbRoles.EditValue == null) return;

//            var roleId = (int)cmbRoles.EditValue;
//            var role = _roleBLL.Select().Roles.FirstOrDefault(r => r.RoleID == roleId);
//            if (role == null) return;

//            // Clear existing selections
//            for (int i = 0; i < chkListForms.Items.Count; i++)
//            {
//                chkListForms.SetItemChecked(i, false);
//            }

//            // Set permissions based on role
//            if (role.FormPermissions != null)
//            {
//                foreach (var permission in role.FormPermissions)
//                {
//                    var index = chkListForms.Items.IndexOf(permission.FormName);
//                    if (index >= 0)
//                    {
//                        chkListForms.SetItemChecked(index, permission.HasAccess);
//                    }
//                }
//            }
//        }

//        private void btnSave_Click(object sender, EventArgs e)
//        {
//            if (cmbRoles.EditValue == null)
//            {
//                XtraMessageBox.Show("Please select a role.", "Validation Error",
//                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }

//            var roleId = (int)cmbRoles.EditValue;
//            var role = _roleBLL.Select().Roles.FirstOrDefault(r => r.RoleID == roleId);
//            if (role == null) return;

//            // Update form permissions
//            role.FormPermissions = new List<FormPermissionDetailDTO>();
//            for (int i = 0; i < chkListForms.Items.Count; i++)
//            {
//                role.FormPermissions.Add(new FormPermissionDetailDTO
//                {
//                    RoleID = roleId,
//                    FormName = chkListForms.Items[i].ToString(),
//                    HasAccess = chkListForms.GetItemChecked(i)
//                });
//            }

//            try
//            {
//                if (_roleBLL.Update(role))
//                {
//                    XtraMessageBox.Show("Role permissions updated successfully.", "Success",
//                        MessageBoxButtons.OK, MessageBoxIcon.Information);
//                    DialogResult = DialogResult.OK;
//                    Close();
//                }
//            }
//            catch (UnauthorizedAccessException ex)
//            {
//                XtraMessageBox.Show(ex.Message, "Access Denied",
//                    MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//            catch (Exception ex)
//            {
//                XtraMessageBox.Show($"An error occurred: {ex.Message}", "Error",
//                    MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        private void btnCancel_Click(object sender, EventArgs e)
//        {
//            DialogResult = DialogResult.Cancel;
//            Close();
//        }
//    }
//} 
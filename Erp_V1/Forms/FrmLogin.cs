//using DevExpress.XtraEditors;
//using Erp_V1.BLL;
//using Erp_V1.DAL.DTO;
//using System;
//using System.Windows.Forms;

//namespace Erp_V1.Forms
//{
//    public partial class FrmLogin : XtraForm
//    {
//        private readonly EmployeeBLL _employeeBLL = new EmployeeBLL();
//        public static EmployeeDetailDTO CurrentUser { get; private set; }

//        public FrmLogin()
//        {
//            InitializeComponent();
//        }

//        private void InitializeComponent()
//        {
//            this.txtUserNo = new TextEdit();
//            this.txtPassword = new TextEdit();
//            this.btnLogin = new SimpleButton();
//            this.btnCancel = new SimpleButton();
//            this.labelControl1 = new LabelControl();
//            this.labelControl2 = new LabelControl();
//            this.SuspendLayout();
//            // 
//            // txtUserNo
//            // 
//            this.txtUserNo.Location = new System.Drawing.Point(100, 20);
//            this.txtUserNo.Name = "txtUserNo";
//            this.txtUserNo.Properties.Mask.EditMask = "d";
//            this.txtUserNo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
//            this.txtUserNo.Size = new System.Drawing.Size(200, 20);
//            this.txtUserNo.TabIndex = 0;
//            // 
//            // txtPassword
//            // 
//            this.txtPassword.Location = new System.Drawing.Point(100, 50);
//            this.txtPassword.Name = "txtPassword";
//            this.txtPassword.Properties.PasswordChar = '*';
//            this.txtPassword.Size = new System.Drawing.Size(200, 20);
//            this.txtPassword.TabIndex = 1;
//            // 
//            // btnLogin
//            // 
//            this.btnLogin.Location = new System.Drawing.Point(100, 80);
//            this.btnLogin.Name = "btnLogin";
//            this.btnLogin.Size = new System.Drawing.Size(90, 30);
//            this.btnLogin.TabIndex = 2;
//            this.btnLogin.Text = "Login";
//            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
//            // 
//            // btnCancel
//            // 
//            this.btnCancel.Location = new System.Drawing.Point(210, 80);
//            this.btnCancel.Name = "btnCancel";
//            this.btnCancel.Size = new System.Drawing.Size(90, 30);
//            this.btnCancel.TabIndex = 3;
//            this.btnCancel.Text = "Cancel";
//            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
//            // 
//            // labelControl1
//            // 
//            this.labelControl1.Location = new System.Drawing.Point(20, 23);
//            this.labelControl1.Name = "labelControl1";
//            this.labelControl1.Size = new System.Drawing.Size(74, 13);
//            this.labelControl1.TabIndex = 4;
//            this.labelControl1.Text = "User Number:";
//            // 
//            // labelControl2
//            // 
//            this.labelControl2.Location = new System.Drawing.Point(20, 53);
//            this.labelControl2.Name = "labelControl2";
//            this.labelControl2.Size = new System.Drawing.Size(50, 13);
//            this.labelControl2.TabIndex = 5;
//            this.labelControl2.Text = "Password:";
//            // 
//            // FrmLogin
//            // 
//            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
//            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
//            this.ClientSize = new System.Drawing.Size(334, 131);
//            this.Controls.Add(this.labelControl2);
//            this.Controls.Add(this.labelControl1);
//            this.Controls.Add(this.btnCancel);
//            this.Controls.Add(this.btnLogin);
//            this.Controls.Add(this.txtPassword);
//            this.Controls.Add(this.txtUserNo);
//            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
//            this.MaximizeBox = false;
//            this.MinimizeBox = false;
//            this.Name = "FrmLogin";
//            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
//            this.Text = "Login";
//            this.ResumeLayout(false);
//            this.PerformLayout();
//        }

//        private TextEdit txtUserNo;
//        private TextEdit txtPassword;
//        private SimpleButton btnLogin;
//        private SimpleButton btnCancel;
//        private LabelControl labelControl1;
//        private LabelControl labelControl2;

//        private void btnLogin_Click(object sender, EventArgs e)
//        {
//            if (string.IsNullOrEmpty(txtUserNo.Text) || string.IsNullOrEmpty(txtPassword.Text))
//            {
//                XtraMessageBox.Show("Please enter both user number and password.", "Validation Error",
//                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }

//            if (!int.TryParse(txtUserNo.Text, out int userNo))
//            {
//                XtraMessageBox.Show("Please enter a valid user number.", "Validation Error",
//                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }

//            var employees = _employeeBLL.Select().Employees;
//            var employee = employees.FirstOrDefault(e => e.UserNo == userNo && e.Password == txtPassword.Text);

//            if (employee == null)
//            {
//                XtraMessageBox.Show("Invalid user number or password.", "Login Failed",
//                    MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return;
//            }

//            if (employee.RoleID == 0)
//            {
//                XtraMessageBox.Show("User has no role assigned. Please contact administrator.", "Login Failed",
//                    MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return;
//            }

//            CurrentUser = employee;
//            DialogResult = DialogResult.OK;
//            Close();
//        }

//        private void btnCancel_Click(object sender, EventArgs e)
//        {
//            DialogResult = DialogResult.Cancel;
//            Close();
//        }
//    }
//} 
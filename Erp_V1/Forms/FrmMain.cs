//using DevExpress.XtraEditors;
//using Erp_V1.DAL.DTO;
//using System;
//using System.Windows.Forms;

//namespace Erp_V1.Forms
//{
//    public partial class FrmMain : XtraForm
//    {
//        public FrmMain()
//        {
//            InitializeComponent();
//            CheckLogin();
//        }

//        private void InitializeComponent()
//        {
//            this.SuspendLayout();
//            // 
//            // FrmMain
//            // 
//            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
//            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
//            this.ClientSize = new System.Drawing.Size(800, 450);
//            this.Name = "FrmMain";
//            this.Text = "ERP System";
//            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
//            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
//            this.ResumeLayout(false);
//        }

//        private void CheckLogin()
//        {
//            using (var loginForm = new FrmLogin())
//            {
//                if (loginForm.ShowDialog() != DialogResult.OK)
//                {
//                    Application.Exit();
//                    return;
//                }

//                // Update UI based on user's role
//                UpdateUIForRole(FrmLogin.CurrentUser);
//            }
//        }

//        private void UpdateUIForRole(EmployeeDetailDTO user)
//        {
//            if (user == null) return;

//            // Example of role-based UI updates
//            switch (user.RoleName.ToLower())
//            {
//                case "admin":
//                    // Enable all features
//                    EnableAllFeatures();
//                    break;
//                case "manager":
//                    // Enable management features
//                    EnableManagementFeatures();
//                    break;
//                case "employee":
//                    // Enable basic features
//                    EnableBasicFeatures();
//                    break;
//                default:
//                    // Unknown role, enable minimal features
//                    EnableMinimalFeatures();
//                    break;
//            }

//            // Update form title to show logged-in user
//            this.Text = $"ERP System - {user.Name} {user.Surname} ({user.RoleName})";
//        }

//        private void EnableAllFeatures()
//        {
//            // Enable all menu items and buttons
//            // Add your UI control enabling logic here
//        }

//        private void EnableManagementFeatures()
//        {
//            // Enable management-level features
//            // Add your UI control enabling logic here
//        }

//        private void EnableBasicFeatures()
//        {
//            // Enable basic employee features
//            // Add your UI control enabling logic here
//        }

//        private void EnableMinimalFeatures()
//        {
//            // Enable only the most basic features
//            // Add your UI control enabling logic here
//        }

//        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
//        {
//            if (e.CloseReason == CloseReason.UserClosing)
//            {
//                var result = XtraMessageBox.Show("Are you sure you want to exit?", "Confirm Exit",
//                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

//                if (result == DialogResult.No)
//                {
//                    e.Cancel = true;
//                }
//            }
//        }
//    }
//} 
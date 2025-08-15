//// File: DeliveryUpdateForm.cs
//using Erp_V1.BLL;
//using Erp_V1.DAL.DTO;
//using MaterialSkin;
//using MaterialSkin.Controls;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Windows.Forms;

//// --- Helper BLL and DTOs for populating Employee ComboBox ---
//// In a real application, these would be in their own files.
//public class EmployeeDetailDTO { public int ID { get; set; } public string Name { get; set; } }
//public class EmployeeDTO { public List<EmployeeDetailDTO> Employees { get; set; } }
//public class EmployeeBLL
//{
//    public EmployeeDTO Select() => new EmployeeDTO
//    {
//        Employees = new List<EmployeeDetailDTO>
//        {
//            new EmployeeDetailDTO { ID = 1, Name = "John Doe" },
//            new EmployeeDetailDTO { ID = 2, Name = "Jane Smith" },
//            new EmployeeDetailDTO { ID = 3, Name = "Peter Jones" }
//        }
//    };
//}
//// -----------------------------------------------------------------

//namespace Erp_V1
//{
//    public partial class DeliveryUpdateForm : MaterialForm
//    {
//        private readonly DeliveryBLL _bll = new DeliveryBLL();
//        private readonly EmployeeBLL _employeeBll = new EmployeeBLL();
//        public DeliveryDetailDTO Delivery { get; }

//        public DeliveryUpdateForm(DeliveryDetailDTO dto)
//        {
//            InitializeComponent();
//            InitializeMaterialSkin();
//            Delivery = dto;
//        }

//        private void InitializeMaterialSkin()
//        {
//            var mgr = MaterialSkinManager.Instance;
//            mgr.AddFormToManage(this);
//            mgr.Theme = MaterialSkinManager.Themes.LIGHT;
//            mgr.ColorScheme = new ColorScheme(
//                Primary.BlueGrey800, Primary.BlueGrey900,
//                Primary.BlueGrey500, Accent.LightBlue200,
//                TextShade.WHITE
//            );
//        }

//        private void DeliveryUpdateForm_Load(object sender, EventArgs e)
//        {
//            LoadComboBoxes();
//            PopulateFields();
//        }

//        private void LoadComboBoxes()
//        {
//            // Load Employees
//            var employees = _employeeBll.Select().Employees;
//            cmbDeliveryPerson.DataSource = employees;
//            cmbDeliveryPerson.DisplayMember = "Name";
//            cmbDeliveryPerson.ValueMember = "ID";
//            cmbDeliveryPerson.SelectedIndex = -1;

//            // Load Statuses
//            cmbStatus.Items.AddRange(new[] { "Pending", "Assigned", "Delivered", "Cancelled" });
//        }

//        private void PopulateFields()
//        {
//            lblCustomerName.Text = Delivery.CustomerName;
//            lblAddress.Text = Delivery.Address;

//            if (Delivery.DeliveryPersonID.HasValue)
//            {
//                cmbDeliveryPerson.SelectedValue = Delivery.DeliveryPersonID.Value;
//            }

//            cmbStatus.SelectedItem = Delivery.Status;

//            dtpAssignedDate.Value = Delivery.AssignedDate ?? DateTime.Now;
//            dtpDeliveredDate.Value = Delivery.DeliveredDate ?? DateTime.Now;

//            dtpAssignedDate.Enabled = Delivery.AssignedDate.HasValue;
//            dtpDeliveredDate.Enabled = Delivery.DeliveredDate.HasValue;
//        }

//        private void btnSave_Click(object sender, EventArgs e)
//        {
//            if (cmbStatus.SelectedIndex == -1)
//            {
//                MaterialMessageBox.Show("Please select a status.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }
//            if (cmbStatus.SelectedItem.ToString() == "Assigned" && cmbDeliveryPerson.SelectedIndex == -1)
//            {
//                MaterialMessageBox.Show("Please select a delivery person for 'Assigned' status.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }

//            // Populate DTO with form values
//            Delivery.Status = cmbStatus.SelectedItem.ToString();
//            Delivery.DeliveryPersonID = (int?)cmbDeliveryPerson.SelectedValue;
//            Delivery.AssignedDate = dtpAssignedDate.Enabled ? (DateTime?)dtpAssignedDate.Value : null;
//            Delivery.DeliveredDate = dtpDeliveredDate.Enabled ? (DateTime?)dtpDeliveredDate.Value : null;

//            if (Delivery.Status == "Assigned" && !Delivery.AssignedDate.HasValue)
//            {
//                Delivery.AssignedDate = DateTime.Now;
//            }
//            if (Delivery.Status == "Delivered" && !Delivery.DeliveredDate.HasValue)
//            {
//                Delivery.DeliveredDate = DateTime.Now;
//            }

//            if (_bll.Update(Delivery))
//            {
//                MaterialMessageBox.Show("Delivery updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                this.DialogResult = DialogResult.OK;
//                this.Close();
//            }
//            else
//            {
//                MaterialMessageBox.Show("Failed to update delivery.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        private void btnCancel_Click(object sender, EventArgs e)
//        {
//            this.DialogResult = DialogResult.Cancel;
//            this.Close();
//        }

//        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            string selectedStatus = cmbStatus.SelectedItem?.ToString();
//            dtpAssignedDate.Enabled = (selectedStatus == "Assigned" || selectedStatus == "Delivered");
//            dtpDeliveredDate.Enabled = (selectedStatus == "Delivered");
//        }
//    }
//}
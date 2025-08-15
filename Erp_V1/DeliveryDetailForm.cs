// File: DeliveryDetailForm.cs
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Erp_V1
{
    public partial class DeliveryDetailForm : MaterialForm
    {
        private readonly DeliveryBLL _bll = new DeliveryBLL();
        private readonly DeliveryDetailDTO _dto;

        private readonly EmployeeBLL _employeeBll = new EmployeeBLL();

        public DeliveryDetailForm(DeliveryDetailDTO dtoToEdit)
        {
            InitializeComponent();
            InitializeMaterialSkin();
            _dto = dtoToEdit;
            this.Text = "Update Delivery Details";
        }

        private void InitializeMaterialSkin()
        {
            var mgr = MaterialSkinManager.Instance;
            mgr.AddFormToManage(this);
            mgr.Theme = MaterialSkinManager.Themes.LIGHT;
            mgr.ColorScheme = new ColorScheme(
                Primary.BlueGrey800, Primary.BlueGrey900,
                Primary.BlueGrey500, Accent.LightBlue200,
                TextShade.WHITE
            );
        }

        private void DeliveryDetailForm_Load(object sender, EventArgs e)
        {
            LoadComboBoxes();
            PopulateFields();
        }

        private void LoadComboBoxes()
        {
            var employees = _employeeBll.Select().Employees;
            cmbDeliveryPerson.DataSource = employees;

            // For the display, you can use the new FullName property for convenience
            cmbDeliveryPerson.DisplayMember = "FullName";

            // --- THIS IS THE FIX ---
            // Change the ValueMember to match the property name in your EmployeeDetailDTO.
            cmbDeliveryPerson.ValueMember = "EmployeeID";

            // Load statuses
            cmbStatus.Items.AddRange(new string[] { "Pending", "Assigned", "In Transit", "Delivered", "Cancelled", "Failed" });
        }

        private void PopulateFields()
        {
            lblCustomerName.Text = _dto.CustomerName;
            lblAddress.Text = _dto.Address;

            // Set delivery person
            if (_dto.DeliveryPersonID.HasValue)
            {
                cmbDeliveryPerson.SelectedValue = _dto.DeliveryPersonID.Value;
            }
            else
            {
                cmbDeliveryPerson.SelectedIndex = -1;
            }

            // Set status
            cmbStatus.SelectedItem = _dto.Status;

            // Set dates, handling potential nulls
            dtpAssignedDate.Value = _dto.AssignedDate ?? DateTime.Now;
            dtpAssignedDate.Checked = _dto.AssignedDate.HasValue;

            dtpDeliveredDate.Value = _dto.DeliveredDate ?? DateTime.Now;
            dtpDeliveredDate.Checked = _dto.DeliveredDate.HasValue;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbStatus.SelectedIndex == -1)
            {
                MaterialMessageBox.Show("Please select a status.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbStatus.SelectedItem.ToString() != "Pending" && cmbDeliveryPerson.SelectedIndex == -1)
            {
                MaterialMessageBox.Show("Please assign a delivery person unless the status is 'Pending'.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Update DTO with form values
            _dto.DeliveryPersonID = (int?)cmbDeliveryPerson.SelectedValue;
            _dto.Status = cmbStatus.SelectedItem.ToString();
            _dto.AssignedDate = dtpAssignedDate.Checked ? dtpAssignedDate.Value : (DateTime?)null;
            _dto.DeliveredDate = dtpDeliveredDate.Checked ? dtpDeliveredDate.Value : (DateTime?)null;

            if (_bll.Update(_dto))
            {
                MaterialMessageBox.Show("Delivery updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MaterialMessageBox.Show("Failed to update delivery.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;
using System;
using System.Windows.Forms;

namespace Erp_V1
{
    public partial class FrmSupplier : Form
    {
        private readonly SupplierBLL bll = new SupplierBLL();

        // Public property to hold supplier details for update operations.
        public SupplierDetailDTO Detail { get; set; } = new SupplierDetailDTO();

        // Public property to indicate update mode.
        public bool IsUpdate { get; set; } = false;

        public FrmSupplier()
        {
            InitializeComponent();
        }

        private void FrmSupplier_Load(object sender, EventArgs e)
        {
            if (IsUpdate)
            {
                // Populate textboxes with existing supplier data.
                txtSupplierName.Text = Detail.SupplierName;
                txtphone.Text = Detail.PhoneNumber;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string supplierName = txtSupplierName.Text.Trim();
            string phoneNumber = txtphone.Text.Trim();

            // Validate supplier name.
            if (string.IsNullOrEmpty(supplierName))
            {
                MessageBox.Show("Supplier Name is empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!GeneralN.IsValidCategoryName(supplierName))  // Adjust validation as needed.
            {
                MessageBox.Show("Invalid Supplier Name. It should only contain letters and spaces.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate phone number.
            if (string.IsNullOrEmpty(phoneNumber))
            {
                MessageBox.Show("Phone number is empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!IsUpdate) // Insert new supplier.
            {
                var supplier = new SupplierDetailDTO
                {
                    SupplierName = supplierName,
                    PhoneNumber = phoneNumber,
                };

                if (bll.Insert(supplier))
                {
                    MessageBox.Show("Supplier was added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSupplierName.Clear();
                    txtphone.Clear();
                }
                else
                {
                    MessageBox.Show("Failed to add supplier.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else // Update existing supplier.
            {
                if (Detail.SupplierName.Equals(supplierName, StringComparison.OrdinalIgnoreCase) &&
                    Detail.PhoneNumber.Equals(phoneNumber, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("There is no change in the supplier information.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Detail.SupplierName = supplierName;
                    Detail.PhoneNumber = phoneNumber;

                    if (bll.Update(Detail))
                    {
                        MessageBox.Show("Supplier was updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Failed to update supplier.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Allow Enter key to trigger the save operation.
        private void FrmSupplier_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnSave_Click(sender, e);
            }
        }
    }
}

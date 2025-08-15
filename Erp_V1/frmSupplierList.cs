using Erp_V1.BLL;
using Erp_V1.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Erp_V1
{
    public partial class frmSupplierList : Form
    {
        // Business objects.
        private SupplierBLL supplierBLL = new SupplierBLL();
        private SupplierDTO supplierDTO = new SupplierDTO();
        private SupplierDetailDTO selectedSupplier = new SupplierDetailDTO();

        public frmSupplierList()
        {
            InitializeComponent();
        }

        // Close the supplier list form.
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Open the supplier form to add a new supplier.
        private void btnSave_Click(object sender, EventArgs e)
        {
            using (FrmSupplier frm = new FrmSupplier())
            {
                frm.ShowDialog();
            }
            // Refresh supplier list after the dialog closes.
            supplierDTO = supplierBLL.Select();
            dataGridView1.DataSource = supplierDTO.Suppliers;
        }

        // On form load, bind the supplier list grid.
        private void frmSupplierList_Load_1(object sender, EventArgs e)
        {
            supplierDTO = supplierBLL.Select();
            dataGridView1.DataSource = supplierDTO.Suppliers;
            // Hide the ID column and set the header for the Supplier Name column.
            if (dataGridView1.Columns.Count > 0)
            {
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Supplier Name";
                dataGridView1.Columns[2].HeaderText = "Supplier Number";
                dataGridView1.Columns[4].Visible = false;
                dataGridView1.Columns[3].Visible = false;
            }
        }

        // Filter the supplier list based on the entered supplier name.
        private void txtSupplierName_TextChanged_1(object sender, EventArgs e)
        {
            List<SupplierDetailDTO> filteredList = supplierDTO.Suppliers
                .Where(s => s.SupplierName.IndexOf(txtSupplierName.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();
            dataGridView1.DataSource = filteredList;
        }

        // When a supplier row is selected, store the selected supplier details.
        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            selectedSupplier = dataGridView1.Rows[e.RowIndex].DataBoundItem as SupplierDetailDTO;
        }

        // Open the supplier form in update mode for the selected supplier.
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedSupplier == null || selectedSupplier.SupplierID == 0)
            {
                MessageBox.Show("Please select a supplier from the table.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (FrmSupplier frm = new FrmSupplier())
            {
                // Use the properties defined in FrmSupplier.
                frm.Detail = selectedSupplier;
                frm.IsUpdate = true;
                this.Hide();
                frm.ShowDialog();
                this.Visible = true;
            }
            // Refresh supplier list after update.
            supplierBLL = new SupplierBLL();
            supplierDTO = supplierBLL.Select();
            dataGridView1.DataSource = supplierDTO.Suppliers;
        }

        // Delete the selected supplier.
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedSupplier == null || selectedSupplier.SupplierID == 0)
            {
                MessageBox.Show("Please select a supplier from the table.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete this supplier?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (supplierBLL.Delete(selectedSupplier))
                {
                    MessageBox.Show("Supplier was deleted successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    supplierBLL = new SupplierBLL();
                    supplierDTO = supplierBLL.Select();
                    dataGridView1.DataSource = supplierDTO.Suppliers;
                    txtSupplierName.Clear();
                }
            }
        }

        
    }
}

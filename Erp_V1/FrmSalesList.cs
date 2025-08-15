
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Erp_V1
{
    public partial class FrmSalesList : DevExpress.XtraEditors.XtraForm
    {
        public FrmSalesList()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void txtSalesAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FrmSales frm = new FrmSales();
            frm.dto = dto;
            FrmSales frmSales = new FrmSales();
            frmSales.ShowDialog();
            dto = bll.Select();
            dataGridView1.DataSource = dto.Sales;
            CleanFilers();
        }
        SalesBLL bll = new SalesBLL();
        SalesDTO dto = new SalesDTO();
        SalesDetailDTO detail = new SalesDetailDTO();
        private void FrmSalesList_Load(object sender, EventArgs e)
        {
            dto = bll.Select();
            dataGridView1.DataSource = dto.Sales;
            dataGridView1.Columns[0].HeaderText = "Customer Name";
            dataGridView1.Columns[1].HeaderText = "Product Name";
            dataGridView1.Columns[2].HeaderText = "Category Name";
            dataGridView1.Columns[6].HeaderText = "Sales Amount";
            dataGridView1.Columns[7].HeaderText = "Price";
            dataGridView1.Columns[8].HeaderText = "Sales Date";

            // Hide unnecessary columns
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns[11].Visible = false;
            dataGridView1.Columns[12].Visible = false;
            dataGridView1.Columns[13].Visible = false;

            // Set up the category ComboBox
            cmbCategory.DataSource = dto.Categories;
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "ID";
            cmbCategory.SelectedIndex = -1;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<SalesDetailDTO> filteredList = dto.Sales;

            // Filter by Product Name
            if (!string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                filteredList = filteredList.Where(x => x.ProductName.Contains(txtProductName.Text)).ToList();
            }

            // Filter by Customer Name
            if (!string.IsNullOrWhiteSpace(txtCustomerName.Text))
            {
                filteredList = filteredList.Where(x => x.CustomerName.Contains(txtCustomerName.Text)).ToList();
            }

            // Filter by Category
            if (cmbCategory.SelectedIndex != -1)
            {
                int selectedCategoryId = Convert.ToInt32(cmbCategory.SelectedValue);
                filteredList = filteredList.Where(x => x.CategoryID == selectedCategoryId).ToList();
            }

            // Filter by Price
            if (!string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                if (int.TryParse(txtPrice.Text, out int price))
                {
                    if (rbPriceEquals.Checked)
                        filteredList = filteredList.Where(x => x.Price == price).ToList();
                    else if (rbPriceMore.Checked)
                        filteredList = filteredList.Where(x => x.Price > price).ToList();
                    else if (rbPriceLess.Checked)
                        filteredList = filteredList.Where(x => x.Price < price).ToList();
                    else
                        MessageBox.Show("Please select a criterion from the price group.");
                }
                else
                {
                    MessageBox.Show("Invalid price format.");
                }
            }

            // Filter by Sales Amount
            if (!string.IsNullOrWhiteSpace(txtSalesAmount.Text))
            {
                if (int.TryParse(txtSalesAmount.Text, out int salesAmount))
                {
                    if (rbSalesEqual.Checked)
                        filteredList = filteredList.Where(x => x.SalesAmount == salesAmount).ToList();
                    else if (rbSalesMore.Checked)
                        filteredList = filteredList.Where(x => x.SalesAmount > salesAmount).ToList();
                    else if (rbSalesLess.Checked)
                        filteredList = filteredList.Where(x => x.SalesAmount < salesAmount).ToList();
                    else
                        MessageBox.Show("Please select a criterion from the sales amount group.");
                }
                else
                {
                    MessageBox.Show("Invalid sales amount format.");
                }
            }

            // Filter by Date Range
            if (chDate.Checked)
            {
                DateTime startDate = dpStart.Value.Date;
                DateTime endDate = dpEnd.Value.Date;
                filteredList = filteredList.Where(x => x.SalesDate >= startDate && x.SalesDate <= endDate).ToList();
            }

            // Update the DataGridView with the filtered list
            dataGridView1.DataSource = filteredList;
        }



        private void btnClean_Click(object sender, EventArgs e)
        {
            CleanFilers();
        }

        private void CleanFilers()
        {
            // Clear text boxes
            txtProductName.Text = string.Empty;
            txtCustomerName.Text = string.Empty;
            txtPrice.Text = string.Empty;
            txtSalesAmount.Text = string.Empty;

            // Uncheck radio buttons
            rbPriceEquals.Checked = false;
            rbPriceMore.Checked = false;
            rbPriceLess.Checked = false;
            rbPriceEquals.Checked = false;
            rbSalesMore.Checked = false;
            rbSalesLess.Checked = false;

            // Reset date pickers to today
            dpStart.Value = DateTime.Today;
            dpEnd.Value = DateTime.Today;

            // Reset combo box selection
            cmbCategory.SelectedIndex = -1;

            // Reset data source
            dataGridView1.DataSource = dto.Sales;
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail = new SalesDetailDTO();
            detail.SalesID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[10].Value);
            detail.ProductID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[4].Value);
            detail.CustomerName = dataGridView1.Rows[e.RowIndex].Cells[0].Value?.ToString();
            detail.ProductName = dataGridView1.Rows[e.RowIndex].Cells[1].Value?.ToString();
            detail.Price = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[7].Value);
            detail.SalesAmount = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[6].Value);
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (detail.SalesID == 0)
            {
                MessageBox.Show("Please select a sale from the table");
            }
            else
            {
                // Create and initialize FrmSales
                FrmSales frm = new FrmSales
                {
                    isUpdate = true,
                    detail = detail,
                    dto = dto
                };

                // Hide current form and show the FrmSales dialog
                this.Hide();
                frm.ShowDialog();
                this.Visible = true;

                // Reinitialize BLL and update DataGridView
                SalesBLL bll = new SalesBLL();
                dto = bll.Select();
                dataGridView1.DataSource = dto.Sales;

                // Clear filters if the method exists
                if (this.GetType().GetMethod("CleanFilters") != null)
                {
                    CleanFilers();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (detail.SalesID == 0)
                MessageBox.Show("please select a sales from table");
            else
            {
                DialogResult result = MessageBox.Show("Are you sure ?", "WARING!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    if (bll.Delete(detail))
                    {
                        MessageBox.Show("Sasles WAs Deleted");
                        bll = new SalesBLL();
                        dto = bll.Select();
                        dataGridView1.DataSource = dto.Sales;
                        CleanFilers();
                    }

                }
            }
        }
    }
}
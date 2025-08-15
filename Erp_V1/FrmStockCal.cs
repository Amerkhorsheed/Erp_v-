using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;

namespace Erp_V1
{
    public partial class FrmStockCal : Form
    {
        private readonly ProductBLL _productBll = new ProductBLL();
        private ProductDTO _productDto = new ProductDTO();

        public FrmStockCal()
        {
            InitializeComponent();
            ConfigureGridAppearance();
        }

        private void ConfigureGridAppearance()
        {
            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.MidnightBlue;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightSteelBlue;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkSlateBlue;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView1.RowTemplate.Height = 30;
        }

        private void FrmStockCal_Load(object sender, EventArgs e)
        {
            LoadProductData();
            ConfigureGridColumns();
        }

        private void LoadProductData()
        {
            _productDto = _productBll.Select();
            dataGridView1.DataSource = _productDto.Products;
        }

        private void ConfigureGridColumns()
        {
            if (dataGridView1.Columns["ProductName"] != null)
                dataGridView1.Columns["ProductName"].HeaderText = "Product Name";
            if (dataGridView1.Columns["CategoryName"] != null)
                dataGridView1.Columns["CategoryName"].HeaderText = "Category";
            if (dataGridView1.Columns["stockAmount"] != null)
            {
                dataGridView1.Columns["stockAmount"].HeaderText = "Stock Level";
                dataGridView1.Columns["stockAmount"].DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            }

            string[] hiddenColumns = { "ProductID", "CategoryID", "isCategoryDeleted" };
            foreach (var col in hiddenColumns)
            {
                if (dataGridView1.Columns[col] != null)
                    dataGridView1.Columns[col].Visible = false;
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtStockLevel.Text, out int threshold))
            {
                var filtered = _productDto.Products
                    .Where(p => p.stockAmount <= threshold)
                    .OrderBy(p => p.stockAmount)
                    .ToList();
                dataGridView1.DataSource = filtered;
                MessageBox.Show($"{filtered.Count} products below stock threshold", "Filter Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please enter a valid numeric stock threshold", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStockLevel.Focus();
            }
        }

        private void btnGetBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["stockAmount"].Value != null && int.TryParse(row.Cells["stockAmount"].Value.ToString(), out int stock))
                {
                    row.DefaultCellStyle.ForeColor = stock <= 0 ? Color.Red : stock <= 10 ? Color.Orange : Color.DarkSlateGray;
                }
            }
        }
    }
}

using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;
using System.Collections.Generic;

namespace Erp_V1
{
    public partial class FrmStockAlert : XtraForm
    {
        private ProductBLL bll = new ProductBLL();
        private ProductDTO dto = new ProductDTO();

        private int stockThreshold = 10; // Default value
        private string comparisonOperator = "<="; // Default operator

        public FrmStockAlert()
        {
            InitializeComponent();
            ApplyCustomStyles();
        }

        private void btnGetBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmStockAlert_Load(object sender, EventArgs e)
        {
            // Open filter form and get user input
            using (FrmStockFilter filterForm = new FrmStockFilter())
            {
                if (filterForm.ShowDialog() == DialogResult.OK)
                {
                    stockThreshold = filterForm.StockThreshold;
                    comparisonOperator = filterForm.ComparisonOperator;
                }
            }

            dto = bll.Select();
            dto.Products = ApplyStockFilter(dto.Products);
            dataGridView1.DataSource = dto.Products;

            // Set column headers
            dataGridView1.Columns[0].HeaderText = "Product Name";
            dataGridView1.Columns[1].HeaderText = "Category Name";
            dataGridView1.Columns[2].HeaderText = "Stock Amount";

            // Hide unnecessary columns
            for (int i = 3; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].Visible = false;
            }
        }

        private List<ProductDetailDTO> ApplyStockFilter(List<ProductDetailDTO> products)
        {
            switch (comparisonOperator)
            {
                case "<=":
                    return products.Where(x => x.stockAmount <= stockThreshold).ToList();
                case ">=":
                    return products.Where(x => x.stockAmount >= stockThreshold).ToList();
                case "<":
                    return products.Where(x => x.stockAmount < stockThreshold).ToList();
                case ">":
                    return products.Where(x => x.stockAmount > stockThreshold).ToList();
                case "=":
                    return products.Where(x => x.stockAmount == stockThreshold).ToList();
                default:
                    return products; // Default case, should not happen
            }
        }

        private void ApplyCustomStyles()
        {
            this.BackColor = Color.FromArgb(45, 45, 48);
            this.ForeColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "🔔 Stock Alert";

            panel1.BackColor = Color.FromArgb(30, 144, 255);
            panel1.Height = 65;

            btnGetBack.Text = "⬅ Go Back";
            btnGetBack.Font = new Font("Tahoma", 10, FontStyle.Bold);
            btnGetBack.Appearance.BackColor = Color.FromArgb(220, 20, 60);
            btnGetBack.Appearance.ForeColor = Color.White;
            btnGetBack.Appearance.Options.UseBackColor = true;
            btnGetBack.Appearance.Options.UseForeColor = true;
            btnGetBack.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            btnGetBack.LookAndFeel.UseDefaultLookAndFeel = false;
            btnGetBack.Size = new Size(220, 50);
            btnGetBack.Location = new Point((panel1.Width - btnGetBack.Width) / 2, (panel1.Height - btnGetBack.Height) / 2);

            dataGridView1.BackgroundColor = Color.FromArgb(60, 63, 65);
            dataGridView1.DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 55);
            dataGridView1.DefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(100, 149, 237);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BorderStyle = BorderStyle.None;
        }
    }
}

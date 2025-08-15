// FrmStockFilter.cs
using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;


namespace Erp_V1
{
    public partial class FrmStockFilter : XtraForm
    {
        public int StockThreshold { get; private set; }
        public string ComparisonOperator { get; private set; } = "<="; // Default condition

        public FrmStockFilter()
        {
            InitializeComponent();
            ApplyCustomStyles();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtStockAmount.Text, out int value))
            {
                StockThreshold = value;
                ComparisonOperator = cmbOperator.SelectedItem.ToString();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                XtraMessageBox.Show("Please enter a valid number.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyCustomStyles()
        {
            this.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Stock Filter";

            lblCondition.ForeColor = System.Drawing.Color.White;

            btnApply.Appearance.BackColor = System.Drawing.Color.FromArgb(30, 144, 255);
            btnApply.Appearance.ForeColor = System.Drawing.Color.White;
            btnApply.Appearance.Options.UseBackColor = true;
            btnApply.Appearance.Options.UseForeColor = true;
        }
    }
}



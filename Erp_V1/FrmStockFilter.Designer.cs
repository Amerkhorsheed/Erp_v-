// FrmStockFilter.Designer.cs
using System.Windows.Forms;

namespace Erp_V1
{
    partial class FrmStockFilter
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblCondition;
        private System.Windows.Forms.ComboBox cmbOperator;
        private TextBox txtStockAmount;
        private DevExpress.XtraEditors.SimpleButton btnApply;

        private void InitializeComponent()
        {
            this.lblCondition = new Label();
            this.cmbOperator = new System.Windows.Forms.ComboBox();
            this.txtStockAmount = new TextBox();
            this.btnApply = new DevExpress.XtraEditors.SimpleButton();

            // 
            // lblCondition
            // 
            this.lblCondition.AutoSize = true;
            this.lblCondition.Location = new System.Drawing.Point(30, 20);
            this.lblCondition.Size = new System.Drawing.Size(120, 20);
            this.lblCondition.Text = "Set Stock Condition:";

            // 
            // cmbOperator
            // 
            this.cmbOperator.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbOperator.Location = new System.Drawing.Point(30, 50);
            this.cmbOperator.Size = new System.Drawing.Size(60, 24);
            this.cmbOperator.Items.AddRange(new string[] { "<=", ">=", "<", ">", "=" });
            this.cmbOperator.SelectedIndex = 0;

            // 
            // txtStockAmount
            // 
            this.txtStockAmount.Location = new System.Drawing.Point(100, 50);
            this.txtStockAmount.Size = new System.Drawing.Size(100, 24);

            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(30, 90);
            this.btnApply.Size = new System.Drawing.Size(170, 30);
            this.btnApply.Text = "✔ Apply";
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);

            // 
            // FrmStockFilter
            // 
            this.ClientSize = new System.Drawing.Size(250, 140);
            this.Controls.Add(this.lblCondition);
            this.Controls.Add(this.cmbOperator);
            this.Controls.Add(this.txtStockAmount);
            this.Controls.Add(this.btnApply);
            this.Name = "FrmStockFilter";
            this.Text = "Stock Filter";
        }
    }
}

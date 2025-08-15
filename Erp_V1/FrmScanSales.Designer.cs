using SD = System.Drawing; // Alias for System.Drawing

namespace Erp_V1
{
    partial class FrmScanSales
    {
        private System.ComponentModel.IContainer components = null;
        private DevExpress.XtraEditors.SimpleButton btnUploadFile;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private System.Windows.Forms.ComboBox cmbProduct; // ComboBox for product selection.
        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.Label lblPrice;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnUploadFile = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.cmbProduct = new System.Windows.Forms.ComboBox();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.lblProduct = new System.Windows.Forms.Label();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.lblAmount = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnUploadFile
            // 
            this.btnUploadFile.Location = new SD.Point(30, 30);
            this.btnUploadFile.Name = "btnUploadFile";
            this.btnUploadFile.Size = new SD.Size(160, 45);
            this.btnUploadFile.Text = "Upload File";
            this.btnUploadFile.Click += new System.EventHandler(this.btnUploadFile_Click);
            this.btnUploadFile.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnUploadFile.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            // 
            // lblProduct
            // 
            this.lblProduct.Location = new SD.Point(30, 100);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new SD.Size(110, 25);
            this.lblProduct.Text = "Product:";
            // 
            // cmbProduct
            // 
            this.cmbProduct.Location = new SD.Point(150, 100);
            this.cmbProduct.Name = "cmbProduct";
            this.cmbProduct.Size = new SD.Size(250, 27);
            // 
            // lblCustomer
            // 
            this.lblCustomer.Location = new SD.Point(30, 140);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new SD.Size(110, 25);
            this.lblCustomer.Text = "Customer:";
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Location = new SD.Point(150, 140);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new SD.Size(250, 27);
            // 
            // lblAmount
            // 
            this.lblAmount.Location = new SD.Point(30, 180);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new SD.Size(110, 25);
            this.lblAmount.Text = "Amount:";
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new SD.Point(150, 180);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new SD.Size(250, 27);
            // 
            // lblPrice
            // 
            this.lblPrice.Location = new SD.Point(30, 220);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new SD.Size(110, 25);
            this.lblPrice.Text = "Price:";
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new SD.Point(150, 220);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new SD.Size(250, 27);
            // 
            // btnSave
            // 
            this.btnSave.Location = new SD.Point(150, 280);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new SD.Size(160, 45);
            this.btnSave.Text = "Save Sales";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnSave.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnSave.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            // 
            // FrmScanSales
            // 
            this.ClientSize = new SD.Size(450, 350);
            this.Controls.Add(this.btnUploadFile);
            this.Controls.Add(this.lblProduct);
            this.Controls.Add(this.cmbProduct);
            this.Controls.Add(this.lblCustomer);
            this.Controls.Add(this.txtCustomerName);
            this.Controls.Add(this.lblAmount);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.btnSave);
            this.Name = "FrmScanSales";
            this.Text = "Scan Sales Document";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

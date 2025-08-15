//namespace Erp_V1
//{
//    partial class FrmSupplierManagement
//    {
//        private System.ComponentModel.IContainer components = null;

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && (components != null))
//            {
//                components.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        #region Windows Form Designer generated code

//        private void InitializeComponent()
//        {
//            this.panelMain = new System.Windows.Forms.Panel();
//            this.splitContainer = new System.Windows.Forms.SplitContainer();
//            this.panelInput = new System.Windows.Forms.Panel();
//            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
//            this.txtCurrentStock = new System.Windows.Forms.TextBox();
//            this.lblCurrentStock = new DevExpress.XtraEditors.LabelControl();
//            this.txtDiscount = new System.Windows.Forms.TextBox();
//            this.lblDiscount = new DevExpress.XtraEditors.LabelControl();
//            this.txtSuppliedAmount = new System.Windows.Forms.TextBox();
//            this.lblSuppliedAmount = new DevExpress.XtraEditors.LabelControl();
//            this.txtPrice = new System.Windows.Forms.TextBox();
//            this.lblPrice = new DevExpress.XtraEditors.LabelControl();
//            this.txtProductName = new System.Windows.Forms.TextBox();
//            this.lblProduct = new DevExpress.XtraEditors.LabelControl();
//            this.txtSupplierName = new System.Windows.Forms.TextBox();
//            this.lblSupplier = new DevExpress.XtraEditors.LabelControl();
//            this.panelSelection = new System.Windows.Forms.Panel();
//            this.grpSuppliers = new System.Windows.Forms.GroupBox();
//            this.gridSupplier = new System.Windows.Forms.DataGridView();
//            this.grpProducts = new System.Windows.Forms.GroupBox();
//            this.gridProduct = new System.Windows.Forms.DataGridView();
//            this.cmbCategory = new System.Windows.Forms.ComboBox();
//            this.lblCategory = new DevExpress.XtraEditors.LabelControl();
//            this.panelActions = new System.Windows.Forms.Panel();
//            this.lblStatus = new DevExpress.XtraEditors.LabelControl();
//            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
//            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
//            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
//            this.panelMain.SuspendLayout();
//            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
//            this.splitContainer.Panel1.SuspendLayout();
//            this.splitContainer.Panel2.SuspendLayout();
//            this.splitContainer.SuspendLayout();
//            this.panelInput.SuspendLayout();
//            this.panelSelection.SuspendLayout();
//            this.grpSuppliers.SuspendLayout();
//            ((System.ComponentModel.ISupportInitialize)(this.gridSupplier)).BeginInit();
//            this.grpProducts.SuspendLayout();
//            ((System.ComponentModel.ISupportInitialize)(this.gridProduct)).BeginInit();
//            this.panelActions.SuspendLayout();
//            this.SuspendLayout();
//            // 
//            // panelMain
//            // 
//            this.panelMain.Controls.Add(this.splitContainer);
//            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.panelMain.Location = new System.Drawing.Point(0, 0);
//            this.panelMain.Name = "panelMain";
//            this.panelMain.Size = new System.Drawing.Size(984, 561);
//            this.panelMain.TabIndex = 0;
//            // 
//            // splitContainer
//            // 
//            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
//            this.splitContainer.Location = new System.Drawing.Point(0, 0);
//            this.splitContainer.Name = "splitContainer";
//            // 
//            // splitContainer.Panel1
//            // 
//            this.splitContainer.Panel1.Controls.Add(this.panelInput);
//            this.splitContainer.Panel1MinSize = 300;
//            // 
//            // splitContainer.Panel2
//            // 
//            this.splitContainer.Panel2.Controls.Add(this.panelSelection);
//            this.splitContainer.Size = new System.Drawing.Size(984, 561);
//            this.splitContainer.SplitterDistance = 300;
//            this.splitContainer.TabIndex = 2;
//            // 
//            // panelInput
//            // 
//            this.panelInput.BackColor = System.Drawing.Color.WhiteSmoke;
//            this.panelInput.Controls.Add(this.lblTitle);
//            this.panelInput.Controls.Add(this.txtCurrentStock);
//            this.panelInput.Controls.Add(this.lblCurrentStock);
//            this.panelInput.Controls.Add(this.txtDiscount);
//            this.panelInput.Controls.Add(this.lblDiscount);
//            this.panelInput.Controls.Add(this.txtSuppliedAmount);
//            this.panelInput.Controls.Add(this.lblSuppliedAmount);
//            this.panelInput.Controls.Add(this.txtPrice);
//            this.panelInput.Controls.Add(this.lblPrice);
//            this.panelInput.Controls.Add(this.txtProductName);
//            this.panelInput.Controls.Add(this.lblProduct);
//            this.panelInput.Controls.Add(this.txtSupplierName);
//            this.panelInput.Controls.Add(this.lblSupplier);
//            this.panelInput.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.panelInput.Location = new System.Drawing.Point(0, 0);
//            this.panelInput.Name = "panelInput";
//            this.panelInput.Padding = new System.Windows.Forms.Padding(15);
//            this.panelInput.Size = new System.Drawing.Size(300, 561);
//            this.panelInput.TabIndex = 0;
//            // 
//            // lblTitle
//            // 
//            this.lblTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
//            this.lblTitle.Appearance.Options.UseFont = true;
//            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
//            this.lblTitle.Location = new System.Drawing.Point(15, 15);
//            this.lblTitle.Name = "lblTitle";
//            this.lblTitle.Padding = new System.Windows.Forms.Padding(0, 0, 0, 15);
//            this.lblTitle.Size = new System.Drawing.Size(150, 43);
//            this.lblTitle.TabIndex = 12;
//            this.lblTitle.Text = "Supplier Details";
//            // 
//            // txtCurrentStock
//            // 
//            this.txtCurrentStock.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
//            | System.Windows.Forms.AnchorStyles.Right)));
//            this.txtCurrentStock.Font = new System.Drawing.Font("Segoe UI", 10F);
//            this.txtCurrentStock.Location = new System.Drawing.Point(15, 320);
//            this.txtCurrentStock.Name = "txtCurrentStock";
//            this.txtCurrentStock.ReadOnly = true;
//            this.txtCurrentStock.Size = new System.Drawing.Size(270, 30);
//            this.txtCurrentStock.TabIndex = 11;
//            // 
//            // lblCurrentStock
//            // 
//            this.lblCurrentStock.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
//            this.lblCurrentStock.Appearance.Options.UseFont = true;
//            this.lblCurrentStock.Location = new System.Drawing.Point(15, 295);
//            this.lblCurrentStock.Name = "lblCurrentStock";
//            this.lblCurrentStock.Size = new System.Drawing.Size(105, 23);
//            this.lblCurrentStock.TabIndex = 10;
//            this.lblCurrentStock.Text = "Current Stock";
//            // 
//            // txtDiscount
//            // 
//            this.txtDiscount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
//            | System.Windows.Forms.AnchorStyles.Right)));
//            this.txtDiscount.Font = new System.Drawing.Font("Segoe UI", 10F);
//            this.txtDiscount.Location = new System.Drawing.Point(15, 265);
//            this.txtDiscount.Name = "txtDiscount";
//            this.txtDiscount.Size = new System.Drawing.Size(270, 30);
//            this.txtDiscount.TabIndex = 9;
//            this.txtDiscount.Text = "0%";
//            // 
//            // lblDiscount
//            // 
//            this.lblDiscount.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
//            this.lblDiscount.Appearance.Options.UseFont = true;
//            this.lblDiscount.Location = new System.Drawing.Point(15, 240);
//            this.lblDiscount.Name = "lblDiscount";
//            this.lblDiscount.Size = new System.Drawing.Size(67, 23);
//            this.lblDiscount.TabIndex = 8;
//            this.lblDiscount.Text = "Discount";
//            // 
//            // txtSuppliedAmount
//            // 
//            this.txtSuppliedAmount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
//            | System.Windows.Forms.AnchorStyles.Right)));
//            this.txtSuppliedAmount.Font = new System.Drawing.Font("Segoe UI", 10F);
//            this.txtSuppliedAmount.Location = new System.Drawing.Point(15, 210);
//            this.txtSuppliedAmount.Name = "txtSuppliedAmount";
//            this.txtSuppliedAmount.Size = new System.Drawing.Size(270, 30);
//            this.txtSuppliedAmount.TabIndex = 7;
//            this.txtSuppliedAmount.Text = "1";
//            // 
//            // lblSuppliedAmount
//            // 
//            this.lblSuppliedAmount.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
//            this.lblSuppliedAmount.Appearance.Options.UseFont = true;
//            this.lblSuppliedAmount.Location = new System.Drawing.Point(15, 185);
//            this.lblSuppliedAmount.Name = "lblSuppliedAmount";
//            this.lblSuppliedAmount.Size = new System.Drawing.Size(133, 23);
//            this.lblSuppliedAmount.TabIndex = 6;
//            this.lblSuppliedAmount.Text = "Supplied Amount";
//            // 
//            // txtPrice
//            // 
//            this.txtPrice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
//            | System.Windows.Forms.AnchorStyles.Right)));
//            this.txtPrice.Font = new System.Drawing.Font("Segoe UI", 10F);
//            this.txtPrice.Location = new System.Drawing.Point(15, 155);
//            this.txtPrice.Name = "txtPrice";
//            this.txtPrice.Size = new System.Drawing.Size(270, 30);
//            this.txtPrice.TabIndex = 5;
//            // 
//            // lblPrice
//            // 
//            this.lblPrice.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
//            this.lblPrice.Appearance.Options.UseFont = true;
//            this.lblPrice.Location = new System.Drawing.Point(15, 130);
//            this.lblPrice.Name = "lblPrice";
//            this.lblPrice.Size = new System.Drawing.Size(37, 23);
//            this.lblPrice.TabIndex = 4;
//            this.lblPrice.Text = "Price";
//            // 
//            // txtProductName
//            // 
//            this.txtProductName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
//            | System.Windows.Forms.AnchorStyles.Right)));
//            this.txtProductName.Font = new System.Drawing.Font("Segoe UI", 10F);
//            this.txtProductName.Location = new System.Drawing.Point(15, 100);
//            this.txtProductName.Name = "txtProductName";
//            this.txtProductName.ReadOnly = true;
//            this.txtProductName.Size = new System.Drawing.Size(270, 30);
//            this.txtProductName.TabIndex = 3;
//            // 
//            // lblProduct
//            // 
//            this.lblProduct.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
//            this.lblProduct.Appearance.Options.UseFont = true;
//            this.lblProduct.Location = new System.Drawing.Point(15, 75);
//            this.lblProduct.Name = "lblProduct";
//            this.lblProduct.Size = new System.Drawing.Size(60, 23);
//            this.lblProduct.TabIndex = 2;
//            this.lblProduct.Text = "Product";
//            // 
//            // txtSupplierName
//            // 
//            this.txtSupplierName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
//            | System.Windows.Forms.AnchorStyles.Right)));
//            this.txtSupplierName.Font = new System.Drawing.Font("Segoe UI", 10F);
//            this.txtSupplierName.Location = new System.Drawing.Point(15, 370);
//            this.txtSupplierName.Name = "txtSupplierName";
//            this.txtSupplierName.Size = new System.Drawing.Size(270, 30);
//            this.txtSupplierName.TabIndex = 1;
//            // 
//            // lblSupplier
//            // 
//            this.lblSupplier.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
//            this.lblSupplier.Appearance.Options.UseFont = true;
//            this.lblSupplier.Location = new System.Drawing.Point(15, 345);
//            this.lblSupplier.Name = "lblSupplier";
//            this.lblSupplier.Size = new System.Drawing.Size(113, 23);
//            this.lblSupplier.TabIndex = 0;
//            this.lblSupplier.Text = "Supplier Name";
//            // 
//            // panelSelection
//            // 
//            this.panelSelection.Controls.Add(this.grpSuppliers);
//            this.panelSelection.Controls.Add(this.grpProducts);
//            this.panelSelection.Controls.Add(this.cmbCategory);
//            this.panelSelection.Controls.Add(this.lblCategory);
//            this.panelSelection.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.panelSelection.Location = new System.Drawing.Point(0, 0);
//            this.panelSelection.Name = "panelSelection";
//            this.panelSelection.Padding = new System.Windows.Forms.Padding(10);
//            this.panelSelection.Size = new System.Drawing.Size(680, 561);
//            this.panelSelection.TabIndex = 1;
//            // 
//            // grpSuppliers
//            // 
//            this.grpSuppliers.Controls.Add(this.gridSupplier);
//            this.grpSuppliers.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.grpSuppliers.Font = new System.Drawing.Font("Segoe UI", 10F);
//            this.grpSuppliers.Location = new System.Drawing.Point(10, 305);
//            this.grpSuppliers.Name = "grpSuppliers";
//            this.grpSuppliers.Size = new System.Drawing.Size(660, 246);
//            this.grpSuppliers.TabIndex = 3;
//            this.grpSuppliers.TabStop = false;
//            this.grpSuppliers.Text = "Suppliers List";
//            // 
//            // gridSupplier
//            // 
//            this.gridSupplier.AllowUserToAddRows = false;
//            this.gridSupplier.AllowUserToDeleteRows = false;
//            this.gridSupplier.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
//            this.gridSupplier.BackgroundColor = System.Drawing.Color.White;
//            this.gridSupplier.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
//            this.gridSupplier.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.gridSupplier.Location = new System.Drawing.Point(3, 26);
//            this.gridSupplier.MultiSelect = false;
//            this.gridSupplier.Name = "gridSupplier";
//            this.gridSupplier.ReadOnly = true;
//            this.gridSupplier.RowHeadersWidth = 51;
//            this.gridSupplier.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
//            this.gridSupplier.Size = new System.Drawing.Size(654, 217);
//            this.gridSupplier.TabIndex = 0;
//            // 
//            // grpProducts
//            // 
//            this.grpProducts.Controls.Add(this.gridProduct);
//            this.grpProducts.Dock = System.Windows.Forms.DockStyle.Top;
//            this.grpProducts.Font = new System.Drawing.Font("Segoe UI", 10F);
//            this.grpProducts.Location = new System.Drawing.Point(10, 64);
//            this.grpProducts.Name = "grpProducts";
//            this.grpProducts.Size = new System.Drawing.Size(660, 241);
//            this.grpProducts.TabIndex = 2;
//            this.grpProducts.TabStop = false;
//            this.grpProducts.Text = "Product Selection";
//            // 
//            // gridProduct
//            // 
//            this.gridProduct.AllowUserToAddRows = false;
//            this.gridProduct.AllowUserToDeleteRows = false;
//            this.gridProduct.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
//            this.gridProduct.BackgroundColor = System.Drawing.Color.White;
//            this.gridProduct.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
//            this.gridProduct.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.gridProduct.Location = new System.Drawing.Point(3, 26);
//            this.gridProduct.MultiSelect = false;
//            this.gridProduct.Name = "gridProduct";
//            this.gridProduct.ReadOnly = true;
//            this.gridProduct.RowHeadersWidth = 51;
//            this.gridProduct.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
//            this.gridProduct.Size = new System.Drawing.Size(654, 212);
//            this.gridProduct.TabIndex = 0;
//            // 
//            // cmbCategory
//            // 
//            this.cmbCategory.Dock = System.Windows.Forms.DockStyle.Top;
//            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
//            this.cmbCategory.Font = new System.Drawing.Font("Segoe UI", 10F);
//            this.cmbCategory.FormattingEnabled = true;
//            this.cmbCategory.Location = new System.Drawing.Point(10, 33);
//            this.cmbCategory.Name = "cmbCategory";
//            this.cmbCategory.Size = new System.Drawing.Size(660, 31);
//            this.cmbCategory.TabIndex = 1;
//            // 
//            // lblCategory
//            // 
//            this.lblCategory.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
//            this.lblCategory.Appearance.Options.UseFont = true;
//            this.lblCategory.Dock = System.Windows.Forms.DockStyle.Top;
//            this.lblCategory.Location = new System.Drawing.Point(10, 10);
//            this.lblCategory.Name = "lblCategory";
//            this.lblCategory.Size = new System.Drawing.Size(69, 23);
//            this.lblCategory.TabIndex = 0;
//            this.lblCategory.Text = "Category";
//            // 
//            // panelActions
//            // 
//            this.panelActions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
//            this.panelActions.Controls.Add(this.lblStatus);
//            this.panelActions.Controls.Add(this.btnClear);
//            this.panelActions.Controls.Add(this.btnClose);
//            this.panelActions.Controls.Add(this.btnSave);
//            this.panelActions.Dock = System.Windows.Forms.DockStyle.Bottom;
//            this.panelActions.Location = new System.Drawing.Point(0, 561);
//            this.panelActions.Name = "panelActions";
//            this.panelActions.Size = new System.Drawing.Size(984, 50);
//            this.panelActions.TabIndex = 1;
//            // 
//            // lblStatus
//            // 
//            this.lblStatus.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
//            this.lblStatus.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
//            this.lblStatus.Appearance.Options.UseFont = true;
//            this.lblStatus.Appearance.Options.UseForeColor = true;
//            this.lblStatus.Location = new System.Drawing.Point(15, 15);
//            this.lblStatus.Name = "lblStatus";
//            this.lblStatus.Size = new System.Drawing.Size(0, 20);
//            this.lblStatus.TabIndex = 3;
//            // 
//            // btnClear
//            // 
//            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
//            this.btnClear.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
//            this.btnClear.Appearance.Options.UseFont = true;
//            this.btnClear.Location = new System.Drawing.Point(587, 10);
//            this.btnClear.Name = "btnClear";
//            this.btnClear.Size = new System.Drawing.Size(125, 30);
//            this.btnClear.TabIndex = 2;
//            this.btnClear.Text = "Clear";
//            // 
//            // btnClose
//            // 
//            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
//            this.btnClose.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
//            this.btnClose.Appearance.Options.UseFont = true;
//            this.btnClose.Location = new System.Drawing.Point(847, 10);
//            this.btnClose.Name = "btnClose";
//            this.btnClose.Size = new System.Drawing.Size(125, 30);
//            this.btnClose.TabIndex = 1;
//            this.btnClose.Text = "Close";
//            // 
//            // btnSave
//            // 
//            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
//            this.btnSave.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
//            this.btnSave.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
//            this.btnSave.Appearance.ForeColor = System.Drawing.Color.White;
//            this.btnSave.Appearance.Options.UseBackColor = true;
//            this.btnSave.Appearance.Options.UseFont = true;
//            this.btnSave.Appearance.Options.UseForeColor = true;
//            this.btnSave.Location = new System.Drawing.Point(717, 10);
//            this.btnSave.Name = "btnSave";
//            this.btnSave.Size = new System.Drawing.Size(125, 30);
//            this.btnSave.TabIndex = 0;
//            this.btnSave.Text = "Save Supplier";
//            // 
//            // FrmSupplierManagement
//            // 
//            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
//            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
//            this.ClientSize = new System.Drawing.Size(984, 611);
//            this.Controls.Add(this.panelMain);
//            this.Controls.Add(this.panelActions);
//            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
//            this.MinimumSize = new System.Drawing.Size(1000, 650);
//            this.Name = "FrmSupplierManagement";
//            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
//            this.Text = "Supplier Management";
//            this.Load += new System.EventHandler(this.FrmSupplierManagement_Load_1);
//            this.panelMain.ResumeLayout(false);
//            this.splitContainer.Panel1.ResumeLayout(false);
//            this.splitContainer.Panel2.ResumeLayout(false);
//            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
//            this.splitContainer.ResumeLayout(false);
//            this.panelInput.ResumeLayout(false);
//            this.panelInput.PerformLayout();
//            this.panelSelection.ResumeLayout(false);
//            this.panelSelection.PerformLayout();
//            this.grpSuppliers.ResumeLayout(false);
//            ((System.ComponentModel.ISupportInitialize)(this.gridSupplier)).EndInit();
//            this.grpProducts.ResumeLayout(false);
//            ((System.ComponentModel.ISupportInitialize)(this.gridProduct)).EndInit();
//            this.panelActions.ResumeLayout(false);
//            this.panelActions.PerformLayout();
//            this.ResumeLayout(false);

//        }

//        #endregion

//        private System.Windows.Forms.Panel panelMain;
//        private System.Windows.Forms.Panel panelInput;
//        private DevExpress.XtraEditors.LabelControl lblTitle;
//        private System.Windows.Forms.TextBox txtCurrentStock;
//        private DevExpress.XtraEditors.LabelControl lblCurrentStock;
//        private System.Windows.Forms.TextBox txtDiscount;
//        private DevExpress.XtraEditors.LabelControl lblDiscount;
//        private System.Windows.Forms.TextBox txtSuppliedAmount;
//        private DevExpress.XtraEditors.LabelControl lblSuppliedAmount;
//        private System.Windows.Forms.TextBox txtPrice;
//        private DevExpress.XtraEditors.LabelControl lblPrice;
//        private System.Windows.Forms.TextBox txtProductName;
//        private DevExpress.XtraEditors.LabelControl lblProduct;
//        private System.Windows.Forms.TextBox txtSupplierName;
//        private DevExpress.XtraEditors.LabelControl lblSupplier;
//        private System.Windows.Forms.Panel panelSelection;
//        private System.Windows.Forms.GroupBox grpSuppliers;
//        private System.Windows.Forms.DataGridView gridSupplier;
//        private System.Windows.Forms.GroupBox grpProducts;
//        private System.Windows.Forms.DataGridView gridProduct;
//        private System.Windows.Forms.ComboBox cmbCategory;
//        private DevExpress.XtraEditors.LabelControl lblCategory;
//        private System.Windows.Forms.Panel panelActions;
//        private DevExpress.XtraEditors.SimpleButton btnClear;
//        private DevExpress.XtraEditors.SimpleButton btnClose;
//        private DevExpress.XtraEditors.SimpleButton btnSave;
//        private System.Windows.Forms.SplitContainer splitContainer;
//        private DevExpress.XtraEditors.LabelControl lblStatus;
//    }
//}
namespace Erp_V1
{
    partial class FrmPartialPaymentSales
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.gridProducts = new System.Windows.Forms.DataGridView();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.gridCustomers = new System.Windows.Forms.DataGridView();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.btnAddToCart = new DevExpress.XtraEditors.SimpleButton();
            this.numQuantity = new System.Windows.Forms.NumericUpDown();
            this.txtAvailableStock = new DevExpress.XtraEditors.TextEdit();
            this.txtProductPrice = new DevExpress.XtraEditors.TextEdit();
            this.txtSelectedProduct = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
            this.txtSelectedCustomer = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl5 = new DevExpress.XtraEditors.GroupControl();
            this.gridCart = new System.Windows.Forms.DataGridView();
            this.groupControl6 = new DevExpress.XtraEditors.GroupControl();
            this.dtSalesDate = new System.Windows.Forms.DateTimePicker();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.btnProcessSale = new DevExpress.XtraEditors.SimpleButton();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.txtRemainingAmount = new DevExpress.XtraEditors.TextEdit();
            this.txtPaidAmount = new DevExpress.XtraEditors.TextEdit();
            this.txtTotalAmount = new DevExpress.XtraEditors.TextEdit();
            this.rbNoPayment = new System.Windows.Forms.RadioButton();
            this.rbPartialPayment = new System.Windows.Forms.RadioButton();
            this.rbFullPayment = new System.Windows.Forms.RadioButton();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridProducts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCustomers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAvailableStock.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductPrice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSelectedProduct.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).BeginInit();
            this.groupControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSelectedCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).BeginInit();
            this.groupControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl6)).BeginInit();
            this.groupControl6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemainingAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPaidAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalAmount.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.gridProducts);
            this.groupControl1.Location = new System.Drawing.Point(12, 12);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(400, 300);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Products";
            // 
            // gridProducts
            // 
            this.gridProducts.AllowUserToAddRows = false;
            this.gridProducts.AllowUserToDeleteRows = false;
            this.gridProducts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridProducts.Location = new System.Drawing.Point(2, 23);
            this.gridProducts.MultiSelect = false;
            this.gridProducts.Name = "gridProducts";
            this.gridProducts.ReadOnly = true;
            this.gridProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridProducts.Size = new System.Drawing.Size(396, 275);
            this.gridProducts.TabIndex = 0;
            this.gridProducts.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridProducts_RowEnter);
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.gridCustomers);
            this.groupControl2.Location = new System.Drawing.Point(418, 12);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(400, 300);
            this.groupControl2.TabIndex = 1;
            this.groupControl2.Text = "Customers";
            // 
            // gridCustomers
            // 
            this.gridCustomers.AllowUserToAddRows = false;
            this.gridCustomers.AllowUserToDeleteRows = false;
            this.gridCustomers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridCustomers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridCustomers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCustomers.Location = new System.Drawing.Point(2, 23);
            this.gridCustomers.MultiSelect = false;
            this.gridCustomers.Name = "gridCustomers";
            this.gridCustomers.ReadOnly = true;
            this.gridCustomers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridCustomers.Size = new System.Drawing.Size(396, 275);
            this.gridCustomers.TabIndex = 0;
            this.gridCustomers.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridCustomers_RowEnter);
            // 
            // groupControl3
            // 
            this.groupControl3.Controls.Add(this.btnAddToCart);
            this.groupControl3.Controls.Add(this.numQuantity);
            this.groupControl3.Controls.Add(this.txtAvailableStock);
            this.groupControl3.Controls.Add(this.txtProductPrice);
            this.groupControl3.Controls.Add(this.txtSelectedProduct);
            this.groupControl3.Controls.Add(this.labelControl5);
            this.groupControl3.Controls.Add(this.labelControl4);
            this.groupControl3.Controls.Add(this.labelControl3);
            this.groupControl3.Controls.Add(this.labelControl2);
            this.groupControl3.Location = new System.Drawing.Point(12, 318);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(400, 150);
            this.groupControl3.TabIndex = 2;
            this.groupControl3.Text = "Product Selection";
            // 
            // btnAddToCart
            // 
            this.btnAddToCart.Location = new System.Drawing.Point(300, 110);
            this.btnAddToCart.Name = "btnAddToCart";
            this.btnAddToCart.Size = new System.Drawing.Size(90, 30);
            this.btnAddToCart.TabIndex = 8;
            this.btnAddToCart.Text = "Add to Cart";
            this.btnAddToCart.Click += new System.EventHandler(this.btnAddToCart_Click);
            // 
            // numQuantity
            // 
            this.numQuantity.Location = new System.Drawing.Point(120, 115);
            this.numQuantity.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numQuantity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numQuantity.Name = "numQuantity";
            this.numQuantity.Size = new System.Drawing.Size(120, 21);
            this.numQuantity.TabIndex = 7;
            this.numQuantity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // txtAvailableStock
            // 
            this.txtAvailableStock.Location = new System.Drawing.Point(120, 85);
            this.txtAvailableStock.Name = "txtAvailableStock";
            this.txtAvailableStock.Properties.ReadOnly = true;
            this.txtAvailableStock.Size = new System.Drawing.Size(270, 20);
            this.txtAvailableStock.TabIndex = 6;
            // 
            // txtProductPrice
            // 
            this.txtProductPrice.Location = new System.Drawing.Point(120, 55);
            this.txtProductPrice.Name = "txtProductPrice";
            this.txtProductPrice.Properties.ReadOnly = true;
            this.txtProductPrice.Size = new System.Drawing.Size(270, 20);
            this.txtProductPrice.TabIndex = 5;
            // 
            // txtSelectedProduct
            // 
            this.txtSelectedProduct.Location = new System.Drawing.Point(120, 25);
            this.txtSelectedProduct.Name = "txtSelectedProduct";
            this.txtSelectedProduct.Properties.ReadOnly = true;
            this.txtSelectedProduct.Size = new System.Drawing.Size(270, 20);
            this.txtSelectedProduct.TabIndex = 4;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(15, 118);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(46, 13);
            this.labelControl5.TabIndex = 3;
            this.labelControl5.Text = "Quantity:";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(15, 88);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(82, 13);
            this.labelControl4.TabIndex = 2;
            this.labelControl4.Text = "Available Stock:";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(15, 58);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(28, 13);
            this.labelControl3.TabIndex = 1;
            this.labelControl3.Text = "Price:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(15, 28);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(43, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Product:";
            // 
            // groupControl4
            // 
            this.groupControl4.Controls.Add(this.txtSelectedCustomer);
            this.groupControl4.Controls.Add(this.labelControl1);
            this.groupControl4.Location = new System.Drawing.Point(418, 318);
            this.groupControl4.Name = "groupControl4";
            this.groupControl4.Size = new System.Drawing.Size(400, 60);
            this.groupControl4.TabIndex = 3;
            this.groupControl4.Text = "Customer Selection";
            // 
            // txtSelectedCustomer
            // 
            this.txtSelectedCustomer.Location = new System.Drawing.Point(120, 25);
            this.txtSelectedCustomer.Name = "txtSelectedCustomer";
            this.txtSelectedCustomer.Properties.ReadOnly = true;
            this.txtSelectedCustomer.Size = new System.Drawing.Size(270, 20);
            this.txtSelectedCustomer.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(15, 28);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(52, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Customer:";
            // 
            // groupControl5
            // 
            this.groupControl5.Controls.Add(this.gridCart);
            this.groupControl5.Location = new System.Drawing.Point(12, 474);
            this.groupControl5.Name = "groupControl5";
            this.groupControl5.Size = new System.Drawing.Size(806, 200);
            this.groupControl5.TabIndex = 4;
            this.groupControl5.Text = "Shopping Cart";
            // 
            // gridCart
            // 
            this.gridCart.AllowUserToAddRows = false;
            this.gridCart.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridCart.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridCart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCart.Location = new System.Drawing.Point(2, 23);
            this.gridCart.MultiSelect = false;
            this.gridCart.Name = "gridCart";
            this.gridCart.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridCart.Size = new System.Drawing.Size(802, 175);
            this.gridCart.TabIndex = 0;
            this.gridCart.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridCart_CellContentClick);
            // 
            // groupControl6
            // 
            this.groupControl6.Controls.Add(this.dtSalesDate);
            this.groupControl6.Controls.Add(this.labelControl11);
            this.groupControl6.Controls.Add(this.btnProcessSale);
            this.groupControl6.Controls.Add(this.btnClear);
            this.groupControl6.Controls.Add(this.btnClose);
            this.groupControl6.Controls.Add(this.txtRemainingAmount);
            this.groupControl6.Controls.Add(this.txtPaidAmount);
            this.groupControl6.Controls.Add(this.txtTotalAmount);
            this.groupControl6.Controls.Add(this.rbNoPayment);
            this.groupControl6.Controls.Add(this.rbPartialPayment);
            this.groupControl6.Controls.Add(this.rbFullPayment);
            this.groupControl6.Controls.Add(this.labelControl10);
            this.groupControl6.Controls.Add(this.labelControl9);
            this.groupControl6.Controls.Add(this.labelControl8);
            this.groupControl6.Controls.Add(this.labelControl7);
            this.groupControl6.Controls.Add(this.labelControl6);
            this.groupControl6.Location = new System.Drawing.Point(418, 384);
            this.groupControl6.Name = "groupControl6";
            this.groupControl6.Size = new System.Drawing.Size(400, 290);
            this.groupControl6.TabIndex = 5;
            this.groupControl6.Text = "Payment Options";
            // 
            // dtSalesDate
            // 
            this.dtSalesDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtSalesDate.Location = new System.Drawing.Point(120, 25);
            this.dtSalesDate.Name = "dtSalesDate";
            this.dtSalesDate.Size = new System.Drawing.Size(270, 21);
            this.dtSalesDate.TabIndex = 15;
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(15, 28);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(58, 13);
            this.labelControl11.TabIndex = 14;
            this.labelControl11.Text = "Sales Date:";
            // 
            // btnProcessSale
            // 
            this.btnProcessSale.Location = new System.Drawing.Point(15, 250);
            this.btnProcessSale.Name = "btnProcessSale";
            this.btnProcessSale.Size = new System.Drawing.Size(100, 30);
            this.btnProcessSale.TabIndex = 13;
            this.btnProcessSale.Text = "Process Sale";
            this.btnProcessSale.Click += new System.EventHandler(this.btnProcessSale_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(150, 250);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 30);
            this.btnClear.TabIndex = 12;
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(290, 250);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 30);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtRemainingAmount
            // 
            this.txtRemainingAmount.Location = new System.Drawing.Point(120, 215);
            this.txtRemainingAmount.Name = "txtRemainingAmount";
            this.txtRemainingAmount.Properties.ReadOnly = true;
            this.txtRemainingAmount.Size = new System.Drawing.Size(270, 20);
            this.txtRemainingAmount.TabIndex = 10;
            // 
            // txtPaidAmount
            // 
            this.txtPaidAmount.Location = new System.Drawing.Point(120, 185);
            this.txtPaidAmount.Name = "txtPaidAmount";
            this.txtPaidAmount.Size = new System.Drawing.Size(270, 20);
            this.txtPaidAmount.TabIndex = 9;
            this.txtPaidAmount.TextChanged += new System.EventHandler(this.txtPaidAmount_TextChanged);
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.Location = new System.Drawing.Point(120, 155);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.Properties.ReadOnly = true;
            this.txtTotalAmount.Size = new System.Drawing.Size(270, 20);
            this.txtTotalAmount.TabIndex = 8;
            // 
            // rbNoPayment
            // 
            this.rbNoPayment.AutoSize = true;
            this.rbNoPayment.Location = new System.Drawing.Point(15, 125);
            this.rbNoPayment.Name = "rbNoPayment";
            this.rbNoPayment.Size = new System.Drawing.Size(108, 17);
            this.rbNoPayment.TabIndex = 7;
            this.rbNoPayment.Text = "No Payment Now";
            this.rbNoPayment.UseVisualStyleBackColor = true;
            this.rbNoPayment.CheckedChanged += new System.EventHandler(this.rbNoPayment_CheckedChanged);
            // 
            // rbPartialPayment
            // 
            this.rbPartialPayment.AutoSize = true;
            this.rbPartialPayment.Location = new System.Drawing.Point(15, 95);
            this.rbPartialPayment.Name = "rbPartialPayment";
            this.rbPartialPayment.Size = new System.Drawing.Size(99, 17);
            this.rbPartialPayment.TabIndex = 6;
            this.rbPartialPayment.Text = "Partial Payment";
            this.rbPartialPayment.UseVisualStyleBackColor = true;
            this.rbPartialPayment.CheckedChanged += new System.EventHandler(this.rbPartialPayment_CheckedChanged);
            // 
            // rbFullPayment
            // 
            this.rbFullPayment.AutoSize = true;
            this.rbFullPayment.Checked = true;
            this.rbFullPayment.Location = new System.Drawing.Point(15, 65);
            this.rbFullPayment.Name = "rbFullPayment";
            this.rbFullPayment.Size = new System.Drawing.Size(87, 17);
            this.rbFullPayment.TabIndex = 5;
            this.rbFullPayment.TabStop = true;
            this.rbFullPayment.Text = "Full Payment";
            this.rbFullPayment.UseVisualStyleBackColor = true;
            this.rbFullPayment.CheckedChanged += new System.EventHandler(this.rbFullPayment_CheckedChanged);
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(15, 218);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(56, 13);
            this.labelControl10.TabIndex = 4;
            this.labelControl10.Text = "Remaining:";
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(15, 188);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(28, 13);
            this.labelControl9.TabIndex = 3;
            this.labelControl9.Text = "Paid:";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(15, 158);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(30, 13);
            this.labelControl8.TabIndex = 2;
            this.labelControl8.Text = "Total:";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(15, 48);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(86, 13);
            this.labelControl7.TabIndex = 1;
            this.labelControl7.Text = "Payment Options:";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(15, 48);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(0, 13);
            this.labelControl6.TabIndex = 0;
            // 
            // FrmPartialPaymentSales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 686);
            this.Controls.Add(this.groupControl6);
            this.Controls.Add(this.groupControl5);
            this.Controls.Add(this.groupControl4);
            this.Controls.Add(this.groupControl3);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPartialPaymentSales";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Partial Payment Sales System";
            this.Load += new System.EventHandler(this.FrmPartialPaymentSales_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridProducts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridCustomers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            this.groupControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAvailableStock.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductPrice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSelectedProduct.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).EndInit();
            this.groupControl4.ResumeLayout(false);
            this.groupControl4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSelectedCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).EndInit();
            this.groupControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridCart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl6)).EndInit();
            this.groupControl6.ResumeLayout(false);
            this.groupControl6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemainingAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPaidAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalAmount.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.DataGridView gridProducts;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private System.Windows.Forms.DataGridView gridCustomers;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.SimpleButton btnAddToCart;
        private System.Windows.Forms.NumericUpDown numQuantity;
        private DevExpress.XtraEditors.TextEdit txtAvailableStock;
        private DevExpress.XtraEditors.TextEdit txtProductPrice;
        private DevExpress.XtraEditors.TextEdit txtSelectedProduct;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.GroupControl groupControl4;
        private DevExpress.XtraEditors.TextEdit txtSelectedCustomer;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.GroupControl groupControl5;
        private System.Windows.Forms.DataGridView gridCart;
        private DevExpress.XtraEditors.GroupControl groupControl6;
        private System.Windows.Forms.DateTimePicker dtSalesDate;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.SimpleButton btnProcessSale;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.TextEdit txtRemainingAmount;
        private DevExpress.XtraEditors.TextEdit txtPaidAmount;
        private DevExpress.XtraEditors.TextEdit txtTotalAmount;
        private System.Windows.Forms.RadioButton rbNoPayment;
        private System.Windows.Forms.RadioButton rbPartialPayment;
        private System.Windows.Forms.RadioButton rbFullPayment;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl6;
    }
}
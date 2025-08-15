namespace Erp_V1
{
    partial class FrmSalesExport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Control declarations.
        private DevExpress.XtraEditors.GroupControl grpFilters;
        private DevExpress.XtraEditors.TextEdit txtCustomerName;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.Label lblSalesRange;
        private DevExpress.XtraEditors.TextEdit txtMinSales;
        private System.Windows.Forms.Label lblTo;
        private DevExpress.XtraEditors.TextEdit txtMaxSales;
        private System.Windows.Forms.Label lblDateRange;
        private System.Windows.Forms.CheckBox chkDateRange;
        private System.Windows.Forms.DateTimePicker dpStart;
        private System.Windows.Forms.DateTimePicker dpEnd;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnExport;
        private System.Windows.Forms.DataGridView dgvSales;
        private System.Windows.Forms.Panel pnlBottom;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Initialize the form controls.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpFilters = new DevExpress.XtraEditors.GroupControl();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.txtCustomerName = new DevExpress.XtraEditors.TextEdit();
            this.lblSalesRange = new System.Windows.Forms.Label();
            this.txtMinSales = new DevExpress.XtraEditors.TextEdit();
            this.lblTo = new System.Windows.Forms.Label();
            this.txtMaxSales = new DevExpress.XtraEditors.TextEdit();
            this.lblDateRange = new System.Windows.Forms.Label();
            this.chkDateRange = new System.Windows.Forms.CheckBox();
            this.dpStart = new System.Windows.Forms.DateTimePicker();
            this.dpEnd = new System.Windows.Forms.DateTimePicker();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnExport = new DevExpress.XtraEditors.SimpleButton();
            this.dgvSales = new System.Windows.Forms.DataGridView();
            this.pnlBottom = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.grpFilters)).BeginInit();
            this.grpFilters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMinSales.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaxSales.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSales)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpFilters
            // 
            this.grpFilters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpFilters.Appearance.BackColor = System.Drawing.Color.White;
            this.grpFilters.Appearance.Options.UseBackColor = true;
            this.grpFilters.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.grpFilters.CaptionLocation = DevExpress.Utils.Locations.Top;
            this.grpFilters.Controls.Add(this.lblCustomer);
            this.grpFilters.Controls.Add(this.txtCustomerName);
            this.grpFilters.Controls.Add(this.lblSalesRange);
            this.grpFilters.Controls.Add(this.txtMinSales);
            this.grpFilters.Controls.Add(this.lblTo);
            this.grpFilters.Controls.Add(this.txtMaxSales);
            this.grpFilters.Controls.Add(this.lblDateRange);
            this.grpFilters.Controls.Add(this.chkDateRange);
            this.grpFilters.Controls.Add(this.dpStart);
            this.grpFilters.Controls.Add(this.dpEnd);
            this.grpFilters.Location = new System.Drawing.Point(12, 12);
            this.grpFilters.Name = "grpFilters";
            this.grpFilters.Size = new System.Drawing.Size(956, 120);
            this.grpFilters.TabIndex = 0;
            this.grpFilters.Text = "Filter Criteria";
            // 
            // lblCustomer
            // 
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCustomer.Location = new System.Drawing.Point(20, 35);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(139, 23);
            this.lblCustomer.TabIndex = 0;
            this.lblCustomer.Text = "Customer Name:";
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Location = new System.Drawing.Point(164, 33);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCustomerName.Properties.Appearance.Options.UseFont = true;
            this.txtCustomerName.Size = new System.Drawing.Size(200, 30);
            this.txtCustomerName.TabIndex = 1;
            // 
            // lblSalesRange
            // 
            this.lblSalesRange.AutoSize = true;
            this.lblSalesRange.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSalesRange.Location = new System.Drawing.Point(20, 75);
            this.lblSalesRange.Name = "lblSalesRange";
            this.lblSalesRange.Size = new System.Drawing.Size(105, 23);
            this.lblSalesRange.TabIndex = 2;
            this.lblSalesRange.Text = "Sales Range:";
            // 
            // txtMinSales
            // 
            this.txtMinSales.Location = new System.Drawing.Point(140, 72);
            this.txtMinSales.Name = "txtMinSales";
            this.txtMinSales.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMinSales.Properties.Appearance.Options.UseFont = true;
            this.txtMinSales.Size = new System.Drawing.Size(80, 30);
            this.txtMinSales.TabIndex = 3;
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTo.Location = new System.Drawing.Point(230, 75);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(26, 23);
            this.lblTo.TabIndex = 4;
            this.lblTo.Text = "to";
            // 
            // txtMaxSales
            // 
            this.txtMaxSales.Location = new System.Drawing.Point(260, 72);
            this.txtMaxSales.Name = "txtMaxSales";
            this.txtMaxSales.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMaxSales.Properties.Appearance.Options.UseFont = true;
            this.txtMaxSales.Size = new System.Drawing.Size(80, 30);
            this.txtMaxSales.TabIndex = 5;
            // 
            // lblDateRange
            // 
            this.lblDateRange.AutoSize = true;
            this.lblDateRange.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDateRange.Location = new System.Drawing.Point(466, 40);
            this.lblDateRange.Name = "lblDateRange";
            this.lblDateRange.Size = new System.Drawing.Size(103, 23);
            this.lblDateRange.TabIndex = 6;
            this.lblDateRange.Text = "Date Range:";
            // 
            // chkDateRange
            // 
            this.chkDateRange.AutoSize = true;
            this.chkDateRange.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkDateRange.Location = new System.Drawing.Point(470, 75);
            this.chkDateRange.Name = "chkDateRange";
            this.chkDateRange.Size = new System.Drawing.Size(18, 17);
            this.chkDateRange.TabIndex = 7;
            this.chkDateRange.UseVisualStyleBackColor = true;
            // 
            // dpStart
            // 
            this.dpStart.CustomFormat = "dd/MM/yyyy";
            this.dpStart.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dpStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpStart.Location = new System.Drawing.Point(500, 70);
            this.dpStart.Name = "dpStart";
            this.dpStart.Size = new System.Drawing.Size(120, 30);
            this.dpStart.TabIndex = 8;
            // 
            // dpEnd
            // 
            this.dpEnd.CustomFormat = "dd/MM/yyyy";
            this.dpEnd.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpEnd.Location = new System.Drawing.Point(630, 70);
            this.dpEnd.Name = "dpEnd";
            this.dpEnd.Size = new System.Drawing.Size(120, 30);
            this.dpEnd.TabIndex = 9;
            // 
            // btnSearch
            // 
            this.btnSearch.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSearch.Appearance.Options.UseFont = true;
            this.btnSearch.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btnSearch.Location = new System.Drawing.Point(20, 10);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(120, 30);
            this.btnSearch.TabIndex = 11;
            this.btnSearch.Text = "Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClear
            // 
            this.btnClear.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnClear.Appearance.Options.UseFont = true;
            this.btnClear.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btnClear.Location = new System.Drawing.Point(150, 10);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(120, 30);
            this.btnClear.TabIndex = 12;
            this.btnClear.Text = "Clear Filters";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnExport
            // 
            this.btnExport.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnExport.Appearance.Options.UseFont = true;
            this.btnExport.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btnExport.Location = new System.Drawing.Point(280, 10);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(120, 30);
            this.btnExport.TabIndex = 13;
            this.btnExport.Text = "Export to Excel";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // dgvSales
            // 
            this.dgvSales.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSales.BackgroundColor = System.Drawing.Color.White;
            this.dgvSales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSales.Location = new System.Drawing.Point(12, 138);
            this.dgvSales.Name = "dgvSales";
            this.dgvSales.RowHeadersWidth = 51;
            this.dgvSales.RowTemplate.Height = 25;
            this.dgvSales.Size = new System.Drawing.Size(956, 390);
            this.dgvSales.TabIndex = 14;
            this.dgvSales.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSales_RowEnter);
            // 
            // pnlBottom
            // 
            this.pnlBottom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlBottom.BackColor = System.Drawing.Color.White;
            this.pnlBottom.Controls.Add(this.btnSearch);
            this.pnlBottom.Controls.Add(this.btnClear);
            this.pnlBottom.Controls.Add(this.btnExport);
            this.pnlBottom.Location = new System.Drawing.Point(12, 540);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(956, 48);
            this.pnlBottom.TabIndex = 10;
            // 
            // FrmSalesExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 600);
            this.Controls.Add(this.dgvSales);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.grpFilters);
            this.Name = "FrmSalesExport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sales Export";
            this.Load += new System.EventHandler(this.FrmSalesExport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grpFilters)).EndInit();
            this.grpFilters.ResumeLayout(false);
            this.grpFilters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMinSales.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaxSales.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSales)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
    }
}

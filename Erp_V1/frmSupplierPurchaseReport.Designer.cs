namespace Erp_V1
{
    partial class frmSupplierPurchaseReport
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ComboBox cboSupplierNames;
        private System.Windows.Forms.Button btnLoadPurchases;
        private System.Windows.Forms.DataGridView dgvPurchaseDetails;
        private System.Windows.Forms.TextBox txtTotalPayment;
        private System.Windows.Forms.Label lblTotalPayment;
        private DevExpress.XtraCharts.ChartControl chartControl1;

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

        private void InitializeComponent()
        {
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            this.cboSupplierNames = new System.Windows.Forms.ComboBox();
            this.btnLoadPurchases = new System.Windows.Forms.Button();
            this.dgvPurchaseDetails = new System.Windows.Forms.DataGridView();
            this.txtTotalPayment = new System.Windows.Forms.TextBox();
            this.lblTotalPayment = new System.Windows.Forms.Label();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchaseDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            this.SuspendLayout();
            // 
            // cboSupplierNames
            // 
            this.cboSupplierNames.FormattingEnabled = true;
            this.cboSupplierNames.Location = new System.Drawing.Point(30, 30);
            this.cboSupplierNames.Name = "cboSupplierNames";
            this.cboSupplierNames.Size = new System.Drawing.Size(200, 24);
            this.cboSupplierNames.TabIndex = 1;
            // 
            // btnLoadPurchases
            // 
            this.btnLoadPurchases.Location = new System.Drawing.Point(250, 30);
            this.btnLoadPurchases.Name = "btnLoadPurchases";
            this.btnLoadPurchases.Size = new System.Drawing.Size(130, 30);
            this.btnLoadPurchases.TabIndex = 2;
            this.btnLoadPurchases.Text = "Load Purchases";
            this.btnLoadPurchases.UseVisualStyleBackColor = true;
            this.btnLoadPurchases.Click += new System.EventHandler(this.btnLoadPurchases_Click);
            // 
            // dgvPurchaseDetails
            // 
            this.dgvPurchaseDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPurchaseDetails.Location = new System.Drawing.Point(30, 80);
            this.dgvPurchaseDetails.Name = "dgvPurchaseDetails";
            this.dgvPurchaseDetails.RowHeadersWidth = 51;
            this.dgvPurchaseDetails.RowTemplate.Height = 24;
            this.dgvPurchaseDetails.Size = new System.Drawing.Size(600, 300);
            this.dgvPurchaseDetails.TabIndex = 3;
            // 
            // lblTotalPayment
            // 
            this.lblTotalPayment.AutoSize = true;
            this.lblTotalPayment.Location = new System.Drawing.Point(30, 400);
            this.lblTotalPayment.Name = "lblTotalPayment";
            this.lblTotalPayment.Size = new System.Drawing.Size(92, 16);
            this.lblTotalPayment.TabIndex = 4;
            this.lblTotalPayment.Text = "Total Payment:";
            // 
            // txtTotalPayment
            // 
            this.txtTotalPayment.Location = new System.Drawing.Point(140, 397);
            this.txtTotalPayment.Name = "txtTotalPayment";
            this.txtTotalPayment.ReadOnly = true;
            this.txtTotalPayment.Size = new System.Drawing.Size(150, 22);
            this.txtTotalPayment.TabIndex = 5;
            // 
            // chartControl1
            // 
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            this.chartControl1.Diagram = xyDiagram1;
            this.chartControl1.Location = new System.Drawing.Point(30, 450);
            this.chartControl1.Name = "chartControl1";
            series1.Name = "PaymentSeries";
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
            series1};
            this.chartControl1.Size = new System.Drawing.Size(600, 300);
            this.chartControl1.TabIndex = 6;
            // 
            // frmSupplierPurchaseReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 800);
            this.Controls.Add(this.chartControl1);
            this.Controls.Add(this.txtTotalPayment);
            this.Controls.Add(this.lblTotalPayment);
            this.Controls.Add(this.dgvPurchaseDetails);
            this.Controls.Add(this.btnLoadPurchases);
            this.Controls.Add(this.cboSupplierNames);
            this.Name = "frmSupplierPurchaseReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Supplier Purchase Report";
            this.Load += new System.EventHandler(this.frmSupplierPurchaseReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchaseDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}

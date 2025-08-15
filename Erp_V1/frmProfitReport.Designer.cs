namespace Erp_V1
{
    partial class frmProfitReport
    {
        private System.Windows.Forms.ComboBox cboProductNames;
        private System.Windows.Forms.Button btnLoadSales;
        private System.Windows.Forms.DataGridView dgvSalesDetails;
        private System.Windows.Forms.TextBox txtTotalProfit;
        private System.Windows.Forms.Label lblTotalProfit;
        private DevExpress.XtraCharts.ChartControl chartControl1;

        private void InitializeComponent()
        {
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();

            this.cboProductNames = new System.Windows.Forms.ComboBox();
            this.btnLoadSales = new System.Windows.Forms.Button();
            this.dgvSalesDetails = new System.Windows.Forms.DataGridView();
            this.txtTotalProfit = new System.Windows.Forms.TextBox();
            this.lblTotalProfit = new System.Windows.Forms.Label();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();

            ((System.ComponentModel.ISupportInitialize)(this.dgvSalesDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            this.SuspendLayout();

            // 
            // cboProductNames
            // 
            this.cboProductNames.FormattingEnabled = true;
            this.cboProductNames.Location = new System.Drawing.Point(30, 30);
            this.cboProductNames.Name = "cboProductNames";
            this.cboProductNames.Size = new System.Drawing.Size(200, 24);
            this.cboProductNames.TabIndex = 1;

            // 
            // btnLoadSales
            // 
            this.btnLoadSales.Location = new System.Drawing.Point(250, 30);
            this.btnLoadSales.Name = "btnLoadSales";
            this.btnLoadSales.Size = new System.Drawing.Size(100, 30);
            this.btnLoadSales.TabIndex = 2;
            this.btnLoadSales.Text = "Load Sales";
            this.btnLoadSales.UseVisualStyleBackColor = true;
            this.btnLoadSales.Click += new System.EventHandler(this.btnLoadSales_Click);

            // 
            // dgvSalesDetails
            // 
            this.dgvSalesDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSalesDetails.Location = new System.Drawing.Point(30, 80);
            this.dgvSalesDetails.Name = "dgvSalesDetails";
            this.dgvSalesDetails.RowHeadersWidth = 51;
            this.dgvSalesDetails.RowTemplate.Height = 24;
            this.dgvSalesDetails.Size = new System.Drawing.Size(600, 300);
            this.dgvSalesDetails.TabIndex = 3;

            // 
            // lblTotalProfit
            // 
            this.lblTotalProfit.AutoSize = true;
            this.lblTotalProfit.Location = new System.Drawing.Point(30, 400);
            this.lblTotalProfit.Name = "lblTotalProfit";
            this.lblTotalProfit.Size = new System.Drawing.Size(76, 16);
            this.lblTotalProfit.TabIndex = 4;
            this.lblTotalProfit.Text = "Total Profit:";

            // 
            // txtTotalProfit
            // 
            this.txtTotalProfit.Location = new System.Drawing.Point(120, 397);
            this.txtTotalProfit.Name = "txtTotalProfit";
            this.txtTotalProfit.Size = new System.Drawing.Size(150, 22);
            this.txtTotalProfit.TabIndex = 5;
            this.txtTotalProfit.ReadOnly = true;

            // 
            // chartControl1
            // 
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            this.chartControl1.Diagram = xyDiagram1;
            this.chartControl1.Location = new System.Drawing.Point(30, 450);
            this.chartControl1.Name = "chartControl1";
            series1.Name = "ProfitSeries";
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[] { series1 };
            this.chartControl1.Size = new System.Drawing.Size(600, 300);
            this.chartControl1.TabIndex = 6;

            // 
            // frmProfitReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 800);
            this.Controls.Add(this.chartControl1);
            this.Controls.Add(this.txtTotalProfit);
            this.Controls.Add(this.lblTotalProfit);
            this.Controls.Add(this.dgvSalesDetails);
            this.Controls.Add(this.btnLoadSales);
            this.Controls.Add(this.cboProductNames);
            this.Name = "frmProfitReport";
            this.Text = "Profit Report";
            this.Load += new System.EventHandler(this.frmProfitReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalesDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

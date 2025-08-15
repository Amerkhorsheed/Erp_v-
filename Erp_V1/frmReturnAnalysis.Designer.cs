// frmReturnAnalysis.Designer.cs
namespace Erp_V1
{
    partial class frmReturnAnalysis
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Designer-declared UI Controls
        private DevExpress.XtraCharts.ChartControl chartControl1;
        private System.Windows.Forms.DataGridView dgvAnalysis;
        private System.Windows.Forms.Button btnAnalyze;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.NumericUpDown numClusterCount;
        private System.Windows.Forms.Label lblClusterCount;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblTitle;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Initializes the components for the Return Analysis form.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            this.dgvAnalysis = new System.Windows.Forms.DataGridView();
            this.btnAnalyze = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.numClusterCount = new System.Windows.Forms.NumericUpDown();
            this.lblClusterCount = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAnalysis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numClusterCount)).BeginInit();
            this.SuspendLayout();
            // 
            // chartControl1
            // 
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            this.chartControl1.Diagram = xyDiagram1;
            this.chartControl1.Location = new System.Drawing.Point(20, 70);
            this.chartControl1.Name = "chartControl1";
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[] { series1 };
            this.chartControl1.Size = new System.Drawing.Size(540, 320);
            this.chartControl1.TabIndex = 0;
            // 
            // dgvAnalysis
            // 
            this.dgvAnalysis.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvAnalysis.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAnalysis.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAnalysis.Location = new System.Drawing.Point(20, 410);
            this.dgvAnalysis.Name = "dgvAnalysis";
            this.dgvAnalysis.ReadOnly = true;
            this.dgvAnalysis.RowHeadersVisible = false;
            this.dgvAnalysis.RowTemplate.Height = 24;
            this.dgvAnalysis.Size = new System.Drawing.Size(540, 220);
            this.dgvAnalysis.TabIndex = 1;
            // 
            // btnAnalyze
            // 
            this.btnAnalyze.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnAnalyze.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAnalyze.ForeColor = System.Drawing.Color.White;
            this.btnAnalyze.Location = new System.Drawing.Point(580, 80);
            this.btnAnalyze.Name = "btnAnalyze";
            this.btnAnalyze.Size = new System.Drawing.Size(150, 45);
            this.btnAnalyze.TabIndex = 2;
            this.btnAnalyze.Text = "Analyze Returns";
            this.btnAnalyze.UseVisualStyleBackColor = false;
            this.btnAnalyze.Click += new System.EventHandler(this.btnAnalyze_Click);
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnExport.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnExport.ForeColor = System.Drawing.Color.White;
            this.btnExport.Location = new System.Drawing.Point(580, 140);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(150, 45);
            this.btnExport.TabIndex = 3;
            this.btnExport.Text = "Export Results";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // numClusterCount
            // 
            this.numClusterCount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numClusterCount.Location = new System.Drawing.Point(580, 230);
            this.numClusterCount.Name = "numClusterCount";
            this.numClusterCount.Size = new System.Drawing.Size(80, 30);
            this.numClusterCount.TabIndex = 4;
            this.numClusterCount.ValueChanged += new System.EventHandler(this.numClusterCount_ValueChanged);
            // 
            // lblClusterCount
            // 
            this.lblClusterCount.AutoSize = true;
            this.lblClusterCount.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblClusterCount.ForeColor = System.Drawing.Color.DimGray;
            this.lblClusterCount.Location = new System.Drawing.Point(580, 200);
            this.lblClusterCount.Name = "lblClusterCount";
            this.lblClusterCount.Size = new System.Drawing.Size(125, 23);
            this.lblClusterCount.TabIndex = 5;
            this.lblClusterCount.Text = "Cluster Count:";
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.LightGray;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblStatus.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblStatus.Location = new System.Drawing.Point(20, 650);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(540, 35);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "Status: Ready";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(710, 50);
            this.lblTitle.TabIndex = 7;
            this.lblTitle.Text = "Return Reasons Analysis";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmReturnAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 700);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblClusterCount);
            this.Controls.Add(this.numClusterCount);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnAnalyze);
            this.Controls.Add(this.dgvAnalysis);
            this.Controls.Add(this.chartControl1);
            this.Name = "frmReturnAnalysis";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Return Reasons Analysis";
            this.Load += new System.EventHandler(this.frmReturnAnalysis_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAnalysis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numClusterCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}

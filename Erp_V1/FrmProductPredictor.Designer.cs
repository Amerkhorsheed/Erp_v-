//namespace Erp_V1
//{
//    partial class FrmProductPredictor
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

//        #region Windows Form Designer code

//        private void InitializeComponent()
//        {
//            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
//            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
//            this.grpControls = new System.Windows.Forms.GroupBox();
//            this.label2 = new System.Windows.Forms.Label();
//            this.label1 = new System.Windows.Forms.Label();
//            this.btnPredict = new System.Windows.Forms.Button();
//            this.numHorizon = new System.Windows.Forms.NumericUpDown();
//            this.numConfidence = new System.Windows.Forms.NumericUpDown();
//            this.dgvPredictions = new System.Windows.Forms.DataGridView();
//            this.chartPredictions = new System.Windows.Forms.DataVisualization.Charting.Chart();
//            this.grpControls.SuspendLayout();
//            ((System.ComponentModel.ISupportInitialize)(this.numHorizon)).BeginInit();
//            ((System.ComponentModel.ISupportInitialize)(this.numConfidence)).BeginInit();
//            ((System.ComponentModel.ISupportInitialize)(this.dgvPredictions)).BeginInit();
//            ((System.ComponentModel.ISupportInitialize)(this.chartPredictions)).BeginInit();
//            this.SuspendLayout();
//            // 
//            // grpControls
//            // 
//            this.grpControls.Controls.Add(this.label2);
//            this.grpControls.Controls.Add(this.label1);
//            this.grpControls.Controls.Add(this.btnPredict);
//            this.grpControls.Controls.Add(this.numHorizon);
//            this.grpControls.Controls.Add(this.numConfidence);
//            this.grpControls.Dock = System.Windows.Forms.DockStyle.Top;
//            this.grpControls.Location = new System.Drawing.Point(0, 0);
//            this.grpControls.Name = "grpControls";
//            this.grpControls.Size = new System.Drawing.Size(984, 80);
//            this.grpControls.TabIndex = 0;
//            this.grpControls.TabStop = false;
//            // 
//            // label2
//            // 
//            this.label2.Location = new System.Drawing.Point(273, 30);
//            this.label2.Name = "label2";
//            this.label2.Size = new System.Drawing.Size(100, 23);
//            this.label2.TabIndex = 0;
//            this.label2.Text = "Confidence Level (%):";
//            // 
//            // label1
//            // 
//            this.label1.Location = new System.Drawing.Point(20, 27);
//            this.label1.Name = "label1";
//            this.label1.Size = new System.Drawing.Size(100, 23);
//            this.label1.TabIndex = 1;
//            this.label1.Text = "Forecast Horizon (Days):";
//            // 
//            // btnPredict
//            // 
//            this.btnPredict.Location = new System.Drawing.Point(561, 28);
//            this.btnPredict.Name = "btnPredict";
//            this.btnPredict.Size = new System.Drawing.Size(120, 30);
//            this.btnPredict.TabIndex = 2;
//            this.btnPredict.Text = "Generate Forecast";
//            this.btnPredict.Click += new System.EventHandler(this.btnPredict_Click);
//            // 
//            // numHorizon
//            // 
//            this.numHorizon.Location = new System.Drawing.Point(126, 28);
//            this.numHorizon.Maximum = new decimal(new int[] {
//            365,
//            0,
//            0,
//            0});
//            this.numHorizon.Minimum = new decimal(new int[] {
//            7,
//            0,
//            0,
//            0});
//            this.numHorizon.Name = "numHorizon";
//            this.numHorizon.Size = new System.Drawing.Size(120, 22);
//            this.numHorizon.TabIndex = 3;
//            this.numHorizon.Value = new decimal(new int[] {
//            30,
//            0,
//            0,
//            0});
//            // 
//            // numConfidence
//            // 
//            this.numConfidence.Location = new System.Drawing.Point(391, 30);
//            this.numConfidence.Maximum = new decimal(new int[] {
//            99,
//            0,
//            0,
//            0});
//            this.numConfidence.Minimum = new decimal(new int[] {
//            80,
//            0,
//            0,
//            0});
//            this.numConfidence.Name = "numConfidence";
//            this.numConfidence.Size = new System.Drawing.Size(120, 22);
//            this.numConfidence.TabIndex = 4;
//            this.numConfidence.Value = new decimal(new int[] {
//            95,
//            0,
//            0,
//            0});
//            // 
//            // dgvPredictions
//            // 
//            this.dgvPredictions.ColumnHeadersHeight = 29;
//            this.dgvPredictions.Dock = System.Windows.Forms.DockStyle.Left;
//            this.dgvPredictions.Location = new System.Drawing.Point(0, 80);
//            this.dgvPredictions.Name = "dgvPredictions";
//            this.dgvPredictions.RowHeadersWidth = 51;
//            this.dgvPredictions.Size = new System.Drawing.Size(600, 481);
//            this.dgvPredictions.TabIndex = 1;
//            this.dgvPredictions.DataSourceChanged += new System.EventHandler(this.dgvPredictions_DataSourceChanged);
//            // 
//            // chartPredictions
//            // 
//            chartArea1.Name = "ChartArea1";
//            this.chartPredictions.ChartAreas.Add(chartArea1);
//            this.chartPredictions.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.chartPredictions.Location = new System.Drawing.Point(600, 80);
//            this.chartPredictions.Name = "chartPredictions";
//            series1.ChartArea = "ChartArea1";
//            series1.Name = "Forecast";
//            this.chartPredictions.Series.Add(series1);
//            this.chartPredictions.Size = new System.Drawing.Size(384, 481);
//            this.chartPredictions.TabIndex = 0;
//            // 
//            // FrmProductPredictor
//            // 
//            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
//            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
//            this.ClientSize = new System.Drawing.Size(984, 561);
//            this.Controls.Add(this.chartPredictions);
//            this.Controls.Add(this.dgvPredictions);
//            this.Controls.Add(this.grpControls);
//            this.Name = "FrmProductPredictor";
//            this.Text = "Advanced Product Prediction System";
//            this.grpControls.ResumeLayout(false);
//            ((System.ComponentModel.ISupportInitialize)(this.numHorizon)).EndInit();
//            ((System.ComponentModel.ISupportInitialize)(this.numConfidence)).EndInit();
//            ((System.ComponentModel.ISupportInitialize)(this.dgvPredictions)).EndInit();
//            ((System.ComponentModel.ISupportInitialize)(this.chartPredictions)).EndInit();
//            this.ResumeLayout(false);

//        }

//        #endregion

//        private System.Windows.Forms.GroupBox grpControls;
//        private System.Windows.Forms.NumericUpDown numConfidence;
//        private System.Windows.Forms.NumericUpDown numHorizon;
//        private System.Windows.Forms.Button btnPredict;
//        private System.Windows.Forms.Label label1;
//        private System.Windows.Forms.Label label2;
//        private System.Windows.Forms.DataGridView dgvPredictions;
//        private System.Windows.Forms.DataVisualization.Charting.Chart chartPredictions;
//    }
//}
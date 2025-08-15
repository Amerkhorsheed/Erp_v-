namespace Erp_V1
{
    partial class FrmProductPredictorModern
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Control declarations
        private System.Windows.Forms.Panel pnlControls;
        private System.Windows.Forms.NumericUpDown numHorizon;
        private System.Windows.Forms.NumericUpDown numConfidence;
        private System.Windows.Forms.NumericUpDown numSeasonality;
        private System.Windows.Forms.NumericUpDown numTrendWindow;
        private System.Windows.Forms.Label lblHorizon;
        private System.Windows.Forms.Label lblConfidence;
        private System.Windows.Forms.Label lblSeasonality;
        private System.Windows.Forms.Label lblTrendWindow;
        private System.Windows.Forms.Button btnPredict;
        private System.Windows.Forms.DataGridView dgvPredictions;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPredictions;

        /// <summary>
        /// Clean up any resources.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Initializes the form components.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlControls = new System.Windows.Forms.Panel();
            this.lblHorizon = new System.Windows.Forms.Label();
            this.numHorizon = new System.Windows.Forms.NumericUpDown();
            this.lblConfidence = new System.Windows.Forms.Label();
            this.numConfidence = new System.Windows.Forms.NumericUpDown();
            this.lblSeasonality = new System.Windows.Forms.Label();
            this.numSeasonality = new System.Windows.Forms.NumericUpDown();
            this.lblTrendWindow = new System.Windows.Forms.Label();
            this.numTrendWindow = new System.Windows.Forms.NumericUpDown();
            this.btnPredict = new System.Windows.Forms.Button();
            this.dgvPredictions = new System.Windows.Forms.DataGridView();
            this.chartPredictions = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHorizon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numConfidence)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSeasonality)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTrendWindow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPredictions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartPredictions)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlControls
            // 
            this.pnlControls.BackColor = System.Drawing.Color.White;
            this.pnlControls.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlControls.Controls.Add(this.lblHorizon);
            this.pnlControls.Controls.Add(this.numHorizon);
            this.pnlControls.Controls.Add(this.lblConfidence);
            this.pnlControls.Controls.Add(this.numConfidence);
            this.pnlControls.Controls.Add(this.lblSeasonality);
            this.pnlControls.Controls.Add(this.numSeasonality);
            this.pnlControls.Controls.Add(this.lblTrendWindow);
            this.pnlControls.Controls.Add(this.numTrendWindow);
            this.pnlControls.Controls.Add(this.btnPredict);
            this.pnlControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlControls.Location = new System.Drawing.Point(0, 0);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(1000, 94);
            this.pnlControls.TabIndex = 0;
            // 
            // lblHorizon
            // 
            this.lblHorizon.AutoSize = true;
            this.lblHorizon.Location = new System.Drawing.Point(15, 16);
            this.lblHorizon.Name = "lblHorizon";
            this.lblHorizon.Size = new System.Drawing.Size(112, 16);
            this.lblHorizon.TabIndex = 0;
            this.lblHorizon.Text = "Forecast Horizon:";
            // 
            // numHorizon
            // 
            this.numHorizon.Location = new System.Drawing.Point(130, 14);
            this.numHorizon.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.numHorizon.Minimum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.numHorizon.Name = "numHorizon";
            this.numHorizon.Size = new System.Drawing.Size(70, 22);
            this.numHorizon.TabIndex = 1;
            this.numHorizon.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // lblConfidence
            // 
            this.lblConfidence.AutoSize = true;
            this.lblConfidence.Location = new System.Drawing.Point(220, 16);
            this.lblConfidence.Name = "lblConfidence";
            this.lblConfidence.Size = new System.Drawing.Size(101, 16);
            this.lblConfidence.TabIndex = 2;
            this.lblConfidence.Text = "Confidence (%):";
            // 
            // numConfidence
            // 
            this.numConfidence.Location = new System.Drawing.Point(326, 14);
            this.numConfidence.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numConfidence.Minimum = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.numConfidence.Name = "numConfidence";
            this.numConfidence.Size = new System.Drawing.Size(70, 22);
            this.numConfidence.TabIndex = 3;
            this.numConfidence.Value = new decimal(new int[] {
            95,
            0,
            0,
            0});
            // 
            // lblSeasonality
            // 
            this.lblSeasonality.AutoSize = true;
            this.lblSeasonality.Location = new System.Drawing.Point(420, 16);
            this.lblSeasonality.Name = "lblSeasonality";
            this.lblSeasonality.Size = new System.Drawing.Size(100, 16);
            this.lblSeasonality.TabIndex = 4;
            this.lblSeasonality.Text = "Seasonality (d):";
            // 
            // numSeasonality
            // 
            this.numSeasonality.Location = new System.Drawing.Point(526, 14);
            this.numSeasonality.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.numSeasonality.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numSeasonality.Name = "numSeasonality";
            this.numSeasonality.Size = new System.Drawing.Size(70, 22);
            this.numSeasonality.TabIndex = 5;
            this.numSeasonality.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // lblTrendWindow
            // 
            this.lblTrendWindow.AutoSize = true;
            this.lblTrendWindow.Location = new System.Drawing.Point(620, 16);
            this.lblTrendWindow.Name = "lblTrendWindow";
            this.lblTrendWindow.Size = new System.Drawing.Size(97, 16);
            this.lblTrendWindow.TabIndex = 6;
            this.lblTrendWindow.Text = "Trend Window:";
            // 
            // numTrendWindow
            // 
            this.numTrendWindow.Location = new System.Drawing.Point(722, 14);
            this.numTrendWindow.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.numTrendWindow.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numTrendWindow.Name = "numTrendWindow";
            this.numTrendWindow.Size = new System.Drawing.Size(70, 22);
            this.numTrendWindow.TabIndex = 7;
            this.numTrendWindow.Value = new decimal(new int[] {
            14,
            0,
            0,
            0});
            // 
            // btnPredict
            // 
            this.btnPredict.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPredict.Location = new System.Drawing.Point(840, 14);
            this.btnPredict.Name = "btnPredict";
            this.btnPredict.Size = new System.Drawing.Size(120, 28);
            this.btnPredict.TabIndex = 8;
            this.btnPredict.Text = "Predict Now";
            this.btnPredict.UseVisualStyleBackColor = true;
            // 
            // dgvPredictions
            // 
            this.dgvPredictions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPredictions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPredictions.Location = new System.Drawing.Point(0, 99);
            this.dgvPredictions.Name = "dgvPredictions";
            this.dgvPredictions.RowHeadersWidth = 51;
            this.dgvPredictions.Size = new System.Drawing.Size(1000, 282);
            this.dgvPredictions.TabIndex = 1;
            // 
            // chartPredictions
            // 
            this.chartPredictions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chartPredictions.Location = new System.Drawing.Point(0, 391);
            this.chartPredictions.Name = "chartPredictions";
            this.chartPredictions.Size = new System.Drawing.Size(1000, 282);
            this.chartPredictions.TabIndex = 2;
            this.chartPredictions.Text = "chart1";
            // 
            // FrmProductPredictorModern
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 678);
            this.Controls.Add(this.chartPredictions);
            this.Controls.Add(this.dgvPredictions);
            this.Controls.Add(this.pnlControls);
            this.Name = "FrmProductPredictorModern";
            this.Text = "Product Forecasting Dashboard";
            this.Load += new System.EventHandler(this.FrmProductPredictorModern_Load);
            this.pnlControls.ResumeLayout(false);
            this.pnlControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHorizon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numConfidence)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSeasonality)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTrendWindow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPredictions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartPredictions)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}

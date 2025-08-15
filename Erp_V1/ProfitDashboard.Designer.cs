namespace Erp_V1
{
    partial class ProfitDashboard
    {
        private System.ComponentModel.IContainer components = null;

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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.profitChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.netProfitLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.salesAmountLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.expensesAmountLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.profitChart)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // profitChart
            // 
            this.profitChart.BackColor = System.Drawing.Color.Transparent;
            chartArea5.Name = "ChartArea1";
            this.profitChart.ChartAreas.Add(chartArea5);
            legend5.Name = "Legend1";
            this.profitChart.Legends.Add(legend5);
            this.profitChart.Location = new System.Drawing.Point(12, 160);
            this.profitChart.Name = "profitChart";
            this.profitChart.Size = new System.Drawing.Size(760, 280);
            this.profitChart.TabIndex = 0;
            // 
            // netProfitLabel
            // 
            this.netProfitLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.netProfitLabel.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.netProfitLabel.Location = new System.Drawing.Point(167, 20);
            this.netProfitLabel.Name = "netProfitLabel";
            this.netProfitLabel.Size = new System.Drawing.Size(442, 40);
            this.netProfitLabel.TabIndex = 1;
            this.netProfitLabel.Text = "�0.00";
            this.netProfitLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.salesAmountLabel);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(12, 80);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(244, 60);
            this.panel1.TabIndex = 2;
            // 
            // salesAmountLabel
            // 
            this.salesAmountLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.salesAmountLabel.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.salesAmountLabel.Location = new System.Drawing.Point(10, 25);
            this.salesAmountLabel.Name = "salesAmountLabel";
            this.salesAmountLabel.Size = new System.Drawing.Size(231, 25);
            this.salesAmountLabel.TabIndex = 1;
            this.salesAmountLabel.Text = "�0.00";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(10, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Total Sales:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.expensesAmountLabel);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Location = new System.Drawing.Point(572, 80);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(229, 60);
            this.panel2.TabIndex = 3;
            // 
            // expensesAmountLabel
            // 
            this.expensesAmountLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.expensesAmountLabel.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.expensesAmountLabel.Location = new System.Drawing.Point(10, 25);
            this.expensesAmountLabel.Name = "expensesAmountLabel";
            this.expensesAmountLabel.Size = new System.Drawing.Size(212, 25);
            this.expensesAmountLabel.TabIndex = 1;
            this.expensesAmountLabel.Text = "�0.00";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(10, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "Total Expenses:";
            // 
            // ProfitDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 479);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.netProfitLabel);
            this.Controls.Add(this.profitChart);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "ProfitDashboard";
            this.Text = "Profit Analysis Dashboard";
            this.Load += new System.EventHandler(this.ProfitDashboard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.profitChart)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart profitChart;
        private System.Windows.Forms.Label netProfitLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label salesAmountLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label expensesAmountLabel;
        private System.Windows.Forms.Label label4;
    }
}
namespace Erp_V1
{
    partial class RecommendationForm
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

        private void InitializeComponent()
        {
            this.cmbCustomer = new System.Windows.Forms.ComboBox();
            this.btnGetRecommendations = new System.Windows.Forms.Button();
            this.dgvRecommendations = new System.Windows.Forms.DataGridView();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.lblNoRecommendations = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecommendations)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbCustomer
            // 
            this.cmbCustomer.FormattingEnabled = true;
            this.cmbCustomer.Location = new System.Drawing.Point(12, 36);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.Size = new System.Drawing.Size(260, 24);
            this.cmbCustomer.TabIndex = 0;
            // 
            // btnGetRecommendations
            // 
            this.btnGetRecommendations.Location = new System.Drawing.Point(278, 34);
            this.btnGetRecommendations.Name = "btnGetRecommendations";
            this.btnGetRecommendations.Size = new System.Drawing.Size(140, 23);
            this.btnGetRecommendations.TabIndex = 1;
            this.btnGetRecommendations.Text = "Get Recommendations";
            this.btnGetRecommendations.UseVisualStyleBackColor = true;
            this.btnGetRecommendations.Click += new System.EventHandler(this.btnGetRecommendations_Click);
            // 
            // dgvRecommendations
            // 
            this.dgvRecommendations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecommendations.Location = new System.Drawing.Point(12, 84);
            this.dgvRecommendations.Name = "dgvRecommendations";
            this.dgvRecommendations.RowHeadersWidth = 51;
            this.dgvRecommendations.Size = new System.Drawing.Size(406, 220);
            this.dgvRecommendations.TabIndex = 2;
            // 
            // lblCustomer
            // 
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Location = new System.Drawing.Point(12, 20);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(67, 16);
            this.lblCustomer.TabIndex = 3;
            this.lblCustomer.Text = "Customer:";
            // 
            // lblNoRecommendations
            // 
            this.lblNoRecommendations.AutoSize = true;
            this.lblNoRecommendations.ForeColor = System.Drawing.Color.Red;
            this.lblNoRecommendations.Location = new System.Drawing.Point(12, 310);
            this.lblNoRecommendations.Name = "lblNoRecommendations";
            this.lblNoRecommendations.Size = new System.Drawing.Size(175, 16);
            this.lblNoRecommendations.TabIndex = 4;
            this.lblNoRecommendations.Text = "No recommendations found.";
            this.lblNoRecommendations.Visible = false;
            // 
            // RecommendationForm
            // 
            this.ClientSize = new System.Drawing.Size(434, 331);
            this.Controls.Add(this.lblNoRecommendations);
            this.Controls.Add(this.lblCustomer);
            this.Controls.Add(this.dgvRecommendations);
            this.Controls.Add(this.btnGetRecommendations);
            this.Controls.Add(this.cmbCustomer);
            this.Name = "RecommendationForm";
            this.Text = "Product Recommendations";
            this.Load += new System.EventHandler(this.RecommendationForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecommendations)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.ComboBox cmbCustomer;
        private System.Windows.Forms.Button btnGetRecommendations;
        private System.Windows.Forms.DataGridView dgvRecommendations;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.Label lblNoRecommendations;
    }
}

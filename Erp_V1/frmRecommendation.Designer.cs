
namespace Erp_V1
{
    partial class frmRecommendation
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ComboBox cboCustomerNames;
        private System.Windows.Forms.Button btnGetRecommendations;
        private System.Windows.Forms.DataGridView dgvRecommendations;

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
            this.cboCustomerNames = new System.Windows.Forms.ComboBox();
            this.btnGetRecommendations = new System.Windows.Forms.Button();
            this.dgvRecommendations = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecommendations)).BeginInit();
            this.SuspendLayout();
            // 
            // cboCustomerNames
            // 
            this.cboCustomerNames.FormattingEnabled = true;
            this.cboCustomerNames.Location = new System.Drawing.Point(12, 12);
            this.cboCustomerNames.Name = "cboCustomerNames";
            this.cboCustomerNames.Size = new System.Drawing.Size(260, 21);
            this.cboCustomerNames.TabIndex = 0;
            // 
            // btnGetRecommendations
            // 
            this.btnGetRecommendations.Location = new System.Drawing.Point(278, 10);
            this.btnGetRecommendations.Name = "btnGetRecommendations";
            this.btnGetRecommendations.Size = new System.Drawing.Size(75, 23);
            this.btnGetRecommendations.TabIndex = 1;
            this.btnGetRecommendations.Text = "Get Recommendations";
            this.btnGetRecommendations.UseVisualStyleBackColor = true;
            this.btnGetRecommendations.Click += new System.EventHandler(this.btnGetRecommendations_Click);
            // 
            // dgvRecommendations
            // 
            this.dgvRecommendations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecommendations.Location = new System.Drawing.Point(12, 39);
            this.dgvRecommendations.Name = "dgvRecommendations";
            this.dgvRecommendations.Size = new System.Drawing.Size(776, 399);
            this.dgvRecommendations.TabIndex = 2;
            // 
            // frmRecommendation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvRecommendations);
            this.Controls.Add(this.btnGetRecommendations);
            this.Controls.Add(this.cboCustomerNames);
            this.Name = "frmRecommendation";
            this.Text = "Product Recommendations";
            this.Load += new System.EventHandler(this.frmRecommendation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecommendations)).EndInit();
            this.ResumeLayout(false);

        }
    }
}

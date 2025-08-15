// File: FrmRecommendations.Designer.cs
namespace Erp_V1
{
    partial class FrmRecommendations
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ComboBox cmbCustomers;
        private System.Windows.Forms.Label lblCustomerPrompt;
        private System.Windows.Forms.Button btnFetchRecommendations;
        private System.Windows.Forms.DataGridView dgvRecommendations;
        private System.Windows.Forms.Label lblStatus;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
        /// Initializes UI components and their properties.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cmbCustomers = new System.Windows.Forms.ComboBox();
            this.lblCustomerPrompt = new System.Windows.Forms.Label();
            this.btnFetchRecommendations = new System.Windows.Forms.Button();
            this.dgvRecommendations = new System.Windows.Forms.DataGridView();
            this.lblStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecommendations)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbCustomers
            // 
            this.cmbCustomers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                               | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCustomers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCustomers.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbCustomers.FormattingEnabled = true;
            this.cmbCustomers.Location = new System.Drawing.Point(160, 20);
            this.cmbCustomers.Name = "cmbCustomers";
            this.cmbCustomers.Size = new System.Drawing.Size(300, 23);
            this.cmbCustomers.TabIndex = 0;
            // 
            // lblCustomerPrompt
            // 
            this.lblCustomerPrompt.AutoSize = true;
            this.lblCustomerPrompt.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCustomerPrompt.Location = new System.Drawing.Point(20, 23);
            this.lblCustomerPrompt.Name = "lblCustomerPrompt";
            this.lblCustomerPrompt.Size = new System.Drawing.Size(120, 15);
            this.lblCustomerPrompt.TabIndex = 1;
            this.lblCustomerPrompt.Text = "Select a Customer:";
            // 
            // btnFetchRecommendations
            // 
            this.btnFetchRecommendations.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFetchRecommendations.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnFetchRecommendations.Location = new System.Drawing.Point(480, 18);
            this.btnFetchRecommendations.Name = "btnFetchRecommendations";
            this.btnFetchRecommendations.Size = new System.Drawing.Size(180, 27);
            this.btnFetchRecommendations.TabIndex = 2;
            this.btnFetchRecommendations.Text = "Get Recommendations";
            this.btnFetchRecommendations.UseVisualStyleBackColor = true;
            // 
            // dgvRecommendations
            // 
            this.dgvRecommendations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                         | System.Windows.Forms.AnchorStyles.Left)
                                                         | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRecommendations.BackgroundColor = System.Drawing.Color.White;
            this.dgvRecommendations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecommendations.GridColor = System.Drawing.Color.LightGray;
            this.dgvRecommendations.Location = new System.Drawing.Point(20, 60);
            this.dgvRecommendations.MultiSelect = false;
            this.dgvRecommendations.Name = "dgvRecommendations";
            this.dgvRecommendations.ReadOnly = true;
            this.dgvRecommendations.RowHeadersVisible = false;
            this.dgvRecommendations.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecommendations.Size = new System.Drawing.Size(640, 320);
            this.dgvRecommendations.TabIndex = 3;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatus.Location = new System.Drawing.Point(20, 395);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(35, 15);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "Ready";
            // 
            // FrmRecommendations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 420);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.dgvRecommendations);
            this.Controls.Add(this.btnFetchRecommendations);
            this.Controls.Add(this.lblCustomerPrompt);
            this.Controls.Add(this.cmbCustomers);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmRecommendations";
            this.Text = "Product Recommendations";
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecommendations)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}

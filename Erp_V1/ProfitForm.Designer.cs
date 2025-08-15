namespace Erp_V1
{
    partial class ProfitForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ProfitDisplayPanel = new System.Windows.Forms.Panel();
            this.ProfitValueLabel = new System.Windows.Forms.Label();
            this.ProfitTitleLabel = new System.Windows.Forms.Label();
            this.ProfitDisplayPanel.SuspendLayout();
            this.SuspendLayout();
            //
            // ProfitDisplayPanel
            //
            this.ProfitDisplayPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(176)))), ((int)(((byte)(225)))));
            this.ProfitDisplayPanel.Controls.Add(this.ProfitValueLabel);
            this.ProfitDisplayPanel.Controls.Add(this.ProfitTitleLabel);
            this.ProfitDisplayPanel.Location = new System.Drawing.Point(50, 50);
            this.ProfitDisplayPanel.Name = "ProfitDisplayPanel";
            this.ProfitDisplayPanel.Padding = new System.Windows.Forms.Padding(20);
            this.ProfitDisplayPanel.Size = new System.Drawing.Size(400, 150);
            this.ProfitDisplayPanel.TabIndex = 0;
            //
            // ProfitValueLabel
            //
            this.ProfitValueLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProfitValueLabel.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProfitValueLabel.ForeColor = System.Drawing.Color.White;
            this.ProfitValueLabel.Location = new System.Drawing.Point(20, 45);
            this.ProfitValueLabel.Name = "ProfitValueLabel";
            this.ProfitValueLabel.Size = new System.Drawing.Size(360, 85);
            this.ProfitValueLabel.TabIndex = 1;
            this.ProfitValueLabel.Text = "$0.00";
            this.ProfitValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // ProfitTitleLabel
            //
            this.ProfitTitleLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ProfitTitleLabel.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProfitTitleLabel.ForeColor = System.Drawing.Color.White;
            this.ProfitTitleLabel.Location = new System.Drawing.Point(20, 20);
            this.ProfitTitleLabel.Name = "ProfitTitleLabel";
            this.ProfitTitleLabel.Size = new System.Drawing.Size(360, 25);
            this.ProfitTitleLabel.TabIndex = 0;
            this.ProfitTitleLabel.Text = "Total Profit";
            this.ProfitTitleLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            //
            // ProfitForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.ClientSize = new System.Drawing.Size(500, 250);
            this.Controls.Add(this.ProfitDisplayPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ProfitForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Profit Dashboard";
            this.Load += new System.EventHandler(this.ProfitForm_Load);
            this.ProfitDisplayPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel ProfitDisplayPanel;
        private System.Windows.Forms.Label ProfitValueLabel;
        private System.Windows.Forms.Label ProfitTitleLabel;
    }
}
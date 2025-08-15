namespace Erp_V1
{
    partial class ProductRecommendationApp
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox textBoxUserId;
        private System.Windows.Forms.TextBox textBoxProductId;
        private System.Windows.Forms.Button buttonRecommend;
        private System.Windows.Forms.Label labelScore;

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
            this.textBoxUserId = new System.Windows.Forms.TextBox();
            this.textBoxProductId = new System.Windows.Forms.TextBox();
            this.buttonRecommend = new System.Windows.Forms.Button();
            this.labelScore = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxUserId
            // 
            this.textBoxUserId.Location = new System.Drawing.Point(12, 12);
            this.textBoxUserId.Name = "textBoxUserId";
            this.textBoxUserId.Size = new System.Drawing.Size(100, 20);
            this.textBoxUserId.TabIndex = 0;
            // 
            // textBoxProductId
            // 
            this.textBoxProductId.Location = new System.Drawing.Point(12, 38);
            this.textBoxProductId.Name = "textBoxProductId";
            this.textBoxProductId.Size = new System.Drawing.Size(100, 20);
            this.textBoxProductId.TabIndex = 1;
            // 
            // buttonRecommend
            // 
            this.buttonRecommend.Location = new System.Drawing.Point(12, 64);
            this.buttonRecommend.Name = "buttonRecommend";
            this.buttonRecommend.Size = new System.Drawing.Size(100, 23);
            this.buttonRecommend.TabIndex = 2;
            this.buttonRecommend.Text = "Recommend";
            this.buttonRecommend.UseVisualStyleBackColor = true;
            this.buttonRecommend.Click += new System.EventHandler(this.buttonRecommend_Click);
            // 
            // labelScore
            // 
            this.labelScore.AutoSize = true;
            this.labelScore.Location = new System.Drawing.Point(12, 90);
            this.labelScore.Name = "labelScore";
            this.labelScore.Size = new System.Drawing.Size(0, 13);
            this.labelScore.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.labelScore);
            this.Controls.Add(this.buttonRecommend);
            this.Controls.Add(this.textBoxProductId);
            this.Controls.Add(this.textBoxUserId);
            this.Name = "Form1";
            this.Text = "Product Recommendation";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
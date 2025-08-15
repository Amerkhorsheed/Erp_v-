//namespace Erp_V1
//{
//    partial class ChurnPredictionForm
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

//        #region Windows Form Designer generated code

//        private void InitializeComponent()
//        {
//            this.btnTrainModel = new MaterialSkin.Controls.MaterialButton();
//            this.btnGetChurningCustomers = new MaterialSkin.Controls.MaterialButton();
//            this.lstChurningCustomers = new MaterialSkin.Controls.MaterialListView();
//            this.colID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
//            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
//            this.colProbability = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader())); // CORRECTED
//            this.lblStatus = new MaterialSkin.Controls.MaterialLabel();
//            this.progressBar = new MaterialSkin.Controls.MaterialProgressBar();
//            this.btnCancel = new MaterialSkin.Controls.MaterialButton(); // ADDED
//            this.SuspendLayout();
//            // 
//            // btnTrainModel
//            // 
//            this.btnTrainModel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
//            this.btnTrainModel.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
//            this.btnTrainModel.Depth = 0;
//            this.btnTrainModel.HighEmphasis = true;
//            this.btnTrainModel.Icon = null;
//            this.btnTrainModel.Location = new System.Drawing.Point(23, 89);
//            this.btnTrainModel.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
//            this.btnTrainModel.MouseState = MaterialSkin.MouseState.HOVER;
//            this.btnTrainModel.Name = "btnTrainModel";
//            this.btnTrainModel.NoAccentTextColor = System.Drawing.Color.Empty;
//            this.btnTrainModel.Size = new System.Drawing.Size(116, 36);
//            this.btnTrainModel.TabIndex = 0;
//            this.btnTrainModel.Text = "Train Model";
//            this.btnTrainModel.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
//            this.btnTrainModel.UseAccentColor = false;
//            this.btnTrainModel.UseVisualStyleBackColor = true;
//            this.btnTrainModel.Click += new System.EventHandler(this.btnTrainModel_Click);
//            // 
//            // btnGetChurningCustomers
//            // 
//            this.btnGetChurningCustomers.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
//            this.btnGetChurningCustomers.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
//            this.btnGetChurningCustomers.Depth = 0;
//            this.btnGetChurningCustomers.HighEmphasis = true;
//            this.btnGetChurningCustomers.Icon = null;
//            this.btnGetChurningCustomers.Location = new System.Drawing.Point(147, 89);
//            this.btnGetChurningCustomers.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
//            this.btnGetChurningCustomers.MouseState = MaterialSkin.MouseState.HOVER;
//            this.btnGetChurningCustomers.Name = "btnGetChurningCustomers";
//            this.btnGetChurningCustomers.NoAccentTextColor = System.Drawing.Color.Empty;
//            this.btnGetChurningCustomers.Size = new System.Drawing.Size(149, 36);
//            this.btnGetChurningCustomers.TabIndex = 1;
//            this.btnGetChurningCustomers.Text = "Get Predictions";
//            this.btnGetChurningCustomers.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
//            this.btnGetChurningCustomers.UseAccentColor = true;
//            this.btnGetChurningCustomers.UseVisualStyleBackColor = true;
//            this.btnGetChurningCustomers.Click += new System.EventHandler(this.btnGetChurningCustomers_Click);
//            // 
//            // lstChurningCustomers
//            // 
//            this.lstChurningCustomers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
//            | System.Windows.Forms.AnchorStyles.Left)
//            | System.Windows.Forms.AnchorStyles.Right)));
//            this.lstChurningCustomers.AutoSizeTable = false;
//            this.lstChurningCustomers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
//            this.lstChurningCustomers.BorderStyle = System.Windows.Forms.BorderStyle.None;
//            this.lstChurningCustomers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
//            this.colID,
//            this.colName,
//            this.colProbability}); // CORRECTED
//            this.lstChurningCustomers.Depth = 0;
//            this.lstChurningCustomers.FullRowSelect = true;
//            this.lstChurningCustomers.HideSelection = false;
//            this.lstChurningCustomers.Location = new System.Drawing.Point(23, 142);
//            this.lstChurningCustomers.MinimumSize = new System.Drawing.Size(200, 100);
//            this.lstChurningCustomers.MouseLocation = new System.Drawing.Point(-1, -1);
//            this.lstChurningCustomers.MouseState = MaterialSkin.MouseState.OUT;
//            this.lstChurningCustomers.Name = "lstChurningCustomers";
//            this.lstChurningCustomers.OwnerDraw = true;
//            this.lstChurningCustomers.Size = new System.Drawing.Size(754, 303);
//            this.lstChurningCustomers.TabIndex = 2;
//            this.lstChurningCustomers.UseCompatibleStateImageBehavior = false;
//            this.lstChurningCustomers.View = System.Windows.Forms.View.Details;
//            // 
//            // colID
//            // 
//            this.colID.Text = "ID";
//            this.colID.Width = 80;
//            // 
//            // colName
//            // 
//            this.colName.Text = "Customer Name";
//            this.colName.Width = 250;
//            // 
//            // colProbability
//            // 
//            this.colProbability.Text = "Churn Probability"; // CORRECTED
//            this.colProbability.Width = 150;
//            // 
//            // lblStatus
//            // 
//            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
//            this.lblStatus.AutoSize = true;
//            this.lblStatus.Depth = 0;
//            this.lblStatus.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
//            this.lblStatus.Location = new System.Drawing.Point(20, 461);
//            this.lblStatus.MouseState = MaterialSkin.MouseState.HOVER;
//            this.lblStatus.Name = "lblStatus";
//            this.lblStatus.Size = new System.Drawing.Size(49, 19);
//            this.lblStatus.TabIndex = 3;
//            this.lblStatus.Text = "Ready.";
//            // 
//            // progressBar
//            // 
//            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
//            | System.Windows.Forms.AnchorStyles.Right)));
//            this.progressBar.Depth = 0;
//            this.progressBar.Location = new System.Drawing.Point(0, 497);
//            this.progressBar.MouseState = MaterialSkin.MouseState.HOVER;
//            this.progressBar.Name = "progressBar";
//            this.progressBar.Size = new System.Drawing.Size(800, 5);
//            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
//            this.progressBar.TabIndex = 4;
//            // 
//            // btnCancel
//            // 
//            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
//            this.btnCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
//            this.btnCancel.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
//            this.btnCancel.Depth = 0;
//            this.btnCancel.HighEmphasis = false;
//            this.btnCancel.Icon = null;
//            this.btnCancel.Location = new System.Drawing.Point(698, 89);
//            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
//            this.btnCancel.MouseState = MaterialSkin.MouseState.HOVER;
//            this.btnCancel.Name = "btnCancel";
//            this.btnCancel.NoAccentTextColor = System.Drawing.Color.Empty;
//            this.btnCancel.Size = new System.Drawing.Size(77, 36);
//            this.btnCancel.TabIndex = 5;
//            this.btnCancel.Text = "Cancel";
//            this.btnCancel.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Text;
//            this.btnCancel.UseAccentColor = false;
//            this.btnCancel.UseVisualStyleBackColor = true;
//            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
//            // 
//            // ChurnPredictionForm
//            // 
//            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
//            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
//            this.ClientSize = new System.Drawing.Size(800, 502);
//            this.Controls.Add(this.btnCancel);
//            this.Controls.Add(this.progressBar);
//            this.Controls.Add(this.lblStatus);
//            this.Controls.Add(this.lstChurningCustomers);
//            this.Controls.Add(this.btnGetChurningCustomers);
//            this.Controls.Add(this.btnTrainModel);
//            this.Name = "ChurnPredictionForm";
//            this.Text = "Professional Churn Prediction";
//            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChurnPredictionForm_FormClosing);
//            this.Load += new System.EventHandler(this.ChurnPredictionForm_Load);
//            this.ResumeLayout(false);
//            this.PerformLayout();
//        }

//        #endregion

//        private MaterialSkin.Controls.MaterialButton btnTrainModel;
//        private MaterialSkin.Controls.MaterialButton btnGetChurningCustomers;
//        private MaterialSkin.Controls.MaterialListView lstChurningCustomers;
//        private System.Windows.Forms.ColumnHeader colID;
//        private System.Windows.Forms.ColumnHeader colName;
//        private System.Windows.Forms.ColumnHeader colProbability;
//        private MaterialSkin.Controls.MaterialLabel lblStatus;
//        private MaterialSkin.Controls.MaterialProgressBar progressBar;
//        private MaterialSkin.Controls.MaterialButton btnCancel;
//    }
//}
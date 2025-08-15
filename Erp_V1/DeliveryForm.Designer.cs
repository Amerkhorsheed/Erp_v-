//// File: DeliveryForm.Designer.cs
//using MaterialSkin.Controls;
//using System.ComponentModel;
//using System.Drawing;
//using System.Windows.Forms;

//namespace Erp_V1
//{
//    partial class DeliveryForm
//    {
//        private IContainer components = null;
//        private MaterialCard panelTop;
//        private MaterialLabel searchLabel;
//        private MaterialTextBox searchTextBox;
//        private MaterialButton btnUpdate;
//        private MaterialButton btnDelete;
//        private MaterialButton btnRefresh;
//        private DataGridView dgvDeliveries;

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && (components != null)) components.Dispose();
//            base.Dispose(disposing);
//        }

//        private void InitializeComponent()
//        {
//            this.panelTop = new MaterialSkin.Controls.MaterialCard();
//            this.searchLabel = new MaterialSkin.Controls.MaterialLabel();
//            this.searchTextBox = new MaterialSkin.Controls.MaterialTextBox();
//            this.btnUpdate = new MaterialSkin.Controls.MaterialButton();
//            this.btnDelete = new MaterialSkin.Controls.MaterialButton();
//            this.btnRefresh = new MaterialSkin.Controls.MaterialButton();
//            this.dgvDeliveries = new System.Windows.Forms.DataGridView();
//            this.panelTop.SuspendLayout();
//            ((System.ComponentModel.ISupportInitialize)(this.dgvDeliveries)).BeginInit();
//            this.SuspendLayout();
//            // 
//            // panelTop
//            // 
//            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
//            this.panelTop.Controls.Add(this.searchLabel);
//            this.panelTop.Controls.Add(this.searchTextBox);
//            this.panelTop.Controls.Add(this.btnUpdate);
//            this.panelTop.Controls.Add(this.btnDelete);
//            this.panelTop.Controls.Add(this.btnRefresh);
//            this.panelTop.Depth = 0;
//            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
//            this.panelTop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
//            this.panelTop.Location = new System.Drawing.Point(3, 64);
//            this.panelTop.Margin = new System.Windows.Forms.Padding(14);
//            this.panelTop.MouseState = MaterialSkin.MouseState.HOVER;
//            this.panelTop.Name = "panelTop";
//            this.panelTop.Padding = new System.Windows.Forms.Padding(16, 8, 16, 8);
//            this.panelTop.Size = new System.Drawing.Size(994, 100);
//            this.panelTop.TabIndex = 1;
//            // 
//            // searchLabel
//            // 
//            this.searchLabel.Depth = 0;
//            this.searchLabel.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
//            this.searchLabel.Location = new System.Drawing.Point(19, 32);
//            this.searchLabel.MouseState = MaterialSkin.MouseState.HOVER;
//            this.searchLabel.Name = "searchLabel";
//            this.searchLabel.Size = new System.Drawing.Size(100, 36);
//            this.searchLabel.TabIndex = 0;
//            this.searchLabel.Text = "Search:";
//            this.searchLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
//            // 
//            // searchTextBox
//            // 
//            this.searchTextBox.AnimateReadOnly = false;
//            this.searchTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
//            this.searchTextBox.Depth = 0;
//            this.searchTextBox.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
//            this.searchTextBox.LeadingIcon = null;
//            this.searchTextBox.Location = new System.Drawing.Point(125, 24);
//            this.searchTextBox.MaxLength = 50;
//            this.searchTextBox.MouseState = MaterialSkin.MouseState.OUT;
//            this.searchTextBox.Multiline = false;
//            this.searchTextBox.Name = "searchTextBox";
//            this.searchTextBox.Size = new System.Drawing.Size(250, 50);
//            this.searchTextBox.TabIndex = 1;
//            this.searchTextBox.Text = "";
//            this.searchTextBox.TrailingIcon = null;
//            this.searchTextBox.TextChanged += new System.EventHandler(this.searchTextBox_TextChanged);
//            // 
//            // btnUpdate
//            // 
//            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
//            this.btnUpdate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
//            this.btnUpdate.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
//            this.btnUpdate.Depth = 0;
//            this.btnUpdate.HighEmphasis = true;
//            this.btnUpdate.Icon = null;
//            this.btnUpdate.Location = new System.Drawing.Point(582, 32);
//            this.btnUpdate.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
//            this.btnUpdate.MouseState = MaterialSkin.MouseState.HOVER;
//            this.btnUpdate.Name = "btnUpdate";
//            this.btnUpdate.NoAccentTextColor = System.Drawing.Color.Empty;
//            this.btnUpdate.Size = new System.Drawing.Size(133, 36);
//            this.btnUpdate.TabIndex = 2;
//            this.btnUpdate.Text = "Update/Assign";
//            this.btnUpdate.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
//            this.btnUpdate.UseAccentColor = false;
//            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
//            // 
//            // btnDelete
//            // 
//            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
//            this.btnDelete.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
//            this.btnDelete.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
//            this.btnDelete.Depth = 0;
//            this.btnDelete.HighEmphasis = true;
//            this.btnDelete.Icon = null;
//            this.btnDelete.Location = new System.Drawing.Point(723, 32);
//            this.btnDelete.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
//            this.btnDelete.MouseState = MaterialSkin.MouseState.HOVER;
//            this.btnDelete.Name = "btnDelete";
//            this.btnDelete.NoAccentTextColor = System.Drawing.Color.Empty;
//            this.btnDelete.Size = new System.Drawing.Size(73, 36);
//            this.btnDelete.TabIndex = 3;
//            this.btnDelete.Text = "Delete";
//            this.btnDelete.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
//            this.btnDelete.UseAccentColor = true;
//            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
//            // 
//            // btnRefresh
//            // 
//            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
//            this.btnRefresh.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
//            this.btnRefresh.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
//            this.btnRefresh.Depth = 0;
//            this.btnRefresh.DialogResult = System.Windows.Forms.DialogResult.Cancel;
//            this.btnRefresh.HighEmphasis = true;
//            this.btnRefresh.Icon = null;
//            this.btnRefresh.Location = new System.Drawing.Point(899, 32);
//            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
//            this.btnRefresh.MouseState = MaterialSkin.MouseState.HOVER;
//            this.btnRefresh.Name = "btnRefresh";
//            this.btnRefresh.NoAccentTextColor = System.Drawing.Color.Empty;
//            this.btnRefresh.Size = new System.Drawing.Size(84, 36);
//            this.btnRefresh.TabIndex = 4;
//            this.btnRefresh.Text = "Refresh";
//            this.btnRefresh.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Text;
//            this.btnRefresh.UseAccentColor = false;
//            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
//            // 
//            // dgvDeliveries
//            // 
//            this.dgvDeliveries.AllowUserToAddRows = false;
//            this.dgvDeliveries.AllowUserToDeleteRows = false;
//            this.dgvDeliveries.BackgroundColor = System.Drawing.Color.White;
//            this.dgvDeliveries.BorderStyle = System.Windows.Forms.BorderStyle.None;
//            this.dgvDeliveries.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
//            this.dgvDeliveries.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.dgvDeliveries.EnableHeadersVisualStyles = false;
//            this.dgvDeliveries.GridColor = System.Drawing.Color.Gainsboro;
//            this.dgvDeliveries.Location = new System.Drawing.Point(3, 164);
//            this.dgvDeliveries.MultiSelect = false;
//            this.dgvDeliveries.Name = "dgvDeliveries";
//            this.dgvDeliveries.ReadOnly = true;
//            this.dgvDeliveries.RowHeadersVisible = false;
//            this.dgvDeliveries.RowTemplate.Height = 24;
//            this.dgvDeliveries.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
//            this.dgvDeliveries.Size = new System.Drawing.Size(994, 433);
//            this.dgvDeliveries.TabIndex = 0;
//            // 
//            // DeliveryForm
//            // 
//            this.CancelButton = this.btnRefresh;
//            this.ClientSize = new System.Drawing.Size(1000, 600);
//            this.Controls.Add(this.dgvDeliveries);
//            this.Controls.Add(this.panelTop);
//            this.Name = "DeliveryForm";
//            this.Text = "Delivery Management";
//            this.Load += new System.EventHandler(this.DeliveryForm_Load);
//            this.panelTop.ResumeLayout(false);
//            this.panelTop.PerformLayout();
//            ((System.ComponentModel.ISupportInitialize)(this.dgvDeliveries)).EndInit();
//            this.ResumeLayout(false);

//        }
//    }
//}
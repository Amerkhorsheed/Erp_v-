// File: CustomerClassificationForm.Designer.cs
using MaterialSkin.Controls;
using System.Drawing;
using System.Windows.Forms;

namespace Erp_V1
{
    partial class CustomerClassificationForm
    {
        private System.ComponentModel.IContainer components = null;
        private MaterialCard panelTop; // Changed to MaterialCard for a richer look
        private MaterialButton btnAdd; // Changed to MaterialButton
        private MaterialButton btnEdit; // Changed to MaterialButton
        private MaterialButton btnDelete; // Changed to MaterialButton
        private MaterialButton btnRefresh; // Changed to MaterialButton
        private MaterialTextBox searchTextBox; // New: Search Text Box
        private MaterialLabel searchLabel; // New: Search Label
        private DataGridView dgvClassifications; // DataGridView remains, but we'll style it

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelTop = new MaterialSkin.Controls.MaterialCard();
            this.searchTextBox = new MaterialSkin.Controls.MaterialTextBox();
            this.searchLabel = new MaterialSkin.Controls.MaterialLabel();
            this.btnRefresh = new MaterialSkin.Controls.MaterialButton();
            this.btnDelete = new MaterialSkin.Controls.MaterialButton();
            this.btnEdit = new MaterialSkin.Controls.MaterialButton();
            this.btnAdd = new MaterialSkin.Controls.MaterialButton();
            this.dgvClassifications = new System.Windows.Forms.DataGridView();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClassifications)).BeginInit();
            this.SuspendLayout();
            //
            // panelTop
            //
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255))))); // MaterialCard uses theme colors
            this.panelTop.Controls.Add(this.searchTextBox);
            this.panelTop.Controls.Add(this.searchLabel);
            this.panelTop.Controls.Add(this.btnRefresh);
            this.panelTop.Controls.Add(this.btnDelete);
            this.panelTop.Controls.Add(this.btnEdit);
            this.panelTop.Controls.Add(this.btnAdd);
            this.panelTop.Depth = 0;
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.panelTop.Location = new System.Drawing.Point(3, 64); // Adjusted for MaterialForm title bar
            this.panelTop.Margin = new System.Windows.Forms.Padding(14); // Default MaterialCard margin
            this.panelTop.MouseState = MaterialSkin.MouseState.HOVER;
            this.panelTop.Name = "panelTop";
            this.panelTop.Padding = new System.Windows.Forms.Padding(16, 8, 16, 8); // Padding inside the card
            this.panelTop.Size = new System.Drawing.Size(794, 90); // Increased height to accommodate search bar
            this.panelTop.TabIndex = 1;
            //
            // searchTextBox
            //
            this.searchTextBox.AnimateReadOnly = false;
            this.searchTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.searchTextBox.Depth = 0;
            this.searchTextBox.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.searchTextBox.LeadingIcon = null;
            this.searchTextBox.Location = new System.Drawing.Point(170, 24); // Positioned next to label
            this.searchTextBox.MaxLength = 50;
            this.searchTextBox.MouseState = MaterialSkin.MouseState.OUT;
            this.searchTextBox.Multiline = false;
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(250, 50);
            this.searchTextBox.TabIndex = 4;
            this.searchTextBox.Text = "";
            this.searchTextBox.TrailingIcon = null;
            this.searchTextBox.TextChanged += new System.EventHandler(this.searchTextBox_TextChanged); // Event for filtering
            //
            // searchLabel
            //
            this.searchLabel.Depth = 0;
            this.searchLabel.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.searchLabel.Location = new System.Drawing.Point(16, 24); // Positioned within card
            this.searchLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.searchLabel.Name = "searchLabel";
            this.searchLabel.Size = new System.Drawing.Size(148, 50);
            this.searchLabel.TabIndex = 5;
            this.searchLabel.Text = "Search Customer:";
            this.searchLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // btnRefresh
            //
            this.btnRefresh.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnRefresh.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnRefresh.Depth = 0;
            this.btnRefresh.HighEmphasis = true;
            this.btnRefresh.Icon = null;
            this.btnRefresh.Location = new System.Drawing.Point(680, 29); // Right-aligned, grouped
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnRefresh.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnRefresh.Size = new System.Drawing.Size(91, 36);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Text; // Text button for less visual weight
            this.btnRefresh.UseAccentColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            //
            // btnDelete
            //
            this.btnDelete.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnDelete.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnDelete.Depth = 0;
            this.btnDelete.HighEmphasis = true;
            this.btnDelete.Icon = null;
            this.btnDelete.Location = new System.Drawing.Point(590, 29); // Positioned next to Refresh
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnDelete.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnDelete.Size = new System.Drawing.Size(78, 36);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Delete";
            this.btnDelete.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained; // Contained for destructive action
            this.btnDelete.UseAccentColor = true; // Use accent for delete
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            //
            // btnEdit
            //
            this.btnEdit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnEdit.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnEdit.Depth = 0;
            this.btnEdit.HighEmphasis = true;
            this.btnEdit.Icon = null;
            this.btnEdit.Location = new System.Drawing.Point(520, 29); // Positioned next to Delete
            this.btnEdit.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnEdit.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnEdit.Size = new System.Drawing.Size(62, 36);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "Edit";
            this.btnEdit.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Outlined; // Outlined for secondary action
            this.btnEdit.UseAccentColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            //
            // btnAdd
            //
            this.btnAdd.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAdd.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnAdd.Depth = 0;
            this.btnAdd.HighEmphasis = true;
            this.btnAdd.Icon = null;
            this.btnAdd.Location = new System.Drawing.Point(450, 29); // Positioned next to Edit
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnAdd.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnAdd.Size = new System.Drawing.Size(62, 36);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add";
            this.btnAdd.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained; // Contained for primary action
            this.btnAdd.UseAccentColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            //
            // dgvClassifications
            //
            this.dgvClassifications.AllowUserToAddRows = false;
            this.dgvClassifications.AllowUserToDeleteRows = false;
            this.dgvClassifications.BackgroundColor = System.Drawing.Color.White;
            this.dgvClassifications.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvClassifications.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClassifications.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvClassifications.EnableHeadersVisualStyles = false; // Important for custom styling
            this.dgvClassifications.GridColor = System.Drawing.Color.LightGray; // This will be set dynamically in the .cs file
            this.dgvClassifications.Location = new System.Drawing.Point(3, 154); // Adjusted for MaterialForm and MaterialCard
            this.dgvClassifications.Name = "dgvClassifications";
            this.dgvClassifications.ReadOnly = true;
            this.dgvClassifications.RowHeadersVisible = false; // Keep headers invisible
            this.dgvClassifications.RowHeadersWidth = 51;
            this.dgvClassifications.Size = new System.Drawing.Size(794, 301); // Remaining space
            this.dgvClassifications.TabIndex = 0;
            //
            // CustomerClassificationForm
            //
            this.ClientSize = new System.Drawing.Size(800, 450); // Keep client size
            this.Controls.Add(this.dgvClassifications);
            this.Controls.Add(this.panelTop);
            this.Name = "CustomerClassificationForm";
            this.Text = "Customer Classification"; // Form title
            this.Load += new System.EventHandler(this.CustomerClassificationForm_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout(); // For searchTextBox
            ((System.ComponentModel.ISupportInitialize)(this.dgvClassifications)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
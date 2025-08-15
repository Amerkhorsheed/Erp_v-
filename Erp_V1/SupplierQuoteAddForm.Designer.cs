// File: SupplierQuoteAddForm.Designer.cs
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace Erp_V1
{
    partial class SupplierQuoteAddForm
    {
        private IContainer components = null;
        private TableLayoutPanel tableLayout;
        private MaterialLabel lblSupplier;
        private MaterialComboBox comboSupplier;
        private MaterialLabel lblDate;
        private DateTimePicker dtpDate;
        private MaterialLabel lblAmount;
        private NumericUpDown numAmount;
        private MaterialLabel lblCurrency;
        private MaterialTextBox txtCurrency;
        private MaterialLabel lblDetails;
        private TextBox txtDetails;
        private FlowLayoutPanel buttonPanel;
        private MaterialButton btnSave;
        private MaterialButton btnCancel;
        private ErrorProvider errorProvider;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.lblSupplier = new MaterialSkin.Controls.MaterialLabel();
            this.comboSupplier = new MaterialSkin.Controls.MaterialComboBox();
            this.lblDate = new MaterialSkin.Controls.MaterialLabel();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.lblAmount = new MaterialSkin.Controls.MaterialLabel();
            this.numAmount = new System.Windows.Forms.NumericUpDown();
            this.lblCurrency = new MaterialSkin.Controls.MaterialLabel();
            this.txtCurrency = new MaterialSkin.Controls.MaterialTextBox();
            this.lblDetails = new MaterialSkin.Controls.MaterialLabel();
            this.txtDetails = new System.Windows.Forms.TextBox();
            this.buttonPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCancel = new MaterialSkin.Controls.MaterialButton();
            this.btnSave = new MaterialSkin.Controls.MaterialButton();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.tableLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAmount)).BeginInit();
            this.buttonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayout
            // 
            this.tableLayout.AutoSize = true;
            this.tableLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayout.ColumnCount = 2;
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayout.Controls.Add(this.lblSupplier, 0, 0);
            this.tableLayout.Controls.Add(this.comboSupplier, 1, 0);
            this.tableLayout.Controls.Add(this.lblDate, 0, 1);
            this.tableLayout.Controls.Add(this.dtpDate, 1, 1);
            this.tableLayout.Controls.Add(this.lblAmount, 0, 2);
            this.tableLayout.Controls.Add(this.numAmount, 1, 2);
            this.tableLayout.Controls.Add(this.lblCurrency, 0, 3);
            this.tableLayout.Controls.Add(this.txtCurrency, 1, 3);
            this.tableLayout.Controls.Add(this.lblDetails, 0, 4);
            this.tableLayout.Controls.Add(this.txtDetails, 1, 4);
            this.tableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayout.Location = new System.Drawing.Point(3, 64);
            this.tableLayout.Name = "tableLayout";
            this.tableLayout.Padding = new System.Windows.Forms.Padding(20, 20, 20, 10);
            this.tableLayout.RowCount = 5;
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayout.Size = new System.Drawing.Size(594, 381);
            this.tableLayout.TabIndex = 0;
            // 
            // lblSupplier
            // 
            this.lblSupplier.Depth = 0;
            this.lblSupplier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSupplier.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblSupplier.Location = new System.Drawing.Point(23, 20);
            this.lblSupplier.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(187, 60);
            this.lblSupplier.TabIndex = 0;
            this.lblSupplier.Text = "Supplier:";
            this.lblSupplier.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboSupplier
            // 
            this.comboSupplier.AutoResize = false;
            this.comboSupplier.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.comboSupplier.Depth = 0;
            this.comboSupplier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboSupplier.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboSupplier.DropDownHeight = 118;
            this.comboSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSupplier.DropDownWidth = 121;
            this.comboSupplier.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.comboSupplier.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.comboSupplier.IntegralHeight = false;
            this.comboSupplier.ItemHeight = 29;
            this.comboSupplier.Location = new System.Drawing.Point(216, 23);
            this.comboSupplier.MaxDropDownItems = 4;
            this.comboSupplier.MouseState = MaterialSkin.MouseState.OUT;
            this.comboSupplier.Name = "comboSupplier";
            this.comboSupplier.Size = new System.Drawing.Size(355, 35);
            this.comboSupplier.StartIndex = 0;
            this.comboSupplier.TabIndex = 1;
            this.comboSupplier.UseTallSize = false;
            // 
            // lblDate
            // 
            this.lblDate.Depth = 0;
            this.lblDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDate.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblDate.Location = new System.Drawing.Point(23, 80);
            this.lblDate.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(187, 60);
            this.lblDate.TabIndex = 2;
            this.lblDate.Text = "Quote Date:";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpDate
            // 
            this.dtpDate.Dock = System.Windows.Forms.DockStyle.Left;
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(216, 83);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(150, 22);
            this.dtpDate.TabIndex = 3;
            // 
            // lblAmount
            // 
            this.lblAmount.Depth = 0;
            this.lblAmount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAmount.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblAmount.Location = new System.Drawing.Point(23, 140);
            this.lblAmount.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(187, 60);
            this.lblAmount.TabIndex = 4;
            this.lblAmount.Text = "Total Amount:";
            this.lblAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numAmount
            // 
            this.numAmount.DecimalPlaces = 2;
            this.numAmount.Dock = System.Windows.Forms.DockStyle.Left;
            this.numAmount.Location = new System.Drawing.Point(216, 143);
            this.numAmount.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numAmount.Name = "numAmount";
            this.numAmount.Size = new System.Drawing.Size(150, 22);
            this.numAmount.TabIndex = 5;
            // 
            // lblCurrency
            // 
            this.lblCurrency.Depth = 0;
            this.lblCurrency.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCurrency.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblCurrency.Location = new System.Drawing.Point(23, 200);
            this.lblCurrency.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblCurrency.Name = "lblCurrency";
            this.lblCurrency.Size = new System.Drawing.Size(187, 60);
            this.lblCurrency.TabIndex = 6;
            this.lblCurrency.Text = "Currency:";
            this.lblCurrency.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCurrency
            // 
            this.txtCurrency.AnimateReadOnly = false;
            this.txtCurrency.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCurrency.Depth = 0;
            this.txtCurrency.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCurrency.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtCurrency.LeadingIcon = null;
            this.txtCurrency.Location = new System.Drawing.Point(216, 203);
            this.txtCurrency.MaxLength = 10;
            this.txtCurrency.MouseState = MaterialSkin.MouseState.OUT;
            this.txtCurrency.Multiline = false;
            this.txtCurrency.Name = "txtCurrency";
            this.txtCurrency.Size = new System.Drawing.Size(355, 50);
            this.txtCurrency.TabIndex = 7;
            this.txtCurrency.Text = "";
            this.txtCurrency.TrailingIcon = null;
            // 
            // lblDetails
            // 
            this.lblDetails.Depth = 0;
            this.lblDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDetails.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblDetails.Location = new System.Drawing.Point(23, 260);
            this.lblDetails.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblDetails.Name = "lblDetails";
            this.lblDetails.Size = new System.Drawing.Size(187, 111);
            this.lblDetails.TabIndex = 8;
            this.lblDetails.Text = "Details:";
            this.lblDetails.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDetails
            // 
            this.txtDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDetails.Location = new System.Drawing.Point(216, 263);
            this.txtDetails.Multiline = true;
            this.txtDetails.Name = "txtDetails";
            this.txtDetails.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDetails.Size = new System.Drawing.Size(355, 105);
            this.txtDetails.TabIndex = 9;
            // 
            // buttonPanel
            // 
            this.buttonPanel.Controls.Add(this.btnCancel);
            this.buttonPanel.Controls.Add(this.btnSave);
            this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.buttonPanel.Location = new System.Drawing.Point(3, 445);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Padding = new System.Windows.Forms.Padding(10);
            this.buttonPanel.Size = new System.Drawing.Size(594, 66);
            this.buttonPanel.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCancel.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnCancel.Depth = 0;
            this.btnCancel.HighEmphasis = true;
            this.btnCancel.Icon = null;
            this.btnCancel.Location = new System.Drawing.Point(493, 16);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnCancel.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnCancel.Size = new System.Drawing.Size(77, 36);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Text;
            this.btnCancel.UseAccentColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSave.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnSave.Depth = 0;
            this.btnSave.HighEmphasis = true;
            this.btnSave.Icon = null;
            this.btnSave.Location = new System.Drawing.Point(421, 16);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnSave.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnSave.Name = "btnSave";
            this.btnSave.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnSave.Size = new System.Drawing.Size(64, 36);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnSave.UseAccentColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // SupplierQuoteAddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 514);
            this.Controls.Add(this.tableLayout);
            this.Controls.Add(this.buttonPanel);
            this.MaximumSize = new System.Drawing.Size(800, 600);
            this.MinimumSize = new System.Drawing.Size(500, 400);
            this.Name = "SupplierQuoteAddForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add/Edit Supplier Quote";
            this.Load += new System.EventHandler(this.SupplierQuoteAddForm_Load);
            this.tableLayout.ResumeLayout(false);
            this.tableLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAmount)).EndInit();
            this.buttonPanel.ResumeLayout(false);
            this.buttonPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
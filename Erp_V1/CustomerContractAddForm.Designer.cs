// File: CustomerContractAddForm.Designer.cs
using MaterialSkin.Controls;
using System.Drawing;
using System.Windows.Forms;

namespace Erp_V1
{
    partial class CustomerContractAddForm
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel mainTable;
        private MaterialLabel customerLabel;
        private MaterialComboBox customerComboBox;
        private MaterialLabel contractNumberLabel;
        private MaterialTextBox contractNumberTextBox;
        private MaterialLabel descriptionLabel;
        private MaterialMultiLineTextBox descriptionTextBox;
        private MaterialLabel startDateLabel;
        private DateTimePicker startDatePicker;
        private MaterialLabel endDateLabel;
        private FlowLayoutPanel endDatePanel;
        private MaterialCheckbox hasEndDateCheckBox;
        private DateTimePicker endDatePicker;
        private MaterialLabel statusLabel;
        private MaterialComboBox statusComboBox;
        private FlowLayoutPanel buttonPanel;
        private MaterialButton saveButton;
        private MaterialButton cancelButton;
        private ErrorProvider errorProvider;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mainTable = new System.Windows.Forms.TableLayoutPanel();
            this.customerLabel = new MaterialSkin.Controls.MaterialLabel();
            this.customerComboBox = new MaterialSkin.Controls.MaterialComboBox();
            this.contractNumberLabel = new MaterialSkin.Controls.MaterialLabel();
            this.contractNumberTextBox = new MaterialSkin.Controls.MaterialTextBox();
            this.descriptionLabel = new MaterialSkin.Controls.MaterialLabel();
            this.descriptionTextBox = new MaterialSkin.Controls.MaterialMultiLineTextBox();
            this.startDateLabel = new MaterialSkin.Controls.MaterialLabel();
            this.startDatePicker = new System.Windows.Forms.DateTimePicker();
            this.endDateLabel = new MaterialSkin.Controls.MaterialLabel();
            this.endDatePanel = new System.Windows.Forms.FlowLayoutPanel();
            this.hasEndDateCheckBox = new MaterialSkin.Controls.MaterialCheckbox();
            this.endDatePicker = new System.Windows.Forms.DateTimePicker();
            this.statusLabel = new MaterialSkin.Controls.MaterialLabel();
            this.statusComboBox = new MaterialSkin.Controls.MaterialComboBox();
            this.buttonPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.saveButton = new MaterialSkin.Controls.MaterialButton();
            this.cancelButton = new MaterialSkin.Controls.MaterialButton();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.mainTable.SuspendLayout();
            this.endDatePanel.SuspendLayout();
            this.buttonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // mainTable
            // 
            this.mainTable.ColumnCount = 2;
            this.mainTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.mainTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.mainTable.Controls.Add(this.customerLabel, 0, 0);
            this.mainTable.Controls.Add(this.customerComboBox, 1, 0);
            this.mainTable.Controls.Add(this.contractNumberLabel, 0, 1);
            this.mainTable.Controls.Add(this.contractNumberTextBox, 1, 1);
            this.mainTable.Controls.Add(this.descriptionLabel, 0, 2);
            this.mainTable.Controls.Add(this.descriptionTextBox, 1, 2);
            this.mainTable.Controls.Add(this.startDateLabel, 0, 3);
            this.mainTable.Controls.Add(this.startDatePicker, 1, 3);
            this.mainTable.Controls.Add(this.endDateLabel, 0, 4);
            this.mainTable.Controls.Add(this.endDatePanel, 1, 4);
            this.mainTable.Controls.Add(this.statusLabel, 0, 5);
            this.mainTable.Controls.Add(this.statusComboBox, 1, 5);
            this.mainTable.Controls.Add(this.buttonPanel, 1, 6);
            this.mainTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTable.Location = new System.Drawing.Point(3, 64);
            this.mainTable.Name = "mainTable";
            this.mainTable.Padding = new System.Windows.Forms.Padding(24);
            this.mainTable.RowCount = 7;
            this.mainTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.mainTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.mainTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.mainTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.mainTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.mainTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.mainTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTable.Size = new System.Drawing.Size(648, 524);
            this.mainTable.TabIndex = 0;
            // 
            // customerLabel
            // 
            this.customerLabel.Depth = 0;
            this.customerLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customerLabel.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.customerLabel.Location = new System.Drawing.Point(27, 24);
            this.customerLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.customerLabel.Name = "customerLabel";
            this.customerLabel.Size = new System.Drawing.Size(204, 56);
            this.customerLabel.TabIndex = 0;
            this.customerLabel.Text = "Customer:";
            this.customerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // customerComboBox
            // 
            this.customerComboBox.AutoResize = false;
            this.customerComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.customerComboBox.Depth = 0;
            this.customerComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customerComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.customerComboBox.DropDownHeight = 174;
            this.customerComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.customerComboBox.DropDownWidth = 121;
            this.customerComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.customerComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.customerComboBox.IntegralHeight = false;
            this.customerComboBox.ItemHeight = 43;
            this.customerComboBox.Location = new System.Drawing.Point(237, 27);
            this.customerComboBox.MaxDropDownItems = 4;
            this.customerComboBox.MouseState = MaterialSkin.MouseState.OUT;
            this.customerComboBox.Name = "customerComboBox";
            this.customerComboBox.Size = new System.Drawing.Size(384, 49);
            this.customerComboBox.StartIndex = 0;
            this.customerComboBox.TabIndex = 1;
            // 
            // contractNumberLabel
            // 
            this.contractNumberLabel.Depth = 0;
            this.contractNumberLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contractNumberLabel.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.contractNumberLabel.Location = new System.Drawing.Point(27, 80);
            this.contractNumberLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.contractNumberLabel.Name = "contractNumberLabel";
            this.contractNumberLabel.Size = new System.Drawing.Size(204, 56);
            this.contractNumberLabel.TabIndex = 2;
            this.contractNumberLabel.Text = "Contract #:";
            this.contractNumberLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // contractNumberTextBox
            // 
            this.contractNumberTextBox.AnimateReadOnly = false;
            this.contractNumberTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.contractNumberTextBox.Depth = 0;
            this.contractNumberTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contractNumberTextBox.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.contractNumberTextBox.LeadingIcon = null;
            this.contractNumberTextBox.Location = new System.Drawing.Point(237, 83);
            this.contractNumberTextBox.MaxLength = 50;
            this.contractNumberTextBox.MouseState = MaterialSkin.MouseState.OUT;
            this.contractNumberTextBox.Multiline = false;
            this.contractNumberTextBox.Name = "contractNumberTextBox";
            this.contractNumberTextBox.Size = new System.Drawing.Size(384, 50);
            this.contractNumberTextBox.TabIndex = 3;
            this.contractNumberTextBox.Text = "";
            this.contractNumberTextBox.TrailingIcon = null;
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.Depth = 0;
            this.descriptionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.descriptionLabel.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.descriptionLabel.Location = new System.Drawing.Point(27, 136);
            this.descriptionLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(204, 120);
            this.descriptionLabel.TabIndex = 4;
            this.descriptionLabel.Text = "Description:";
            this.descriptionLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.descriptionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.descriptionTextBox.Depth = 0;
            this.descriptionTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.descriptionTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.descriptionTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.descriptionTextBox.Location = new System.Drawing.Point(237, 139);
            this.descriptionTextBox.MouseState = MaterialSkin.MouseState.HOVER;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.descriptionTextBox.Size = new System.Drawing.Size(384, 114);
            this.descriptionTextBox.TabIndex = 5;
            this.descriptionTextBox.Text = "";
            // 
            // startDateLabel
            // 
            this.startDateLabel.Depth = 0;
            this.startDateLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.startDateLabel.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.startDateLabel.Location = new System.Drawing.Point(27, 256);
            this.startDateLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.startDateLabel.Name = "startDateLabel";
            this.startDateLabel.Size = new System.Drawing.Size(204, 56);
            this.startDateLabel.TabIndex = 6;
            this.startDateLabel.Text = "Start Date:";
            this.startDateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // startDatePicker
            // 
            this.startDatePicker.Dock = System.Windows.Forms.DockStyle.Left;
            this.startDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.startDatePicker.Location = new System.Drawing.Point(237, 259);
            this.startDatePicker.Name = "startDatePicker";
            this.startDatePicker.Size = new System.Drawing.Size(200, 22);
            this.startDatePicker.TabIndex = 7;
            // 
            // endDateLabel
            // 
            this.endDateLabel.Depth = 0;
            this.endDateLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.endDateLabel.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.endDateLabel.Location = new System.Drawing.Point(27, 312);
            this.endDateLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.endDateLabel.Name = "endDateLabel";
            this.endDateLabel.Size = new System.Drawing.Size(204, 56);
            this.endDateLabel.TabIndex = 8;
            this.endDateLabel.Text = "End Date:";
            this.endDateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // endDatePanel
            // 
            this.endDatePanel.Controls.Add(this.hasEndDateCheckBox);
            this.endDatePanel.Controls.Add(this.endDatePicker);
            this.endDatePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.endDatePanel.Location = new System.Drawing.Point(237, 315);
            this.endDatePanel.Name = "endDatePanel";
            this.endDatePanel.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.endDatePanel.Size = new System.Drawing.Size(384, 50);
            this.endDatePanel.TabIndex = 9;
            // 
            // hasEndDateCheckBox
            // 
            this.hasEndDateCheckBox.Depth = 0;
            this.hasEndDateCheckBox.Location = new System.Drawing.Point(0, 8);
            this.hasEndDateCheckBox.Margin = new System.Windows.Forms.Padding(0);
            this.hasEndDateCheckBox.MouseLocation = new System.Drawing.Point(-1, -1);
            this.hasEndDateCheckBox.MouseState = MaterialSkin.MouseState.HOVER;
            this.hasEndDateCheckBox.Name = "hasEndDateCheckBox";
            this.hasEndDateCheckBox.ReadOnly = false;
            this.hasEndDateCheckBox.Ripple = true;
            this.hasEndDateCheckBox.Size = new System.Drawing.Size(26, 26);
            this.hasEndDateCheckBox.TabIndex = 0;
            this.hasEndDateCheckBox.UseVisualStyleBackColor = true;
            // 
            // endDatePicker
            // 
            this.endDatePicker.Location = new System.Drawing.Point(29, 11);
            this.endDatePicker.Name = "endDatePicker";
            this.endDatePicker.Size = new System.Drawing.Size(200, 22);
            this.endDatePicker.TabIndex = 1;
            // 
            // statusLabel
            // 
            this.statusLabel.Depth = 0;
            this.statusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusLabel.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.statusLabel.Location = new System.Drawing.Point(27, 368);
            this.statusLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(204, 56);
            this.statusLabel.TabIndex = 10;
            this.statusLabel.Text = "Status:";
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // statusComboBox
            // 
            this.statusComboBox.AutoResize = false;
            this.statusComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.statusComboBox.Depth = 0;
            this.statusComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.statusComboBox.DropDownHeight = 174;
            this.statusComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.statusComboBox.DropDownWidth = 121;
            this.statusComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.statusComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.statusComboBox.IntegralHeight = false;
            this.statusComboBox.ItemHeight = 43;
            this.statusComboBox.Location = new System.Drawing.Point(237, 371);
            this.statusComboBox.MaxDropDownItems = 4;
            this.statusComboBox.MouseState = MaterialSkin.MouseState.OUT;
            this.statusComboBox.Name = "statusComboBox";
            this.statusComboBox.Size = new System.Drawing.Size(384, 49);
            this.statusComboBox.StartIndex = 0;
            this.statusComboBox.TabIndex = 11;
            // 
            // buttonPanel
            // 
            this.buttonPanel.Controls.Add(this.saveButton);
            this.buttonPanel.Controls.Add(this.cancelButton);
            this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.buttonPanel.Location = new System.Drawing.Point(359, 427);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Padding = new System.Windows.Forms.Padding(0, 16, 0, 0);
            this.buttonPanel.Size = new System.Drawing.Size(262, 70);
            this.buttonPanel.TabIndex = 12;
            // 
            // saveButton
            // 
            this.saveButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.saveButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.saveButton.Depth = 0;
            this.saveButton.HighEmphasis = true;
            this.saveButton.Icon = null;
            this.saveButton.Location = new System.Drawing.Point(194, 22);
            this.saveButton.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.saveButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.saveButton.Name = "saveButton";
            this.saveButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.saveButton.Size = new System.Drawing.Size(64, 36);
            this.saveButton.TabIndex = 1;
            this.saveButton.Text = "Save";
            this.saveButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.saveButton.UseAccentColor = false;
            // 
            // cancelButton
            // 
            this.cancelButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cancelButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.cancelButton.Depth = 0;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.HighEmphasis = false;
            this.cancelButton.Icon = null;
            this.cancelButton.Location = new System.Drawing.Point(109, 22);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.cancelButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.cancelButton.Size = new System.Drawing.Size(77, 36);
            this.cancelButton.TabIndex = 0;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Outlined;
            this.cancelButton.UseAccentColor = false;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // CustomerContractAddForm
            // 
            this.AcceptButton = this.saveButton;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(654, 591);
            this.Controls.Add(this.mainTable);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomerContractAddForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.mainTable.ResumeLayout(false);
            this.endDatePanel.ResumeLayout(false);
            this.buttonPanel.ResumeLayout(false);
            this.buttonPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
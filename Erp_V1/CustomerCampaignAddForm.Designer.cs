// File: CustomerCampaignAddForm.Designer.cs
namespace Erp_V1
{
    partial class CustomerCampaignAddForm
    {
        private System.ComponentModel.IContainer components = null;
        private MaterialSkin.Controls.MaterialLabel customerLabel;
        private MaterialSkin.Controls.MaterialComboBox customerComboBox;
        private MaterialSkin.Controls.MaterialLabel nameLabel;
        private MaterialSkin.Controls.MaterialTextBox campaignNameTextBox;
        private MaterialSkin.Controls.MaterialLabel startDateLabel;
        private System.Windows.Forms.DateTimePicker startDatePicker;
        private MaterialSkin.Controls.MaterialCheckbox hasEndDateCheckBox;
        private System.Windows.Forms.DateTimePicker endDatePicker;
        private MaterialSkin.Controls.MaterialLabel impactLabel;
        private MaterialSkin.Controls.MaterialTextBox impactTextBox;
        private MaterialSkin.Controls.MaterialButton saveButton;
        private MaterialSkin.Controls.MaterialButton cancelButton;
        private System.Windows.Forms.ErrorProvider errorProvider;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.customerLabel = new MaterialSkin.Controls.MaterialLabel();
            this.customerComboBox = new MaterialSkin.Controls.MaterialComboBox();
            this.nameLabel = new MaterialSkin.Controls.MaterialLabel();
            this.campaignNameTextBox = new MaterialSkin.Controls.MaterialTextBox();
            this.startDateLabel = new MaterialSkin.Controls.MaterialLabel();
            this.startDatePicker = new System.Windows.Forms.DateTimePicker();
            this.hasEndDateCheckBox = new MaterialSkin.Controls.MaterialCheckbox();
            this.endDatePicker = new System.Windows.Forms.DateTimePicker();
            this.impactLabel = new MaterialSkin.Controls.MaterialLabel();
            this.impactTextBox = new MaterialSkin.Controls.MaterialTextBox();
            this.saveButton = new MaterialSkin.Controls.MaterialButton();
            this.cancelButton = new MaterialSkin.Controls.MaterialButton();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // customerLabel
            // 
            this.customerLabel.AutoSize = true;
            this.customerLabel.Depth = 0;
            this.customerLabel.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.customerLabel.Location = new System.Drawing.Point(33, 110);
            this.customerLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.customerLabel.Name = "customerLabel";
            this.customerLabel.Size = new System.Drawing.Size(73, 19);
            this.customerLabel.TabIndex = 0;
            this.customerLabel.Text = "Customer:";
            // 
            // customerComboBox
            // 
            this.customerComboBox.AutoResize = false;
            this.customerComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.customerComboBox.Depth = 0;
            this.customerComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.customerComboBox.DropDownHeight = 147;
            this.customerComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.customerComboBox.DropDownWidth = 121;
            this.customerComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.customerComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.customerComboBox.FormattingEnabled = true;
            this.customerComboBox.IntegralHeight = false;
            this.customerComboBox.ItemHeight = 29;
            this.customerComboBox.Location = new System.Drawing.Point(181, 94);
            this.customerComboBox.MaxDropDownItems = 5;
            this.customerComboBox.MouseState = MaterialSkin.MouseState.OUT;
            this.customerComboBox.Name = "customerComboBox";
            this.customerComboBox.Size = new System.Drawing.Size(280, 35);
            this.customerComboBox.StartIndex = 0;
            this.customerComboBox.TabIndex = 1;
            this.customerComboBox.UseTallSize = false;
            this.customerComboBox.Validating += new System.ComponentModel.CancelEventHandler(this.customerComboBox_Validating);
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Depth = 0;
            this.nameLabel.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.nameLabel.Location = new System.Drawing.Point(27, 162);
            this.nameLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(124, 19);
            this.nameLabel.TabIndex = 2;
            this.nameLabel.Text = "Campaign Name:";
            // 
            // campaignNameTextBox
            // 
            this.campaignNameTextBox.AnimateReadOnly = false;
            this.campaignNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.campaignNameTextBox.Depth = 0;
            this.campaignNameTextBox.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.campaignNameTextBox.LeadingIcon = null;
            this.campaignNameTextBox.Location = new System.Drawing.Point(211, 150);
            this.campaignNameTextBox.MaxLength = 50;
            this.campaignNameTextBox.MouseState = MaterialSkin.MouseState.OUT;
            this.campaignNameTextBox.Multiline = false;
            this.campaignNameTextBox.Name = "campaignNameTextBox";
            this.campaignNameTextBox.Size = new System.Drawing.Size(320, 36);
            this.campaignNameTextBox.TabIndex = 3;
            this.campaignNameTextBox.Text = "";
            this.campaignNameTextBox.TrailingIcon = null;
            this.campaignNameTextBox.UseTallSize = false;
            this.campaignNameTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.campaignNameTextBox_Validating);
            // 
            // startDateLabel
            // 
            this.startDateLabel.AutoSize = true;
            this.startDateLabel.Depth = 0;
            this.startDateLabel.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.startDateLabel.Location = new System.Drawing.Point(30, 197);
            this.startDateLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.startDateLabel.Name = "startDateLabel";
            this.startDateLabel.Size = new System.Drawing.Size(76, 19);
            this.startDateLabel.TabIndex = 4;
            this.startDateLabel.Text = "Start Date:";
            // 
            // startDatePicker
            // 
            this.startDatePicker.Cursor = System.Windows.Forms.Cursors.Hand;
            this.startDatePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.startDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.startDatePicker.Location = new System.Drawing.Point(222, 197);
            this.startDatePicker.Name = "startDatePicker";
            this.startDatePicker.Size = new System.Drawing.Size(232, 26);
            this.startDatePicker.TabIndex = 5;
            // 
            // hasEndDateCheckBox
            // 
            this.hasEndDateCheckBox.AutoSize = true;
            this.hasEndDateCheckBox.Depth = 0;
            this.hasEndDateCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.hasEndDateCheckBox.Location = new System.Drawing.Point(74, 246);
            this.hasEndDateCheckBox.Margin = new System.Windows.Forms.Padding(0);
            this.hasEndDateCheckBox.MouseLocation = new System.Drawing.Point(-1, -1);
            this.hasEndDateCheckBox.MouseState = MaterialSkin.MouseState.HOVER;
            this.hasEndDateCheckBox.Name = "hasEndDateCheckBox";
            this.hasEndDateCheckBox.ReadOnly = false;
            this.hasEndDateCheckBox.Ripple = true;
            this.hasEndDateCheckBox.Size = new System.Drawing.Size(156, 37);
            this.hasEndDateCheckBox.TabIndex = 6;
            this.hasEndDateCheckBox.Text = "Specify End Date";
            this.hasEndDateCheckBox.UseVisualStyleBackColor = true;
            // 
            // endDatePicker
            // 
            this.endDatePicker.Cursor = System.Windows.Forms.Cursors.Hand;
            this.endDatePicker.Enabled = false;
            this.endDatePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.endDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.endDatePicker.Location = new System.Drawing.Point(315, 257);
            this.endDatePicker.Name = "endDatePicker";
            this.endDatePicker.Size = new System.Drawing.Size(199, 26);
            this.endDatePicker.TabIndex = 7;
            // 
            // impactLabel
            // 
            this.impactLabel.AutoSize = true;
            this.impactLabel.Depth = 0;
            this.impactLabel.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.impactLabel.Location = new System.Drawing.Point(27, 323);
            this.impactLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.impactLabel.Name = "impactLabel";
            this.impactLabel.Size = new System.Drawing.Size(54, 19);
            this.impactLabel.TabIndex = 8;
            this.impactLabel.Text = "Impact:";
            // 
            // impactTextBox
            // 
            this.impactTextBox.AnimateReadOnly = false;
            this.impactTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.impactTextBox.Depth = 0;
            this.impactTextBox.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.impactTextBox.LeadingIcon = null;
            this.impactTextBox.Location = new System.Drawing.Point(157, 318);
            this.impactTextBox.MaxLength = 32767;
            this.impactTextBox.MouseState = MaterialSkin.MouseState.OUT;
            this.impactTextBox.Multiline = false;
            this.impactTextBox.Name = "impactTextBox";
            this.impactTextBox.Size = new System.Drawing.Size(297, 36);
            this.impactTextBox.TabIndex = 9;
            this.impactTextBox.Text = "";
            this.impactTextBox.TrailingIcon = null;
            this.impactTextBox.UseTallSize = false;
            this.impactTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.impactTextBox_Validating);
            // 
            // saveButton
            // 
            this.saveButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.saveButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.saveButton.Depth = 0;
            this.saveButton.HighEmphasis = true;
            this.saveButton.Icon = null;
            this.saveButton.Location = new System.Drawing.Point(143, 405);
            this.saveButton.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.saveButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.saveButton.Name = "saveButton";
            this.saveButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.saveButton.Size = new System.Drawing.Size(64, 36);
            this.saveButton.TabIndex = 10;
            this.saveButton.Text = "Save";
            this.saveButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.saveButton.UseAccentColor = true;
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cancelButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.cancelButton.Depth = 0;
            this.cancelButton.HighEmphasis = false;
            this.cancelButton.Icon = null;
            this.cancelButton.Location = new System.Drawing.Point(303, 405);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.cancelButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.cancelButton.Size = new System.Drawing.Size(77, 36);
            this.cancelButton.TabIndex = 11;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Outlined;
            this.cancelButton.UseAccentColor = false;
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // CustomerCampaignAddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 470);
            this.Controls.Add(this.customerLabel);
            this.Controls.Add(this.customerComboBox);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.campaignNameTextBox);
            this.Controls.Add(this.startDateLabel);
            this.Controls.Add(this.startDatePicker);
            this.Controls.Add(this.hasEndDateCheckBox);
            this.Controls.Add(this.endDatePicker);
            this.Controls.Add(this.impactLabel);
            this.Controls.Add(this.impactTextBox);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.cancelButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomerCampaignAddForm";
            this.Text = "Add Customer Campaign";
            this.Load += new System.EventHandler(this.CustomerCampaignAddForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}

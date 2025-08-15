// File: CustomerClassificationAddForm.Designer.cs
namespace Erp_V1
{
    partial class CustomerClassificationAddForm
    {
        private System.ComponentModel.IContainer components = null;
        private MaterialSkin.Controls.MaterialLabel customerLabel;
        private MaterialSkin.Controls.MaterialComboBox customerComboBox;
        private MaterialSkin.Controls.MaterialLabel tierLabel;
        private MaterialSkin.Controls.MaterialTextBox tierTextBox;
        private MaterialSkin.Controls.MaterialLabel dateLabel;
        private System.Windows.Forms.DateTimePicker assignedDatePicker;
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
            this.tierLabel = new MaterialSkin.Controls.MaterialLabel();
            this.tierTextBox = new MaterialSkin.Controls.MaterialTextBox();
            this.dateLabel = new MaterialSkin.Controls.MaterialLabel();
            this.assignedDatePicker = new System.Windows.Forms.DateTimePicker();
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
            this.customerLabel.Location = new System.Drawing.Point(30, 104);
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
            this.customerComboBox.DropDownHeight = 174;
            this.customerComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.customerComboBox.DropDownWidth = 121;
            this.customerComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.customerComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.customerComboBox.IntegralHeight = false;
            this.customerComboBox.ItemHeight = 43;
            this.customerComboBox.Location = new System.Drawing.Point(160, 88);
            this.customerComboBox.MaxDropDownItems = 4;
            this.customerComboBox.MouseState = MaterialSkin.MouseState.OUT;
            this.customerComboBox.Name = "customerComboBox";
            this.customerComboBox.Size = new System.Drawing.Size(284, 49);
            this.customerComboBox.StartIndex = 0;
            this.customerComboBox.TabIndex = 1;
            this.customerComboBox.Validating += new System.ComponentModel.CancelEventHandler(this.customerComboBox_Validating);
            // 
            // tierLabel
            // 
            this.tierLabel.AutoSize = true;
            this.tierLabel.Depth = 0;
            this.tierLabel.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tierLabel.Location = new System.Drawing.Point(30, 173);
            this.tierLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.tierLabel.Name = "tierLabel";
            this.tierLabel.Size = new System.Drawing.Size(32, 19);
            this.tierLabel.TabIndex = 2;
            this.tierLabel.Text = "Tier:";
            // 
            // tierTextBox
            // 
            this.tierTextBox.AnimateReadOnly = false;
            this.tierTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tierTextBox.Depth = 0;
            this.tierTextBox.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tierTextBox.LeadingIcon = null;
            this.tierTextBox.Location = new System.Drawing.Point(162, 163);
            this.tierTextBox.MaxLength = 50;
            this.tierTextBox.MouseState = MaterialSkin.MouseState.OUT;
            this.tierTextBox.Multiline = false;
            this.tierTextBox.Name = "tierTextBox";
            this.tierTextBox.Size = new System.Drawing.Size(284, 50);
            this.tierTextBox.TabIndex = 3;
            this.tierTextBox.Text = "";
            this.tierTextBox.TrailingIcon = null;
            this.tierTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.tierTextBox_Validating);
            // 
            // dateLabel
            // 
            this.dateLabel.AutoSize = true;
            this.dateLabel.Depth = 0;
            this.dateLabel.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.dateLabel.Location = new System.Drawing.Point(30, 245);
            this.dateLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.dateLabel.Name = "dateLabel";
            this.dateLabel.Size = new System.Drawing.Size(107, 19);
            this.dateLabel.TabIndex = 4;
            this.dateLabel.Text = "Assigned Date:";
            // 
            // assignedDatePicker
            // 
            this.assignedDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.assignedDatePicker.Location = new System.Drawing.Point(174, 244);
            this.assignedDatePicker.Name = "assignedDatePicker";
            this.assignedDatePicker.Size = new System.Drawing.Size(220, 22);
            this.assignedDatePicker.TabIndex = 5;
            // 
            // saveButton
            // 
            this.saveButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.saveButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.saveButton.Depth = 0;
            this.saveButton.HighEmphasis = true;
            this.saveButton.Icon = null;
            this.saveButton.Location = new System.Drawing.Point(99, 317);
            this.saveButton.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.saveButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.saveButton.Name = "saveButton";
            this.saveButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.saveButton.Size = new System.Drawing.Size(64, 36);
            this.saveButton.TabIndex = 6;
            this.saveButton.Text = "Save";
            this.saveButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.saveButton.UseAccentColor = false;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cancelButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.cancelButton.Depth = 0;
            this.cancelButton.HighEmphasis = false;
            this.cancelButton.Icon = null;
            this.cancelButton.Location = new System.Drawing.Point(317, 317);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.cancelButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.cancelButton.Size = new System.Drawing.Size(77, 36);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Outlined;
            this.cancelButton.UseAccentColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // CustomerClassificationAddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(489, 380);
            this.Controls.Add(this.customerLabel);
            this.Controls.Add(this.customerComboBox);
            this.Controls.Add(this.tierLabel);
            this.Controls.Add(this.tierTextBox);
            this.Controls.Add(this.dateLabel);
            this.Controls.Add(this.assignedDatePicker);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.cancelButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomerClassificationAddForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Customer Classification";
            this.Load += new System.EventHandler(this.CustomerClassificationAddForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}

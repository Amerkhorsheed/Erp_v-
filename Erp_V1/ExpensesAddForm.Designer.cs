// File: ExpensesAddForm.Designer.cs
namespace Erp_V1
{
    partial class ExpensesAddForm
    {
        private System.ComponentModel.IContainer components = null;
        private MaterialSkin.Controls.MaterialLabel nameLabel;
        private MaterialSkin.Controls.MaterialTextBox expenseNameTextBox;
        private MaterialSkin.Controls.MaterialLabel categoryLabel;
        private MaterialSkin.Controls.MaterialComboBox categoryComboBox;
        private MaterialSkin.Controls.MaterialLabel dateLabel;
        private System.Windows.Forms.DateTimePicker expenseDatePicker;
        private MaterialSkin.Controls.MaterialLabel amountLabel;
        private MaterialSkin.Controls.MaterialTextBox amountTextBox;
        private MaterialSkin.Controls.MaterialLabel currencyLabel;
        private MaterialSkin.Controls.MaterialComboBox currencyComboBox;
        private MaterialSkin.Controls.MaterialLabel noteLabel;
        private MaterialSkin.Controls.MaterialMultiLineTextBox noteTextBox;
        private MaterialSkin.Controls.MaterialLabel attachmentLabel;
        private MaterialSkin.Controls.MaterialTextBox attachmentTextBox;
        private MaterialSkin.Controls.MaterialButton browseButton;
        private MaterialSkin.Controls.MaterialLabel statusLabel;
        private MaterialSkin.Controls.MaterialComboBox statusComboBox;
        private MaterialSkin.Controls.MaterialButton saveButton;
        private MaterialSkin.Controls.MaterialButton cancelButton;
        private System.Windows.Forms.ErrorProvider errorProvider;

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
            this.components = new System.ComponentModel.Container();
            this.nameLabel = new MaterialSkin.Controls.MaterialLabel();
            this.expenseNameTextBox = new MaterialSkin.Controls.MaterialTextBox();
            this.categoryLabel = new MaterialSkin.Controls.MaterialLabel();
            this.categoryComboBox = new MaterialSkin.Controls.MaterialComboBox();
            this.dateLabel = new MaterialSkin.Controls.MaterialLabel();
            this.expenseDatePicker = new System.Windows.Forms.DateTimePicker();
            this.amountLabel = new MaterialSkin.Controls.MaterialLabel();
            this.amountTextBox = new MaterialSkin.Controls.MaterialTextBox();
            this.currencyLabel = new MaterialSkin.Controls.MaterialLabel();
            this.currencyComboBox = new MaterialSkin.Controls.MaterialComboBox();
            this.noteLabel = new MaterialSkin.Controls.MaterialLabel();
            this.noteTextBox = new MaterialSkin.Controls.MaterialMultiLineTextBox();
            this.attachmentLabel = new MaterialSkin.Controls.MaterialLabel();
            this.attachmentTextBox = new MaterialSkin.Controls.MaterialTextBox();
            this.browseButton = new MaterialSkin.Controls.MaterialButton();
            this.statusLabel = new MaterialSkin.Controls.MaterialLabel();
            this.statusComboBox = new MaterialSkin.Controls.MaterialComboBox();
            this.saveButton = new MaterialSkin.Controls.MaterialButton();
            this.cancelButton = new MaterialSkin.Controls.MaterialButton();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Depth = 0;
            this.nameLabel.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.nameLabel.Location = new System.Drawing.Point(25, 105);
            this.nameLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(110, 19);
            this.nameLabel.TabIndex = 0;
            this.nameLabel.Text = "Expense Name:";
            // 
            // expenseNameTextBox
            // 
            this.expenseNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.expenseNameTextBox.AnimateReadOnly = false;
            this.expenseNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.expenseNameTextBox.Depth = 0;
            this.expenseNameTextBox.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.expenseNameTextBox.LeadingIcon = null;
            this.expenseNameTextBox.Location = new System.Drawing.Point(171, 88);
            this.expenseNameTextBox.MaxLength = 100;
            this.expenseNameTextBox.MouseState = MaterialSkin.MouseState.OUT;
            this.expenseNameTextBox.Multiline = false;
            this.expenseNameTextBox.Name = "expenseNameTextBox";
            this.expenseNameTextBox.Size = new System.Drawing.Size(528, 36);
            this.expenseNameTextBox.TabIndex = 1;
            this.expenseNameTextBox.Text = "";
            this.expenseNameTextBox.TrailingIcon = null;
            this.expenseNameTextBox.UseTallSize = false;
            this.expenseNameTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.expenseNameTextBox_Validating);
            // 
            // categoryLabel
            // 
            this.categoryLabel.AutoSize = true;
            this.categoryLabel.Depth = 0;
            this.categoryLabel.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.categoryLabel.Location = new System.Drawing.Point(25, 155);
            this.categoryLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.categoryLabel.Name = "categoryLabel";
            this.categoryLabel.Size = new System.Drawing.Size(68, 19);
            this.categoryLabel.TabIndex = 2;
            this.categoryLabel.Text = "Category:";
            // 
            // categoryComboBox
            // 
            this.categoryComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.categoryComboBox.AutoResize = false;
            this.categoryComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.categoryComboBox.Depth = 0;
            this.categoryComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.categoryComboBox.DropDownHeight = 174;
            this.categoryComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.categoryComboBox.DropDownWidth = 121;
            this.categoryComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.categoryComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.categoryComboBox.FormattingEnabled = true;
            this.categoryComboBox.IntegralHeight = false;
            this.categoryComboBox.ItemHeight = 43;
            this.categoryComboBox.Location = new System.Drawing.Point(171, 140);
            this.categoryComboBox.MaxDropDownItems = 4;
            this.categoryComboBox.MouseState = MaterialSkin.MouseState.OUT;
            this.categoryComboBox.Name = "categoryComboBox";
            this.categoryComboBox.Size = new System.Drawing.Size(528, 49);
            this.categoryComboBox.StartIndex = 0;
            this.categoryComboBox.TabIndex = 3;
            this.categoryComboBox.Validating += new System.ComponentModel.CancelEventHandler(this.categoryComboBox_Validating);
            // 
            // dateLabel
            // 
            this.dateLabel.AutoSize = true;
            this.dateLabel.Depth = 0;
            this.dateLabel.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.dateLabel.Location = new System.Drawing.Point(25, 213);
            this.dateLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.dateLabel.Name = "dateLabel";
            this.dateLabel.Size = new System.Drawing.Size(101, 19);
            this.dateLabel.TabIndex = 4;
            this.dateLabel.Text = "Expense Date:";
            // 
            // expenseDatePicker
            // 
            this.expenseDatePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.expenseDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.expenseDatePicker.Location = new System.Drawing.Point(207, 213);
            this.expenseDatePicker.Name = "expenseDatePicker";
            this.expenseDatePicker.Size = new System.Drawing.Size(150, 27);
            this.expenseDatePicker.TabIndex = 5;
            // 
            // amountLabel
            // 
            this.amountLabel.AutoSize = true;
            this.amountLabel.Depth = 0;
            this.amountLabel.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.amountLabel.Location = new System.Drawing.Point(25, 263);
            this.amountLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.amountLabel.Name = "amountLabel";
            this.amountLabel.Size = new System.Drawing.Size(61, 19);
            this.amountLabel.TabIndex = 6;
            this.amountLabel.Text = "Amount:";
            // 
            // amountTextBox
            // 
            this.amountTextBox.AnimateReadOnly = false;
            this.amountTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.amountTextBox.Depth = 0;
            this.amountTextBox.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.amountTextBox.LeadingIcon = null;
            this.amountTextBox.Location = new System.Drawing.Point(225, 253);
            this.amountTextBox.MaxLength = 50;
            this.amountTextBox.MouseState = MaterialSkin.MouseState.OUT;
            this.amountTextBox.Multiline = false;
            this.amountTextBox.Name = "amountTextBox";
            this.amountTextBox.Size = new System.Drawing.Size(150, 36);
            this.amountTextBox.TabIndex = 7;
            this.amountTextBox.Text = "";
            this.amountTextBox.TrailingIcon = null;
            this.amountTextBox.UseTallSize = false;
            this.amountTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.amountTextBox_Validating);
            // 
            // currencyLabel
            // 
            this.currencyLabel.AutoSize = true;
            this.currencyLabel.Depth = 0;
            this.currencyLabel.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.currencyLabel.Location = new System.Drawing.Point(424, 253);
            this.currencyLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.currencyLabel.Name = "currencyLabel";
            this.currencyLabel.Size = new System.Drawing.Size(67, 19);
            this.currencyLabel.TabIndex = 8;
            this.currencyLabel.Text = "Currency:";
            // 
            // currencyComboBox
            // 
            this.currencyComboBox.AutoResize = false;
            this.currencyComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.currencyComboBox.Depth = 0;
            this.currencyComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.currencyComboBox.DropDownHeight = 174;
            this.currencyComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.currencyComboBox.DropDownWidth = 121;
            this.currencyComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.currencyComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.currencyComboBox.FormattingEnabled = true;
            this.currencyComboBox.IntegralHeight = false;
            this.currencyComboBox.ItemHeight = 43;
            this.currencyComboBox.Location = new System.Drawing.Point(518, 233);
            this.currencyComboBox.MaxDropDownItems = 4;
            this.currencyComboBox.MouseState = MaterialSkin.MouseState.OUT;
            this.currencyComboBox.Name = "currencyComboBox";
            this.currencyComboBox.Size = new System.Drawing.Size(170, 49);
            this.currencyComboBox.StartIndex = 0;
            this.currencyComboBox.TabIndex = 9;
            // 
            // noteLabel
            // 
            this.noteLabel.AutoSize = true;
            this.noteLabel.Depth = 0;
            this.noteLabel.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.noteLabel.Location = new System.Drawing.Point(25, 313);
            this.noteLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.noteLabel.Name = "noteLabel";
            this.noteLabel.Size = new System.Drawing.Size(38, 19);
            this.noteLabel.TabIndex = 10;
            this.noteLabel.Text = "Note:";
            // 
            // noteTextBox
            // 
            this.noteTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.noteTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.noteTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.noteTextBox.Depth = 0;
            this.noteTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.noteTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.noteTextBox.Location = new System.Drawing.Point(160, 313);
            this.noteTextBox.MouseState = MaterialSkin.MouseState.HOVER;
            this.noteTextBox.Name = "noteTextBox";
            this.noteTextBox.Size = new System.Drawing.Size(528, 96);
            this.noteTextBox.TabIndex = 11;
            this.noteTextBox.Text = "";
            // 
            // attachmentLabel
            // 
            this.attachmentLabel.AutoSize = true;
            this.attachmentLabel.Depth = 0;
            this.attachmentLabel.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.attachmentLabel.Location = new System.Drawing.Point(25, 447);
            this.attachmentLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.attachmentLabel.Name = "attachmentLabel";
            this.attachmentLabel.Size = new System.Drawing.Size(87, 19);
            this.attachmentLabel.TabIndex = 12;
            this.attachmentLabel.Text = "Attachment:";
            // 
            // attachmentTextBox
            // 
            this.attachmentTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.attachmentTextBox.AnimateReadOnly = false;
            this.attachmentTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.attachmentTextBox.Depth = 0;
            this.attachmentTextBox.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.attachmentTextBox.LeadingIcon = null;
            this.attachmentTextBox.Location = new System.Drawing.Point(171, 445);
            this.attachmentTextBox.MaxLength = 255;
            this.attachmentTextBox.MouseState = MaterialSkin.MouseState.OUT;
            this.attachmentTextBox.Multiline = false;
            this.attachmentTextBox.Name = "attachmentTextBox";
            this.attachmentTextBox.Size = new System.Drawing.Size(398, 36);
            this.attachmentTextBox.TabIndex = 13;
            this.attachmentTextBox.Text = "";
            this.attachmentTextBox.TrailingIcon = null;
            this.attachmentTextBox.UseTallSize = false;
            // 
            // browseButton
            // 
            this.browseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseButton.AutoSize = false;
            this.browseButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.browseButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.browseButton.Depth = 0;
            this.browseButton.HighEmphasis = true;
            this.browseButton.Icon = null;
            this.browseButton.Location = new System.Drawing.Point(579, 445);
            this.browseButton.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.browseButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.browseButton.Name = "browseButton";
            this.browseButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.browseButton.Size = new System.Drawing.Size(120, 36);
            this.browseButton.TabIndex = 14;
            this.browseButton.Text = "Browse...";
            this.browseButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.browseButton.UseAccentColor = false;
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Depth = 0;
            this.statusLabel.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.statusLabel.Location = new System.Drawing.Point(25, 524);
            this.statusLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(51, 19);
            this.statusLabel.TabIndex = 15;
            this.statusLabel.Text = "Status:";
            // 
            // statusComboBox
            // 
            this.statusComboBox.AutoResize = false;
            this.statusComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.statusComboBox.Depth = 0;
            this.statusComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.statusComboBox.DropDownHeight = 174;
            this.statusComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.statusComboBox.DropDownWidth = 121;
            this.statusComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.statusComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.statusComboBox.FormattingEnabled = true;
            this.statusComboBox.IntegralHeight = false;
            this.statusComboBox.ItemHeight = 43;
            this.statusComboBox.Location = new System.Drawing.Point(211, 506);
            this.statusComboBox.MaxDropDownItems = 4;
            this.statusComboBox.MouseState = MaterialSkin.MouseState.OUT;
            this.statusComboBox.Name = "statusComboBox";
            this.statusComboBox.Size = new System.Drawing.Size(280, 49);
            this.statusComboBox.StartIndex = 0;
            this.statusComboBox.TabIndex = 16;
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.saveButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.saveButton.Depth = 0;
            this.saveButton.HighEmphasis = true;
            this.saveButton.Icon = null;
            this.saveButton.Location = new System.Drawing.Point(478, 603);
            this.saveButton.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.saveButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.saveButton.Name = "saveButton";
            this.saveButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.saveButton.Size = new System.Drawing.Size(64, 36);
            this.saveButton.TabIndex = 17;
            this.saveButton.Text = "Save";
            this.saveButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.saveButton.UseAccentColor = true;
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cancelButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.cancelButton.Depth = 0;
            this.cancelButton.HighEmphasis = false;
            this.cancelButton.Icon = null;
            this.cancelButton.Location = new System.Drawing.Point(611, 603);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.cancelButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.cancelButton.Size = new System.Drawing.Size(77, 36);
            this.cancelButton.TabIndex = 18;
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
            // ExpensesAddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(718, 658);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.statusComboBox);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.attachmentTextBox);
            this.Controls.Add(this.attachmentLabel);
            this.Controls.Add(this.noteTextBox);
            this.Controls.Add(this.noteLabel);
            this.Controls.Add(this.currencyComboBox);
            this.Controls.Add(this.currencyLabel);
            this.Controls.Add(this.amountTextBox);
            this.Controls.Add(this.amountLabel);
            this.Controls.Add(this.expenseDatePicker);
            this.Controls.Add(this.dateLabel);
            this.Controls.Add(this.categoryComboBox);
            this.Controls.Add(this.categoryLabel);
            this.Controls.Add(this.expenseNameTextBox);
            this.Controls.Add(this.nameLabel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExpensesAddForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Expense";
            this.Load += new System.EventHandler(this.ExpensesAddForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
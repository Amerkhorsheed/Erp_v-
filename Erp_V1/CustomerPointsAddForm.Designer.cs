// File: CustomerPointsAddForm.Designer.cs
namespace Erp_V1
{
    partial class CustomerPointsAddForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label customerLabel;
        private System.Windows.Forms.ComboBox customerComboBox;
        private System.Windows.Forms.Label pointsLabel;
        private System.Windows.Forms.NumericUpDown pointsNumericUpDown;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ErrorProvider errorProvider;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.customerLabel = new System.Windows.Forms.Label();
            this.customerComboBox = new System.Windows.Forms.ComboBox();
            this.pointsLabel = new System.Windows.Forms.Label();
            this.pointsNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pointsNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // customerLabel
            // 
            this.customerLabel.AutoSize = true;
            this.customerLabel.Location = new System.Drawing.Point(20, 20);
            this.customerLabel.Name = "customerLabel";
            this.customerLabel.Size = new System.Drawing.Size(57, 13);
            this.customerLabel.TabIndex = 0;
            this.customerLabel.Text = "Customer:";
            // 
            // customerComboBox
            // 
            this.customerComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.customerComboBox.FormattingEnabled = true;
            this.customerComboBox.Location = new System.Drawing.Point(110, 17);
            this.customerComboBox.Name = "customerComboBox";
            this.customerComboBox.Size = new System.Drawing.Size(220, 21);
            this.customerComboBox.TabIndex = 1;
            this.customerComboBox.Validating += new System.ComponentModel.CancelEventHandler(this.customerComboBox_Validating);
            // 
            // pointsLabel
            // 
            this.pointsLabel.AutoSize = true;
            this.pointsLabel.Location = new System.Drawing.Point(20, 60);
            this.pointsLabel.Name = "pointsLabel";
            this.pointsLabel.Size = new System.Drawing.Size(40, 13);
            this.pointsLabel.TabIndex = 2;
            this.pointsLabel.Text = "Points:";
            // 
            // pointsNumericUpDown
            // 
            this.pointsNumericUpDown.Location = new System.Drawing.Point(110, 58);
            this.pointsNumericUpDown.Maximum = new decimal(new int[] {
            1000000, 0, 0, 0});
            this.pointsNumericUpDown.Minimum = new decimal(new int[] {
            1, 0, 0, 0});
            this.pointsNumericUpDown.Name = "pointsNumericUpDown";
            this.pointsNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.pointsNumericUpDown.TabIndex = 3;
            this.pointsNumericUpDown.Value = new decimal(new int[] {
            1, 0, 0, 0});
            this.pointsNumericUpDown.Validating += new System.ComponentModel.CancelEventHandler(this.pointsNumericUpDown_Validating);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(110, 100);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 25);
            this.saveButton.TabIndex = 4;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(255, 100);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 25);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // CustomerPointsAddForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 145);
            this.Controls.Add(this.customerLabel);
            this.Controls.Add(this.customerComboBox);
            this.Controls.Add(this.pointsLabel);
            this.Controls.Add(this.pointsNumericUpDown);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.cancelButton);
            this.Name = "CustomerPointsAddForm";
            this.Text = "Add Customer Points";
            this.Load += new System.EventHandler(this.CustomerPointsAddForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pointsNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}

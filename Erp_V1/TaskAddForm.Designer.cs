// File: TaskAddForm.Designer.cs
namespace Erp_V1
{
    partial class TaskAddForm
    {
        private System.ComponentModel.IContainer components = null;
        private MaterialSkin.Controls.MaterialTextBox txtTitle;
        private MaterialSkin.Controls.MaterialMultiLineTextBox txtContent;
        private MaterialSkin.Controls.MaterialComboBox cmbFilterDepartment;
        private MaterialSkin.Controls.MaterialComboBox cmbFilterPosition;
        private MaterialSkin.Controls.MaterialButton btnClearFilters;
        private MaterialSkin.Controls.MaterialComboBox cmbEmployee;
        private MaterialSkin.Controls.MaterialComboBox cmbTaskState; // <-- NEW CONTROL
        private MaterialSkin.Controls.MaterialTextBox txtDepartmentDisplay;
        private MaterialSkin.Controls.MaterialTextBox txtPositionDisplay;
        private MaterialSkin.Controls.MaterialLabel lblTitle;
        private MaterialSkin.Controls.MaterialLabel lblContent;
        private MaterialSkin.Controls.MaterialLabel lblFilter;
        private MaterialSkin.Controls.MaterialLabel lblEmployee;
        private MaterialSkin.Controls.MaterialLabel lblTaskState; // <-- NEW LABEL
        private MaterialSkin.Controls.MaterialButton btnSave;
        private MaterialSkin.Controls.MaterialButton btnCancel;
        private System.Windows.Forms.ErrorProvider errorProvider;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtTitle = new MaterialSkin.Controls.MaterialTextBox();
            this.txtContent = new MaterialSkin.Controls.MaterialMultiLineTextBox();
            this.cmbFilterDepartment = new MaterialSkin.Controls.MaterialComboBox();
            this.cmbFilterPosition = new MaterialSkin.Controls.MaterialComboBox();
            this.btnClearFilters = new MaterialSkin.Controls.MaterialButton();
            this.cmbEmployee = new MaterialSkin.Controls.MaterialComboBox();
            this.txtDepartmentDisplay = new MaterialSkin.Controls.MaterialTextBox();
            this.txtPositionDisplay = new MaterialSkin.Controls.MaterialTextBox();
            this.lblTitle = new MaterialSkin.Controls.MaterialLabel();
            this.lblContent = new MaterialSkin.Controls.MaterialLabel();
            this.lblFilter = new MaterialSkin.Controls.MaterialLabel();
            this.lblEmployee = new MaterialSkin.Controls.MaterialLabel();
            this.btnSave = new MaterialSkin.Controls.MaterialButton();
            this.btnCancel = new MaterialSkin.Controls.MaterialButton();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.cmbTaskState = new MaterialSkin.Controls.MaterialComboBox();
            this.lblTaskState = new MaterialSkin.Controls.MaterialLabel();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // txtTitle
            // 
            this.txtTitle.AnimateReadOnly = false;
            this.txtTitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTitle.Depth = 0;
            this.txtTitle.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtTitle.LeadingIcon = null;
            this.txtTitle.Location = new System.Drawing.Point(160, 99);
            this.txtTitle.MaxLength = 100;
            this.txtTitle.MouseState = MaterialSkin.MouseState.OUT;
            this.txtTitle.Multiline = false;
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(400, 36);
            this.txtTitle.TabIndex = 1;
            this.txtTitle.Text = "";
            this.txtTitle.TrailingIcon = null;
            this.txtTitle.UseTallSize = false;
            this.txtTitle.Validating += new System.ComponentModel.CancelEventHandler(this.txtTitle_Validating);
            // 
            // txtContent
            // 
            this.txtContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtContent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtContent.Depth = 0;
            this.txtContent.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtContent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtContent.Hint = "Task Description";
            this.txtContent.Location = new System.Drawing.Point(160, 164);
            this.txtContent.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new System.Drawing.Size(400, 96);
            this.txtContent.TabIndex = 3;
            this.txtContent.Text = "";
            // 
            // cmbFilterDepartment
            // 
            this.cmbFilterDepartment.AutoResize = false;
            this.cmbFilterDepartment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbFilterDepartment.Depth = 0;
            this.cmbFilterDepartment.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbFilterDepartment.DropDownHeight = 174;
            this.cmbFilterDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterDepartment.DropDownWidth = 121;
            this.cmbFilterDepartment.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.cmbFilterDepartment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbFilterDepartment.FormattingEnabled = true;
            this.cmbFilterDepartment.Hint = "Filter by Department";
            this.cmbFilterDepartment.IntegralHeight = false;
            this.cmbFilterDepartment.ItemHeight = 43;
            this.cmbFilterDepartment.Location = new System.Drawing.Point(160, 297);
            this.cmbFilterDepartment.MaxDropDownItems = 4;
            this.cmbFilterDepartment.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbFilterDepartment.Name = "cmbFilterDepartment";
            this.cmbFilterDepartment.Size = new System.Drawing.Size(234, 49);
            this.cmbFilterDepartment.StartIndex = 0;
            this.cmbFilterDepartment.TabIndex = 5;
            this.cmbFilterDepartment.SelectedIndexChanged += new System.EventHandler(this.cmbFilterDepartment_SelectedIndexChanged);
            // 
            // cmbFilterPosition
            // 
            this.cmbFilterPosition.AutoResize = false;
            this.cmbFilterPosition.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbFilterPosition.Depth = 0;
            this.cmbFilterPosition.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbFilterPosition.DropDownHeight = 174;
            this.cmbFilterPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterPosition.DropDownWidth = 121;
            this.cmbFilterPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.cmbFilterPosition.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbFilterPosition.FormattingEnabled = true;
            this.cmbFilterPosition.Hint = "Filter by Position";
            this.cmbFilterPosition.IntegralHeight = false;
            this.cmbFilterPosition.ItemHeight = 43;
            this.cmbFilterPosition.Location = new System.Drawing.Point(440, 297);
            this.cmbFilterPosition.MaxDropDownItems = 4;
            this.cmbFilterPosition.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbFilterPosition.Name = "cmbFilterPosition";
            this.cmbFilterPosition.Size = new System.Drawing.Size(222, 49);
            this.cmbFilterPosition.StartIndex = 0;
            this.cmbFilterPosition.TabIndex = 6;
            this.cmbFilterPosition.SelectedIndexChanged += new System.EventHandler(this.cmbFilterPosition_SelectedIndexChanged);
            // 
            // btnClearFilters
            // 
            this.btnClearFilters.AutoSize = false;
            this.btnClearFilters.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnClearFilters.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnClearFilters.Depth = 0;
            this.btnClearFilters.HighEmphasis = false;
            this.btnClearFilters.Icon = null;
            this.btnClearFilters.Location = new System.Drawing.Point(687, 302);
            this.btnClearFilters.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnClearFilters.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnClearFilters.Name = "btnClearFilters";
            this.btnClearFilters.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnClearFilters.Size = new System.Drawing.Size(80, 36);
            this.btnClearFilters.TabIndex = 7;
            this.btnClearFilters.Text = "Clear";
            this.btnClearFilters.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Text;
            this.btnClearFilters.UseAccentColor = false;
            this.btnClearFilters.UseVisualStyleBackColor = true;
            this.btnClearFilters.Click += new System.EventHandler(this.btnClearFilters_Click);
            // 
            // cmbEmployee
            // 
            this.cmbEmployee.AutoResize = false;
            this.cmbEmployee.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbEmployee.Depth = 0;
            this.cmbEmployee.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbEmployee.DropDownHeight = 174;
            this.cmbEmployee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEmployee.DropDownWidth = 121;
            this.cmbEmployee.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.cmbEmployee.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbEmployee.FormattingEnabled = true;
            this.cmbEmployee.IntegralHeight = false;
            this.cmbEmployee.ItemHeight = 43;
            this.cmbEmployee.Location = new System.Drawing.Point(160, 382);
            this.cmbEmployee.MaxDropDownItems = 4;
            this.cmbEmployee.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbEmployee.Name = "cmbEmployee";
            this.cmbEmployee.Size = new System.Drawing.Size(250, 49);
            this.cmbEmployee.StartIndex = 0;
            this.cmbEmployee.TabIndex = 9;
            this.cmbEmployee.SelectedIndexChanged += new System.EventHandler(this.cmbEmployee_SelectedIndexChanged);
            this.cmbEmployee.Validating += new System.ComponentModel.CancelEventHandler(this.cmbEmployee_Validating);
            // 
            // txtDepartmentDisplay
            // 
            this.txtDepartmentDisplay.AnimateReadOnly = false;
            this.txtDepartmentDisplay.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDepartmentDisplay.Depth = 0;
            this.txtDepartmentDisplay.Enabled = false;
            this.txtDepartmentDisplay.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtDepartmentDisplay.Hint = "Selected Employee\'s Department";
            this.txtDepartmentDisplay.LeadingIcon = null;
            this.txtDepartmentDisplay.Location = new System.Drawing.Point(530, 382);
            this.txtDepartmentDisplay.MaxLength = 50;
            this.txtDepartmentDisplay.MouseState = MaterialSkin.MouseState.OUT;
            this.txtDepartmentDisplay.Multiline = false;
            this.txtDepartmentDisplay.Name = "txtDepartmentDisplay";
            this.txtDepartmentDisplay.ReadOnly = true;
            this.txtDepartmentDisplay.Size = new System.Drawing.Size(250, 36);
            this.txtDepartmentDisplay.TabIndex = 10;
            this.txtDepartmentDisplay.Text = "";
            this.txtDepartmentDisplay.TrailingIcon = null;
            this.txtDepartmentDisplay.UseTallSize = false;
            // 
            // txtPositionDisplay
            // 
            this.txtPositionDisplay.AnimateReadOnly = false;
            this.txtPositionDisplay.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPositionDisplay.Depth = 0;
            this.txtPositionDisplay.Enabled = false;
            this.txtPositionDisplay.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtPositionDisplay.Hint = "Selected Employee\'s Position";
            this.txtPositionDisplay.LeadingIcon = null;
            this.txtPositionDisplay.Location = new System.Drawing.Point(530, 432);
            this.txtPositionDisplay.MaxLength = 50;
            this.txtPositionDisplay.MouseState = MaterialSkin.MouseState.OUT;
            this.txtPositionDisplay.Multiline = false;
            this.txtPositionDisplay.Name = "txtPositionDisplay";
            this.txtPositionDisplay.ReadOnly = true;
            this.txtPositionDisplay.Size = new System.Drawing.Size(250, 36);
            this.txtPositionDisplay.TabIndex = 11;
            this.txtPositionDisplay.Text = "";
            this.txtPositionDisplay.TrailingIcon = null;
            this.txtPositionDisplay.UseTallSize = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Depth = 0;
            this.lblTitle.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblTitle.Location = new System.Drawing.Point(40, 116);
            this.lblTitle.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(75, 19);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Task Title:";
            // 
            // lblContent
            // 
            this.lblContent.AutoSize = true;
            this.lblContent.Depth = 0;
            this.lblContent.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblContent.Location = new System.Drawing.Point(40, 177);
            this.lblContent.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblContent.Name = "lblContent";
            this.lblContent.Size = new System.Drawing.Size(85, 19);
            this.lblContent.TabIndex = 2;
            this.lblContent.Text = "Description:";
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Depth = 0;
            this.lblFilter.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblFilter.Location = new System.Drawing.Point(40, 312);
            this.lblFilter.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(62, 19);
            this.lblFilter.TabIndex = 4;
            this.lblFilter.Text = "Filter By:";
            // 
            // lblEmployee
            // 
            this.lblEmployee.AutoSize = true;
            this.lblEmployee.Depth = 0;
            this.lblEmployee.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblEmployee.Location = new System.Drawing.Point(40, 399);
            this.lblEmployee.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblEmployee.Name = "lblEmployee";
            this.lblEmployee.Size = new System.Drawing.Size(93, 19);
            this.lblEmployee.TabIndex = 8;
            this.lblEmployee.Text = "Assigned To:";
            // 
            // btnSave
            // 
            this.btnSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSave.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnSave.Depth = 0;
            this.btnSave.HighEmphasis = true;
            this.btnSave.Icon = null;
            this.btnSave.Location = new System.Drawing.Point(232, 610);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnSave.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnSave.Name = "btnSave";
            this.btnSave.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnSave.Size = new System.Drawing.Size(64, 36);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Save";
            this.btnSave.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnSave.UseAccentColor = true;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCancel.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnCancel.Depth = 0;
            this.btnCancel.HighEmphasis = false;
            this.btnCancel.Icon = null;
            this.btnCancel.Location = new System.Drawing.Point(352, 610);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnCancel.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnCancel.Size = new System.Drawing.Size(77, 36);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Outlined;
            this.btnCancel.UseAccentColor = false;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // cmbTaskState
            // 
            this.cmbTaskState.AutoResize = false;
            this.cmbTaskState.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbTaskState.Depth = 0;
            this.cmbTaskState.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbTaskState.DropDownHeight = 174;
            this.cmbTaskState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTaskState.DropDownWidth = 121;
            this.cmbTaskState.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.cmbTaskState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbTaskState.FormattingEnabled = true;
            this.cmbTaskState.IntegralHeight = false;
            this.cmbTaskState.ItemHeight = 43;
            this.cmbTaskState.Location = new System.Drawing.Point(160, 477);
            this.cmbTaskState.MaxDropDownItems = 4;
            this.cmbTaskState.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbTaskState.Name = "cmbTaskState";
            this.cmbTaskState.Size = new System.Drawing.Size(250, 49);
            this.cmbTaskState.StartIndex = 0;
            this.cmbTaskState.TabIndex = 14;
            // 
            // lblTaskState
            // 
            this.lblTaskState.AutoSize = true;
            this.lblTaskState.Depth = 0;
            this.lblTaskState.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblTaskState.Location = new System.Drawing.Point(40, 492);
            this.lblTaskState.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblTaskState.Name = "lblTaskState";
            this.lblTaskState.Size = new System.Drawing.Size(90, 19);
            this.lblTaskState.TabIndex = 15;
            this.lblTaskState.Text = "Task Status:";
            // 
            // TaskAddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 677);
            this.Controls.Add(this.lblTaskState);
            this.Controls.Add(this.cmbTaskState);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblEmployee);
            this.Controls.Add(this.lblFilter);
            this.Controls.Add(this.lblContent);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.txtPositionDisplay);
            this.Controls.Add(this.txtDepartmentDisplay);
            this.Controls.Add(this.cmbEmployee);
            this.Controls.Add(this.btnClearFilters);
            this.Controls.Add(this.cmbFilterPosition);
            this.Controls.Add(this.cmbFilterDepartment);
            this.Controls.Add(this.txtContent);
            this.Controls.Add(this.txtTitle);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TaskAddForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add New Task";
            this.Load += new System.EventHandler(this.TaskAddForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
    }
}
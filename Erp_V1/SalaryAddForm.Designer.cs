// File: SalaryAddForm.Designer.cs
namespace Erp_V1
{
    partial class SalaryAddForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.DataGridView dgvEmployees;
        private MaterialSkin.Controls.MaterialTextBox txtUserNo;
        private MaterialSkin.Controls.MaterialTextBox txtName;
        private MaterialSkin.Controls.MaterialTextBox txtSurname;
        private MaterialSkin.Controls.MaterialTextBox txtAmount;
        private MaterialSkin.Controls.MaterialTextBox txtYear;
        private MaterialSkin.Controls.MaterialComboBox cmbMonth;
        private MaterialSkin.Controls.MaterialLabel lblAmount;
        private MaterialSkin.Controls.MaterialLabel lblYear;
        private MaterialSkin.Controls.MaterialLabel lblMonth;
        private MaterialSkin.Controls.MaterialButton btnSave;
        private MaterialSkin.Controls.MaterialButton btnClose;

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
            this.panelLeft = new System.Windows.Forms.Panel();
            this.dgvEmployees = new System.Windows.Forms.DataGridView();
            this.panelRight = new System.Windows.Forms.Panel();
            this.btnClose = new MaterialSkin.Controls.MaterialButton();
            this.btnSave = new MaterialSkin.Controls.MaterialButton();
            this.lblMonth = new MaterialSkin.Controls.MaterialLabel();
            this.lblYear = new MaterialSkin.Controls.MaterialLabel();
            this.lblAmount = new MaterialSkin.Controls.MaterialLabel();
            this.cmbMonth = new MaterialSkin.Controls.MaterialComboBox();
            this.txtYear = new MaterialSkin.Controls.MaterialTextBox();
            this.txtAmount = new MaterialSkin.Controls.MaterialTextBox();
            this.txtSurname = new MaterialSkin.Controls.MaterialTextBox();
            this.txtName = new MaterialSkin.Controls.MaterialTextBox();
            this.txtUserNo = new MaterialSkin.Controls.MaterialTextBox();
            this.panelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployees)).BeginInit();
            this.panelRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.dgvEmployees);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(3, 64);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(550, 560);
            this.panelLeft.TabIndex = 0;
            // 
            // dgvEmployees
            // 
            this.dgvEmployees.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEmployees.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEmployees.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEmployees.Location = new System.Drawing.Point(0, 0);
            this.dgvEmployees.MultiSelect = false;
            this.dgvEmployees.Name = "dgvEmployees";
            this.dgvEmployees.ReadOnly = true;
            this.dgvEmployees.RowHeadersWidth = 51;
            this.dgvEmployees.RowTemplate.Height = 24;
            this.dgvEmployees.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEmployees.Size = new System.Drawing.Size(550, 560);
            this.dgvEmployees.TabIndex = 0;
            this.dgvEmployees.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEmployees_RowEnter);
            // 
            // panelRight
            // 
            this.panelRight.Controls.Add(this.btnClose);
            this.panelRight.Controls.Add(this.btnSave);
            this.panelRight.Controls.Add(this.lblMonth);
            this.panelRight.Controls.Add(this.lblYear);
            this.panelRight.Controls.Add(this.lblAmount);
            this.panelRight.Controls.Add(this.cmbMonth);
            this.panelRight.Controls.Add(this.txtYear);
            this.panelRight.Controls.Add(this.txtAmount);
            this.panelRight.Controls.Add(this.txtSurname);
            this.panelRight.Controls.Add(this.txtName);
            this.panelRight.Controls.Add(this.txtUserNo);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(553, 64);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(444, 560);
            this.panelRight.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnClose.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnClose.Depth = 0;
            this.btnClose.HighEmphasis = false;
            this.btnClose.Icon = null;
            this.btnClose.Location = new System.Drawing.Point(289, 458);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnClose.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnClose.Name = "btnClose";
            this.btnClose.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnClose.Size = new System.Drawing.Size(66, 36);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "Close";
            this.btnClose.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Outlined;
            this.btnClose.UseAccentColor = false;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSave.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnSave.Depth = 0;
            this.btnSave.HighEmphasis = true;
            this.btnSave.Icon = null;
            this.btnSave.Location = new System.Drawing.Point(159, 458);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnSave.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnSave.Name = "btnSave";
            this.btnSave.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnSave.Size = new System.Drawing.Size(64, 36);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Save";
            this.btnSave.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnSave.UseAccentColor = true;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblMonth
            // 
            this.lblMonth.AutoSize = true;
            this.lblMonth.Depth = 0;
            this.lblMonth.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblMonth.Location = new System.Drawing.Point(49, 383);
            this.lblMonth.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Size = new System.Drawing.Size(51, 19);
            this.lblMonth.TabIndex = 8;
            this.lblMonth.Text = "Month:";
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.Depth = 0;
            this.lblYear.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblYear.Location = new System.Drawing.Point(49, 318);
            this.lblYear.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(37, 19);
            this.lblYear.TabIndex = 7;
            this.lblYear.Text = "Year:";
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Depth = 0;
            this.lblAmount.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblAmount.Location = new System.Drawing.Point(49, 253);
            this.lblAmount.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(61, 19);
            this.lblAmount.TabIndex = 6;
            this.lblAmount.Text = "Amount:";
            // 
            // cmbMonth
            // 
            this.cmbMonth.AutoResize = false;
            this.cmbMonth.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbMonth.Depth = 0;
            this.cmbMonth.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbMonth.DropDownHeight = 174;
            this.cmbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMonth.DropDownWidth = 121;
            this.cmbMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.cmbMonth.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbMonth.FormattingEnabled = true;
            this.cmbMonth.IntegralHeight = false;
            this.cmbMonth.ItemHeight = 43;
            this.cmbMonth.Location = new System.Drawing.Point(129, 373);
            this.cmbMonth.MaxDropDownItems = 4;
            this.cmbMonth.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbMonth.Name = "cmbMonth";
            this.cmbMonth.Size = new System.Drawing.Size(200, 49);
            this.cmbMonth.StartIndex = 0;
            this.cmbMonth.TabIndex = 5;
            // 
            // txtYear
            // 
            this.txtYear.AnimateReadOnly = false;
            this.txtYear.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtYear.Depth = 0;
            this.txtYear.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtYear.LeadingIcon = null;
            this.txtYear.Location = new System.Drawing.Point(129, 308);
            this.txtYear.MaxLength = 4;
            this.txtYear.MouseState = MaterialSkin.MouseState.OUT;
            this.txtYear.Multiline = false;
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(200, 36);
            this.txtYear.TabIndex = 4;
            this.txtYear.Text = "";
            this.txtYear.TrailingIcon = null;
            this.txtYear.UseTallSize = false;
            // 
            // txtAmount
            // 
            this.txtAmount.AnimateReadOnly = false;
            this.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAmount.Depth = 0;
            this.txtAmount.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtAmount.LeadingIcon = null;
            this.txtAmount.Location = new System.Drawing.Point(129, 243);
            this.txtAmount.MaxLength = 50;
            this.txtAmount.MouseState = MaterialSkin.MouseState.OUT;
            this.txtAmount.Multiline = false;
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(200, 36);
            this.txtAmount.TabIndex = 3;
            this.txtAmount.Text = "";
            this.txtAmount.TrailingIcon = null;
            this.txtAmount.UseTallSize = false;
            // 
            // txtSurname
            // 
            this.txtSurname.AnimateReadOnly = false;
            this.txtSurname.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSurname.Depth = 0;
            this.txtSurname.Enabled = false;
            this.txtSurname.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtSurname.Hint = "Last Name";
            this.txtSurname.LeadingIcon = null;
            this.txtSurname.Location = new System.Drawing.Point(75, 178);
            this.txtSurname.MaxLength = 50;
            this.txtSurname.MouseState = MaterialSkin.MouseState.OUT;
            this.txtSurname.Multiline = false;
            this.txtSurname.Name = "txtSurname";
            this.txtSurname.Size = new System.Drawing.Size(280, 36);
            this.txtSurname.TabIndex = 2;
            this.txtSurname.Text = "";
            this.txtSurname.TrailingIcon = null;
            this.txtSurname.UseTallSize = false;
            // 
            // txtName
            // 
            this.txtName.AnimateReadOnly = false;
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtName.Depth = 0;
            this.txtName.Enabled = false;
            this.txtName.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtName.Hint = "First Name";
            this.txtName.LeadingIcon = null;
            this.txtName.Location = new System.Drawing.Point(75, 109);
            this.txtName.MaxLength = 50;
            this.txtName.MouseState = MaterialSkin.MouseState.OUT;
            this.txtName.Multiline = false;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(280, 36);
            this.txtName.TabIndex = 1;
            this.txtName.Text = "";
            this.txtName.TrailingIcon = null;
            this.txtName.UseTallSize = false;
            // 
            // txtUserNo
            // 
            this.txtUserNo.AnimateReadOnly = false;
            this.txtUserNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUserNo.Depth = 0;
            this.txtUserNo.Enabled = false;
            this.txtUserNo.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtUserNo.Hint = "User No";
            this.txtUserNo.LeadingIcon = null;
            this.txtUserNo.Location = new System.Drawing.Point(75, 41);
            this.txtUserNo.MaxLength = 50;
            this.txtUserNo.MouseState = MaterialSkin.MouseState.OUT;
            this.txtUserNo.Multiline = false;
            this.txtUserNo.Name = "txtUserNo";
            this.txtUserNo.Size = new System.Drawing.Size(280, 36);
            this.txtUserNo.TabIndex = 0;
            this.txtUserNo.Text = "";
            this.txtUserNo.TrailingIcon = null;
            this.txtUserNo.UseTallSize = false;
            // 
            // SalaryAddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 627);
            this.Controls.Add(this.panelRight);
            this.Controls.Add(this.panelLeft);
            this.Name = "SalaryAddForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add New Salary";
            this.Load += new System.EventHandler(this.SalaryAddForm_Load);
            this.panelLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployees)).EndInit();
            this.panelRight.ResumeLayout(false);
            this.panelRight.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion
    }
}
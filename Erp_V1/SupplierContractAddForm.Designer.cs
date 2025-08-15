// File: SupplierContractAddForm.Designer.cs
using MaterialSkin.Controls; // Make sure this using directive is present if you're using MaterialSkin controls
using System.ComponentModel;
using System.Drawing; // Added for Point and Size
using System.Windows.Forms;

namespace Erp_V1
{
    partial class SupplierContractAddForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblNumber = new MaterialSkin.Controls.MaterialLabel();
            this.txtNumber = new MaterialSkin.Controls.MaterialTextBox();
            this.lblSupplier = new MaterialSkin.Controls.MaterialLabel();
            this.cbSupplier = new MaterialSkin.Controls.MaterialComboBox();
            this.lblStart = new MaterialSkin.Controls.MaterialLabel();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.lblEnd = new MaterialSkin.Controls.MaterialLabel();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.lblRenewal = new MaterialSkin.Controls.MaterialLabel();
            this.dtpRenewal = new System.Windows.Forms.DateTimePicker();
            this.lblTerms = new MaterialSkin.Controls.MaterialLabel();
            this.txtTerms = new System.Windows.Forms.TextBox();
            this.btnSave = new MaterialSkin.Controls.MaterialButton();
            this.btnCancel = new MaterialSkin.Controls.MaterialButton();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.chkEndDate = new System.Windows.Forms.CheckBox();
            this.chkRenewalDate = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNumber
            // 
            this.lblNumber.Depth = 0;
            this.lblNumber.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblNumber.Location = new System.Drawing.Point(30, 80);
            this.lblNumber.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblNumber.Name = "lblNumber";
            this.lblNumber.Size = new System.Drawing.Size(100, 23);
            this.lblNumber.TabIndex = 0;
            this.lblNumber.Text = "Contract No.:";
            // 
            // txtNumber
            // 
            this.txtNumber.AnimateReadOnly = false;
            this.txtNumber.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNumber.Depth = 0;
            this.txtNumber.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtNumber.LeadingIcon = null;
            this.txtNumber.Location = new System.Drawing.Point(150, 75);
            this.txtNumber.MaxLength = 50;
            this.txtNumber.MouseState = MaterialSkin.MouseState.OUT;
            this.txtNumber.Multiline = false;
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(300, 36);
            this.txtNumber.TabIndex = 1;
            this.txtNumber.Text = "";
            this.txtNumber.TrailingIcon = null;
            this.txtNumber.UseTallSize = false;
            this.txtNumber.Validating += new System.ComponentModel.CancelEventHandler(this.txtNumber_Validating);
            // 
            // lblSupplier
            // 
            this.lblSupplier.Depth = 0;
            this.lblSupplier.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblSupplier.Location = new System.Drawing.Point(30, 135);
            this.lblSupplier.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(100, 23);
            this.lblSupplier.TabIndex = 2;
            this.lblSupplier.Text = "Supplier:";
            // 
            // cbSupplier
            // 
            this.cbSupplier.AutoResize = false;
            this.cbSupplier.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cbSupplier.Depth = 0;
            this.cbSupplier.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbSupplier.DropDownHeight = 174;
            this.cbSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSupplier.DropDownWidth = 121;
            this.cbSupplier.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.cbSupplier.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cbSupplier.FormattingEnabled = true;
            this.cbSupplier.IntegralHeight = false;
            this.cbSupplier.ItemHeight = 43;
            this.cbSupplier.Location = new System.Drawing.Point(150, 125);
            this.cbSupplier.MaxDropDownItems = 4;
            this.cbSupplier.MouseState = MaterialSkin.MouseState.OUT;
            this.cbSupplier.Name = "cbSupplier";
            this.cbSupplier.Size = new System.Drawing.Size(300, 49);
            this.cbSupplier.StartIndex = 0;
            this.cbSupplier.TabIndex = 3;
            this.cbSupplier.Validating += new System.ComponentModel.CancelEventHandler(this.cbSupplier_Validating);
            // 
            // lblStart
            // 
            this.lblStart.Depth = 0;
            this.lblStart.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblStart.Location = new System.Drawing.Point(30, 190);
            this.lblStart.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(100, 23);
            this.lblStart.TabIndex = 4;
            this.lblStart.Text = "Start Date:";
            // 
            // dtpStart
            // 
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStart.Location = new System.Drawing.Point(150, 185);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(200, 22);
            this.dtpStart.TabIndex = 5;
            // 
            // lblEnd
            // 
            this.lblEnd.Depth = 0;
            this.lblEnd.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblEnd.Location = new System.Drawing.Point(30, 240);
            this.lblEnd.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(100, 23);
            this.lblEnd.TabIndex = 6;
            this.lblEnd.Text = "End Date:";
            // 
            // dtpEnd
            // 
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEnd.Location = new System.Drawing.Point(150, 235);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(200, 22);
            this.dtpEnd.TabIndex = 7;
            // 
            // lblRenewal
            // 
            this.lblRenewal.Depth = 0;
            this.lblRenewal.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblRenewal.Location = new System.Drawing.Point(30, 287);
            this.lblRenewal.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblRenewal.Name = "lblRenewal";
            this.lblRenewal.Size = new System.Drawing.Size(100, 42);
            this.lblRenewal.TabIndex = 9;
            this.lblRenewal.Text = "Renewal Date:";
            // 
            // dtpRenewal
            // 
            this.dtpRenewal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpRenewal.Location = new System.Drawing.Point(150, 285);
            this.dtpRenewal.Name = "dtpRenewal";
            this.dtpRenewal.Size = new System.Drawing.Size(200, 22);
            this.dtpRenewal.TabIndex = 10;
            // 
            // lblTerms
            // 
            this.lblTerms.Depth = 0;
            this.lblTerms.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblTerms.Location = new System.Drawing.Point(30, 362);
            this.lblTerms.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblTerms.Name = "lblTerms";
            this.lblTerms.Size = new System.Drawing.Size(100, 23);
            this.lblTerms.TabIndex = 12;
            this.lblTerms.Text = "Terms:";
            // 
            // txtTerms
            // 
            this.txtTerms.Location = new System.Drawing.Point(150, 335);
            this.txtTerms.Multiline = true;
            this.txtTerms.Name = "txtTerms";
            this.txtTerms.Size = new System.Drawing.Size(300, 100);
            this.txtTerms.TabIndex = 13;
            this.txtTerms.Validating += new System.ComponentModel.CancelEventHandler(this.txtTerms_Validating);
            // 
            // btnSave
            // 
            this.btnSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSave.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnSave.Depth = 0;
            this.btnSave.HighEmphasis = true;
            this.btnSave.Icon = null;
            this.btnSave.Location = new System.Drawing.Point(150, 460);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnSave.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnSave.Name = "btnSave";
            this.btnSave.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnSave.Size = new System.Drawing.Size(64, 36);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "Save";
            this.btnSave.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnSave.UseAccentColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCancel.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnCancel.Depth = 0;
            this.btnCancel.HighEmphasis = true;
            this.btnCancel.Icon = null;
            this.btnCancel.Location = new System.Drawing.Point(300, 460);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnCancel.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnCancel.Size = new System.Drawing.Size(77, 36);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnCancel.UseAccentColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // chkEndDate
            // 
            this.chkEndDate.AutoSize = true;
            this.chkEndDate.Location = new System.Drawing.Point(360, 238);
            this.chkEndDate.Name = "chkEndDate";
            this.chkEndDate.Size = new System.Drawing.Size(18, 17);
            this.chkEndDate.TabIndex = 8;
            this.chkEndDate.UseVisualStyleBackColor = true;
            this.chkEndDate.CheckedChanged += new System.EventHandler(this.chkEndDate_CheckedChanged);
            // 
            // chkRenewalDate
            // 
            this.chkRenewalDate.AutoSize = true;
            this.chkRenewalDate.Location = new System.Drawing.Point(360, 288);
            this.chkRenewalDate.Name = "chkRenewalDate";
            this.chkRenewalDate.Size = new System.Drawing.Size(18, 17);
            this.chkRenewalDate.TabIndex = 11;
            this.chkRenewalDate.UseVisualStyleBackColor = true;
            this.chkRenewalDate.CheckedChanged += new System.EventHandler(this.chkRenewalDate_CheckedChanged);
            // 
            // SupplierContractAddForm
            // 
            this.ClientSize = new System.Drawing.Size(500, 520);
            this.Controls.Add(this.chkRenewalDate);
            this.Controls.Add(this.chkEndDate);
            this.Controls.Add(this.cbSupplier);
            this.Controls.Add(this.lblSupplier);
            this.Controls.Add(this.lblNumber);
            this.Controls.Add(this.txtNumber);
            this.Controls.Add(this.lblStart);
            this.Controls.Add(this.dtpStart);
            this.Controls.Add(this.lblEnd);
            this.Controls.Add(this.dtpEnd);
            this.Controls.Add(this.lblRenewal);
            this.Controls.Add(this.dtpRenewal);
            this.Controls.Add(this.lblTerms);
            this.Controls.Add(this.txtTerms);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SupplierContractAddForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Supplier Contract";
            this.Load += new System.EventHandler(this.SupplierContractAddForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialLabel lblNumber;
        private MaterialTextBox txtNumber;
        private MaterialLabel lblStart;
        private DateTimePicker dtpStart;
        private MaterialLabel lblEnd;
        private DateTimePicker dtpEnd;
        private MaterialLabel lblRenewal;
        private DateTimePicker dtpRenewal;
        private MaterialLabel lblTerms;
        private TextBox txtTerms;
        private MaterialButton btnSave;
        private MaterialButton btnCancel;
        private ErrorProvider errorProvider;
        private CheckBox chkEndDate;
        private CheckBox chkRenewalDate;
        private MaterialLabel lblSupplier; // Declaration for the new supplier label
        private MaterialComboBox cbSupplier; // Declaration for the new supplier ComboBox
    }
}
// File: SupplyScheduleAddForm.Designer.cs
using MaterialSkin.Controls;
using System.ComponentModel;
using System.Windows.Forms;

namespace Erp_V1
{
    partial class SupplyScheduleAddForm
    {
        private IContainer components = null;
        private MaterialLabel lblSupplier;
        private MaterialComboBox cmbSupplier;
        private MaterialLabel lblContract;
        private MaterialComboBox cmbContract;
        private MaterialLabel lblExpected;
        private DateTimePicker dtpExpected;
        private MaterialLabel lblQuantity;
        private NumericUpDown numQuantity;
        private MaterialLabel lblStatus;
        private MaterialTextBox txtStatus;
        private MaterialButton btnSave;
        private MaterialButton btnCancel;
        private ErrorProvider errorProvider;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblSupplier = new MaterialSkin.Controls.MaterialLabel();
            this.cmbSupplier = new MaterialSkin.Controls.MaterialComboBox();
            this.lblContract = new MaterialSkin.Controls.MaterialLabel();
            this.cmbContract = new MaterialSkin.Controls.MaterialComboBox();
            this.lblExpected = new MaterialSkin.Controls.MaterialLabel();
            this.dtpExpected = new System.Windows.Forms.DateTimePicker();
            this.lblQuantity = new MaterialSkin.Controls.MaterialLabel();
            this.numQuantity = new System.Windows.Forms.NumericUpDown();
            this.lblStatus = new MaterialSkin.Controls.MaterialLabel();
            this.txtStatus = new MaterialSkin.Controls.MaterialTextBox();
            this.btnSave = new MaterialSkin.Controls.MaterialButton();
            this.btnCancel = new MaterialSkin.Controls.MaterialButton();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSupplier
            // 
            this.lblSupplier.Depth = 0;
            this.lblSupplier.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblSupplier.Location = new System.Drawing.Point(30, 94);
            this.lblSupplier.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(100, 23);
            this.lblSupplier.TabIndex = 0;
            this.lblSupplier.Text = "Supplier:";
            // 
            // cmbSupplier
            // 
            this.cmbSupplier.AutoResize = false;
            this.cmbSupplier.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbSupplier.Depth = 0;
            this.cmbSupplier.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbSupplier.DropDownHeight = 174;
            this.cmbSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSupplier.DropDownWidth = 121;
            this.cmbSupplier.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.cmbSupplier.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbSupplier.FormattingEnabled = true;
            this.cmbSupplier.IntegralHeight = false;
            this.cmbSupplier.ItemHeight = 43;
            this.cmbSupplier.Location = new System.Drawing.Point(157, 77);
            this.cmbSupplier.MaxDropDownItems = 4;
            this.cmbSupplier.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbSupplier.Name = "cmbSupplier";
            this.cmbSupplier.Size = new System.Drawing.Size(200, 49);
            this.cmbSupplier.StartIndex = 0;
            this.cmbSupplier.TabIndex = 1;
            this.cmbSupplier.SelectedIndexChanged += new System.EventHandler(this.cmbSupplier_SelectedIndexChanged);
            this.cmbSupplier.Validating += new System.ComponentModel.CancelEventHandler(this.cmbSupplier_Validating);
            // 
            // lblContract
            // 
            this.lblContract.Depth = 0;
            this.lblContract.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblContract.Location = new System.Drawing.Point(30, 166);
            this.lblContract.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblContract.Name = "lblContract";
            this.lblContract.Size = new System.Drawing.Size(100, 23);
            this.lblContract.TabIndex = 2;
            this.lblContract.Text = "Contract:";
            // 
            // cmbContract
            // 
            this.cmbContract.AutoResize = false;
            this.cmbContract.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbContract.Depth = 0;
            this.cmbContract.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbContract.DropDownHeight = 174;
            this.cmbContract.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbContract.DropDownWidth = 121;
            this.cmbContract.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.cmbContract.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbContract.FormattingEnabled = true;
            this.cmbContract.IntegralHeight = false;
            this.cmbContract.ItemHeight = 43;
            this.cmbContract.Location = new System.Drawing.Point(160, 161);
            this.cmbContract.MaxDropDownItems = 4;
            this.cmbContract.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbContract.Name = "cmbContract";
            this.cmbContract.Size = new System.Drawing.Size(200, 49);
            this.cmbContract.StartIndex = 0;
            this.cmbContract.TabIndex = 3;
            this.cmbContract.Validating += new System.ComponentModel.CancelEventHandler(this.cmbContract_Validating);
            // 
            // lblExpected
            // 
            this.lblExpected.Depth = 0;
            this.lblExpected.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblExpected.Location = new System.Drawing.Point(30, 231);
            this.lblExpected.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblExpected.Name = "lblExpected";
            this.lblExpected.Size = new System.Drawing.Size(120, 23);
            this.lblExpected.TabIndex = 4;
            this.lblExpected.Text = "Expected Date:";
            // 
            // dtpExpected
            // 
            this.dtpExpected.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpExpected.Location = new System.Drawing.Point(160, 226);
            this.dtpExpected.Name = "dtpExpected";
            this.dtpExpected.Size = new System.Drawing.Size(200, 22);
            this.dtpExpected.TabIndex = 5;
            // 
            // lblQuantity
            // 
            this.lblQuantity.Depth = 0;
            this.lblQuantity.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblQuantity.Location = new System.Drawing.Point(30, 286);
            this.lblQuantity.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(100, 23);
            this.lblQuantity.TabIndex = 6;
            this.lblQuantity.Text = "Quantity:";
            // 
            // numQuantity
            // 
            this.numQuantity.Location = new System.Drawing.Point(160, 281);
            this.numQuantity.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numQuantity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numQuantity.Name = "numQuantity";
            this.numQuantity.Size = new System.Drawing.Size(120, 22);
            this.numQuantity.TabIndex = 7;
            this.numQuantity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numQuantity.Validating += new System.ComponentModel.CancelEventHandler(this.numQuantity_Validating);
            // 
            // lblStatus
            // 
            this.lblStatus.Depth = 0;
            this.lblStatus.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblStatus.Location = new System.Drawing.Point(30, 331);
            this.lblStatus.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(100, 23);
            this.lblStatus.TabIndex = 8;
            this.lblStatus.Text = "Status:";
            // 
            // txtStatus
            // 
            this.txtStatus.AnimateReadOnly = false;
            this.txtStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtStatus.Depth = 0;
            this.txtStatus.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtStatus.LeadingIcon = null;
            this.txtStatus.Location = new System.Drawing.Point(160, 326);
            this.txtStatus.MaxLength = 50;
            this.txtStatus.MouseState = MaterialSkin.MouseState.OUT;
            this.txtStatus.Multiline = false;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(200, 50);
            this.txtStatus.TabIndex = 9;
            this.txtStatus.Text = "";
            this.txtStatus.TrailingIcon = null;
            this.txtStatus.Validating += new System.ComponentModel.CancelEventHandler(this.txtStatus_Validating);
            // 
            // btnSave
            // 
            this.btnSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSave.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnSave.Depth = 0;
            this.btnSave.HighEmphasis = true;
            this.btnSave.Icon = null;
            this.btnSave.Location = new System.Drawing.Point(160, 396);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnSave.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnSave.Name = "btnSave";
            this.btnSave.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnSave.Size = new System.Drawing.Size(64, 36);
            this.btnSave.TabIndex = 10;
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
            this.btnCancel.Location = new System.Drawing.Point(280, 396);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnCancel.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnCancel.Size = new System.Drawing.Size(77, 36);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnCancel.UseAccentColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // SupplyScheduleAddForm
            // 
            this.ClientSize = new System.Drawing.Size(420, 456);
            this.Controls.Add(this.cmbSupplier);
            this.Controls.Add(this.lblSupplier);
            this.Controls.Add(this.cmbContract);
            this.Controls.Add(this.lblContract);
            this.Controls.Add(this.lblExpected);
            this.Controls.Add(this.dtpExpected);
            this.Controls.Add(this.lblQuantity);
            this.Controls.Add(this.numQuantity);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SupplyScheduleAddForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Supply Schedule";
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}

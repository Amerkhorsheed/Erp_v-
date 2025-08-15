// File: DeliveryDetailForm.Designer.cs
using MaterialSkin.Controls;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Erp_V1
{
    partial class DeliveryDetailForm
    {
        private IContainer components = null;
        private MaterialLabel lblCustomerPrompt;
        private MaterialLabel lblCustomerName;
        private MaterialLabel lblAddressPrompt;
        private MaterialLabel lblAddress;
        private MaterialLabel lblDeliveryPersonPrompt;
        private MaterialComboBox cmbDeliveryPerson;
        private MaterialLabel lblStatusPrompt;
        private MaterialComboBox cmbStatus;
        private MaterialLabel lblAssignedDatePrompt;
        private DateTimePicker dtpAssignedDate;
        private MaterialLabel lblDeliveredDatePrompt;
        private DateTimePicker dtpDeliveredDate;
        private MaterialButton btnSave;
        private MaterialButton btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblCustomerPrompt = new MaterialSkin.Controls.MaterialLabel();
            this.lblCustomerName = new MaterialSkin.Controls.MaterialLabel();
            this.lblAddressPrompt = new MaterialSkin.Controls.MaterialLabel();
            this.lblAddress = new MaterialSkin.Controls.MaterialLabel();
            this.lblDeliveryPersonPrompt = new MaterialSkin.Controls.MaterialLabel();
            this.cmbDeliveryPerson = new MaterialSkin.Controls.MaterialComboBox();
            this.lblStatusPrompt = new MaterialSkin.Controls.MaterialLabel();
            this.cmbStatus = new MaterialSkin.Controls.MaterialComboBox();
            this.lblAssignedDatePrompt = new MaterialSkin.Controls.MaterialLabel();
            this.dtpAssignedDate = new System.Windows.Forms.DateTimePicker();
            this.lblDeliveredDatePrompt = new MaterialSkin.Controls.MaterialLabel();
            this.dtpDeliveredDate = new System.Windows.Forms.DateTimePicker();
            this.btnSave = new MaterialSkin.Controls.MaterialButton();
            this.btnCancel = new MaterialSkin.Controls.MaterialButton();
            this.SuspendLayout();
            // 
            // lblCustomerPrompt
            // 
            this.lblCustomerPrompt.Depth = 0;
            this.lblCustomerPrompt.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblCustomerPrompt.Location = new System.Drawing.Point(25, 90);
            this.lblCustomerPrompt.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblCustomerPrompt.Name = "lblCustomerPrompt";
            this.lblCustomerPrompt.Size = new System.Drawing.Size(150, 23);
            this.lblCustomerPrompt.TabIndex = 0;
            this.lblCustomerPrompt.Text = "Customer:";
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.Depth = 0;
            this.lblCustomerName.Font = new System.Drawing.Font("Roboto Medium", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.lblCustomerName.FontType = MaterialSkin.MaterialSkinManager.fontType.Subtitle2;
            this.lblCustomerName.Location = new System.Drawing.Point(190, 90);
            this.lblCustomerName.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(350, 23);
            this.lblCustomerName.TabIndex = 1;
            this.lblCustomerName.Text = "[Customer Name]";
            // 
            // lblAddressPrompt
            // 
            this.lblAddressPrompt.Depth = 0;
            this.lblAddressPrompt.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblAddressPrompt.Location = new System.Drawing.Point(25, 130);
            this.lblAddressPrompt.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblAddressPrompt.Name = "lblAddressPrompt";
            this.lblAddressPrompt.Size = new System.Drawing.Size(150, 23);
            this.lblAddressPrompt.TabIndex = 2;
            this.lblAddressPrompt.Text = "Address:";
            // 
            // lblAddress
            // 
            this.lblAddress.Depth = 0;
            this.lblAddress.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblAddress.Location = new System.Drawing.Point(190, 130);
            this.lblAddress.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(350, 50);
            this.lblAddress.TabIndex = 3;
            this.lblAddress.Text = "[Delivery Address]";
            // 
            // lblDeliveryPersonPrompt
            // 
            this.lblDeliveryPersonPrompt.Depth = 0;
            this.lblDeliveryPersonPrompt.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblDeliveryPersonPrompt.Location = new System.Drawing.Point(25, 190);
            this.lblDeliveryPersonPrompt.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblDeliveryPersonPrompt.Name = "lblDeliveryPersonPrompt";
            this.lblDeliveryPersonPrompt.Size = new System.Drawing.Size(150, 23);
            this.lblDeliveryPersonPrompt.TabIndex = 4;
            this.lblDeliveryPersonPrompt.Text = "Assign To:";
            // 
            // cmbDeliveryPerson
            // 
            this.cmbDeliveryPerson.AutoResize = false;
            this.cmbDeliveryPerson.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbDeliveryPerson.Depth = 0;
            this.cmbDeliveryPerson.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbDeliveryPerson.DropDownHeight = 174;
            this.cmbDeliveryPerson.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeliveryPerson.DropDownWidth = 121;
            this.cmbDeliveryPerson.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.cmbDeliveryPerson.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbDeliveryPerson.FormattingEnabled = true;
            this.cmbDeliveryPerson.IntegralHeight = false;
            this.cmbDeliveryPerson.ItemHeight = 43;
            this.cmbDeliveryPerson.Location = new System.Drawing.Point(190, 180);
            this.cmbDeliveryPerson.MaxDropDownItems = 4;
            this.cmbDeliveryPerson.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbDeliveryPerson.Name = "cmbDeliveryPerson";
            this.cmbDeliveryPerson.Size = new System.Drawing.Size(350, 49);
            this.cmbDeliveryPerson.StartIndex = 0;
            this.cmbDeliveryPerson.TabIndex = 5;
            // 
            // lblStatusPrompt
            // 
            this.lblStatusPrompt.Depth = 0;
            this.lblStatusPrompt.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblStatusPrompt.Location = new System.Drawing.Point(25, 255);
            this.lblStatusPrompt.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblStatusPrompt.Name = "lblStatusPrompt";
            this.lblStatusPrompt.Size = new System.Drawing.Size(150, 23);
            this.lblStatusPrompt.TabIndex = 6;
            this.lblStatusPrompt.Text = "Status:";
            // 
            // cmbStatus
            // 
            this.cmbStatus.AutoResize = false;
            this.cmbStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbStatus.Depth = 0;
            this.cmbStatus.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbStatus.DropDownHeight = 174;
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.DropDownWidth = 121;
            this.cmbStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.cmbStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.IntegralHeight = false;
            this.cmbStatus.ItemHeight = 43;
            this.cmbStatus.Location = new System.Drawing.Point(190, 245);
            this.cmbStatus.MaxDropDownItems = 4;
            this.cmbStatus.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(350, 49);
            this.cmbStatus.StartIndex = 0;
            this.cmbStatus.TabIndex = 7;
            // 
            // lblAssignedDatePrompt
            // 
            this.lblAssignedDatePrompt.Depth = 0;
            this.lblAssignedDatePrompt.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblAssignedDatePrompt.Location = new System.Drawing.Point(25, 320);
            this.lblAssignedDatePrompt.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblAssignedDatePrompt.Name = "lblAssignedDatePrompt";
            this.lblAssignedDatePrompt.Size = new System.Drawing.Size(150, 23);
            this.lblAssignedDatePrompt.TabIndex = 8;
            this.lblAssignedDatePrompt.Text = "Assigned Date:";
            // 
            // dtpAssignedDate
            // 
            this.dtpAssignedDate.Checked = false;
            this.dtpAssignedDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpAssignedDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpAssignedDate.Location = new System.Drawing.Point(190, 320);
            this.dtpAssignedDate.Name = "dtpAssignedDate";
            this.dtpAssignedDate.ShowCheckBox = true;
            this.dtpAssignedDate.Size = new System.Drawing.Size(150, 26);
            this.dtpAssignedDate.TabIndex = 9;
            // 
            // lblDeliveredDatePrompt
            // 
            this.lblDeliveredDatePrompt.Depth = 0;
            this.lblDeliveredDatePrompt.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblDeliveredDatePrompt.Location = new System.Drawing.Point(25, 365);
            this.lblDeliveredDatePrompt.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblDeliveredDatePrompt.Name = "lblDeliveredDatePrompt";
            this.lblDeliveredDatePrompt.Size = new System.Drawing.Size(150, 23);
            this.lblDeliveredDatePrompt.TabIndex = 10;
            this.lblDeliveredDatePrompt.Text = "Delivered Date:";
            // 
            // dtpDeliveredDate
            // 
            this.dtpDeliveredDate.Checked = false;
            this.dtpDeliveredDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpDeliveredDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDeliveredDate.Location = new System.Drawing.Point(190, 365);
            this.dtpDeliveredDate.Name = "dtpDeliveredDate";
            this.dtpDeliveredDate.ShowCheckBox = true;
            this.dtpDeliveredDate.Size = new System.Drawing.Size(150, 26);
            this.dtpDeliveredDate.TabIndex = 11;
            // 
            // btnSave
            // 
            this.btnSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSave.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnSave.Depth = 0;
            this.btnSave.HighEmphasis = true;
            this.btnSave.Icon = null;
            this.btnSave.Location = new System.Drawing.Point(380, 420);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnSave.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnSave.Name = "btnSave";
            this.btnSave.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnSave.Size = new System.Drawing.Size(64, 36);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Save";
            this.btnSave.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnSave.UseAccentColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCancel.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnCancel.Depth = 0;
            this.btnCancel.HighEmphasis = false;
            this.btnCancel.Icon = null;
            this.btnCancel.Location = new System.Drawing.Point(460, 420);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnCancel.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnCancel.Size = new System.Drawing.Size(77, 36);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Text;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // DeliveryDetailForm
            // 
            this.ClientSize = new System.Drawing.Size(570, 480);
            this.Controls.Add(this.lblCustomerPrompt);
            this.Controls.Add(this.lblCustomerName);
            this.Controls.Add(this.lblAddressPrompt);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.lblDeliveryPersonPrompt);
            this.Controls.Add(this.cmbDeliveryPerson);
            this.Controls.Add(this.lblStatusPrompt);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.lblAssignedDatePrompt);
            this.Controls.Add(this.dtpAssignedDate);
            this.Controls.Add(this.lblDeliveredDatePrompt);
            this.Controls.Add(this.dtpDeliveredDate);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DeliveryDetailForm";
            this.Padding = new System.Windows.Forms.Padding(3, 64, 3, 3);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Update Delivery Details";
            this.Load += new System.EventHandler(this.DeliveryDetailForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
//// File: DeliveryUpdateForm.Designer.cs
//using MaterialSkin.Controls;
//using System.ComponentModel;
//using System.Drawing;
//using System.Windows.Forms;

//namespace Erp_V1
//{
//    partial class DeliveryUpdateForm
//    {
//        private IContainer components = null;
//        private MaterialLabel lblCustomerName;
//        private MaterialLabel lblAddress;
//        private MaterialComboBox cmbDeliveryPerson;
//        private MaterialComboBox cmbStatus;
//        private DateTimePicker dtpAssignedDate;
//        private DateTimePicker dtpDeliveredDate;
//        private MaterialButton btnSave;
//        private MaterialButton btnCancel;
//        private MaterialLabel lblCustomerHeader;
//        private MaterialLabel lblAddressHeader;
//        private MaterialLabel lblAssignedDate;
//        private MaterialLabel lblDeliveredDate;

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && (components != null)) components.Dispose();
//            base.Dispose(disposing);
//        }

//        private void InitializeComponent()
//        {
//            this.lblCustomerName = new MaterialSkin.Controls.MaterialLabel();
//            this.lblAddress = new MaterialSkin.Controls.MaterialLabel();
//            this.cmbDeliveryPerson = new MaterialSkin.Controls.MaterialComboBox();
//            this.cmbStatus = new MaterialSkin.Controls.MaterialComboBox();
//            this.dtpAssignedDate = new System.Windows.Forms.DateTimePicker();
//            this.dtpDeliveredDate = new System.Windows.Forms.DateTimePicker();
//            this.btnSave = new MaterialSkin.Controls.MaterialButton();
//            this.btnCancel = new MaterialSkin.Controls.MaterialButton();
//            this.lblCustomerHeader = new MaterialSkin.Controls.MaterialLabel();
//            this.lblAddressHeader = new MaterialSkin.Controls.MaterialLabel();
//            this.lblAssignedDate = new MaterialSkin.Controls.MaterialLabel();
//            this.lblDeliveredDate = new MaterialSkin.Controls.MaterialLabel();
//            this.SuspendLayout();
//            // 
//            // lblCustomerHeader
//            // 
//            this.lblCustomerHeader.AutoSize = true;
//            this.lblCustomerHeader.Depth = 0;
//            this.lblCustomerHeader.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
//            this.lblCustomerHeader.Location = new System.Drawing.Point(25, 80);
//            this.lblCustomerHeader.MouseState = MaterialSkin.MouseState.HOVER;
//            this.lblCustomerHeader.Name = "lblCustomerHeader";
//            this.lblCustomerHeader.Size = new System.Drawing.Size(71, 19);
//            this.lblCustomerHeader.Text = "Customer:";
//            // 
//            // lblCustomerName
//            // 
//            this.lblCustomerName.AutoSize = true;
//            this.lblCustomerName.Depth = 0;
//            this.lblCustomerName.Font = new System.Drawing.Font("Roboto Medium", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
//            this.lblCustomerName.FontType = MaterialSkin.MaterialSkinManager.fontType.Subtitle2;
//            this.lblCustomerName.Location = new System.Drawing.Point(150, 80);
//            this.lblCustomerName.MouseState = MaterialSkin.MouseState.HOVER;
//            this.lblCustomerName.Name = "lblCustomerName";
//            this.lblCustomerName.Size = new System.Drawing.Size(1, 0);
//            // 
//            // lblAddressHeader
//            // 
//            this.lblAddressHeader.AutoSize = true;
//            this.lblAddressHeader.Depth = 0;
//            this.lblAddressHeader.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
//            this.lblAddressHeader.Location = new System.Drawing.Point(25, 110);
//            this.lblAddressHeader.MouseState = MaterialSkin.MouseState.HOVER;
//            this.lblAddressHeader.Name = "lblAddressHeader";
//            this.lblAddressHeader.Size = new System.Drawing.Size(60, 19);
//            this.lblAddressHeader.Text = "Address:";
//            // 
//            // lblAddress
//            // 
//            this.lblAddress.AutoSize = true;
//            this.lblAddress.Depth = 0;
//            this.lblAddress.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
//            this.lblAddress.Location = new System.Drawing.Point(150, 110);
//            this.lblAddress.MouseState = MaterialSkin.MouseState.HOVER;
//            this.lblAddress.Name = "lblAddress";
//            this.lblAddress.Size = new System.Drawing.Size(1, 0);
//            // 
//            // cmbDeliveryPerson
//            // 
//            this.cmbDeliveryPerson.AutoResize = false;
//            this.cmbDeliveryPerson.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
//            this.cmbDeliveryPerson.Depth = 0;
//            this.cmbDeliveryPerson.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
//            this.cmbDeliveryPerson.DropDownHeight = 174;
//            this.cmbDeliveryPerson.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
//            this.cmbDeliveryPerson.DropDownWidth = 121;
//            this.cmbDeliveryPerson.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
//            this.cmbDeliveryPerson.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
//            this.cmbDeliveryPerson.FormattingEnabled = true;
//            this.cmbDeliveryPerson.Hint = "Delivery Person";
//            this.cmbDeliveryPerson.IntegralHeight = false;
//            this.cmbDeliveryPerson.ItemHeight = 43;
//            this.cmbDeliveryPerson.Location = new System.Drawing.Point(28, 150);
//            this.cmbDeliveryPerson.MaxDropDownItems = 4;
//            this.cmbDeliveryPerson.MouseState = MaterialSkin.MouseState.OUT;
//            this.cmbDeliveryPerson.Name = "cmbDeliveryPerson";
//            this.cmbDeliveryPerson.Size = new System.Drawing.Size(350, 49);
//            this.cmbDeliveryPerson.StartIndex = 0;
//            this.cmbDeliveryPerson.TabIndex = 0;
//            // 
//            // cmbStatus
//            // 
//            this.cmbStatus.AutoResize = false;
//            this.cmbStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
//            this.cmbStatus.Depth = 0;
//            this.cmbStatus.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
//            this.cmbStatus.DropDownHeight = 174;
//            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
//            this.cmbStatus.DropDownWidth = 121;
//            this.cmbStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
//            this.cmbStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
//            this.cmbStatus.FormattingEnabled = true;
//            this.cmbStatus.Hint = "Status";
//            this.cmbStatus.IntegralHeight = false;
//            this.cmbStatus.ItemHeight = 43;
//            this.cmbStatus.Location = new System.Drawing.Point(28, 215);
//            this.cmbStatus.MaxDropDownItems = 4;
//            this.cmbStatus.MouseState = MaterialSkin.MouseState.OUT;
//            this.cmbStatus.Name = "cmbStatus";
//            this.cmbStatus.Size = new System.Drawing.Size(350, 49);
//            this.cmbStatus.StartIndex = 0;
//            this.cmbStatus.TabIndex = 1;
//            this.cmbStatus.SelectedIndexChanged += new System.EventHandler(this.cmbStatus_SelectedIndexChanged);
//            // 
//            // lblAssignedDate
//            // 
//            this.lblAssignedDate.AutoSize = true;
//            this.lblAssignedDate.Depth = 0;
//            this.lblAssignedDate.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
//            this.lblAssignedDate.Location = new System.Drawing.Point(25, 285);
//            this.lblAssignedDate.MouseState = MaterialSkin.MouseState.HOVER;
//            this.lblAssignedDate.Name = "lblAssignedDate";
//            this.lblAssignedDate.Size = new System.Drawing.Size(102, 19);
//            this.lblAssignedDate.Text = "Assigned Date:";
//            // 
//            // dtpAssignedDate
//            // 
//            this.dtpAssignedDate.Location = new System.Drawing.Point(150, 285);
//            this.dtpAssignedDate.Name = "dtpAssignedDate";
//            this.dtpAssignedDate.Size = new System.Drawing.Size(228, 22);
//            this.dtpAssignedDate.TabIndex = 2;
//            // 
//            // lblDeliveredDate
//            // 
//            this.lblDeliveredDate.AutoSize = true;
//            this.lblDeliveredDate.Depth = 0;
//            this.lblDeliveredDate.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
//            this.lblDeliveredDate.Location = new System.Drawing.Point(25, 325);
//            this.lblDeliveredDate.MouseState = MaterialSkin.MouseState.HOVER;
//            this.lblDeliveredDate.Name = "lblDeliveredDate";
//            this.lblDeliveredDate.Size = new System.Drawing.Size(106, 19);
//            this.lblDeliveredDate.Text = "Delivered Date:";
//            // 
//            // dtpDeliveredDate
//            // 
//            this.dtpDeliveredDate.Location = new System.Drawing.Point(150, 325);
//            this.dtpDeliveredDate.Name = "dtpDeliveredDate";
//            this.dtpDeliveredDate.Size = new System.Drawing.Size(228, 22);
//            this.dtpDeliveredDate.TabIndex = 3;
//            // 
//            // btnSave
//            // 
//            this.btnSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
//            this.btnSave.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
//            this.btnSave.Depth = 0;
//            this.btnSave.HighEmphasis = true;
//            this.btnSave.Icon = null;
//            this.btnSave.Location = new System.Drawing.Point(220, 380);
//            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
//            this.btnSave.MouseState = MaterialSkin.MouseState.HOVER;
//            this.btnSave.Name = "btnSave";
//            this.btnSave.NoAccentTextColor = System.Drawing.Color.Empty;
//            this.btnSave.Size = new System.Drawing.Size(64, 36);
//            this.btnSave.TabIndex = 4;
//            this.btnSave.Text = "Save";
//            this.btnSave.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
//            this.btnSave.UseAccentColor = false;
//            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
//            // 
//            // btnCancel
//            // 
//            this.btnCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
//            this.btnCancel.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
//            this.btnCancel.Depth = 0;
//            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
//            this.btnCancel.HighEmphasis = false;
//            this.btnCancel.Icon = null;
//            this.btnCancel.Location = new System.Drawing.Point(300, 380);
//            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
//            this.btnCancel.MouseState = MaterialSkin.MouseState.HOVER;
//            this.btnCancel.Name = "btnCancel";
//            this.btnCancel.NoAccentTextColor = System.Drawing.Color.Empty;
//            this.btnCancel.Size = new System.Drawing.Size(77, 36);
//            this.btnCancel.TabIndex = 5;
//            this.btnCancel.Text = "Cancel";
//            this.btnCancel.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Text;
//            this.btnCancel.UseAccentColor = false;
//            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
//            // 
//            // DeliveryUpdateForm
//            // 
//            this.AcceptButton = this.btnSave;
//            this.CancelButton = this.btnCancel;
//            this.ClientSize = new System.Drawing.Size(420, 450);
//            this.Controls.Add(this.btnCancel);
//            this.Controls.Add(this.btnSave);
//            this.Controls.Add(this.dtpDeliveredDate);
//            this.Controls.Add(this.lblDeliveredDate);
//            this.Controls.Add(this.dtpAssignedDate);
//            this.Controls.Add(this.lblAssignedDate);
//            this.Controls.Add(this.cmbStatus);
//            this.Controls.Add(this.cmbDeliveryPerson);
//            this.Controls.Add(this.lblAddress);
//            this.Controls.Add(this.lblAddressHeader);
//            this.Controls.Add(this.lblCustomerName);
//            this.Controls.Add(this.lblCustomerHeader);
//            this.Name = "DeliveryUpdateForm";
//            this.Text = "Update Delivery";
//            this.Load += new System.EventHandler(this.DeliveryUpdateForm_Load);
//            this.ResumeLayout(false);
//            this.PerformLayout();
//        }
//    }
//}
namespace Erp_V1
{
    partial class frmReturn
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.GroupBox grpSales;
        private System.Windows.Forms.DataGridView dgvSales;
        private System.Windows.Forms.GroupBox grpReturnDetails;
        private System.Windows.Forms.Label lblSaleID;
        private System.Windows.Forms.TextBox txtSaleID;
        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.Label lblReturnQuantity;
        private System.Windows.Forms.NumericUpDown nudReturnQuantity;
        private System.Windows.Forms.Label lblReturnDate;
        private System.Windows.Forms.DateTimePicker dtpReturnDate;
        private System.Windows.Forms.Label lblReturnReason;
        private System.Windows.Forms.TextBox txtReturnReason;
        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox grpReturns;
        private System.Windows.Forms.DataGridView dgvReturns;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grpSales = new System.Windows.Forms.GroupBox();
            this.dgvSales = new System.Windows.Forms.DataGridView();
            this.grpReturnDetails = new System.Windows.Forms.GroupBox();
            this.lblSaleID = new System.Windows.Forms.Label();
            this.txtSaleID = new System.Windows.Forms.TextBox();
            this.lblProductName = new System.Windows.Forms.Label();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.lblReturnQuantity = new System.Windows.Forms.Label();
            this.nudReturnQuantity = new System.Windows.Forms.NumericUpDown();
            this.lblReturnDate = new System.Windows.Forms.Label();
            this.dtpReturnDate = new System.Windows.Forms.DateTimePicker();
            this.lblReturnReason = new System.Windows.Forms.Label();
            this.txtReturnReason = new System.Windows.Forms.TextBox();
            this.btnInsert = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.grpReturns = new System.Windows.Forms.GroupBox();
            this.dgvReturns = new System.Windows.Forms.DataGridView();
            this.grpSales.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSales)).BeginInit();
            this.grpReturnDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudReturnQuantity)).BeginInit();
            this.grpReturns.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReturns)).BeginInit();
            this.SuspendLayout();
            // 
            // grpSales
            // 
            this.grpSales.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpSales.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.grpSales.Controls.Add(this.dgvSales);
            this.grpSales.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.grpSales.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.grpSales.Location = new System.Drawing.Point(12, 12);
            this.grpSales.Name = "grpSales";
            this.grpSales.Size = new System.Drawing.Size(1020, 200);
            this.grpSales.TabIndex = 0;
            this.grpSales.TabStop = false;
            this.grpSales.Text = "Select Sale Record";
            // 
            // dgvSales
            // 
            this.dgvSales.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSales.BackgroundColor = System.Drawing.Color.White;
            this.dgvSales.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.White;
            this.dgvSales.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvSales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSales.DefaultCellStyle = dataGridViewCellStyle11;
            this.dgvSales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSales.Location = new System.Drawing.Point(3, 28);
            this.dgvSales.Name = "dgvSales";
            this.dgvSales.ReadOnly = true;
            this.dgvSales.RowHeadersWidth = 51;
            this.dgvSales.RowTemplate.Height = 24;
            this.dgvSales.Size = new System.Drawing.Size(1014, 169);
            this.dgvSales.TabIndex = 0;
            this.dgvSales.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSales_RowEnter);
            // 
            // grpReturnDetails
            // 
            this.grpReturnDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpReturnDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.grpReturnDetails.Controls.Add(this.lblSaleID);
            this.grpReturnDetails.Controls.Add(this.txtSaleID);
            this.grpReturnDetails.Controls.Add(this.lblProductName);
            this.grpReturnDetails.Controls.Add(this.txtProductName);
            this.grpReturnDetails.Controls.Add(this.lblCustomerName);
            this.grpReturnDetails.Controls.Add(this.txtCustomerName);
            this.grpReturnDetails.Controls.Add(this.lblReturnQuantity);
            this.grpReturnDetails.Controls.Add(this.nudReturnQuantity);
            this.grpReturnDetails.Controls.Add(this.lblReturnDate);
            this.grpReturnDetails.Controls.Add(this.dtpReturnDate);
            this.grpReturnDetails.Controls.Add(this.lblReturnReason);
            this.grpReturnDetails.Controls.Add(this.txtReturnReason);
            this.grpReturnDetails.Controls.Add(this.btnInsert);
            this.grpReturnDetails.Controls.Add(this.btnUpdate);
            this.grpReturnDetails.Controls.Add(this.btnDelete);
            this.grpReturnDetails.Controls.Add(this.btnClear);
            this.grpReturnDetails.Controls.Add(this.btnClose);
            this.grpReturnDetails.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.grpReturnDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.grpReturnDetails.Location = new System.Drawing.Point(12, 218);
            this.grpReturnDetails.Name = "grpReturnDetails";
            this.grpReturnDetails.Size = new System.Drawing.Size(1020, 206);
            this.grpReturnDetails.TabIndex = 1;
            this.grpReturnDetails.TabStop = false;
            this.grpReturnDetails.Text = "Return Details";
            // 
            // lblSaleID
            // 
            this.lblSaleID.AutoSize = true;
            this.lblSaleID.Location = new System.Drawing.Point(16, 38);
            this.lblSaleID.Name = "lblSaleID";
            this.lblSaleID.Size = new System.Drawing.Size(78, 25);
            this.lblSaleID.TabIndex = 0;
            this.lblSaleID.Text = "Sale ID:";
            // 
            // txtSaleID
            // 
            this.txtSaleID.Location = new System.Drawing.Point(150, 35);
            this.txtSaleID.Name = "txtSaleID";
            this.txtSaleID.ReadOnly = true;
            this.txtSaleID.Size = new System.Drawing.Size(140, 32);
            this.txtSaleID.TabIndex = 1;
            // 
            // lblProductName
            // 
            this.lblProductName.AutoSize = true;
            this.lblProductName.Location = new System.Drawing.Point(16, 80);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(146, 25);
            this.lblProductName.TabIndex = 2;
            this.lblProductName.Text = "Product Name:";
            // 
            // txtProductName
            // 
            this.txtProductName.Location = new System.Drawing.Point(164, 75);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.ReadOnly = true;
            this.txtProductName.Size = new System.Drawing.Size(140, 32);
            this.txtProductName.TabIndex = 3;
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.AutoSize = true;
            this.lblCustomerName.Location = new System.Drawing.Point(16, 122);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(160, 25);
            this.lblCustomerName.TabIndex = 4;
            this.lblCustomerName.Text = "Customer Name:";
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Location = new System.Drawing.Point(181, 118);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.ReadOnly = true;
            this.txtCustomerName.Size = new System.Drawing.Size(140, 32);
            this.txtCustomerName.TabIndex = 5;
            // 
            // lblReturnQuantity
            // 
            this.lblReturnQuantity.AutoSize = true;
            this.lblReturnQuantity.Location = new System.Drawing.Point(480, 38);
            this.lblReturnQuantity.Name = "lblReturnQuantity";
            this.lblReturnQuantity.Size = new System.Drawing.Size(160, 25);
            this.lblReturnQuantity.TabIndex = 6;
            this.lblReturnQuantity.Text = "Return Quantity:";
            // 
            // nudReturnQuantity
            // 
            this.nudReturnQuantity.Location = new System.Drawing.Point(650, 35);
            this.nudReturnQuantity.Name = "nudReturnQuantity";
            this.nudReturnQuantity.Size = new System.Drawing.Size(140, 32);
            this.nudReturnQuantity.TabIndex = 7;
            // 
            // lblReturnDate
            // 
            this.lblReturnDate.AutoSize = true;
            this.lblReturnDate.Location = new System.Drawing.Point(480, 80);
            this.lblReturnDate.Name = "lblReturnDate";
            this.lblReturnDate.Size = new System.Drawing.Size(124, 25);
            this.lblReturnDate.TabIndex = 8;
            this.lblReturnDate.Text = "Return Date:";
            // 
            // dtpReturnDate
            // 
            this.dtpReturnDate.CalendarForeColor = System.Drawing.Color.Black;
            this.dtpReturnDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpReturnDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpReturnDate.Location = new System.Drawing.Point(650, 75);
            this.dtpReturnDate.Name = "dtpReturnDate";
            this.dtpReturnDate.Size = new System.Drawing.Size(140, 32);
            this.dtpReturnDate.TabIndex = 9;
            // 
            // lblReturnReason
            // 
            this.lblReturnReason.AutoSize = true;
            this.lblReturnReason.Location = new System.Drawing.Point(480, 122);
            this.lblReturnReason.Name = "lblReturnReason";
            this.lblReturnReason.Size = new System.Drawing.Size(147, 25);
            this.lblReturnReason.TabIndex = 10;
            this.lblReturnReason.Text = "Return Reason:";
            // 
            // txtReturnReason
            // 
            this.txtReturnReason.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.txtReturnReason.Location = new System.Drawing.Point(633, 118);
            this.txtReturnReason.Multiline = true;
            this.txtReturnReason.Name = "txtReturnReason";
            this.txtReturnReason.Size = new System.Drawing.Size(381, 82);
            this.txtReturnReason.TabIndex = 11;
            // 
            // btnInsert
            // 
            this.btnInsert.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnInsert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInsert.ForeColor = System.Drawing.Color.White;
            this.btnInsert.Location = new System.Drawing.Point(16, 165);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(100, 35);
            this.btnInsert.TabIndex = 12;
            this.btnInsert.Text = "Insert";
            this.btnInsert.UseVisualStyleBackColor = false;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Location = new System.Drawing.Point(130, 165);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(100, 35);
            this.btnUpdate.TabIndex = 13;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(244, 165);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 35);
            this.btnDelete.TabIndex = 14;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(358, 165);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 35);
            this.btnClear.TabIndex = 15;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(472, 165);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 35);
            this.btnClose.TabIndex = 16;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // grpReturns
            // 
            this.grpReturns.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpReturns.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.grpReturns.Controls.Add(this.dgvReturns);
            this.grpReturns.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.grpReturns.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.grpReturns.Location = new System.Drawing.Point(12, 424);
            this.grpReturns.Name = "grpReturns";
            this.grpReturns.Size = new System.Drawing.Size(1020, 250);
            this.grpReturns.TabIndex = 2;
            this.grpReturns.TabStop = false;
            this.grpReturns.Text = "Return Records";
            // 
            // dgvReturns
            // 
            this.dgvReturns.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReturns.BackgroundColor = System.Drawing.Color.White;
            this.dgvReturns.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvReturns.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvReturns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvReturns.DefaultCellStyle = dataGridViewCellStyle12;
            this.dgvReturns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReturns.Location = new System.Drawing.Point(3, 28);
            this.dgvReturns.Name = "dgvReturns";
            this.dgvReturns.ReadOnly = true;
            this.dgvReturns.RowHeadersWidth = 51;
            this.dgvReturns.RowTemplate.Height = 24;
            this.dgvReturns.Size = new System.Drawing.Size(1014, 219);
            this.dgvReturns.TabIndex = 0;
            this.dgvReturns.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReturns_RowEnter);
            // 
            // frmReturn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1044, 686);
            this.Controls.Add(this.grpReturns);
            this.Controls.Add(this.grpReturnDetails);
            this.Controls.Add(this.grpSales);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmReturn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Product Return Management";
            this.Load += new System.EventHandler(this.frmReturn_Load);
            this.grpSales.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSales)).EndInit();
            this.grpReturnDetails.ResumeLayout(false);
            this.grpReturnDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudReturnQuantity)).EndInit();
            this.grpReturns.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReturns)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}

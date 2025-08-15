// File: SupplierCommunicationAddForm.Designer.cs
using MaterialSkin.Controls;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Erp_V1
{
    partial class SupplierCommunicationAddForm
    {
        private IContainer components = null;
        private MaterialLabel lblSupplier;
        private ComboBox cmbSupplier;
        private MaterialLabel lblDate;
        private DateTimePicker dtpDate;
        private MaterialLabel lblType;
        private MaterialTextBox txtType;
        private MaterialLabel lblSubject;
        private MaterialTextBox txtSubject;
        private MaterialLabel lblContent;
        private TextBox txtContent;
        private MaterialLabel lblReference;
        private MaterialTextBox txtReference;
        private MaterialButton btnSave;
        private MaterialButton btnCancel;
        private ErrorProvider errorProvider;

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
            this.lblSupplier = new MaterialSkin.Controls.MaterialLabel();
            this.cmbSupplier = new System.Windows.Forms.ComboBox();
            this.lblDate = new MaterialSkin.Controls.MaterialLabel();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.lblType = new MaterialSkin.Controls.MaterialLabel();
            this.txtType = new MaterialSkin.Controls.MaterialTextBox();
            this.lblSubject = new MaterialSkin.Controls.MaterialLabel();
            this.txtSubject = new MaterialSkin.Controls.MaterialTextBox();
            this.lblContent = new MaterialSkin.Controls.MaterialLabel();
            this.txtContent = new System.Windows.Forms.TextBox();
            this.lblReference = new MaterialSkin.Controls.MaterialLabel();
            this.txtReference = new MaterialSkin.Controls.MaterialTextBox();
            this.btnSave = new MaterialSkin.Controls.MaterialButton();
            this.btnCancel = new MaterialSkin.Controls.MaterialButton();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSupplier
            // 
            this.lblSupplier.AutoSize = true;
            this.lblSupplier.Depth = 0;
            this.lblSupplier.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblSupplier.Location = new System.Drawing.Point(25, 97);
            this.lblSupplier.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(63, 19);
            this.lblSupplier.TabIndex = 0;
            this.lblSupplier.Text = "Supplier:";
            // 
            // cmbSupplier
            // 
            this.cmbSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSupplier.Location = new System.Drawing.Point(130, 94);
            this.cmbSupplier.Name = "cmbSupplier";
            this.cmbSupplier.Size = new System.Drawing.Size(300, 24);
            this.cmbSupplier.TabIndex = 0;
            this.cmbSupplier.Validating += new System.ComponentModel.CancelEventHandler(this.cmbSupplier_Validating);
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Depth = 0;
            this.lblDate.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblDate.Location = new System.Drawing.Point(30, 160);
            this.lblDate.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(38, 19);
            this.lblDate.TabIndex = 1;
            this.lblDate.Text = "Date:";
            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(130, 155);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(150, 22);
            this.dtpDate.TabIndex = 1;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Depth = 0;
            this.lblType.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblType.Location = new System.Drawing.Point(30, 205);
            this.lblType.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(40, 19);
            this.lblType.TabIndex = 2;
            this.lblType.Text = "Type:";
            // 
            // txtType
            // 
            this.txtType.AnimateReadOnly = false;
            this.txtType.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtType.Depth = 0;
            this.txtType.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtType.LeadingIcon = null;
            this.txtType.Location = new System.Drawing.Point(130, 199);
            this.txtType.MaxLength = 50;
            this.txtType.MouseState = MaterialSkin.MouseState.OUT;
            this.txtType.Multiline = false;
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(300, 50);
            this.txtType.TabIndex = 2;
            this.txtType.Text = "";
            this.txtType.TrailingIcon = null;
            this.txtType.Validating += new System.ComponentModel.CancelEventHandler(this.txtType_Validating);
            // 
            // lblSubject
            // 
            this.lblSubject.AutoSize = true;
            this.lblSubject.Depth = 0;
            this.lblSubject.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblSubject.Location = new System.Drawing.Point(30, 255);
            this.lblSubject.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblSubject.Name = "lblSubject";
            this.lblSubject.Size = new System.Drawing.Size(58, 19);
            this.lblSubject.TabIndex = 3;
            this.lblSubject.Text = "Subject:";
            // 
            // txtSubject
            // 
            this.txtSubject.AnimateReadOnly = false;
            this.txtSubject.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSubject.Depth = 0;
            this.txtSubject.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtSubject.LeadingIcon = null;
            this.txtSubject.Location = new System.Drawing.Point(130, 252);
            this.txtSubject.MaxLength = 50;
            this.txtSubject.MouseState = MaterialSkin.MouseState.OUT;
            this.txtSubject.Multiline = false;
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(300, 50);
            this.txtSubject.TabIndex = 3;
            this.txtSubject.Text = "";
            this.txtSubject.TrailingIcon = null;
            this.txtSubject.Validating += new System.ComponentModel.CancelEventHandler(this.txtSubject_Validating);
            // 
            // lblContent
            // 
            this.lblContent.AutoSize = true;
            this.lblContent.Depth = 0;
            this.lblContent.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblContent.Location = new System.Drawing.Point(30, 309);
            this.lblContent.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblContent.Name = "lblContent";
            this.lblContent.Size = new System.Drawing.Size(60, 19);
            this.lblContent.TabIndex = 4;
            this.lblContent.Text = "Content:";
            // 
            // txtContent
            // 
            this.txtContent.Location = new System.Drawing.Point(130, 306);
            this.txtContent.Multiline = true;
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new System.Drawing.Size(300, 94);
            this.txtContent.TabIndex = 4;
            // 
            // lblReference
            // 
            this.lblReference.AutoSize = true;
            this.lblReference.Depth = 0;
            this.lblReference.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblReference.Location = new System.Drawing.Point(30, 415);
            this.lblReference.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblReference.Name = "lblReference";
            this.lblReference.Size = new System.Drawing.Size(75, 19);
            this.lblReference.TabIndex = 5;
            this.lblReference.Text = "Reference:";
            // 
            // txtReference
            // 
            this.txtReference.AnimateReadOnly = false;
            this.txtReference.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtReference.Depth = 0;
            this.txtReference.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtReference.LeadingIcon = null;
            this.txtReference.Location = new System.Drawing.Point(130, 410);
            this.txtReference.MaxLength = 50;
            this.txtReference.MouseState = MaterialSkin.MouseState.OUT;
            this.txtReference.Multiline = false;
            this.txtReference.Name = "txtReference";
            this.txtReference.Size = new System.Drawing.Size(300, 50);
            this.txtReference.TabIndex = 5;
            this.txtReference.Text = "";
            this.txtReference.TrailingIcon = null;
            // 
            // btnSave
            // 
            this.btnSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSave.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnSave.Depth = 0;
            this.btnSave.HighEmphasis = true;
            this.btnSave.Icon = null;
            this.btnSave.Location = new System.Drawing.Point(130, 470);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnSave.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnSave.Name = "btnSave";
            this.btnSave.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnSave.Size = new System.Drawing.Size(64, 36);
            this.btnSave.TabIndex = 6;
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
            this.btnCancel.Location = new System.Drawing.Point(260, 470);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnCancel.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnCancel.Size = new System.Drawing.Size(77, 36);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnCancel.UseAccentColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // SupplierCommunicationAddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 530);
            this.Controls.Add(this.lblSupplier);
            this.Controls.Add(this.cmbSupplier);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.txtType);
            this.Controls.Add(this.lblSubject);
            this.Controls.Add(this.txtSubject);
            this.Controls.Add(this.lblContent);
            this.Controls.Add(this.txtContent);
            this.Controls.Add(this.lblReference);
            this.Controls.Add(this.txtReference);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SupplierCommunicationAddForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Supplier Communication";
            this.Load += new System.EventHandler(this.SupplierCommunicationAddForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
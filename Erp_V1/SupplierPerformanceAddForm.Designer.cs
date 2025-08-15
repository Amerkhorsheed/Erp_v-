using MaterialSkin.Controls;
using System.ComponentModel;
using System.Windows.Forms;

namespace Erp_V1
{
    partial class SupplierPerformanceAddForm
    {
        private IContainer components = null;
        private MaterialLabel lblDate;
        private DateTimePicker dtpDate;
        private MaterialLabel lblScore;
        private NumericUpDown numScore;
        private MaterialLabel lblParameters;
        private MaterialTextBox txtParameters;
        private MaterialLabel lblComments;
        private TextBox txtComments;
        private MaterialButton btnSave;
        private MaterialButton btnCancel;
        private ErrorProvider errorProvider;

        // New controls for supplier selection
        private MaterialLabel lblSupplier;
        private MaterialComboBox cmbSupplier;


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblDate = new MaterialSkin.Controls.MaterialLabel();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.lblScore = new MaterialSkin.Controls.MaterialLabel();
            this.numScore = new System.Windows.Forms.NumericUpDown();
            this.lblParameters = new MaterialSkin.Controls.MaterialLabel();
            this.txtParameters = new MaterialSkin.Controls.MaterialTextBox();
            this.lblComments = new MaterialSkin.Controls.MaterialLabel();
            this.txtComments = new System.Windows.Forms.TextBox();
            this.btnSave = new MaterialSkin.Controls.MaterialButton();
            this.btnCancel = new MaterialSkin.Controls.MaterialButton();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblSupplier = new MaterialSkin.Controls.MaterialLabel(); // New line
            this.cmbSupplier = new MaterialSkin.Controls.MaterialComboBox(); // New line
            ((System.ComponentModel.ISupportInitialize)(this.numScore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            //
            // lblSupplier
            //
            this.lblSupplier.Depth = 0;
            this.lblSupplier.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblSupplier.Location = new System.Drawing.Point(30, 80); // Adjusted position
            this.lblSupplier.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(100, 23);
            this.lblSupplier.TabIndex = 0; // Adjusted tab index
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
            this.cmbSupplier.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.cmbSupplier.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbSupplier.FormattingEnabled = true;
            this.cmbSupplier.Hint = "";
            this.cmbSupplier.IntegralHeight = false;
            this.cmbSupplier.ItemHeight = 43;
            this.cmbSupplier.Location = new System.Drawing.Point(150, 75); // Adjusted position
            this.cmbSupplier.MaxDropDownItems = 4;
            this.cmbSupplier.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbSupplier.Name = "cmbSupplier";
            this.cmbSupplier.Size = new System.Drawing.Size(300, 49);
            this.cmbSupplier.StartIndex = 0;
            this.cmbSupplier.TabIndex = 1; // Adjusted tab index
            this.cmbSupplier.Validating += new System.ComponentModel.CancelEventHandler(this.cmbSupplier_Validating); // New event handler

            //
            // lblDate (Adjusted for new control)
            //
            this.lblDate.Depth = 0;
            this.lblDate.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblDate.Location = new System.Drawing.Point(30, 140); // Shifted down
            this.lblDate.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(100, 23);
            this.lblDate.TabIndex = 2; // Adjusted tab index
            this.lblDate.Text = "Date:";
            //
            // dtpDate (Adjusted for new control)
            //
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(150, 135); // Shifted down
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(200, 22);
            this.dtpDate.TabIndex = 3; // Adjusted tab index
            //
            // lblScore (Adjusted for new control)
            //
            this.lblScore.Depth = 0;
            this.lblScore.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblScore.Location = new System.Drawing.Point(33, 190); // Shifted down
            this.lblScore.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(97, 39);
            this.lblScore.TabIndex = 4; // Adjusted tab index
            this.lblScore.Text = "Score (0–100):";
            //
            // numScore (Adjusted for new control)
            //
            this.numScore.Location = new System.Drawing.Point(150, 185); // Shifted down
            this.numScore.Name = "numScore";
            this.numScore.Size = new System.Drawing.Size(120, 22);
            this.numScore.TabIndex = 5; // Adjusted tab index
            this.numScore.Validating += new System.ComponentModel.CancelEventHandler(this.numScore_Validating);
            //
            // lblParameters (Adjusted for new control)
            //
            this.lblParameters.Depth = 0;
            this.lblParameters.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblParameters.Location = new System.Drawing.Point(30, 240); // Shifted down
            this.lblParameters.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblParameters.Name = "lblParameters";
            this.lblParameters.Size = new System.Drawing.Size(100, 23);
            this.lblParameters.TabIndex = 6; // Adjusted tab index
            this.lblParameters.Text = "Parameters:";
            //
            // txtParameters (Adjusted for new control)
            //
            this.txtParameters.AnimateReadOnly = false;
            this.txtParameters.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtParameters.Depth = 0;
            this.txtParameters.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtParameters.LeadingIcon = null;
            this.txtParameters.Location = new System.Drawing.Point(150, 235); // Shifted down
            this.txtParameters.MaxLength = 50;
            this.txtParameters.MouseState = MaterialSkin.MouseState.OUT;
            this.txtParameters.Multiline = false;
            this.txtParameters.Name = "txtParameters";
            this.txtParameters.Size = new System.Drawing.Size(300, 50);
            this.txtParameters.TabIndex = 7; // Adjusted tab index
            this.txtParameters.Text = "";
            this.txtParameters.TrailingIcon = null;
            this.txtParameters.Validating += new System.ComponentModel.CancelEventHandler(this.txtParameters_Validating);
            //
            // lblComments (Adjusted for new control)
            //
            this.lblComments.Depth = 0;
            this.lblComments.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblComments.Location = new System.Drawing.Point(30, 300); // Shifted down
            this.lblComments.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblComments.Name = "lblComments";
            this.lblComments.Size = new System.Drawing.Size(100, 23);
            this.lblComments.TabIndex = 8; // Adjusted tab index
            this.lblComments.Text = "Comments:";
            //
            // txtComments (Adjusted for new control)
            //
            this.txtComments.Location = new System.Drawing.Point(150, 295); // Shifted down
            this.txtComments.Multiline = true;
            this.txtComments.Name = "txtComments";
            this.txtComments.Size = new System.Drawing.Size(300, 100);
            this.txtComments.TabIndex = 9; // Adjusted tab index
            //
            // btnSave (Adjusted for new control)
            //
            this.btnSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSave.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnSave.Depth = 0;
            this.btnSave.HighEmphasis = true;
            this.btnSave.Icon = null;
            this.btnSave.Location = new System.Drawing.Point(150, 420); // Shifted down
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnSave.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnSave.Name = "btnSave";
            this.btnSave.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnSave.Size = new System.Drawing.Size(64, 36);
            this.btnSave.TabIndex = 10; // Adjusted tab index
            this.btnSave.Text = "Save";
            this.btnSave.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnSave.UseAccentColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            //
            // btnCancel (Adjusted for new control)
            //
            this.btnCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCancel.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnCancel.Depth = 0;
            this.btnCancel.HighEmphasis = true;
            this.btnCancel.Icon = null;
            this.btnCancel.Location = new System.Drawing.Point(300, 420); // Shifted down
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnCancel.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnCancel.Size = new System.Drawing.Size(77, 36);
            this.btnCancel.TabIndex = 11; // Adjusted tab index
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnCancel.UseAccentColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            //
            // errorProvider
            //
            this.errorProvider.ContainerControl = this;
            //
            // SupplierPerformanceAddForm
            //
            this.ClientSize = new System.Drawing.Size(500, 480); // Increased form height to accommodate new controls
            this.Controls.Add(this.lblSupplier); // Add new control
            this.Controls.Add(this.cmbSupplier); // Add new control
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.numScore);
            this.Controls.Add(this.lblParameters);
            this.Controls.Add(this.txtParameters);
            this.Controls.Add(this.lblComments);
            this.Controls.Add(this.txtComments);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SupplierPerformanceAddForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Supplier Performance";
            this.Load += new System.EventHandler(this.SupplierPerformanceAddForm_Load); // Ensure this is present
            ((System.ComponentModel.ISupportInitialize)(this.numScore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
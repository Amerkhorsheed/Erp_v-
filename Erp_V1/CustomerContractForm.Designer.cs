// File: CustomerContractForm.Designer.cs
using MaterialSkin.Controls;
using System.Drawing;
using System.Windows.Forms;

namespace Erp_V1
{
    partial class CustomerContractForm
    {
        private System.ComponentModel.IContainer components = null;
        private MaterialCard panelTop;
        private MaterialLabel searchLabel;
        private MaterialTextBox searchTextBox;
        private MaterialButton btnAdd;
        private MaterialButton btnEdit;
        private MaterialButton btnDelete;
        private MaterialButton btnRefresh;
        private DataGridView dgvContracts;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelTop = new MaterialCard();
            this.searchLabel = new MaterialLabel();
            this.searchTextBox = new MaterialTextBox();
            this.btnRefresh = new MaterialButton();
            this.btnDelete = new MaterialButton();
            this.btnEdit = new MaterialButton();
            this.btnAdd = new MaterialButton();
            this.dgvContracts = new DataGridView();

            // 
            // panelTop
            // 
            this.panelTop.BackColor = Color.White;
            this.panelTop.Depth = 0;
            this.panelTop.Dock = DockStyle.Top;
            this.panelTop.Padding = new Padding(16, 8, 16, 8);
            this.panelTop.Size = new Size(800, 90);
            this.panelTop.Controls.Add(this.searchLabel);
            this.panelTop.Controls.Add(this.searchTextBox);
            this.panelTop.Controls.Add(this.btnAdd);
            this.panelTop.Controls.Add(this.btnEdit);
            this.panelTop.Controls.Add(this.btnDelete);
            this.panelTop.Controls.Add(this.btnRefresh);

            // 
            // searchLabel
            // 
            this.searchLabel.Depth = 0;
            this.searchLabel.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            this.searchLabel.Location = new Point(16, 24);
            this.searchLabel.Size = new Size(140, 36);
            this.searchLabel.Text = "Search Customer:";
            this.searchLabel.TextAlign = ContentAlignment.MiddleLeft;

            // 
            // searchTextBox
            // 
            this.searchTextBox.BorderStyle = BorderStyle.None;
            this.searchTextBox.Depth = 0;
            this.searchTextBox.Location = new Point(162, 24);
            this.searchTextBox.Size = new Size(240, 36);
            this.searchTextBox.TextChanged += new System.EventHandler(this.searchTextBox_TextChanged);

            // 
            // btnAdd
            // 
            this.btnAdd.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.btnAdd.Depth = 0;
            this.btnAdd.Location = new Point(450, 24);
            this.btnAdd.Size = new Size(62, 36);
            this.btnAdd.Text = "Add";
            this.btnAdd.Type = MaterialButton.MaterialButtonType.Contained;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            // 
            // btnEdit
            // 
            this.btnEdit.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.btnEdit.Depth = 0;
            this.btnEdit.Location = new Point(522, 24);
            this.btnEdit.Size = new Size(62, 36);
            this.btnEdit.Text = "Edit";
            this.btnEdit.Type = MaterialButton.MaterialButtonType.Outlined;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);

            // 
            // btnDelete
            // 
            this.btnDelete.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.btnDelete.Depth = 0;
            this.btnDelete.Location = new Point(594, 24);
            this.btnDelete.Size = new Size(78, 36);
            this.btnDelete.Text = "Delete";
            this.btnDelete.Type = MaterialButton.MaterialButtonType.Contained;
            this.btnDelete.UseAccentColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // 
            // btnRefresh
            // 
            this.btnRefresh.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.btnRefresh.Depth = 0;
            this.btnRefresh.Location = new Point(682, 24);
            this.btnRefresh.Size = new Size(91, 36);
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Type = MaterialButton.MaterialButtonType.Text;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // 
            // dgvContracts
            // 
            this.dgvContracts.Dock = DockStyle.Fill; // This line makes the DGV fill its parent container
            this.dgvContracts.Location = new Point(0, 90);
            this.dgvContracts.ReadOnly = true;
            this.dgvContracts.AllowUserToAddRows = false;
            this.dgvContracts.AllowUserToDeleteRows = false;
            this.dgvContracts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvContracts.MultiSelect = false;
            this.dgvContracts.EnableHeadersVisualStyles = false;
            this.dgvContracts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvContracts.BackgroundColor = Color.White;
            this.dgvContracts.BorderStyle = BorderStyle.None;
            this.dgvContracts.GridColor = Color.Gainsboro;
            this.dgvContracts.RowHeadersVisible = false;

            // 
            // CustomerContractForm
            // 
            this.ClientSize = new Size(800, 450);
            this.Controls.Add(this.dgvContracts);
            this.Controls.Add(this.panelTop);
            this.Text = "Customer Contracts";
            this.Load += new System.EventHandler(this.CustomerContractForm_Load);
        }
    }
}

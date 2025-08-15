// File: CustomerInteractionForm.Designer.cs
using MaterialSkin.Controls;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Erp_V1
{
    partial class CustomerInteractionForm
    {
        private IContainer components = null;
        private MaterialCard panelTop;
        private MaterialLabel searchLabel;
        private MaterialTextBox searchTextBox;
        private MaterialButton btnAdd;
        private MaterialButton btnEdit;
        private MaterialButton btnDelete;
        private MaterialButton btnRefresh;
        private DataGridView dgvInteractions;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new Container();
            panelTop = new MaterialCard();
            searchLabel = new MaterialLabel();
            searchTextBox = new MaterialTextBox();
            btnAdd = new MaterialButton();
            btnEdit = new MaterialButton();
            btnDelete = new MaterialButton();
            btnRefresh = new MaterialButton();
            dgvInteractions = new DataGridView();

            // panelTop
            panelTop.BackColor = Color.White;
            panelTop.Depth = 0;
            panelTop.Dock = DockStyle.Top;
            panelTop.Padding = new Padding(16, 8, 16, 8);
            panelTop.Size = new Size(800, 90);
            panelTop.Controls.Add(searchLabel);
            panelTop.Controls.Add(searchTextBox);
            panelTop.Controls.Add(btnAdd);
            panelTop.Controls.Add(btnEdit);
            panelTop.Controls.Add(btnDelete);
            panelTop.Controls.Add(btnRefresh);

            // searchLabel
            searchLabel.Depth = 0;
            searchLabel.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            searchLabel.Location = new Point(16, 24);
            searchLabel.Size = new Size(140, 36);
            searchLabel.Text = "Search Customer:";
            searchLabel.TextAlign = ContentAlignment.MiddleLeft;

            // searchTextBox
            searchTextBox.BorderStyle = BorderStyle.None;
            searchTextBox.Depth = 0;
            searchTextBox.Location = new Point(162, 24);
            searchTextBox.Size = new Size(240, 36);
            searchTextBox.TextChanged += new System.EventHandler(this.searchTextBox_TextChanged);

            // btnAdd
            btnAdd.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnAdd.Depth = 0;
            btnAdd.Location = new Point(420, 24);
            btnAdd.Size = new Size(62, 36);
            btnAdd.Text = "Add";
            btnAdd.Type = MaterialButton.MaterialButtonType.Contained;
            btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            // btnEdit
            btnEdit.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnEdit.Depth = 0;
            btnEdit.Location = new Point(492, 24);
            btnEdit.Size = new Size(62, 36);
            btnEdit.Text = "Edit";
            btnEdit.Type = MaterialButton.MaterialButtonType.Outlined;
            btnEdit.Click += new System.EventHandler(this.btnEdit_Click);

            // btnDelete
            btnDelete.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnDelete.Depth = 0;
            btnDelete.Location = new Point(564, 24);
            btnDelete.Size = new Size(78, 36);
            btnDelete.Text = "Delete";
            btnDelete.Type = MaterialButton.MaterialButtonType.Contained;
            btnDelete.UseAccentColor = true;
            btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // btnRefresh
            btnRefresh.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnRefresh.Depth = 0;
            btnRefresh.Location = new Point(650, 24);
            btnRefresh.Size = new Size(91, 36);
            btnRefresh.Text = "Refresh";
            btnRefresh.Type = MaterialButton.MaterialButtonType.Text;
            btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // dgvInteractions
            dgvInteractions.Dock = DockStyle.Fill;
            dgvInteractions.Location = new Point(0, 90);
            dgvInteractions.ReadOnly = true;
            dgvInteractions.AllowUserToAddRows = false;
            dgvInteractions.AllowUserToDeleteRows = false;
            dgvInteractions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInteractions.MultiSelect = false;
            dgvInteractions.EnableHeadersVisualStyles = false;
            dgvInteractions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvInteractions.BackgroundColor = Color.White;
            dgvInteractions.BorderStyle = BorderStyle.None;
            dgvInteractions.GridColor = Color.Gainsboro;
            dgvInteractions.RowHeadersVisible = false;

            // CustomerInteractionForm
            ClientSize = new Size(800, 450);
            Controls.Add(dgvInteractions);
            Controls.Add(panelTop);
            Text = "Customer Interactions";
            Load += new System.EventHandler(this.CustomerInteractionForm_Load);
        }
    }
}

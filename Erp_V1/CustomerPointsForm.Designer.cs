// File: CustomerPointsForm.Designer.cs
using MaterialSkin.Controls;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Erp_V1
{
    partial class CustomerPointsForm
    {
        private IContainer components = null;
        private MaterialCard panelTop;
        private MaterialLabel searchLabel;
        private MaterialTextBox searchTextBox;
        private MaterialButton btnAdd;
        private MaterialButton btnEdit;
        private MaterialButton btnRefresh;
        private DataGridView dgvPoints;

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
            btnRefresh = new MaterialButton();
            dgvPoints = new DataGridView();

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
            btnAdd.Location = new Point(450, 24);
            btnAdd.Size = new Size(62, 36);
            btnAdd.Text = "Add";
            btnAdd.Type = MaterialButton.MaterialButtonType.Contained;
            btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            // btnEdit
            btnEdit.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnEdit.Depth = 0;
            btnEdit.Location = new Point(522, 24);
            btnEdit.Size = new Size(62, 36);
            btnEdit.Text = "Edit";
            btnEdit.Type = MaterialButton.MaterialButtonType.Outlined;
            btnEdit.Click += new System.EventHandler(this.btnEdit_Click);

            // btnRefresh
            btnRefresh.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnRefresh.Depth = 0;
            btnRefresh.Location = new Point(594, 24);
            btnRefresh.Size = new Size(91, 36);
            btnRefresh.Text = "Refresh";
            btnRefresh.Type = MaterialButton.MaterialButtonType.Text;
            btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // dgvPoints
            dgvPoints.Dock = DockStyle.Fill;
            dgvPoints.Location = new Point(0, 90);
            dgvPoints.ReadOnly = true;
            dgvPoints.AllowUserToAddRows = false;
            dgvPoints.AllowUserToDeleteRows = false;
            dgvPoints.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPoints.MultiSelect = false;
            dgvPoints.EnableHeadersVisualStyles = false;
            dgvPoints.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPoints.BackgroundColor = Color.White;
            dgvPoints.BorderStyle = BorderStyle.None;
            dgvPoints.GridColor = Color.Gainsboro;
            dgvPoints.RowHeadersVisible = false;

            // CustomerPointsForm
            ClientSize = new Size(800, 450);
            Controls.Add(dgvPoints);
            Controls.Add(panelTop);
            Text = "Customer Points";
            Load += new System.EventHandler(this.CustomerPointsForm_Load);
        }
    }
}

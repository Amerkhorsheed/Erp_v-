// File: SupplierPerformanceForm.Designer.cs
using MaterialSkin.Controls;
using System.ComponentModel;
using System.Windows.Forms;

namespace Erp_V1
{
    partial class SupplierPerformanceForm
    {
        private IContainer components = null;
        private MaterialCard panelTop;
        private MaterialLabel lblSearch;
        private MaterialTextBox txtSearch;
        private MaterialButton btnAdd;
        private MaterialButton btnEdit;
        private MaterialButton btnDelete;
        private MaterialButton btnRefresh;
        private DataGridView dgvPerformance;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelTop = new MaterialCard();
            this.lblSearch = new MaterialLabel();
            this.txtSearch = new MaterialTextBox();
            this.btnAdd = new MaterialButton();
            this.btnEdit = new MaterialButton();
            this.btnDelete = new MaterialButton();
            this.btnRefresh = new MaterialButton();
            this.dgvPerformance = new DataGridView();

            this.panelTop.SuspendLayout();
            ((ISupportInitialize)(this.dgvPerformance)).BeginInit();
            this.SuspendLayout();

            // panelTop
            this.panelTop.Dock = DockStyle.Top;
            this.panelTop.Padding = new Padding(16, 8, 16, 8);
            this.panelTop.Controls.Add(this.lblSearch);
            this.panelTop.Controls.Add(this.txtSearch);
            this.panelTop.Controls.Add(this.btnAdd);
            this.panelTop.Controls.Add(this.btnEdit);
            this.panelTop.Controls.Add(this.btnDelete);
            this.panelTop.Controls.Add(this.btnRefresh);
            this.panelTop.Size = new System.Drawing.Size(900, 100);

            // lblSearch
            this.lblSearch.Text = "Search:";
            this.lblSearch.Location = new System.Drawing.Point(20, 30);

            // txtSearch
            this.txtSearch.Location = new System.Drawing.Point(90, 20);
            this.txtSearch.Size = new System.Drawing.Size(220, 48);
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);

            // btnAdd
            this.btnAdd.Text = "Add";
            this.btnAdd.Location = new System.Drawing.Point(330, 20);
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            // btnEdit
            this.btnEdit.Text = "Edit";
            this.btnEdit.Location = new System.Drawing.Point(410, 20);
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);

            // btnDelete
            this.btnDelete.Text = "Delete";
            this.btnDelete.Location = new System.Drawing.Point(490, 20);
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // btnRefresh
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Location = new System.Drawing.Point(570, 20);
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // dgvPerformance
            this.dgvPerformance.Dock = DockStyle.Fill;
            this.dgvPerformance.Location = new System.Drawing.Point(0, 100);
            this.dgvPerformance.CellDoubleClick += new DataGridViewCellEventHandler(this.dgvPerformance_CellDoubleClick);

            // SupplierPerformanceForm
            this.ClientSize = new System.Drawing.Size(900, 500);
            this.Controls.Add(this.dgvPerformance);
            this.Controls.Add(this.panelTop);
            this.Text = "Supplier Performance";

            this.panelTop.ResumeLayout(false);
            ((ISupportInitialize)(this.dgvPerformance)).EndInit();
            this.ResumeLayout(false);
        }
    }
}

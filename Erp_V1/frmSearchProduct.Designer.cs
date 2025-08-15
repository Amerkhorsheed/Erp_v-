namespace Erp_V1
{
    partial class frmSearchProduct
    {
        private System.ComponentModel.IContainer components = null;
        private DevExpress.XtraBars.Docking.DockManager dockManager;
        private DevExpress.XtraBars.Docking.DockPanel dpSearch;
        private DevExpress.XtraBars.Docking.ControlContainer dpSearchContainer;
        private DevExpress.XtraEditors.TextEdit txtSearch;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.ListBoxControl lstSearchResults;
        private DevExpress.XtraBars.Docking.DockPanel dpDetails;
        private DevExpress.XtraBars.Docking.ControlContainer dpDetailsContainer;
        private DevExpress.XtraEditors.LabelControl lblProductName;
        private DevExpress.XtraEditors.LabelControl lblCategoryName;
        private DevExpress.XtraEditors.LabelControl lblPrice;
        private DevExpress.XtraEditors.LabelControl lblStockAmount;
        private DevExpress.XtraEditors.LabelControl lblProductNameValue;
        private DevExpress.XtraEditors.LabelControl lblCategoryNameValue;
        private DevExpress.XtraEditors.LabelControl lblPriceValue;
        private DevExpress.XtraEditors.LabelControl lblStockAmountValue;

        /// <summary>
        /// Clean up resources.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dockManager = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dpSearch = new DevExpress.XtraBars.Docking.DockPanel();
            this.dpSearchContainer = new DevExpress.XtraBars.Docking.ControlContainer();
            this.txtSearch = new DevExpress.XtraEditors.TextEdit();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.lstSearchResults = new DevExpress.XtraEditors.ListBoxControl();
            this.dpDetails = new DevExpress.XtraBars.Docking.DockPanel();
            this.dpDetailsContainer = new DevExpress.XtraBars.Docking.ControlContainer();
            this.lblProductName = new DevExpress.XtraEditors.LabelControl();
            this.lblCategoryName = new DevExpress.XtraEditors.LabelControl();
            this.lblPrice = new DevExpress.XtraEditors.LabelControl();
            this.lblStockAmount = new DevExpress.XtraEditors.LabelControl();
            this.lblProductNameValue = new DevExpress.XtraEditors.LabelControl();
            this.lblCategoryNameValue = new DevExpress.XtraEditors.LabelControl();
            this.lblPriceValue = new DevExpress.XtraEditors.LabelControl();
            this.lblStockAmountValue = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager)).BeginInit();
            this.dpSearch.SuspendLayout();
            this.dpSearchContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstSearchResults)).BeginInit();
            this.dpDetails.SuspendLayout();
            this.dpDetailsContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // dockManager
            // 
            this.dockManager.Form = this;
            // 
            // dpSearch
            // 
            this.dpSearch.Controls.Add(this.dpSearchContainer);
            this.dpSearch.Cursor = System.Windows.Forms.Cursors.Default;
            this.dpSearch.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dpSearch.ID = new System.Guid("6b0e27b1-ec56-49f4-8b4d-3a16f4e4c8b0");
            this.dpSearch.Location = new System.Drawing.Point(0, 0);
            this.dpSearch.Name = "dpSearch";
            this.dpSearch.OriginalSize = new System.Drawing.Size(300, 200);
            this.dpSearch.Size = new System.Drawing.Size(300, 450);
            this.dpSearch.Text = "Search Products";
            // 
            // dpSearchContainer
            // 
            this.dpSearchContainer.Controls.Add(this.txtSearch);
            this.dpSearchContainer.Controls.Add(this.btnSearch);
            this.dpSearchContainer.Controls.Add(this.lstSearchResults);
            this.dpSearchContainer.Location = new System.Drawing.Point(4, 30);
            this.dpSearchContainer.Name = "dpSearchContainer";
            this.dpSearchContainer.Size = new System.Drawing.Size(292, 416);
            this.dpSearchContainer.TabIndex = 0;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(15, 15);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.Properties.Appearance.Options.UseFont = true;
            this.txtSearch.Size = new System.Drawing.Size(260, 30);
            this.txtSearch.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(15, 55);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(260, 35);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lstSearchResults
            // 
            this.lstSearchResults.Location = new System.Drawing.Point(15, 100);
            this.lstSearchResults.Name = "lstSearchResults";
            this.lstSearchResults.Size = new System.Drawing.Size(260, 310);
            this.lstSearchResults.TabIndex = 2;
            this.lstSearchResults.SelectedIndexChanged += new System.EventHandler(this.lstSearchResults_SelectedIndexChanged);
            // 
            // dpDetails
            // 
            this.dpDetails.Controls.Add(this.dpDetailsContainer);
            this.dpDetails.Cursor = System.Windows.Forms.Cursors.Default;
            this.dpDetails.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dpDetails.ID = new System.Guid("15a3c3c7-07e2-4a82-87b2-b1a2a4a5a9bb");
            this.dpDetails.Location = new System.Drawing.Point(300, 0);
            this.dpDetails.Name = "dpDetails";
            this.dpDetails.OriginalSize = new System.Drawing.Size(300, 200);
            this.dpDetails.Size = new System.Drawing.Size(500, 450);
            this.dpDetails.Text = "Product Details";
            // 
            // dpDetailsContainer
            // 
            this.dpDetailsContainer.Controls.Add(this.lblProductName);
            this.dpDetailsContainer.Controls.Add(this.lblCategoryName);
            this.dpDetailsContainer.Controls.Add(this.lblPrice);
            this.dpDetailsContainer.Controls.Add(this.lblStockAmount);
            this.dpDetailsContainer.Controls.Add(this.lblProductNameValue);
            this.dpDetailsContainer.Controls.Add(this.lblCategoryNameValue);
            this.dpDetailsContainer.Controls.Add(this.lblPriceValue);
            this.dpDetailsContainer.Controls.Add(this.lblStockAmountValue);
            this.dpDetailsContainer.Location = new System.Drawing.Point(4, 30);
            this.dpDetailsContainer.Name = "dpDetailsContainer";
            this.dpDetailsContainer.Size = new System.Drawing.Size(492, 416);
            this.dpDetailsContainer.TabIndex = 0;
            // 
            // lblProductName
            // 
            this.lblProductName.Location = new System.Drawing.Point(20, 20);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(89, 16);
            this.lblProductName.TabIndex = 0;
            this.lblProductName.Text = "Product Name:";
            // 
            // lblCategoryName
            // 
            this.lblCategoryName.Location = new System.Drawing.Point(20, 60);
            this.lblCategoryName.Name = "lblCategoryName";
            this.lblCategoryName.Size = new System.Drawing.Size(98, 16);
            this.lblCategoryName.TabIndex = 1;
            this.lblCategoryName.Text = "Category Name:";
            // 
            // lblPrice
            // 
            this.lblPrice.Location = new System.Drawing.Point(20, 100);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(34, 16);
            this.lblPrice.TabIndex = 2;
            this.lblPrice.Text = "Price:";
            // 
            // lblStockAmount
            // 
            this.lblStockAmount.Location = new System.Drawing.Point(20, 140);
            this.lblStockAmount.Name = "lblStockAmount";
            this.lblStockAmount.Size = new System.Drawing.Size(85, 16);
            this.lblStockAmount.TabIndex = 3;
            this.lblStockAmount.Text = "Stock Amount:";
            // 
            // lblProductNameValue
            // 
            this.lblProductNameValue.Location = new System.Drawing.Point(150, 20);
            this.lblProductNameValue.Name = "lblProductNameValue";
            this.lblProductNameValue.Size = new System.Drawing.Size(0, 16);
            this.lblProductNameValue.TabIndex = 4;
            // 
            // lblCategoryNameValue
            // 
            this.lblCategoryNameValue.Location = new System.Drawing.Point(150, 60);
            this.lblCategoryNameValue.Name = "lblCategoryNameValue";
            this.lblCategoryNameValue.Size = new System.Drawing.Size(0, 16);
            this.lblCategoryNameValue.TabIndex = 5;
            // 
            // lblPriceValue
            // 
            this.lblPriceValue.Location = new System.Drawing.Point(150, 100);
            this.lblPriceValue.Name = "lblPriceValue";
            this.lblPriceValue.Size = new System.Drawing.Size(0, 16);
            this.lblPriceValue.TabIndex = 6;
            // 
            // lblStockAmountValue
            // 
            this.lblStockAmountValue.Location = new System.Drawing.Point(150, 140);
            this.lblStockAmountValue.Name = "lblStockAmountValue";
            this.lblStockAmountValue.Size = new System.Drawing.Size(0, 16);
            this.lblStockAmountValue.TabIndex = 7;
            // 
            // frmSearchProduct
            // 
            this.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Appearance.Options.UseBackColor = true;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dpDetails);
            this.Controls.Add(this.dpSearch);
            this.MaximizeBox = false;
            this.Name = "frmSearchProduct";
            this.Text = "Product Search & Details";
            ((System.ComponentModel.ISupportInitialize)(this.dockManager)).EndInit();
            this.dpSearch.ResumeLayout(false);
            this.dpSearchContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstSearchResults)).EndInit();
            this.dpDetails.ResumeLayout(false);
            this.dpDetailsContainer.ResumeLayout(false);
            this.dpDetailsContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
    }
}

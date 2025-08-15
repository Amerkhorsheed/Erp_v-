using Erp_V1.BLL;
using Erp_V1.DAL.DTO;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Erp_V1
{
    public partial class ExpensesListForm : MaterialForm
    {
        private readonly ExpensesBLL _bll = new ExpensesBLL();
        private ExpensesDTO _dto = new ExpensesDTO();

        public ExpensesListForm()
        {
            InitializeComponent();
            InitializeMaterialSkin();
        }

        private void InitializeMaterialSkin()
        {
            var skinManager = MaterialSkinManager.Instance;
            skinManager.AddFormToManage(this);
            skinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            skinManager.ColorScheme = new ColorScheme(Primary.Teal500, Primary.Teal700, Primary.Teal100, Accent.Red400, TextShade.WHITE);
        }

        private void ExpensesListForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private async void LoadData()
        {
            try
            {
                // First, clear the existing data source to prevent errors and refresh the grid
                dataGridView.DataSource = null;

                // Fetch the data from the business logic layer asynchronously
                _dto = await Task.Run(() => _bll.Select());

                // Check if the returned data is valid and has items
                if (_dto?.Expenses != null && _dto.Expenses.Any())
                {
                    // If data exists, bind it to the grid
                    dataGridView.DataSource = _dto.Expenses;

                    #region Safely Configure Columns
                    // It's safer to check if a column exists before accessing it.

                    // Hide unnecessary ID and audit columns
                    if (dataGridView.Columns[nameof(ExpensesDetailDTO.ID)] != null) dataGridView.Columns[nameof(ExpensesDetailDTO.ID)].Visible = false;
                    if (dataGridView.Columns[nameof(ExpensesDetailDTO.CategoryID)] != null) dataGridView.Columns[nameof(ExpensesDetailDTO.CategoryID)].Visible = false;
                    if (dataGridView.Columns[nameof(ExpensesDetailDTO.RequestedBy)] != null) dataGridView.Columns[nameof(ExpensesDetailDTO.RequestedBy)].Visible = false;
                    if (dataGridView.Columns[nameof(ExpensesDetailDTO.ApprovedBy)] != null) dataGridView.Columns[nameof(ExpensesDetailDTO.ApprovedBy)].Visible = false;
                    if (dataGridView.Columns[nameof(ExpensesDetailDTO.IsDeleted)] != null) dataGridView.Columns[nameof(ExpensesDetailDTO.IsDeleted)].Visible = false;
                    if (dataGridView.Columns[nameof(ExpensesDetailDTO.CreatedBy)] != null) dataGridView.Columns[nameof(ExpensesDetailDTO.CreatedBy)].Visible = false;
                    if (dataGridView.Columns[nameof(ExpensesDetailDTO.ModifiedBy)] != null) dataGridView.Columns[nameof(ExpensesDetailDTO.ModifiedBy)].Visible = false;
                    if (dataGridView.Columns[nameof(ExpensesDetailDTO.DeletedBy)] != null) dataGridView.Columns[nameof(ExpensesDetailDTO.DeletedBy)].Visible = false;
                    if (dataGridView.Columns[nameof(ExpensesDetailDTO.DeletedDate)] != null) dataGridView.Columns[nameof(ExpensesDetailDTO.DeletedDate)].Visible = false;
                    if (dataGridView.Columns[nameof(ExpensesDetailDTO.CreatedDate)] != null) dataGridView.Columns[nameof(ExpensesDetailDTO.CreatedDate)].Visible = false;
                    if (dataGridView.Columns[nameof(ExpensesDetailDTO.ModifiedDate)] != null) dataGridView.Columns[nameof(ExpensesDetailDTO.ModifiedDate)].Visible = false;
                    if (dataGridView.Columns[nameof(ExpensesDetailDTO.RequestedDate)] != null) dataGridView.Columns[nameof(ExpensesDetailDTO.RequestedDate)].Visible = false;
                    if (dataGridView.Columns[nameof(ExpensesDetailDTO.ApprovedDate)] != null) dataGridView.Columns[nameof(ExpensesDetailDTO.ApprovedDate)].Visible = false;
                    if (dataGridView.Columns[nameof(ExpensesDetailDTO.ApprovalComment)] != null) dataGridView.Columns[nameof(ExpensesDetailDTO.ApprovalComment)].Visible = false;
                    if (dataGridView.Columns[nameof(ExpensesDetailDTO.AttachmentPath)] != null) dataGridView.Columns[nameof(ExpensesDetailDTO.AttachmentPath)].Visible = false;
                    if (dataGridView.Columns[nameof(ExpensesDetailDTO.Note)] != null) dataGridView.Columns[nameof(ExpensesDetailDTO.Note)].Visible = false;

                    // Configure the headers and formatting of visible columns
                    if (dataGridView.Columns[nameof(ExpensesDetailDTO.ExpensesName)] != null) dataGridView.Columns[nameof(ExpensesDetailDTO.ExpensesName)].HeaderText = "Expense Title";
                    if (dataGridView.Columns[nameof(ExpensesDetailDTO.CategoryName)] != null) dataGridView.Columns[nameof(ExpensesDetailDTO.CategoryName)].HeaderText = "Category";
                    if (dataGridView.Columns[nameof(ExpensesDetailDTO.Status)] != null) dataGridView.Columns[nameof(ExpensesDetailDTO.Status)].HeaderText = "Status";
                    if (dataGridView.Columns[nameof(ExpensesDetailDTO.Amount)] != null) dataGridView.Columns[nameof(ExpensesDetailDTO.Amount)].DefaultCellStyle.Format = "C2";
                    if (dataGridView.Columns[nameof(ExpensesDetailDTO.ExpenseDate)] != null) dataGridView.Columns[nameof(ExpensesDetailDTO.ExpenseDate)].HeaderText = "Date";
                    if (dataGridView.Columns[nameof(ExpensesDetailDTO.RequestedByName)] != null) dataGridView.Columns[nameof(ExpensesDetailDTO.RequestedByName)].HeaderText = "Requested By";
                    if (dataGridView.Columns[nameof(ExpensesDetailDTO.ApprovedByName)] != null) dataGridView.Columns[nameof(ExpensesDetailDTO.ApprovedByName)].HeaderText = "Approved By";

                    #endregion
                }
            }
            catch (Exception ex)
            {
                // If any error occurs during the process, it will be caught and displayed.
                MessageBox.Show($"A critical error occurred while loading data: {ex.Message}", "Loading Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var form = new ExpensesAddForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadData(); // Refresh data after adding
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an expense to update.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedDto = (ExpensesDetailDTO)dataGridView.SelectedRows[0].DataBoundItem;

            using (var form = new ExpensesAddForm(selectedDto))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadData(); // Refresh data after updating
                }
            }
        }

        private async void btnApprove_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0) return;
            var selectedDto = (ExpensesDetailDTO)dataGridView.SelectedRows[0].DataBoundItem;

            if (selectedDto.Status != "Pending")
            {
                MessageBox.Show("Only pending expenses can be approved.", "Workflow Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show($"Are you sure you want to approve the expense '{selectedDto.ExpensesName}'?", "Confirm Approval", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                // In a real app, ApprovedBy would be the current logged-in user ID
                await Task.Run(() => _bll.UpdateStatus(selectedDto.ID, "Approved", 1));
                LoadData();
            }
        }

        private async void btnReject_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0) return;
            var selectedDto = (ExpensesDetailDTO)dataGridView.SelectedRows[0].DataBoundItem;

            if (selectedDto.Status != "Pending")
            {
                MessageBox.Show("Only pending expenses can be rejected.", "Workflow Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show($"Are you sure you want to reject the expense '{selectedDto.ExpensesName}'?", "Confirm Rejection", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                // In a real app, ApprovedBy would be the current logged-in user ID
                await Task.Run(() => _bll.UpdateStatus(selectedDto.ID, "Rejected", 1));
                LoadData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an expense to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var selectedDto = (ExpensesDetailDTO)dataGridView.SelectedRows[0].DataBoundItem;

            var result = MessageBox.Show($"Are you sure you want to delete the expense '{selectedDto.ExpensesName}'?\nThis action is a soft delete and can be undone by an administrator.", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                // In a real app, this would be the logged-in user's ID
                selectedDto.DeletedBy = 1;

                if (_bll.Delete(selectedDto))
                {
                    MessageBox.Show("Expense deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData(); // Refresh the grid
                }
                else
                {
                    MessageBox.Show("Failed to delete the expense.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                // Ensure the selection is a valid data row
                if (dataGridView.SelectedRows[0].DataBoundItem is ExpensesDetailDTO selectedDto)
                {
                    bool isPending = selectedDto.Status == "Pending";
                    btnApprove.Enabled = isPending;
                    btnReject.Enabled = isPending;
                }
            }
            else
            {
                btnApprove.Enabled = false;
                btnReject.Enabled = false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
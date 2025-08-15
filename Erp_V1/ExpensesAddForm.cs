// File: ExpensesAddForm.cs
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Erp_V1
{
    public partial class ExpensesAddForm : MaterialForm
    {
        private readonly ExpensesBLL _bll = new ExpensesBLL();
        private ExpensesDetailDTO _dto;
        private readonly bool _isEditMode;

        public ExpensesAddForm()
        {
            InitializeComponent();
            InitializeMaterialSkin();
            _isEditMode = false;
            this.KeyPreview = true;
            this.KeyDown += ExpensesAddForm_KeyDown;
        }

        public ExpensesAddForm(ExpensesDetailDTO dtoToEdit) : this()
        {
            _isEditMode = true;
            _dto = dtoToEdit;
            this.Text = "Edit Expense";
        }

        private void InitializeMaterialSkin()
        {
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.BlueGrey800,
                Primary.BlueGrey900,
                Primary.BlueGrey500,
                Accent.LightBlue200,
                TextShade.WHITE
            );
        }

        private async void ExpensesAddForm_Load(object sender, EventArgs e)
        {
            // Fetch all the lookup data (categories, currencies, statuses)
            var lookupData = await Task.Run(() => _bll.Select());

            // Populate the dropdowns that are the same for both modes
            categoryComboBox.DisplayMember = nameof(CategoryExpensesDetailDTO.CategoryName);
            categoryComboBox.ValueMember = nameof(CategoryExpensesDetailDTO.ID);
            categoryComboBox.DataSource = lookupData.Categories;

            currencyComboBox.DataSource = lookupData.CurrencyCodes;

            // ** THIS IS THE CORRECTED LOGIC FOR THE STATUS COMBOBOX **
            // Always populate the status dropdown with the full list
            statusComboBox.DataSource = lookupData.StatusList;

            if (_isEditMode)
            {
                // In edit mode, populate all fields from the DTO
                PopulateFields();
                // And ensure the user can change the status
                statusComboBox.Enabled = true;
            }
            else
            {
                // In new mode, set defaults
                expenseDatePicker.Value = DateTime.Today;
                // Default the status to "Pending" and disable the dropdown
                statusComboBox.SelectedItem = "Pending";
                statusComboBox.Enabled = false;
            }
        }

        private void PopulateFields()
        {
            expenseNameTextBox.Text = _dto.ExpensesName;
            categoryComboBox.SelectedValue = _dto.CategoryID;
            expenseDatePicker.Value = _dto.ExpenseDate;
            amountTextBox.Text = _dto.Amount.ToString("F2");
            currencyComboBox.SelectedItem = _dto.CurrencyCode;
            noteTextBox.Text = _dto.Note;
            attachmentTextBox.Text = _dto.AttachmentPath;
            // ** THIS LINE IS NEW **: Set the selected status
            statusComboBox.SelectedItem = _dto.Status;
        }

        #region Validation (No Changes Here)
        private void expenseNameTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(expenseNameTextBox.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(expenseNameTextBox, "Expense name is required.");
            }
            else
            {
                errorProvider.SetError(expenseNameTextBox, "");
            }
        }

        private void categoryComboBox_Validating(object sender, CancelEventArgs e)
        {
            if (categoryComboBox.SelectedIndex < 0)
            {
                e.Cancel = true;
                errorProvider.SetError(categoryComboBox, "Please select a category.");
            }
            else
            {
                errorProvider.SetError(categoryComboBox, "");
            }
        }

        private void amountTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (!decimal.TryParse(amountTextBox.Text, out var amount) || amount <= 0)
            {
                e.Cancel = true;
                errorProvider.SetError(amountTextBox, "Please enter a valid amount greater than zero.");
            }
            else
            {
                errorProvider.SetError(amountTextBox, "");
            }
        }
        #endregion

        private void ExpensesAddForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !noteTextBox.Focused)
            {
                e.SuppressKeyPress = true;
                saveButton.PerformClick();
            }
        }

        private async void saveButton_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                MessageBox.Show("Please correct validation errors before saving.", "Validation Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dto = _isEditMode ? _dto : new ExpensesDetailDTO();
            dto.ExpensesName = expenseNameTextBox.Text.Trim();
            dto.CategoryID = (int)categoryComboBox.SelectedValue;
            dto.ExpenseDate = expenseDatePicker.Value.Date;
            dto.Amount = decimal.Parse(amountTextBox.Text);
            dto.CurrencyCode = currencyComboBox.SelectedItem.ToString();
            dto.Note = noteTextBox.Text.Trim();
            dto.AttachmentPath = attachmentTextBox.Text.Trim();
            // ** THIS LINE IS NEW **: Read the selected status from the combobox
            dto.Status = statusComboBox.SelectedItem.ToString();

            // In a real app, CreatedBy/ModifiedBy would be set based on the logged-in user
            dto.CreatedBy = 1; // Example user ID
            dto.ModifiedBy = 1; // Example user ID

            saveButton.Enabled = false;
            bool success;

            try
            {
                success = _isEditMode
                    ? await Task.Run(() => _bll.Update(dto))
                    : await Task.Run(() => _bll.Insert(dto));
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder($"An error occurred: {ex.Message}");
                var inner = ex.InnerException;
                while (inner != null)
                {
                    sb.AppendLine($"\nInner Exception: {inner.Message}");
                    inner = inner.InnerException;
                }
                MessageBox.Show(sb.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                saveButton.Enabled = true;
                return;
            }

            saveButton.Enabled = true;

            if (success)
            {
                MessageBox.Show("Expense saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("The operation failed to save.", "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "All Files (*.*)|*.*|PDF Documents (*.pdf)|*.pdf|Images (*.jpg;*.png)|*.jpg;*.png";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    attachmentTextBox.Text = ofd.FileName;
                }
            }
        }
    }
}
// File: CategoryExpensesAddForm.cs
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.ComponentModel;
using System.Threading.Tasks; // <--- FIX IS HERE
using System.Windows.Forms;

namespace Erp_V1
{
    public partial class CategoryExpensesAddForm : MaterialForm
    {
        private readonly CategoryExpensesBLL _bll = new CategoryExpensesBLL();
        private CategoryExpensesDetailDTO _dto;
        private readonly bool _isEditMode;

        public CategoryExpensesAddForm()
        {
            InitializeComponent();
            InitializeMaterialSkin();
            _isEditMode = false;
            this.Text = "Add Expense Category";
        }

        public CategoryExpensesAddForm(CategoryExpensesDetailDTO dtoToEdit) : this()
        {
            _isEditMode = true;
            _dto = dtoToEdit;
            this.Text = "Edit Expense Category";
            PopulateFields();
        }

        private void InitializeMaterialSkin()
        {
            var skinManager = MaterialSkinManager.Instance;
            skinManager.AddFormToManage(this);
            skinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            skinManager.ColorScheme = new ColorScheme(Primary.Indigo500, Primary.Indigo700, Primary.Indigo100, Accent.Pink200, TextShade.WHITE);
        }

        private void PopulateFields()
        {
            categoryNameTextBox.Text = _dto.CategoryName;
        }

        private void categoryNameTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(categoryNameTextBox.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(categoryNameTextBox, "Category name cannot be empty.");
            }
            else
            {
                errorProvider.SetError(categoryNameTextBox, "");
            }
        }

        private async void saveButton_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                MessageBox.Show("Please fix the validation errors.", "Validation Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dto = _isEditMode ? _dto : new CategoryExpensesDetailDTO();
            dto.CategoryName = categoryNameTextBox.Text.Trim();

            saveButton.Enabled = false;
            bool success;

            try
            {
                if (_isEditMode)
                {
                    success = await _bll.UpdateAsync(dto);
                }
                else
                {
                    success = await _bll.InsertAsync(dto);
                }

                if (success)
                {
                    MessageBox.Show("Category saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to save the category.", "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                saveButton.Enabled = true;
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
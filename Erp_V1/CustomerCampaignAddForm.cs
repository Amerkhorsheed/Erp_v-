// File: CustomerCampaignAddForm.cs
using System;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;
using MaterialSkin;
using MaterialSkin.Controls; // Make sure this is present

namespace Erp_V1
{
    public partial class CustomerCampaignAddForm : MaterialForm
    {
        private readonly CustomerCampaignBLL _bll = new CustomerCampaignBLL();
        private CustomerCampaignDTO _dto;
        private readonly bool _isEditMode;

        public CustomerCampaignAddForm()
        {
            InitializeComponent();
            InitializeMaterialSkin();
            _isEditMode = false;
            this.KeyPreview = true;
            this.KeyDown += CustomerCampaignAddForm_KeyDown;
        }

        public CustomerCampaignAddForm(CustomerCampaignDTO dtoToEdit) : this()
        {
            _isEditMode = true;
            _dto = dtoToEdit;
            this.Text = "Edit Customer Campaign";
        }

        private void InitializeMaterialSkin()
        {
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.DeepPurple500,
                Primary.DeepPurple700,
                Primary.DeepPurple100,
                Accent.Pink200,
                TextShade.WHITE
            );

            startDatePicker.CalendarTitleBackColor = materialSkinManager.ColorScheme.PrimaryColor;
            startDatePicker.CalendarTitleForeColor = materialSkinManager.ColorScheme.TextColor;
            startDatePicker.CalendarMonthBackground = materialSkinManager.ColorScheme.LightPrimaryColor;
            startDatePicker.CalendarForeColor = materialSkinManager.ColorScheme.TextColor;

            endDatePicker.CalendarTitleBackColor = materialSkinManager.ColorScheme.PrimaryColor;
            endDatePicker.CalendarTitleForeColor = materialSkinManager.ColorScheme.TextColor;
            endDatePicker.CalendarMonthBackground = materialSkinManager.ColorScheme.LightPrimaryColor;
            endDatePicker.CalendarForeColor = materialSkinManager.ColorScheme.TextColor;
        }

        private async void CustomerCampaignAddForm_Load(object sender, EventArgs e)
        {
            var customers = await new CustomerBLL().SelectAsync();
            customerComboBox.DisplayMember = nameof(CustomerDetailDTO.CustomerName);
            customerComboBox.ValueMember = nameof(CustomerDetailDTO.ID);
            customerComboBox.DataSource = customers;

            hasEndDateCheckBox.CheckedChanged += (_, __) => endDatePicker.Enabled = hasEndDateCheckBox.Checked;

            if (_isEditMode)
                PopulateFields();
            else
                startDatePicker.Value = DateTime.Today;
        }

        private void PopulateFields()
        {
            customerComboBox.SelectedValue = _dto.CustomerID;
            campaignNameTextBox.Text = _dto.CampaignName;
            startDatePicker.Value = _dto.StartDate;
            hasEndDateCheckBox.Checked = _dto.EndDate.HasValue;
            endDatePicker.Value = _dto.EndDate ?? DateTime.Today;
            impactTextBox.Text = _dto.Impact;
        }

        #region Validation

        private void customerComboBox_Validating(object sender, CancelEventArgs e)
        {
            if (customerComboBox.SelectedIndex < 0)
            {
                e.Cancel = true;
                errorProvider.SetError(customerComboBox, "Please select a customer.");
            }
            else
            {
                errorProvider.SetError(customerComboBox, "");
            }
        }

        private void campaignNameTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(campaignNameTextBox.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(campaignNameTextBox, "Campaign name is required.");
            }
            else
            {
                errorProvider.SetError(campaignNameTextBox, "");
            }
        }

        private void impactTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(impactTextBox.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(impactTextBox, "Impact description is required.");
            }
            else
            {
                errorProvider.SetError(impactTextBox, "");
            }
        }

        #endregion

        private void CustomerCampaignAddForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ActiveControl is MaterialTextBox currentTextBox && currentTextBox.Multiline)
                {
                    if (!e.Shift) return;
                }

                e.SuppressKeyPress = true;
                saveButton.PerformClick();
            }
        }

        private async void saveButton_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                MessageBox.Show("Please fix validation errors first.", "Validation Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dto = _isEditMode ? _dto : new CustomerCampaignDTO();
            dto.CustomerID = (int)customerComboBox.SelectedValue;
            dto.CampaignName = campaignNameTextBox.Text.Trim();
            dto.StartDate = startDatePicker.Value.Date;
            dto.EndDate = hasEndDateCheckBox.Checked ? (DateTime?)endDatePicker.Value.Date : null;
            dto.Impact = impactTextBox.Text.Trim();

            saveButton.Enabled = false;
            bool success;
            try
            {
                success = _isEditMode
                    ? await _bll.UpdateAsync(dto)
                    : await _bll.InsertAsync(dto);
            }
            catch (Exception ex)
            {
                var eb = new StringBuilder($"Save failed: {ex.Message}");
                var inner = ex.InnerException;
                while (inner != null)
                {
                    eb.AppendLine($"\nInner Exception: {inner.Message}");
                    inner = inner.InnerException;
                }
                MessageBox.Show(eb.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                saveButton.Enabled = true;
                return;
            }

            saveButton.Enabled = true;
            if (success)
            {
                MessageBox.Show("Campaign saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Operation did not succeed.", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

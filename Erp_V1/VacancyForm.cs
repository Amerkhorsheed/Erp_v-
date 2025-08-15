///*************************************************
//* ERP System Version 1.0
//* Presentation Layer Components
//* VacancyForm.cs
//*************************************************/
//using System;
//using System.Windows.Forms;
//using Erp_V1.BLL;
//using Erp_V1.DAL.DTO;

//namespace Erp_V1
//{
//    /// <summary>
//    /// Provides the user interface for vacancy management.
//    /// </summary>
//    public partial class VacancyForm : Form
//    {
//        private readonly VacancyBLL _vacancyBll;

//        public VacancyForm()
//        {
//            InitializeComponent();
//            _vacancyBll = new VacancyBLL();
//            LoadVacancies();
//        }

//        private void btnSave_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                // Create DTO from form inputs.
//                var vacancy = new VacancyDetailDTO
//                {
//                    Position = txtPosition.Text.Trim(),
//                    JobDescription = txtJobDescription.Text.Trim(),
//                    Requirements = txtRequirements.Text.Trim()
//                };

//                if (_vacancyBll.Insert(vacancy))
//                {
//                    MessageBox.Show("Vacancy inserted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                    LoadVacancies();
//                    ClearFields();
//                }
//                else
//                {
//                    MessageBox.Show("Vacancy insertion failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        private void LoadVacancies()
//        {
//            try
//            {
//                var vacancies = _vacancyBll.Select();
//                lstVacancies.Items.Clear();
//                foreach (var vacancy in vacancies.Vacancies)
//                {
//                    lstVacancies.Items.Add($"{vacancy.ID} - {vacancy.Position}");
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"An error occurred while loading vacancies: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        private void ClearFields()
//        {
//            txtPosition.Clear();
//            txtJobDescription.Clear();
//            txtRequirements.Clear();
//        }

//        private void VacancyForm_Load(object sender, EventArgs e)
//        {
//            // Additional initialization if needed.
//        }
//    }
//}

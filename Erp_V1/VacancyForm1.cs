//using System;
//using System.Diagnostics;
//using System.IO;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using Erp_V1.BLL;
//using Org.BouncyCastle.Asn1.Cmp;

//namespace Erp_V1
//{
//    public partial class VacancyForm1 : Form
//    {
        
        
//        private readonly string pythonScriptPath = @"C:\Users\ahmad\OneDrive\Pictures\Erp_V1\Erp_V1\python_script.py";

//        public VacancyForm1()
//        {
//            InitializeComponent();
//            CustomizeUI();
//        }

//        private void CustomizeUI()
//        {
//            this.Text = "Erp_V1 Resume Analyzer";
//            lblStatus.Text = string.Empty;
//            lblResult.Text = string.Empty;
//            progressBar.Visible = false;
//        }

//        private async void btnUpload_Click(object sender, EventArgs e)
//        {
//            using (OpenFileDialog openFileDialog = new OpenFileDialog())
//            {
//                openFileDialog.Filter = "PDF files (*.pdf)|*.pdf|Word files (*.docx)|*.docx";
//                if (openFileDialog.ShowDialog() == DialogResult.OK)
//                {
//                    string resumeFilePath = openFileDialog.FileName;
//                    string jobDescription = txtJobDescription.Text.Trim();

//                    if (string.IsNullOrEmpty(jobDescription))
//                    {
//                        MessageBox.Show("Please enter a job description before analyzing the resume.",
//                                        "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                        return;
//                    }

//                    try
//                    {
//                        progressBar.Visible = true;
//                        lblStatus.Text = "Analyzing resume...";

//                        // Call the Python script asynchronously.
//                        string result = await Task.Run(() => AnalyzeResume(resumeFilePath, jobDescription));

//                        progressBar.Visible = false;
//                        lblStatus.Text = "Analysis complete.";
//                        lblResult.Text = $"Suggested Position: {result}";

//                        // Retrieve vacancy information from SQL Server and display it.
//                        VacancyBLL vacancyBLL = new VacancyBLL();
//                        var vacancies = vacancyBLL.Select().Vacancies;
//                        string vacancyList = "Vacancies:\n";
//                        foreach (var vacancy in vacancies)
//                        {
//                            vacancyList += $"{vacancy.Position} - Requirements: {vacancy.Requirements}\n";
//                        }
//                        MessageBox.Show(vacancyList, "Vacancy Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                    }
//                    catch (Exception ex)
//                    {
//                        progressBar.Visible = false;
//                        lblStatus.Text = "Analysis failed.";
//                        MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                    }
//                }
//            }
//        }

//        private void btnManageVacancies_Click(object sender, EventArgs e)
//        {
//            VacancyForm vacancyForm = new VacancyForm();
//            vacancyForm.ShowDialog();
//        }

//        private string AnalyzeResume(string resumeFilePath, string jobDescription)
//        {
//            if (!File.Exists(pythonScriptPath))
//            {
//                throw new FileNotFoundException("Python script not found.", pythonScriptPath);
//            }

//            ProcessStartInfo startInfo = new ProcessStartInfo
//            {
//                FileName = "python",
//                Arguments = $"\"{pythonScriptPath}\" \"{resumeFilePath}\" \"{jobDescription}\"",
//                UseShellExecute = false,
//                RedirectStandardOutput = true,
//                RedirectStandardError = true,
//                CreateNoWindow = true
//            };

//            using (Process process = new Process())
//            {
//                process.StartInfo = startInfo;
//                process.Start();

//                string output = process.StandardOutput.ReadToEnd();
//                string errorOutput = process.StandardError.ReadToEnd();
//                process.WaitForExit();

//                if (!string.IsNullOrEmpty(errorOutput))
//                {
//                    throw new Exception(errorOutput);
//                }
//                return output.Trim();
//            }
//        }

//        private void VacancyForm1_Load(object sender, EventArgs e)
//        {

//        }
//    }
//}

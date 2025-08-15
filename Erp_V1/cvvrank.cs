using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace Erp_V1
{
    public partial class cvvrank : Form
    {
        private string pdfFilePath = "";

        public cvvrank()
        {
            InitializeComponent();
        }

        private async void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PDF Files (*.pdf)|*.pdf|All files (*.*)|*.*";
            openFileDialog.Title = "Select a PDF CV File";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pdfFilePath = openFileDialog.FileName;
                txtFilePath.Text = pdfFilePath;
                btnAnalyze.Enabled = true; // Enable Analyze button once a file is chosen
                txtRank.Text = ""; // Clear previous rank
                txtFeedback.Text = ""; // Clear previous feedback
            }
        }

        private async void btnAnalyze_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(pdfFilePath))
            {
                MessageBox.Show("Please browse and select a PDF CV file first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            txtRank.Text = "Analyzing..."; // Indicate analysis is in progress
            txtFeedback.Text = "Processing CV, please wait...";
            btnAnalyze.Enabled = false; // Disable Analyze button during processing

            try
            {
                string cvText = ExtractTextFromPdf(pdfFilePath);
                if (string.IsNullOrEmpty(cvText))
                {
                    MessageBox.Show("Could not extract text from the PDF file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtRank.Text = "Error";
                    txtFeedback.Text = "Text extraction failed.";
                    return;
                }

                string rankResult = await GetCVRankFromOllama(cvText);

                // Basic parsing of the result - you might need to adjust this based on Llama's response format
                string rankValue = "N/A";
                string feedbackText = "Analysis complete. Rank not parsed correctly.";

                // **Improved Parsing Logic (Example - Adjust based on Llama's response):**
                // Let's assume Llama returns JSON or a string like "Rank: 7/10. Feedback: ... "
                try
                {
                    // Attempt to parse JSON if Llama might return structured data
                    dynamic responseJson = JsonConvert.DeserializeObject(rankResult);
                    if (responseJson != null && responseJson.rank != null)
                    {
                        rankValue = responseJson.rank.ToString();
                        feedbackText = responseJson.feedback?.ToString() ?? "Analysis complete."; // Use feedback if available
                    }
                    else if (rankResult.Contains("Rank:")) // Simple string parsing if JSON fails or Llama gives plain text
                    {
                        string[] parts = rankResult.Split(new string[] { "Rank:" }, StringSplitOptions.None);
                        if (parts.Length > 1)
                        {
                            string rankPart = parts[1].Trim().Split('.')[0].Trim(); // Get part after "Rank:" and before first period
                            rankValue = rankPart;
                            feedbackText = rankResult; // Use the whole response as feedback initially
                        }
                        else
                        {
                            feedbackText = rankResult; // Fallback if rank keyword is present but parsing fails
                        }
                    }
                    else
                    {
                        feedbackText = rankResult; // If no "Rank:" keyword, just show the whole output as feedback
                    }

                }
                catch (JsonException)
                {
                    // JSON parsing failed, try simple string parsing or just use the raw result as feedback
                    if (rankResult.Contains("Rank:"))
                    {
                        string[] parts = rankResult.Split(new string[] { "Rank:" }, StringSplitOptions.None);
                        if (parts.Length > 1)
                        {
                            string rankPart = parts[1].Trim().Split('.')[0].Trim();
                            rankValue = rankPart;
                            feedbackText = rankResult;
                        }
                        else
                        {
                            feedbackText = rankResult; // Fallback if rank keyword is present but parsing fails
                        }
                    }
                    else
                    {
                        feedbackText = rankResult; // If no "Rank:" keyword, just show the whole output as feedback
                    }
                }


                txtRank.Text = rankValue;
                txtFeedback.Text = feedbackText;

            }
            catch (Exception ex)
            {
                txtRank.Text = "Error";
                txtFeedback.Text = $"Analysis failed: {ex.Message}";
                MessageBox.Show($"Error during analysis: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnAnalyze.Enabled = true; // Re-enable Analyze button after processing (success or error)
            }
        }

        private string ExtractTextFromPdf(string filePath)
        {
            StringBuilder text = new StringBuilder();
            try
            {
                using (PdfReader reader = new PdfReader(filePath))
                {
                    for (int i = 1; i <= reader.NumberOfPages; i++)
                    {
                        text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle PDF reading errors (e.g., file corrupted, not a valid PDF)
                MessageBox.Show($"Error reading PDF: {ex.Message}", "PDF Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ""; // Return empty string to indicate failure
            }
            return text.ToString();
        }


        private async Task<string> GetCVRankFromOllama(string cvText)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:11434/"); // Default Ollama API base URL
                string ollamaRequestEndpoint = "api/generate";

                var requestData = new
                {
                    model = "llama3.2", // Or whatever you named your llama3.2 model in Ollama
                    prompt = $"Analyze the following CV text and provide a CV rank out of 10, along with a brief explanation of your ranking. Be concise.\n\nCV Text:\n\n{cvText}",
                    stream = false // We want the full response, not a stream
                };
                string jsonData = JsonConvert.SerializeObject(requestData);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                try
                {
                    HttpResponseMessage response = await client.PostAsync(ollamaRequestEndpoint, content);
                    response.EnsureSuccessStatusCode(); // Throw if not successful

                    string responseContent = await response.Content.ReadAsStringAsync();
                    dynamic responseObject = JsonConvert.DeserializeObject(responseContent);

                    if (responseObject != null && responseObject.response != null)
                    {
                        return responseObject.response.ToString();
                    }
                    else
                    {
                        return "Error: Could not get a valid response from Ollama. Response structure unexpected.";
                    }
                }
                catch (HttpRequestException ex)
                {
                    return $"Error communicating with Ollama API: {ex.Message}. Is Ollama server running and accessible at http://localhost:11434?";
                }
                catch (JsonException ex)
                {
                    return $"Error parsing JSON response from Ollama: {ex.Message}. Response was: ... (check debug logs if available)"; // Add more debug info if needed
                }
            }
        }
    }
}
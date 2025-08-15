using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SD = System.Drawing; // Alias for System.Drawing
using System.ComponentModel;
using System.Text.RegularExpressions;
using DevExpress.XtraEditors;
using Tesseract;
using OfficeOpenXml;
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;
using DocumentFormat.OpenXml.Packaging;
using WordProc = DocumentFormat.OpenXml.Wordprocessing;
using DevExpress.LookAndFeel;

namespace Erp_V1
{
    public partial class FrmScanSales : XtraForm
    {
        // Business layer and DTO instances.
        private readonly SalesBLL _salesBLL = new SalesBLL();
        private SalesDTO _dto;
        private readonly SalesDetailDTO _detail = new SalesDetailDTO();

        public FrmScanSales()
        {
            InitializeComponent();
            InitializeCustomComponents();
            LoadData();

            // Apply a modern DevExpress theme.
            UserLookAndFeel.Default.SkinName = "Office 2019 Colorful";
        }

        /// <summary>
        /// Applies custom UI styles and subscribes to events.
        /// </summary>
        private void InitializeCustomComponents()
        {
            // Set form background.
            this.BackColor = SD.Color.WhiteSmoke;

            // Style for the Upload File button.
            btnUploadFile.Appearance.BackColor = SD.Color.FromArgb(142, 68, 173);
            btnUploadFile.Appearance.ForeColor = SD.Color.White;
            btnUploadFile.Appearance.Font = new SD.Font("Segoe UI Semibold", 10, SD.FontStyle.Bold);
            btnUploadFile.Cursor = Cursors.Hand;

            // Style for the Save Sales button.
            btnSave.Appearance.BackColor = SD.Color.FromArgb(230, 126, 34);
            btnSave.Appearance.ForeColor = SD.Color.White;
            btnSave.Appearance.Font = new SD.Font("Segoe UI Semibold", 10, SD.FontStyle.Bold);
            btnSave.Cursor = Cursors.Hand;

            // Labels: Set modern fonts and colors.
            lblCustomer.Font = new SD.Font("Segoe UI", 10, SD.FontStyle.Regular);
            lblCustomer.ForeColor = SD.Color.FromArgb(44, 62, 80);
            lblAmount.Font = new SD.Font("Segoe UI", 10, SD.FontStyle.Regular);
            lblAmount.ForeColor = SD.Color.FromArgb(44, 62, 80);
            lblPrice.Font = new SD.Font("Segoe UI", 10, SD.FontStyle.Regular);
            lblPrice.ForeColor = SD.Color.FromArgb(44, 62, 80);
            lblProduct.Font = new SD.Font("Segoe UI", 10, SD.FontStyle.Regular);
            lblProduct.ForeColor = SD.Color.FromArgb(44, 62, 80);

            // TextBoxes: Set modern fonts and border style.
            txtCustomerName.Font = new SD.Font("Segoe UI", 10);
            txtAmount.Font = new SD.Font("Segoe UI", 10);
            txtPrice.Font = new SD.Font("Segoe UI", 10);
            txtCustomerName.BorderStyle = BorderStyle.FixedSingle;
            txtAmount.BorderStyle = BorderStyle.FixedSingle;
            txtPrice.BorderStyle = BorderStyle.FixedSingle;

            // ComboBox: Set modern font.
            cmbProduct.Font = new SD.Font("Segoe UI", 10);
            cmbProduct.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProduct.SelectedIndexChanged += cmbProduct_SelectedIndexChanged;
        }

        /// <summary>
        /// Loads reference data (including products) from the business layer.
        /// </summary>
        private void LoadData()
        {
            try
            {
                _dto = _salesBLL.Select();
                if (_dto == null)
                    _dto = new SalesDTO();

                // Bind products to the ComboBox.
                cmbProduct.DataSource = _dto.Products;
                cmbProduct.DisplayMember = "ProductName";
                cmbProduct.ValueMember = "ProductID";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading reference data: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Automatically fills in the price textbox when the product is selected.
        /// </summary>
        private void cmbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProduct.SelectedItem != null)
            {
                // Assuming each product has a Sale_Price property.
                var product = cmbProduct.SelectedItem as ProductDetailDTO;
                if (product != null)
                {
                    txtPrice.Text = product.Sale_Price.ToString();
                    _detail.Price = (int)product.Sale_Price;
                    _detail.ProductID = product.ProductID;
                }
            }
        }

        /// <summary>
        /// Opens a file dialog for file upload.
        /// </summary>
        private void btnUploadFile_Click(object sender, EventArgs e)
        {
            try
            {
                using (var ofd = new OpenFileDialog())
                {
                    ofd.Filter = "Image Files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png|" +
                                 "PDF Files (*.pdf)|*.pdf|" +
                                 "Word Documents (*.doc, *.docx)|*.doc;*.docx|" +
                                 "Excel Files (*.xls, *.xlsx)|*.xls;*.xlsx";
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        ProcessFile(ofd.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error uploading file: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Processes the selected file based on its extension.
        /// </summary>
        private void ProcessFile(string filePath)
        {
            string ext = Path.GetExtension(filePath)?.ToLower();
            string extractedText = string.Empty;

            if (string.IsNullOrEmpty(ext))
            {
                MessageBox.Show("Invalid file extension.");
                return;
            }

            try
            {
                switch (ext)
                {
                    case ".jpg":
                    case ".jpeg":
                    case ".png":
                        extractedText = ExtractTextFromImage(filePath);
                        break;
                    case ".pdf":
                        MessageBox.Show("PDF text extraction is not implemented in this sample.");
                        return;
                    case ".docx":
                        extractedText = ExtractTextFromWord(filePath);
                        MessageBox.Show($"DEBUG - Extracted Word text:\n{extractedText}");
                        break;
                    case ".doc":
                        MessageBox.Show("DOC text extraction is not implemented. Please convert to DOCX.");
                        return;
                    case ".xlsx":
                        extractedText = ExtractTextFromExcel(filePath);
                        break;
                    case ".xls":
                        MessageBox.Show("XLS text extraction is not implemented. Please convert to XLSX.");
                        return;
                    default:
                        MessageBox.Show("Unsupported file type.");
                        return;
                }

                if (!string.IsNullOrEmpty(extractedText))
                {
                    ParseExtractedText(extractedText);
                }
                else
                {
                    MessageBox.Show("No text could be extracted from the file.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing file: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Uses Tesseract OCR to extract text from an image file.
        /// </summary>
        private string ExtractTextFromImage(string imagePath)
        {
            try
            {
                using (var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default))
                {
                    using (var img = Pix.LoadFromFile(imagePath))
                    {
                        using (var page = engine.Process(img))
                        {
                            return page.GetText();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"OCR extraction failed: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }

        /// <summary>
        /// Extracts text from a DOCX file using the Open XML SDK.
        /// </summary>
        private string ExtractTextFromWord(string filePath)
        {
            try
            {
                using (var wordDoc = WordprocessingDocument.Open(filePath, false))
                {
                    var body = wordDoc.MainDocumentPart.Document.Body;
                    return string.Join("\n", body.Descendants<WordProc.Text>().Select(t => t.Text))
                                       .Replace('\a', '\n');
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Word extraction failed: {ex.Message}\n{ex.StackTrace}");
                return string.Empty;
            }
        }

        /// <summary>
        /// Extracts text from an XLSX file using EPPlus.
        /// </summary>
        private string ExtractTextFromExcel(string filePath)
        {
            try
            {
                // Ensure EPPlus license is set.
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                var fileInfo = new FileInfo(filePath);
                if (!fileInfo.Exists)
                {
                    MessageBox.Show("Excel file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return string.Empty;
                }
                using (var package = new ExcelPackage(fileInfo))
                {
                    // Check if there is at least one worksheet.
                    if (package.Workbook.Worksheets.Count == 0)
                    {
                        MessageBox.Show("No worksheets found in the Excel file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return string.Empty;
                    }
                    var worksheet = package.Workbook.Worksheets[0];
                    if (worksheet.Dimension == null)
                    {
                        MessageBox.Show("Worksheet is empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return string.Empty;
                    }

                    var sb = new StringBuilder();
                    for (int row = worksheet.Dimension.Start.Row; row <= worksheet.Dimension.End.Row; row++)
                    {
                        for (int col = worksheet.Dimension.Start.Column; col <= worksheet.Dimension.End.Column; col++)
                        {
                            var cell = worksheet.Cells[row, col];
                            // Append the cell text if it's not empty.
                            if (!string.IsNullOrEmpty(cell.Text))
                                sb.Append(cell.Text + " ");
                        }
                        sb.AppendLine(); // New line at end of each row.
                    }
                    string extractedText = sb.ToString().Trim();
                    // Optional: Show debug message to see extracted text.
                    if (!string.IsNullOrEmpty(extractedText))
                        MessageBox.Show($"Extracted Excel text:\n{extractedText}", "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return extractedText;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Excel extraction failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }


        /// <summary>
        /// Parses the extracted text and populates form fields.
        /// Expected format: each line as "Key: Value"
        /// </summary>
        private void ParseExtractedText(string text)
        {
            try
            {
                var pattern = @"^\s*(product|customer|amount|price)\s*[:=]\s*(.*?)\s*$";
                var matches = Regex.Matches(text, pattern, RegexOptions.Multiline | RegexOptions.IgnoreCase);
                foreach (Match match in matches)
                {
                    var key = match.Groups[1].Value.Trim().ToLower();
                    var value = match.Groups[2].Value.Trim();
                    switch (key)
                    {
                        case "product":
                            _detail.ProductName = value;
                            // Try to find the product in the product list.
                            var prod = _dto.Products.FirstOrDefault(p =>
                                string.Equals(p.ProductName?.Trim(), value, StringComparison.CurrentCultureIgnoreCase));
                            if (prod != null)
                            {
                                // Set the ComboBox selected value so that its SelectedIndexChanged event fires.
                                cmbProduct.SelectedValue = prod.ProductID;
                            }
                            break;
                        case "customer":
                            _detail.CustomerName = value;
                            txtCustomerName.Text = value;
                            break;
                        case "amount" when int.TryParse(value, out int amt):
                            _detail.SalesAmount = amt;
                            txtAmount.Text = amt.ToString();
                            break;
                        case "price" when int.TryParse(value, out int price):
                            _detail.Price = price;
                            txtPrice.Text = price.ToString();
                            break;
                    }
                }
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Parsing error: {ex.Message}\nExtracted text was:\n{text}");
            }
        }

        /// <summary>
        /// Validates master data names and resolves IDs.
        /// </summary>
        private bool ResolveIDs()
        {
            // Validate product via ComboBox selection.
            if (cmbProduct.SelectedItem == null)
            {
                MessageBox.Show("Please select a product.",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            // Validate Customer Name.
            var customer = _dto?.Customers?.FirstOrDefault(c =>
                string.Equals(c.CustomerName?.Trim(), txtCustomerName.Text?.Trim(), StringComparison.CurrentCultureIgnoreCase));
            if (customer == null)
            {
                MessageBox.Show("Customer name not found in the system. Please verify the customer name.",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            _detail.CustomerID = customer.ID;
            _detail.CategoryID = _dto.Products.FirstOrDefault(p => p.ProductID == _detail.ProductID)?.CategoryID ?? 0;
            return true;
        }

        /// <summary>
        /// Validates sales details and saves the record.
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Update detail object.
                _detail.CustomerName = txtCustomerName.Text?.Trim();

                if (!int.TryParse(txtAmount.Text?.Trim(), out int amount))
                {
                    MessageBox.Show("Invalid sales amount.",
                        "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                _detail.SalesAmount = amount;

                if (!int.TryParse(txtPrice.Text?.Trim(), out int price))
                {
                    MessageBox.Show("Invalid price value.",
                        "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                _detail.Price = price;
                _detail.SalesDate = DateTime.Today;

                if (!ResolveIDs())
                    return;

                bool result = _salesBLL.Insert(_detail);
                if (result)
                {
                    MessageBox.Show("Sales record saved successfully!",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("Failed to save the sales record.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving sales record: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Clears all form fields.
        /// </summary>
        private void ClearForm()
        {
            cmbProduct.SelectedIndex = 0;
            txtCustomerName.Clear();
            txtAmount.Clear();
            txtPrice.Clear();

            _detail.ProductName = null;
            _detail.CustomerID = 0;
            _detail.ProductID = 0;
            _detail.SalesAmount = 0;
            _detail.Price = 0;
            _detail.SalesDate = default(DateTime);
            _detail.CategoryID = 0;
        }
    }
}

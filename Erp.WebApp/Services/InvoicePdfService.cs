using Erp_V1.DAL.DAL;
using Erp.WebApp.Services.Interfaces;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Fonts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace Erp.WebApp.Services
{
    public class InvoicePdfService : IInvoicePdfService
    {
        private readonly erp_v2Entities _context;

        public InvoicePdfService(erp_v2Entities context)
        {
            _context = context;
        }

        public async Task<byte[]> GenerateInvoicePdfAsync(List<int> saleIds)
        {
            try
            {
                // Configure font resolver for Windows fonts
                if (GlobalFontSettings.FontResolver == null)
                {
                    GlobalFontSettings.UseWindowsFontsUnderWindows = true;
                }

                var salesWithDetails = await (from s in _context.SALES
                                              join c in _context.CUSTOMER on s.CustomerID equals c.ID
                                              join p in _context.PRODUCT on s.ProductID equals p.ID
                                              where saleIds.Contains(s.ID)
                                              select new { Sale = s, Customer = c, Product = p })
                                              .ToListAsync();

                if (salesWithDetails == null || !salesWithDetails.Any())
                {
                    throw new InvalidOperationException("No sale records found for the provided IDs.");
                }

                var document = new PdfDocument();
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            var colors = new
            {
                Primary = XColor.FromArgb(25, 42, 86),
                Text = XColor.FromArgb(34, 34, 34),
                SubtleText = XColor.FromArgb(136, 136, 136),
                Border = XColor.FromArgb(240, 240, 240)
            };

            var fonts = new
            {
                Title = new XFont("Arial", 26, XFontStyleEx.Bold),
                H1 = new XFont("Arial", 14, XFontStyleEx.Bold),
                H2 = new XFont("Arial", 11, XFontStyleEx.Bold),
                Body = new XFont("Arial", 10, XFontStyleEx.Regular),
                Small = new XFont("Arial", 8, XFontStyleEx.Regular)
            };

            double margin = 40;
            double currentY = 0;

            // === 1. HEADER BAND ===
            gfx.DrawRectangle(new XSolidBrush(colors.Primary), 0, 0, page.Width, 90);
            currentY = 25;

            // Draw a simple text logo instead of loading an image file
            gfx.DrawString("ERP SYSTEM", fonts.H1, XBrushes.White, margin, currentY + 15);

            gfx.DrawString("INVOICE", fonts.Title, XBrushes.White, new XRect(0, currentY + 5, page.Width - margin, 0), XStringFormats.TopRight);
            currentY = 120;

            // === 2. BILLING INFORMATION (Unchanged) ===
            var firstDetail = salesWithDetails.First();
            gfx.DrawString("BILLED TO", fonts.H2, new XSolidBrush(colors.SubtleText), margin, currentY);
            gfx.DrawString(firstDetail.Customer?.CustomerName ?? "N/A", fonts.H1, new XSolidBrush(colors.Text), margin, currentY + 20);
            gfx.DrawString(firstDetail.Customer?.Cust_Address ?? "Address not provided", fonts.Body, new XSolidBrush(colors.Text), margin, currentY + 40);
            gfx.DrawString(firstDetail.Customer?.Email ?? "Email not provided", fonts.Body, new XSolidBrush(colors.Text), margin, currentY + 55);
            double rightColX = page.Width - margin - 200;
            gfx.DrawString("INVOICE #", fonts.H2, new XSolidBrush(colors.SubtleText), rightColX, currentY);
            gfx.DrawString(firstDetail.Sale.ID.ToString(), fonts.H1, new XSolidBrush(colors.Text), rightColX, currentY + 20);
            gfx.DrawString("DATE OF ISSUE", fonts.H2, new XSolidBrush(colors.SubtleText), rightColX, currentY + 50);
            gfx.DrawString(firstDetail.Sale.SalesDate.ToString("MMMM dd, yyyy", CultureInfo.InvariantCulture), fonts.Body, new XSolidBrush(colors.Text), rightColX, currentY + 65);
            currentY += 100;

            // === 3. INVOICE TABLE (Unchanged) ===
            gfx.DrawRectangle(new XSolidBrush(colors.Border), margin, currentY, page.Width - (2 * margin), 25);
            gfx.DrawString("DESCRIPTION", fonts.H2, new XSolidBrush(colors.SubtleText), margin + 10, currentY + 18);
            gfx.DrawString("QTY", fonts.H2, new XSolidBrush(colors.SubtleText), page.Width - margin - 180, currentY + 18);
            gfx.DrawString("UNIT PRICE", fonts.H2, new XSolidBrush(colors.SubtleText), page.Width - margin - 120, currentY + 18);
            gfx.DrawString("TOTAL", fonts.H2, new XSolidBrush(colors.SubtleText), page.Width - margin - 50, currentY + 18);
            currentY += 35;
            decimal grandTotal = 0;
            decimal totalPaid = 0;
            decimal totalOutstanding = 0;
            
            foreach (var detail in salesWithDetails)
            {
                // Use the same calculation as HTML preview - use the Total field from database
                var lineTotal = detail.Sale.Total ?? 0;
                var linePaid = detail.Sale.Madfou3 ?? 0;
                var lineOutstanding = detail.Sale.Baky ?? 0;
                
                grandTotal += lineTotal;
                totalPaid += linePaid;
                totalOutstanding += lineOutstanding;
                
                gfx.DrawString(detail.Product?.ProductName ?? "N/A", fonts.Body, new XSolidBrush(colors.Text), margin + 10, currentY);
                gfx.DrawString(detail.Sale.ProductSalesAmout.ToString(), fonts.Body, new XSolidBrush(colors.Text), page.Width - margin - 180, currentY);
                gfx.DrawString($"${detail.Sale.ProductSalesPrice:N2}", fonts.Body, new XSolidBrush(colors.Text), page.Width - margin - 120, currentY);
                gfx.DrawString($"${lineTotal:N2}", fonts.Body, new XSolidBrush(colors.Text), page.Width - margin - 50, currentY);
                currentY += 20;
                gfx.DrawLine(new XPen(colors.Border), margin, currentY, page.Width - margin, currentY);
                currentY += 15;
            }

            // === 4. TOTALS SECTION (REFINED AND PERFECTED) ===
            currentY += 20;

            // FIX: Define the X coordinates for the two columns for perfect alignment.
            // All labels will align to the right edge of 'labelX'.
            // All values will align to the right edge of 'valueX'.
            double labelX = page.Width - margin - 100;
            double valueX = page.Width - margin;

            // --- Total Amount ---
            gfx.DrawString("Total Amount", fonts.Body, new XSolidBrush(colors.Text), new XRect(0, currentY, labelX, 0), XStringFormats.TopRight);
            gfx.DrawString($"${grandTotal:N2}", fonts.Body, new XSolidBrush(colors.Text), new XRect(0, currentY, valueX, 0), XStringFormats.TopRight);
            currentY += 20;

            // --- Amount Paid ---
            gfx.DrawString("Amount Paid", fonts.Body, new XSolidBrush(colors.Text), new XRect(0, currentY, labelX, 0), XStringFormats.TopRight);
            gfx.DrawString($"${totalPaid:N2}", fonts.Body, new XSolidBrush(colors.Text), new XRect(0, currentY, valueX, 0), XStringFormats.TopRight);
            currentY += 25;

            // --- Separator Line ---
            gfx.DrawLine(new XPen(colors.Primary, 2), labelX - 80, currentY, valueX, currentY);
            currentY += 10;

            // --- Outstanding Balance ---
            var balanceColor = totalOutstanding > 0 ? XColor.FromArgb(220, 53, 69) : XColor.FromArgb(25, 135, 84); // Red for outstanding, green for paid
            gfx.DrawString("Outstanding Balance", fonts.H1, new XSolidBrush(balanceColor), new XRect(0, currentY, labelX, 0), XStringFormats.TopRight);
            gfx.DrawString($"${totalOutstanding:N2}", fonts.H1, new XSolidBrush(balanceColor), new XRect(0, currentY, valueX, 0), XStringFormats.TopRight);
            currentY += 30;

            // === 5. PAYMENT STATUS SECTION ===
            // Determine payment status using already calculated totals
            string paymentStatus = "Unknown";
            if (totalOutstanding == 0)
                paymentStatus = "Fully Paid";
            else if (totalPaid > 0)
                paymentStatus = "Partially Paid";
            else
                paymentStatus = "Unpaid";

            // Payment Status Header
            gfx.DrawString("PAYMENT STATUS", fonts.H2, new XSolidBrush(colors.Primary), margin, currentY);
            currentY += 25;

            // Payment details in two columns
            double leftColX = margin;
            double paymentRightColX = margin + 200;

            // Left column - Payment Status and Amount Paid
            gfx.DrawString("Status:", fonts.Body, new XSolidBrush(colors.Text), leftColX, currentY);
            XColor statusColor = totalOutstanding == 0 ? XColor.FromArgb(34, 139, 34) : 
                                totalPaid > 0 ? XColor.FromArgb(255, 140, 0) : XColor.FromArgb(220, 20, 60);
            gfx.DrawString(paymentStatus, fonts.H2, new XSolidBrush(statusColor), leftColX + 60, currentY);
            currentY += 20;

            gfx.DrawString("Amount Paid:", fonts.Body, new XSolidBrush(colors.Text), leftColX, currentY);
            gfx.DrawString($"${totalPaid:N2}", fonts.H2, new XSolidBrush(XColor.FromArgb(34, 139, 34)), leftColX + 100, currentY);
            currentY += 20;

            gfx.DrawString("Outstanding Balance:", fonts.Body, new XSolidBrush(colors.Text), leftColX, currentY);
            gfx.DrawString($"${totalOutstanding:N2}", fonts.H2, new XSolidBrush(totalOutstanding > 0 ? XColor.FromArgb(220, 20, 60) : colors.Text), leftColX + 130, currentY);
            currentY += 30;

            // === 6. FOOTER (Unchanged) ===
            currentY = page.Height - margin - 50;
            gfx.DrawRectangle(new XSolidBrush(colors.Border), 0, currentY, page.Width, 90);
            gfx.DrawString("Terms & Conditions", fonts.H2, new XSolidBrush(colors.Text), margin, currentY + 20);
            gfx.DrawString("Payment is due within 30 days. Thank you for your business.", fonts.Small, new XSolidBrush(colors.SubtleText), margin, currentY + 35);

                // --- Save the document to a memory stream ---
                byte[] fileContents;
                using (MemoryStream stream = new MemoryStream())
                {
                    document.Save(stream, true);
                    fileContents = stream.ToArray();
                }

                System.Diagnostics.Debug.WriteLine($"[PDF_GENERATED] PDF generated successfully with {fileContents.Length} bytes");
                return fileContents;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[PDF_ERROR] Failed to generate PDF: {ex.Message}");
                throw new InvalidOperationException($"Failed to generate PDF: {ex.Message}", ex);
            }
        }
    }
}
#pragma warning disable CS0618
using Erp_V1.DAL.DAL;
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

namespace Seller.API.Services
{
    public interface IInvoicePdfService
    {
        Task<byte[]> GenerateInvoicePdfAsync(List<int> saleIds);
    }

    public class InvoicePdfService : IInvoicePdfService
    {
        private readonly erp_v2Entities _context;

        public InvoicePdfService(erp_v2Entities context)
        {
            _context = context;
        }

        public async Task<byte[]> GenerateInvoicePdfAsync(List<int> saleIds)
        {
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

            try
            {
                // Configure font resolver for Windows fonts
                if (GlobalFontSettings.FontResolver == null)
                {
                    GlobalFontSettings.UseWindowsFontsUnderWindows = true;
                }

                // Create a new PDF document
                var document = new PdfDocument();
                var page = document.AddPage();
                page.Size = PdfSharp.PageSize.A4;
                var gfx = XGraphics.FromPdfPage(page);

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

            // === 2. BILLING INFORMATION ===
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

            // === 3. INVOICE TABLE ===
            gfx.DrawRectangle(new XSolidBrush(colors.Border), margin, currentY, page.Width - (2 * margin), 25);
            gfx.DrawString("DESCRIPTION", fonts.H2, new XSolidBrush(colors.SubtleText), margin + 10, currentY + 18);
            gfx.DrawString("QTY", fonts.H2, new XSolidBrush(colors.SubtleText), page.Width - margin - 180, currentY + 18);
            gfx.DrawString("UNIT PRICE", fonts.H2, new XSolidBrush(colors.SubtleText), page.Width - margin - 120, currentY + 18);
            gfx.DrawString("TOTAL", fonts.H2, new XSolidBrush(colors.SubtleText), page.Width - margin - 50, currentY + 18);
            currentY += 35;
            decimal grandTotal = 0;
            foreach (var detail in salesWithDetails)
            {
                var lineTotal = (decimal)((detail.Sale.ProductSalesPrice * detail.Sale.ProductSalesAmout) - (detail.Sale.MaxDiscount ?? 0));
                grandTotal += lineTotal;
                gfx.DrawString(detail.Product?.ProductName ?? "N/A", fonts.Body, new XSolidBrush(colors.Text), margin + 10, currentY);
                gfx.DrawString(detail.Sale.ProductSalesAmout.ToString(), fonts.Body, new XSolidBrush(colors.Text), page.Width - margin - 180, currentY);
                gfx.DrawString($"${detail.Sale.ProductSalesPrice:N2}", fonts.Body, new XSolidBrush(colors.Text), page.Width - margin - 120, currentY);
                gfx.DrawString($"${lineTotal:N2}", fonts.Body, new XSolidBrush(colors.Text), page.Width - margin - 50, currentY);
                currentY += 20;
                gfx.DrawLine(new XPen(colors.Border), margin, currentY, page.Width - margin, currentY);
                currentY += 15;
            }

            // === 4. TOTALS SECTION ===
            currentY += 20;

            double labelX = page.Width - margin - 100;
            double valueX = page.Width - margin;

            // --- Subtotal ---
            gfx.DrawString("Subtotal", fonts.Body, new XSolidBrush(colors.Text), new XRect(0, currentY, labelX, 0), XStringFormats.TopRight);
            gfx.DrawString($"${grandTotal:N2}", fonts.Body, new XSolidBrush(colors.Text), new XRect(0, currentY, valueX, 0), XStringFormats.TopRight);
            currentY += 20;

            // --- Tax ---
            gfx.DrawString("Tax (0.00%)", fonts.Body, new XSolidBrush(colors.SubtleText), new XRect(0, currentY, labelX, 0), XStringFormats.TopRight);
            gfx.DrawString("$0.00", fonts.Body, new XSolidBrush(colors.SubtleText), new XRect(0, currentY, valueX, 0), XStringFormats.TopRight);
            currentY += 25;

            // --- Separator Line ---
            gfx.DrawLine(new XPen(colors.Primary, 2), labelX - 80, currentY, valueX, currentY);
            currentY += 10;

            // --- Grand Total ---
            gfx.DrawString("TOTAL", fonts.H1, new XSolidBrush(colors.Primary), new XRect(0, currentY, labelX, 0), XStringFormats.TopRight);
            gfx.DrawString($"${grandTotal:N2}", fonts.H1, new XSolidBrush(colors.Primary), new XRect(0, currentY, valueX, 0), XStringFormats.TopRight);

            // === 5. FOOTER ===
            currentY = page.Height - margin - 50;
            gfx.DrawRectangle(new XSolidBrush(colors.Border), XUnit.FromPoint(0), XUnit.FromPoint(currentY), XUnit.FromPoint(page.Width), XUnit.FromPoint(90));
            gfx.DrawString("Terms & Conditions", fonts.H2, new XSolidBrush(colors.Text), margin, currentY + 20);
            gfx.DrawString("Payment is due within 30 days. Thank you for your business.", fonts.Small, new XSolidBrush(colors.SubtleText), margin, currentY + 35);

            // --- Save the document to a memory stream ---
            byte[] fileContents;
            using (MemoryStream stream = new MemoryStream())
            {
                document.Save(stream, true);
                fileContents = stream.ToArray();
            }

            return fileContents;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to generate PDF: {ex.Message}", ex);
            }
        }
    }
}
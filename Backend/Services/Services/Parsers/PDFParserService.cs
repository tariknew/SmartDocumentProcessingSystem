using Model.Dtos.Responses;
using Services.Interfaces.Parsers;
using System.Globalization;
using UglyToad.PdfPig;
using UglyToad.PdfPig.DocumentLayoutAnalysis.TextExtractor;

namespace Services.Services.Parsers
{
    public class PdfParserService : IParserService
    {
        public ParserResponse Parse(Stream fileStream)
        {
            var text = ConvertPdfToText(fileStream);

            return ParseDocument(text);
        }
        private string ConvertPdfToText(Stream stream)
        {
            using var doc = PdfDocument.Open(stream);

            if (doc.NumberOfPages == 0)
                return string.Empty;

            var page = doc.GetPage(1);

            return ContentOrderTextExtractor.GetText(page);
        }
        public bool CanParse(string fileType) => fileType.Equals(".pdf", StringComparison.OrdinalIgnoreCase);
        private ParserResponse ParseDocument(string text)
        {
            var invoice = new ParserResponse();

            var lines = text
                .Replace("\r", "")
                .Split('\n', StringSplitOptions.RemoveEmptyEntries);

            foreach (var raw in lines)
            {
                var line = raw.Trim();

                if (line.StartsWith("Invoice"))
                    invoice.DocumentType = "Invoice";

                else if (line.StartsWith("Purchase Order"))
                    invoice.DocumentType = "Purchase Order";

                else if (line.StartsWith("Supplier:"))
                    invoice.SupplierName = GetValue(line, "Supplier:");

                else if (line.StartsWith("Number:"))
                    invoice.DocumentNumber = GetValue(line, "Number:");

                else if (line.StartsWith("IssueDate:"))
                {
                    var dateStr = GetValue(line, "IssueDate:");
                    if (DateTime.TryParse(dateStr, out var date))
                        invoice.IssueDate = date;
                }

                else if (line.StartsWith("DueDate:"))
                {
                    var dateStr = GetValue(line, "DueDate:");
                    if (DateTime.TryParse(dateStr, out var date))
                        invoice.DueDate = date;
                }

                else if (line.StartsWith("Subtotal"))
                    invoice.Subtotal = GetLastNumber(line);

                else if (line.StartsWith("Tax"))
                {
                    invoice.Tax = GetLastNumber(line);
                    invoice.TaxRate = decimal.Parse(line.Split('(', ')')[1].Replace("%", "")) / 100;
                }

                else if (line.StartsWith("Total"))
                    invoice.Total = GetLastNumber(line);

                else if (line.StartsWith("Currency:"))
                    invoice.Currency = GetValue(line, "Currency:");

                else if (!line.StartsWith("Description") &&
                !line.StartsWith("Invoice") &&
                !line.StartsWith("Purchase Order"))
                {
                    var item = TryParseItem(line);
                    if (item != null)
                        invoice.LineItems.Add(item);
                }
            }

            return invoice;
        }
        private string GetValue(string line, string key) => line.Replace(key, "").Trim();
        private decimal GetLastNumber(string line)
        {
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var last = parts.Last();

            decimal.TryParse(last, NumberStyles.Any, CultureInfo.InvariantCulture, out var value);
            return value;
        }
        private LineItem? TryParseItem(string line)
        {
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 5)
                return null;

            if (int.TryParse(parts[2], out int qty) &&
                decimal.TryParse(parts[3], NumberStyles.Any, CultureInfo.InvariantCulture, out decimal price) &&
                decimal.TryParse(parts[4], NumberStyles.Any, CultureInfo.InvariantCulture, out decimal total))
            {
                return new LineItem
                {
                    Description = string.Join(" ", parts.Take(parts.Length - 3)),
                    Quantity = qty,
                    UnitPrice = price,
                    Total = total
                };
            }

            return null;
        }
    }
}


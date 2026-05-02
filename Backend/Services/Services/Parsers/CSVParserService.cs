using Model.Dtos.Responses;
using Services.Interfaces.Parsers;

namespace Services.Services.Parsers
{
    public class CSVParserService : IParserService
    {
        public ParserResponse Parse(Stream fileStream)
        {
            using var reader = new StreamReader(fileStream);

            var csv = reader.ReadToEnd();

            var lines = csv
                .Replace("\r", "")
                .Split('\n', StringSplitOptions.RemoveEmptyEntries)
                .Skip(1);

            var result = new ParserResponse
            {
                LineItems = new List<LineItem>()
            };

            foreach (var line in lines)
            {
                var parts = line.Split(',');

                // All rows belong to one document — initialize basic data once, then only add line items
                if (string.IsNullOrEmpty(result.DocumentNumber))
                {
                    result.DocumentType = parts[0];
                    result.SupplierName = parts[1];
                    result.DocumentNumber = parts[2];
                    result.IssueDate = DateTime.Parse(parts[3]);
                    result.DueDate = DateTime.Parse(parts[4]);
                    result.Currency = parts[5];
                    result.TaxRate = decimal.Parse(parts[6]);
                    result.Subtotal = decimal.Parse(parts[7]);
                    result.Tax = decimal.Parse(parts[8]);
                    result.Total = decimal.Parse(parts[9]);
                }

                result.LineItems.Add(new LineItem
                {
                    Description = parts[10],
                    Quantity = int.Parse(parts[11]),
                    UnitPrice = decimal.Parse(parts[12]),
                    Total = decimal.Parse(parts[13])
                });
            }

            return result;
        }
        public bool CanParse(string fileType) => fileType.Equals(".csv", StringComparison.OrdinalIgnoreCase);
    }
}

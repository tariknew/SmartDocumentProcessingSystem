using Model.Dtos.Responses;
using System;
using System.Linq;

namespace Helpers.Utilities
{
    public static class DocumentValidator
    {
        public static void Validate(ParserResponse doc)
        {
            ValidateDates(doc);
            ValidateTotals(doc);
            ValidateLineItems(doc);
        }
        public static void CleanStrings(ParserResponse doc)
        {
            doc.DocumentType = Normalize(doc.DocumentType);
            doc.SupplierName = Normalize(doc.SupplierName);
            doc.DocumentNumber = Normalize(doc.DocumentNumber);
            doc.Currency = Normalize(doc.Currency);
        }

        private static string Normalize(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : value;
        }
        private static void ValidateDates(ParserResponse doc)
        {
            if (doc.IssueDate > DateTime.Now)
            {
                doc.Errors.Add(new ValidationIssue
                {
                    FieldName = "IssueDate",
                    Message = "IssueDate cannot be in the future"
                });
            }
            if (doc.DueDate < doc.IssueDate)
            {
                doc.Errors.Add(new ValidationIssue
                {
                    FieldName = "DueDate",
                    Message = "DueDate cannot be before IssueDate"
                });
            }
        }
        private static void ValidateLineItems(ParserResponse doc)
        {
            int i = 0;

            foreach (var item in doc.LineItems)
            {
                var expected = item.Quantity * item.UnitPrice;

                var totalFromDoc = Math.Round(item.Total.Value, 2);
                var calculated = Math.Round(expected.Value, 2);

                if (totalFromDoc != calculated)
                {
                    doc.Errors.Add(new ValidationIssue
                    {
                        FieldName = $"LineItem-[{i}]",
                        Message = $"Line item '{item.Description}' has incorrect total (expected {calculated})"
                    });
                }
                i++;
            }
        }
        private static void ValidateTotals(ParserResponse doc)
        {
            var subtotalCalc = doc.LineItems
                .Sum(x => x.Quantity * x.UnitPrice);

            if (Math.Round(subtotalCalc.Value, 2) != Math.Round(doc.Subtotal.Value, 2))
            {
                doc.Errors.Add(new ValidationIssue
                {
                    FieldName = "subtotal",
                    Message = $"Subtotal mismatch (expected {Math.Round(subtotalCalc.Value, 2)})"
                });
            }

            decimal? expectedTax = 0;

            if (doc.TaxRate.HasValue)
            {
                expectedTax = subtotalCalc * doc.TaxRate.Value;

                if (Math.Round(expectedTax.Value, 2) != Math.Round(doc.Tax.Value, 2))
                {
                    doc.Errors.Add(new ValidationIssue
                    {
                        FieldName = "tax",
                        Message = $"Tax mismatch (expected {Math.Round(expectedTax.Value,2)})"
                    });
                }
            }

            var totalCalc = subtotalCalc + expectedTax;

            if (Math.Round(totalCalc.Value, 2) != Math.Round(doc.Total.Value, 2))
            {
                doc.Errors.Add(new ValidationIssue
                {
                    FieldName = "total",
                    Message = $"Total of all line items with tax mismatch (expected {Math.Round(totalCalc.Value,2)})"
                });
            }
        }
    }
}

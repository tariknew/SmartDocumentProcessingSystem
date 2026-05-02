using System;
using System.Collections.Generic;

namespace Model.Dtos.Responses
{
    public class ParserResponse
    {
        public int? Id { get; set; }
        public string? DocumentType { get; set; }
        public string? SupplierName { get; set; }
        public string? DocumentNumber { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? DueDate { get; set; }
        public string? Currency { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal? Tax { get; set; }
        public decimal? TaxRate { get; set; }
        public decimal? Total { get; set; }
        public string? Status { get; set; }
        public ICollection<LineItem> LineItems { get; set; } = new List<LineItem>();
        public ICollection<ValidationIssue> Errors { get; set; } = new List<ValidationIssue>();
        public int ErrorCount => Errors?.Count ?? 0;
    }
    public class LineItem
    {
        public int? Id { get; set; }
        public int? DocumentId { get; set; }
        public string? Description { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? Total { get; set; }
    }
    public class ValidationIssue
    {
        public string? FieldName { get; set; }
        public string? Message { get; set; }
        public int DocumentId { get; set; }
        public int Issues { get; set; }
    }
}

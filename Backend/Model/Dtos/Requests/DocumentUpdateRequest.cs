using System;
using System.Collections.Generic;

namespace Model.Dtos.Requests
{
    public class DocumentUpdateRequest
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
        public ICollection<LineItemInsertRequest> LineItems { get; set; } = new List<LineItemInsertRequest>();
    }
    public class LineItemInsertRequest
    {
        public int? Id { get; set; }
        public int? DocumentId { get; set; }
        public string? Description { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? Total { get; set; }
    }
}

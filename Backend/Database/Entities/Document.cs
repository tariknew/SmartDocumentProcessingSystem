using System.ComponentModel.DataAnnotations;

namespace Database.Entities
{
    public class Document
    {
        [Key]
        public int Id { get; set; }
        public string? DocumentType { get; set; }
        public string? SupplierName { get; set; }
        public string? DocumentNumber { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? DueDate { get; set; }
        public string? Currency { get; set; }
        public virtual ICollection<LineItem> LineItems { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal? Tax { get; set; }
        public decimal? Total { get; set; }
        public decimal? TaxRate { get; set; }
        public string Status { get; set; }
    }
}

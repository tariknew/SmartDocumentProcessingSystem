using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities
{
    public class LineItem
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Document")]
        public int DocumentId { get; set; }
        public virtual Document Document { get; set; }
        public string? Description { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? Total { get; set; }
    }
}

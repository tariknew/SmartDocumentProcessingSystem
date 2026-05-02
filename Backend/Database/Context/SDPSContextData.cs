using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database.Context
{
    public partial class SDPSContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Document>().HasData(new Document
            {
                Id = 1,
                DocumentType = "Invoice",
                SupplierName = "Company X",
                DocumentNumber = "INV-111",
                IssueDate = new DateTime(2026, 04, 30),
                DueDate = new DateTime(2026, 05, 10),
                Currency = "EUR",
                TaxRate = 0.17m,
                Subtotal = 246,
                Tax = 41.82m,
                Total = 287.82m,
                Status = "Uploaded"
            },
            new Document
            {
                Id = 2,
                DocumentType = "Invoice",
                SupplierName = "Company 0",
                DocumentNumber = "INV-222",
                IssueDate = new DateTime(2026, 08, 02),
                DueDate = new DateTime(2026, 09, 01),
                Currency = "BAM",
                TaxRate = 0.2m,
                Subtotal = 645,
                Tax = 129,
                Total = 774,
                Status = "Uploaded"
            },
                        new Document
                        {
                            Id = 3,
                            DocumentType = "Purchase Order",
                            SupplierName = "Buyer 0",
                            DocumentNumber = "INV-333",
                            IssueDate = new DateTime(2026, 04, 28),
                            DueDate = null,
                            Currency = "BAM",
                            TaxRate = 0.5m,
                            Subtotal = 213,
                            Tax = 7,
                            Total = 9,
                            Status = "Uploaded"
                        },
                           new Document
                           {
                               Id = 4,
                               DocumentType = null,
                               SupplierName = null,
                               DocumentNumber = null,
                               IssueDate = new DateTime(2026, 08, 02),
                               DueDate = new DateTime(2026, 09, 01),
                               Currency = "EUR",
                               TaxRate = 0.2m,
                               Subtotal = 645,
                               Tax = 129,
                               Total = 774,
                               Status = "Uploaded"
                           });

            modelBuilder.Entity<LineItem>().HasData(new LineItem
            {
                Id = 1,
                DocumentId = 1,
                Description = "Item1",
                Quantity = 1,
                UnitPrice = 78,
                Total = 78
            },
            new LineItem
            {
                Id = 2,
                DocumentId = 1,
                Description = "Item2",
                Quantity = 2,
                UnitPrice = 84,
                Total = 168
            },
            new LineItem
            {
                Id = 3,
                DocumentId = 2,
                Description = "Service A",
                Quantity = 5,
                UnitPrice = 129,
                Total = 645
            },
            new LineItem
            {
                Id = 4,
                DocumentId = 3,
                Description = "Service B",
                Quantity = 3,
                UnitPrice = 96,
                Total = 204
            },
              new LineItem
              {
                  Id = 5,
                  DocumentId = 4,
                  Description = "Service C",
                  Quantity = 5,
                  UnitPrice = 129,
                  Total = 645
              });
        }
    }
}

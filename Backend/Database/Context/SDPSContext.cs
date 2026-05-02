using Database.Entities;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Database.Context
{
    public partial class SDPSContext : DbContext
    {
        public SDPSContext() { }
        public SDPSContext(DbContextOptions<SDPSContext> options) : base(options) { }
        public DbSet<Document> Documents { get; set; }
        public DbSet<LineItem> LineItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Document>()
                .Property(x => x.Subtotal)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Document>()
                .Property(x => x.Tax)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Document>()
                .Property(x => x.TaxRate)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Document>()
                .Property(x => x.Total)
                .HasPrecision(18, 2);

            modelBuilder.Entity<LineItem>()
                .Property(x => x.UnitPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<LineItem>()
                .Property(x => x.Total)
                .HasPrecision(18, 2);

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

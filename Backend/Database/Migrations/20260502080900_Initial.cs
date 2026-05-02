using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Total = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    TaxRate = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LineItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Total = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LineItems_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Documents",
                columns: new[] { "Id", "Currency", "DocumentNumber", "DocumentType", "DueDate", "IssueDate", "Status", "Subtotal", "SupplierName", "Tax", "TaxRate", "Total" },
                values: new object[,]
                {
                    { 1, "EUR", "INV-111", "Invoice", new DateTime(2026, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Uploaded", 246m, "Company X", 41.82m, 0.17m, 287.82m },
                    { 2, "BAM", "INV-222", "Invoice", new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Uploaded", 645m, "Company 0", 129m, 0.2m, 774m },
                    { 3, "BAM", "INV-333", "Purchase Order", null, new DateTime(2026, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Uploaded", 213m, "Buyer 0", 7m, 0.5m, 9m },
                    { 4, "EUR", null, null, new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Uploaded", 645m, null, 129m, 0.2m, 774m }
                });

            migrationBuilder.InsertData(
                table: "LineItems",
                columns: new[] { "Id", "Description", "DocumentId", "Quantity", "Total", "UnitPrice" },
                values: new object[,]
                {
                    { 1, "Item1", 1, 1, 78m, 78m },
                    { 2, "Item2", 1, 2, 168m, 84m },
                    { 3, "Service A", 2, 5, 645m, 129m },
                    { 4, "Service B", 3, 3, 204m, 96m },
                    { 5, "Service C", 4, 5, 645m, 129m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LineItems_DocumentId",
                table: "LineItems",
                column: "DocumentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LineItems");

            migrationBuilder.DropTable(
                name: "Documents");
        }
    }
}

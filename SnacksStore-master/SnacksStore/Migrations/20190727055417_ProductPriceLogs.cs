using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace SnacksStore.Migrations
{
    public partial class ProductPriceLogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductPriceLogs",
                columns: table => new {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<int>(nullable: false),
                    OldPrice = table.Column<decimal>(type: "money", nullable: false),
                    NewPrice = table.Column<decimal>(type: "money", nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql:"GETDATE()"),
                    CreatedBy = table.Column<int>(nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_ProductPriceLogs", x => x.Id);
                    table.ForeignKey(
                      name: "FK_ProductPriceLogs_Products",
                      column: x => x.ProductId,
                      principalTable: "Products",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                      name: "FK_ProductPriceLogs_CreatedBy",
                      column: x => x.CreatedBy,
                      principalTable: "Users",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Restrict);
                }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductPriceLogs");
        }
    }
}

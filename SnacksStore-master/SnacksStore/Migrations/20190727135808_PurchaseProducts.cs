using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace SnacksStore.Migrations
{
    public partial class PurchaseProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "PurchaseProducts",
               columns: table => new {
                   Id = table.Column<int>(nullable: false)
                       .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                   PurchaseId = table.Column<int>(nullable: false),
                   ProductId = table.Column<int>(nullable: false),
                   ProductQuantity = table.Column<int>(nullable: false),
                   Price = table.Column<decimal>(type: "money", nullable: false),
                   SubTotal = table.Column<decimal>(type: "money", nullable: false),
                   CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                   CreatedBy = table.Column<int>(nullable: false),
                   UpdatedAt = table.Column<DateTime>(nullable: true),
                   UpdatedBy = table.Column<int>(nullable: true)
               },
               constraints: table => {
                   table.PrimaryKey("PK_PurchaseProducts", x => x.Id);
                   table.ForeignKey(
                    name: "FK_PurchaseProducts_PurchaseId",
                    column: x => x.PurchaseId,
                    principalTable: "Purchases",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                   table.ForeignKey(
                     name: "FK_PurchaseProducts_ProductId",
                     column: x => x.ProductId,
                     principalTable: "Products",
                     principalColumn: "Id",
                     onDelete: ReferentialAction.Restrict);
                   table.ForeignKey(
                     name: "FK_PurchaseProducts_CreatedBy",
                     column: x => x.CreatedBy,
                     principalTable: "Users",
                     principalColumn: "Id",
                     onDelete: ReferentialAction.Restrict);
                   table.ForeignKey(
                     name: "FK_PurchaseProducts_UpdatedBy",
                     column: x => x.UpdatedBy,
                     principalTable: "Users",
                     principalColumn: "Id",
                     onDelete: ReferentialAction.Restrict);
               }
           );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "PurchaseProducts");
        }
    }
}

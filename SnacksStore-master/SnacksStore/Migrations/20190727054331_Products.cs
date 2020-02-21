using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SnacksStore.Migrations
{
    public partial class Products : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SKU = table.Column<string>(nullable: false, maxLength: 36, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(nullable: true, maxLength: 150),
                    Description = table.Column<string>(nullable: true, maxLength: 500),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    Stock = table.Column<int>(nullable: false),
                    Likes = table.Column<int>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false, maxLength: 25),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<int>(nullable: true, maxLength: 25)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.UniqueConstraint("UQ_Products_SKU", x => x.SKU);
                    table.ForeignKey(
                      name: "FK_Products_CreatedBy",
                      column: x => x.CreatedBy,
                      principalTable: "Users",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                      name: "FK_Products_UpdatedBy",
                      column: x => x.UpdatedBy,
                      principalTable: "Users",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Restrict);
                });

         
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

          
        }
    }
}

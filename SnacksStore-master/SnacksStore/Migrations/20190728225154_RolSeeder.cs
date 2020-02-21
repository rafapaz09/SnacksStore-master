using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SnacksStore.Migrations
{
    public partial class RolSeeder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Name", "Active","CreatedAt" },
                values: new object[] { "Admin", true, DateTime.Now }
            );

            migrationBuilder.InsertData(
               table: "Roles",
               columns: new[] { "Name", "Active", "CreatedAt" },
               values: new object[] { "Client", true, DateTime.Now }
           );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"TRUNCATE TABLE Roles");
        }
    }
}

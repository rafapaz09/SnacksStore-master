using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

using System;

namespace SnacksStore.Migrations
{
    public partial class Users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "Users",
               columns: table => new {
                   Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                   Username = table.Column<string>(nullable: false, maxLength: 50),
                   Password = table.Column<string>(nullable: false, maxLength: 250),
                   PasswordSalt = table.Column<string>(nullable: false, maxLength: 100),
                   RolId = table.Column<int>(nullable: false),
                   Active = table.Column<bool>(nullable: false),
                   CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()")
               },
               constraints: table => {
                   table.PrimaryKey("PK_Users", x => x.Id);
                   table.ForeignKey(
                       name: "FK_Users_Roles",
                       column: x => x.RolId,
                       principalTable: "Roles",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.Restrict);
                   table.UniqueConstraint("UQ_Users_Username", x => x.Username);
               }
           );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
              name: "Users"
          );
        }
    }
}

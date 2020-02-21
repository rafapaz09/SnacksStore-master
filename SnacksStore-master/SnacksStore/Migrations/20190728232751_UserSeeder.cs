using Microsoft.EntityFrameworkCore.Migrations;
using SnacksStore.Helpers.Security;
using System;

namespace SnacksStore.Migrations
{
    public partial class UserSeeder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var userSalt = PasswordHasher.GetSalt();
            var userPassword = PasswordHasher.GetHash("k3vin19*" + userSalt);
            migrationBuilder.InsertData(
               table: "Users",
               columns: new[] { "Username", "Password", "PasswordSalt", "RolId", "Active","CreatedAt" },
               values: new object[] { "admin",userPassword,userSalt, 1, true, DateTime.Now }
           );

            userSalt = PasswordHasher.GetSalt();
            userPassword = PasswordHasher.GetHash("1234" + userSalt);
            migrationBuilder.InsertData(
               table: "Users",
               columns: new[] { "Username", "Password", "PasswordSalt", "RolId", "Active", "CreatedAt" },
               values: new object[] { "ricky", userPassword, userSalt, 2, true, DateTime.Now }
           );

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"TRUNCATE TABLE Users");
        }
    }
}

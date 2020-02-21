using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SnacksStore.Migrations
{
    public partial class ProductSeeder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
              table: "Products",
              columns: new[] { "SKU", "Name","Description", "Price", "Stock", "Active", "CreatedAt", "CreatedBy" },
              values: new object[] { Guid.NewGuid().ToString(), "Apple", "Red Apples", 0.75,1000, true, DateTime.Now,1 }
            );

            migrationBuilder.InsertData(
              table: "Products",
              columns: new[] { "SKU", "Name", "Description", "Price", "Stock", "Active", "CreatedAt", "CreatedBy" },
              values: new object[] { Guid.NewGuid().ToString(), "Coffee", "Coffee", 3.5, 20, true, DateTime.Now, 1 }
            );

            migrationBuilder.InsertData(
              table: "Products",
              columns: new[] { "SKU", "Name", "Description", "Price", "Stock", "Active", "CreatedAt", "CreatedBy" },
              values: new object[] { Guid.NewGuid().ToString(), "Smoothie", "Red fruit smoothie", 0.75, 100, true, DateTime.Now, 1 }
            );

            migrationBuilder.InsertData(
              table: "Products",
              columns: new[] { "SKU", "Name", "Description", "Price", "Stock", "Active", "CreatedAt", "CreatedBy" },
              values: new object[] { Guid.NewGuid().ToString(), "Chocolate", "", 2, 1000, true, DateTime.Now, 1 }
            );


            migrationBuilder.InsertData(
              table: "Products",
              columns: new[] { "SKU", "Name", "Description", "Price", "Stock", "Active", "CreatedAt", "CreatedBy" },
              values: new object[] { Guid.NewGuid().ToString(), "Nachos", "Nachos with cheese", 5, 10, true, DateTime.Now, 1 }
            );

            migrationBuilder.InsertData(
              table: "Products",
              columns: new[] { "SKU", "Name", "Description", "Price", "Stock", "Active", "CreatedAt", "CreatedBy" },
              values: new object[] { Guid.NewGuid().ToString(), "Popcorn", "Red Apples", 3.75, 1, true, DateTime.Now, 1 }
            );

            migrationBuilder.InsertData(
              table: "Products",
              columns: new[] { "SKU", "Name", "Description", "Price", "Stock", "Active", "CreatedAt", "CreatedBy" },
              values: new object[] { Guid.NewGuid().ToString(), "Candy", "", 0.75, 1000, true, DateTime.Now, 1 }
            );

            migrationBuilder.InsertData(
              table: "Products",
              columns: new[] { "SKU", "Name", "Description", "Price", "Stock", "Active", "CreatedAt", "CreatedBy" },
              values: new object[] { Guid.NewGuid().ToString(), "Dorito", "", 1, 10, true, DateTime.Now, 1 }
            );

        }
    }
}

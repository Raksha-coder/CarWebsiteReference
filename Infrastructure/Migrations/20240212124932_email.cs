using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class email : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "RegisterTable",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "CarTable",
                columns: new[] { "Id", "Category", "ContactDetails", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("848a8b95-4782-4757-8cb5-03e0e685172c"), "buy", "222222", "fordTwo", 50000.0 },
                    { new Guid("a60a3ddf-dc1a-4f92-84a9-59cdf41ea408"), "sell", "111111", "fordOne", 250000.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CarTable",
                keyColumn: "Id",
                keyValue: new Guid("848a8b95-4782-4757-8cb5-03e0e685172c"));

            migrationBuilder.DeleteData(
                table: "CarTable",
                keyColumn: "Id",
                keyValue: new Guid("a60a3ddf-dc1a-4f92-84a9-59cdf41ea408"));

            migrationBuilder.DropColumn(
                name: "Email",
                table: "RegisterTable");
        }
    }
}

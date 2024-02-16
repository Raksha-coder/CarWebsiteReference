using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedPhoneNumberinRegistration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CarTable",
                keyColumn: "Id",
                keyValue: new Guid("8efa7487-2529-496c-9e65-b633a31ac26b"));

            migrationBuilder.DeleteData(
                table: "CarTable",
                keyColumn: "Id",
                keyValue: new Guid("d7f5ad92-17b1-40a3-ae5b-2a960b811d47"));

            migrationBuilder.AddColumn<string>(
                name: "PhoneNum",
                table: "RegisterTable",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "CarTable",
                columns: new[] { "Id", "Category", "ContactDetails", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("93bd5422-2a42-4170-801f-c61c623ec9a8"), "buy", "222222", "fordTwo", 50000.0 },
                    { new Guid("b9b15ae1-8a89-4587-b0e7-1da0b5bed453"), "sell", "111111", "fordOne", 250000.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CarTable",
                keyColumn: "Id",
                keyValue: new Guid("93bd5422-2a42-4170-801f-c61c623ec9a8"));

            migrationBuilder.DeleteData(
                table: "CarTable",
                keyColumn: "Id",
                keyValue: new Guid("b9b15ae1-8a89-4587-b0e7-1da0b5bed453"));

            migrationBuilder.DropColumn(
                name: "PhoneNum",
                table: "RegisterTable");

            migrationBuilder.InsertData(
                table: "CarTable",
                columns: new[] { "Id", "Category", "ContactDetails", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("8efa7487-2529-496c-9e65-b633a31ac26b"), "buy", "222222", "fordTwo", 50000.0 },
                    { new Guid("d7f5ad92-17b1-40a3-ae5b-2a960b811d47"), "sell", "111111", "fordOne", 250000.0 }
                });
        }
    }
}

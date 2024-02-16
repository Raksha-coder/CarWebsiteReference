using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class otpExpirationTIMEONLY : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CarTable",
                keyColumn: "Id",
                keyValue: new Guid("0ecf0a3e-4ab2-4683-9a7a-fdafb3fd1faa"));

            migrationBuilder.DeleteData(
                table: "CarTable",
                keyColumn: "Id",
                keyValue: new Guid("40fbeb1c-cab8-400c-abca-afb0d8ad047e"));

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "CurrentTime",
                table: "OtpVerificationTable",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "CarTable",
                columns: new[] { "Id", "Category", "ContactDetails", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("cbe8e7ef-4b61-4c45-9963-745d26431351"), "buy", "222222", "fordTwo", 50000.0 },
                    { new Guid("e228cdc4-b63c-43b5-80c7-1b209fa1404b"), "sell", "111111", "fordOne", 250000.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CarTable",
                keyColumn: "Id",
                keyValue: new Guid("cbe8e7ef-4b61-4c45-9963-745d26431351"));

            migrationBuilder.DeleteData(
                table: "CarTable",
                keyColumn: "Id",
                keyValue: new Guid("e228cdc4-b63c-43b5-80c7-1b209fa1404b"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CurrentTime",
                table: "OtpVerificationTable",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.InsertData(
                table: "CarTable",
                columns: new[] { "Id", "Category", "ContactDetails", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("0ecf0a3e-4ab2-4683-9a7a-fdafb3fd1faa"), "buy", "222222", "fordTwo", 50000.0 },
                    { new Guid("40fbeb1c-cab8-400c-abca-afb0d8ad047e"), "sell", "111111", "fordOne", 250000.0 }
                });
        }
    }
}

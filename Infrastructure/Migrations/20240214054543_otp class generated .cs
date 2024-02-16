using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class otpclassgenerated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CarTable",
                keyColumn: "Id",
                keyValue: new Guid("147d4d86-d109-4ec8-a8ba-8adc298f24dd"));

            migrationBuilder.DeleteData(
                table: "CarTable",
                keyColumn: "Id",
                keyValue: new Guid("caad1f2d-eeb6-4408-a20c-c87d1927b70a"));

            migrationBuilder.CreateTable(
                name: "OtpVerificationTable",
                columns: table => new
                {
                    UserId = table.Column<Guid>(name: "User_Id", type: "uniqueidentifier", nullable: false),
                    OTP = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtpVerificationTable", x => x.UserId);
                });

            migrationBuilder.InsertData(
                table: "CarTable",
                columns: new[] { "Id", "Category", "ContactDetails", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("8efa7487-2529-496c-9e65-b633a31ac26b"), "buy", "222222", "fordTwo", 50000.0 },
                    { new Guid("d7f5ad92-17b1-40a3-ae5b-2a960b811d47"), "sell", "111111", "fordOne", 250000.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OtpVerificationTable");

            migrationBuilder.DeleteData(
                table: "CarTable",
                keyColumn: "Id",
                keyValue: new Guid("8efa7487-2529-496c-9e65-b633a31ac26b"));

            migrationBuilder.DeleteData(
                table: "CarTable",
                keyColumn: "Id",
                keyValue: new Guid("d7f5ad92-17b1-40a3-ae5b-2a960b811d47"));

            migrationBuilder.InsertData(
                table: "CarTable",
                columns: new[] { "Id", "Category", "ContactDetails", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("147d4d86-d109-4ec8-a8ba-8adc298f24dd"), "buy", "222222", "fordTwo", 50000.0 },
                    { new Guid("caad1f2d-eeb6-4408-a20c-c87d1927b70a"), "sell", "111111", "fordOne", 250000.0 }
                });
        }
    }
}

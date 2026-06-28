using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniERP.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddSaleStatusFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CancelReason",
                table: "Sales",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CancelledDate",
                table: "Sales",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Sales",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CancelReason",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "CancelledDate",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Sales");
        }
    }
}

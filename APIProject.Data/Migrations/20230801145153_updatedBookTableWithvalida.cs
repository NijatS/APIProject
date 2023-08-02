using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatedBookTableWithvalida : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 1, 18, 51, 53, 343, DateTimeKind.Utc).AddTicks(2462),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 7, 31, 19, 24, 47, 661, DateTimeKind.Utc).AddTicks(3524));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Books",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 1, 18, 51, 53, 343, DateTimeKind.Utc).AddTicks(3642),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 7, 31, 19, 24, 47, 661, DateTimeKind.Utc).AddTicks(4963));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 31, 19, 24, 47, 661, DateTimeKind.Utc).AddTicks(3524),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 8, 1, 18, 51, 53, 343, DateTimeKind.Utc).AddTicks(2462));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Books",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 31, 19, 24, 47, 661, DateTimeKind.Utc).AddTicks(4963),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 8, 1, 18, 51, 53, 343, DateTimeKind.Utc).AddTicks(3642));
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartGallery.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class removeTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "Reservation");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Reservation",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldDefaultValue: "Pending");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Reservation",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "Pending",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AddColumn<int>(
                name: "Value",
                table: "Reservation",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

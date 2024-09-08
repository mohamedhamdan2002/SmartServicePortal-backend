using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartGallery.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAdreesAndContactForReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address_City",
                table: "Reservation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Country",
                table: "Reservation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Street",
                table: "Reservation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Contact_FirstName",
                table: "Reservation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Contact_LastName",
                table: "Reservation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Contact_PhoneNumber",
                table: "Reservation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProblemDescription",
                table: "Reservation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address_City",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "Address_Country",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "Address_Street",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "Contact_FirstName",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "Contact_LastName",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "Contact_PhoneNumber",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "ProblemDescription",
                table: "Reservation");
        }
    }
}

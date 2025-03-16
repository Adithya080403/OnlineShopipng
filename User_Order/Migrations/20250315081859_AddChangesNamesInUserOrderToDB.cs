using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace User_Order.Migrations
{
    /// <inheritdoc />
    public partial class AddChangesNamesInUserOrderToDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Users",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "UserPhoneNum",
                table: "Users",
                newName: "UserPhoneNumber");

            migrationBuilder.RenameColumn(
                name: "Ordername",
                table: "Orders",
                newName: "OrderName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Users",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "UserPhoneNumber",
                table: "Users",
                newName: "UserPhoneNum");

            migrationBuilder.RenameColumn(
                name: "OrderName",
                table: "Orders",
                newName: "Ordername");
        }
    }
}

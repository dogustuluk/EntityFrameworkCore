using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFrameworkCore.Model.Migrations
{
    public partial class updateOwnedType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Person_LastName",
                table: "Managers",
                newName: "Soyadı");

            migrationBuilder.RenameColumn(
                name: "Person_FirstName",
                table: "Managers",
                newName: "Adı");

            migrationBuilder.RenameColumn(
                name: "Person_Age",
                table: "Managers",
                newName: "Yaşı");

            migrationBuilder.RenameColumn(
                name: "Person_LastName",
                table: "Employees",
                newName: "Soyadı");

            migrationBuilder.RenameColumn(
                name: "Person_FirstName",
                table: "Employees",
                newName: "Adı");

            migrationBuilder.RenameColumn(
                name: "Person_Age",
                table: "Employees",
                newName: "Yaşı");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Yaşı",
                table: "Managers",
                newName: "Person_Age");

            migrationBuilder.RenameColumn(
                name: "Soyadı",
                table: "Managers",
                newName: "Person_LastName");

            migrationBuilder.RenameColumn(
                name: "Adı",
                table: "Managers",
                newName: "Person_FirstName");

            migrationBuilder.RenameColumn(
                name: "Yaşı",
                table: "Employees",
                newName: "Person_Age");

            migrationBuilder.RenameColumn(
                name: "Soyadı",
                table: "Employees",
                newName: "Person_LastName");

            migrationBuilder.RenameColumn(
                name: "Adı",
                table: "Employees",
                newName: "Person_FirstName");
        }
    }
}

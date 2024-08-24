using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElmnasaInfrastructure.AppContext.Migrations
{
    public partial class editAccountSubject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Teacher_id",
                table: "Subject",
                newName: "Account_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Account_id",
                table: "Subject",
                newName: "Teacher_id");
        }
    }
}

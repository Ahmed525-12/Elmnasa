using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElmnasaInfrastructure.AppContext.Migrations
{
    public partial class addteacheridtoquestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Grade",
                table: "Quiz",
                newName: "Degree");

            migrationBuilder.AddColumn<string>(
                name: "Teacher_id",
                table: "Question",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Teacher_id",
                table: "Question");

            migrationBuilder.RenameColumn(
                name: "Degree",
                table: "Quiz",
                newName: "Grade");
        }
    }
}

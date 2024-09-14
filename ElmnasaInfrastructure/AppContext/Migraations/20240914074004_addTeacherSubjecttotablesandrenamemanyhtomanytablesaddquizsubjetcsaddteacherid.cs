using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElmnasaInfrastructure.AppContext.Migraations
{
    public partial class addTeacherSubjecttotablesandrenamemanyhtomanytablesaddquizsubjetcsaddteacherid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Teacher_id",
                table: "TeacherSubject",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Teacher_id",
                table: "TeacherSubject");
        }
    }
}

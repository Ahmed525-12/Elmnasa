using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElmnasaInfrastructure.AppContext.Migrations
{
    public partial class removeTeacherIdFromSubscribeSubject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Teacher_id",
                table: "SubscribeSubject");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Teacher_id",
                table: "SubscribeSubject",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

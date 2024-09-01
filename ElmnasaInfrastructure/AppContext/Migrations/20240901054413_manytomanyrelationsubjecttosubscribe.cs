using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElmnasaInfrastructure.AppContext.Migrations
{
    public partial class manytomanyrelationsubjecttosubscribe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subject_SubscribeSubject_SubscribeSubjectId",
                table: "Subject");

            migrationBuilder.DropIndex(
                name: "IX_Subject_SubscribeSubjectId",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "SubscribeSubjectId",
                table: "Subject");

            migrationBuilder.CreateTable(
                name: "StudentSubscribeSubject",
                columns: table => new
                {
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    SubscribeSubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSubscribeSubject", x => new { x.SubjectId, x.SubscribeSubjectId });
                    table.ForeignKey(
                        name: "FK_StudentSubscribeSubject_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentSubscribeSubject_SubscribeSubject_SubscribeSubjectId",
                        column: x => x.SubscribeSubjectId,
                        principalTable: "SubscribeSubject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubscribeSubject_SubscribeSubjectId",
                table: "StudentSubscribeSubject",
                column: "SubscribeSubjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentSubscribeSubject");

            migrationBuilder.AddColumn<int>(
                name: "SubscribeSubjectId",
                table: "Subject",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subject_SubscribeSubjectId",
                table: "Subject",
                column: "SubscribeSubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_SubscribeSubject_SubscribeSubjectId",
                table: "Subject",
                column: "SubscribeSubjectId",
                principalTable: "SubscribeSubject",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}

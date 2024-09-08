using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElmnasaInfrastructure.AppContext.Migrations
{
    public partial class addQuiztoquestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_Quiz_QuizId",
                table: "Question");

            migrationBuilder.DropForeignKey(
                name: "FK_subjectQuiz_Quiz_QuizId",
                table: "subjectQuiz");

            migrationBuilder.DropForeignKey(
                name: "FK_subjectQuiz_Subject_SubjectId",
                table: "subjectQuiz");

            migrationBuilder.DropIndex(
                name: "IX_Question_QuizId",
                table: "Question");

            migrationBuilder.DropPrimaryKey(
                name: "PK_subjectQuiz",
                table: "subjectQuiz");

            migrationBuilder.DropColumn(
                name: "QuizId",
                table: "Question");

            migrationBuilder.RenameTable(
                name: "subjectQuiz",
                newName: "QuizSubjects");

            migrationBuilder.RenameIndex(
                name: "IX_subjectQuiz_SubjectId",
                table: "QuizSubjects",
                newName: "IX_QuizSubjects_SubjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizSubjects",
                table: "QuizSubjects",
                columns: new[] { "QuizId", "SubjectId" });

            migrationBuilder.CreateTable(
                name: "QuizQuetions",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    QuizId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizQuetions", x => new { x.QuestionId, x.QuizId });
                    table.ForeignKey(
                        name: "FK_QuizQuetions_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuizQuetions_Quiz_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quiz",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuizQuetions_QuizId",
                table: "QuizQuetions",
                column: "QuizId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizSubjects_Quiz_QuizId",
                table: "QuizSubjects",
                column: "QuizId",
                principalTable: "Quiz",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuizSubjects_Subject_SubjectId",
                table: "QuizSubjects",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizSubjects_Quiz_QuizId",
                table: "QuizSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizSubjects_Subject_SubjectId",
                table: "QuizSubjects");

            migrationBuilder.DropTable(
                name: "QuizQuetions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizSubjects",
                table: "QuizSubjects");

            migrationBuilder.RenameTable(
                name: "QuizSubjects",
                newName: "subjectQuiz");

            migrationBuilder.RenameIndex(
                name: "IX_QuizSubjects_SubjectId",
                table: "subjectQuiz",
                newName: "IX_subjectQuiz_SubjectId");

            migrationBuilder.AddColumn<int>(
                name: "QuizId",
                table: "Question",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_subjectQuiz",
                table: "subjectQuiz",
                columns: new[] { "QuizId", "SubjectId" });

            migrationBuilder.CreateIndex(
                name: "IX_Question_QuizId",
                table: "Question",
                column: "QuizId");

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Quiz_QuizId",
                table: "Question",
                column: "QuizId",
                principalTable: "Quiz",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_subjectQuiz_Quiz_QuizId",
                table: "subjectQuiz",
                column: "QuizId",
                principalTable: "Quiz",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_subjectQuiz_Subject_SubjectId",
                table: "subjectQuiz",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

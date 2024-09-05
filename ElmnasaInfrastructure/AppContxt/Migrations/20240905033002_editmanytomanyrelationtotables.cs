using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElmnasaInfrastructure.AppContxt.Migrations
{
    public partial class editmanytomanyrelationtotables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Question_QuestionId",
                table: "Answer");

            migrationBuilder.DropForeignKey(
                name: "FK_Subject_Quiz_QuizId",
                table: "Subject");

            migrationBuilder.DropForeignKey(
                name: "FK_Subject_UploadPdf_UploadPdfId",
                table: "Subject");

            migrationBuilder.DropForeignKey(
                name: "FK_Subject_UploadVideo_UploadVideoId",
                table: "Subject");

            migrationBuilder.DropIndex(
                name: "IX_Subject_QuizId",
                table: "Subject");

            migrationBuilder.DropIndex(
                name: "IX_Subject_UploadPdfId",
                table: "Subject");

            migrationBuilder.DropIndex(
                name: "IX_Subject_UploadVideoId",
                table: "Subject");

            migrationBuilder.DropIndex(
                name: "IX_Answer_QuestionId",
                table: "Answer");

            migrationBuilder.DropColumn(
                name: "QuizId",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "UploadPdfId",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "UploadVideoId",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "Answer");

            migrationBuilder.AddColumn<int>(
                name: "Grade",
                table: "Quiz",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuizId",
                table: "Question",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AnswersQuetions",
                columns: table => new
                {
                    AnswersId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswersQuetions", x => new { x.AnswersId, x.QuestionId });
                    table.ForeignKey(
                        name: "FK_AnswersQuetions_Answer_AnswersId",
                        column: x => x.AnswersId,
                        principalTable: "Answer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnswersQuetions_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "subjectQuiz",
                columns: table => new
                {
                    QuizId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subjectQuiz", x => new { x.QuizId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_subjectQuiz_Quiz_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quiz",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_subjectQuiz_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "subjectuploadpdf",
                columns: table => new
                {
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    UploadPdfId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subjectuploadpdf", x => new { x.SubjectId, x.UploadPdfId });
                    table.ForeignKey(
                        name: "FK_subjectuploadpdf_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_subjectuploadpdf_UploadPdf_UploadPdfId",
                        column: x => x.UploadPdfId,
                        principalTable: "UploadPdf",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "subjectuploadVideo",
                columns: table => new
                {
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    UploadVideoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subjectuploadVideo", x => new { x.SubjectId, x.UploadVideoId });
                    table.ForeignKey(
                        name: "FK_subjectuploadVideo_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_subjectuploadVideo_UploadVideo_UploadVideoId",
                        column: x => x.UploadVideoId,
                        principalTable: "UploadVideo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Question_QuizId",
                table: "Question",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswersQuetions_QuestionId",
                table: "AnswersQuetions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_subjectQuiz_SubjectId",
                table: "subjectQuiz",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_subjectuploadpdf_UploadPdfId",
                table: "subjectuploadpdf",
                column: "UploadPdfId");

            migrationBuilder.CreateIndex(
                name: "IX_subjectuploadVideo_UploadVideoId",
                table: "subjectuploadVideo",
                column: "UploadVideoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Quiz_QuizId",
                table: "Question",
                column: "QuizId",
                principalTable: "Quiz",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_Quiz_QuizId",
                table: "Question");

            migrationBuilder.DropTable(
                name: "AnswersQuetions");

            migrationBuilder.DropTable(
                name: "subjectQuiz");

            migrationBuilder.DropTable(
                name: "subjectuploadpdf");

            migrationBuilder.DropTable(
                name: "subjectuploadVideo");

            migrationBuilder.DropIndex(
                name: "IX_Question_QuizId",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "Grade",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "QuizId",
                table: "Question");

            migrationBuilder.AddColumn<int>(
                name: "QuizId",
                table: "Subject",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UploadPdfId",
                table: "Subject",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UploadVideoId",
                table: "Subject",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuestionId",
                table: "Answer",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subject_QuizId",
                table: "Subject",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_UploadPdfId",
                table: "Subject",
                column: "UploadPdfId");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_UploadVideoId",
                table: "Subject",
                column: "UploadVideoId");

            migrationBuilder.CreateIndex(
                name: "IX_Answer_QuestionId",
                table: "Answer",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Question_QuestionId",
                table: "Answer",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_Quiz_QuizId",
                table: "Subject",
                column: "QuizId",
                principalTable: "Quiz",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_UploadPdf_UploadPdfId",
                table: "Subject",
                column: "UploadPdfId",
                principalTable: "UploadPdf",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_UploadVideo_UploadVideoId",
                table: "Subject",
                column: "UploadVideoId",
                principalTable: "UploadVideo",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElmnasaInfrastructure.AppContext.Migraations
{
    public partial class DropAlltables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnswersQuetions");

            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropTable(
                name: "QuizQuetions");

            migrationBuilder.DropTable(
                name: "QuizSubjects");

            migrationBuilder.DropTable(
                name: "StudentSubscribeSubject");

            migrationBuilder.DropTable(
                name: "subjectuploadpdf");

            migrationBuilder.DropTable(
                name: "subjectuploadVideo");

            migrationBuilder.DropTable(
                name: "Answer");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "Quiz");

            migrationBuilder.DropTable(
                name: "SubscribeSubject");

            migrationBuilder.DropTable(
                name: "UploadPdf");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropTable(
                name: "UploadVideo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Answer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Teacher_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isTrue = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Student_id = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Degree = table.Column<int>(type: "int", nullable: false),
                    Question_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Teacher_id = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Quiz",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Degree = table.Column<int>(type: "int", nullable: false),
                    Teacher_id = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quiz", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Account_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Subject_Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubscribeSubject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Student_id = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscribeSubject", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UploadPdf",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PdfName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pdf_Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Teacher_id = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadPdf", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UploadVideo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Teacher_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Video_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Video_Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadVideo", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "QuizSubjects",
                columns: table => new
                {
                    QuizId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizSubjects", x => new { x.QuizId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_QuizSubjects_Quiz_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quiz",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuizSubjects_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_AnswersQuetions_QuestionId",
                table: "AnswersQuetions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizQuetions_QuizId",
                table: "QuizQuetions",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizSubjects_SubjectId",
                table: "QuizSubjects",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubscribeSubject_SubscribeSubjectId",
                table: "StudentSubscribeSubject",
                column: "SubscribeSubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_subjectuploadpdf_UploadPdfId",
                table: "subjectuploadpdf",
                column: "UploadPdfId");

            migrationBuilder.CreateIndex(
                name: "IX_subjectuploadVideo_UploadVideoId",
                table: "subjectuploadVideo",
                column: "UploadVideoId");
        }
    }
}

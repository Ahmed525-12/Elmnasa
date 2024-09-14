using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElmnasaInfrastructure.AppContext.Migraations
{
    public partial class addTeacherSubjecttotablesandrenamemanyhtomanytablesaddquizsubjetcs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Answer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isTrue = table.Column<bool>(type: "bit", nullable: false),
                    Teacher_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Student_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    Teacher_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Question_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Degree = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    Degree = table.Column<int>(type: "int", nullable: false),
                    Teacher_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    Subject_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Account_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    Student_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    Teacher_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pdf_Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PdfName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    Teacher_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Video_Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Video_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "TeacherSubject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherSubject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherSubject_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuizTeacherSubject",
                columns: table => new
                {
                    QuizId = table.Column<int>(type: "int", nullable: false),
                    TeacherSubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizTeacherSubject", x => new { x.QuizId, x.TeacherSubjectId });
                    table.ForeignKey(
                        name: "FK_QuizTeacherSubject_Quiz_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quiz",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuizTeacherSubject_TeacherSubject_TeacherSubjectId",
                        column: x => x.TeacherSubjectId,
                        principalTable: "TeacherSubject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentSubscribeTeacherSubject",
                columns: table => new
                {
                    SubscribeSubjectId = table.Column<int>(type: "int", nullable: false),
                    TeacherSubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSubscribeTeacherSubject", x => new { x.SubscribeSubjectId, x.TeacherSubjectId });
                    table.ForeignKey(
                        name: "FK_StudentSubscribeTeacherSubject_SubscribeSubject_SubscribeSubjectId",
                        column: x => x.SubscribeSubjectId,
                        principalTable: "SubscribeSubject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentSubscribeTeacherSubject_TeacherSubject_TeacherSubjectId",
                        column: x => x.TeacherSubjectId,
                        principalTable: "TeacherSubject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherSubjectuploadpdf",
                columns: table => new
                {
                    TeacherSubjectId = table.Column<int>(type: "int", nullable: false),
                    UploadPdfId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherSubjectuploadpdf", x => new { x.TeacherSubjectId, x.UploadPdfId });
                    table.ForeignKey(
                        name: "FK_TeacherSubjectuploadpdf_TeacherSubject_TeacherSubjectId",
                        column: x => x.TeacherSubjectId,
                        principalTable: "TeacherSubject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherSubjectuploadpdf_UploadPdf_UploadPdfId",
                        column: x => x.UploadPdfId,
                        principalTable: "UploadPdf",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherSubjectuploadVideo",
                columns: table => new
                {
                    TeacherSubjectId = table.Column<int>(type: "int", nullable: false),
                    UploadVideoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherSubjectuploadVideo", x => new { x.TeacherSubjectId, x.UploadVideoId });
                    table.ForeignKey(
                        name: "FK_TeacherSubjectuploadVideo_TeacherSubject_TeacherSubjectId",
                        column: x => x.TeacherSubjectId,
                        principalTable: "TeacherSubject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherSubjectuploadVideo_UploadVideo_UploadVideoId",
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
                name: "IX_QuizTeacherSubject_TeacherSubjectId",
                table: "QuizTeacherSubject",
                column: "TeacherSubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubscribeTeacherSubject_TeacherSubjectId",
                table: "StudentSubscribeTeacherSubject",
                column: "TeacherSubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSubject_SubjectId",
                table: "TeacherSubject",
                column: "SubjectId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSubjectuploadpdf_UploadPdfId",
                table: "TeacherSubjectuploadpdf",
                column: "UploadPdfId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSubjectuploadVideo_UploadVideoId",
                table: "TeacherSubjectuploadVideo",
                column: "UploadVideoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnswersQuetions");

            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropTable(
                name: "QuizQuetions");

            migrationBuilder.DropTable(
                name: "QuizTeacherSubject");

            migrationBuilder.DropTable(
                name: "StudentSubscribeTeacherSubject");

            migrationBuilder.DropTable(
                name: "TeacherSubjectuploadpdf");

            migrationBuilder.DropTable(
                name: "TeacherSubjectuploadVideo");

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
                name: "TeacherSubject");

            migrationBuilder.DropTable(
                name: "UploadVideo");

            migrationBuilder.DropTable(
                name: "Subject");
        }
    }
}

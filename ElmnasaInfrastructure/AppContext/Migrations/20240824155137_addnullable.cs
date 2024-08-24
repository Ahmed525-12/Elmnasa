using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElmnasaInfrastructure.AppContext.Migrations
{
    public partial class addnullable : Migration
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
                name: "FK_Subject_SubscribeSubject_SubscribeSubjectId",
                table: "Subject");

            migrationBuilder.DropForeignKey(
                name: "FK_Subject_UploadPdf_UploadPdfId",
                table: "Subject");

            migrationBuilder.DropForeignKey(
                name: "FK_Subject_UploadVideo_UploadVideoId",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "Question_id",
                table: "Answer");

            migrationBuilder.AlterColumn<int>(
                name: "UploadVideoId",
                table: "Subject",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "UploadPdfId",
                table: "Subject",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SubscribeSubjectId",
                table: "Subject",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "QuizId",
                table: "Subject",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "QuestionId",
                table: "Answer",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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
                name: "FK_Subject_SubscribeSubject_SubscribeSubjectId",
                table: "Subject",
                column: "SubscribeSubjectId",
                principalTable: "SubscribeSubject",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Question_QuestionId",
                table: "Answer");

            migrationBuilder.DropForeignKey(
                name: "FK_Subject_Quiz_QuizId",
                table: "Subject");

            migrationBuilder.DropForeignKey(
                name: "FK_Subject_SubscribeSubject_SubscribeSubjectId",
                table: "Subject");

            migrationBuilder.DropForeignKey(
                name: "FK_Subject_UploadPdf_UploadPdfId",
                table: "Subject");

            migrationBuilder.DropForeignKey(
                name: "FK_Subject_UploadVideo_UploadVideoId",
                table: "Subject");

            migrationBuilder.AlterColumn<int>(
                name: "UploadVideoId",
                table: "Subject",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UploadPdfId",
                table: "Subject",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SubscribeSubjectId",
                table: "Subject",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "QuizId",
                table: "Subject",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "QuestionId",
                table: "Answer",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Question_id",
                table: "Answer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Question_QuestionId",
                table: "Answer",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_Quiz_QuizId",
                table: "Subject",
                column: "QuizId",
                principalTable: "Quiz",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_SubscribeSubject_SubscribeSubjectId",
                table: "Subject",
                column: "SubscribeSubjectId",
                principalTable: "SubscribeSubject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_UploadPdf_UploadPdfId",
                table: "Subject",
                column: "UploadPdfId",
                principalTable: "UploadPdf",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_UploadVideo_UploadVideoId",
                table: "Subject",
                column: "UploadVideoId",
                principalTable: "UploadVideo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

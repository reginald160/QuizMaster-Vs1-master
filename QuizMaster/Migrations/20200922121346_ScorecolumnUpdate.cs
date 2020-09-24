using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QuizMaster.Migrations
{
    public partial class ScorecolumnUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Scores_Examinations_ExaminationId",
                table: "Scores");

            migrationBuilder.DropIndex(
                name: "IX_Scores_ExaminationId",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "ExaminationId",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "RegistrationNumber",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "StudentName",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "StudentScore",
                table: "Scores");

            migrationBuilder.AddColumn<string>(
                name: "CandidateName",
                table: "Scores",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CandidateScore",
                table: "Scores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ExamCode",
                table: "Scores",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TestDate",
                table: "Scores",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ScoreId",
                table: "Examinations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Examinations_ScoreId",
                table: "Examinations",
                column: "ScoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Examinations_Scores_ScoreId",
                table: "Examinations",
                column: "ScoreId",
                principalTable: "Scores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Examinations_Scores_ScoreId",
                table: "Examinations");

            migrationBuilder.DropIndex(
                name: "IX_Examinations_ScoreId",
                table: "Examinations");

            migrationBuilder.DropColumn(
                name: "CandidateName",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "CandidateScore",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "ExamCode",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "TestDate",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "ScoreId",
                table: "Examinations");

            migrationBuilder.AddColumn<int>(
                name: "ExaminationId",
                table: "Scores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RegistrationNumber",
                table: "Scores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StudentName",
                table: "Scores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudentScore",
                table: "Scores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Scores_ExaminationId",
                table: "Scores",
                column: "ExaminationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Scores_Examinations_ExaminationId",
                table: "Scores",
                column: "ExaminationId",
                principalTable: "Examinations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

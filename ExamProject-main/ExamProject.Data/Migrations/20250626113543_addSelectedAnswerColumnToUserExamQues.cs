using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addSelectedAnswerColumnToUserExamQues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SelectedAnswer",
                table: "UserExamQuestion",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SelectedAnswer",
                table: "UserExamQuestion");
        }
    }
}

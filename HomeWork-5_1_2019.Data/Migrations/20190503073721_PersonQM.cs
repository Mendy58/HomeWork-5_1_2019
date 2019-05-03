using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeWork_5_1_2019.Data.Migrations
{
    public partial class PersonQM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionsTags_Questions_QuestionId",
                table: "QuestionsTags");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionsTags_Tags_TagId",
                table: "QuestionsTags");

            migrationBuilder.AddColumn<int>(
                name: "Userid",
                table: "Answers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AnswerLikes",
                columns: table => new
                {
                    Answerid = table.Column<int>(nullable: false),
                    Userid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerLikes", x => new { x.Answerid, x.Userid });
                    table.ForeignKey(
                        name: "FK_AnswerLikes_Answers_Answerid",
                        column: x => x.Answerid,
                        principalTable: "Answers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnswerLikes_User_Userid",
                        column: x => x.Userid,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionLikes",
                columns: table => new
                {
                    Questionid = table.Column<int>(nullable: false),
                    Userid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionLikes", x => new { x.Questionid, x.Userid });
                    table.ForeignKey(
                        name: "FK_QuestionLikes_Questions_Questionid",
                        column: x => x.Questionid,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionLikes_User_Userid",
                        column: x => x.Userid,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_Userid",
                table: "Questions",
                column: "Userid");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_Userid",
                table: "Answers",
                column: "Userid");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerLikes_Userid",
                table: "AnswerLikes",
                column: "Userid");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionLikes_Userid",
                table: "QuestionLikes",
                column: "Userid");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_User_Userid",
                table: "Answers",
                column: "Userid",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_User_Userid",
                table: "Questions",
                column: "Userid",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionsTags_Questions_QuestionId",
                table: "QuestionsTags",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionsTags_Tags_TagId",
                table: "QuestionsTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_User_Userid",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_User_Userid",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionsTags_Questions_QuestionId",
                table: "QuestionsTags");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionsTags_Tags_TagId",
                table: "QuestionsTags");

            migrationBuilder.DropTable(
                name: "AnswerLikes");

            migrationBuilder.DropTable(
                name: "QuestionLikes");

            migrationBuilder.DropIndex(
                name: "IX_Questions_Userid",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Answers_Userid",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "Userid",
                table: "Answers");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionsTags_Questions_QuestionId",
                table: "QuestionsTags",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionsTags_Tags_TagId",
                table: "QuestionsTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

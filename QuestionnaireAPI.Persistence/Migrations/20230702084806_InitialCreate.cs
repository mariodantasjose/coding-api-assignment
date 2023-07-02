using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuestionnaireAPI.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubmittedAnswers",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionnaireId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    AnswerType = table.Column<int>(type: "int", nullable: false),
                    Department = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmittedAnswers", x => new { x.UserId, x.QuestionnaireId, x.SubjectId, x.QuestionId });
                    table.ForeignKey(
                        name: "FK_SubmittedAnswers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Department", "Email", "Name" },
                values: new object[,]
                {
                    { new Guid("0308e2ba-0bff-4777-96c5-bf7f43ed2858"), 4, "williamdoe@company.com", "William Doe" },
                    { new Guid("163a5b8f-f8e7-4cb8-88d1-85a52b4bb7cb"), 1, "francescocorleone@company.com", "Francesco Corleone" },
                    { new Guid("345a8a04-8449-4427-bea3-7c60dadbf00e"), 5, "lizdoe@company.com", "Elizabeth Doe" },
                    { new Guid("43996d7e-17bf-4ddd-8dea-05cddf9dedfb"), 2, "sophiecorleone@company.com", "Sophie Corleone" },
                    { new Guid("4c0b9bcd-b5e2-442a-8db2-c3538d24a219"), 2, "mariedoe@company.com", "Marie Doe" },
                    { new Guid("6bbf92ef-d065-4ae7-9d2f-2f8a023b42e1"), 4, "patrickcorleone@company.com", "Patrick Corleone" },
                    { new Guid("718af9c4-4546-4645-b4bd-739640bb8e5e"), 3, "donvitocorleone@company.com", "Don Vito Corleone" },
                    { new Guid("bca592ae-2abd-4c98-b613-567971ab0004"), 1, "johndoe@company.com", "John Doe" },
                    { new Guid("ed37d0e2-bcf9-4282-a486-a39d1d2fea0c"), 3, "patrickdoe@company.com", "Patrick Doe" },
                    { new Guid("fc73f606-ebc4-4689-a9c4-9339a33d70b9"), 5, "georgescarface@company.com", "George Scarface" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubmittedAnswer_Department_FK_QuestionnaireId_FK_SubjectId_FK_QuestionId",
                table: "SubmittedAnswers",
                columns: new[] { "Department", "QuestionnaireId", "SubjectId", "QuestionId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubmittedAnswers");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

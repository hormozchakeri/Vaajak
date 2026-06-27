using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vaajak.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddSrsFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserVocabProgresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    VocabId = table.Column<Guid>(type: "uuid", nullable: false),
                    PackageId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastRating = table.Column<string>(type: "text", nullable: false),
                    ReviewCount = table.Column<int>(type: "integer", nullable: false),
                    LastReviewedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Interval = table.Column<int>(type: "integer", nullable: false),
                    Repetitions = table.Column<int>(type: "integer", nullable: false),
                    EaseFactor = table.Column<double>(type: "double precision", nullable: false),
                    NextReviewAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVocabProgresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserVocabProgresses_Vocabs_VocabId",
                        column: x => x.VocabId,
                        principalTable: "Vocabs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserVocabProgresses_UserId_VocabId_PackageId",
                table: "UserVocabProgresses",
                columns: new[] { "UserId", "VocabId", "PackageId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserVocabProgresses_VocabId",
                table: "UserVocabProgresses",
                column: "VocabId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserVocabProgresses");
        }
    }
}

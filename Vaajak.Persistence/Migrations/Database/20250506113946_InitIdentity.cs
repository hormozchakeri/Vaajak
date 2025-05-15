using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vaajak.Persistence.Migrations.Database
{
    /// <inheritdoc />
    public partial class InitIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PackageName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vocabs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Vocabulary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Voice = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vocabs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PackageVocab",
                columns: table => new
                {
                    PackageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VocabsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageVocab", x => new { x.PackageId, x.VocabsId });
                    table.ForeignKey(
                        name: "FK_PackageVocab_Packages_PackageId",
                        column: x => x.PackageId,
                        principalTable: "Packages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PackageVocab_Vocabs_VocabsId",
                        column: x => x.VocabsId,
                        principalTable: "Vocabs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Translates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Vocabtran = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Usage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VocabId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Translates_Vocabs_VocabId",
                        column: x => x.VocabId,
                        principalTable: "Vocabs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Examples",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Caseexample = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Exampletran = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Voice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TranslateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examples", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Examples_Translates_TranslateId",
                        column: x => x.TranslateId,
                        principalTable: "Translates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Examples_TranslateId",
                table: "Examples",
                column: "TranslateId");

            migrationBuilder.CreateIndex(
                name: "IX_PackageVocab_VocabsId",
                table: "PackageVocab",
                column: "VocabsId");

            migrationBuilder.CreateIndex(
                name: "IX_Translates_VocabId",
                table: "Translates",
                column: "VocabId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Examples");

            migrationBuilder.DropTable(
                name: "PackageVocab");

            migrationBuilder.DropTable(
                name: "Translates");

            migrationBuilder.DropTable(
                name: "Packages");

            migrationBuilder.DropTable(
                name: "Vocabs");
        }
    }
}

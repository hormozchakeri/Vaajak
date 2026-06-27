using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vaajak.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddVocabCsvFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Antonyms",
                table: "Vocabs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Example1",
                table: "Vocabs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Example2",
                table: "Vocabs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Example3",
                table: "Vocabs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageFile",
                table: "Vocabs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IpaPronunciation",
                table: "Vocabs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersianMeaning",
                table: "Vocabs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Synonyms",
                table: "Vocabs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WordFamily",
                table: "Vocabs",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Antonyms",
                table: "Vocabs");

            migrationBuilder.DropColumn(
                name: "Example1",
                table: "Vocabs");

            migrationBuilder.DropColumn(
                name: "Example2",
                table: "Vocabs");

            migrationBuilder.DropColumn(
                name: "Example3",
                table: "Vocabs");

            migrationBuilder.DropColumn(
                name: "ImageFile",
                table: "Vocabs");

            migrationBuilder.DropColumn(
                name: "IpaPronunciation",
                table: "Vocabs");

            migrationBuilder.DropColumn(
                name: "PersianMeaning",
                table: "Vocabs");

            migrationBuilder.DropColumn(
                name: "Synonyms",
                table: "Vocabs");

            migrationBuilder.DropColumn(
                name: "WordFamily",
                table: "Vocabs");
        }
    }
}

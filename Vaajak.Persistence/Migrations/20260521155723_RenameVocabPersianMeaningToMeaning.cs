using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vaajak.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RenameVocabPersianMeaningToMeaning : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PersianMeaning",
                table: "Vocabs",
                newName: "Meaning");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Meaning",
                table: "Vocabs",
                newName: "PersianMeaning");
        }
    }
}

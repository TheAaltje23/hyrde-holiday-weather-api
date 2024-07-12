using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hyrde.Challenge.Migrations
{
    /// <inheritdoc />
    public partial class MakeUsernameCaseSensitive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "User",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                collation: "utf8mb4_bin",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldCollation: "utf8_bin")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "User",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                collation: "utf8_bin",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldCollation: "utf8mb4_bin")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}

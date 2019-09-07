using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiBookLibrary.Migrations
{
    public partial class Books_second2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "Books",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Books",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Books",
                newName: "id");

            migrationBuilder.AlterColumn<int>(
                name: "Title",
                table: "Books",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}

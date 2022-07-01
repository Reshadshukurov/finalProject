using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProject.Migrations
{
    public partial class latestUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Subitle",
                table: "LatestWork",
                newName: "SubTitle");

            migrationBuilder.RenameColumn(
                name: "Imgae",
                table: "BigScreen",
                newName: "Image");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "LatestWork",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "LatestWork");

            migrationBuilder.RenameColumn(
                name: "SubTitle",
                table: "LatestWork",
                newName: "Subitle");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "BigScreen",
                newName: "Imgae");
        }
    }
}

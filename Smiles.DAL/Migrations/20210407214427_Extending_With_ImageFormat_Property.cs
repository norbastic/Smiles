using Microsoft.EntityFrameworkCore.Migrations;

namespace Smiles.DAL.Migrations
{
    public partial class Extending_With_ImageFormat_Property : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageFormat",
                table: "SmilesEntities",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageFormat",
                table: "SmilesEntities");
        }
    }
}

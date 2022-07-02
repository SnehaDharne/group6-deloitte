using Microsoft.EntityFrameworkCore.Migrations;

namespace Deloitte_Project.Migrations
{
    public partial class migration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Metadata",
                table: "Metadata");

            migrationBuilder.RenameTable(
                name: "Metadata",
                newName: "Metadatas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Metadatas",
                table: "Metadatas",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Metadatas",
                table: "Metadatas");

            migrationBuilder.RenameTable(
                name: "Metadatas",
                newName: "Metadata");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Metadata",
                table: "Metadata",
                column: "Id");
        }
    }
}

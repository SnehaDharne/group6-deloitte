using Microsoft.EntityFrameworkCore.Migrations;

namespace Deloitte_Project.Migrations
{
    public partial class createMetadata3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Metadata",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    file_name = table.Column<string>(nullable: true),
                    file_size = table.Column<long>(nullable: false),
                    sub_directory = table.Column<string>(nullable: true),
                    created_on = table.Column<string>(nullable: true),
                    created_by = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metadata", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Metadata");
        }
    }
}

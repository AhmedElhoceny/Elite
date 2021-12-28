using Microsoft.EntityFrameworkCore.Migrations;

namespace Elite.Migrations
{
    public partial class AddNewColumnsToTaskTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Argent",
                table: "Tasks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Argent",
                table: "Tasks");
        }
    }
}

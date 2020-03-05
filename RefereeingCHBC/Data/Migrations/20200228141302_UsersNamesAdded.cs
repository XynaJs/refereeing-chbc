using Microsoft.EntityFrameworkCore.Migrations;

namespace RefereeingCHBC.Data.Migrations
{
    public partial class UsersNamesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NameFirst",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameLast",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameFirst",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NameLast",
                table: "AspNetUsers");
        }
    }
}

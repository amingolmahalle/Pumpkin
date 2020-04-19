using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sample.Test.Migrations
{
    public partial class CreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fullname = table.Column<string>(type: "nvarchar(40)", maxLength: 30, nullable: false),
                    MobileNumber = table.Column<string>(type: "char(11)", maxLength: 11, nullable: false),
                    NationalCode = table.Column<string>(type: "char(10)", maxLength: 10, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    SubmitTime = table.Column<DateTime>(nullable: false),
                    LastUpdateTime = table.Column<DateTime>(nullable: true),
                    SubmitUser = table.Column<int>(nullable: false),
                    LastUpdateUser = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SampleWebApi.Data.SqlServer.Migrations
{
    public partial class initialize_database : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fullname = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    MobileNumber = table.Column<string>(type: "char(11)", maxLength: 11, nullable: false),
                    NationalCode = table.Column<string>(type: "char(10)", maxLength: 10, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    RemovedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemovedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BirthDate", "CreatedAt", "CreatedBy", "Deleted", "Email", "Fullname", "MobileNumber", "ModifiedAt", "ModifiedBy", "NationalCode", "RemovedAt", "RemovedBy", "Status" },
                values: new object[] { 10001, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 1, 1, 10, 0, 0, 0, DateTimeKind.Utc), 10001L, false, "user1@gmail.com", "user 1", "09120000001", new DateTime(2022, 1, 1, 10, 0, 0, 0, DateTimeKind.Utc), 10001L, "5820005546", null, null, false });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BirthDate", "CreatedAt", "CreatedBy", "Deleted", "Email", "Fullname", "MobileNumber", "ModifiedAt", "ModifiedBy", "NationalCode", "RemovedAt", "RemovedBy", "Status" },
                values: new object[] { 10002, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 1, 1, 10, 0, 0, 0, DateTimeKind.Utc), 10001L, false, "user2@gmail.com", "user 2", "09120000002", new DateTime(2022, 1, 1, 10, 0, 0, 0, DateTimeKind.Utc), 10001L, "5820005545", null, null, false });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Pumpkin.Infrastructure.Contexts.Migrations
{
    /// <inheritdoc />
    public partial class initialize_database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Order",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    basket_code = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: true),
                    payment_tracking_code = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: true),
                    customer_first_name = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: true),
                    customer_last_name = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: true),
                    customer_mobile_number = table.Column<string>(type: "CHAR(11)", maxLength: 11, nullable: true),
                    customer_national_code = table.Column<string>(type: "VARCHAR(10)", maxLength: 10, nullable: true),
                    customer_address = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: true),
                    customer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    current_state = table.Column<short>(type: "SMALLINT", nullable: false, defaultValue: (short)100),
                    payment_state = table.Column<short>(type: "SMALLINT", nullable: false, defaultValue: (short)10),
                    order_date = table.Column<DateTime>(type: "DateTime", nullable: false),
                    cancel_deadline = table.Column<DateTime>(type: "datetime2", nullable: false),
                    is_confirmed = table.Column<bool>(type: "BIT", nullable: false, defaultValue: false),
                    is_paid = table.Column<bool>(type: "BIT", nullable: false, defaultValue: false),
                    total_product_price = table.Column<decimal>(type: "DECIMAL(18,0)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    modified_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modified_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    removed_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    removed_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    first_name = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: true),
                    last_name = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: true),
                    mobile_number = table.Column<string>(type: "CHAR(11)", maxLength: 11, nullable: true),
                    national_code = table.Column<string>(type: "VARCHAR(10)", maxLength: 10, nullable: true),
                    address = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: true),
                    gender = table.Column<bool>(type: "BIT", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    modified_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modified_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    removed_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    removed_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    basket_item_code = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: true),
                    product_category = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: true),
                    product_brand = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: true),
                    product_model = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: true),
                    device_serial_number = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: true),
                    product_price = table.Column<decimal>(type: "DECIMAL(18,0)", nullable: false),
                    order_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    modified_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modified_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    removed_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    removed_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_item", x => x.id);
                    table.ForeignKey(
                        name: "fk_order_orderItem",
                        column: x => x.order_id,
                        principalSchema: "dbo",
                        principalTable: "Order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Policy",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    policy_number = table.Column<long>(type: "bigint", nullable: false),
                    customer_first_name = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: true),
                    customer_last_name = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: true),
                    customer_mobile_number = table.Column<string>(type: "CHAR(11)", maxLength: 11, nullable: true),
                    customer_national_code = table.Column<string>(type: "VARCHAR(10)", maxLength: 10, nullable: true),
                    customer_address = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: true),
                    customer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    issued_at = table.Column<DateTime>(type: "DateTime", nullable: true),
                    start_at = table.Column<DateTime>(type: "DateTime", nullable: true),
                    expire_at = table.Column<DateTime>(type: "DateTime", nullable: true),
                    is_active = table.Column<bool>(type: "BIT", nullable: false, defaultValue: false),
                    current_state = table.Column<short>(type: "SMALLINT", nullable: false, defaultValue: (short)100),
                    order_item_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    modified_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modified_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    removed_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    removed_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_policy", x => x.id);
                    table.ForeignKey(
                        name: "fk_Policy_orderItem",
                        column: x => x.order_item_id,
                        principalSchema: "dbo",
                        principalTable: "OrderItem",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "User",
                columns: new[] { "id", "address", "created_at", "created_by", "first_name", "gender", "last_name", "mobile_number", "modified_at", "modified_by", "national_code", "removed_at", "removed_by" },
                values: new object[,]
                {
                    { new Guid("3a94f7d5-c621-4713-8c5a-656f12ba43b1"), "Westerville, 2044 Winding Way Street", new DateTime(2023, 7, 6, 13, 25, 30, 66, DateTimeKind.Local).AddTicks(8615), new Guid("3a94f7d5-c621-4713-8c5a-656f12ba43b1"), "Jason", true, "Momoa", "09123456789", null, null, "9999999999", null, null },
                    { new Guid("f9ca67a1-9bbc-4889-8fa7-21c4847fa51f"), "Tehran, Azadi Street", new DateTime(2023, 7, 6, 13, 25, 30, 66, DateTimeKind.Local).AddTicks(8555), new Guid("f9ca67a1-9bbc-4889-8fa7-21c4847fa51f"), "Amin", true, "Golmahalleh", "09365545252", null, null, "1111111111", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "ix_order_basket_code",
                schema: "dbo",
                table: "Order",
                column: "basket_code",
                unique: true,
                filter: "[basket_code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "ix_order_item_order_id",
                schema: "dbo",
                table: "OrderItem",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "ix_policy_order_item_id",
                schema: "dbo",
                table: "Policy",
                column: "order_item_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Policy",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "User",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "OrderItem",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Order",
                schema: "dbo");
        }
    }
}

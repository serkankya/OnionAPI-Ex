using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class init_sec : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryProduct");

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => new { x.ProductId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_ProductCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCategories_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2025, 10, 19, 20, 49, 48, 19, DateTimeKind.Local).AddTicks(1167), "Movies, Computers & Music" });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2025, 10, 19, 20, 49, 48, 24, DateTimeKind.Local).AddTicks(3370), "Tools, Baby & Computers" });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2025, 10, 19, 20, 49, 48, 24, DateTimeKind.Local).AddTicks(3566), "Grocery, Kids & Clothing" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 19, 20, 49, 48, 26, DateTimeKind.Local).AddTicks(512));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 19, 20, 49, 48, 26, DateTimeKind.Local).AddTicks(784));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 19, 20, 49, 48, 26, DateTimeKind.Local).AddTicks(786));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 19, 20, 49, 48, 26, DateTimeKind.Local).AddTicks(788));

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Description", "Title" },
                values: new object[] { new DateTime(2025, 10, 19, 20, 49, 48, 45, DateTimeKind.Local).AddTicks(8452), "Masanın türemiş eaque tempora türemiş quis.", "Otobüs aut." });

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Description", "Title" },
                values: new object[] { new DateTime(2025, 10, 19, 20, 49, 48, 45, DateTimeKind.Local).AddTicks(8566), "Qui domates inventore autem dağılımı aut.", "Mi." });

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Description", "Title" },
                values: new object[] { new DateTime(2025, 10, 19, 20, 49, 48, 45, DateTimeKind.Local).AddTicks(8684), "Sarmal düşünüyor sed hesap quia.", "Sit." });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Description", "Discount", "Price", "Title" },
                values: new object[] { new DateTime(2025, 10, 19, 20, 49, 48, 60, DateTimeKind.Local).AddTicks(2064), "The Football Is Good For Training And Recreational Purposes", 9.994955364037280m, 250.65m, "Licensed Frozen Hat" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Description", "Discount", "Price", "Title" },
                values: new object[] { new DateTime(2025, 10, 19, 20, 49, 48, 60, DateTimeKind.Local).AddTicks(2189), "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", 0.5179161912787730m, 438.67m, "Rustic Granite Bike" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_CategoryId",
                table: "ProductCategories",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.CreateTable(
                name: "CategoryProduct",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "int", nullable: false),
                    ProductsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryProduct", x => new { x.CategoriesId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_CategoryProduct_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryProduct_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2025, 10, 16, 20, 2, 49, 739, DateTimeKind.Local).AddTicks(4261), "Tools & Outdoors" });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2025, 10, 16, 20, 2, 49, 745, DateTimeKind.Local).AddTicks(61), "Clothing & Outdoors" });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2025, 10, 16, 20, 2, 49, 745, DateTimeKind.Local).AddTicks(445), "Sports, Home & Tools" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 16, 20, 2, 49, 746, DateTimeKind.Local).AddTicks(7882));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 16, 20, 2, 49, 746, DateTimeKind.Local).AddTicks(8262));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 16, 20, 2, 49, 746, DateTimeKind.Local).AddTicks(8265));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 16, 20, 2, 49, 746, DateTimeKind.Local).AddTicks(8267));

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Description", "Title" },
                values: new object[] { new DateTime(2025, 10, 16, 20, 2, 49, 773, DateTimeKind.Local).AddTicks(5302), "Kapının sarmal qui consequatur consequuntur ab.", "Dicta veniam." });

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Description", "Title" },
                values: new object[] { new DateTime(2025, 10, 16, 20, 2, 49, 773, DateTimeKind.Local).AddTicks(5438), "Anlamsız in enim deleniti nihil nesciunt.", "Ötekinden." });

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Description", "Title" },
                values: new object[] { new DateTime(2025, 10, 16, 20, 2, 49, 773, DateTimeKind.Local).AddTicks(5487), "Ama ullam ekşili amet beğendim.", "Quia." });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Description", "Discount", "Price", "Title" },
                values: new object[] { new DateTime(2025, 10, 16, 20, 2, 49, 778, DateTimeKind.Local).AddTicks(2460), "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", 1.963961603370330m, 186.18m, "Incredible Plastic Tuna" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Description", "Discount", "Price", "Title" },
                values: new object[] { new DateTime(2025, 10, 16, 20, 2, 49, 778, DateTimeKind.Local).AddTicks(2641), "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", 5.6423784726960m, 517.94m, "Ergonomic Plastic Computer" });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryProduct_ProductsId",
                table: "CategoryProduct",
                column: "ProductsId");
        }
    }
}
